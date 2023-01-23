using ChristianHelle.DeveloperTools.CodeGenerators.JMeter.Core;

namespace JMeterCodeGen.Core.Tests;

public class JMeterScriptGeneratorTests
{
    [Fact]
    public void Generate()
    {
        var csproj = TestFiles.Create();
        var swaggerSpec = SwaggerFileGenerator.LaunchAndGetSwaggerFile(csproj);

        var workingFolder = Path.GetDirectoryName(csproj)!;
        var outputFolder = Path.Combine(workingFolder, "Output");
        
        var swaggerFile = Path.Combine(workingFolder, "swagger.json");
        File.WriteAllText(swaggerFile, swaggerSpec);

        JMeterScriptGenerator.Generate(swaggerFile, outputFolder);
        Assert.NotEmpty(Directory.GetFiles(outputFolder));
    }
}