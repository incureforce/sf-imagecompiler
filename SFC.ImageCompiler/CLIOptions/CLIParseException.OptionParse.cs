namespace SFC.ImageCompiler
{
    public class CLIOptionParseException : CLIParseException
    {
        public CLIOptionParseException(ICLIOption option, string text) : base(Format(option, text))
        {
            Option = option;
            Text = text;
        }

        public string Text { get; }

        public ICLIOption Option { get; }

        static string Format(ICLIOption option, string text) => $"Unable to parse Option with key '{string.Join(" ", option.Keys)}' with value '{text}'";
    }
}
