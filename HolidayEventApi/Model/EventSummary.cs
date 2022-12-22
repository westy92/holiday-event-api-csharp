using Newtonsoft.Json;

/// <summary>
/// Information about an Event
/// </summary>
public class EventSummary
{
    /// <summary>
    /// The Event Id
    /// </summary>
    [JsonProperty(Required = Required.Always)]
    public string Id { get; set; } = null!;
    /// <summary>
    /// The Event Name
    /// </summary>
    [JsonProperty(Required = Required.Always)]
    public string Name { get; set; } = null!;
    /// <summary>
    /// The Event URL
    /// </summary>
    [JsonProperty(Required = Required.Always)]
    public string Url { get; set; } = null!;
}
