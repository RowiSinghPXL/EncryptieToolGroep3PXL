using System;
using System.Collections.Generic;
using System.Security.Cryptography;
using System.Text;

namespace ClassLibrary1
{
    public static class GenerateAesKey
    {
        public static byte[] AesKey()
        {
            AesManaged aes = new AesManaged();
            aes.GenerateKey();
            return aes.Key;
        }
        public static byte[] AesIv()
        {
            AesManaged aes = new AesManaged();
            aes.GenerateKey();
            return aes.IV;
        }


        public static string AesKeyString()
        {
            
            return Convert.ToBase64String(AesKey());
        }

        public static string AesIvString()
        {
            return Convert.ToBase64String(AesIv());
        }
    }
}
