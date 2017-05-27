using System;
using System.IO;
using System.Security.Cryptography;
using System.Text;

namespace DemoSecurity.Security
{
    public sealed class Encryption
    {
        private const string Key = "ZiyR4KCpceSMGCs4JzrZBzQlc8y0Vk+pz7pvm/24tfM=";
        private const string IV = "a89kRY0Pc1oPgWYmkOCVKQ==";

        private readonly SymmetricAlgorithm cipher;

        public Encryption()
        {
            cipher = Aes.Create();

            cipher.Key = Convert.FromBase64String(Key);
            cipher.IV = Convert.FromBase64String(IV);
        }

        public string Encrypt(string plainText)
        {
            using (var encryptor = cipher.CreateEncryptor())
            using (var buffer = new MemoryStream())
            using (var stream = new CryptoStream(buffer, encryptor, CryptoStreamMode.Write))
            {
                using (var writer = new StreamWriter(stream, Encoding.Unicode))
                {
                    writer.Write(plainText);
                }

                return Convert.ToBase64String(buffer.ToArray());
            }
        }

        public string Decrypt(string encryptedText)
        {
            using (ICryptoTransform decryptor = cipher.CreateDecryptor())
            using (var buffer = new MemoryStream(Convert.FromBase64String(encryptedText)))
            using (var stream = new CryptoStream(buffer, decryptor, CryptoStreamMode.Read))
            using (var reader = new StreamReader(stream, Encoding.Unicode))
            {
                return reader.ReadToEnd();
            }
        }
    }
}
