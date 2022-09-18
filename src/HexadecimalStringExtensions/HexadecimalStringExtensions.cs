using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace GON.Extensions
{
    public static class HexadecimalStringExtensions
    {
        private static byte[] HexToByteArray(string hex)
        {
            hex = ClearHexUnwantedSymbols(hex);

            if (hex.Length == 0)
                return new byte[] { };

            if (hex.Length % 2 != 0)
                throw new ArgumentException("Hexadecimal string cannot have an odd number of digits");

            byte[] arr = new byte[hex.Length >> 1];
            for (int i = 0; i < (hex.Length >> 1); ++i)
            {
                arr[i] = (byte)((hex[i << 1].GetHexValue() << 4) + (hex[(i << 1) + 1].GetHexValue()));
            }

            return arr;
        }

        private static string ClearHexUnwantedSymbols(string hex)
        {
            return hex.ToUpper()
                .Replace(" ", "")
                .Replace("\t", "")
                .Replace("-", "")
                .Replace(":", "")
                .Replace(";", "")
                .Replace("H", "")
                .Replace("0X", "")
                .Replace(",", "")
                .Replace("\t", "")
                .Replace("\r", "")
                .Replace("\n", "");
        }

        private static int GetHexValue(this char hex)
        {
            int val = (int)hex;
            return val - (val < 58 ? 48 : 55); //uppercase A-F letters
        }

        public static IEnumerable<byte> ToByteArray(this string str) => HexToByteArray(str);

        public static byte[] bin(this string str) => HexToByteArray(str);

        public static string ToHex(this byte[] buf) => BitConverter.ToString(buf).Replace("-", "").ToUpper();
        public static string ToHex(this IEnumerable<byte> buf) => BitConverter.ToString(buf.ToArray()).Replace("-", "").ToUpper();

        public static string hex(this byte[] buf) => BitConverter.ToString(buf).Replace("-", "").ToUpper();
        public static string hex(this IEnumerable<byte> buf) => BitConverter.ToString(buf.ToArray()).Replace("-", "").ToUpper();
        public static string hex(this byte b) => b.ToString("X2");

    }
}
