namespace DemoSecurity.Security
{
    public static class SecurityExtensions
    {
        private static readonly Encryption encryption = new Encryption();

        private static readonly Hashing hashing = new Hashing();

        public static string Encrypt(this string plainText)
        {
            if (string.IsNullOrEmpty(plainText))
            {
                return plainText;
            }

            return encryption.Encrypt(plainText);
        }

        public static string Decrypt(this string encryptedText)
        {
            if (string.IsNullOrEmpty(encryptedText))
            {
                return encryptedText;
            }

            return encryption.Decrypt(encryptedText);
        }

        public static string Hash(this string text)
        {
            if (string.IsNullOrEmpty(text))
            {
                return text;
            }

            return hashing.Hash(text);
        }
    }
}
