using Newtonsoft.Json;

public class FounderInfo
{
    [JsonProperty(Required = Required.Always)]
    public string Name { get; set; } = null!;
    public string? Url { get; set; }
    public string? Date { get; set; }
}
