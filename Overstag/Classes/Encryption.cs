﻿using System;
using System.Text;
using System.Security.Cryptography;
using System.Collections.Generic;
using System.Linq;

namespace Overstag.Encryption
{
    public static class Random
    {
        private static System.Random rnd = new System.Random();
        public static string rString (int length)
        {
            const string chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789!@#$%^&*()_+";
            return new string(Enumerable.Repeat(chars, length)
              .Select(s => s[rnd.Next(s.Length)]).ToArray());
        }
        public static int rInt(int min, int max){return rnd.Next(min, max);}
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
        const string PEPPER = "ohgouhjkgukdftkfyjhjlG#*YJSJEIGE%UH(";
        const int DEFAULT_ITERATIONS = 50000;
        const int DEFAULT_LENGTH = 32;

        /// <summary>
        /// Hash a string with PBKDF2.
        /// </summary>
        /// <param name="Input">String to hash</param>
        /// <param name="Iterations">Rounds PBKDF2 will make to genarete the hash</param>
        /// <param name="Length">Lenth of the hash, the output will also contains the salt</param>
        /// <returns>The generated salt+hash</returns>
        public static string Hash(string Input, int Iterations = DEFAULT_ITERATIONS, int Length = DEFAULT_LENGTH)
        {
            if (string.IsNullOrEmpty(Input)) throw new ArgumentException("Could not hash input: Input is empty.");
            else if (Iterations <= 0) throw new ArgumentOutOfRangeException("Could not hash input: Iterations can't be negative or 0.");
            else if (Length <= 0) throw new ArgumentOutOfRangeException("Could not hash input: Length can't be negative or 0.");

            //Create salt of 16 byte's.
            byte[] Salt = new byte[16];
            new RNGCryptoServiceProvider().GetBytes(Salt);

            //Create hash of (Length) byte's.
            byte[] Hash = new Rfc2898DeriveBytes(PEPPER+Input, Salt, Iterations).GetBytes(Length);

            //Combine salt and hash.
            byte[] CombinedBytes = new byte[16 + Length];
            Buffer.BlockCopy(Salt, 0, CombinedBytes, 0, 16);
            Buffer.BlockCopy(Hash, 0, CombinedBytes, 16, Length);

            //Return CombinedBytes as base64 string.
            return Convert.ToBase64String(CombinedBytes);
        }

        /// <summary>
        /// Hash a byte[] with PBKDF2.
        /// </summary>
        /// <param name="Input">Byte[] to hash</param>
        /// <param name="Iterations">Rounds PBKDF2 will make to genarete the hash</param>
        /// <param name="Length">Lenth of the hash, the output will also contains the salt</param>
        /// <returns>The generated salt+hash</returns>
        public static byte[] Hash(byte[] Input, int Iterations = DEFAULT_ITERATIONS, int Length = DEFAULT_LENGTH)
        {
            if (Input == null) throw new ArgumentNullException("Could not hash input: Input is null.");
            else if (Iterations <= 0) throw new ArgumentOutOfRangeException("Could not hash input: Iterations can't be negative or 0.");
            else if (Length <= 0) throw new ArgumentOutOfRangeException("Could not hash input: Length can't be negative or 0.");

            //Create salt of 16 byte's.
            byte[] Salt = new byte[16];
            new RNGCryptoServiceProvider().GetBytes(Salt);

            //Create hash of 20 byte's.
            byte[] Hash = new Rfc2898DeriveBytes(Input, Salt, Iterations).GetBytes(Length);

            //Combine salt and hash.
            byte[] CombinedBytes = new byte[16 + Length];
            Buffer.BlockCopy(Salt, 0, CombinedBytes, 0, 16);
            Buffer.BlockCopy(Hash, 0, CombinedBytes, 16, Length);

            //Return CombinedBytes;
            return CombinedBytes;
        }

        /// <summary>
        /// Compare hashed string with another string.
        /// </summary>
        /// <param name="HashedInput">Hashed string</param>
        /// <param name="Input">String to compare with HashedInput</param>
        /// <param name="Iterations">Rounds PBKDF2 made to generate HashedInput</param>
        /// <returns>boolean, true if HashedInput is the same as Input</returns>
        public static bool Verify(string HashedInput, string Input, int Iterations = DEFAULT_ITERATIONS)
        {
            if (string.IsNullOrEmpty(Input)) throw new ArgumentException("Could not hash input: HashedInput is empty.");
            else if (string.IsNullOrEmpty(Input)) throw new ArgumentException("Could not hash input: Input is empty.");
            else if (Iterations <= 0) throw new ArgumentOutOfRangeException("Could not hash input: Iterations can't be negative or null.");

            //Get byte's from HashedInput.
            byte[] HashedBytes = Convert.FromBase64String(HashedInput);

            //Get the length of the hash.
            int Length = HashedBytes.Length - 16;
            Console.WriteLine(Length);

            //Get the salt from HashedInputBytes.
            byte[] Salt = new byte[16];
            Buffer.BlockCopy(HashedBytes, 0, Salt, 0, 16);

            //Generate hash of Input, to compare it with HashedInput
            byte[] Hash = new Rfc2898DeriveBytes(PEPPER + Input, Salt, Iterations).GetBytes(Length);

            //Compare the result's.
            for (int i = 0; i < Length; i++)
                if (HashedBytes[i + 16] != Hash[i]) return false;

            return true;
        }

        /// <summary>
        /// Compare hashed byte[] with another byte[].
        /// </summary>
        /// <param name="HashedInput">Hashed string</param>
        /// <param name="Input">String to compare with HashedInput</param>
        /// <param name="Iterations">Rounds PBKDF2 made to generate HashedInput</param>
        /// <returns>boolean, true if HashedInput is the same as Input</returns>
        public static bool Verify(byte[] HashedInput, byte[] Input, int Iterations = DEFAULT_ITERATIONS)
        {
            if (HashedInput == null) throw new ArgumentException("Could not hash input: HashedInput is empty.");
            else if (Input == null) throw new ArgumentException("Could not hash input: Input is empty.");
            else if (Iterations <= 0) throw new ArgumentOutOfRangeException("Could not hash input: Iterations can't be negative or null.");

            //Get byte's from HashedInput.
            byte[] HashedBytes = HashedInput;

            //Get the length of the hash.
            int Length = HashedBytes.Length - 16;
            Console.WriteLine(Length);

            //Get the salt from HashedInputBytes.
            byte[] Salt = new byte[16];
            Buffer.BlockCopy(HashedBytes, 0, Salt, 0, 16);

            //Generate hash of Input, to compare it with HashedInput
            byte[] Hash = new Rfc2898DeriveBytes(Input, Salt, Iterations).GetBytes(Length);

            //Compare the result's.
            for (int i = 0; i < Length; i++)
                if (HashedBytes[i + 16] != Hash[i]) return false;

            return true;
        }
    }
}
