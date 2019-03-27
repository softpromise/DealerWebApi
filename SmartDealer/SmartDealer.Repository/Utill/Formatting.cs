using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;

namespace SmartDealer.Repository.Utill
{
    public static class FormattingExtensions
    {
        public static string FormatBytes(ulong bytes)
        {
            var suffixes = new[] { "b", "kb", "mb", "gb", "tb", "pb", "eb", "zb", "yb" };

            var i = 0;
            double dblSByte = bytes;

            if (bytes <= 1024)
                return String.Format("{0:0.##}{1}", dblSByte, suffixes[i]);

            for (i = 0; (bytes / 1024) > 0; i++, bytes /= 1024)
                dblSByte = bytes / 1024.0;

            return String.Format("{0:0.##}{1}", dblSByte, suffixes[i]);
        }


        public static string Convertphonetomax10digits(this string phonestring)
        {
            string retVal = "";
            int indexlocationofext = 0;
            string phonestringwithoutext = "";

            // example (800) 499-1914 ext 102
            // this functionn will then return 8004991914
            // if contains an extension will need to strip that out
            // maximum 10 digits

            if (phonestring.Contains("ext"))
            {
                indexlocationofext = phonestring.IndexOf("ext");
                phonestringwithoutext = phonestring.Substring(0, indexlocationofext);
                phonestring = phonestringwithoutext;
            }
            //retVal = phonestring.Replace("-", "").Replace(".", "").Replace("(", "").Replace(")", "").Replace(" ", "");
            retVal = string.Join(null, System.Text.RegularExpressions.Regex.Split(phonestring, "[^\\d]"));
            if (retVal.Length > 10)
            {
                retVal = retVal.Substring(0, 10);
            }
            return retVal;
        }


        #region Proper Case Formatting

        static readonly string[] mac_exceptions = {
                                                      "macajewski",
                                                      "mace",
                                                      "macer",
                                                      "machin",
                                                      "machison",
                                                      "macie",
                                                      "maciejewski",
                                                      "mack",
                                                      "macken",
                                                      "mackender",
                                                      "mackey",
                                                      "mackie",
                                                      "macklin",
                                                      "maclam",
                                                      "macy"
                                                  };

        public static string FormatProper(this string source)
        {
            string ret = source.Trim().ToLower();
            if (Array.IndexOf(mac_exceptions, ret) != -1)
            {
                return "M" + ret.Substring(1);
            }
            else
            {
                if (ret.SubstringLoose(0, 2) == "mc")
                    return "Mc" + FormatCapitalized(ret.SubstringLoose(2));
                else if (ret.SubstringLoose(0, 3) == "mac")
                    return "Mac" + FormatCapitalized(ret.SubstringLoose(3));
                else
                    return FormatCapitalized(ret);
            }
        }

        public static string FormatCapitalized(string s)
        {
            string ret = "";
            if (!string.IsNullOrEmpty(s))
            {
                ret = s.Substring(0, 1).ToUpper() + s.SubstringLoose(1).ToLower();
            }
            return ret;
        }

        #endregion


        public static string FormatPhone(this string s)
        {
            return s != null ? Regex.Replace(s, @"(\d{3})(\d{3})(\d{4})", "$1-$2-$3") : string.Empty;
        }
    }


    public static class TimeSpanFormattingExtensions
    {
        public static string ToReadableString(this TimeSpan? span)
        {
            if (span == null)
                return "?";

            return string.Join(" ", ((TimeSpan)span).GetReadableStringElements()
               .Where(str => !string.IsNullOrWhiteSpace(str)));
        }

        private static IEnumerable<string> GetReadableStringElements(this TimeSpan span)
        {
            yield return GetDaysString((int)Math.Floor(span.TotalDays));
            yield return GetHoursString(span.Hours);
            yield return GetMinutesString(span.Minutes);
            yield return GetSecondsString(span.Seconds);
        }

        private static string GetDaysString(int days)
        {
            if (days == 0)
                return string.Empty;

            if (days == 1)
                return "1 day";

            return string.Format("{0:0}d", days);
        }

        private static string GetHoursString(int hours)
        {
            if (hours == 0)
                return string.Empty;

            if (hours == 1)
                return "1 hour";

            return string.Format("{0:0}h", hours);
        }

        private static string GetMinutesString(int minutes)
        {
            if (minutes == 0)
                return string.Empty;

            if (minutes == 1)
                return "1 minute";

            return string.Format("{0:0}m", minutes);
        }

        private static string GetSecondsString(int seconds)
        {
            return string.Empty;
        }
    }
}
