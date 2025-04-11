using ShieldSec.Core.Analysis.PostProcessing;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tests.Core.Analysis
{
    /// <summary>
    ///  This is the test class for the SerialNumberChecker class.
    ///  
    ///  It uses "Arrange, Act, Assert" pattern to test the SerialNumberChecker class.
    /// </summary>
    [TestClass]
    public class SerialNumberCheckerTests
    {
        private string _tempDir = string.Empty;
        private string _originalCurrentDirectory = string.Empty;

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
        /// Creates a file with the given content.
        /// </summary>
        private void CreateFile(string fileName, string content)
        {
            File.WriteAllText(Path.Combine(_tempDir, fileName), content);
        }
        /// <summary>
        ///  Initializes the SerialNumberChecker and verifies that it populates the dictionary correctly.
        /// </summary>
        [TestMethod]
        public void Initialize_ValidFiles_PopulatesDictionary()
        {
            // Arrange
            string serialContent = "ABC123";
            string malwareContent = "TestMalware";
            CreateFile("malicious_serial_numbers.txt", serialContent);
            CreateFile("corresonding_malware_names.txt", malwareContent);

            // Act
            var checker = new SerialNumberChecker();

            // Assert
            string malwareName = checker.getMalwareName("ABC123");
            Assert.AreEqual("TestMalware", malwareName, "Expected malware name to match the one in the file.");
        }
        /// <summary>
        ///  Checks if the SerialNumberChecker handles empty files correctly.
        /// </summary>
        [TestMethod]
        public void Initialize_MismatchedFiles_LeavesEmptyDictionary()
        {
            // Arrange
            CreateFile("malicious_serial_numbers.txt", "ABC123\r\nDEF456");
            CreateFile("corresonding_malware_names.txt", "TestMalware");
            // Act
            var checker = new SerialNumberChecker();

            // Assert
            string malwareName = checker.getMalwareName("ABC123");
            Assert.AreEqual("N/A", malwareName, "Expected no entry due to mismatched file lines.");
        }
        /// <summary>
        ///  Checks if a file that is not signed (or has an invalid signature) returns false.
        /// </summary>
        /// <returns></returns>
        [TestMethod]
        public async Task IsFileMalicious_FileNotSigned_ReturnsFalse()
        {
            // Arrange
            string serialContent = "ABC123";
            string malwareContent = "TestMalware";
            CreateFile("malicious_serial_numbers.txt", serialContent);
            CreateFile("corresonding_malware_names.txt", malwareContent);
            var checker = new SerialNumberChecker();
            string dummyFileName = Path.Combine(_tempDir, "dummy.exe");
            File.WriteAllText(dummyFileName, "This is a dummy file without a valid signature.");

            // Act
            bool result = await checker.IsFileMalicious(dummyFileName);

            // Assert
            Assert.IsFalse(result, "A file that is not signed (or with an invalid signature) should return false.");
        }
        /// <summary>
        ///  Checks if an n
        /// </summary>
        /// <returns></returns>
        [TestMethod]
        public async Task IsFileMalicious_NonExistentFile_ReturnsFalse()
        {
            // Arrange
            string serialContent = "ABC123";
            string malwareContent = "TestMalware";
            CreateFile("malicious_serial_numbers.txt", serialContent);
            CreateFile("corresonding_malware_names.txt", malwareContent);
            var checker = new SerialNumberChecker();

            // Act
            string fakeFilePath = Path.Combine(_tempDir, "nonexistent.exe");
            bool result = await checker.IsFileMalicious(fakeFilePath);

            // Assert
            Assert.IsFalse(result, "A non-existent file should return false.");
        }

        [TestMethod]
        public void GetMalwareName_UnknownSerial_ReturnsNA()
        {
            // Arrange
            string serialContent = "ABC123";
            string malwareContent = "TestMalware";
            CreateFile("malicious_serial_numbers.txt", serialContent);
            CreateFile("corresonding_malware_names.txt", malwareContent);
            var checker = new SerialNumberChecker();

            // Act
            string result = checker.getMalwareName("UNKNOWN");

            // Assert
            Assert.AreEqual("N/A", result, "Unknown serial numbers should return 'N/A'.");
        }
    }
}
