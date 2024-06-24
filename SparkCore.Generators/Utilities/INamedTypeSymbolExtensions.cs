using System.Text;
using Microsoft.CodeAnalysis;

namespace SparkCore.Generators.Utilities
{
    internal static class INamedTypeSymbolExtensions
    {
        public static string GetFullName(this INamedTypeSymbol type)
        {
            var sb = new StringBuilder();
            INamespaceSymbolExtensions.WriteNameSpaceHead(sb, type.ContainingNamespace);
            sb.Append(type.Name);
            return sb.ToString();
        }
    }
}
