using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;

namespace MundiPagg.Payment.Application.Extensions
{
    public static class StringExtensions
    {
        public static bool HasSameKeys(this string seq1, string seq2)
        {
            return seq1.GetAllKeys().SequenceEqual(seq1.GetAllKeys());
        }

        public static string Sanitize(this string s)
        {
            return Regex.Replace(s, @"[^\w\.@-]", "");
        }

        public static string RemoveBetween(this string s, char begin, char end)
        {
            Regex regex = new Regex(string.Format("\\{0}.*?\\{1}", begin, end));
            return regex.Replace(s, string.Empty);
        }

        public static string RemoveBrackets(this string s)
        {
            return s.RemoveBetween('[', ']');
        }

        public static List<string> GetAllKeys(this string json)
        {
            var regex = new Regex(@"\[\d*\].", RegexOptions.Compiled);
            return JObject.Parse(json).DescendantsAndSelf()
                           .OfType<JProperty>()
                           .Where(jp => jp.Value is JValue)
                           .Select(jp => regex.Replace(jp.Path, "."))
                           .Distinct()
                           .OrderBy(x => x)
                           .ToList();
        }

        public static Dictionary<string, string> GetKeyAndValuesToDictionary(this string json)
        {
            var regex = new Regex(@"\[\d*\].", RegexOptions.Compiled);
            return JObject.Parse(json).DescendantsAndSelf()
                           .OfType<JProperty>()
                           .Where(jp => jp.Value is JValue)
                           .Select(jp => new KeyValuePair<string, string>(jp.Path, (string)jp.Value)
                           ).ToDictionary(pair => pair.Key, pair => pair.Value);
        }
    }
}
