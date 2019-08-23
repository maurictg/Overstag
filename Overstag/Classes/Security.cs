﻿using System;
using System.Text;
using System.Security.Cryptography;
using TwoFactorAuthentication;
using Overstag.Models;
using System.Linq;
using System.Collections.Generic;

namespace Overstag.Encryption
{
    public static class Random
    {
        private static System.Random rnd = new System.Random();
        public static string rString (int length)
        {
            const string chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz0123456789!@#$%^&*()_+";
            return new string(Enumerable.Repeat(chars, length)
              .Select(s => s[rnd.Next(s.Length)]).ToArray());
        }
        public static int rInt(int min, int max){return rnd.Next(min, max);}
        public static string rHash(string seed) { return PBKDF2.Hash(seed + rString(6)).Replace("/","").Replace("?",""); }
    }
    public static class SHA
    {
        public static string S256(string input)
        {
            var crypt = new System.Security.Cryptography.SHA256Managed();
            var hash = new System.Text.StringBuilder();
            byte[] crypto = crypt.ComputeHash(Encoding.UTF8.GetBytes(input));
            foreach (byte theByte in crypto)
            {
                hash.Append(theByte.ToString("x2"));
            }
            return hash.ToString();
        }
    }
    public static class PBKDF2
    {
        private const int defaultIterations = 10000;
        private const int defaultLength = 16;

        /// <summary>
        /// Hash a string with PBKDF2.
        /// </summary>
        /// <param name="input">String to hash</param>
        /// <param name="iterations">Rounds PBKDF2 will make to genarete the hash</param>
        /// <param name="length">Lenth of the hash, the output will also contains the salt</param>
        /// <returns>The generated salt+hash</returns>
        public static string Hash(string input, int iterations = defaultIterations, int length = defaultLength)
        {
            if (string.IsNullOrEmpty(input))
                throw new ArgumentException("Could not hash input: Input is empty.");
            else if (iterations <= 0)
                throw new ArgumentOutOfRangeException("Could not hash input: Iterations can't be negative or 0.");
            else if (length <= 0)
                throw new ArgumentOutOfRangeException("Could not hash input: Length can't be negative or 0.");

            //Create salt of 16 byte's.
            byte[] salt = new byte[16];
            new RNGCryptoServiceProvider().GetBytes(salt);

            //Create hash of [length] byte's.
            byte[] hash = new Rfc2898DeriveBytes(input, salt, iterations).GetBytes(length);

            //Combine salt and hash.
            byte[] combinedBytes = new byte[16 + length];
            Buffer.BlockCopy(salt, 0, combinedBytes, 0, 16);//Add salt.
            Buffer.BlockCopy(hash, 0, combinedBytes, 16, length);//Add hash.

            //Return combinedBytes as base64 string.
            return Convert.ToBase64String(combinedBytes);
        }

        /// <summary>
        /// Hash a byte[] with PBKDF2.
        /// </summary>
        /// <param name="input">Byte[] to hash</param>
        /// <param name="iterations">Rounds PBKDF2 will make to genarete the hash</param>
        /// <param name="length">Lenth of the hash, the output will also contains the salt</param>
        /// <returns>The generated salt+hash</returns>
        public static byte[] Hash(byte[] input, int iterations = defaultIterations, int length = defaultLength)
        {
            if (input == null)
                throw new ArgumentNullException("Could not hash input: Input is null.");
            else if (iterations <= 0)
                throw new ArgumentOutOfRangeException("Could not hash input: Iterations can't be negative or 0.");
            else if (length <= 0)
                throw new ArgumentOutOfRangeException("Could not hash input: Length can't be negative or 0.");

            //Create salt of 16 byte's.
            byte[] salt = new byte[16];
            new RNGCryptoServiceProvider().GetBytes(salt);

            //Create hash of [length] byte's.
            byte[] hash = new Rfc2898DeriveBytes(input, salt, iterations).GetBytes(length);

            //Combine salt and hash.
            byte[] combinedBytes = new byte[16 + length];
            Buffer.BlockCopy(salt, 0, combinedBytes, 0, 16);//Add salt.
            Buffer.BlockCopy(hash, 0, combinedBytes, 16, length);//Add hash.

            //Return combinedBytes;
            return combinedBytes;
        }

