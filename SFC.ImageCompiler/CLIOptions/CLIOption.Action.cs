using System;
using System.Collections.Generic;
using System.Diagnostics;

namespace SFC.ImageCompiler
{
    [DebuggerDisplay("Action {GetDebugKeys()}")]
    public class CLIActionOption : CLIOptionBase, ICLIOptionWithSet
    {
        public CLIActionOption(IEnumerable<string> keys) : base(keys)
        {
        }

        public bool Invoke {
            get; set;
        }

        public bool TrySet()
        {
            if (Invoke) {
                throw new NotSupportedException("Action already active");
            }

            Invoke = true;

            return true;
        }
    }
}
