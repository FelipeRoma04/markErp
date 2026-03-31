using System;
using System.Security.Cryptography;
using System.Text;

namespace Proyecto.Controler
{
    public static class PasswordHelper
    {
        /// <summary>
        /// Hash a plaintext password using SHA256 (simplified, BCrypt preferred in production)
        /// </summary>
        public static string HashPassword(string password)
        {
            if (string.IsNullOrWhiteSpace(password))
                throw new ArgumentException("Password cannot be empty");
            
            using (var sha = SHA256.Create())
            {
                var hashedBytes = sha.ComputeHash(Encoding.UTF8.GetBytes(password + "markERP2026"));
                return Convert.ToBase64String(hashedBytes);
            }
        }

        /// <summary>
        /// Verify a plaintext password against a hash
        /// </summary>
        public static bool VerifyPassword(string password, string hash)
        {
            if (string.IsNullOrWhiteSpace(password) || string.IsNullOrWhiteSpace(hash))
                return false;
            
            try
            {
                var hashOfInput = HashPassword(password);
                return hashOfInput == hash;
            }
            catch
            {
                return false;
            }
        }
    }
}
