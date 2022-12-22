using Newtonsoft.Json;

/// <summary>
/// Information about an Event Founder
/// </summary>
public class FounderInfo
{
    /// <summary>
    /// The Founder's name
    /// </summary>
    [JsonProperty(Required = Required.Always)]
    public string Name { get; set; } = null!;
    /// <summary>
    /// A link to the Founder
    /// </summary>
    public string? Url { get; set; }
    /// <summary>
    /// The date the Event was founded
    /// </summary>
    public string? Date { get; set; }
}
