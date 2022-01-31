using System;
using System.Globalization;
using System.Text.RegularExpressions;

namespace ChileComprasProxy.Framework.Utilities.ExtensionMethod
{
    public static class StringEx
    {
        public static string ValidNumericStrig = "[^a-zA-Z0-9 ]";
        public static string ValidCharactersStrig = "[^a-zA-Z0-9 ]";

        public static DateTime? ToDateTime(this string text, string format)
        {
            if (DateTime.TryParseExact(text, format, null, DateTimeStyles.None, out var result))
                return result;
            return null;
        }

        public static string CleanInvalidNumericChars(this string text)
        {
            return Regex.Replace(text, ValidNumericStrig, string.Empty);
        }


        public static object ToSystemType(this string field, Type type)
        {
            switch (type.FullName)
            {
                case "System.Boolean":
                    return Convert.ToBoolean(field);
                case "System.Double":
                    return Convert.ToDouble(field);
                case "System.DateTime":
                    return Convert.ToDateTime(field);
                case "System.Int32":
                    return Convert.ToInt32(field);
                case "System.Int64":
                    return Convert.ToInt64(field);
                default:
                    return field;
            }
        }
    }
}