using Newtonsoft.Json;

public class AlternateName
{
    [JsonProperty(Required = Required.Always)]
    public string Name { get; set; } = null!;
    [JsonProperty("first_year")]
    public int? FirstYear { get; set; }
    [JsonProperty("last_year")]
    public int? LastYear { get; set; }
}
