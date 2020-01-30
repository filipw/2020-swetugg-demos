using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Composition;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CodeFixes;
using Microsoft.CodeAnalysis.CodeActions;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using Microsoft.CodeAnalysis.Rename;
using Microsoft.CodeAnalysis.Text;

namespace Swetugg.VariableLengthAnalyzer
{
    [ExportCodeFixProvider(LanguageNames.CSharp, Name = nameof(SwetuggVariableLengthAnalyzerCodeFixProvider)), Shared]
    public class SwetuggVariableLengthAnalyzerCodeFixProvider : CodeFixProvider
    {
        private const string title = "Adjust length";

        public sealed override ImmutableArray<string> FixableDiagnosticIds => ImmutableArray.Create("SWG001");

        public sealed override FixAllProvider GetFixAllProvider() => WellKnownFixAllProviders.BatchFixer;

        public sealed override async Task RegisterCodeFixesAsync(CodeFixContext context)
        {
            var root = await context.Document.GetSyntaxRootAsync(context.CancellationToken).ConfigureAwait(false);
            var diagnostic = context.Diagnostics.First();
            var diagnosticSpan = diagnostic.Location.SourceSpan;

            var declaration = root.FindToken(diagnosticSpan.Start).Parent.AncestorsAndSelf().OfType<VariableDeclaratorSyntax>().First();

            context.RegisterCodeFix(
                CodeAction.Create(
                    title: title,
                    createChangedSolution: c => AdjustLength(context.Document, declaration, c),
                    equivalenceKey: title),
                diagnostic);
        }

        private async Task<Solution> AdjustLength(Document document, VariableDeclaratorSyntax variableDeclarationSyntax, CancellationToken cancellationToken)
        {
            var variableName = variableDeclarationSyntax.Identifier.Text;
            var newName = variableName.Length < 8 ? variableName.PadRight(8, 'X') : variableName.Substring(0, 20);
            var semanticModel = await document.GetSemanticModelAsync(cancellationToken);
            var localSymbol = semanticModel.GetDeclaredSymbol(variableDeclarationSyntax, cancellationToken);
            var newSolution = await Renamer.RenameSymbolAsync(document.Project.Solution, localSymbol, newName, document.Project.Solution.Workspace.Options, cancellationToken).ConfigureAwait(false);

            return newSolution;
        }
    }
}
