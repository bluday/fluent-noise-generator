using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using System.Collections.Immutable;

namespace FluentNoiseGenerator.CodeAnalysis.Generators;

[Generator]
public class ResourceCollectionSectionGenerator : IIncrementalGenerator
{
    public void Initialize(IncrementalGeneratorInitializationContext context)
    {
        IncrementalValueProvider<ImmutableArray<SyntaxNode>> syntaxNodes = context.SyntaxProvider
            .ForAttributeWithMetadataName(
                "FluentNoiseGenerator.Common.Localization.Attributes.ResourceCollectionAttribute",
                static (syntaxNode, _)    => syntaxNode is ClassDeclarationSyntax,
                static (syntaxContext, _) => syntaxContext.TargetNode
            )
            .Collect();

        context.RegisterPostInitializationOutput((context) =>
        {
            context.AddSource("Cool.g.cs", 
$@"namespace FluentNoiseGenerator;

public partial class Cool
{{
    public static string Haha {{ get; }} = ""Haha"";
}}"
            );
        });

        context.RegisterSourceOutput(syntaxNodes, static (sourceProductionContext, syntaxNodes) =>
        {
            sourceProductionContext.AddSource($"Awesome.g.cs",
$@"namespace FluentNoiseGenerator;

public partial class Cool
{{
    public static int NodeCount {{ get; }} = {syntaxNodes.Length};
}}"
            );
        });
    }
}