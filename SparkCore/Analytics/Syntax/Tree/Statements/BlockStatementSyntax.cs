using System.Collections;
using System.Collections.Generic;
using System.Collections.Immutable;

namespace SparkCore.Analytics.Syntax.Tree.Statements;

public sealed partial class BlockStatementSyntax : StatementSyntax
{
    public BlockStatementSyntax(SyntaxTree syntaxTree, SyntaxToken openBraceToken, IEnumerable<StatementSyntax> statements, SyntaxToken closeBraceToken) : base(syntaxTree)
    {
        OpenBraceToken = openBraceToken;
        Statements = statements;
        CloseBraceToken = closeBraceToken;
    }

    public override SyntaxKind Kind => SyntaxKind.BlockStatement;
    public SyntaxToken OpenBraceToken
    {
        get;
    }
    public IEnumerable<StatementSyntax> Statements
    {
        get;
    }
    public SyntaxToken CloseBraceToken
    {
        get;
    }

}
