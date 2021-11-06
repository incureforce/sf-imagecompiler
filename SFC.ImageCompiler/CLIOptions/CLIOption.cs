using System.Collections.Generic;
using System.Linq;

namespace SFC.ImageCompiler
{
    public abstract class CLIOptionBase  : ICLIOption
    {
        public CLIOptionBase(IEnumerable<string> keys)
        {
            Keys = keys;
        }

        public IEnumerable<string> Keys {
            get;
        }

        protected string GetDebugKeys()
        {
            return string.Join(", ", Keys.Select(x => x ?? "(default)"));
        }
    }
}
