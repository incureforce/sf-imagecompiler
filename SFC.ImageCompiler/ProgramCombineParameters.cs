using System;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Text.Json;

namespace SFC.ImageCompiler
{
    public class ProgramCombineParameters
    {
        static readonly ImageExtension[] extenions = {
            new ImageExtension { Format = ImageFormat.Bmp, Name = "bmp" },
            new ImageExtension { Format = ImageFormat.Png, Name = "png" },
        };

        public int Size {
            get; set;
        } = 0;

        public string Name {
            get; set;
        }

        public string Source {
            get; set;
        }

        public string Target {
            get; set;
        }

        public string Filter {
            get; set;
        }

        public ImageExtension Extension {
            get; set;
        }

        public ProgramCombineCSSParameters CSS {
            get; set;
        }
        internal ProgramCombineJSONParameters JSON { get; set; }

        internal static ImageExtension FindByExtension(string text)
        {
            foreach (var e in extenions) {
                if (string.Equals(e.Name, text, StringComparison.OrdinalIgnoreCase)) {
                    return e;
                }
            }

            throw new NotSupportedException($"Extension not found: {text}");
        }

        public void PrintInfo()
        {
            Console.WriteLine($"Source: {Source}");
            Console.WriteLine($"Target: {Target}");
            Console.WriteLine($"Filter: {Filter}");
            Console.WriteLine($"Output: {Extension.Name}");
            Console.WriteLine($"Name: {Name}");
            Console.WriteLine($"Size: {Size}");
        }

        public void Run()
        {
            var output = $"{Name}.{Extension.Name}";
            var files = Directory.GetFiles(Source, Filter)
                .Where(x => Path.GetFileName(x) != output)
                .ToList();
            var w = Size;
            var h = Size;
            var count = files.Count;

            if (Size == 0) {
                Console.WriteLine($"Scan images ...");

                foreach (var file in files) {
                    using var source = new Bitmap(file);

                    if (w < source.Width) {
                        w = source.Width;
                    }

                    if (h < source.Height) {
                        h = source.Height;
                    }
                }

                Console.WriteLine($"Scan images ... complete");
            }
            else {
                Console.WriteLine($"Scan images ... skipped");
            }

            var stride = (int)Math.Ceiling(Math.Sqrt(count));
            var doc = new ImageDescriptionDocument() {
                Dimensions = new ImageDimensions {
                    W = w,
                    H = h,
                },
            };

            Console.WriteLine($"Image size: {stride * w}x{stride * h} (pixels)");

            using var target = new Bitmap(stride * w, stride * h);
            using (var graphics = Graphics.FromImage(target)) {
                Console.WriteLine($"Merge images ...");

                graphics.CompositingQuality = CompositingQuality.HighQuality;
                graphics.InterpolationMode = InterpolationMode.HighQualityBicubic;

                var index = 0;

                foreach (var file in files) {
                    Console.WriteLine($"Merge image '{file}'");

                    using var source = new Bitmap(file);

                    var x = (index % stride) * w;
                    var y = (index / stride) * h;

                    graphics.DrawImage(source, new Rectangle {
                        Y = y,
                        X = x,
                        Width = w,
                        Height = h,
                    });

                    var fileName = Path.GetFileName(file);

                    doc.AddImage(fileName, x, y);

                    ++index;
                }

                graphics.Save();

                Console.WriteLine($"Merge images ... complete");
            }

            Console.WriteLine("Export image ...");

            using (var stream = File.Create($"{Path.Combine(Target, Name)}.{Extension.Name}")) {
                target.Save(stream, Extension.Format);
            }

            Console.WriteLine("Export image ... complete");

            if (CSS is not null) {
                ExportCSS(doc, output);
            }

            if (JSON is not null) {
                ExportJSON(doc);
            }
        }

        private void ExportJSON(ImageDescriptionDocument doc)
        {
            Console.WriteLine("Export json");

            using (var stream = File.Create($"{Path.Combine(Target, Name)}.json")) {
                JsonSerializer.SerializeAsync(stream, doc);
            }

            Console.WriteLine("Export json ... complete");
        }

        private void ExportCSS(ImageDescriptionDocument doc, string output)
        {
            Console.WriteLine("Export css");

            var dimensions = doc.Dimensions;

            using (var stream = File.Create($"{Path.Combine(Target, Name)}.css")) {
                var streamWriter = new StreamWriter(stream);
                var indentWriter = new IndentWriter(streamWriter);

                indentWriter.WriteLine($".{CSS.BaseClass} {{");
                indentWriter.WriteLine($"    background-image: url({CSS.PathPrefix}{output});");
                indentWriter.WriteLine($"    background-color: transparent;");
                //indentWriter.WriteLine($"    background-repeat: no-repeat;");
                indentWriter.WriteLine($"    width: {dimensions.W}px;");
                indentWriter.WriteLine($"    height: {dimensions.H}px;");
                indentWriter.WriteLine($"}}");

                foreach (var e in doc.Descriptions) {
                    indentWriter.WriteLine($"");
                    indentWriter.WriteLine($".{CSS.BaseClass}.{Path.GetFileNameWithoutExtension(e.Name)} {{");
                    //indentWriter.WriteLine($"    background-size: {w}px {h}px;");
                    indentWriter.WriteLine($"    background-position: -{e.X}px -{e.Y}px;");
                    indentWriter.WriteLine($"}}");
                }

                streamWriter.Flush();
            }

            Console.WriteLine("Export css ... complete");
        }
    }
}
