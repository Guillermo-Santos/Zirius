using System.Reflection;

namespace spi.Replies;
public abstract partial class Repl
{
    private sealed class MetaCommand
    {
        public MetaCommand(string name, string description, MethodInfo method)
        {
            Name = name;
            Description = description;
            Method = method;
        }

        public string Name
        {
            get;
        }
        public string Description
        {
            get;
            set;
        }
        public MethodInfo Method
        {
            get;
        }
    }
}