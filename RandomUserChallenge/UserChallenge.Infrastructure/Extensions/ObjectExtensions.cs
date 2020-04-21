using System;
using System.Collections.Generic;
using System.Linq;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;

namespace VMS.Infrastructure.Extensions
{
    public static class ObjectExtensions
    {
        public static string ToJson(this object obj, bool camelCase = true)
        {
            var settings = camelCase
                ? new JsonSerializerSettings
                {
                    ContractResolver = new CamelCasePropertyNamesContractResolver()
                }
                : null;

            return JsonConvert.SerializeObject(
                obj,
                Formatting.Indented,
                settings);
        }

        public static IEnumerable<T> DistinctBy<T, TKey>(this IEnumerable<T> items, Func<T, TKey> property)
        {
            return items.GroupBy(property).Select(x => x.First());
        }
    }
}
