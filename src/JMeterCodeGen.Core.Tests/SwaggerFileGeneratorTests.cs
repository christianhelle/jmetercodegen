using ChristianHelle.DeveloperTools.CodeGenerators.JMeter.Core;

namespace JMeterCodeGen.Core.Tests;

public class SwaggerFileGeneratorTests
{
    [Fact]
    public void GenerateTest()
    {
        var sut = new SwaggerFileGenerator();
        var swaggerFile = sut.BuildProject("");
        Assert.NotNull(swaggerFile);
    }
}