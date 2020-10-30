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
        private static int HashCount=0;
        public static string UIDGenerator()
        {

            

            return (HashCount++).ToString();
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
