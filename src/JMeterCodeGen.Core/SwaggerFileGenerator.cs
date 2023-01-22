using System.Diagnostics;
using System.Net;

namespace ChristianHelle.DeveloperTools.CodeGenerators.JMeter.Core;

public class SwaggerFileGenerator
{
    public string LaunchAndGetSwaggerFile(string projectFilepath)
    {
        var dotnet = Path.Combine(
            Environment.GetFolderPath(Environment.SpecialFolder.ProgramFiles),
            "dotnet",
            "dotnet.exe");

        var port = new Random().Next(50000, 59999);
        using var process = new Process();
        process.OutputDataReceived += (_, args) => Trace.WriteLine(args.Data);
        process.ErrorDataReceived += (_, args) => Trace.WriteLine(args.Data);
        process.StartInfo = new ProcessStartInfo
        {
            FileName = dotnet,
            Arguments = $"run --project {projectFilepath} --urls=http://localhost:{port}",
        };
        process.Start();

        try
        {
            using var client = new WebClient();
            var swaggerSpec = client.DownloadString($"http://localhost:{port}/swagger/v1/swagger.json");
            return swaggerSpec;
        }
        finally
        {
            process.Kill();
        }
    }
}