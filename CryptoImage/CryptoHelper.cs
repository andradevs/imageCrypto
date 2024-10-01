using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Security.Cryptography;

namespace CryptoImage
{
    public class CryptoHelper
    {
        ///<summary>
        /// Criptografa o conteúdo do fluxo de entrada fornecido, convertendo-o primeiro em uma string Base64 e, 
        /// em seguida, criptografando essa string usando a chave especificada.
        ///</summary>
        /// <returns>Uma string Base64 encriptada.</returns>
        public static string EncryptBase64(Stream input, string key)
        {
            string base64Input;
            byte[] hash = MD5.Create().ComputeHash(Encoding.ASCII.GetBytes(key));

            using (MemoryStream memoryStream = new MemoryStream())
            {
                input.CopyTo(memoryStream);
                byte[] bytes = memoryStream.ToArray();
                base64Input = Convert.ToBase64String(bytes);
            }

            try
            {
                string encryptedBase64 = Encrypt(base64Input, hash);
                return encryptedBase64;
            }
            catch (Exception ex)
            {
                throw new Exception("Erro durante a criptografia:" + ex.Message);
            }
        }

        ///<summary>
        /// Criptografa o conteúdo de uma string Base64 e,
        /// em seguida, criptografa essa string usando a chave especificada. 
        ///</summary>
        /// <returns>Uma string Base64 encriptada.</returns>
        public static string EncryptBase64(string inputBase64, string key)
        {
            byte[] hash = MD5.Create().ComputeHash(Encoding.ASCII.GetBytes(key));

            try
            {
                string encryptedBase64 = Encrypt(inputBase64, hash);
                return encryptedBase64;
            }
            catch (Exception ex)
            {
                throw new Exception("Erro durante a criptografia:" + ex.Message);
            }
        }

        ///<summary>
        /// Descriptografa o conteúdo de uma string Base64 e,
        /// em seguida, criptografa essa string usando a chave especificada.
        ///</summary>
        /// <returns>Uma string Base64 decriptada.</returns>
        public static string DecryptBase64(string value, string key)
        {
            byte[] hash = MD5.Create().ComputeHash(Encoding.ASCII.GetBytes(key));

            try
            {
                string DecryptedBase64 = Decrypt(value, hash);
                return DecryptedBase64;
            }
            catch (Exception ex)
            {
                throw new Exception("Erro durante a criptografia:" + ex.Message);
            }
        }

        private static string Encrypt(string value, byte[] hash)
        {
            byte[] bytes = Encoding.UTF8.GetBytes(value);
            TripleDESCryptoServiceProvider cryptoServiceProvider = new TripleDESCryptoServiceProvider();
            cryptoServiceProvider.Key = hash;
            cryptoServiceProvider.Mode = CipherMode.ECB;
            cryptoServiceProvider.Padding = PaddingMode.PKCS7;
            byte[] encrypted = cryptoServiceProvider.CreateEncryptor().TransformFinalBlock(bytes, 0, bytes.Length);
            cryptoServiceProvider.Clear();
            return Convert.ToBase64String(encrypted, 0, encrypted.Length);
        }

        private static string Decrypt(string value, byte[] hash)
        {
            byte[] bytes = Convert.FromBase64String(value);
            TripleDESCryptoServiceProvider cryptoServiceProvider = new TripleDESCryptoServiceProvider();
            cryptoServiceProvider.Key = hash;
            cryptoServiceProvider.Mode = CipherMode.ECB;
            cryptoServiceProvider.Padding = PaddingMode.PKCS7;
            byte[] decrypted = cryptoServiceProvider.CreateDecryptor().TransformFinalBlock(bytes, 0, bytes.Length);
            cryptoServiceProvider.Clear();
            return Encoding.UTF8.GetString(decrypted);
        }

    }
}
