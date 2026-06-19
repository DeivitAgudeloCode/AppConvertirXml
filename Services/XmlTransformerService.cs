using ConvertirXml.Models;
using System.Text;
using System.Text.Json;
using System.Xml.Linq;

namespace ConvertirXml.Services
{
    public class XmlTransformerService
    {
        private readonly MappingRules _mapping;

        public XmlTransformerService()
        {
            var mapPath = "reglas.mapeo.json";
            if (!File.Exists(mapPath)) throw new FileNotFoundException($"No existe el archivo: {mapPath}");

            _mapping = JsonSerializer.Deserialize<MappingRules>(
                File.ReadAllText(mapPath),
                new JsonSerializerOptions
                {
                    PropertyNameCaseInsensitive = true
                }) ?? throw new Exception("No se pudieron cargar las reglas.");
        }

        public string Convert(
            string xmlContent,
            string nbPersonal,
            string nbPersonalSecun)
        {
            if (string.IsNullOrWhiteSpace(xmlContent))
                return string.Empty;

            xmlContent = xmlContent.Trim();

            // Permite múltiples ROW sin nodo raíz
            if (!xmlContent.StartsWith("<ROOT>", StringComparison.OrdinalIgnoreCase))
            {
                xmlContent = $"<ROOT>{xmlContent}</ROOT>";
            }

            XDocument doc;

            try
            {
                doc = XDocument.Parse(xmlContent);
            }
            catch (Exception ex)
            {
                throw new Exception($"Error parseando XML: {ex.Message}");
            }

            var rows = doc
                .Descendants()
                .Where(x =>
                    x.Name.LocalName.Equals(
                        _mapping.RowTag,
                        StringComparison.OrdinalIgnoreCase))
                .ToList();

            var sb = new StringBuilder();

            sb.AppendLine($"tw.local.{nbPersonal} = new tw.object.listOf.{nbPersonalSecun}();");

            int idx = 0;

            foreach (var row in rows)
            {
                sb.Append(
                    BuildSevenTodoBlock(
                        row,
                        _mapping,
                        idx,
                        nbPersonal,
                        nbPersonalSecun));
                idx++;
            }
            return sb.ToString();
        }

        private static string BuildSevenTodoBlock(
            XElement row,
            MappingRules rules,
            int i,
            string nbPersonal,
            string nbPersonalSecun)
        {
            var values = row.Elements()
                .ToDictionary(
                    e => e.Name.LocalName,
                    e => e.Value?.Trim() ?? "",
                    StringComparer.OrdinalIgnoreCase);

            var sb = new StringBuilder();

            sb.AppendLine($"\t\ttw.local.{nbPersonal}[{i}] = new tw.object.{nbPersonalSecun}();");

            if (values.TryGetValue("FECHA", out var fechaRaw) &&
                !string.IsNullOrWhiteSpace(fechaRaw))
            {
                var fechaTmp = fechaRaw.Replace("-", "/");

                sb.AppendLine($"\t\tvar fechaTmp = '{Escape(fechaTmp)}';");
                sb.AppendLine($"\t\ttw.local.{nbPersonal}[{i}].FECHA = new TWDate();");
                sb.AppendLine($"\t\ttw.local.{nbPersonal}[{i}].FECHA.parse(fechaTmp,\"yyyy/MM/dd\");");
            }

            foreach (var field in rules.Fields)
            {
                if (field.Tag.Equals(
                    "FECHA",
                    StringComparison.OrdinalIgnoreCase))
                    continue;

                values.TryGetValue(field.Tag, out var value);

                value ??= "";

                value = Apply(value, field.Format);

                if (string.IsNullOrWhiteSpace(value))
                    value = field.DefaultValue ?? "";

                sb.AppendLine(
                    $"\t\ttw.local.{nbPersonal}[{i}].{field.Tag} = '{Escape(value)}';");
            }

            return sb.ToString();
        }

        private static string Escape(string value)
        {
            return value.Replace("\\", "\\\\").Replace("'", "\\'");
        }

        private static string Apply(string value, string? format)
        {
            if (string.IsNullOrWhiteSpace(format))
                return value;

            var parts = format.Split('|', StringSplitOptions.RemoveEmptyEntries | StringSplitOptions.TrimEntries);
            var result = value;

            foreach (var part in parts)
            {
                if (part.Equals("trim", StringComparison.OrdinalIgnoreCase))
                    result = result.Trim();
                else if (part.Equals("upper", StringComparison.OrdinalIgnoreCase))
                    result = result.ToUpperInvariant();
                else if (part.Equals("lower", StringComparison.OrdinalIgnoreCase))
                    result = result.ToLowerInvariant();
            }
            return result;
        }
    }
}