using System.Collections.Generic;

namespace SFC.ImageCompiler
{
    public class CLIArrayOption : ICLIOptionWithParameter
    {
        private readonly LinkedList<string>
            itemList = new LinkedList<string>();

        public CLIArrayOption(IEnumerable<string> keys)
        {
            Keys = keys;
        }

        public IEnumerable<string> Keys {
            get;
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
