using System.Collections.Generic;

namespace SFC.ImageCompiler
{
    public class CLISwitchOption : ICLIOptionWithSet, ICLIOptionWithParameter
    {
        public CLISwitchOption(IEnumerable<string> keys)
        {
            Keys = keys;
        }

        public IEnumerable<string> Keys {
            get;
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
