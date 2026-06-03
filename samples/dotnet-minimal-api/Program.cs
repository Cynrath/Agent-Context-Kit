var builder = WebApplication.CreateBuilder(args);
var app = builder.Build();

app.MapGet("/", () => Results.Ok(new
{
    name = "AgentContextKit sample",
    kind = "minimal-api"
}));

app.Run();
