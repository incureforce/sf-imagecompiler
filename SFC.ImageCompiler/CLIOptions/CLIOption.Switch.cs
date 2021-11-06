using System.Collections.Generic;
using System.Diagnostics;

namespace SFC.ImageCompiler
{
    [DebuggerDisplay("Switch {GetDebugKeys()}")]
    public class CLISwitchOption : CLIOptionBase, ICLIOptionWithSet, ICLIOptionWithParameter
    {
        public CLISwitchOption(IEnumerable<string> keys) : base(keys)
        {
        }

        public bool Flag {
            get; set;
        }

        public bool TryParse(string text)
        {
            if (bool.TryParse(text, out var flag) is false) {
                return false;
            }

            Flag = flag;

            return true;

        }

        public bool TrySet()
        {
            Flag = Flag == false;

            return true;
        }
    }
}
