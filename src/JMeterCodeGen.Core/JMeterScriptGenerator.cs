using Rapicgen.Core.Generators;
using Rapicgen.Core.Generators.OpenApi;
using Rapicgen.Core.Installer;
using Rapicgen.Core.Options.General;

namespace ChristianHelle.DeveloperTools.CodeGenerators.JMeter.Core;

public class JMeterScriptGenerator
{
    public void Generate(string swaggerFilePath, string outputDirectory)
    {
        var generator = new OpenApiJMeterCodeGenerator(
            swaggerFilePath,
            outputDirectory,
            new DefaultGeneralOptions(),
            new ProcessLauncher(),
            new DependencyInstaller(
                new NpmInstaller(new ProcessLauncher()),
                new FileDownloader(new WebDownloader())));

        generator.GenerateCode(null);
    }
}