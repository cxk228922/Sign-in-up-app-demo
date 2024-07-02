using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace MauiApp1.Helpers
{
    internal class AESCipher
    {
        private static readonly byte[] Key = new byte[32]
        {
            1, 2, 3, 4, 5, 6, 7, 8,
            9, 10, 11, 12, 13, 14, 15, 16,
            17, 18, 19, 20, 21, 22, 23, 24,
            25, 26, 27, 28, 29, 30, 31, 32
        };

        private static readonly byte[] IV = new byte[16]
        {
            1, 2, 3, 4, 5, 6, 7, 8,
            9, 10, 11, 12, 13, 14, 15, 16
        };

        public static string Encrypt(string plainText)
        {
            using (Aes aes = Aes.Create())
            {
                aes.Key = Key;
                aes.IV = IV;
                aes.Padding = PaddingMode.PKCS7;    //PKCS7填充方法
                byte[] source = Encoding.UTF8.GetBytes(plainText);  //轉換為byte
                ICryptoTransform cryptoTransform = aes.CreateEncryptor();   //加密
                byte[] finalBytes = cryptoTransform.TransformFinalBlock(source, 0, source.Length);  //處理最後一個state
                return Convert.ToBase64String(finalBytes);  //Base64轉換
            }
        }
        
        public static string Decrypt(string cipherText)
        {
            using (Aes aes = Aes.Create())
            {
                aes.Key = Key;
                aes.IV = IV;
                aes.Padding = PaddingMode.PKCS7;
                byte[] cipherBytes = Convert.FromBase64String(cipherText);

                ICryptoTransform decryptor = aes.CreateDecryptor();
                byte[] plainBytes = decryptor.TransformFinalBlock(cipherBytes, 0, cipherBytes.Length);
                return Encoding.UTF8.GetString(plainBytes);
            }
        }
    }
}
