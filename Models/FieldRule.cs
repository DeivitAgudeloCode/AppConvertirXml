namespace ConvertirXml.Models
{
    public sealed record FieldRule(
        string Tag,
        string? Label,
        string? Format,
        string? DefaultValue
    );
}