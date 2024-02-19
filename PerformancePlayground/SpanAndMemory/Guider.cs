using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PerformancePlayground.SpanAndMemory
{
    public static class Guider
    {
        public static string ToStringFromGuidBefore(Guid id)
        {
            return Convert.ToBase64String(id.ToByteArray())
                .Replace("/", "-")
                .Replace("+", "_")
                .Replace("=", string.Empty);
        }


        public static Guid ToGuidFromStringBefore(string id)
        {
            var result = Convert.FromBase64String(id.Replace("-", "/")
                .Replace("_", "+")
                + "==");

            return new Guid(result);
        }
    }
}
