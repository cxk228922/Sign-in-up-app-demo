using System;
using System.Linq;
using System.Security.Cryptography;
using System.Text;

namespace MauiApp1.Helpers
{
    public static class SecurityHelper
    {
        public static string HashPassword(string password)
        {
            using (var hmac = new HMACSHA512())
            {
                var salt = hmac.Key;
                var passwordBytes = Encoding.UTF8.GetBytes(password);
                var hashedPassword = hmac.ComputeHash(passwordBytes);
                return $"{Convert.ToBase64String(salt)}:{Convert.ToBase64String(hashedPassword)}";
            }
        }

        
        public static bool VerifyPassword(string storedHash, string password)
        {
            var parts = storedHash.Split(':');
            var salt = Convert.FromBase64String(parts[0]);
            var storedPasswordHash = Convert.FromBase64String(parts[1]);
            
            using (var hmac = new HMACSHA512(salt))
            {
                var passwordBytes = Encoding.UTF8.GetBytes(password);
                var computedHash = hmac.ComputeHash(passwordBytes);
                
                return computedHash.SequenceEqual(storedPasswordHash);
            }
        }
    }
}
