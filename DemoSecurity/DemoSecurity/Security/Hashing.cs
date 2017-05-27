using System;
using System.Security.Cryptography;
using System.Text;

namespace DemoSecurity.Security
{
    public class Hashing
    {
        private readonly SHA256 sha;

        public Hashing()
        {
            sha = SHA256.Create();
        }

        public string Hash(string text)
        {
            var textBytes = Encoding.UTF8.GetBytes(text);
            var hashedText = sha.ComputeHash(textBytes);
            return Convert.ToBase64String(hashedText);
        }
    }
}
