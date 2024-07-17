using Microsoft.AspNetCore.Http.Json;
using System.Text.Json.Serialization;
using Terse.Config;
using Terse.Models;
using Terse.Services;

var b = WebApplication.CreateBuilder(args);

b.Services.Configure<JsonOptions>(o => o.SerializerOptions.DefaultIgnoreCondition = JsonIgnoreCondition.WhenWritingNull);

b.Services.Configure<ToolOptions>(b.Configuration.GetRequiredSection("Tool"));

b.Services.AddTransient<ToolClassService>();
b.Services.AddTransient<ToolService>();
b.Services.AddTransient<ToolFilesService>();


var app = b.Build();

const string trsPrefix = "ga4gh/trs/v2";

// Service Info

app.MapGet(trsPrefix + "/service-info",
    () => new ServiceInfo
    {
        Id = "dev.local",
    });

// Tool Classes

app.MapGet(trsPrefix + "/toolClasses",
    (ToolClassService toolClasses) => toolClasses.List());

// Tools

app.MapGet(trsPrefix + "/tools", (ToolService tools) => tools.List());

app.MapGet(trsPrefix + "/tools/{id}",
    (string id, ToolService tools) => tools.Get(id));

// Tool Versions

app.MapGet(trsPrefix + "/tools/{id}/versions",
    (string id, ToolService tools) => tools.ListVersions(id));

app.MapGet(trsPrefix + "/tools/{toolId}/versions/{versionId}",
    (string toolId, string versionId, ToolService tools) =>
        tools.GetVersion(toolId, versionId)); // TODO: configure version?

// Tool Version Details

app.MapGet(trsPrefix + "/tools/{toolId}/versions/{versionId}/containerfile",
    () => Results.NotFound(new ErrorResponse
    {
        Code = 404,
        Message = "No container file ('./Dockerfile') found for this tool version"
    }));

app.MapGet(trsPrefix + "/tools/{toolId}/versions/{versionId}/{type}/tests",
    (string type) =>
        type.Contains("cwl")
            ? Results.Ok()
            : Terse.Results.WrongType());

app.MapGet(trsPrefix + "/tools/{toolId}/versions/{versionId}/{type}/files",
    (string toolId, string versionId, string type, string? format, ToolFilesService files) =>
        type.Contains("cwl")
            ? Results.Ok(
                // TODO: Service
                format == "zip"
                    ? files.ArchiveAll()
                    : files.List(toolId)
            )
            : Terse.Results.WrongType());


app.MapFallback(() => Results.Redirect($"/{trsPrefix}/service-info"));

app.Run();