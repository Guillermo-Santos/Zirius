using System.Collections.Generic;
using System.Collections.Immutable;
using SparkCore.Analytics.Syntax.Tree.Nodes;

namespace SparkCore.Analytics.Symbols;
public sealed class FunctionSymbol : Symbol
{
    public FunctionSymbol(string name, ParameterSymbol[] parameters, TypeSymbol type, FunctionDeclarationSyntax? declaration = null) : base(name)
    {
        Parameters = parameters;
        Type = type;
        Declaration = declaration;
    }

    public override SymbolKind Kind => SymbolKind.Function;
    public FunctionDeclarationSyntax? Declaration
    {
        get;
    }
    public ParameterSymbol[] Parameters
    {
        get;
    }
    public TypeSymbol Type
    {
        get;
    }
}
