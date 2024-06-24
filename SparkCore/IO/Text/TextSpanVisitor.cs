using SparkCore.Analytics.Syntax.Tree;
using SparkCore.Analytics.Syntax.Tree.Expressions;
using SparkCore.Analytics.Syntax.Tree.Nodes;
using SparkCore.Analytics.Syntax.Tree.Statements;

namespace SparkCore.IO.Text
{
    public class TextSpanVisitor : SyntaxNodeVisitor<TextSpan>
    {
        public override TextSpan VisitAssignmentExpressionSyntax(AssignmentExpressionSyntax node)
        {
            var start = node.IdentifierToken.Span;
            var end = node.Expression.Accept(this);

            return TextSpan.FromBounds(start.Start, end.End);
        }
        public override TextSpan VisitBinaryExpressionSyntax(BinaryExpressionSyntax node)
        {
            var start = node.Left.Accept(this);
            var end = node.Right.Accept(this);

            return TextSpan.FromBounds(start.Start, end.End);
        }
        public override TextSpan VisitBlockStatementSyntax(BlockStatementSyntax node)
        {
            var start = node.OpenBraceToken.Span;
            var end = node.CloseBraceToken.Span;

            return TextSpan.FromBounds(start.Start, end.End);
        }
        public override TextSpan VisitBreakStatementSyntax(BreakStatementSyntax node)
        {
            return node.Keyword.Span;
        }
        public override TextSpan VisitCallExpressionSyntax(CallExpressionSyntax node)
        {
            var start = node.Identifier.Span;
            var end  = node.CloseParentesis.Span;

            return TextSpan.FromBounds(start.Start, end.End);
        }
        public override TextSpan VisitCompilationUnitSyntax(CompilationUnitSyntax node)
        {
            //Will this one ever be used? Xd
            return TextSpan.FromBounds(0, node.EndOfFileToken.Span.End);
        }
        public override TextSpan VisitContinueStatementSyntax(ContinueStatementSyntax node)
        {
            return node.Span;
        }
        public override TextSpan VisitDoWhileStatementSyntax(DoWhileStatementSyntax node)
        {
            var start = node.DoKeyword.Span;
            var end = node.Condition.Accept(this);

            return TextSpan.FromBounds(start.Start, end.End);
        }
        public override TextSpan VisitElseClauseSyntax(ElseClauseSyntax node)
        {
            var start = node.ElseKeyword.Span;
            var end = node.ElseStatement.Accept(this);

            return TextSpan.FromBounds(start.Start, end.End);
        }

        public override TextSpan VisitExpressionStatementSyntax(ExpressionStatementSyntax node)
        {
            return node.Expression.Accept(this);
        }
        public override TextSpan VisitForStatementSyntax(ForStatementSyntax node)
        {
            var start = node.Keyword.Span;
            var end = node.Body.Accept(this);

            return TextSpan.FromBounds(start.Start, end.End);
        }
        public override TextSpan VisitFunctionDeclarationSyntax(FunctionDeclarationSyntax node)
        {
            var start = node.FunctionKeyword.Span;
            var end = node.Body.Accept(this);

            return TextSpan.FromBounds(start.Start, end.End);
        }
        public override TextSpan VisitGlobalStatementSyntax(GlobalStatementSyntax node)
        {
            return node.Statement.Accept(this);
        }
        public override TextSpan VisitIfStatementSyntax(IfStatementSyntax node)
        {
            var start = node.IfKeyword.Span;
            var end = (node.ElseClause is null) ? node.ThenStatement.Accept(this) : node.ElseClause.Accept(this);

            return TextSpan.FromBounds(start.Start, end.End);
        }
        public override TextSpan VisitLiteralExpressionSyntax(LiteralExpressionSyntax node)
        {
            return node.LiteralToken.Span;
        }
        public override TextSpan VisitNameExpressionSyntax(NameExpressionSyntax node)
        {
            return node.IdentifierToken.Span;
        }
        public override TextSpan VisitParameterSyntax(ParameterSyntax node)
        {
            var start = node.Identifier.Span;
            var end = node.Type.Accept(this);

            return TextSpan.FromBounds(start.Start, end.End);
        }
        public override TextSpan VisitParenthesizedExpressionSyntax(ParenthesizedExpressionSyntax node)
        {
            var start = node.OpenParenthesisToken.Span;
            var end = node.CloseParenthesisToken.Span;

            return TextSpan.FromBounds(start.Start, end.End);
        }
        public override TextSpan VisitReturnStatementSyntax(ReturnStatementSyntax node)
        {
            var start = node.ReturnKeyword.Span;

            if (node.Expression is null)
            {
                return start;
            }

            var end = node.Expression.Accept(this);

            return TextSpan.FromBounds(start.Start, end.End);
        }
        public override TextSpan VisitTypeClauseSyntax(TypeClauseSyntax node)
        {
            var start = node.ColonToken.Span;
            var end = node.Identifier.Span;

            return TextSpan.FromBounds(start.Start, end.End);
        }
        public override TextSpan VisitUnaryExpressionSyntax(UnaryExpressionSyntax node)
        {
            var start = node.OperatorToken.Span;
            var end = node.Operand.Accept(this);

            return TextSpan.FromBounds(start.Start, end.End);
        }
        public override TextSpan VisitVariableDeclarationStatementSyntax(VariableDeclarationStatementSyntax node)
        {
            var start = node.Keyword.Span;
            var end = node.Initializer.Accept(this);

            return TextSpan.FromBounds(start.Start, end.End);
        }
        public override TextSpan VisitWhileStatementSyntax(WhileStatementSyntax node)
        {
            var start = node.Keyword.Span;
            var end = node.Body.Accept(this);

            return TextSpan.FromBounds(start.Start, end.End);
        }
    }
}