using Newtonsoft.Json;
using System.Collections.Generic;

public class SearchResponse
{
    [JsonProperty(Required = Required.Always)]
    public bool Adult { get; set; }
    [JsonProperty(Required = Required.Always)]
    public string Query { get; set; } = null!;
    [JsonProperty(Required = Required.Always)]
    public List<EventSummary> Events { get; set; } = null!;
}
