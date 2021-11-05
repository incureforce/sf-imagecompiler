using System.Collections.Generic;

namespace SFC.ImageCompiler
{
    public class CLINumberOption : ICLIOptionWithParameter
    {
        public CLINumberOption(IEnumerable<string> keys)
        {
            Keys = keys;
        }

        public IEnumerable<string> Keys {
            get;
        }

        public int Number {
            get; set;
        }

        public bool TryParse(string text)
        {
            if (int.TryParse(text, out var flag) is false) {
                return false;
            }

            Number = flag;

            return true;

        }
    }
}
