using Newtonsoft.Json;

public class GetEventInfoResponse: StandardResponse
{
    [JsonProperty(Required = Required.Always)]
    public EventInfo Event { get; set; } = null!;
}
