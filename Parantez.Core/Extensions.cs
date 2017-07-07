using System;
using System.Globalization;
using System.Text.RegularExpressions;

namespace Parantez.Core
{
    public static class Extensions
    {
        public static string FirstLetterToLowerCase(this string text)
        {
            if (String.IsNullOrWhiteSpace(text))
                return String.Empty;

            return
                Char.ToLowerInvariant(text[0]) + text.Substring(1);
        }

        public static string Limit(this string text, int limit)
        {
            var result = String.Empty;
            if (text.Length > limit)
                result = $"{text.Substring(0, limit)}...";
            else
                result = text;

            return Regex.Replace(($"{result}".Replace("\n", " ").Replace("\r", " ")), @"\s+", " ");
        }

        public static string SpaceControl(this string text)
        {
            if (String.IsNullOrWhiteSpace(text))
                return "n/a";
            else
                return text;
        }

        public static string ConvertDbFormat(this DateTime dateTime)
        {
            string date = dateTime.ToString("yyyy-MM-ddTHH:mm:ss.fff", CultureInfo.InvariantCulture);
            return date;
        }

        public static string NumberFormat(this int value)
        {
            try { return value.ToString("#,##0"); }
            catch { return value.ToString(); }
        }

        public static string NumberFormatLong(this long value)
        {
            try { return value.ToString("#,##0"); }
            catch { return value.ToString(); }
        }

        public static string MoneyFormat(this decimal value)
        {
            try { return value.ToString("C"); }
            catch { return value.ToString(); }
        }
    }
}
