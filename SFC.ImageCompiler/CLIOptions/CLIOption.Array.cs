using System.Collections.Generic;
using System.Diagnostics;

namespace SFC.ImageCompiler
{
    [DebuggerDisplay("Array {GetDebugKeys()}")]
    public class CLIArrayOption : CLIOptionBase
    {
        private readonly LinkedList<string>
            itemList = new LinkedList<string>();

        public CLIArrayOption(IEnumerable<string> keys) : base(keys)
        {
        }

        public IEnumerable<string> Items {
            get => itemList;
        }

        public bool TryParse(string text)
        {
            itemList.AddLast(text);

            return true;
        }
    }
}
