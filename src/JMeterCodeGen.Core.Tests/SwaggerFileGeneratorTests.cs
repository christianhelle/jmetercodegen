using ChristianHelle.DeveloperTools.CodeGenerators.JMeter.Core;

namespace JMeterCodeGen.Core.Tests;

public class SwaggerFileGeneratorTests
{
    [Fact]
    public void LaunchAndGetSwaggerFile_Returns_NotNull()
    {
        var folder = Path.Combine(Path.GetTempPath(), Guid.NewGuid().ToString("N"));
        Directory.CreateDirectory(folder);

        var csproj = Path.Combine(folder, "Sample.csproj");
        File.WriteAllText(csproj, TestCode.CSProj);
        File.WriteAllText(Path.Combine(folder, "Program.cs"), TestCode.CSharp);
        File.WriteAllText(Path.Combine(folder, "launchsettings.json"), TestCode.LaunchSettings);

        var sut = new SwaggerFileGenerator();
        var swaggerFile = sut.LaunchAndGetSwaggerFile(csproj);
        Assert.NotNull(swaggerFile);
    }
}