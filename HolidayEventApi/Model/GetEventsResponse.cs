using Newtonsoft.Json;
using System.Collections.Generic;

public class GetEventsResponse: StandardResponse
{
    [JsonProperty(Required = Required.Always)]
    public bool Adult { get; set; }
    [JsonProperty(Required = Required.Always)]
    public string Date { get; set; } = null!;
    [JsonProperty(Required = Required.Always)]
    public string Timezone { get; set; } = null!;
    [JsonProperty(Required = Required.Always)]
    public List<EventSummary> Events { get; set; } = null!;
    [JsonProperty("multiday_starting")]
    public List<EventSummary>? MultidayStarting { get; set; }
   [JsonProperty("multiday_ongoing")]
   public List<EventSummary>? MultidayOngoing { get; set; }
}
