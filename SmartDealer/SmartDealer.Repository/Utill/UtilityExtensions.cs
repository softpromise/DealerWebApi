using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.IO.Compression;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Text.RegularExpressions;
using System.Xml;
using System.Xml.Serialization;

namespace SmartDealer.Repository.Utill
{
    public static class UtilityExtensions
    {
        #region Searching

        /// <summary>
        /// Determine whether a given string contains a specified substring according to the specified comparison type.
        /// </summary>
        /// <param name="s">String to search.</param>
        /// <param name="searchText">Substring to search for.</param>
        /// <param name="comp">Type of comparison to perform.</param>
        /// <returns>True if the search string is contained in the source string according to the specified comparison 
        /// type, otherwise false.</returns>
        public static bool Contains(this string s, string searchText, StringComparison comp)
        {
            return s.IndexOf(searchText, comp) >= 0;
        }

        /// <summary>
        /// Determine the index of the nth occurence of one string within another string.
        /// </summary>
        /// <param name="s">String to search.</param>
        /// <param name="match">Substring to search for.</param>
        /// <param name="n">Occurence to find.</param>
        /// <returns>The index of the nth occurence.</returns>
        public static int IndexOfOccurence(this string s, string match, int n)
        {
            int i = 1;
            int index = 0;

            while (i <= n && (index = s.IndexOf(match, index + 1)) != -1)
            {
                if (i == n)
                    return index;

                i++;
            }

            return -1;
        }

        #endregion

        #region Hashing

        /// <summary>
        /// Applies one-way hash to specified string.
        /// </summary>
        /// <param name="source">String to hash.</param>
        /// <returns>String containing hashed input.</returns>
        public static string Hash(this string source)
        {
            SHA1 sha = new SHA1CryptoServiceProvider();
            string hashedValue = string.Empty;

            byte[] hashedData = sha.ComputeHash(Encoding.Unicode.GetBytes(source));

            //loop through each byte in the byte array
            foreach (byte b in hashedData)
            {
                //convert each byte and append
                hashedValue += String.Format("{0,2:X2}", b);
            }

            //return the hashed value
            return hashedValue;
        }

        #endregion

        public static XmlElement GetXmlElement(this string xml)
        {
            XmlDocument doc = new XmlDocument();
            doc.LoadXml(xml);
            return doc.DocumentElement;
        }

        #region XmlSerialize XmlDeserialize
        /// <summary>Serialises an object of type T in to an xml string</summary>
        /// <typeparam name="T">Any class type</typeparam>
        /// <param name="objectToSerialise">Object to serialise</param>
        /// <returns>A string that represents Xml, empty oterwise</returns>
        public static string XmlSerialize<T>(this T objectToSerialise) where T : class
        {
            var serialiser = new XmlSerializer(typeof(T));
            string xml;
            using (var memStream = new MemoryStream())
            {
                using (var xmlWriter = new XmlTextWriter(memStream, Encoding.UTF8))
                {
                    serialiser.Serialize(xmlWriter, objectToSerialise);
                    xml = Encoding.UTF8.GetString(memStream.GetBuffer());
                }
            }

            // ascii 60 = '<' and ascii 62 = '>'
            xml = xml.Substring(xml.IndexOf(Convert.ToChar(60)));
            xml = xml.Substring(0, (xml.LastIndexOf(Convert.ToChar(62)) + 1));
            return xml;
        }

        /// <summary>Serialises an object of type T in to an xml string</summary>
        /// <typeparam name="T">Any class type</typeparam>
        /// <param name="objectToSerialise">Object to serialise</param>
        /// <param name="ns">Namespaces to use during serialization</param>
        /// <returns>A string that represents Xml, empty oterwise</returns>
        public static string XmlSerialize<T>(this T objectToSerialise, XmlSerializerNamespaces ns) where T : class
        {
            var serialiser = new XmlSerializer(typeof(T));
            string xml;
            using (var memStream = new MemoryStream())
            {
                using (var xmlWriter = new XmlTextWriter(memStream, Encoding.UTF8))
                {
                    serialiser.Serialize(xmlWriter, objectToSerialise, ns);
                    xml = Encoding.UTF8.GetString(memStream.GetBuffer());
                }
            }

            // ascii 60 = '<' and ascii 62 = '>'
            xml = xml.Substring(xml.IndexOf(Convert.ToChar(60)));
            xml = xml.Substring(0, (xml.LastIndexOf(Convert.ToChar(62)) + 1));
            return xml;
        }

