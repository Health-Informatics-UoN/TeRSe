using Terse.Models;

var builder = WebApplication.CreateBuilder(args);
var app = builder.Build();

const string TRS_PREFIX = "ga4gh/trs/v2";

app.MapGet(TRS_PREFIX + "/service-info", () => new ServiceInfo());

app.MapGet("/", (httpContext) =>
{
    httpContext.Response.Redirect("/ga4gh/trs/v2/service-info");
    return Task.CompletedTask;
});

app.Run();
