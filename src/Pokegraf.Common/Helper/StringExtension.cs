using System;

namespace Pokegraf.Common.Helper
{
    public static class StringExtension
    {
        /// <summary>
        /// Fastest way to upper first letter of a word on thread:
        /// https://stackoverflow.com/questions/4135317/make-first-letter-of-a-string-upper-case-with-maximum-performance/27073919#27073919
        /// </summary>
        /// <param name="s"></param>
        /// <returns></returns>
        /// <exception cref="ArgumentException"></exception>
        public static string FirstLetterToUpperCase(this string s)
        {
            if (string.IsNullOrEmpty(s))
                throw new ArgumentException("There is no first letter");

            char[] a = s.ToCharArray();
            a[0] = char.ToUpper(a[0]);
            return new string(a);
        }
    }
}