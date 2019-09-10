using System;
using System.IO;
using System.Text;
using System.Security.Cryptography;

namespace Overstag.Classes.Cryptography
{
    public class Encryption
    {
        /// <summary>
        /// Algorithm for encryption and decryption data.
        /// </summary> 
        private readonly SymmetricAlgorithm algorithm;

        /// <summary>
        /// Create class with an already set up algorithm.
        /// </summary>
        /// <param name="algorithm">Algorithm wich is alreay set up properly</param>
        public Encryption(SymmetricAlgorithm algorithm)
            => this.algorithm = algorithm ?? throw new ArgumentException("Invalid algorithm, algorithm is null.");
        /// <summary>
        /// Create class with an algorithm and overide the key for the selected algorithm.
        /// </summary>
        /// <param name="algorithm">New algorithm</param>
        /// <param name="key">Generated key</param>
        public Encryption(SymmetricAlgorithm algorithm, byte[] key)
        {
            this.algorithm = algorithm ?? throw new ArgumentException("Invalid algorithm, algorithm is null.");
            if (key == null) throw new ArgumentException("Invalid key, key is null.");
            if (!this.algorithm.ValidKeySize(key.Length * 8)) throw new ArgumentException("Invalid key, key has an invalid size.");
            this.algorithm.Key = key;
        }
        /// <summary>
        /// Create class and create a new key for the passed algorithm.
        /// </summary>
        /// <param name="algorithm">New algorithm</param>
        /// <param name="password">Password, used to generate key</param>
        /// <param name="salt">Salt, used to make generated key more random(min 8 characters)</param>
        /// <param name="iterations">Rounds PBKDF2 will make to genarete a key</param>
        public Encryption(SymmetricAlgorithm algorithm, string password, string salt, int iterations = 10000)
        {
            this.algorithm = algorithm ?? throw new ArgumentException("Invalid algorithm, algorithm is null.");
            this.algorithm.Key = CreateKey(this.algorithm, password, salt, iterations);
        }
        /// <summary>
        /// Create class and create a new key for the passed algorithm with a fixed keysize.
        /// </summary>
        /// <param name="algorithm">new algorithm</param>
        /// <param name="keySize">Keysize in bits(8 bits = 1 byte)</param>
        /// <param name="password">Password, used to generate key</param>
        /// <param name="salt">Salt, used to make generated key more random(min 8 characters)</param>
        /// <param name="iterations">Rounds PBKDF2 will make to genarete a key</param>
        public Encryption(SymmetricAlgorithm algorithm, int keySize, string password, string salt, int iterations = 10000)
        {
            this.algorithm = algorithm ?? throw new ArgumentException("Invalid algorithm, algorithm is null.");
            if (!this.algorithm.ValidKeySize(keySize)) throw new ArgumentException("Invalid key, key has an invalid size.");
            this.algorithm.Key = CreateKey(keySize, password, salt, iterations);
        }

        /// <summary>
        /// Encrypt a string and decode with UTF8.
        /// </summary>
        /// <param name="text">Text to encrypt</param>
        /// <returns>Encrypted text(base64 string)</returns>
        public string Encrypt(string text)
            => Convert.ToBase64String(Encrypt(Encoding.UTF8.GetBytes(text ?? throw new ArgumentException("Could not decrypt text: Text can't be null."))));
        /// <summary>
        /// Encrypt a string.
        /// </summary>
        /// <param name="text">Text to encrypt</param>
        /// <param name="encoder">Encoding used to convert string to byte[]</param>
        /// <returns>Encrypted text(base64 string)</returns>
        public string Encrypt(string text, Encoding encoder)
            => Convert.ToBase64String(Encrypt(encoder.GetBytes(text ?? throw new ArgumentException("Could not decrypt text: Text can't be null."))));
        /// <summary>
        /// Encrypt a byte[].
        /// </summary>
        /// <param name="data">Data to encrypt</param>
        /// <returns>Encrypted data(byte[])</returns>
        public byte[] Encrypt(byte[] data)
        {
            if (data == null) throw new Exception("Could not encrypt data: Data can't be null.");

            algorithm.GenerateIV();//Genarate new random IV.

            using (MemoryStream ms = new MemoryStream())
            {
                ms.Write(algorithm.IV, 0, algorithm.IV.Length);//Write IV to ms(first 16 bytes)
                using (CryptoStream cs = new CryptoStream(ms, algorithm.CreateEncryptor(algorithm.Key, algorithm.IV), CryptoStreamMode.Write))
                {
                    cs.Write(data, 0, data.Length);
                    cs.FlushFinalBlock();
                }
                return ms.ToArray();
            }
        }

