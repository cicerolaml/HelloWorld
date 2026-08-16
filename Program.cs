using Microsoft.Extensions.Hosting.WindowsServices;

var builder = WebApplication.CreateBuilder(args);
builder.Host.UseWindowsService();
var app = builder.Build();

app.MapGet("/", () => Results.Content("""
    <html>
      <body style="background-color: yellow;">
        <h2>Hello World!</h2>
      </body>
    </html>
    """, "text/html"));

app.Run();