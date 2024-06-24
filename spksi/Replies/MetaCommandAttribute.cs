namespace spi.Replies;
public abstract partial class Repl
{
    [AttributeUsage(AttributeTargets.Method, AllowMultiple = false)]
    protected sealed class MetaCommandAttribute : Attribute
    {
        public MetaCommandAttribute(string name, string description)
        {
            Name = name;
            Description = description;
        }

        public string Name
        {
            get;
        }
        public string Description
        {
            get;
        }
    }
}