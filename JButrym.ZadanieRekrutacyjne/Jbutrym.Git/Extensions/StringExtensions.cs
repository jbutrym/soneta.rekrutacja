using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JButrym.Git.Extensions
{
    public static class StringExtensions
    {
        public static string[] SplitToNewLine (this string value)
        {
            return value.Split(new string[] { "\n" }, StringSplitOptions.RemoveEmptyEntries);
        }
    }
}
