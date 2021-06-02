using System;
using System.Collections.Generic;
using System.Text;

namespace MyPoints.Identity.Test.Utils
{
    public static class StringExtensions
    {
        public static string Cut (this string s, int length)
        {
            if (s.Length < length)
            {
                return s;
            }
            return s.Substring(0,length);
        }
    }
}
