using System;
using System.Collections.Generic;
using System.Text;
using SparkCore;
using Sys = Cosmos.System;
using spi.Replies;
using SparkCore.Analytics.Syntax.Tree;
using SparkCore.Analytics.Symbols;

namespace CosmosKernel;
public class Kernel : Sys.Kernel
{
    protected override void BeforeRun()
    {
        Console.WriteLine("Cosmos booted successfully. Type a line of text to get it echoed back.");
    }

    protected override void Run()
    {
        Console.WriteLine("Hellow world!");
        _ = SyntaxTree.Parse("print(\"Hola Mundo!\")");
        //var compilation = Compilation.CreateScript(parent, tree);
        //compilation.Evaluate(_variables);
        //_variables.Clear();
        Console.WriteLine("Hellow world!");
    }
}
