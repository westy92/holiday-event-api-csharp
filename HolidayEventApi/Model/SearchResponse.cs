using Newtonsoft.Json;
using System.Collections.Generic;

public class SearchResponse
{
    public bool Adult { get; set; }
    public string Query { get; set; } = null!;
    public string Timezone { get; set; } = null!;
    public List<EventSummary> Events { get; set; } = null!;
}
