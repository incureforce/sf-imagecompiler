using System;
using System.IO;

namespace SFC.ImageCompiler
{
    public class IndentWriter
    {
        public IndentWriter(TextWriter writer)
        {
            Writer = writer;
        }

        public int Level {
            get; set;
        }

        public TextWriter Writer {
            get;
        }

        public void Indent()
        {
            Level++;
        }

        public void Dedent()
        {
            Level--;
        }

        public IDisposable UseIndent()
        {
            Indent();

            var disposer = new Disposable();

            disposer.Callback += delegate {
                Dedent();
            };

            return disposer;
        }

        public void WriteLine(string text)
        {
            Writer.Write(new string(' ', Level * 2));
            Writer.Write(text);
            Writer.WriteLine();
        }
    }
}
