using System.Collections.Generic;
using System.Diagnostics;

namespace SFC.ImageCompiler
{
    public interface ICLIOption
    {
        IEnumerable<string> Keys {
            get;
        }
    }
}
