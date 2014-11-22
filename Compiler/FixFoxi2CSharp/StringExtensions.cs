using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FixFoxi2CSharp
{
    public static class StringExtensions
    {
        public static string FirstUpper(this string value)
        {
            if (String.IsNullOrEmpty(value)) { return value; }
            else { return Char.ToUpper(value[0]) + value.Substring(1); }
        }
    }
}
