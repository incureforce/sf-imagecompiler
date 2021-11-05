using System.Collections.Generic;

namespace SFC.ImageCompiler
{
    public static class CLIOptionsExtension
    {
        public static CLIStringOption AddArrayOption(this CLIOptions self, params string[] args)
        {
            return AddArrayOption(self, args);
        }

        public static CLIArrayOption AddArrayOption(this CLIOptions self, IEnumerable<string> args)
        {
            var option = new CLIArrayOption(args);

            self.AddOption(option);

            return option;
        }

        public static CLIStringOption AddSwitchOption(this CLIOptions self, params string[] args)
        {
            return AddSwitchOption(self, args);
        }

        public static CLISwitchOption AddSwitchOption(this CLIOptions self, IEnumerable<string> args)
        {
            var option = new CLISwitchOption(args);

            self.AddOption(option);

            return option;
        }

        public static CLIStringOption AddStringOption(this CLIOptions self, params string[] args)
        {
            return AddStringOption(self, args);
        }

        public static CLIStringOption AddStringOption(this CLIOptions self, IEnumerable<string> args)
        {
            var option = new CLIStringOption(args);

            self.AddOption(option);

            return option;
        }

        public static CLINumberOption AddNumberOption(this CLIOptions self, params string[] args)
        {
            return AddNumberOption(self, args);
        }

        public static CLINumberOption AddNumberOption(this CLIOptions self, IEnumerable<string> args)
        {
            var option = new CLINumberOption(args);

            self.AddOption(option);

            return option;
        }

        public static CLIActionOption AddActionOption(this CLIOptions self, params string[] args)
        {
            return AddActionOption(self, args);
        }

        public static CLIActionOption AddActionOption(this CLIOptions self, IEnumerable<string> args)
        {
            var option = new CLIActionOption(args);

            self.AddOption(option);

            return option;
        }
    }
}
