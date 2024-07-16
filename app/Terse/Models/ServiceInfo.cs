namespace Terse.Models;

public class ServiceInfo
{
    public string ContactUrl { get; set; } = string.Empty; // TODO

    public string Description { get; set; } = "Terse. A minimal TRS API for a single workflow.";
    public string DocumentationUrl { get; set; } = "https://editor.swagger.io/?url=https://raw.githubusercontent.com/ga4gh/tool-registry-service-schemas/release/v2.0.1/openapi/openapi.yaml";
    public string Environment { get; set; } = "development";
    public required string Id { get; set; }
    public string Name { get; set; } = "Terse";

    public ServiceOrganisation Organization { get; set; } = new();

    public ServiceType Type { get; set; } = new();

    public string Version { get; set; } = "1.0.0-alpha.1";
}

public class ServiceOrganisation
{
    public string Name { get; set; } = "University of Nottingham Health Informatics";
    public string Url { get; set; } = "https://github.com/Health-Informatics-UoN";
}

public class ServiceType
{
    public string Artifact { get; set; } = "trs";
    public string Group { get; set; } = "ga4gh";
    public string Version { get; set; } = "2.0.1";
}