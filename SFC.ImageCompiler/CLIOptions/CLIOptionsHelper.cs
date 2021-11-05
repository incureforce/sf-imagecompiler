using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;

namespace SFC.ImageCompiler
{
    public class CLIOptionsHelper
    {
        public static CLIOptionKeyGeneration KeyGeneration {
            get; set;
        } = CLIOptionKeyGeneration.Detect;

        public static IEnumerable<string> GetKeys(string completeKey, string shortKey = null, bool withDefault = false)
        {
            if (string.IsNullOrEmpty(completeKey)) {
                throw new ArgumentException("Can not be null or empty", nameof(completeKey));
            }

            if (char.IsUpper(completeKey.FirstOrDefault()) is false) {
                throw new ArgumentException("Can only start with upper characters", nameof(completeKey));
            }

            var stringBuilder = new StringBuilder();

            if (shortKey is null) {
                foreach (var completeKeyChar in completeKey) {
                    if (char.IsUpper(completeKeyChar)) {
                        stringBuilder.Append(completeKeyChar);
                    }
                }

                shortKey = stringBuilder.ToString();

                stringBuilder.Clear();
            }

            var generation = KeyGeneration;
            if (generation == CLIOptionKeyGeneration.Detect) {
                generation = RuntimeInformation.IsOSPlatform(OSPlatform.Windows)
                    ? CLIOptionKeyGeneration.Windows
                    : CLIOptionKeyGeneration.Unix;
            }

            if (generation.HasFlag(CLIOptionKeyGeneration.Windows)) {
                stringBuilder.Append("/");
                stringBuilder.Append(completeKey);

                yield return stringBuilder.ToString();

                stringBuilder.Clear();
            }

            if (generation.HasFlag(CLIOptionKeyGeneration.Unix)) {
                stringBuilder.Append("-");

                foreach (var completeKeyChar in completeKey) {
                    if (char.IsUpper(completeKeyChar)) {
                        stringBuilder.Append("-");
                        stringBuilder.Append(char.ToLower(completeKeyChar));
                    }
                    else {
                        stringBuilder.Append(completeKeyChar);
                    }
                }

                yield return stringBuilder.ToString();

                stringBuilder.Clear();

                stringBuilder.Append("-");
                stringBuilder.Append(shortKey.ToLower());

                yield return stringBuilder.ToString();

                stringBuilder.Clear();
            }

            if (withDefault) {
                yield return null;
            }
        }
    }
}
