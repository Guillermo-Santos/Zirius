using System.Collections;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Linq;

namespace SparkCore.IO.Diagnostics;

public sealed class EvaluationResult
{
    public EvaluationResult(IEnumerable<Diagnostic> diagnostics, object? value)
    {
        Diagnostics = diagnostics.ToList();
        Value = value;
    }

    public List<Diagnostic> Diagnostics
    {
        get;
    }
    public object? Value
    {
        get;
    }
}
