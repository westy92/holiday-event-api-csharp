using Newtonsoft.Json;
using System.Collections.Generic;

public class GetEventsResponse
{
    public bool Adult { get; set; }
    public string Date { get; set; }
    public string Timezone { get; set; }
    public List<EventSummary> Events { get; set; }
    [JsonProperty("multiday_starting")]
    public List<EventSummary> MultidayStarting { get; set; }
   [JsonProperty("multiday_ongoing")]
   public List<EventSummary> MultidayOngoing { get; set; }
}
