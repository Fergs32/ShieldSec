using ShieldSec.Core.Managers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tests.Core.Managers
{
    /// <summary>
    ///  This is the test class for the ShieldRuleManager class.
    ///  
    ///  It uses "Arrange, Act, Assert" pattern to test the ShieldRuleManager class.
    /// </summary>
    [TestClass]
    public class ShieldRuleManagerTests
    {
        private List<string>? _tempFiles;
        private ShieldRuleManager? _manager;

        /// <summary>
        ///  Initializes the test environment before each test.
        /// </summary>
        [TestInitialize]
        public void TestInitialize()
        {
            _tempFiles = new List<string>();
            _manager = new ShieldRuleManager();
        }
        /// <summary>
        ///  Cleans up the test environment after each test.
        /// </summary>
        [TestCleanup]
        public void TestCleanup()
        {
            foreach (var file in _tempFiles)
            {
                try
                {
                    if (File.Exists(file))
                        File.Delete(file);
                }
                catch { /* ignore cleanup errors */ }
            }
        }
        /// <summary>
        /// Helper method to create a temporary file with the given content and extension.
        /// The file will be tracked and deleted at cleanup.
        /// </summary>
        private string CreateTempFile(string content, string extension)
        {
            var tempPath = Path.GetTempPath();
            var fileName = Guid.NewGuid().ToString() + extension;
            var fullPath = Path.Combine(tempPath, fileName);
            File.WriteAllBytes(fullPath, Encoding.UTF8.GetBytes(content));
            _tempFiles.Add(fullPath);
            return fullPath;
        }
        /// <summary>
        ///  Tests if the CheckMalwareAsync method matches the suspicious network pattern rule.
        /// </summary>
        /// <returns></returns>
        [TestMethod]
        public async Task CheckMalwareAsync_MatchesSuspiciousNetworkPattern()
        {
            // Arrange
            string content = "This file has a suspicious connection to 192.168.0.1.";
            string filePath = CreateTempFile(content, ".exe");

            // Act
            var matches = await _manager.CheckMalwareAsync(filePath);

            // Assert
            Assert.IsNotNull(matches);
            Assert.IsTrue(matches.Any(m => m.Rule.Name.Contains("Suspicious Network Patterns")),
                "Expected a match for the suspicious network pattern rule.");
        }
        /// <summary>
        ///  Checks if the CheckMalwareAsync method matches the suspicious process injection rule.
        /// </summary>
        /// <returns></returns>
        [TestMethod]
        public async Task CheckMalwareAsync_MatchesSuspiciousProcessInjection()
        {
            // Arrange
            string content = "This script calls CreateRemoteThread to inject code.";
            string filePath = CreateTempFile(content, ".ps1");

            // Act
            var matches = await _manager.CheckMalwareAsync(filePath);

            // Assert
            Assert.IsNotNull(matches);
            Assert.IsTrue(matches.Any(m => m.Rule.Name.Contains("Suspicious Process Injection")),
                "Expected a match for the suspicious process injection rule.");
        }
        /// <summary>
        ///   cHECKS if the CheckMalwareAsync method does not match for a non-high-risk extension.
        /// </summary>
        /// <returns></returns>
        [TestMethod]
        public async Task CheckMalwareAsync_NoMatchForNonHighRiskExtension()
        {
            // Arrange
            string content = "This file contains 192.168.0.1 and CreateRemoteThread but is .txt.";
            string filePath = CreateTempFile(content, ".txt");

            // Act
            var matches = await _manager.CheckMalwareAsync(filePath);

            // Assert
            Assert.IsNotNull(matches);
            Assert.AreEqual(0, matches.Count, "Expected no rule matches for non-high-risk extension.");
        }
        /// <summary>
        ///  Checks if the CheckMalwareAsync method matches multiple rules at once.
        /// </summary>
        /// <returns></returns>
        [TestMethod]
        public async Task CheckMalwareAsync_MultipleMatches()
        {
            // Arrange
            string content = "Connect to 10.0.0.5 and then call WriteProcessMemory.";
            string filePath = CreateTempFile(content, ".exe");

            // Act
            var matches = await _manager.CheckMalwareAsync(filePath);

            // Assert
            Assert.IsNotNull(matches);
            Assert.IsTrue(matches.Any(m => m.Rule.Name.Contains("Suspicious Network Patterns")),
                "Expected a match for the suspicious network pattern rule.");
            Assert.IsTrue(matches.Any(m => m.Rule.Name.Contains("Suspicious Process Injection")),
                "Expected a match for the suspicious process injection rule.");
        }
        /// <summary>
        ///  Checks if the CheckMalwareAsync method returns no matches for an empty file.
        /// </summary>
        /// <returns></returns>
        [TestMethod]
        public async Task CheckMalwareAsync_EmptyFile_ReturnsNoMatches()
        {
            // Arrange
            string content = "";
            string filePath = CreateTempFile(content, ".exe");

            // Act
            var matches = await _manager.CheckMalwareAsync(filePath);

            // Assert
            Assert.IsNotNull(matches);
            Assert.AreEqual(0, matches.Count, "Expected no matches for an empty file.");
        }
        /// <summary>
        ///  Checks if the CheckMalwareAsync method throws an exception for a non-existent file.
        /// </summary>
        /// <returns></returns>
        [TestMethod]
        [ExpectedException(typeof(FileNotFoundException))]
        public async Task CheckMalwareAsync_FileNotFound_ThrowsException()
        {
            // Arrange: Provide a non-existent file path.
            string filePath = Path.Combine(Path.GetTempPath(), Guid.NewGuid().ToString() + ".exe");

            // Act
            await _manager.CheckMalwareAsync(filePath);

            // Assert is handled by ExpectedException
            // If the exception is not thrown, the test will fail anyway.
        }
    }
}
