using System;
using System.Security.Cryptography;
using System.Text;

namespace VSphere.Utils
{
    static class Crypto
    {
        public static string EncryptMd5(string password)
        {
            MD5 md5 = MD5.Create();
            byte[] s = md5.ComputeHash(Encoding.UTF8.GetBytes(password));

            return BitConverter.ToString(s).ToLower();
        }
    }
}
