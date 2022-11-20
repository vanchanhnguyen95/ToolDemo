using System.Security.Cryptography;
using System.Text;

namespace Sys.Common.Helper
{
    public static class MD5Algorithm
    {
        public static string HashMD5(this string token)
        {
            var hasdmd5 = MD5.Create();
            var newArray = hasdmd5.ComputeHash(Encoding.UTF8.GetBytes(token));

            // step 2, convert byte array to hex string
            var sb = new StringBuilder();
            for (int i = 0; i < newArray.Length; i++)
            {
                sb.Append(newArray[i].ToString("x2"));
            }

            return sb.ToString();
        }
    }
}