using System.Collections.Generic;

namespace SFC.ImageCompiler
{
    public interface ICLIOption
    {
        IEnumerable<string> Keys {
            get;
        }
    }
}
