using FluentNoiseGenerator.Common.Localization.Attributes;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.Text;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;

namespace FluentNoiseGenerator.SourceGenerator;

/// <summary>
/// Represents a source generator for generating properties for sections, specified using the
/// <see cref="HasResourceSectionAttribute"/> attribute.
/// </summary>
public class ResourceCollectionSectionGenerator : IIncrementalGenerator
{
    #region Constants
    /// <summary>
    /// The class template to be used for generation, including the namespace declaration.
    /// </summary>
    public const string CODE_CLASS_TEMPLATE = @"
namespace $RESOURCE_COLLECTION_ASSEMBLY_NAME_IDENTIFIER$;

public sealed partial class $RESOURCE_COLLECTION_TYPE_NAME_IDENTIFIER$
{
    $SECTIONS$
}";

    /// <summary>
    /// The source code template used for generating properties within the generated class.
    /// </summary>
    public const string CODE_PROPERTY_TEMPLATE = @"
/// <summary>
/// Gets the ""$RESOURCE_SECTION_PROPERTY_NAME_IDENTIFIER$"" section.
/// </summary>
public $RESOURCE_SECTION_PROPERTY_TYPE_IDENTIFIER$ $RESOURCE_SECTION_PROPERTY_NAME_IDENTIFIER$ { get; } = new();
";

    /// <summary>
    /// The unique identifier for replacing the identifier in the class template string with the
    /// fully-qualified name of the target assembly.
    /// </summary>
    public const string RESOURCE_COLLECTION_ASSEMBLY_NAME_IDENTIFIER = "$RESOURCE_COLLECTION_ASSEMBLY_NAME$";

    /// <summary>
    /// The fully-qualified name of the <see cref="ResourceCollectionAttribute"/> type.
    /// </summary>
    public const string RESOURCE_COLLECTION_ATTRIBUTE_FULL_QUALIFIER_NAME =
        "FluentNoiseGenerator.Common.Localization.Attributes.ResourceCollectionAttribute";

    /// <summary>
    /// The unique identifier for replacing the identifier in the class template string with the
    /// targeted class name that utilizes the <see cref="ResourceCollectionAttribute"/> attribute.
    /// </summary>
    public const string RESOURCE_COLLECTION_TYPE_NAME_IDENTIFIER = "$RESOURCE_COLLECTION_TYPE_NAME$";

    /// <summary>
    /// The unique identifier for replacing the identifier in the property template string with the
    /// name of the property for a section, provided through the <see cref="HasResourceSectionAttribute"/>
    /// attribute.
    /// </summary>
    public const string RESOURCE_SECTION_PROPERTY_NAME_IDENTIFIER = "$RESOURCE_SECTION_PROPERTY_NAME$";

    /// <summary>
    /// The unique identifier for replacing the identifier in the property template string with the
    /// type of the property for a section, provided through the <see cref="HasResourceSectionAttribute"/>
    /// attribute.
    /// </summary>
    public const string RESOURCE_SECTION_PROPERTY_TYPE_IDENTIFIER = "$RESOURCE_SECTION_PROPERTY_TYPE$";

    /// <summary>
    /// The unique identifier for replacing the identifier in the class template string with a
    /// constructed multiline string containing generated properties.
    /// </summary>
    public const string SECTIONS_IDENTIFIER = "$SECTIONS$";
    #endregion

    #region fields
    private static readonly DiagnosticDescriptor DebugDescriptor = new(
        id:                 "GEN999",
        title:              "Generator debug",
        messageFormat:      "Processing type: {0}",
        category:           "IncrementalGenerator",
        defaultSeverity:    DiagnosticSeverity.Warning,
        isEnabledByDefault: true
    );
    #endregion

    #region Instance methods
    public void Initialize(IncrementalGeneratorInitializationContext context)
    {
        IncrementalValuesProvider<INamedTypeSymbol> namedTypeSymbols = context.SyntaxProvider
            .ForAttributeWithMetadataName(
                fullyQualifiedMetadataName: RESOURCE_COLLECTION_ATTRIBUTE_FULL_QUALIFIER_NAME,
                predicate:                  EvaluateAttribute,
                transform:                  ToNamedTypeSymbol
            )
            .Where(static (symbol) => symbol is not null);

        context.RegisterSourceOutput(
            namedTypeSymbols,
            static (sourceProductionContext, namedTypeSymbol) =>
            {
                sourceProductionContext.ReportDiagnostic(
                    Diagnostic.Create(DebugDescriptor, Location.None, namedTypeSymbol.Name)
                );

                sourceProductionContext.AddSource(
                    $"{namedTypeSymbol.Name}.g.cs",
                    SourceText.From(GetSourceContent(namedTypeSymbol), Encoding.UTF8)
                );
            }
        );
    }
    #endregion

    #region Static methods
    private static bool EvaluateAttribute(SyntaxNode syntaxNode, CancellationToken cancellationToken)
    {
        return true;
    }

    private static string GetSourceContent(INamedTypeSymbol namedTypeSymbol)
    {
        ArgumentNullException.ThrowIfNull(namedTypeSymbol);

        IEnumerable<AttributeData> hasResourceSectionAttributes = namedTypeSymbol.GetAttributes();

        StringBuilder stringBuilder = new();

        // TODO: Construct the code.

        return stringBuilder.ToString();
    }

    private static INamedTypeSymbol ToNamedTypeSymbol(
        GeneratorAttributeSyntaxContext generatorAttributeSyntaxContext,
        CancellationToken               cancellationToken)
    {
        return (INamedTypeSymbol)generatorAttributeSyntaxContext.TargetSymbol;
    }
    #endregion
}