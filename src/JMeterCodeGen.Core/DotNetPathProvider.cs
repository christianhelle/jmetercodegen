namespace ChristianHelle.DeveloperTools.CodeGenerators.JMeter.Core;

public static class DotNetPathProvider
{
    public static string GetDotNetPath()
    {
        return Path.Combine(
            Environment.GetFolderPath(Environment.SpecialFolder.ProgramFiles),
            "dotnet",
            "dotnet.exe");
    }
}