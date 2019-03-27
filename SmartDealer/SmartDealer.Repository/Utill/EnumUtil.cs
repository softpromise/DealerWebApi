using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Text.RegularExpressions;

namespace SmartDealer.Repository.Utill
{
    public static class EnumUtil
    {

        public static T GetAttribute<T>(this Enum enumValue) where T : Attribute
        {
            var memberInfo = enumValue.GetType().GetMember(enumValue.ToString()).FirstOrDefault();

            if (memberInfo == null)
                return null;

            var attribute = (T)memberInfo.GetCustomAttributes(typeof(T), false).FirstOrDefault();
            return attribute;
        }


        public static IEnumerable<T> GetValues<T>()
        {
            return Enum.GetValues(typeof(T)).Cast<T>();
        }


        /// <summary>
        /// Get the descrition of an enum type value.
        /// </summary>
        /// <param name="enumerationValue">Enum member</param>
        /// <returns>Description attribute of an enum value if available. Otherwise, returns enum value name. </returns>
        public static string GetDescription(this object enumerationValue)
        {
            Type type = enumerationValue.GetType();
            if (!type.IsEnum)
                throw new ArgumentException("EnumerationValue must be of Enum type", "enumerationValue");

            // Tries to find a DescriptionAttribute for a potential friendly name for the enum
            MemberInfo[] memberInfo = type.GetMember(enumerationValue.ToString());
            if (memberInfo != null && memberInfo.Length > 0)
            {
                object[] attrs = memberInfo[0].GetCustomAttributes(typeof(DescriptionAttribute), false);

                if (attrs != null && attrs.Length > 0)
                    return ((DescriptionAttribute)attrs[0]).Description;  //Pull out the description value

            }
            // If we have no description attribute, just return the ToString of the enum
            return enumerationValue.ToString();
        }

        public static T GetLeastEnumValueByDescription<T>(string descriptionValue)
            where T : struct
        {
            T ret = default(T);
            var type = GetEnumType<T>();

            if (descriptionValue != null)
            {
                foreach (T val in Enum.GetValues(type))
                {
                    if (descriptionValue.Equals(GetDescription(val)))
                    {
                        ret = val;
                        break;
                    }
                }
            }

            return ret;
        }


        public static T[] GetEnumValuesByDescription<T>(string descriptionValue)
            where T : struct
        {
            List<T> ret = new List<T>();
            var type = GetEnumType<T>();

            foreach (T val in Enum.GetValues(type))
            {
                if (descriptionValue.Equals(GetDescription(val)))
                {
                    ret.Add(val);
                    break;
                }
            }
            return ret.ToArray();
        }


        /// <summary>
        /// Parses a string of single-letter flag enum descriptions and bitwise ORs them.
        /// 
        /// E.g., for
        /// [Flags] enum Foo { [Description("A")] FlagA = 1, [Description("B")] FlagB = 2 },
        /// GetFlagsBySingleLetterDescriptions&lt;Foo&gt;("AB") returns Foo.FlagA | Foo.FlagB.
        /// </summary>
        /// <typeparam name="T">a flag enum type with single-letter Description attributes</typeparam>
        public static T GetFlagsBySingleLetterDescriptions<T>(string descriptions)
            where T : struct
        {
            descriptions = descriptions ?? "";
            var strings = Regex.Split(descriptions, ""); // split into characters as strings
            return GetFlagsByDescriptions<T>(strings);
        }

        /// <summary>
        /// Parses comma-separated flag enum descriptions and bitwise ORs them.
        /// 
        /// E.g., for
        /// [Flags] enum Foo { [Description("A")] FlagA = 1, [Description("Bbb")] FlagB = 2 },
        /// GetFlagsByCommaSeparatedDescriptions&lt;Foo&gt;("A,Bbb") returns Foo.FlagA | Foo.FlagB.
        /// GetFlagsByCommaSeparatedDescriptions&lt;Foo&gt;("A, Bbb") also returns Foo.FlagA | Foo.FlagB.
        /// </summary>
        public static T GetFlagsByCommaSeparatedDescriptions<T>(string descriptions)
            where T : struct
        {
            descriptions = descriptions ?? "";
            var strings = descriptions.Split(',').Select(s => s.Trim());
            return GetFlagsByDescriptions<T>(strings);
        }

        public static T GetFlagsByDescriptions<T>(IEnumerable<string> descriptions)
            where T : struct
        {
            var flags = descriptions.Select(GetLeastEnumValueByDescription<T>);
            return CombineFlags(flags);
        }

        public static T CombineFlags<T>(IEnumerable<T> flags)
            where T : struct
        {
            var type = GetEnumType<T>();

            var underlyingType = Enum.GetUnderlyingType(type);
            var isUnderlyingTypeUnsigned = underlyingType == typeof(UInt32) || underlyingType == typeof(UInt64);

            var ret = isUnderlyingTypeUnsigned
                          ? flags.Aggregate(0UL, (current, value) => current | Convert.ToUInt64(value))
                          : (object)flags.Aggregate(0L, (current, value) => current | Convert.ToInt64(value));

            return (T)Enum.Parse(type, ret.ToString());
        }

