using Newtonsoft.Json;

/// <summary>
/// Information about an Event's Alternate Name
/// </summary>
public class AlternateName
{
    /// <summary>
    /// An Event's Alternate Name
    /// </summary>
    [JsonProperty(Required = Required.Always)]
    public string Name { get; set; } = null!;
    /// <summary>
    /// The first year this Alternate Name was in effect (null implies none or unknown)
    /// </summary>
    [JsonProperty("first_year")]
    public int? FirstYear { get; set; }
    /// <summary>
    /// The last year this Alternate Name was in effect (null implies none or unknown)
    /// </summary>
    [JsonProperty("last_year")]
    public int? LastYear { get; set; }
}
