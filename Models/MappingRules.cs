namespace ConvertirXml.Models
{
    public sealed record MappingRules(
    string RowTag,
    string OutputMode,
    string OutputSeparator,
    bool PrintLabels,
    List<FieldRule> Fields
);
}