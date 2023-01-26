using ChristianHelle.DeveloperTools.CodeGenerators.JMeter.Core;

namespace JMeterCodeGen.Core.Tests;

public class SwaggerFileGeneratorTests
{
    [Fact]
    public async Task LaunchAndGetSwaggerFile_Returns_NotNullAsync()
    {
        var csproj = TestFiles.Create();
        var swaggerSpec = await SwaggerFileGenerator.LaunchAndGetSwaggerFile(csproj);
        Assert.NotNull(swaggerSpec);
    }
}