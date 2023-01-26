using System.Diagnostics;
using System.Net;

namespace ChristianHelle.DeveloperTools.CodeGenerators.JMeter.Core;

public static class SwaggerFileGenerator
{
    public static string LaunchAndGetSwaggerFile(string projectFilepath)
    {
        using var process = new Process();
        process.OutputDataReceived += (_, args) => Trace.WriteLine(args.Data);
        process.ErrorDataReceived += (_, args) => Trace.WriteLine(args.Data);

        var port = new Random().Next(50000, 59999);
        process.StartInfo = new ProcessStartInfo
        {
            FileName = DotNetPathProvider.GetDotNetPath(),
            Arguments = $"run --project {projectFilepath} --urls=http://localhost:{port}",
            RedirectStandardOutput = true,
            RedirectStandardError = true,
            RedirectStandardInput = true,
            UseShellExecute = false,
            CreateNoWindow = true,
        };

        process.Start();
        process.BeginErrorReadLine();
        process.BeginOutputReadLine();

        try
        {
            var attempts = 0;
            string content = string.Empty;

            using var client = new WebClient();
            while (string.IsNullOrWhiteSpace(content) && attempts < 10)
            {
                try
                {
                    content = client.DownloadString($"http://localhost:{port}/swagger/v1/swagger.json");
                }
                catch (WebException)
                {
                    attempts++;
                    Thread.Sleep(1000);
                }
            }

            return content;
        }
        finally
        {
            process.Kill();
        }
    }
}