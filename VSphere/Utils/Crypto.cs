using System;
using System.Security.Cryptography;
using System.Text;

namespace VCenter.Utils
{
    static class Crypto
    {
        public static string CryptoMd5(string password)
        {
            MD5 md5 = System.Security.Cryptography.MD5.Create();
            byte[] s = md5.ComputeHash(Encoding.UTF8.GetBytes(password));

            return BitConverter.ToString(s).ToLower();
        }
    }
}
