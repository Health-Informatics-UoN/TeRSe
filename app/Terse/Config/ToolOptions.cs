namespace Terse.Config;

public class ToolOptions
{
    /// <summary>
    /// The path to the directory containing files for the tool to serve
    /// </summary>
    public string RootPath { get; set; } = "tool";

    /// <summary>
    /// The relative path inside the <see cref="ToolRootPath"/> to the Primary Descriptor file (i.e. workflow entrypoint)
    /// </summary>
    public string PrimaryDescriptorPath { get; set; } = "main.cwl";

    /// <summary>
    /// Optional name for the tool.
    /// </summary>
    public string? Name { get; set; }

    /// <summary>
    /// Version identifier for the files being served
    /// </summary>
    public string Version { get; set; } = "1";

    /// <summary>
    /// The Organization that published the tool
    /// </summary>
    public string? Organization { get; set; }

    /// <summary>
    /// Optional, to include as an alias to record the original source/id of the workflow, e.g. a WorkflowHub URL
    /// </summary>
    public string? OriginalAlias { get; set; }
}