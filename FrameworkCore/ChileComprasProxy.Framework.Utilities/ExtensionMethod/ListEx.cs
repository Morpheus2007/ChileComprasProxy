using System.Collections.Generic;
using System.Linq;

namespace ChileComprasProxy.Framework.Utilities.ExtensionMethod
{
    public static class ListEx
    {
        public static string Join(this List<int> me)
        {
            return me.Join(",");
        }

        public static string Join(this IList<int> me)
        {
            return me.Join(",");
        }

        public static string Join(this List<int> me, string delimiter)
        {
            return string.Join(delimiter, me.Select(e => e.ToString()).ToArray());
        }

        public static string Join(this IList<int> me, string delimiter)
        {
            return string.Join(delimiter, me.Select(e => e.ToString()).ToArray());
        }

        //long
        public static string Join(this List<long> me)
        {
            return me.Join(",");
        }

        public static string Join(this IList<long> me)
        {
            return me.Join(",");
        }

        public static string Join(this List<long> me, string delimiter)
        {
            return string.Join(delimiter, me.Select(e => e.ToString()).ToArray());
        }

        public static string Join(this IList<long> me, string delimiter)
        {
            return string.Join(delimiter, me.Select(e => e.ToString()).ToArray());
        }
    }
}