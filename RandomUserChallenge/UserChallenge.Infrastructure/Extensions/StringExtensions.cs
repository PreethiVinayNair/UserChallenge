using System.Collections.Generic;
using System.IO;
using System.Text.RegularExpressions;

namespace VMS.Infrastructure.Extensions
{
    public static class StringExtensions
    {
        public static IEnumerable<string> ToLines(this string s)
        {
            if (s == null)
            {
                yield break;
            }

            using (var reader = new StringReader(s))
            {
                string line;
                while ((line = reader.ReadLine()) != null)
                {
                    yield return line;
                }
            }
        }

        public static string BreakToWords(this string text)
        {
            text = text ?? string.Empty;
            text = Regex.Replace(text, @"[^ a-zA-Z0-9_-]", string.Empty);
            text = Regex.Replace(text, @"((?<=\p{Ll})\p{Lu})|((?!\A)\p{Lu}(?>\p{Ll}))", " $0");

            return text;
        }
    }
}
