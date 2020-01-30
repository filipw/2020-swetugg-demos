using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Linq;
using System.Threading;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using Microsoft.CodeAnalysis.Diagnostics;

namespace Swetugg.VariableLengthAnalyzer
{
    [DiagnosticAnalyzer(LanguageNames.CSharp)]
    public class SwetuggVariableLengthAnalyzerAnalyzer : DiagnosticAnalyzer
    {
        public const string DiagnosticId = "SWG001";

        private static readonly LocalizableString Title = new LocalizableResourceString(nameof(Resources.AnalyzerTitle), Resources.ResourceManager, typeof(Resources));
        private static readonly LocalizableString MessageFormat = new LocalizableResourceString(nameof(Resources.AnalyzerMessageFormat), Resources.ResourceManager, typeof(Resources));
        private static readonly LocalizableString Description = new LocalizableResourceString(nameof(Resources.AnalyzerDescription), Resources.ResourceManager, typeof(Resources));
        private const string Category = "Naming";

        private static DiagnosticDescriptor Rule = new DiagnosticDescriptor(DiagnosticId, Title, MessageFormat, Category, DiagnosticSeverity.Error, isEnabledByDefault: true, description: Description);

        public override ImmutableArray<DiagnosticDescriptor> SupportedDiagnostics { get { return ImmutableArray.Create(Rule); } }

        public override void Initialize(AnalysisContext context)
        {
            context.RegisterSyntaxNodeAction(AnalyzeSyntaxNode, SyntaxKind.VariableDeclarator);
        }

        private static void AnalyzeSyntaxNode(SyntaxNodeAnalysisContext context)
        {
            var variableDeclaratorSyntax = (VariableDeclaratorSyntax)context.Node;
            var variableName = variableDeclaratorSyntax.Identifier.Text;
            if (variableName != null && (variableName.Length < 8 || variableName.Length > 20))
            {
                var diagnostic = Diagnostic.Create(Rule, variableDeclaratorSyntax.GetLocation(), variableName, variableName.Length);
                context.ReportDiagnostic(diagnostic);
            }
        }
    }
}
