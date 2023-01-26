using System.Diagnostics;

namespace ChristianHelle.DeveloperTools.CodeGenerators.JMeter.Core;

public static class SwaggerFileGenerator
{
    public static async Task<string> LaunchAndGetSwaggerFile(string projectFilepath)
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
            var url = $"http://localhost:{port}/swagger/v1/swagger.json";

            using var client = new HttpClient();
            while (string.IsNullOrWhiteSpace(content) && attempts < 10)
            {
                try
                {
                    using var response = await client.GetAsync(url);
                    if (response.StatusCode == System.Net.HttpStatusCode.NotFound)
                    {
                        url = $"http://localhost:{port}/swagger/v1.0/swagger.json";
                        continue;
                    }
                    content = await response.Content.ReadAsStringAsync();
                }
                catch (HttpRequestException e)
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