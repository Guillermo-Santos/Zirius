using System;
using System.CodeDom.Compiler;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using Microsoft.CodeAnalysis.Text;
using SparkCore.Generators.Utilities;

namespace SparkCore.Generators
{
    [Generator]
    public class SyntaxNodeVisitorGenerator : ISourceGenerator
    {
        public void Initialize(GeneratorInitializationContext context)
        {
        }
        public void Execute(GeneratorExecutionContext context)
        {
            var compilation = (CSharpCompilation)context.Compilation;
          
            var types = GetAllTypes(compilation.Assembly);
            var syntaxNodeType = compilation.GetTypeByMetadataName("SparkCore.Analytics.Syntax.Tree.SyntaxNode");
            var syntaxNodeTypes = types.Where(t => !t.IsAbstract && IsPartial(t) && IsDerivedFrom(t, syntaxNodeType));//.OrderBy(t => t.ContainingNamespace.MetadataName).ThenBy(t => t.MetadataName);
            SourceText sourceText;

            using (var stringWriter = new StringWriter())
            using (var indentedTextWriter = new IndentedTextWriter(stringWriter, "    "))
            {
                var lastNameSpace = syntaxNodeTypes.First().ContainingNamespace;

                indentedTextWriter.WriteLine("using System;");
                indentedTextWriter.WriteLine("using System.Collections.Generic;");
                indentedTextWriter.WriteLine($"namespace {syntaxNodeType.ContainingNamespace.GetFullName()}");
                StartBlock(indentedTextWriter);
                WriteVoidImpl(syntaxNodeTypes, indentedTextWriter);
                WriteGenericImpl(syntaxNodeTypes, indentedTextWriter);
                CloseBlock(indentedTextWriter);
                indentedTextWriter.Flush();

                sourceText = SourceText.From(stringWriter.ToString(), Encoding.UTF8);
            }
            context.AddSource("Generated.cs", sourceText);
        }

        private static void WriteVoidImpl(IEnumerable<INamedTypeSymbol> syntaxNodeTypes, IndentedTextWriter indentedTextWriter)
        {
            indentedTextWriter.WriteLine("public abstract class SyntaxNodeVisitor");
            StartBlock(indentedTextWriter);

            indentedTextWriter.WriteLine("/// <summary>\r\n        /// Calls the `Accept` Method of the <paramref name=\"node\"/>, should resolve to the apropiate 'Visit[NodeType]` method.\r\n        /// </summary>\r\n        /// <param name=\"node\"></param>");
            indentedTextWriter.WriteLine("public virtual void Visit(SyntaxNode node) => node.Accept(this);");
            indentedTextWriter.WriteLine("/// <summary>\r\n        /// Default method called for unsupported <see cref=\"SyntaxNode\"/> types.\r\n        /// </summary>\r\n        /// <param name=\"node\"></param>");
            indentedTextWriter.WriteLine("public virtual void DefaultVisit(SyntaxNode node){ }");

            foreach (var type in syntaxNodeTypes)
            {
                indentedTextWriter.WriteLine($"public virtual void Visit{type.Name}({type.GetFullName()} node) => DefaultVisit(node);");
            }
            CloseBlock(indentedTextWriter);
        }
        private static void WriteGenericImpl(IEnumerable<INamedTypeSymbol> syntaxNodeTypes, IndentedTextWriter indentedTextWriter)
        {
            indentedTextWriter.WriteLine("public abstract class SyntaxNodeVisitor<TResult>");
            StartBlock(indentedTextWriter);

            indentedTextWriter.WriteLine("/// <summary>\r\n        /// Calls the `Accept` Method of the <paramref name=\"node\"/>, should resolve to the apropiate 'Visit[NodeType]` method.\r\n        /// </summary>\r\n        /// <param name=\"node\"></param>");
            indentedTextWriter.WriteLine("public virtual TResult Visit(SyntaxNode node) => node.Accept(this);");
            indentedTextWriter.WriteLine("/// <summary>\r\n        /// Default method called for unsupported <see cref=\"SyntaxNode\"/> types.\r\n        /// </summary>\r\n        /// <param name=\"node\"></param>");
            indentedTextWriter.WriteLine("public virtual TResult DefaultVisit(SyntaxNode node) => default(TResult);");

            foreach (var type in syntaxNodeTypes)
            {
                indentedTextWriter.WriteLine($"public virtual TResult Visit{type.Name}({type.GetFullName()} node) => DefaultVisit(node);");
            }
            CloseBlock(indentedTextWriter);
        }

        private static void WriteNameSpaceHead(INamedTypeSymbol syntaxNodeType, IndentedTextWriter indentedTextWriter, INamedTypeSymbol type)
        {
            indentedTextWriter.WriteLine($"namespace SparkCore.Analytics.Syntax.Tree.{type.ContainingNamespace.MetadataName}");
        }
        private static void StartBlock(IndentedTextWriter indentedTextWriter)
        {
            indentedTextWriter.WriteLine("{");
            indentedTextWriter.Indent++;
        }
        private static void CloseBlock(IndentedTextWriter indentedTextWriter)
        {
            indentedTextWriter.Indent--;
            indentedTextWriter.WriteLine("}");
        }

        private IReadOnlyList<INamedTypeSymbol> GetAllTypes(IAssemblySymbol symbol)
        {
            var result = new List<INamedTypeSymbol>();
            GetAllTypes(result, symbol.GlobalNamespace);
            result.OrderBy(t => t.ContainingNamespace.MetadataName).ThenBy(x => x.MetadataName);
            return result;
        }

        private void GetAllTypes(List<INamedTypeSymbol> result, INamespaceOrTypeSymbol symbol)
        {
            if(symbol is INamedTypeSymbol type)
            {
                result.Add(type);
            }

            foreach (var child in symbol.GetMembers())
            {
                if (child is INamespaceOrTypeSymbol nsChild)
                {
                    GetAllTypes(result, nsChild);
                }
            }
        }
        private bool IsDerivedFrom(ITypeSymbol type, INamedTypeSymbol baseType)
        {
            while(type != null)
            {
                if (SymbolEqualityComparer.Default.Equals(type, baseType))
                {
                    return true;
                }

                type = type.BaseType;
            }
            return false;
        }
        private bool IsPartial(INamedTypeSymbol type)
        {
            foreach(var declaration in type.DeclaringSyntaxReferences)
            {
                var syntax = declaration.GetSyntax();
                if(syntax is ClassDeclarationSyntax typeDeclaration)
                {
                    foreach(var modifier in typeDeclaration.Modifiers)
                    {
                        if (modifier.ValueText == "partial")
                        {
                            return true;
                        }
                    }
                }
            }

            return false;
        }
    }
}