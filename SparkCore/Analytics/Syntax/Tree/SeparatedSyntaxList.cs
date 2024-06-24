using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.Immutable;

namespace SparkCore.Analytics.Syntax.Tree;


public abstract class SeparatedSyntaxList
{
    public abstract IEnumerable<SyntaxNode> GetWithSeparators();
}

public sealed class SeparatedSyntaxList<T> : SeparatedSyntaxList, IEnumerable<T>
    where T : SyntaxNode
{
    private readonly List<SyntaxNode> _nodesAndSeparators;
    public SeparatedSyntaxList(List<SyntaxNode> separatorsAndNodes)
    {
        _nodesAndSeparators = separatorsAndNodes;
    }
    public int Count => (_nodesAndSeparators.Count + 1) / 2;
    public T this[int index] => (T)_nodesAndSeparators[index * 2];
    public SyntaxToken GetSeparator(int index)
    {
        if (index < 0 || index >= Count - 1)
        {
            throw new ArgumentOutOfRangeException(nameof(index));
        }

        return (SyntaxToken)_nodesAndSeparators[(index * 2) + 1];
    }
    public override IEnumerable<SyntaxNode> GetWithSeparators() => _nodesAndSeparators;
    public IEnumerator<T> GetEnumerator()
    {
        for (var i = 0; i < Count; i++)
        {
            yield return this[i];
        }
    }
    IEnumerator IEnumerable.GetEnumerator()
    {
        return GetEnumerator();
    }
}
