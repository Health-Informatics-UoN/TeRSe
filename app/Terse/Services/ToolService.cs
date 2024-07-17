using System.Text.Json.Serialization;
using Terse.Models;

namespace Terse.Services;

public class ToolService
{
    // TODO: handle not found?
    // TODO: support query parameter filters
    public List<Tool> List() => [Get("1")];


    // TODO: if config is missing, throw KeyNotFound?
    public Tool Get(string id) => new()
    {
        Id = id,
        Url = "", // TODO: may need to tell the service about the incoming request...
        Organization = "", // TODO from config?
        Aliases = [], // TODO: can use this to retain original source url e.g. workflowhub, if we add TRS to Hutch
        Versions = [
            new() {
                Id = "x", // TODO: config?
                Url = "", // TODO: may need to tell the service about the incoming request...
                DescriptorType = ["CWL"] // fix to CWL for now? TODO config?
            }
        ]
    };


    // TODO correctly match configured version only, 404 if not?
    public ToolVersion GetVersion(string toolId, string versionId) => new()
    {
        Id = "x", // TODO: config?
        Url = "", // TODO: may need to tell the service about the incoming request...
        DescriptorType = ["CWL"] // fix to CWL for now? TODO config?
    };

    // TODO: correctly match configured version only?
    public List<ToolVersion> ListVersions(string toolId) => [GetVersion(toolId, "")];
}