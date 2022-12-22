using Newtonsoft.Json;

/// <summary>
/// Your API plan's current Rate Limit and status. Upgrade to increase these limits.
/// </summary>
public class RateLimit
{
    /// <summary>
    /// The amount of requests allowed this month
    /// </summary>
    [JsonProperty(Required = Required.Always)]
    public int LimitMonth { get; set; }
    /// <summary>
    /// The amount of requests remaining this month
    /// </summary>
    [JsonProperty(Required = Required.Always)]
    public int RemainingMonth { get; set; }
}
