namespace SFC.ImageCompiler
{
    public class CLIOptionSetException : CLIParseException
    {
        public CLIOptionSetException(ICLIOption option) : base(Format(option))
        {
            Option = option;
        }

        public ICLIOption Option { get; }

        static string Format(ICLIOption option) => $"Unable to set Option with key '{string.Join(" ", option.Keys)}'";
    }
}
