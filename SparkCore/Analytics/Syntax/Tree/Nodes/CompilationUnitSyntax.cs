using System.Collections;
using System.Collections.Generic;
using System.Collections.Immutable;

namespace SparkCore.Analytics.Syntax.Tree.Nodes;

public sealed partial class CompilationUnitSyntax : SyntaxNode
{
    public CompilationUnitSyntax(SyntaxTree syntaxTree, IEnumerable<MemberSyntax> members, SyntaxToken endOfFileToken) : base(syntaxTree)
    {
        Members = members;
        EndOfFileToken = endOfFileToken;
    }
    public override SyntaxKind Kind => SyntaxKind.CompilationUnit;
    public IEnumerable<MemberSyntax> Members
    {
        get;
    }
    public SyntaxToken EndOfFileToken
    {
        get;
    }
}
