namespace SFC.ImageCompiler
{
    public class CLIOptionMissingException : CLIParseException
    {
        public CLIOptionMissingException(string text) : base(Format(text))
        {
            Text = text;
        }

        public string Text { get; }

        static string Format(string text) => $"Unable to find Option with key '{text}' or an option with a null key";

    }
}
