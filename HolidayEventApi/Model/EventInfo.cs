using System.Collections.Generic;
using Newtonsoft.Json;

/// <summary>
/// Information about an Event
/// </summary>
public class EventInfo: EventSummary
{
    /// <summary>
    /// Whether this Event is unsafe for children or viewing at work
    /// </summary>
    public bool Adult { get; set; }
    /// <summary>
    /// The Event's Alternate Names
    /// </summary>
    [JsonProperty("alternate_names", Required = Required.Always)]
    public List<AlternateName> AlternateNames { get; set; } = null!;
    /// <summary>
    /// The Event's hashtags
    /// </summary>
    public List<string>? Hashtags { get; set; }
    /// <summary>
    /// The Event's images
    /// </summary>
    public ImageInfo? Image { get; set; }
    /// <summary>
    /// The Event's sources
    /// </summary>
    public List<string>? Sources { get; set; }
    /// <summary>
    /// The Event's description
    /// </summary>
    public RichText? Description { get; set; }
    /// <summary>
    /// How to observe the Event
    /// </summary>
    [JsonProperty("how_to_observe")]
    public RichText? HowToObserve { get; set; }
    /// <summary>
    /// Patterns defining when the Event is observed
    /// </summary>
    public List<Pattern>? Patterns { get; set; }
    /// <summary>
    /// The Event's founders
    /// </summary>
    public List<FounderInfo>? Founders { get; set; }
    /// <summary>
    /// The Event's tags
    /// </summary>
    public List<Tag>? Tags { get; set; }
    /// <summary>
    /// The Event Occurrences (when it occurs)
    /// </summary>
    public List<Occurrence>? Occurrences { get; set; }
}
