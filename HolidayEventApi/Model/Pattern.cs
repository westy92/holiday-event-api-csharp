using Newtonsoft.Json;

public class Pattern
{
    [JsonProperty("first_year")]
    public int? FirstYear { get; set; }
    [JsonProperty("last_year")]
    public int? LastYear { get; set; }
    [JsonProperty(Required = Required.Always)]
    public string Observed { get; set; } = null!;
    [JsonProperty("observed_html", Required = Required.Always)]
    public string ObservedHtml { get; set; } = null!;
    [JsonProperty("observed_markdown", Required = Required.Always)]
    public string ObservedMarkdown { get; set; } = null!;
    [JsonProperty(Required = Required.Always)]
    public int Length { get; set; }
}
