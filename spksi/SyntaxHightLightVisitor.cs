using System.Collections.Immutable;
using SparkCore.Analytics.Syntax;
using SparkCore.Analytics.Syntax.Tree;
using SparkCore.Analytics.Syntax.Tree.Expressions;
using SparkCore.Analytics.Syntax.Tree.Nodes;
using SparkCore.Analytics.Syntax.Tree.Statements;
using SparkCore.IO.Text;
using spi.Replies;

public class SyntaxHightLightVisitor : SyntaxNodeVisitor
{
    private readonly List<ClassifiedSpan> spans = new();
    public IReadOnlyCollection<ClassifiedSpan> ClassifiedSpans => spans;
    public TextSpan TextSpan
    {
        get; set;
    }

    public override void DefaultVisit(SyntaxNode node) 
    {
        if (node is SyntaxToken token) 
        {
            ClassifyToken(token);
        }
    }

    public override void VisitAssignmentExpressionSyntax(AssignmentExpressionSyntax node)
    {
        ClassifyToken(node.IdentifierToken);
        ClassifyToken(node.EqualsToken);
        node.Expression.Accept(this);
    }
    public override void VisitBinaryExpressionSyntax(BinaryExpressionSyntax node)
    {
        node.Left.Accept(this);
        ClassifyToken(node.OperatorToken);
        node.Right.Accept(this);
    }
    public override void VisitBlockStatementSyntax(BlockStatementSyntax node)
    {
        ClassifyToken(node.OpenBraceToken);
        foreach (var statement in node.Statements) 
        { 
            statement.Accept(this);
        }
        ClassifyToken(node.CloseBraceToken);
    }
    public override void VisitBreakStatementSyntax(BreakStatementSyntax node)
    {
        ClassifyToken(node.Keyword);
    }
    public override void VisitCallExpressionSyntax(CallExpressionSyntax node)
    {
        ClassifyToken(node.Identifier);
        ClassifyToken(node.OpenParentesis);
        foreach(var arg in node.Arguments.GetWithSeparators())
        {
            arg.Accept(this);
        }
        ClassifyToken(node.CloseParentesis);
    }
    public override void VisitCompilationUnitSyntax(CompilationUnitSyntax node)
    {
        foreach(var member in node.Members)
        {
            member.Accept(this);
        }
        ClassifyToken(node.EndOfFileToken);
    }
    public override void VisitContinueStatementSyntax(ContinueStatementSyntax node)
    {
        ClassifyToken(node.Keyword);
    }
    public override void VisitDoWhileStatementSyntax(DoWhileStatementSyntax node)
    {
        ClassifyToken(node.DoKeyword);
        node.Body.Accept(this);
        ClassifyToken(node.WhileKeyword);
        node.Condition.Accept(this);
    }
    public override void VisitElseClauseSyntax(ElseClauseSyntax node)
    {
        ClassifyToken(node.ElseKeyword);
        node.ElseStatement.Accept(this);
    }
    public override void VisitExpressionStatementSyntax(ExpressionStatementSyntax node)
    {
        node.Expression.Accept(this);
    }
    public override void VisitForStatementSyntax(ForStatementSyntax node)
    {
        ClassifyToken(node.Keyword);
        ClassifyToken(node.Identifier);
        ClassifyToken(node.EqualsToken);
        node.LowerBound.Accept(this);
        ClassifyToken(node.ToKeyword);
        node.UpperBound.Accept(this);
        node.Body.Accept(this);
    }
    public override void VisitFunctionDeclarationSyntax(FunctionDeclarationSyntax node)
    {
        ClassifyToken(node.FunctionKeyword);
        ClassifyToken(node.Identifier);
        ClassifyToken(node.OpenParenthesisToken);
        foreach(var parameter in node.Parameters.GetWithSeparators())
        {
            parameter.Accept(this);
        }
        ClassifyToken(node.CloseParethesisToken);
        node.Type?.Accept(this);
        node.Body.Accept(this);
    }
    public override void VisitGlobalStatementSyntax(GlobalStatementSyntax node)
    {
        node.Statement.Accept(this);
    }
    public override void VisitIfStatementSyntax(IfStatementSyntax node)
    {
        ClassifyToken(node.IfKeyword);
        node.Condition.Accept(this);
        node.ThenStatement.Accept(this);
        node.ElseClause?.Accept(this);
    }
    public override void VisitLiteralExpressionSyntax(LiteralExpressionSyntax node)
    {
        ClassifyToken(node.LiteralToken);
    }
    public override void VisitNameExpressionSyntax(NameExpressionSyntax node)
    {
        ClassifyToken(node.IdentifierToken);
    }
    public override void VisitParameterSyntax(ParameterSyntax node)
    {
        ClassifyToken(node.Identifier);
        node.Type.Accept(this);
    }
    public override void VisitParenthesizedExpressionSyntax(ParenthesizedExpressionSyntax node)
    {
        ClassifyToken(node.OpenParenthesisToken);
        node.Expression.Accept(this);
        ClassifyToken(node.CloseParenthesisToken);
    }
    public override void VisitReturnStatementSyntax(ReturnStatementSyntax node)
    {
        ClassifyToken(node.ReturnKeyword);
        node.Expression?.Accept(this);
    }
    public override void VisitTypeClauseSyntax(TypeClauseSyntax node)
    {
        ClassifyToken(node.ColonToken);
        ClassifyToken(node.Identifier);
    }
    public override void VisitUnaryExpressionSyntax(UnaryExpressionSyntax node)
    {
        ClassifyToken(node.OperatorToken);
        node.Operand.Accept(this);
    }
    public override void VisitVariableDeclarationStatementSyntax(VariableDeclarationStatementSyntax node)
    {
        ClassifyToken(node.Keyword);
        ClassifyToken(node.Identifier);
        node.TypeClause?.Accept(this);
        ClassifyToken(node.EqualsToken);
        node.Initializer.Accept(this);
    }
    public override void VisitWhileStatementSyntax(WhileStatementSyntax node)
    {
        ClassifyToken(node.Keyword);
        node.Condition.Accept(this);
        node.Body.Accept(this);
    }

