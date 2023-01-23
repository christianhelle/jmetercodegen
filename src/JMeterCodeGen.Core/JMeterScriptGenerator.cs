﻿using System.Diagnostics;

namespace ChristianHelle.DeveloperTools.CodeGenerators.JMeter.Core;

public static class JMeterScriptGenerator
{
    public static void Generate(string swaggerFilePath, string outputDirectory)
    {
        var workingDirectory = Path.GetDirectoryName(swaggerFilePath);
        
        RunProcess(
            DotNetPathProvider.GetDotNetPath(),
            $"tool new-manifest --output {workingDirectory}");

        RunProcess(
            DotNetPathProvider.GetDotNetPath(),
            "tool install rapicgen",
            workingDirectory);

        RunProcess(
            "rapicgen",
            $"jmeter {swaggerFilePath} {outputDirectory}");
    }

    private static void RunProcess(string filename, string arguments, string? workingDirectory = null)
    {
        using var process = new Process();
        process.OutputDataReceived += (_, args) => Trace.WriteLine(args.Data);
        process.ErrorDataReceived += (_, args) => Trace.WriteLine(args.Data);
        process.StartInfo = new ProcessStartInfo
        {
            FileName = filename,
            Arguments = arguments,
        };
        if (!string.IsNullOrWhiteSpace(workingDirectory))
        {
            process.StartInfo.WorkingDirectory = workingDirectory;
        }
        process.Start();
        process.WaitForExit();
    }
}