        /// <summary>
        /// Compare hashed string with another string.
        /// </summary>
        /// <param name="hashedInput">Hashed string</param>
        /// <param name="input">String to compare with hashedInput</param>
        /// <param name="iterations">Rounds PBKDF2 made to generate hashedInput</param>
        /// <returns>boolean, true if hashedInput is the same as input</returns>
        public static bool Verify(string hashedInput, string input, int iterations = defaultIterations)
        {
            if (string.IsNullOrEmpty(hashedInput))
                throw new ArgumentException("Could not hash input: HashedInput is empty.");
            else if (string.IsNullOrEmpty(input))
                throw new ArgumentException("Could not hash input: Input is empty.");
            else if (iterations <= 0)
                throw new ArgumentOutOfRangeException("Could not hash input: Iterations can't be negative or 0.");

            //Get byte's from hashedInput.
            byte[] hashedBytes = Convert.FromBase64String(hashedInput);

            //Get the length of the hash.
            int length = hashedBytes.Length - 16;

            //Get the salt from hashedBytes.
            byte[] salt = new byte[16];
            Buffer.BlockCopy(hashedBytes, 0, salt, 0, 16);

            //Generate hash of input, to compare it with hashedBytes
            byte[] hash = new Rfc2898DeriveBytes(input, salt, iterations).GetBytes(length);

            //Compare the result's.
            for (int i = 0; i < length; i++)
                if (hashedBytes[i + 16] != hash[i])
                    return false;

            return true;
        }

        /// <summary>
        /// Compare hashed byte[] with another byte[].
        /// </summary>
        /// <param name="hashedBytes">Hashed byte[]</param>
        /// <param name="input">byte[] to compare with hashedBytes</param>
        /// <param name="iterations">Rounds PBKDF2 made to generate hashedBytes</param>
        /// <returns>boolean, true if hashedBytes is the same as input</returns>
        public static bool Verify(byte[] hashedBytes, byte[] input, int iterations = defaultIterations)
        {
            if (hashedBytes == null)
                throw new ArgumentException("Could not hash input: hashedBytes is null.");
            else if (input == null)
                throw new ArgumentException("Could not hash input: Input is empty.");
            else if (iterations <= 0)
                throw new ArgumentOutOfRangeException("Could not hash input: Iterations can't be negative or null.");

            //Get the length of the hash.
            int length = hashedBytes.Length - 16;

            //Get the salt from HashedInputBytes.
            byte[] salt = new byte[16];
            Buffer.BlockCopy(hashedBytes, 0, salt, 0, 16);

            //Generate hash of Input, to compare it with hashedBytes.
            byte[] hash = new Rfc2898DeriveBytes(input, salt, iterations).GetBytes(length);

            //Compare the result's.
            for (int i = 0; i < length; i++)
                if (hashedBytes[i + 16] != hash[i])
                    return false;

            return true;
        }
    }

}

namespace Overstag.Security
{
    public static class TFA
    {
        public static string GenerateSecret(string token)
        {
            try
            {
                string secret = TwoFactor.GenerateSecret();
                using (var context = new OverstagContext())
                {
                    var a = context.Accounts.First(f => f.Token == token);
                    a.TwoFactor = secret;
                    context.Update(a);
                    context.SaveChanges();
                    return secret;
                }
            }
            catch
            {
                return null;
            }
        }

        public static string GetSecret(string token)
        {
            using (var context = new OverstagContext())
            {
                return context.Accounts.First(f => f.Token == token).TwoFactor;
            }
        }

        public static bool Disable(string token)
        {
            try
            {
                using (var context = new OverstagContext())
                {
                    var account = context.Accounts.First(f => f.Token == token);
                    account.TwoFactor = string.Empty;
                    context.Update(account);
                    context.SaveChanges();
                }
                return true;
            }
            catch
            {
                return false;
            }
        }

        public static bool Validate(string code,string token)
        {
            TwoFactor generator = new TwoFactor(GetSecret(token));
            generator.GenerateCode();
            return generator.ValidateCode(code);
        }

        public static string[] GetBackupCodes(string token, int amount = 10)
        {
            string secret = new OverstagContext().Accounts.First(f => f.Token == token).TwoFactor;
            List<string> Codes = new List<string>();
            for (int i = 0; i < amount; i++)
                Codes.Add(Encryption.PBKDF2.Hash(secret, 1234, 5));
            return Codes.ToArray();
        }

        public static bool RestoreBackupCode(string code, string token)
            =>  (Encryption.PBKDF2.Verify(code, new OverstagContext().Accounts.First(f => f.Token == token).TwoFactor, 1234));
    }
}
