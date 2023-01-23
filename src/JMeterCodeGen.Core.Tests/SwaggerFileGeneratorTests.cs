using ChristianHelle.DeveloperTools.CodeGenerators.JMeter.Core;

namespace JMeterCodeGen.Core.Tests;

public class SwaggerFileGeneratorTests
{
    [Fact]
    public void LaunchAndGetSwaggerFile_Returns_NotNull()
    {
        var csproj = TestFiles.Create();
        var swaggerSpec = SwaggerFileGenerator.LaunchAndGetSwaggerFile(csproj);
        Assert.NotNull(swaggerSpec);
    }
}