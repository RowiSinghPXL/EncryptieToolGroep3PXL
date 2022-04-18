using System;
using System.Collections.Generic;
using System.Security.Cryptography;
using System.Text;

namespace ClassLibrary1
{
    public class RsaHelper
    {
        private RSA _rsa;

        public RsaHelper()
        {
            _rsa = RSA.Create(2048);
            //var pulickey = _rsa.ExportRSAPublicKey();
            //var privatekey = _rsa.ExportPkcs8PrivateKey();
        }

        public byte[] Encrypt(byte[] dataToEncrypt)
        {
            return _rsa.Encrypt(dataToEncrypt, RSAEncryptionPadding.OaepSHA256);
        }

        public byte[] Decrypt(byte[] dataToDecrypt)
        {
            return _rsa.Decrypt(dataToDecrypt, RSAEncryptionPadding.OaepSHA256);
        }


        public byte[] ExportPrivateKey(int numberOfIterations, string password)
        {
            byte[] encryptedPrivateKey = new byte[2000];

            PbeParameters keyParams = new PbeParameters(PbeEncryptionAlgorithm.Aes256Cbc, HashAlgorithmName.SHA256, numberOfIterations);
            var arraySpan = new Span<byte>(encryptedPrivateKey);
            _rsa.TryExportEncryptedPkcs8PrivateKey(Encoding.UTF8.GetBytes(password), keyParams, arraySpan, out _);

            return encryptedPrivateKey;
        }

        public string ExportPrivateKeyString(int numberOfIterations, string password)
        {
            byte[] encryptedPrivateKey = new byte[2000];

            PbeParameters keyParams = new PbeParameters(PbeEncryptionAlgorithm.Aes256Cbc, HashAlgorithmName.SHA256, numberOfIterations);
            var arraySpan = new Span<byte>(encryptedPrivateKey);
            _rsa.TryExportEncryptedPkcs8PrivateKey(Encoding.UTF8.GetBytes(password), keyParams, arraySpan, out _);

            return Convert.ToBase64String(encryptedPrivateKey);
        }

        public void ImportEncryptedPrivatekey(byte[] encryptedKey, string password)
        {
            _rsa.ImportEncryptedPkcs8PrivateKey(Encoding.UTF8.GetBytes(password), encryptedKey, out _);
        }

        public byte[] ExportPublicKey()
        {
            return _rsa.ExportRSAPublicKey();
        }

        public string ExportPublicKeyString()
        {
            return Convert.ToBase64String(_rsa.ExportRSAPublicKey());
        }

        public void ImportPublicKey(byte[] publicKey)
        {
            _rsa.ImportRSAPublicKey(publicKey, out _);
        }


    }
}
