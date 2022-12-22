using Newtonsoft.Json;

public class StandardResponse
{
    [JsonProperty(Required = Required.Always)]
    public RateLimit RateLimit { get; set; } = null!;
}
