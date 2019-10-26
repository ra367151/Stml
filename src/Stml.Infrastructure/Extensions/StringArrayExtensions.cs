using System;
using System.Collections.Generic;
using System.Text;

namespace Stml.Infrastructure.Extensions
{
    public static class StringArrayExtensions
    {
        public static string Join(this string[] arr, string separator)
        {
            return string.Join(separator, arr);
        }
    }
}
