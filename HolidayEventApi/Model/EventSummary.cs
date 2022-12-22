using Newtonsoft.Json;

public class EventSummary
{
    [JsonProperty(Required = Required.Always)]
    public string Id { get; set; } = null!;
    [JsonProperty(Required = Required.Always)]
    public string Name { get; set; } = null!;
    [JsonProperty(Required = Required.Always)]
    public string Url { get; set; } = null!;
}
