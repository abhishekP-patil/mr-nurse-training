using System;
using System.Text;
using System.Security.Cryptography;

namespace DTT.PlayerPrefsEnhanced
{
    /// <summary>
    /// Class used for encryption following the AES mode of operation.
    /// </summary>
    public class EncryptorAES : IEncryptor<string, string>
    {
        /// <summary>
        /// Crypto provider used for encrypting and decrypting.
        /// </summary>
        private static AesCryptoServiceProvider _cryptoProviderAes;

        /// <summary>
        /// Key used to encrypt
        /// </summary>
        private static string _key = "j1a4L8nKVobthNK7QzFWP1YCAJpFUhFj"; // Has to be exactly 32 chars.

        /// <summary>
        /// Initialization vector used to randomise encryption.
        /// </summary>
        private static string _iv = "JpjJGVl5ZEMhGvUN"; // Has to be exactly 16 chars.

        /// <summary>
        /// Initialize the crypto service provider settings.
        /// </summary>
        public void InitializeServiceProvider()
        {
            _cryptoProviderAes = new AesCryptoServiceProvider();
            _cryptoProviderAes.BlockSize = 128;
            _cryptoProviderAes.KeySize = 256;
            _cryptoProviderAes.Key = ASCIIEncoding.ASCII.GetBytes(_key);
            _cryptoProviderAes.IV = ASCIIEncoding.ASCII.GetBytes(_iv);
            _cryptoProviderAes.Mode = CipherMode.CBC;
            _cryptoProviderAes.Padding = PaddingMode.PKCS7;
        }

        /// <summary>
        /// Encrypts a given string.
        /// </summary>
        /// <param name="data">String to encrypt.</param>
        /// <returns>The encrypted string value.</returns>
        public string Encrypt(string data)
        {
            if (_cryptoProviderAes == null)
                InitializeServiceProvider();

            byte[] txtByteData = ASCIIEncoding.ASCII.GetBytes(data);

            ICryptoTransform cryptoTransform = _cryptoProviderAes.CreateEncryptor(_cryptoProviderAes.Key, _cryptoProviderAes.IV);

            byte[] result = cryptoTransform.TransformFinalBlock(txtByteData, 0, txtByteData.Length);

            return Convert.ToBase64String(result);
        }

        /// <summary>
        /// Decrypts a given string.
        /// </summary>
        /// <param name="data">String to decrypt.</param>
        /// <returns>The decrypted string value.</returns>
        public string Decrypt(string data)
        {
            if (_cryptoProviderAes == null)
                InitializeServiceProvider();

            byte[] txtByteData = Convert.FromBase64String(data);

            ICryptoTransform trnsfrm = _cryptoProviderAes.CreateDecryptor();

            byte[] result = trnsfrm.TransformFinalBlock(txtByteData, 0, txtByteData.Length);

            return ASCIIEncoding.ASCII.GetString(result);
        }
    }
}