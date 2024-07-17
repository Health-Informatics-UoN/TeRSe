using System.Text.Json.Serialization;

namespace Terse.Models;

public class ToolVersion
{
    public List<string>? Author { get; set; } = [];

    public string? Name { get; set; }

    public required string Url { get; set; }

    public required string Id { get; set; }

    [JsonPropertyName("is_production")]
    public string? IsProduction { get; set; }

    public List<string>? Images { get; set; } = []; // TODO: model type for ImageData

    [JsonPropertyName("descriptor_type")]
    public List<string>? DescriptorType { get; set; }

    public bool? Containerfile { get; set; }

    [JsonPropertyName("meta_version")]
    public string? MetaVersion { get; set; }

    public bool? Verified { get; set; }

    [JsonPropertyName("verified_source")]
    public List<string>? VerifiedSource { get; set; }

    public bool? Signed { get; set; }

    [JsonPropertyName("included_apps")]
    public List<string>? IncludedApps { get; set; }
}

