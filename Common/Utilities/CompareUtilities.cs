using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Common.Utilities
{
    public static class CompareUtilities
    {
        public static bool ByteArrayEquals(this byte[] first, byte[] second)
        {
            if (first.Length != second.Length)
                return false;
            for (int i = 0; i < first.Length; i++)
                if (first[i] != second[i])
                    return false;
            return true;
        }


        public static string RequestReverse(this string text)
        {

            var split = text.Split('/');

            return $"{split[2]}/{split[1]}/{split[0]}";

        }
    }
}