    private void ClassifyToken(SyntaxToken token)
    {
        foreach (var leadingTrivia in token.LeadingTrivia)
        {
            ClassifyTrivia(leadingTrivia, TextSpan);
        }

        AddClassification(token.Kind, token.Span, TextSpan);

        foreach (var trailingTrivia in token.TrailingTrivia)
        {
            ClassifyTrivia(trailingTrivia, TextSpan);
        }
    }

    private void ClassifyTrivia(SyntaxTrivia trivia, TextSpan span)
    {
        AddClassification(trivia.Kind, trivia.Span, span);
    }

    private void AddClassification(SyntaxKind elementKind, TextSpan elementSpan, TextSpan span)
    {
        if (!elementSpan.OverlapsWith(span))
        {
            return;
        }

        var adjustedStart = Math.Max(elementSpan.Start, span.Start);
        var adjustedEnd = Math.Min(elementSpan.End, span.End);
        var adjustedSpan = TextSpan.FromBounds(adjustedStart, adjustedEnd);
        var classification = GetClassification(elementKind);

        var classifiedSpan = new ClassifiedSpan(adjustedSpan, classification);
        spans.Add(classifiedSpan);
    }

    private static Classification GetClassification(SyntaxKind elementKind)
    {
        return elementKind switch
        {
            SyntaxKind.IdentifierToken
                => Classification.Identifier,
            SyntaxKind.NumberToken
                => Classification.Number,
            SyntaxKind.StringToken
                => Classification.String,
            SyntaxKind.SingleLineCommentTrivia or SyntaxKind.MultiLineCommentTrivia
                => Classification.Comment,
            _ => elementKind.IsKeyWord()
                    ? Classification.Keyword
                : elementKind.IsOperator()
                    ? Classification.Operator
                    : Classification.Text
        };
    }
}