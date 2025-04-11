using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace ShieldSec.Core.Managers
{
    // Singleton pattern reference: 
    // https://refactoring.guru/design-patterns/singleton
    /// <summary>
    ///  Handles quarantining files and logging actions.
    /// </summary>
    public sealed class QuarantineManager
    {
        private static QuarantineManager? _instance;
        public static QuarantineManager Instance => _instance ??= new QuarantineManager();
        private readonly ConcurrentDictionary<string, QuarantinedFile> _files;
        private readonly string _quarantinePath;

        /// <summary>
        ///  Private constructor for the Quarantine Manager class.
        /// </summary>
        private QuarantineManager()
        {
            _quarantinePath = Path.Combine(Environment.GetFolderPath(
                Environment.SpecialFolder.ApplicationData), "ShieldSecQuarantine");
            Directory.CreateDirectory(_quarantinePath);
            _files = new ConcurrentDictionary<string, QuarantinedFile>();
        }

        // AES encryption reference:
        // https://learn.microsoft.com/en-us/dotnet/api/system.security.cryptography.aes
        /// <summary>
        ///  Handles quarantining a file by encrypting it and moving it to the quarantine folder.
        /// </summary>
        /// <typeparam name="T"> The symmetric algorithm to use for encryption.</typeparam>
        /// <param name="filePath"> The path of the file to quarantine.</param>
        /// <returns> True if the file was successfully quarantined, false otherwise.</returns>
        public bool Quarantine<T>(string filePath) where T : SymmetricAlgorithm, new()
        {
            try
            { 
                var quarantineId = Guid.NewGuid().ToString();
                var newPath = Path.Combine(_quarantinePath, quarantineId);

                using var algorithm = new T();
                EncryptFile(filePath, newPath, algorithm.Key, algorithm.IV);

                _files.TryAdd(quarantineId, new QuarantinedFile(filePath));
                SecureDelete(filePath);
                return true;
            }
            catch { return false; }
        }
        /// <summary>
        ///  Encrypts a file using the AES algorithm.
        /// </summary>
        /// <param name="input"> The path of the file to encrypt.</param>
        /// <param name="output"> The path of the encrypted file.</param>
        /// <param name="key"> The encryption key.</param>
        /// <param name="iv"> The initialization vector. Example: [0, 1, 2, 3]</param>
        private void EncryptFile(string input, string output, byte[] key, byte[] iv)
        {
            using var encryptor = Aes.Create().CreateEncryptor(key, iv);
            using var fsOut = new FileStream(output, FileMode.Create);
            using var cryptoStream = new CryptoStream(fsOut, encryptor, CryptoStreamMode.Write);
            cryptoStream.Write(File.ReadAllBytes(input));
        }

        // References:
        // https://en.wikipedia.org/wiki/Gutmann_method
        // https://www.nist.gov/publications/guidelines-media-sanitization
        // https://nvlpubs.nist.gov/nistpubs/SpecialPublications/NIST.SP.800-88r1.pdf
        /// <summary>
        ///  This method securely deletes a file by overwriting it with random data.
        ///  This was coded following the NIST guidelines for media sanitization.
        /// </summary>
        /// <param name="path"> The path of the file to delete.</param>
        private static void SecureDelete(string path)
        {
            try
            {
                const int passes = 1;
                var fileInfo = new FileInfo(path);
                long length = fileInfo.Length;

                using (var fs = new FileStream(path, FileMode.Open, FileAccess.Write, FileShare.None))
                {
                    var random = new Random();
                    byte[] randomData = new byte[length];

                    for (int i = 0; i < passes; i++)
                    {
                        fs.Position = 0;
                        random.NextBytes(randomData);
                        fs.Write(randomData, 0, randomData.Length);
                        fs.Flush();
                    }
                }

                File.Delete(path);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Secure deletion failed: {ex.Message}");
            }
        }
        /// <summary>
        /// Logs an action to the quarantine log file.
        /// </summary>
        /// <typeparam name="T"> The type of the message to log.</typeparam>
        /// <param name="message"> The message to log.</param>
        public void LogAction<T>(T message)
        {
            File.AppendAllText(
                Path.Combine(_quarantinePath, "log.txt"),
                $"{DateTime.Now}: {message}\n"
            );

            // we now need to create a local file in the executed directory to keep logs
            File.WriteAllText(
                               Path.Combine(Environment.CurrentDirectory, "qurantine-log.txt"),
                                              $"{DateTime.Now}: {message}\n"
                                                         );
        }
    }
    /// <summary>
    ///  Represents a quarantined file.
    /// </summary>
    /// <param name="OriginalPath"> The original path of the quarantined file.</param>
    public record QuarantinedFile(string OriginalPath)
    {
        public DateTime QuarantineDate { get; } = DateTime.Now;
    }
}
