namespace JMeterCodeGen.Core.Tests;

public static class TestFiles
{
    public static string Create()
    {
        var folder = Path.Combine(Path.GetTempPath(), Guid.NewGuid().ToString("N"));
        Directory.CreateDirectory(folder);

        var csproj = Path.Combine(folder, "Sample.csproj");
        File.WriteAllText(csproj, TestCode.CSProj);
        File.WriteAllText(Path.Combine(folder, "Program.cs"), TestCode.CSharp);

        return csproj;
    }
}