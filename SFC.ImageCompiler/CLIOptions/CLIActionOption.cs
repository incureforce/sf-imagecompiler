using System;
using System.Collections.Generic;

namespace SFC.ImageCompiler
{
    public class CLIActionOption : ICLIOptionWithSet
    {
        public CLIActionOption(IEnumerable<string> keys)
        {
            Keys = keys;
        }

        public IEnumerable<string> Keys {
            get;
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