        /// <summary>Deserialises an xml string into an object of Type T</summary>
        /// <typeparam name="T">Any class type</typeparam>
        /// <param name="xml">Xml as string to deserialise from</param>
        /// <returns>A new object of type T is successful, null if failed</returns>
        public static T XmlDeserialize<T>(this string xml) where T : class
        {
            var serialiser = new XmlSerializer(typeof(T));
            T newObject;

            using (var stringReader = new StringReader(xml.Replace("\r", "").Replace("\n", "")))
            {
                using (var xmlReader = new XmlTextReader(stringReader))
                {
                    try
                    {
                        newObject = serialiser.Deserialize(xmlReader) as T;
                    }
                    catch (InvalidOperationException) // String passed is not Xml, return null
                    {
                        return null;
                    }

                }
            }

            return newObject;
        }

        /// <summary>Deserialises an xml document into an object of Type T</summary>
        /// <typeparam name="T">Any class type</typeparam>
        /// <param name="xml">XmlDocument to deserialise from</param>
        /// <returns>A new object of type T is successful, null if failed</returns>
        public static T XmlDeserialize<T>(this XmlDocument xmlDocument) where T : class
        {
            return XmlDeserialize<T>(xmlDocument.OuterXml.ToString());
        }




        public static string XmlSerialize(this ArrayList items)
        {
            var listTypes = new ArrayList();

            foreach (var item in items)
            {
                if (listTypes.Contains(item.GetType()) == false)
                    listTypes.Add(item.GetType());
            }

            var types = (Type[])listTypes.ToArray(typeof(Type));
            var serializer = new XmlSerializer(typeof(ArrayList), types);

            using (var writer = new StringWriter())
            {
                serializer.Serialize(writer, items);
                return writer.ToString();
            }
        }
        #endregion

        #region Escaping

        /// <summary>
        /// Escape special XML characters: &amp;, &lt;, &gt;, &quot;, and &apos;
        /// </summary>
        /// <param name="s">Input string containing special characters.</param>
        /// <returns>Escaped version of input string.</returns>
        public static string EscapeXml(this string s)
        {
            string xml = s;
            if (!string.IsNullOrEmpty(xml))
            {
                // replace literal values with entities
                xml = xml.Replace("&", "&amp;");
                xml = xml.Replace("<", "&lt;");
                xml = xml.Replace(">", "&gt;");
                xml = xml.Replace("\"", "&quot;");
                xml = xml.Replace("'", "&apos;");
            }
            return xml;
        }

        /// <summary>
        /// Unescape special XML characters: &amp;, &lt;, &gt;, &quot;, and &apos;
        /// </summary>
        /// <param name="s">Input string containing escaped special characters.</param>
        /// <returns>Unescaped version of input string.</returns>
        public static string UnescapeXml(this string s)
        {
            string unxml = s;
            if (!string.IsNullOrEmpty(unxml))
            {
                // replace entities with literal values
                unxml = unxml.Replace("&apos;", "'");
                unxml = unxml.Replace("&quot;", "\"");
                unxml = unxml.Replace("&gt;", ">");
                unxml = unxml.Replace("&lt;", "<");
                unxml = unxml.Replace("&amp;", "&");
            }
            return unxml;
        }

        #endregion

        public static string SubstringLoose(this string source, int startIndex, int length)
        {
            if (source == null)
                return "";

            length = Math.Min(source.Length - startIndex, length);
            if (length <= 0)
                return "";

            return source.Substring(startIndex, length);
        }

        public static string SubstringLoose(this string source, int startIndex)
        {
            return SubstringLoose(source, startIndex, Math.Max(0, source.Length - startIndex));
        }

        public static string RightLoose(this string source, int length)
        {
            if (source == null)
                return "";

            return source.Right(Math.Min(source.Length, length));
        }

        public static string Right(this string source, int length)
        {
            return source.Substring(source.Length - length, length);
        }

        public static string AppendLine(this string source, string TextToAppend)
        {
            if (source.IsNullOrWhiteSpace())
            {
                source = TextToAppend;
            }
            else
            {
                source += System.Environment.NewLine + TextToAppend;
            }
            return source;
        }

        public static bool IsNumeric(this string s)
        {
            double myNum = 0;
            return (Double.TryParse(s, out myNum));
        }

        // use string.IsNullOrWhiteSpace when upgraded to 4.0
        public static bool IsNullOrWhiteSpace(this string s)
        {
            return string.IsNullOrWhiteSpace(s);
        }

        public static bool IsValidEmailAddress(this string emailAddress)
        {
            return Regex.IsMatch(emailAddress, @"\A(?:[a-z0-9!#$%&'*+/=?^_`{|}~-]+(?:\.[a-z0-9!#$%&'*+/=?^_`{|}~-]+)*@(?:[a-z0-9](?:[a-z0-9-]*[a-z0-9])?\.)+[a-z0-9](?:[a-z0-9-]*[a-z0-9])?)\Z", RegexOptions.IgnoreCase);
        }

        public static string ToOrdinalString<T>(this T value)
        {
            return value.ToString() + value.ToOrdinalStringPortion();
        }

