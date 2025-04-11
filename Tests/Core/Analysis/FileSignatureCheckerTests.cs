using ShieldSec.Core.Analysis.Scanners;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace Tests.Core.Analysis
{
    /// <summary>
    ///  This is the test class for the FileSignatureScanner class.
    ///  
    ///  It uses "Arrange, Act, Assert" pattern to test the FileSignatureScanner class.
    /// </summary>
    [TestClass]
    public class FileSignatureScannerTests
    {
        private string _tempDir = string.Empty;
        private string _originalCurrentDirectory = string.Empty;
        private string _hashFilePath = string.Empty;
        private string _testFilePath = string.Empty;

        /// <summary>
        ///  Initializes the test environment before each test.
        /// </summary>
        [TestInitialize]
        public void TestInitialize()
        {
            _originalCurrentDirectory = Environment.CurrentDirectory;
            _tempDir = Path.Combine(Path.GetTempPath(), Guid.NewGuid().ToString());
            Directory.CreateDirectory(_tempDir);
            Environment.CurrentDirectory = _tempDir;
        }
        /// <summary>
        ///  Cleans up the test environment after each test.
        /// </summary>
        [TestCleanup]
        public void TestCleanup()
        {
            Environment.CurrentDirectory = _originalCurrentDirectory;
            try
            {
                if (Directory.Exists(_tempDir))
                {
                    Directory.Delete(_tempDir, true);
                }
            }
            catch { /* ignore cleanup errors */ }
        }
        /// <summary>
        /// Helper method to compute SHA256 hash for a given file.
        /// </summary>
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
        /// <summary>
        /// Creates a file with the specified content.
        /// </summary>
        private string CreateFile(string fileName, string content)
        {
            var fullPath = Path.Combine(_tempDir, fileName);
            File.WriteAllText(fullPath, content);
            return fullPath;
        }
        /// <summary>
        ///  Tests if the ScanFileAsync method returns true when the file hash matches a known hash.
        /// </summary>
        /// <returns></returns>
        [TestMethod]
        public async Task ScanFileAsync_HashMatches_ReturnsTrue()
        {
            // Arrange
            string fileContent = "This is a test file for signature scanning.";
            _testFilePath = CreateFile("testfile.txt", fileContent);
            string computedHash = await ComputeSHA256HashAsync(_testFilePath);
            _hashFilePath = CreateFile("hashes.txt", computedHash);

            var scanner = new FileSignatureScanner(_hashFilePath);

            // Act
            bool isMalicious = await scanner.ScanFileAsync(_testFilePath);

            // Assert
            Assert.IsTrue(isMalicious, "Expected ScanFileAsync to return true when file hash is in the known list.");
        }
        /// <summary>
        ///  Checks if the ScanFileAsync method returns false when the file hash does not match any known hash.
        /// </summary>
        /// <returns></returns>
        [TestMethod]
        public async Task ScanFileAsync_HashDoesNotMatch_ReturnsFalse()
        {
            // Arrange
            string fileContent = "This is another test file.";
            _testFilePath = CreateFile("testfile.txt", fileContent);
            string computedHash = await ComputeSHA256HashAsync(_testFilePath);
            string fakeHash = new string('0', computedHash.Length);
            _hashFilePath = CreateFile("hashes.txt", fakeHash);

            var scanner = new FileSignatureScanner(_hashFilePath);

            // Act
            bool isMalicious = await scanner.ScanFileAsync(_testFilePath);

            // Assert
            Assert.IsFalse(isMalicious, "Expected ScanFileAsync to return false when file hash is not in the known list.");
        }
        /// <summary>
        ///  Tests if the ScanFileAsync method returns false when the hash file is empty.
        /// </summary>
        /// <returns></returns>
        [TestMethod]
        public async Task ScanFileAsync_MissingHashFile_ReturnsFalse()
        {
            // Arrange
            _hashFilePath = Path.Combine(_tempDir, "nonexistent_hashes.txt");
            string fileContent = "Test content for file scanning.";
            _testFilePath = CreateFile("testfile.txt", fileContent);

            var scanner = new FileSignatureScanner(_hashFilePath);

            // Act
            bool isMalicious = await scanner.ScanFileAsync(_testFilePath);

            // Assert
            Assert.IsFalse(isMalicious, "Expected ScanFileAsync to return false when no known hashes are loaded.");
        }
        /// <summary>
        ///  Tests if the ScanFileAsync method returns false when the file does not exist.
        /// </summary>
        /// <returns></returns>
        [TestMethod]
        public async Task ScanFileAsync_FileDoesNotExist_ReturnsFalse()
        {
            // Arrange
            _hashFilePath = CreateFile("hashes.txt", "");
            var scanner = new FileSignatureScanner(_hashFilePath);

            // Act
            string nonExistentFile = Path.Combine(_tempDir, "nonexistentfile.txt");
            bool isMalicious = await scanner.ScanFileAsync(nonExistentFile);

            // Assert
            Assert.IsFalse(isMalicious, "Expected ScanFileAsync to return false when the file does not exist.");
        }
    }
}
