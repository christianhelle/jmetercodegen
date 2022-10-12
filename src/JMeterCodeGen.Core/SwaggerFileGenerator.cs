using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.OpenApi.Writers;
using Swashbuckle.AspNetCore.Swagger;
using System.Reflection;
using System.Runtime.Loader;

namespace ChristianHelle.DeveloperTools.CodeGenerators.JMeter.Core;

public class SwaggerFileGenerator
{
    public string BuildSwaggerFile(string startupAssemblyPath, string swaggerFile = "v1")
    {
        // 1) Configure host with provided startupassembly
        var startupAssembly = AssemblyLoadContext.Default.LoadFromAssemblyPath(
                        Path.Combine(Directory.GetCurrentDirectory(), startupAssemblyPath));

        // 2) Build a service container that's based on the startup assembly
        var serviceProvider = GetServiceProvider(startupAssembly);

        // 3) Retrieve Swagger via configured provider
        var swaggerProvider = serviceProvider.GetRequiredService<ISwaggerProvider>();
        var swagger = swaggerProvider.GetSwagger(swaggerFile);

        // 4) Serialize to specified output location or stdout
        var outputPath = Path.Combine(Path.GetTempPath(), Path.GetRandomFileName());

        using (var streamWriter = File.CreateText(outputPath))
        {
            IOpenApiWriter writer = new OpenApiYamlWriter(streamWriter);
            swagger.SerializeAsV3(writer);
        }

        var contents = File.ReadAllText(outputPath);
        File.Delete(outputPath);
        return contents;
    }

    private static IServiceProvider GetServiceProvider(Assembly startupAssembly)
    {
        if (TryGetCustomHost(startupAssembly, "SwaggerHostFactory", "CreateHost", out IHost host))
        {
            return host.Services;
        }

        if (TryGetCustomHost(startupAssembly, "SwaggerWebHostFactory", "CreateWebHost", out IWebHost webHost))
        {
            return webHost.Services;
        }

        try
        {
            return WebHost.CreateDefaultBuilder()
               .UseStartup(startupAssembly.GetName().Name)
               .Build()
               .Services;
        }
        catch
        {
            var serviceProvider = HostingApplication.GetServiceProvider(startupAssembly);

            if (serviceProvider != null)
            {
                return serviceProvider;
            }

            throw;
        }
    }

    private static bool TryGetCustomHost<THost>(
        Assembly startupAssembly,
        string factoryClassName,
        string factoryMethodName,
        out THost host)
    {
        // Scan the assembly for any types that match the provided naming convention
        var factoryTypes = startupAssembly.DefinedTypes
            .Where(t => t.Name == factoryClassName)
            .ToList();

        if (!factoryTypes.Any())
        {
            host = default;
            return false;
        }

        if (factoryTypes.Count() > 1)
            throw new InvalidOperationException($"Multiple {factoryClassName} classes detected");

        var factoryMethod = factoryTypes
            .Single()
            .GetMethod(factoryMethodName, BindingFlags.Public | BindingFlags.Static);

        if (factoryMethod == null || factoryMethod.ReturnType != typeof(THost))
            throw new InvalidOperationException(
                $"{factoryClassName} class detected but does not contain a public static method " +
                $"called {factoryMethodName} with return type {typeof(THost).Name}");

        host = (THost)factoryMethod.Invoke(null, null);
        return true;
    }
}