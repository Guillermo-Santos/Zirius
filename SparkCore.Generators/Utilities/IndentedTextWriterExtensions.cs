using System;
using System.CodeDom.Compiler;
using System.Collections.Generic;
using Microsoft.CodeAnalysis;

namespace SparkCore.Generators.Utilities
{
    internal static class IndentedTextWriterExtensions
    {
        public static void WriteNameSpaceHead(this IndentedTextWriter indentedTextWriter, INamedTypeSymbol type)
        {
            indentedTextWriter.WriteLine(type.ContainingNamespace.GetFullName());
        }
    }
}
