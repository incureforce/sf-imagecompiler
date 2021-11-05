using System;

namespace SFC.ImageCompiler
{
    public class CLIParseException : Exception
    {
        public CLIParseException(string message) : base(message)
        {
        }
    }
}
