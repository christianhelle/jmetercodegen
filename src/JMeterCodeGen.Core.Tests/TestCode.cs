namespace JMeterCodeGen.Core.Tests;

public sealed class TestCode
{
    public const string CSProj = @"
<Project Sdk=\""Microsoft.NET.Sdk.Web\"">
    <PropertyGroup>
        <TargetFramework>net6.0</TargetFramework>
        <Nullable>enable</Nullable>
        <ImplicitUsings>enable</ImplicitUsings>
    </PropertyGroup>
    <ItemGroup>
        <PackageReference Include=\""Swashbuckle.AspNetCore\"" Version=\""6.2.3\"" />
    </ItemGroup>
</Project>
";
    
    public const string CSharp = @"
var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

var summaries = new[]
{
    \""Freezing\"", \""Bracing\"", \""Chilly\"", \""Cool\"", \""Mild\"", \""Warm\"", \""Balmy\"", \""Hot\"", \""Sweltering\"", \""Scorching\""
};

app.MapGet(\""/weatherforecast\"", () =>
    {
        var forecast = Enumerable.Range(1, 5).Select(index =>
                new WeatherForecast
                (
                    DateTime.Now.AddDays(index),
                    Random.Shared.Next(-20, 55),
                    summaries[Random.Shared.Next(summaries.Length)]
                ))
            .ToArray();
        return forecast;
    })
    .WithName(\""GetWeatherForecast\"");

app.Run();

internal record WeatherForecast(DateTime Date, int TemperatureC, string? Summary)
{
    public int TemperatureF => 32 + (int)(TemperatureC / 0.5556);
}
";
    
    public const string LaunchSettings = @"
{
  \""$schema\"": \""https://json.schemastore.org/launchsettings.json\"",
    \""iisSettings\"": {
        \""windowsAuthentication\"": false,
        \""anonymousAuthentication\"": true,
        \""iisExpress\"": {
            \""applicationUrl\"": \""http://localhost:63079\"",
            \""sslPort\"": 44381
        }
    },
\""profiles\"": {
    \""API\"": {
        \""commandName\"": \""Project\"",
        \""dotnetRunMessages\"": true,
        \""launchBrowser\"": true,
        \""launchUrl\"": \""swagger\"",
        \""applicationUrl\"": \""https://localhost:5001;http://localhost:5000\"",
        \""environmentVariables\"": {
            \""ASPNETCORE_ENVIRONMENT\"": \""Development\""
        }
    },
    \""IIS Express\"": {
        \""commandName\"": \""IISExpress\"",
        \""launchBrowser\"": true,
        \""launchUrl\"": \""swagger\"",
        \""environmentVariables\"": {
            \""ASPNETCORE_ENVIRONMENT\"": \""Development\""
        }
    }
  }
}
";
}