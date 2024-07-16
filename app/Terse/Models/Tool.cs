using System.Text.Json.Serialization;

namespace Terse.Models;

public class Tool
{
    public required string Url { get; set; }

    public required string Id { get; set; }

    public List<string>? Aliases { get; set; } = [];

    public required string Organization { get; set; }

    public string? Name { get; set; }

    public ToolClass Toolclass { get; set; } = new();

    public string? Description { get; set; }

    [JsonPropertyName("meta_version")]
    public string? MetaVersion { get; set; }

    [JsonPropertyName("has_checker")]
    public bool? HasChecker { get; set; }

    [JsonPropertyName("checker_url")]
    public string? CheckerUrl { get; set; }

    public required List<ToolVersion> Versions { get; set; }
}