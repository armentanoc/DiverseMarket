using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace SalesApp.Infrastructure.Util
{
    internal class HashUtil
    {
        internal static (string, string) GetHashedAndSalt(string input)
        {
            byte[] salt;
            new RNGCryptoServiceProvider().GetBytes(salt = new byte[16]);

            var pbkdf2 = new Rfc2898DeriveBytes(input, salt, 2500);

            byte[] hash = pbkdf2.GetBytes(30);
            byte[] hashBytes = new byte[hash.Length + salt.Length];

            Array.Copy(salt, 0, hashBytes, 0, salt.Length);
            Array.Copy(hash, 0, hashBytes, salt.Length, hash.Length);

            return (Convert.ToBase64String(hashBytes), Convert.ToBase64String(salt));
        }

        internal static string GetHashedWithGivenSalt(string input, string salt)
        {
            byte[] saltBytes = Convert.FromBase64String(salt);

            var pbkdf2 = new Rfc2898DeriveBytes(input, saltBytes, 2500);

            byte[] hash = pbkdf2.GetBytes(30);
            byte[] hashBytes = new byte[hash.Length + saltBytes.Length];


            Array.Copy(saltBytes, 0, hashBytes, 0, saltBytes.Length);
            Array.Copy(hash, 0, hashBytes, saltBytes.Length, hash.Length);

            string computedHash = Convert.ToBase64String(hashBytes);

            return computedHash;

        }
    }
}
