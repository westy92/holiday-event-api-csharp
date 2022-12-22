using Newtonsoft.Json;

/// <summary>
/// Information about an Event's Pattern
/// </summary>
public class Pattern
{
    /// <summary>
    /// The first year this event is observed (null implies none or unknown)
    /// </summary>
    [JsonProperty("first_year")]
    public int? FirstYear { get; set; }
    /// <summary>
    /// The last year this event is observed (null implies none or unknown)
    /// </summary>
    [JsonProperty("last_year")]
    public int? LastYear { get; set; }
    /// <summary>
    /// A description of how this event is observed (formatted as plain text)
    /// </summary>
    [JsonProperty(Required = Required.Always)]
    public string Observed { get; set; } = null!;
    /// <summary>
    /// A description of how this event is observed (formatted as HTML)
    /// </summary>
    [JsonProperty("observed_html", Required = Required.Always)]
    public string ObservedHtml { get; set; } = null!;
    /// <summary>
    /// A description of how this event is observed (formatted as Markdown)
    /// </summary>
    [JsonProperty("observed_markdown", Required = Required.Always)]
    public string ObservedMarkdown { get; set; } = null!;
    /// <summary>
    /// For how many days this event is celebrated
    /// </summary>
    [JsonProperty(Required = Required.Always)]
    public int Length { get; set; }
}
