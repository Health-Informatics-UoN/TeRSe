using Terse.Models;
using Terse.Services;

var b = WebApplication.CreateBuilder(args);

b.Services.AddTransient<ToolClassService>();

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

app.MapGet("/tools", (ToolService tools) => tools.List());

app.MapGet("/tools/{id}",
    (string id, ToolService tools) => tools.Get(id));

// Tool Versions

app.MapGet("/tools/{id}/versions",
    (string id, ToolService tools) => tools.ListVersions(id));

app.MapGet("/tools/{toolId}/versions/{versionId}",
    (string toolId, string versionId, ToolService tools) =>
        tools.GetVersion(toolId, versionId)); // TODO configure version?

// Tool Version Details

app.MapFallback((httpContext) =>
{
    httpContext.Response.Redirect($"/${trsPrefix}/service-info");
    return Task.CompletedTask;
});

app.Run();
