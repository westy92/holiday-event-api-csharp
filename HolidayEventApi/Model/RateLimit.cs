using Newtonsoft.Json;

public class RateLimit
{
    [JsonProperty(Required = Required.Always)]
    public int LimitMonth { get; set; }
    [JsonProperty(Required = Required.Always)]
    public int RemainingMonth { get; set; }
}
