using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Linq;
using System.Reflection;

namespace SparkCore.Analytics.Symbols;

internal static class BuiltinFunctions
{
    public static FunctionSymbol Print = new("print", new ParameterSymbol[] { new("value", TypeSymbol.Any, 0) }, TypeSymbol.Void);
    public static FunctionSymbol Input = new("input", Array.Empty<ParameterSymbol>(), TypeSymbol.String);
    public static FunctionSymbol Random = new("random", new ParameterSymbol[] { new("max", TypeSymbol.Int, 0) }, TypeSymbol.Int);
   
    internal static IEnumerable<FunctionSymbol> GetAll()
    {
        yield return Print;
        yield return Input;
        yield return Random;
    }
}