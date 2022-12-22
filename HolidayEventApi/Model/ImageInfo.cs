using Newtonsoft.Json;

/// <summary>
/// Information about an Event image
/// </summary>
public class ImageInfo
{
    /// <summary>
    /// A small image
    /// </summary>
    [JsonProperty(Required = Required.Always)]
    public string Small { get; set; } = null!;
    /// <summary>
    /// A medium image
    /// </summary>
    [JsonProperty(Required = Required.Always)]
    public string Medium { get; set; } = null!;
    /// <summary>
    /// A large image
    /// </summary>
    [JsonProperty(Required = Required.Always)]
    public string Large { get; set; } = null!;
}
