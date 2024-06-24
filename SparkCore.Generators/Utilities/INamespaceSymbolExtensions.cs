using System.Text;
using Microsoft.CodeAnalysis;

namespace SparkCore.Generators.Utilities
{
    internal static class INamespaceSymbolExtensions
    {
        public static string GetFullName(this INamespaceSymbol namespaceSymbol)
        {
            var sb = new StringBuilder();
            WriteNameSpaceHead(sb, namespaceSymbol.ContainingNamespace);
            sb.Append(namespaceSymbol.MetadataName);

            return sb.ToString();
        }

        internal static void WriteNameSpaceHead(StringBuilder sb, INamespaceSymbol namespaceSymbol)
        {
            if (namespaceSymbol is null || string.IsNullOrEmpty(namespaceSymbol.MetadataName))
            {
                return;
            }
            WriteNameSpaceHead(sb, namespaceSymbol.ContainingNamespace);
            sb.Append(namespaceSymbol.MetadataName);
            sb.Append(".");
        }
    }
}
