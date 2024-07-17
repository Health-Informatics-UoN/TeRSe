namespace Terse.Models;

public class DescriptorFile
{
    public required string Content { get; set; }

    public List<ChecksumInfo> Checksum { get; set; } = [];

    public string? Url { get; set; }
}