using System.Collections.Generic;
using System.Diagnostics;

namespace SFC.ImageCompiler
{
    [DebuggerDisplay("Number {GetDebugKeys()}")]
    public class CLINumberOption : CLIOptionBase, ICLIOptionWithParameter
    {
        public CLINumberOption(IEnumerable<string> keys) : base(keys)
        {
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
