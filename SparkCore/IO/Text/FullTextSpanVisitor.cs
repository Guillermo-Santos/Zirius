using SparkCore.Analytics.Syntax.Tree;
using SparkCore.Analytics.Syntax.Tree.Expressions;
using SparkCore.Analytics.Syntax.Tree.Nodes;
using SparkCore.Analytics.Syntax.Tree.Statements;

namespace SparkCore.IO.Text
{
    public class FullTextSpanVisitor : SyntaxNodeVisitor<TextSpan>
    {
        public override TextSpan VisitAssignmentExpressionSyntax(AssignmentExpressionSyntax node)
        {
            var start = node.IdentifierToken.FullSpan;
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
            var start = node.OpenBraceToken.FullSpan;
            var end = node.CloseBraceToken.FullSpan;

            return TextSpan.FromBounds(start.Start, end.End);
        }
        public override TextSpan VisitBreakStatementSyntax(BreakStatementSyntax node)
        {
            return node.Keyword.FullSpan;
        }
        public override TextSpan VisitCallExpressionSyntax(CallExpressionSyntax node)
        {
            var start = node.Identifier.FullSpan;
            var end = node.CloseParentesis.FullSpan;

            return TextSpan.FromBounds(start.Start, end.End);
        }
        public override TextSpan VisitCompilationUnitSyntax(CompilationUnitSyntax node)
        {
            //Will this one ever be used? Xd
            return TextSpan.FromBounds(0, node.EndOfFileToken.FullSpan.End);
        }
        public override TextSpan VisitContinueStatementSyntax(ContinueStatementSyntax node)
        {
            return node.FullSpan;
        }
        public override TextSpan VisitDoWhileStatementSyntax(DoWhileStatementSyntax node)
        {
            var start = node.DoKeyword.FullSpan;
            var end = node.Condition.Accept(this);

            return TextSpan.FromBounds(start.Start, end.End);
        }
        public override TextSpan VisitElseClauseSyntax(ElseClauseSyntax node)
        {
            var start = node.ElseKeyword.FullSpan;
            var end = node.ElseStatement.Accept(this);

            return TextSpan.FromBounds(start.Start, end.End);
        }

        public override TextSpan VisitExpressionStatementSyntax(ExpressionStatementSyntax node)
        {
            return node.Expression.Accept(this);
        }
        public override TextSpan VisitForStatementSyntax(ForStatementSyntax node)
        {
            var start = node.Keyword.FullSpan;
            var end = node.Body.Accept(this);

            return TextSpan.FromBounds(start.Start, end.End);
        }
        public override TextSpan VisitFunctionDeclarationSyntax(FunctionDeclarationSyntax node)
        {
            var start = node.FunctionKeyword.FullSpan;
            var end = node.Body.Accept(this);

            return TextSpan.FromBounds(start.Start, end.End);
        }
        public override TextSpan VisitGlobalStatementSyntax(GlobalStatementSyntax node)
        {
            return node.Statement.Accept(this);
        }
        public override TextSpan VisitIfStatementSyntax(IfStatementSyntax node)
        {
            var start = node.IfKeyword.FullSpan;
            var end = (node.ElseClause is null) ? node.ThenStatement.Accept(this) : node.ElseClause.Accept(this);

            return TextSpan.FromBounds(start.Start, end.End);
        }
        public override TextSpan VisitLiteralExpressionSyntax(LiteralExpressionSyntax node)
        {
            return node.LiteralToken.FullSpan;
        }
        public override TextSpan VisitNameExpressionSyntax(NameExpressionSyntax node)
        {
            return node.IdentifierToken.FullSpan;
        }
        public override TextSpan VisitParameterSyntax(ParameterSyntax node)
        {
            var start = node.Identifier.FullSpan;
            var end = node.Type.Accept(this);

            return TextSpan.FromBounds(start.Start, end.End);
        }
        public override TextSpan VisitParenthesizedExpressionSyntax(ParenthesizedExpressionSyntax node)
        {
            var start = node.OpenParenthesisToken.FullSpan;
            var end = node.CloseParenthesisToken.FullSpan;

            return TextSpan.FromBounds(start.Start, end.End);
        }
        public override TextSpan VisitReturnStatementSyntax(ReturnStatementSyntax node)
        {
            var start = node.ReturnKeyword.FullSpan;

            if (node.Expression is null)
            {
                return start;
            }

            var end = node.Expression.Accept(this);

            return TextSpan.FromBounds(start.Start, end.End);
        }
        public override TextSpan VisitTypeClauseSyntax(TypeClauseSyntax node)
        {
            var start = node.ColonToken.FullSpan;
            var end = node.Identifier.FullSpan;

            return TextSpan.FromBounds(start.Start, end.End);
        }
        public override TextSpan VisitUnaryExpressionSyntax(UnaryExpressionSyntax node)
        {
            var start = node.OperatorToken.FullSpan;
            var end = node.Operand.Accept(this);

            return TextSpan.FromBounds(start.Start, end.End);
        }
        public override TextSpan VisitVariableDeclarationStatementSyntax(VariableDeclarationStatementSyntax node)
        {
            var start = node.Keyword.FullSpan;
            var end = node.Initializer.Accept(this);

            return TextSpan.FromBounds(start.Start, end.End);
        }
        public override TextSpan VisitWhileStatementSyntax(WhileStatementSyntax node)
        {
            var start = node.Keyword.FullSpan;
            var end = node.Body.Accept(this);

            return TextSpan.FromBounds(start.Start, end.End);
        }
    }
}