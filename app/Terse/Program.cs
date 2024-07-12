using Terse.Models;
using Terse.Services;

var b = WebApplication.CreateBuilder(args);

b.Services.AddTransient<ToolClassService>();

var app = b.Build();

const string trsPrefix = "ga4gh/trs/v2";

app.MapGet(trsPrefix + "/service-info", () => new ServiceInfo());

app.MapGet(trsPrefix + "/toolClasses",
    () => app.Services.GetRequiredService<ToolClassService>().List());

app.MapFallback((httpContext) =>
{
    httpContext.Response.Redirect("/ga4gh/trs/v2/service-info");
    return Task.CompletedTask;
});

app.Run();
