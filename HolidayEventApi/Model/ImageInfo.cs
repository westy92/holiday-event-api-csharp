using Newtonsoft.Json;

public class ImageInfo
{
    [JsonProperty(Required = Required.Always)]
    public string Small { get; set; } = null!;
    [JsonProperty(Required = Required.Always)]
    public string Medium { get; set; } = null!;
    [JsonProperty(Required = Required.Always)]
    public string Large { get; set; } = null!;
}
