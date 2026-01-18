using Microsoft.CodeAnalysis;

namespace FluentNoiseGenerator.SourceGenerators;

[Generator]
public class ResourceCollectionSectionGenerator : IIncrementalGenerator
{
    public void Initialize(IncrementalGeneratorInitializationContext context)
    {
        context.RegisterPostInitializationOutput(static (context) =>
        {
            context.AddSource("HelloDude.g.cs", @"
                namespace Hello;

                public static class Cool
                {
                    public static readonly string Haha = ""Haha"";
                }
            ");
        });
    }
}