using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace SmartDealer.WebAPI.Extensions
{
    public static class StringExtensions
    {
        public static string Capitalize(this String s)
        {
            if (!String.IsNullOrEmpty(s))
            {
                return char.ToUpper(s[0]) + s.Substring(1).ToLower();
            }
            return s;
        }

        public static GroupCollection GrabAll(this String s, string pattern, bool ignoreCase = true)
        {
            Match match = (ignoreCase) ? Regex.Match(s, pattern, RegexOptions.IgnoreCase) : Regex.Match(s, pattern);
            return match.Groups;
        }

        public static string GrabFirst(this String s, string pattern, bool ignoreCase = true)
        {
            Match match = (ignoreCase) ? Regex.Match(s, pattern, RegexOptions.IgnoreCase) : Regex.Match(s, pattern);

            if (match.Success)
            {
                return match.Groups[1].Value;
            }

            return null;
        }

        public static bool Matches(this String s, string pattern, bool ignoreCase = true)
        {
            if (ignoreCase)
            {
                return ((Regex.IsMatch(s, pattern, RegexOptions.IgnoreCase))) ? true : false;
            }
            else
            {
                return (Regex.IsMatch(s, pattern)) ? true : false;
            }
        }

        /// Like linq take - takes the first x characters
        public static string Take(this string theString, int count, bool ellipsis = false)
        {
            int lengthToTake = Math.Min(count, theString.Length);
            var cutDownString = theString.Substring(0, lengthToTake);

            if (ellipsis && lengthToTake < theString.Length)
                cutDownString += "...";

            return cutDownString;
        }

        //like linq skip - skips the first x characters and returns the remaining string
        public static string Skip(this string theString, int count)
        {
            int startIndex = Math.Min(count, theString.Length);
            int remainingLength = theString.Length - startIndex;

            var cutDownString = theString.Substring(startIndex - 1, remainingLength);

            return cutDownString;
        }

        //reverses the string... pretty obvious really
        public static string Reverse(this string input)
        {
            char[] chars = input.ToCharArray();
            Array.Reverse(chars);
            return new String(chars);
        }

        // "a string".IsNullOrEmpty() beats string.IsNullOrEmpty("a string")
        public static bool IsNullOrEmpty(this string theString)
        {
            return string.IsNullOrEmpty(theString);
        }

        //not so sure about this one -
        //"a string {0}".Format("blah") vs string.Format("a string {0}", "blah")
        public static string Format(this string format, params object[] args)
        {
            return string.Format(format, args);
        }

        public static bool Match(this string value, string pattern)
        {
            return Regex.IsMatch(value, pattern);
        }

        //splits string into array with chunks of given size. not really that useful..
        public static string[] SplitIntoChunks(this string toSplit, int chunkSize)
        {
            if (string.IsNullOrEmpty(toSplit))
                return new string[] { "" };

            int stringLength = toSplit.Length;

            int chunksRequired = (int)Math.Ceiling((decimal)stringLength / (decimal)chunkSize);
            var stringArray = new string[chunksRequired];

            int lengthRemaining = stringLength;

            for (int i = 0; i < chunksRequired; i++)
            {
                int lengthToUse = Math.Min(lengthRemaining, chunkSize);
                int startIndex = chunkSize * i;
                stringArray[i] = toSplit.Substring(startIndex, lengthToUse);

                lengthRemaining = lengthRemaining - lengthToUse;
            }

            return stringArray;
        }

        public static string FirstCharToUpper(this string input)
        {
            if (String.IsNullOrEmpty(input))
            {
                throw new ArgumentException("ArgumentException");
            }
            return input.First().ToString().ToUpper() + input.Substring(1);
        }

        public static bool IsNumeric(this string theValue)
        {
            long retNum;
            return long.TryParse(theValue, System.Globalization.NumberStyles.Integer, System.Globalization.NumberFormatInfo.InvariantInfo, out retNum);
        }

        /// <summary>
        /// Converts a string into a "SecureString"
        /// </summary>
        /// <param name="str">Input String</param>
        /// <returns></returns>
        public static System.Security.SecureString ToSecureString(this String str)
        {
            System.Security.SecureString secureString = new System.Security.SecureString();
            foreach (Char c in str)
                secureString.AppendChar(c);

            return secureString;
        }


        public static bool IsValidEmailAddress(this string s)
        {
            Regex regex = new Regex(@"^[\w-\.]+@([\w-]+\.)+[\w-]{2,4}$");
            return regex.IsMatch(s);
        }

        public static string ToCamelCase(this string the_string)
        {
            if (the_string == null || the_string.Length < 2)
                return the_string;

            string[] words = the_string.Split(
                new char[] { },
                StringSplitOptions.RemoveEmptyEntries);

            string result = words[0].ToLower();
            for (int i = 1; i < words.Length; i++)
            {
                result +=
                    words[i].Substring(0, 1).ToUpper() +
                    words[i].Substring(1);
            }

            return result;
        }
    }
}
