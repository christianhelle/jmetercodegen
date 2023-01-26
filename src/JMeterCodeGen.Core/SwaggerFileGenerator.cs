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
            while (true)
            {
                var request = WebRequest.Create($"http://localhost:{port}/swagger/v1/swagger.json");
                request.Timeout = 120000;
                using var response = request.GetResponse();
                using var stream = response.GetResponseStream();
                if (stream == null)
                    continue;

                using var reader = new StreamReader(stream);
                var content = reader.ReadToEnd();
                if (string.IsNullOrWhiteSpace(content))
                    continue;

                return content;
            }
        }
        finally
        {
            process.Kill();
        }
    }
}