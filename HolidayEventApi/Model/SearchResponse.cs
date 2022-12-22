using Newtonsoft.Json;
using System.Collections.Generic;

/// <summary>
/// The Response returned by Search
/// </summary>
public class SearchResponse: StandardResponse
{
    /// <summary>
    /// The search query
    /// </summary>
    [JsonProperty(Required = Required.Always)]
    public string Query { get; set; } = null!;
    /// <summary>
    /// Whether Adult entries can be included
    /// </summary>
    [JsonProperty(Required = Required.Always)]
    public bool Adult { get; set; }
    /// <summary>
    /// The found Events
    /// </summary>
    [JsonProperty(Required = Required.Always)]
    public List<EventSummary> Events { get; set; } = null!;
}
