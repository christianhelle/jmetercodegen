namespace ChristianHelle.DeveloperTools.CodeGenerators.JMeter.Core
{
    public static class Generator
    {
        public static async Task GenerateFromProjectAsync(string projectFullPath)
        {
            var csproj = projectFullPath;
            var swaggerSpec = await SwaggerFileGenerator.LaunchAndGetSwaggerFile(csproj);

            var workingFolder = Path.GetDirectoryName(csproj)!;
            var outputFolder = Path.Combine(workingFolder, "JMeter");

            var swaggerFile = Path.GetRandomFileName();
            File.WriteAllText(swaggerFile, swaggerSpec);

            await JMeterScriptGenerator.GenerateAsync(swaggerFile, outputFolder);
        }
    }
}
