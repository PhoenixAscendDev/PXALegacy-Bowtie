using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using System.Text.RegularExpressions;
using System.Security.Cryptography;

using JB2.Common.Extensions;

namespace JB2.Bowtie
{
    public static class Utility
    {


        public static string[] BowtieSplit(string stringToSplit)
        {
            //return Regex.Split(value, ">*<")
            return stringToSplit.Split(">*<");
        }

        public static string GenerateNewObjectID()
        {
            Guid guid = Guid.NewGuid();
            JB2.Common.ShortGuid sguid = guid;

            return sguid.ToString();

            //return JB2.Common.Utility.GenerateKey(Common.Enum.KeyBitSize.keybit256, null);

        }

        public static char[] asciiArray = new char[95] {
        ' ', '!', '"', '#', '$', '%', '&', '\'', '(', ')', 
       '*', '+', ',', '-', '.', '/','0', '1', '2', '3', 
       '4', '5', '6', '7', '8', '9', ':', ';', '<', '=', 
       '>', '?', '@', 'A', 'B', 'C', 'D', 'E', 'F', 'G', 
       'H', 'I', 'J', 'K', 'L', 'M', 'N', 'O', 'P', 'Q', 
       'R', 'S', 'T', 'U', 'V', 'W', 'X', 'Y', 'Z', '[', 
       '\\', ']', '^', '_', '\'', 'a', 'b', 'c', 'd', 'e', 
       'f', 'g', 'h', 'i', 'j', 'k', 'l', 'm', 'n', 'o', 
       'p', 'q', 'r', 's', 't', 'u', 'v', 'w', 'x', 'y', 
       'z', '{', '|', '}', '~'};

        public static string[] hexArray = new string[95] {
          "20","21","22","23","24","25","26","27","28","29",
           "2A","2B","2C","2D","2E","2F","30","31","32","33",
           "34","35","36","37","38","39","3A","3B","3C","3D",
          "3E","3F","40","41","42","43","44","45","46","47",
           "48","49","4A","4B","4C","4D","4E","4F","50","51",
          "52","53","54","55","56","57","58","59","5A","5B",
       "5C","5D","5E","5F","60","61","62","63","64","65",
          "66","67","68","69","6A","6B","6C","6D","6E","6F",
          "70","71","72","73","74","75","76","77","78","79",
           "7A","7B","7C","7D","7E" 
   };



        public static List<byte[]> ComputeWep40(string str)
        {
            byte[] pseed = new byte[4];
            int i = 0;
            byte[] data = System.Text.Encoding.ASCII.GetBytes(str);
            foreach (byte b in data)
            {
                pseed[i] ^= b;
                i = (++i) & 3;
            }
            int seed = BitConverter.ToInt32(pseed, 0);
            List<byte[]> result = new List<byte[]>();
            for (i = 0; i < 4; ++i)
            {
                byte[] wep = new byte[5];
                for (int j = 0; j < 5; ++j)
                {
                    seed = (214013 * seed + 0x269ec3) & 0xFFFFFF;
                    byte keybyte = (byte)(seed >> 16);
                    wep[j] = keybyte;
                }
                result.Add(wep);
            }
            return result;
        }

        public static byte[] ComputeWep128(string str)
        {
            byte[] result = ComputeWep156(str);
            Array.Resize(ref result, 13);
            return result;
        }

        public static byte[] ComputeWep156(string str)
        {
            while (str.Length < 64)
                str = str + str;

            str = str.Substring(0, 64);

            byte[] data = System.Text.Encoding.ASCII.GetBytes(str);
            MD5 md5 = new System.Security.Cryptography.MD5CryptoServiceProvider();
            return md5.ComputeHash(data);
        }


        /// <summary>
        /// Transforms byte array into an enumeration of blocks of 'blockSize' bytes
        /// </summary>
        /// <param name="inputAsBytes"></param>
        /// <param name="blockSize"></param>
        /// <returns></returns>
        private static IEnumerable<UInt64> Blockify(byte[] inputAsBytes, int blockSize)
        {
            int i = 0;

            //UInt64 used since that is the biggest possible value we can return.
            //Using an unsigned type is important - otherwise an arithmetic overflow will result
            UInt64 block = 0;

            //Run through all the bytes         
            while (i < inputAsBytes.Length)
            {
                //Keep stacking them side by side by shifting left and OR-ing               
                block = block << 8 | inputAsBytes[i];

                i++;

                //Return a block whenever we meet a boundary
                if (i % blockSize == 0 || i == inputAsBytes.Length)
                {
                    yield return block;

                    //Set to 0 for next iteration
                    block = 0;
                }
            }
        }

        /// <summary>
        /// Get Fletcher's checksum, n can be either 16, 32 or 64
        /// </summary>
        /// <param name="inputWord"></param>
        /// <param name="n"></param>
        /// <returns></returns>
        public static UInt64 GetChecksum(String inputWord, int n)
        {
            
            //Fletcher 16: Read a single byte
            //Fletcher 32: Read a 16 bit block (two bytes)
            //Fletcher 64: Read a 32 bit block (four bytes)
            int bytesPerCycle = n / 16;

            //2^x gives max value that can be stored in x bits
            //no of bits here is 8 * bytesPerCycle (8 bits to a byte)
            UInt64 modValue = (UInt64)(Math.Pow(2, 8 * bytesPerCycle) - 1);

            //ASCII encoding conveniently gives us 1 byte per character 
            byte[] inputAsBytes = Encoding.ASCII.GetBytes(inputWord);

            UInt64 sum1 = 0;
            UInt64 sum2 = 0;
            foreach (UInt64 block in Blockify(inputAsBytes, bytesPerCycle))
            {
                sum1 = (sum1 + block) % modValue;
                sum2 = (sum2 + sum1) % modValue;
            }

            return sum1 + (sum2 * (modValue + 1));
        }


        //public static string HashData(string rawdata)
        //{
        //    string result = string.Empty;
        //    var secretKeyByteArray = Convert.FromBase64String(JB2.Bowtie.Settings.CurrentApplication.Secret);

        //    byte[] signature = Encoding.UTF8.GetBytes(rawdata);

        //    using (HMACSHA256 hmac = new HMACSHA256(secretKeyByteArray))
        //    {
        //        byte[] signatureBytes = hmac.ComputeHash(signature);
        //         result = Convert.ToBase64String(signatureBytes);
        //    }

        //    return result;


        //}
    }
}
