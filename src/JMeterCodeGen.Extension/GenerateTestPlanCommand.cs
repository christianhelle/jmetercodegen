using System.Diagnostics;
using ChristianHelle.DeveloperTools.CodeGenerators.JMeter.Core;
using Microsoft;
using Microsoft.VisualStudio.Extensibility;
using Microsoft.VisualStudio.Extensibility.Commands;
using Microsoft.VisualStudio.Extensibility.Definitions;

namespace ChristianHelle.DeveloperTools.CodeGenerators.JMeter.Extension
{
    [CommandIcon(KnownMonikers.Extension, IconSettings.IconAndText)]
    [Command("JMeterCodeGen.ExtensionContainer.GenerateTestPlanCommand", "Generate JMeter Test Plan", placement: CommandPlacement.ToolsMenu)]
    [CommandVisibleWhen(
        expression: "SolutionLoaded & IsValidFile",
        termNames: new string[] { "SolutionLoaded", "IsValidFile" },
        termValues: new string[] { "SolutionState:Exists", "ClientContext:Shell.ActiveSelectionFileName=(.csproj)$" })]
    internal class GenerateTestPlanCommand : Command
    {
        private readonly TraceSource traceSource;

        public GenerateTestPlanCommand(VisualStudioExtensibility extensibility, TraceSource traceSource, string id)
            : base(extensibility, id)
        {
            this.traceSource = Requires.NotNull(traceSource, nameof(traceSource));
        }

        public override async Task ExecuteCommandAsync(IClientContext context, CancellationToken cancellationToken)
        {
            var activeProjectIdentifier = context["Shell.ActiveProjectIdentifier"];
            var activeSelectionPath = context["Shell.ActiveSelectionPath"];
            await Task.Run(() => Generator.GenerateFromProject(activeSelectionPath));
        }
    }
}