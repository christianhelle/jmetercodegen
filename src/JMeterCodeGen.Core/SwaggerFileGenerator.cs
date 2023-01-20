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
        
        using var process = new Process();
        process.OutputDataReceived += (_, args) => Trace.WriteLine(args.Data);
        process.ErrorDataReceived += (_, args) => Trace.WriteLine(args.Data);
        process.StartInfo = new ProcessStartInfo
        {
            FileName = dotnet,
            Arguments = $"run --project {projectFilepath} --urls=http://localhost:54321",
        };
        process.Start();
        
        using var client = new WebClient();
        var swaggerSpec = client.DownloadString("http://localhost:54321/swagger/v1/swagger.json");
        return swaggerSpec;
    }
}