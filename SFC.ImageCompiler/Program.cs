using System;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text.Json.Serialization;

namespace SFC.ImageCompiler
{
    class ProgramOptionDescriptions
    {
        public string Name {
            get; set;
        }

        public string Description {
            get; set;
        }

        public ICLIOption Option {
            get; set;
        }
    }

    class Program
    {
        static void Main(string[] args)
        {
            var debug = Debugger.IsLogging();

            try {
                Execute(args);
            }
            catch (Exception ex) when (debug == false) {
                Console.Error
                    .WriteLine(ex.Message);
            }

            if (debug) {
                Console.WriteLine();
                Console.Write("Press any key to exit ...");
                Console.ReadKey(true);
            }
        }

        private static void Execute(string[] args)
        {
            var options = new CLIOptions();

            CLIOptionsHelper.KeyGeneration = CLIOptionKeyGeneration.Everything;

            var help = options.AddActionOption(CLIOptionsHelper.GetKeys("Help"));
            var size = options.AddNumberOption(CLIOptionsHelper.GetKeys("Size", "SZ"));
            var name = options.AddStringOption(CLIOptionsHelper.GetKeys("Name"));
            var filter = options.AddStringOption(CLIOptionsHelper.GetKeys("Filter"));
            var target = options.AddStringOption(CLIOptionsHelper.GetKeys("Target"));
            var output = options.AddStringOption(CLIOptionsHelper.GetKeys("Output"));
            var source = options.AddStringOption(CLIOptionsHelper.GetKeys("Source", withDefault: true));
            var cssBaseClass = options.AddStringOption(CLIOptionsHelper.GetKeys("CSSBaseClass", "CSSBC"));
            var cssPathPrefix = options.AddStringOption(CLIOptionsHelper.GetKeys("CSSPathSuffix", "CSSPS"));

            filter.Text = "*.png";

            options.Parse(args);

            if (help.Invoke || args.Any() is false) {
                var descriptions = new ProgramOptionDescriptions[] {
                    new ProgramOptionDescriptions {
                        Name = "Help",
                        Description = "Display help",

                        Option = help,
                    },
                    new ProgramOptionDescriptions {
                        Name = "Source",
                        Description = "Set Source directory for images",

                        Option = source,
                    },
                    new ProgramOptionDescriptions {
                        Name = "Target",
                        Description = "Set Target directory for images",

                        Option = target,
                    },
                    new ProgramOptionDescriptions {
                        Name = "Filter",
                        Description = "Set Source directory extension filter",

                        Option = filter,
                    },
                    new ProgramOptionDescriptions {
                        Name = "Output",
                        Description = "Set Target image output format",

                        Option = output,
                    },
                    new ProgramOptionDescriptions {
                        Name = "Name",
                        Description = "Project Name (example.png, example.png.json)",

                        Option = name,
                    },
                    new ProgramOptionDescriptions {
                        Name = "Size",
                        Description = "Set Size for images",

                        Option = size,
                    },
                    new ProgramOptionDescriptions {
                        Name = "CSS base-class",
                        Description = "Sets the base class for the css output",

                        Option = cssBaseClass,
                    },
                    new ProgramOptionDescriptions {
                        Name = "CSS path prefix",
                        Description = "Sets the path prefix for the css output",

                        Option = cssPathPrefix,
                    }
                };

                RunHelp(descriptions);
                return;
            }

            if (string.IsNullOrEmpty(source.Text)) {
                throw new InvalidOperationException("Source is not defined");
            }

            var parameters = new ProgramCombineParameters();

            parameters.Source = Path.GetFullPath(source.Text);
            parameters.Target = Path.GetFullPath(target.Text ?? parameters.Source);

            if (Directory.Exists(parameters.Source) == false) {
                throw new FileNotFoundException(parameters.Source);
            }

            parameters.Name = name.Text ?? Path.GetFileName(parameters.Source);
            parameters.Size = size.Number;
            parameters.Filter = filter.Text;
            parameters.Extension = output.Text is null
                    ? ProgramCombineParameters.FindByExtension("png")
                    : ProgramCombineParameters.FindByExtension(output.Text);

            if (string.IsNullOrEmpty(cssBaseClass.Text) is false) {
                parameters.CSS = new ProgramCombineCSSParameters {
                    BaseClass = cssBaseClass.Text,
                    PathPrefix = cssPathPrefix.Text,
                };
            }

            parameters.Run();
            //parameters.PrintInfo();
        }

        private static void RunHelp(ProgramOptionDescriptions[] descriptions)
        {
            Console.WriteLine("ImageCompiler: Imagefolder to spritesheet + json");
            Console.WriteLine("");

            foreach (var description in descriptions) {

                var option = description.Option;
                var optionKeys = option.Keys
                    .Select(x => x is null ? "(default)" : x);

                var synopsis = string.Join(", ", optionKeys);

                Console.WriteLine("  " + description.Name);
                Console.WriteLine("    " + synopsis);
                Console.WriteLine("    " + description.Description);
                Console.WriteLine();
            }
        }
    }
}
