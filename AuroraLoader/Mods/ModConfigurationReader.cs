using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AuroraLoader.Mods
{
    public static class ModConfigurationReader
    {
        public static Dictionary<string, string> FromString(string str)
        {
            var result = new Dictionary<string, string>();

            var lines = str.Split(new[] { "\n", "\r" }, StringSplitOptions.RemoveEmptyEntries)
                .Select(l => l.Trim())
                .Where(l => l.Length > 0)
                .Where(l => !l.StartsWith(";"))
                .ToList();

            foreach (var line in lines)
            {
                if (!line.Contains("="))
                {
                    throw new Exception("Invalid config line: " + line);
                }

                var index = line.IndexOf('=');
                var name = line.Substring(0, index);
                var val = line.Substring(index + 1);

                result.Add(name, val);
            }

            return result;
        }

        public static string ToString(Dictionary<string, string> values)
        {
            var sb = new StringBuilder();

            foreach (var kvp in values)
            {
                sb.AppendLine(kvp.Key + "=" + kvp.Value);
            }

            return sb.ToString();
        }
    }
}
