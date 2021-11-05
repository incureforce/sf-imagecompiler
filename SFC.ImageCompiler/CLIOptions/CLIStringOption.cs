using System.Collections.Generic;

namespace SFC.ImageCompiler
{
    public class CLIStringOption : ICLIOptionWithParameter
    {
        public CLIStringOption(IEnumerable<string> keys)
        {
            Keys = keys;
        }

        public IEnumerable<string> Keys {
            get;
        }

        public string Text {
            get; set;
        }

        public bool TryParse(string text)
        {
            Text = text;

            return true;
        }
    }
}
