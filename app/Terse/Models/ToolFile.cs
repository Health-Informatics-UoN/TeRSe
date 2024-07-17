using System.Text.Json.Serialization;

namespace Terse.Models;

public class ToolFile
{
    public required string Path { get; set; }

    [JsonPropertyName("file_type")]
    public string FileType { get; set; } = "OTHER";

    public ChecksumInfo? Checksum { get; set; }
}