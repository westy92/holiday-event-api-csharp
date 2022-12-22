using Newtonsoft.Json;

public class Occurrence
{
    [JsonProperty(Required = Required.Always)]
    public string Date { get; set; } = null!;
    [JsonProperty(Required = Required.Always)]
    public int Length { get; set; }
}
