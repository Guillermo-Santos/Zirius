using System.Collections.Generic;
using System.Collections.Immutable;
using SparkCore.Analytics.Binding.Tree.Statements;
using SparkCore.Analytics.Symbols;
using SparkCore.IO.Diagnostics;

namespace SparkCore.Analytics.Binding.Tree;
internal sealed class BoundGlobalScope
{
    public BoundGlobalScope(BoundGlobalScope? previous, IEnumerable<Diagnostic> diagnostics, FunctionSymbol? mainFunction, FunctionSymbol? scriptFunction, IEnumerable<FunctionSymbol> functions, IEnumerable<VariableSymbol> variables, IEnumerable<BoundStatement> statements)
    {
        Previous = previous;
        Diagnostics = diagnostics;
        MainFunction = mainFunction;
        ScriptFunction = scriptFunction;
        Functions = functions;
        Variables = variables;
        Statements = statements;
    }

    public BoundGlobalScope? Previous
    {
        get;
    }
    public IEnumerable<Diagnostic> Diagnostics
    {
        get;
    }
    public FunctionSymbol? MainFunction
    {
        get;
    }
    public FunctionSymbol? ScriptFunction
    {
        get;
    }
    public IEnumerable<FunctionSymbol> Functions
    {
        get;
    }
    public IEnumerable<VariableSymbol> Variables
    {
        get;
    }
    public IEnumerable<BoundStatement> Statements
    {
        get;
    }
}
