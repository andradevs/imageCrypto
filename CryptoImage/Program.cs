using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CryptoImage
{
    internal class Program
    {
        static void Main(string[] args)
        {
            string hashEco = ConfigurationManager.AppSettings["CRYPTO_KEY"];

            using (Stream inputStream = new FileStream(@"C:\Users\Fadami\Pictures\Screenshots\Captura de tela 2024-09-30 150923.jpg", FileMode.Open))
            {
                // Criptografar
                string imagemCripto = CryptoHelper.EncryptBase64(inputStream, hashEco);

                //Console.WriteLine("Texto Criptografado:" + imagemCripto);
                //string base64Input;
                //using (MemoryStream memoryStream = new MemoryStream())
                //{
                //    inputStream.CopyTo(memoryStream);
                //    byte[] bytes = memoryStream.ToArray();
                //    base64Input = Convert.ToBase64String(bytes);
                //}
                //string imagemCripto = CryptoHelper.EncryptBase64(base64Input, hashEco);

                // Descriptografar
                string imagemDecript = CryptoHelper.DecryptBase64(imagemCripto, hashEco);
                //Console.WriteLine("Texto Descriptografado: " + imagemDecript);
            }
        }
    }
}
