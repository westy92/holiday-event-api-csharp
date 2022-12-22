using Newtonsoft.Json;
using System.Collections.Generic;

public class GetEventsResponse
{
    public bool Adult { get; set; }
    public string Date { get; set; } = null!;
    public string Timezone { get; set; } = null!;
    public List<EventSummary> Events { get; set; } = null!;
    [JsonProperty("multiday_starting")]
    public List<EventSummary>? MultidayStarting { get; set; }
   [JsonProperty("multiday_ongoing")]
   public List<EventSummary>? MultidayOngoing { get; set; }
}
