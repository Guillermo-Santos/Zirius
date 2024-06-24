namespace SparkCore.IO.Text;
public struct TextSpan
{
    public TextSpan(int start, int length)
    {
        Start = start;
        Length = length;
    }
    public int Start { get; init; }
    public int Length;
    public readonly int End => Start + Length;

    public static TextSpan FromBounds(int start, int end)
    {
        var lenght = end - start;
        return new TextSpan(start, lenght);
    }
    public bool OverlapsWith(TextSpan span)
    {
        return Start < span.End &&
               End > span.Start;
    }
    public override string ToString() => $"{Start}..{End}";
}
