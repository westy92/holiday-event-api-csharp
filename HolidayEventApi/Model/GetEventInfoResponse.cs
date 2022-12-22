using Newtonsoft.Json;

/// <summary>
/// The Response returned by GetEventInfo
/// </summary>
public class GetEventInfoResponse: StandardResponse
{
    /// <summary>
    /// The Event Info
    /// </summary>
    [JsonProperty(Required = Required.Always)]
    public EventInfo Event { get; set; } = null!;
}
