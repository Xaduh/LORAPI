using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Threading.Tasks;

namespace LORAPI.Controllers
{
    public class SALT
    {
        /// <summary>
        /// Generates a cryptographically secure block of data suitable for salting hashes.
        /// </summary>
        /// <returns>A cryptographically secure block of data suitable for salting hashes.</returns>
        public static byte[] GenerateSalt(string pass)
        {
            byte[] salt = Convert.FromBase64String("LoL12Salt15Factory18");
            byte[] password = Convert.FromBase64String(pass);            
            HashAlgorithm algorithm = new SHA256Managed();
            byte[] plainTextWithSaltBytes = new byte[password.Length + salt.Length];
            for (int i = 0; i < password.Length; i++)
            {
                plainTextWithSaltBytes[i] = password[i];
            }
            for (int i = 0; i < salt.Length; i++)
            {
                plainTextWithSaltBytes[password.Length + i] = salt[i];
            }
            return algorithm.ComputeHash(plainTextWithSaltBytes);
        }        
    }
}