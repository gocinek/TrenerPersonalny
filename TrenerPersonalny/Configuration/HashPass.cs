using Microsoft.AspNetCore.Cryptography.KeyDerivation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Threading.Tasks;

namespace TrenerPersonalny.Configuration
{
    public class HashPass
    {
        public static byte[] salt()
        {
            byte[] salt = new byte[128 / 8];
            using (var rngCsp = new RNGCryptoServiceProvider())
            {
                rngCsp.GetNonZeroBytes(salt);
            }
            return salt;
        }

        public static string hashPass(string password, byte[] salt)
        {
            
            string hashedPassword = Convert.ToBase64String(KeyDerivation.Pbkdf2(
            password: password,
            salt: salt,
            prf: KeyDerivationPrf.HMACSHA256,
            iterationCount: 100000,
            numBytesRequested: 256 / 8));
            
            return hashedPassword;
        }

        public static bool VerifyPassword(string enteredPassword, string storedPassword, byte[] salt)
        {
            string encryptedPassw = Convert.ToBase64String(KeyDerivation.Pbkdf2(
                password: enteredPassword,
                salt: salt,
                prf: KeyDerivationPrf.HMACSHA256,
                iterationCount: 100000,
                numBytesRequested: 256 / 8));
            //encryptedPassw =  encryptedPassw.ToString();
        //    Console.WriteLine(encryptedPassw.ToString());
        //   Console.WriteLine(storedPassword.ToString());
            return 
                //String.Equals(encryptedPassw.ToString(), storedPassword.ToString());
                encryptedPassw == storedPassword;
        }


    }
}