        /// <summary>
        /// Decrypt a string and decode with UTF8.
        /// </summary>
        /// <param name="text">Text to decrypt</param>
        /// <returns>Decrypted text(string encoded with UTF8)</returns>
        public string Decrypt(string text)
            => Encoding.UTF8.GetString(Decrypt(Convert.FromBase64String(text ?? throw new ArgumentException("Could not decrypt text: Text can't be null."))));
        /// <summary>
        /// Decrypt a string.
        /// </summary>
        /// <param name="text">Text to decrypt</param>
        /// <param name="encoder">Encoding used to convert byte[] to string</param>
        /// <returns>Decrypted text(string encoded with Encoder)</returns>
        public string Decrypt(string text, Encoding encoder)
            => encoder.GetString(Decrypt(Convert.FromBase64String(text ?? throw new ArgumentException("Could not decrypt text: Text can't be null."))));
        /// <summary>
        /// Decrypt byte[].
        /// </summary>
        /// <param name="data">Data to decrypt</param>
        /// <returns>Decrypted data</returns>
        public byte[] Decrypt(byte[] data)
        {
            if (data == null) throw new ArgumentException("Could not encrypt data: Data can't be null.");

            using (MemoryStream ms = new MemoryStream(data))
            {
                byte[] IV = new byte[algorithm.IV.Length];
                ms.Read(IV, 0, IV.Length);//Get IV from ms(first 16 bytes)
                algorithm.IV = IV;

                using (CryptoStream cs = new CryptoStream(ms, algorithm.CreateDecryptor(algorithm.Key, algorithm.IV), CryptoStreamMode.Read))
                {
                    byte[] decrypted = new byte[data.Length];
                    int byteCount = cs.Read(decrypted, 0, data.Length);
                    return new MemoryStream(decrypted, 0, byteCount).ToArray();
                }
            }
        }

        /// <summary>
        /// Return the current key.
        /// </summary>
        /// <returns>Encryption key</returns>
        public byte[] GetKey()
            => algorithm.Key;

        /*static members*/
        /// <summary>
        /// Create a new encryption key with PBKDF2 and a selected keysize.
        /// </summary>
        /// <param name="keySize">Keysize in bits(8 bits = 1 byte)</param>
        /// <param name="password">Password, used to generate key</param>
        /// <param name="salt">Salt, used to make generated key more random(min 8 characters)</param>
        /// <param name="iterations">Rounds PBKDF2 will make to genarete a key</param>
        /// <returns>Encryption key</returns>
        public static byte[] CreateKey(int keySize, string password, string salt, int iterations = 10000)
        {
            if (keySize <= 0) throw new ArgumentException("Could not create key: Invalid KeySize.");
            else if (salt.Length < 8) throw new ArgumentException("Could not create key: Salt is to short.");
            else if (string.IsNullOrEmpty(password)) throw new ArgumentException("Could not create key: Password can't be empty.");
            else if (iterations <= 0) throw new ArgumentException("Could not create key: Invalid Iterations count.");

            return new Rfc2898DeriveBytes(password,
                Encoding.UTF8.GetBytes(salt),//Convert salt to byte[]
                iterations).GetBytes(keySize / 8);
        }
        /// <summary>
        /// Create a new encryption key with PBKDF2 for the selected Algorithm.
        /// </summary>
        /// <param name="algorithm">Algorithm is used to get the keysize</param>
        /// <param name="password">Password, used to generate key</param>
        /// <param name="salt">Salt, used to make generated key more random(min 8 characters)</param>
        /// <param name="iterations">Rounds PBKDF2 will make to genarete a key</param>
        /// <returns>Encryption key</returns>
        public static byte[] CreateKey(SymmetricAlgorithm algorithm, string password, string salt, int iterations = 10000)
            => CreateKey(algorithm.LegalKeySizes[0].MaxSize, password, salt, iterations);
    }

    public class FileEncryption
    {
        /// <summary>
        /// Algorithm for encryption and decryption data.
        /// </summary>
        private readonly SymmetricAlgorithm algorithm;

