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

        public static byte[] XOR(this byte[] buf, byte[] buf2)
        {
            if (buf.Length != buf2.Length)
                throw new ArgumentException("Length of the arrays must be equal");

            byte[] ret = new byte[buf.Length];
            for (int i = 0; i < buf.Length; i++)
            {
                ret[i] = (byte)(buf[i] ^ buf2[i]);
            }

            return ret;
        }

        public static byte[] XOR(this byte[] buf, string buf2) => buf.XOR(buf2.bin());
        public static string XOR(this string buf, string buf2) => (buf.bin().XOR(HexToByteArray(buf2))).hex();
        public static string XOR(this string buf, byte[] buf2) => (buf.bin().XOR(buf2)).hex();

        public static byte[] Cut(this byte[] buf, int nStart, int nLen)
        {
            byte[] ret = new byte[nLen];
            Buffer.BlockCopy(buf, nStart, ret, 0, nLen);
            return ret;
        }

        public static byte[] Combine(this byte[] buf, params byte[][] arrays)
        {
            int nSize = buf.Length;
            foreach (byte[] data in arrays)
                nSize += data.Length;

            byte[] ret = new byte[nSize];
            int offset = 0;
            Buffer.BlockCopy(buf, 0, ret, offset, buf.Length);
            offset += buf.Length;
            foreach (byte[] data in arrays)
            {
                Buffer.BlockCopy(data, 0, ret, offset, data.Length);
                offset += data.Length;
            }
            return ret;
        }

        public static byte[] AdjustDESKeyParity(this byte[] pucKey)
        {
            bool cPar;
            for (int i = 0; i < pucKey.Length; i++)
            {
                cPar = true;
                for (int j = 1; j < 8; j++)
                {
                    if (0 != (pucKey[i] & (0x01 << j)))
                        cPar = !cPar;
                }
                if (cPar)
                    pucKey[i] |= 0x01;
                else
                    pucKey[i] &= 0xFE;
            }
            return pucKey;
        }
        public static string AdjustDESKeyParity(this string pucKey) => pucKey.bin().AdjustDESKeyParity().hex();

        public static string GetExceptionString(ref Exception e, string prefix = "")
        {
            StringBuilder sb = new StringBuilder();
            sb.AppendLine($"{prefix}\t{e.Source} : {e.Message}\r\n{e.StackTrace}");
            while (e.InnerException != null)
            {
                e = e.InnerException;
                sb.AppendLine($"[Inner]\t{e.Source} : {e.Message}\r\n{e.StackTrace}");
            }

            return sb.ToString();
        }

        public static bool IsHexDigit(this char ch) => char.IsDigit(ch) || (ch >= 'A' && ch <= 'F') || (ch >= 'a' && ch <= 'f');

        public static bool IsHexString(this string str) => (str.Length % 2 == 0) && !str.ToCharArray().Any(ch => !ch.IsHexDigit());

        public static byte[] PadLeft(this byte[] arr, int pad)
        {
            var ret = new byte[pad];
            Buffer.BlockCopy(arr, 0, ret, pad - arr.Length, arr.Length);
            return ret;
        }

        public static byte[] PadRight(this byte[] arr, int pad)
        {
            var ret = new byte[pad];
            Buffer.BlockCopy(arr, 0, ret, 0, arr.Length);
            return ret;
        }

        public static IEnumerable<IEnumerable<byte>> SplitToChunks(this byte[] arr, int size)
        {
            int count = (int)Math.Ceiling((double)arr.Length / size);
            for (int i = 0; i < count; i++)
                yield return arr.Skip(size * i).Take(size);
        }

        public static IEnumerable<string> SplitToChunks(this string str, int size)
        {
            int count = (int)Math.Ceiling((double)str.Length / size);
            for (int i = 0; i < count; i++)
                yield return new string(str.ToCharArray().Skip(size * i).Take(size).ToArray());
        }

        public static bool Compare(this IEnumerable<byte> pV1, IEnumerable<byte> pV2, bool bCompareLength = true)
        {
            if (bCompareLength && pV1.Count() != pV2.Count())
                return false;

            var e1 = pV1.GetEnumerator();
            var e2 = pV2.GetEnumerator();
            while (e1.MoveNext() && e2.MoveNext())
            {
                if (e1.Current != e2.Current)
                    return false;
            }

            return true;
        }
    }
}
