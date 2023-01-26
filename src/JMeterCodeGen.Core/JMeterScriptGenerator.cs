using System.Diagnostics;

namespace ChristianHelle.DeveloperTools.CodeGenerators.JMeter.Core;

public static class JMeterScriptGenerator
{
    public static async Task GenerateAsync(string swaggerFilePath, string outputDirectory)
    {
        var workingDirectory = Path.GetDirectoryName(swaggerFilePath);

        await RunProcessAsync(
            DotNetPathProvider.GetDotNetPath(),
            $"tool new-manifest --output {workingDirectory}");

        await RunProcessAsync(
            DotNetPathProvider.GetDotNetPath(),
            "tool install rapicgen",
            workingDirectory);

        await RunProcessAsync(
            "rapicgen",
            $"jmeter {swaggerFilePath} {outputDirectory}");

        TryDeleteGeneratorIgnoreFile(outputDirectory);
    }

    private static void TryDeleteGeneratorIgnoreFile(string outputDirectory)
    {
        try
        {
            File.Delete(Path.Combine(outputDirectory, ".openapi-generator-ignore"));
        }
        catch (Exception e)
        {
            Trace.WriteLine(e);
        }
    }

    private static Task RunProcessAsync(
        string filename,
        string arguments,
        string? workingDirectory = null) =>
        Task.Run(() => RunProcess(filename, arguments, workingDirectory));

    private static void RunProcess(string filename, string arguments, string? workingDirectory = null)
    {
        var process = new Process();
        process.OutputDataReceived += (_, args) => Trace.WriteLine(args.Data);
        process.ErrorDataReceived += (_, args) => Trace.WriteLine(args.Data);

        process.StartInfo = new ProcessStartInfo
        {
            FileName = filename,
            Arguments = arguments,
            RedirectStandardOutput = true,
            RedirectStandardError = true,
            RedirectStandardInput = true,
            UseShellExecute = false,
            CreateNoWindow = true,
        };

        if (!string.IsNullOrWhiteSpace(workingDirectory))
            process.StartInfo.WorkingDirectory = workingDirectory;

        process.Start();
        process.BeginErrorReadLine();
        process.BeginOutputReadLine();
        process.WaitForExit();
    }
}