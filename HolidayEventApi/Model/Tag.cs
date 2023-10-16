using Newtonsoft.Json;

/// <summary>
/// Information about an Event tag
/// </summary>
public class Tag
{
    /// <summary>
    /// The name of the tag
    /// </summary>
    [JsonProperty(Required = Required.Always)]
    public string Name { get; set; } = null!;
}
