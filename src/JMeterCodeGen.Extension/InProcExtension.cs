using Microsoft.Extensions.DependencyInjection;
using Microsoft.VisualStudio.Extensibility;

namespace ChristianHelle.DeveloperTools.CodeGenerators.JMeter.Extension
{
    [HostingOptions(requiresInProcessHosting: true)]
    public class InProcExtension : Microsoft.VisualStudio.Extensibility.Extension
    {
        protected override void InitializeServices(IServiceCollection serviceCollection)
        {
            base.InitializeServices(serviceCollection);
        }
    }
}