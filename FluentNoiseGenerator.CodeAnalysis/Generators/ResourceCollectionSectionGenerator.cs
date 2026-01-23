using Microsoft.CodeAnalysis;
using System.Collections.Immutable;
using System.Linq;

namespace FluentNoiseGenerator.CodeAnalysis.Generators;

[Generator]
public class ResourceCollectionSectionGenerator : IIncrementalGenerator
{
    public void Initialize(IncrementalGeneratorInitializationContext context)
    {
        IncrementalValueProvider<ImmutableArray<ISymbol>> symbols = context.SyntaxProvider
            .ForAttributeWithMetadataName(
                "FluentNoiseGenerator.Common.Localization.Attributes.ResourceCollectionAttribute",
                static (syntaxNode, _) => true,
                static (syntaxContext, _) => syntaxContext.TargetSymbol
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

        context.RegisterSourceOutput(symbols, static (sourceProductionContext, symbols) =>
        {
            string[] names = symbols.Select(symbol => symbol.Name).ToArray();

            string output = names.Length > 1 ? string.Join(",", names) : "No symbols";

            sourceProductionContext.AddSource($"Awesome.g.cs",
$@"namespace FluentNoiseGenerator;

public partial class Awesome
{{
    // public static string symbol.Name {{ get; }} = ""Haha"";

    public static void PrintSymbols()
    {{
        System.Diagnostics.Debug.WriteLine(""{output}"");
    }}
}}"
            );
        });
    }
}