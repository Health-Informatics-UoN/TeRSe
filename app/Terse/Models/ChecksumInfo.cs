namespace Terse.Models;

public class ChecksumInfo
{
    public required string Checksum { get; set; }


    // values per https://github.com/ga4gh-discovery/ga4gh-checksum/blob/master/hash-alg.csv
    // though in practice this implementation exclusively uses sha-256
    public string Type { get; set; } = "sha-256";
}