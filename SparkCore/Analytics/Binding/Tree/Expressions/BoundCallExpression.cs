using System.Collections.Generic;
using System.Collections.Immutable;
using System.Linq;
using SparkCore.Analytics.Symbols;

namespace SparkCore.Analytics.Binding.Tree.Expressions;

internal sealed class BoundCallExpression : BoundExpression
{
    public BoundCallExpression(FunctionSymbol function, IEnumerable<BoundExpression> arguments)
    {
        Function = function;
        Arguments = arguments.ToArray();
    }

    public override TypeSymbol Type => Function.Type;
    public override BoundNodeKind Kind => BoundNodeKind.CallExpression;
    public FunctionSymbol Function
    {
        get;
    }
    public BoundExpression[] Arguments
    {
        get;
    }
}
