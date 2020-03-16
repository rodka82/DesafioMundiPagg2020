using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MundiPagg.Payment.Application.Extensions
{
    public static class DictionaryExtensions
    {
        public static bool isPossibleGetKey(this Dictionary<string, string> dic, string key)
        {
            return dic.Any(k => k.Key.RemoveBrackets() == key);
        }

        public static bool CanBeMappedTo(this Dictionary<string, string> dic, Dictionary<string, string> map)
        {
            var isPossibleGetCustomer = true;
            foreach (var item in map)
            {
                var key = item.Key;
                var value = item.Value;

                isPossibleGetCustomer = dic.isPossibleGetKey(key);
                if (!isPossibleGetCustomer)
                    break;
            }

            return isPossibleGetCustomer;
        }
    }

}
