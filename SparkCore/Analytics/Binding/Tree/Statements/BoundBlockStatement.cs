using System.Collections.Generic;
using System.Collections.Immutable;
using System.Linq;

namespace SparkCore.Analytics.Binding.Tree.Statements
{
    internal sealed class BoundBlockStatement : BoundStatement
    {
        public BoundBlockStatement(IEnumerable<BoundStatement> statements)
        {
            Statements = statements.ToArray();
        }

        public override BoundNodeKind Kind => BoundNodeKind.BlockStatement;
        public BoundStatement[] Statements
        {
            get;
        }
    }
}
