using Newtonsoft.Json;
using System.Collections.Generic;

public class SearchResponse
{
    public bool Adult { get; set; }
    public string Query { get; set; }
    public string Timezone { get; set; }
    public List<EventSummary> Events { get; set; }
}
