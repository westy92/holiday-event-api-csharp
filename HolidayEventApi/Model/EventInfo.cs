using System.Collections.Generic;
using Newtonsoft.Json;

public class EventInfo: EventSummary
{
    [JsonProperty("alternate_names", Required = Required.Always)]
    public List<AlternateName> AlternateNames { get; set; } = null!;
    public List<string>? Hashtags { get; set; }
    public ImageInfo? Image { get; set; }
    public List<string>? Sources { get; set; }
    public RichText? Description { get; set; }
    [JsonProperty("how_to_observe")]
    public RichText? HowToObserve { get; set; }
    public List<Pattern>? Patterns { get; set; }
    public List<FounderInfo>? Founders { get; set; }
    public List<Occurrence>? Occurrences { get; set; }
}