        private static string ToOrdinalStringPortion<T>(this T value)
        {
            // todo: optimize
            string twoch = value.ToString().RightLoose(2);
            switch (value.ToString().RightLoose(1))
            {
                case "1":
                    return (twoch == "11" ? "th" : "st");
                case "2":
                    return (twoch == "12" ? "th" : "nd");
                case "3":
                    return (twoch == "13" ? "th" : "rd");
                default:
                    return "th";
            }
        }

        public static string ToBase26String(this long value)
        {
            var sb = new StringBuilder();

            do
            {
                value--;
                long remainder = 0;
                value = Math.DivRem(value, 26, out remainder);
                sb.Insert(0, Convert.ToChar('A' + remainder));

            } while (value > 0);

            return sb.ToString();
        }

        public static long ToLongFromBase26String(this string value)
        {
            if (string.IsNullOrWhiteSpace(value))
                throw new ArgumentNullException("value");

            char[] letters = value.ToUpperInvariant().ToCharArray().Reverse().ToArray(); //smallest column first

            if (letters.Where(c => !char.IsLetter(c)).Any())
                throw new ArgumentOutOfRangeException("value", "Encoded Number must only contain the letters A-Z");

            int[] numbers = letters.Select(c => (((int)c - 'A') + 1)).ToArray();

            long ret = 0;

            for (int i = 0; i < letters.Length; i++)
            {
                ret += (long)Math.Pow(26, i) * numbers[i];
            }

            return ret;
        }

        public static string ToTrimmedStringOfMaxLength(this string InputString, int MaxLength)
        {
            string ret = "";
            if (InputString != null && MaxLength > 0)
            {
                if (InputString.Trim().Length > MaxLength)
                {
                    ret = InputString.Trim().Remove(MaxLength);
                }
                else
                {
                    ret = InputString.Trim();
                }
            }
            return ret;
        }

        public static string ToTrimmedStringOfMaxLengthFromRight(this string InputString, int MaxLength)
        {
            string ret = "";
            if (InputString != null && MaxLength > 0)
            {
                if (InputString.Trim().Length > MaxLength)
                {
                    ret = InputString.Trim().Substring(InputString.Trim().Length - MaxLength);
                }
                else
                {
                    ret = InputString.Trim();
                }
            }
            return ret;
        }

        public static bool IsNullOrEmptyOrSomethingLikeThat(this string str)
        {
            if (str != null && str.Trim() != "")
            {
                if (str.Trim().Replace("'", "").Replace("%", "") != "")
                {
                    return false;
                }
                else
                {
                    return true;
                }
            }
            else
            {
                return true;
            }
        }

        public static byte[] GZipCompress(this string inputString)
        {
            Encoding encoding = System.Text.Encoding.ASCII;
            byte[] textBytes = encoding.GetBytes(inputString);

            var memStream = new MemoryStream();
            using (GZipStream Compress = new GZipStream(memStream, CompressionMode.Compress))
            {
                Compress.Write(textBytes, 0, textBytes.Length);
            }
            return memStream.ToArray();
        }

        public static string GetFileSizeText(this long Bytes)
        {
            if (Bytes >= 1152921504606846976)
            {
                Decimal size = Decimal.Divide(Bytes, 1152921504606846976);
                return String.Format("{0:##.##} EB", size);//exabyte
            }
            else if (Bytes >= 1125899906842624)
            {
                Decimal size = Decimal.Divide(Bytes, 1125899906842624);
                return String.Format("{0:##.##} PB", size);//petabyte
            }
            else if (Bytes >= 1099511627776)
            {
                Decimal size = Decimal.Divide(Bytes, 1099511627776);
                return String.Format("{0:##.##} TB", size);//terabyte
            }
            else if (Bytes >= 1073741824)
            {
                Decimal size = Decimal.Divide(Bytes, 1073741824);
                return String.Format("{0:##.##} GB", size);//gigabyte
            }
            else if (Bytes >= 1048576)
            {
                Decimal size = Decimal.Divide(Bytes, 1048576);
                return String.Format("{0:##.##} MB", size);//megabyte
            }
            else if (Bytes >= 1024)
            {
                Decimal size = Decimal.Divide(Bytes, 1024);
                return String.Format("{0:##.##} KB", size);//kilobyte
            }
            else if (Bytes > 0 & Bytes < 1024)
            {
                Decimal size = Bytes;
                return String.Format("{0:##.##} Bytes", size);//byte
            }
            else
            {
                return "0 Bytes";
            }
        }

        //http://extensionmethod.net/csharp/string
       
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


        #region Vehicle-related
        public static bool IsValidVin(string vinstring)
        {
            // WDBVG78J38A002076
            if (vinstring.Length != 17)  // invalid length
                return false;
            else
                return true;
        }



        #endregion




    }
}
