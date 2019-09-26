﻿using System;
using System.Text;
using System.Security.Cryptography;
using TwoFactorAuthentication;
using Overstag.Models;
using System.Linq;
using System.Collections.Generic;
using System.IO;

namespace Overstag.Encryption
{
    //© 2019 MaurictG
    public static class Random
    {
        private static System.Random rnd = new System.Random();

        public static string rString(int length, string chars = "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789!@#$%^&*()_+")
        {
            StringBuilder sb = new StringBuilder();

            for (int i = 0; i < length; i++)
                sb.Append(chars[rInt(0, chars.Length - 1)]);

            return sb.ToString();
        }

        public static string rCode(int length)
            => rString(length, "0123456789");

        public static int rInt(int min, int max)
        {
            int result = BitConverter.ToInt32(rBytes(4), 0);
            return new System.Random(result).Next(min, max);
        }

        public static byte[] rBytes(int length)
        {
            byte[] buffer = new byte[length];
            new RNGCryptoServiceProvider().GetBytes(buffer);
            return buffer;
        }

        public static string rHash(string seed) =>
            PBKDF2.Hash(seed + rString(6)).Replace("/", "").Replace("?", "");
    }

    public static class SHA
    {
        public static string S256(string input)
        {
            var crypt = new SHA256Managed();
            var hash = new StringBuilder();
            byte[] crypto = crypt.ComputeHash(Encoding.UTF8.GetBytes(input));
            foreach (byte b in crypto)
            {
                hash.Append(b.ToString("x2"));
            }
            return hash.ToString();
        }
    }

    // © 2019 ghenkje (github.com/ghenkje) & MIT LICENCE
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
                    a.TwoFactorCodes = string.Join(',',TFA.GenerateBackupCodes());
                    context.Accounts.Update(a);
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
                    account.TwoFactorCodes = string.Empty;
                    context.Accounts.Update(account);
                    context.SaveChanges();
                }
                return true;
            }
            catch
            {
                return false;
            }
        }

        public static bool Validate(string code,string token, DateTime now)
        {
            TwoFactor generator = new TwoFactor(GetSecret(token));
            //generator.GenerateCode(now);
            return generator.ValidateCode(code,now.ToUniversalTime());
        }

        public static string[] GenerateBackupCodes(int amount = 10)
        {
            using(var context = new OverstagContext())
            {
                List<string> Codes = new List<string>();
                for (int i = 0; i < amount; i++)
                    Codes.Add(Encryption.Random.rCode(10));

                return Codes.ToArray();
            }
        }

        public static string[] GetBackupCodes(string token)
            => new OverstagContext().Accounts.First(f => f.Token == token).TwoFactorCodes.Split(",");

        public static bool RestoreBackupCode(string code, string token)
        {
            using(var context = new OverstagContext())
            {
                var a = context.Accounts.First(f => f.Token == token);
                if ((a.TwoFactorCodes.Contains(code) && !code.Contains(",") && code.Length == a.TwoFactorCodes.Split(",")[0].Length))
                {
                    a.TwoFactor = string.Empty;
                    a.TwoFactorCodes = string.Empty;
                    context.Accounts.Update(a);
                    context.SaveChanges();
                    return true;
                }
                else
                    return false;
            }
        }
    }

    public class Backup
    {
        private string Folder = Path.Combine(Environment.CurrentDirectory, "Backup");

        int _errors = 0;
        List<Exception> _exceptions = new List<Exception>();

        private List<Account> Accounts { get; set; }
        private List<Event> Events { get; set; }
        private List<Invoice> Invoices { get; set; }
        private List<Logging> Logging { get; set; }
        private List<Family> Families { get; set; }
        private List<Idea> Ideas { get; set; }
        private List<Payment> Payments { get; set; }
        private List<Ticket> Tickets { get; set; }


        public Backup(string folder)
        {
            if(folder != string.Empty)
                Folder = folder;

            if (!Directory.Exists(Folder))
                Directory.CreateDirectory(Folder);
        }

        public void Create(ref int errorcount, ref List<Exception> exceptions)
        {
            using(var context = new OverstagContext())
            {
                Accounts = context.Accounts.ToList();
                Write(Accounts,"Accounts");
                Accounts.Clear();
                Accounts = null;
                GC.Collect();

                Events = context.Events.ToList();
                Write(Events, "Events");
                Events.Clear();
                Events = null;
                GC.Collect();

                Invoices = context.Invoices.ToList();
                Write(Invoices, "Invoices");
                Invoices.Clear();
                Invoices = null;
                GC.Collect();

                Logging = context.Logging.ToList();
                Write(Logging, "Logging");
                Logging.Clear();
                Logging = null;
                GC.Collect();

                Families = context.Families.ToList();
                Write(Families, "Families");
                Families.Clear();
                Families = null;
                GC.Collect();

                Ideas = context.Ideas.ToList();
                Write(Ideas, "Ideas");
                Ideas.Clear();
                Ideas = null;
                GC.Collect();

                Payments = context.Payments.ToList();
                Write(Payments, "Payments");
                Payments.Clear();
                Payments = null;
                GC.Collect();

                Tickets = context.Tickets.ToList();
                Write(Tickets, "Tickets");
                Tickets.Clear();
                Tickets = null;
                GC.Collect();
            }

            errorcount = _errors;
            exceptions = _exceptions;
        }

        public void Write(Object obj, string objname)
        {
            try
            {
                string json = Newtonsoft.Json.JsonConvert.SerializeObject(obj);
                string path = Path.Combine(Folder, objname + ".json");
                File.WriteAllText(path, json);
                Console.WriteLine("Saved " + objname);
            }
            catch(Exception e)
            {
                _errors+=1;
                _exceptions.Add(e);
            }
        }
    }


}
