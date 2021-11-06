using System.Collections.Generic;
using System.Diagnostics;

namespace SFC.ImageCompiler
{
    [DebuggerDisplay("String {GetDebugKeys()}")]
    public class CLIStringOption : CLIOptionBase, ICLIOptionWithParameter
    {
        public CLIStringOption(IEnumerable<string> keys) : base(keys)
        {
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
