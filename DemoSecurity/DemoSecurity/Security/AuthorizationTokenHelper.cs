using System;
using Newtonsoft.Json;

namespace DemoSecurity.Security
{
    public static class AuthorizationTokenHelper
    {
        public static string GenerateToken()
        {
            var token = new Token();
            var jsonToken = JsonConvert.SerializeObject(token);

            return jsonToken.Encrypt();
        }

        public static bool IsTokenValid(this string encryptedToken)
        {
            if (string.IsNullOrEmpty(encryptedToken))
            {
                return false;
            }

            var isValid = true;
            try
            {
                var decryptedToken = encryptedToken.Decrypt();
                var token = JsonConvert.DeserializeObject<Token>(decryptedToken);
                return IsTokenValid(token);
            }
            catch
            {
                isValid = false;
            }

            return isValid;
        }

        private static bool IsTokenValid(Token token)
        {
            return token.ValidUntil >= DateTime.UtcNow;
        }
    }
}
