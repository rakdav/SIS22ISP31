using WebApplicationEmpty;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddRazorComponents().
    AddInteractiveServerComponents();
var app = builder.Build();
app.UseAntiforgery();
app.MapRazorComponents<App>().
    AddInteractiveServerRenderMode();
app.MapGet("/", () => "Hello World!");
app.Run();