        public static T AllFlags<T>()
            where T : struct
        {
            return CombineFlags((T[])Enum.GetValues(typeof(T)));
        }

        public static T[] SplitFlags<T>(T value)
            where T : struct
        {
            var type = GetEnumType<T>();
            var enumValue = (Enum)(object)value;
            var all = (T[])Enum.GetValues(type);
            return all.Where(t => enumValue.HasFlag((Enum)(object)t)).ToArray();
        }

        private static Type GetEnumType<T>()
            where T : struct
        {
            var type = typeof(T);
            if (!type.IsEnum)
                throw new ArgumentException("T must be of Enum type", "T");
            return type;
        }
    }







    // from http://hugoware.net/blog/enumeration-extensions-2-0
    /*
    Example:

        //create the typical object
    RegexOptions options = RegexOptions.None;
 
    //Assign a value
    options = options.Include(RegexOptions.IgnoreCase);
    //options = IgnoreCase
 
    //Or assign multiple values
    options = options.Include(RegexOptions.Multiline | RegexOptions.Singleline);
    //options = IgnoreCase, Multiline, Singleline
 
    //Remove values from the list
    options = options.Remove(RegexOptions.IgnoreCase);
    //options = Multiline, Singleline
 
    //Check if a value even exists
    bool multiline = options.Has(RegexOptions.Multiline); //true
    bool ignoreCase = options.Missing(RegexOptions.IgnoreCase); //true
     */

    /// <summary>
    /// Extension methods to make working with Enum values easier
    /// </summary>
    public static class EnumerationExtensions
    {

        #region Extension Methods

        /// <summary>
        /// Includes an enumerated type and returns the new value
        /// </summary>
        public static T Include<T>(this Enum value, T append)
        {
            Type type = value.GetType();

            //determine the values
            object result = value;
            _Value parsed = new _Value(append, type);
            if (parsed.Signed is long)
            {
                result = Convert.ToInt64(value) | (long)parsed.Signed;
            }
            else if (parsed.Unsigned is ulong)
            {
                result = Convert.ToUInt64(value) | (ulong)parsed.Unsigned;
            }

            //return the final value
            return (T)Enum.Parse(type, result.ToString());
        }

        /// <summary>
        /// Removes an enumerated type and returns the new value
        /// </summary>
        public static T Remove<T>(this Enum value, T remove)
        {
            Type type = value.GetType();

            //determine the values
            object result = value;
            _Value parsed = new _Value(remove, type);
            if (parsed.Signed is long)
            {
                result = Convert.ToInt64(value) & ~(long)parsed.Signed;
            }
            else if (parsed.Unsigned is ulong)
            {
                result = Convert.ToUInt64(value) & ~(ulong)parsed.Unsigned;
            }

            //return the final value
            return (T)Enum.Parse(type, result.ToString());
        }

        /// <summary>
        /// Checks if an enumerated type contains a value
        /// </summary>
        public static bool Has<T>(this Enum value, T check)
        {
            Type type = value.GetType();

            //determine the values
            object result = value;
            _Value parsed = new _Value(check, type);
            if (parsed.Signed is long)
            {
                return (Convert.ToInt64(value) &
                    (long)parsed.Signed) == (long)parsed.Signed;
            }
            else if (parsed.Unsigned is ulong)
            {
                return (Convert.ToUInt64(value) &
                    (ulong)parsed.Unsigned) == (ulong)parsed.Unsigned;
            }
            else
            {
                return false;
            }
        }

        /// <summary>
        /// Checks if an enumerated type is missing a value
        /// </summary>
        public static bool Missing<T>(this Enum obj, T value)
        {
            return !EnumerationExtensions.Has<T>(obj, value);
        }

        #endregion

        #region Helper Classes

        //class to simplfy narrowing values between
        //a ulong and long since either value should
        //cover any lesser value
        private class _Value
        {

            //cached comparisons for tye to use
            private static Type _UInt64 = typeof(ulong);
            private static Type _UInt32 = typeof(long);

            public long? Signed;
            public ulong? Unsigned;

            public _Value(object value, Type type)
            {

                //make sure it is even an enum to work with
                if (!type.IsEnum)
                {
                    throw new
                        ArgumentException("Value provided is not an enumerated type!");
                }

                //then check for the enumerated value
                Type compare = Enum.GetUnderlyingType(type);

                //if this is an unsigned long then the only
                //value that can hold it would be a ulong
                if (compare.Equals(_Value._UInt32) || compare.Equals(_Value._UInt64))
                {
                    this.Unsigned = Convert.ToUInt64(value);
                }
                //otherwise, a long should cover anything else
                else
                {
                    this.Signed = Convert.ToInt64(value);
                }

            }

        }

        #endregion

    }
}
