using System.Text.Json.Serialization;
using Terse.Constants;

namespace Terse.Models;

public class ToolFile
{
    public required string Path { get; set; }

    [JsonPropertyName("file_type")]
    public string FileType { get; set; } = ToolFileTypes.OTHER;

    public ChecksumInfo? Checksum { get; set; }
}