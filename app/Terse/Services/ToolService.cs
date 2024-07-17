using Microsoft.Extensions.Options;
using Terse.Config;
using Terse.Constants;
using Terse.Models;

namespace Terse.Services;

public class ToolService(IOptionsMonitor<ToolOptions> toolOptions, ToolFilesService toolFiles)
{

    private readonly ToolOptions _toolOptions = toolOptions.CurrentValue;

    public List<Tool> List(string baseUrl)
    {
        try
        {
            var id = "1";
            return [Get(id, $"{baseUrl}/{id}")]; // TODO: join URL safely
        }
        catch (KeyNotFoundException)
        {
            return [];
        }
    }

    public Tool Get(string id, string baseUrl)
    {
        var files = toolFiles.GetToolFiles();

        if (files.All(x => x.FileType != ToolFileTypes.PRIMARY_DESCRIPTOR))
            throw new KeyNotFoundException(
                "The requested tool does not exist in the registry. Check tool configuration to ensure Primary Descriptor path is correct.");

        return new()
        {
            Id = id,
            Url = baseUrl,
            Organization = _toolOptions.Organization ?? string.Empty,
            Aliases = _toolOptions.OriginalAlias is null ? [] : [_toolOptions.OriginalAlias],
            Versions = [GetVersion(id, _toolOptions.Version, $"{baseUrl}/versions/{_toolOptions.Version}")] // TODO: join URL safely
        };
    }

    public ToolVersion GetVersion(string toolId, string versionId, string url) => versionId == _toolOptions.Version ? new()
    {
        Id = versionId,
        Url = url,
        DescriptorType = ["CWL"] // fix to CWL for now
    } : throw new KeyNotFoundException("The requested version does not exist in the registry.");

    public List<ToolVersion> ListVersions(string toolId, string baseUrl) => [GetVersion(toolId, _toolOptions.Version, $"{baseUrl}/{_toolOptions.Version}")]; // TODO: join URL safely
}