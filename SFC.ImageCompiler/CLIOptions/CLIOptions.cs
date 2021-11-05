using System;
using System.Collections.Generic;
using System.Linq;

namespace SFC.ImageCompiler
{
    public class CLIOptions
    {
        static readonly string[] Empty = { };

        private readonly List<ICLIOption>
            optionList = new List<ICLIOption>();

        public CLIOptions()
        {
        }

        public IReadOnlyList<ICLIOption> Options {
            get => optionList;
        }

        public void AddOption(ICLIOption option)
        {
            if (option is null) {
                throw new ArgumentNullException(nameof(option));
            }

            optionList.Add(option);
        }

        public IEnumerable<string> Parse(IEnumerable<string> args)
        {
            var map = new Dictionary<string, ICLIOption>();
            var defaultOption = default(ICLIOption);

            foreach (var option in optionList) {
                foreach (var key in option.Keys) {
                    if (key == default) {
                        if (defaultOption != null) {
                            throw new CLIParseException("Duplicate default option");
                        }

                        defaultOption = option;
                    }
                    else {
                        map.Add(key, option);
                    }
                }
            }

            var queue = new Queue<string>(args);

            while (queue.Count > 0) {
                var key = queue.Dequeue();

                if (key == "--") {
                    return queue.ToList();
                }

                if (map.TryGetValue(key, out var option) is false) {
                    if (defaultOption is null) {
                        throw new CLIOptionMissingException(key);
                    }

                    if (defaultOption is ICLIOptionWithParameter optionWithParameter) {
                        if (optionWithParameter.TryParse(key)) {
                            continue;
                        }
                    }

                    throw new CLIOptionParseException(defaultOption, key);
                }

                if (queue.TryPeek(out var arg) is false) {
                    if (option is ICLIOptionWithSet optionWithSet) {
                        if (optionWithSet.TrySet()) {
                            continue;
                        }

                        throw new CLIOptionSetException(option);
                    }

                    throw new CLIOptionSetException(option);
                }

                if (map.TryGetValue(arg, out _) is false) {
                    if (option is ICLIOptionWithParameter optionWithParameter) {
                        queue.Dequeue();

                        if (optionWithParameter.TryParse(arg)) {
                            continue;
                        }

                        throw new CLIOptionParseException(option, arg);
                    }

                    if (option is ICLIOptionWithSet optionWithSet) {
                        if (optionWithSet.TrySet()) {
                            continue;
                        }

                        throw new CLIOptionSetException(option);
                    }

                    throw new CLIOptionParseException(option, arg);
                }
                else {
                    if (option is ICLIOptionWithSet optionWithSet) {
                        if (optionWithSet.TrySet()) {
                            continue;
                        }

                        throw new CLIOptionSetException(option);
                    }
                }
            }

            return Empty;
        }
    }
}