        /// <summary>
        /// Create class with an already set up algorithm.
        /// </summary>
        /// <param name="algorithm">Algorithm wich is alreay set up properly</param>
        public FileEncryption(SymmetricAlgorithm algorithm)
            => this.algorithm = algorithm ?? throw new ArgumentException("Invalid algorithm, algorithm is null.");
        /// <summary>
        /// Create class with an algorithm and overide the key for the selected algorithm.
        /// </summary>
        /// <param name="algorithm">new algorithm</param>
        /// <param name="key">Generated key</param>
        public FileEncryption(SymmetricAlgorithm algorithm, byte[] key)
        {
            this.algorithm = algorithm ?? throw new ArgumentException("Invalid algorithm, algorithm is null.");
            if (key == null) throw new ArgumentException("Invalid key, key is null.");
            if (!this.algorithm.ValidKeySize(key.Length * 8)) throw new ArgumentException("Invalid key, key has an invalid size.");
            this.algorithm.Key = key;
        }
        /// <summary>
        /// Create class and create a new key for the passed algorithm.
        /// </summary>
        /// <param name="algorithm">New algorithm</param>
        /// <param name="password">Password, used to generate key</param>
        /// <param name="salt">Salt, used to make generated key more random(min 8 characters)</param>
        /// <param name="iterations">Rounds PBKDF2 will make to genarete a key</param>
        public FileEncryption(SymmetricAlgorithm algorithm, string password, string salt, int iterations = 10000)
        {
            this.algorithm = algorithm ?? throw new ArgumentException("Invalid algorithm, algorithm is null.");
            this.algorithm.Key = Encryption.CreateKey(this.algorithm, password, salt, iterations);
        }
        /// <summary>
        /// Create class and create a new key for the passed algorithm with a fixed keysize.
        /// </summary>
        /// <param name="algorithm">new algorithm</param>
        /// <param name="keySize">Keysize in bits(8 bits = 1 byte)</param>
        /// <param name="password">Password, used to generate key</param>
        /// <param name="salt">Salt, used to make generated key more random(min 8 characters)</param>
        /// <param name="iterations">Rounds PBKDF2 will make to genarete a key</param>
        public FileEncryption(SymmetricAlgorithm algorithm, int keySize, string password, string salt, int iterations = 10000)
        {
            this.algorithm = algorithm ?? throw new ArgumentException("Invalid algorithm, algorithm is null.");
            if (!this.algorithm.ValidKeySize(keySize)) throw new ArgumentException("Invalid key, key has an invalid size.");
            this.algorithm.Key = Encryption.CreateKey(keySize, password, salt, iterations);
        }

        /// <summary>
        /// Encrypt a file.
        /// </summary>
        /// <param name="inputFile">Path of the plain file</param>
        /// <param name="outputFile">Location to save encrypted file</param>
        /// <param name="bufferSize">BufferSize for reading the inputfile(Default = 1MB)</param>
        public void Encrypt(string inputFile, string outputFile, int bufferSize = 1048576)
        {
            if (!File.Exists(inputFile)) throw new ArgumentException("Could not encrypt file: File not found.");
            else if (File.Exists(outputFile)) throw new ArgumentException("Could not encrypt file: File already exists.");

            using (FileStream outputStream = new FileStream(outputFile, FileMode.Create))
            {
                algorithm.GenerateIV();//Genarate new random IV.
                outputStream.Write(algorithm.IV, 0, algorithm.IV.Length);//Write IV to the first bytes(16) of the file.

                using (CryptoStream cs = new CryptoStream(outputStream, algorithm.CreateEncryptor(), CryptoStreamMode.Write))
                {
                    using (FileStream inputStream = new FileStream(inputFile, FileMode.Open))
                    {
                        byte[] buffer = new byte[bufferSize];
                        int read;

                        while ((read = inputStream.Read(buffer, 0, buffer.Length)) > 0)
                            cs.Write(buffer, 0, read);
                    }
                }
            }
        }

        /// <summary>
        /// Decrypt a file.
        /// </summary>
        /// <param name="inputFile">Path of the encrypted file</param>
        /// <param name="outputFile">Location to save decrypted file</param>
        /// <param name="bufferSize">BufferSize for reading the inputfile(Default = 1MB)</param>
        public void Decrypt(string inputFile, string outputFile, int bufferSize = 1048576)
        {
            if (!File.Exists(inputFile)) throw new ArgumentException("Could not decrypt file: File not found.");
            else if (File.Exists(outputFile)) throw new ArgumentException("Could not decrypt file: File already exists.");

            using (FileStream inputStream = new FileStream(inputFile, FileMode.Open))
            {
                byte[] IV = new byte[algorithm.IV.Length];
                inputStream.Read(IV, 0, IV.Length);//Read the IV from the first bytes(16) of the file.
                algorithm.IV = IV;

                using (CryptoStream cs = new CryptoStream(inputStream, algorithm.CreateDecryptor(), CryptoStreamMode.Read))
                {
                    using (FileStream outputStream = new FileStream(outputFile, FileMode.Create))
                    {
                        byte[] buffer = new byte[bufferSize];
                        int read;

                        while ((read = cs.Read(buffer, 0, buffer.Length)) > 0)
                            outputStream.Write(buffer, 0, read);
                    }
                }
            }
        }

        /// <summary>
        /// Return the current key.
        /// </summary>
        /// <returns>Encryption key</returns>
        public byte[] GetKey()
            => algorithm.Key;
    }
}
