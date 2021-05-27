using System;
using System.Linq;

namespace TwoNEL.API.Extensions
{
    public static class StringExtensions
    {
        public static string ToSnakeCase(this string str)
        {
            if (!(String.IsNullOrEmpty(str)))
            {
                return string.Concat(
                    str.Select((x, i) => i > 0 &&
                    char.IsUpper(x) ? "_" + x.ToString() :
                    x.ToString())).ToLower();
            }
            else return "xd";
        }
    }
}
