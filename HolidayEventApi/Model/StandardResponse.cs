using Newtonsoft.Json;

/// <summary>
/// The API's standard response
/// </summary>
public class StandardResponse
{
    /// <summary>
    /// The API plan's current rate limit and status
    /// </summary>
    [JsonProperty(Required = Required.Always)]
    public RateLimit RateLimit { get; set; } = null!;
}
