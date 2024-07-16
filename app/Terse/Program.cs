using Microsoft.AspNetCore.Http.Json;
using System.Text.Json.Serialization;
using Terse.Models;
using Terse.Services;

var b = WebApplication.CreateBuilder(args);

b.Services.AddTransient<ToolClassService>();
b.Services.AddTransient<ToolService>();
b.Services.Configure<JsonOptions>(o => o.SerializerOptions.DefaultIgnoreCondition = JsonIgnoreCondition.WhenWritingNull);

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
        tools.GetVersion(toolId, versionId)); // TODO configure version?

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


app.MapFallback(() => Results.Redirect($"/{trsPrefix}/service-info"));

app.Run();