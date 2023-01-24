namespace ChristianHelle.DeveloperTools.CodeGenerators.JMeter.Core
{
    public static class Generator
    {
        public static void GenerateFromProject(string projectFullPath)
        {
            var csproj = projectFullPath;
            var swaggerSpec = SwaggerFileGenerator.LaunchAndGetSwaggerFile(csproj);

            var workingFolder = Path.GetDirectoryName(csproj)!;
            var outputFolder = Path.Combine(workingFolder, "JMeter");

            var swaggerFile = Path.GetRandomFileName();
            File.WriteAllText(swaggerFile, swaggerSpec);

            JMeterScriptGenerator.Generate(swaggerFile, outputFolder);
        }
    }
}
