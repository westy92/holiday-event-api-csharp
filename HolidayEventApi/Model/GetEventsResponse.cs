using Newtonsoft.Json;
using System.Collections.Generic;

/// <summary>
/// The Response returned by GetEvents
/// </summary>
public class GetEventsResponse: StandardResponse
{
    /// <summary>
    /// Whether Adult entries can be included
    /// </summary>
    [JsonProperty(Required = Required.Always)]
    public bool Adult { get; set; }
    /// <summary>
    /// The Date string
    /// </summary>
    [JsonProperty(Required = Required.Always)]
    public string Date { get; set; } = null!;
    /// <summary>
    /// The Timezone used to calculate the Date's Events
    /// </summary>
    [JsonProperty(Required = Required.Always)]
    public string Timezone { get; set; } = null!;
    /// <summary>
    /// The Date's Events
    /// </summary>
    [JsonProperty(Required = Required.Always)]
    public List<EventSummary> Events { get; set; } = null!;
    /// <summary>
    /// Multi-day Events that start on Date
    /// </summary>
    [JsonProperty("multiday_starting")]
    public List<EventSummary>? MultidayStarting { get; set; }
    /// <summary>
    /// Multi-day Events that are continuing their observance on Date
    /// </summary>
   [JsonProperty("multiday_ongoing")]
   public List<EventSummary>? MultidayOngoing { get; set; }
}
