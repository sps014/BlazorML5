using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace ML5
{
    public class Helper
    {
        public static string UIDGenerator()
        {
            string milliseconds = GetMillisecondsSince().ToString();
            milliseconds += new Random(DateTime.Now.Millisecond).Next();

            byte[] hash;
            using (MD5 md5 = MD5.Create())
            {
                hash = md5.ComputeHash(Encoding.UTF8.GetBytes(milliseconds));
            }

            return StringFromByte(hash);
        }
        private static string StringFromByte(byte[] data)
        {
            StringBuilder builder = new StringBuilder("");
            foreach (byte item in data)
            {
                builder.Append((int)(item));
            }
            return builder.ToString();
        }
        private static long GetMillisecondsSince()
        {
            DateTime baseDate = new DateTime(1970, 1, 1);
            TimeSpan diff = DateTime.Now - baseDate;
            return diff.Milliseconds;
        }
    }

}
