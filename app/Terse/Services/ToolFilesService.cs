using Microsoft.Extensions.FileProviders;
using Microsoft.Extensions.Options;
using Terse.Config;
using Terse.Models;
using UoN.ZipBuilder;

namespace Terse.Services;

public class ToolFilesService(IOptionsMonitor<ToolOptions> toolOptions, IWebHostEnvironment env)
{
    private readonly ToolOptions _toolOptions = toolOptions.CurrentValue;

    private readonly PhysicalFileProvider _toolRootFileProvider = Path.IsPathRooted(toolOptions.CurrentValue.RootPath)
        ? new PhysicalFileProvider(toolOptions.CurrentValue.RootPath)
        : new PhysicalFileProvider(Path.Combine(env.ContentRootPath, toolOptions.CurrentValue.RootPath));

    /// <summary>
    /// Zip up all files for the requested tool and return a stream of the archive
    /// </summary>
    /// <returns></returns>
    public byte[] ArchiveAll()
    { // TODO: allow optionally masking RO-Crates by excluding/renaming the metadata
        var zipBuilder = new ZipBuilder()
            .CreateZipStream();

        try
        {
            foreach (var f in GetToolFiles())
            {
                var fileInfo = _toolRootFileProvider.GetFileInfo(f.Path);
                zipBuilder.AddFile(fileInfo.PhysicalPath, f.Path); // TODO: does this work or should we get bytes via the IFileInfo?
            };
        }
        catch (KeyNotFoundException e)
        {
            throw new KeyNotFoundException(
                "The requested tool does not exist in the registry. Check tool configuration.", e);
        }

        return zipBuilder.AsByteArray();
    }

    public List<ToolFile> List(string id)
    {
        try
        {
            return GetToolFiles().ToList();
        }
        catch (KeyNotFoundException e)
        {
            throw new KeyNotFoundException(
                "The requested tool does not exist in the registry. Check tool configuration.", e);
        }
    }


    public IEnumerable<ToolFile> GetToolFiles(string path = "")
    {
        var directoryContents = _toolRootFileProvider.GetDirectoryContents(path);
        if (!directoryContents.Exists || directoryContents is NotFoundDirectoryContents)
            throw new KeyNotFoundException();

        foreach (var file in directoryContents)
        {
            if (file.IsDirectory)
            {
                // recursively call GetFiles and return each one
                // file.Name is the directory name alone (eg. images)
                foreach (var f in GetToolFiles(Path.Combine(path, file.Name)))
                {
                    yield return f;
                }
            }
            else
            {
                var fileRelativePath = Path.Combine(path, file.Name);
                yield return new ToolFile
                {
                    Path = fileRelativePath,
                    FileType = fileRelativePath == _toolOptions.PrimaryDescriptorPath ? "PRIMARY_DESCRIPTOR" : "OTHER", // TODO: stop using magic strings <3
                };
            }
        }
    }

    public IEnumerable<IFileInfo> GetToolFileInfo(string path = "")
    {
        var directoryContents = _toolRootFileProvider.GetDirectoryContents(path);
        if (!directoryContents.Exists || directoryContents is NotFoundDirectoryContents)
            throw new KeyNotFoundException();

        foreach (var file in directoryContents)
        {
            if (file.IsDirectory)
            {
                // recursively call GetFiles and return each one
                // file.Name is the directory name alone (eg. images)
                foreach (var f in GetToolFileInfo(Path.Combine(path, file.Name)))
                {
                    yield return f;
                }
            }
            else
            {
                yield return file;
            }
        }
    }
}