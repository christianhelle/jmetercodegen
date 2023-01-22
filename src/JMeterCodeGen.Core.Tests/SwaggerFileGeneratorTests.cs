using ChristianHelle.DeveloperTools.CodeGenerators.JMeter.Core;

namespace JMeterCodeGen.Core.Tests;

public class SwaggerFileGeneratorTests
{
    [Fact]
    public void LaunchAndGetSwaggerFile_Returns_NotNull()
    {
        var csproj = TestFiles.Create();
        var sut = new SwaggerFileGenerator();
        var swaggerSpec = sut.LaunchAndGetSwaggerFile(csproj);
        Assert.NotNull(swaggerSpec);
    }
}