using Newtonsoft.Json;

/// <summary>
/// Information about an Event's Occurrence
/// </summary>
public class Occurrence
{
    /// <summary>
    /// The date or timestamp the Event occurs
    /// </summary>
    [JsonProperty(Required = Required.Always)]
    public string Date { get; set; } = null!;
    /// <summary>
    /// The length (in days) of the Event occurrence
    /// </summary>
    [JsonProperty(Required = Required.Always)]
    public int Length { get; set; }
}
