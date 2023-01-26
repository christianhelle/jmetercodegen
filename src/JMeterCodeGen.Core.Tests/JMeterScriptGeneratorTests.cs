using ChristianHelle.DeveloperTools.CodeGenerators.JMeter.Core;

namespace JMeterCodeGen.Core.Tests;

public class JMeterScriptGeneratorTests
{
    [Fact]
    public async Task GenerateAsync()
    {
        var csproj = TestFiles.Create();
        var swaggerSpec = await SwaggerFileGenerator.LaunchAndGetSwaggerFile(csproj);

        var workingFolder = Path.GetDirectoryName(csproj)!;
        var outputFolder = Path.Combine(workingFolder, "Output");

        var swaggerFile = Path.Combine(workingFolder, "Swagger.json");
        File.WriteAllText(swaggerFile, swaggerSpec);

        JMeterScriptGenerator.GenerateAsync(swaggerFile, outputFolder);
        Assert.NotEmpty(Directory.GetFiles(outputFolder));
    }
}