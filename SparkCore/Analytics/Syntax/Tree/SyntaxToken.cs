using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Linq;
using SparkCore.IO.Text;

namespace SparkCore.Analytics.Syntax.Tree;
public sealed class SyntaxToken : SyntaxNode
{
    public SyntaxToken(SyntaxTree syntaxTree, SyntaxKind type, int position, string? text, object? value, List<SyntaxTrivia> leadingTrivia, List<SyntaxTrivia> trailingTrivia)
        : base(syntaxTree)
    {
        Kind = type;
        Position = position;
        Text = text ?? string.Empty;
        IsMissing = text == null;
        Value = value;
        LeadingTrivia = leadingTrivia;
        TrailingTrivia = trailingTrivia;
    }

    public override SyntaxKind Kind
    {
        get;
    }
    public int Position
    {
        get;
    }
    public string Text
    {
        get;
    }
    public object? Value
    {
        get;
    }
    public override TextSpan Span => new(Position, Text.Length);
    public override TextSpan FullSpan
    {
        get
        {
            var start = LeadingTrivia.Count == 0
                            ? Span.Start
                            : LeadingTrivia.First().Span.Start;
            var end = TrailingTrivia.Count == 0
                            ? Span.End
                            : TrailingTrivia.Last().Span.End;
            return TextSpan.FromBounds(start, end);
        }
    }
    public List<SyntaxTrivia> LeadingTrivia
    {
        get;
    }

    public List<SyntaxTrivia> TrailingTrivia
    {
        get;
    }
    public override IEnumerable<SyntaxNode> GetChildren()
    {
        return Array.Empty<SyntaxNode>();
    }
    public bool IsMissing { get; }
}
