using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace ShieldSec.Core.Analysis.Scanners
{
    /// <summary>
    ///  This class is responsible for scanning files for known file signatures
    /// </summary>
    public class FileSignatureScanner
    {
        // https://bazaar.abuse.ch/export/
        private readonly HashSet<string> knownHashes;
        /// <summary>
        ///  Constructor for the File Signature Scanner class
        /// </summary>
        /// <param name="hashFilePath"> The path to the file containing known file signatures </param>
        public FileSignatureScanner(string hashFilePath)
        {
            knownHashes = new HashSet<string>();
            Initialize(hashFilePath);
        }
        /// <summary>
        ///  Initializes the scanner by loading known file signatures from disk
        /// </summary>
        /// <param name="hashFilePath"> The path to the file containing known file signatures </param>
        private void Initialize(string hashFilePath)
        {
            if (!File.Exists(hashFilePath)) return;

            using (var reader = new StreamReader(hashFilePath))
            {
                string line;
                while ((line = reader.ReadLine()) != null)
                {
                    knownHashes.Add(line.Trim());
                }
            }
        }
        /// <summary>
        ///  Scans a file for known signatures
        /// </summary>
        /// <param name="filePath"> The path to the file to scan </param>
        /// <returns> True if the file is known to be malicious, false otherwise </returns>
        /// <exception cref="FileNotFoundException"> Thrown when the file does not exist </exception>
        public async Task<bool> ScanFileAsync(string filePath)
        {
            if (!File.Exists(filePath)) return false;

            var fileHash = await ComputeSHA256HashAsync(filePath);
            return knownHashes.Contains(fileHash);
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="filePath"></param>
        /// <returns></returns>
        private async Task<string> ComputeSHA256HashAsync(string filePath)
        {
            using (var sha256 = SHA256.Create())
            {
                using (var stream = File.OpenRead(filePath))
                {
                    var hash = await Task.Run(() => sha256.ComputeHash(stream));
                    return BitConverter.ToString(hash).Replace("-", "").ToLowerInvariant();
                }
            }
        }
    }
}
