using ShieldSec.Core.Managers;
using System.Collections.Concurrent;
using System.Reflection;
using System.Security.Cryptography;
namespace Tests.Core.Managers
{
    /// <summary>
    ///  This class is responsible for testing the QuarantineManagerTests class.
    ///  It uses "Arrange, Act, Assert" pattern to test the QuarantineManagerTests class.
    ///  Which means that it first arranges the test environment, then acts on the test subject, and finally asserts the expected results.
    /// </summary>
    [TestClass]
    public class QuarantineManagerTests
    {
        private QuarantineManager _manager;
        private string _originalQuarantinePath;
        private string _tempQuarantinePath;

        /// <summary>
        ///  Initializes the test environment before each test.
        /// </summary>
        [TestInitialize]
        public void TestInitialize()
        {
            // this instance is set to null to avoid singleton pattern issues
            typeof(QuarantineManager)
                .GetField("_instance", BindingFlags.NonPublic | BindingFlags.Static)
                .SetValue(null, null);

            _manager = QuarantineManager.Instance;

            _originalQuarantinePath = (string)typeof(QuarantineManager)
                .GetField("_quarantinePath", BindingFlags.NonPublic | BindingFlags.Instance)
                .GetValue(_manager);

            _tempQuarantinePath = Path.Combine(Path.GetTempPath(), Guid.NewGuid().ToString());
            typeof(QuarantineManager)
                .GetField("_quarantinePath", BindingFlags.NonPublic | BindingFlags.Instance)
                .SetValue(_manager, _tempQuarantinePath);

            Directory.CreateDirectory(_tempQuarantinePath);
        }
        /// <summary>
        ///  Cleans up the test environment after each test.
        /// </summary>
        [TestCleanup]
        public void TestCleanup()
        {
            if (Directory.Exists(_tempQuarantinePath))
            {
                Directory.Delete(_tempQuarantinePath, true);
            }

            typeof(QuarantineManager)
                .GetField("_quarantinePath", BindingFlags.NonPublic | BindingFlags.Instance)
                .SetValue(_manager, _originalQuarantinePath);
        }
        /// <summary>
        ///  Checks if the the file is quarantined correctly, and if the file is moved to the quarantine path.
        /// </summary>
        [TestMethod]
        public void Quarantine_ValidFile_EncryptsAndMovesFile()
        {
            // Arrange
            var testFile = Path.GetTempFileName();
            File.WriteAllText(testFile, "Test content");

            // Act

            var result = _manager.Quarantine<AesCryptoServiceProvider>(testFile);

            // Assert
            Assert.IsTrue(result);

            var files = Directory.GetFiles(_tempQuarantinePath);
            Assert.AreEqual(1, files.Length);

            // if we dont cast here, then quaratinedFiles.Count cannot be accessed due to converting group to int (very stupid)
            var quarantinedFiles = (ConcurrentDictionary<string, QuarantinedFile>)typeof(QuarantineManager)
                .GetField("_files", BindingFlags.NonPublic | BindingFlags.Instance)
                .GetValue(_manager);

            Assert.AreEqual(1, quarantinedFiles.Count);
        }
        /// <summary>
        ///  Tests the logging functionality of the QuarantineManager, which appends the log entry to the log file.
        /// </summary>
        [TestMethod]
        public void LogAction_WhenCalled_AppendsToLogFile()
        {
            // Arrange
            var testMessage = "Test log entry";

            // Act
            _manager.LogAction(testMessage);

            // Assert
            var logPath = Path.Combine(_tempQuarantinePath, "log.txt");
            var logContent = File.ReadAllText(logPath);
            Assert.IsTrue(logContent.Contains(testMessage));
        }
        /// <summary>
        ///  Tests the SecureDelete method, which securely deletes a file by overwriting it with random data.
        /// </summary>
        [TestMethod]
        public void SecureDelete_ValidFile_RemovesFileSecurely()
        {
            // Arrange
            var testFile = Path.GetTempFileName();
            File.WriteAllText(testFile, "Test content");

            // Act
            typeof(QuarantineManager)
                .GetMethod("SecureDelete", BindingFlags.NonPublic | BindingFlags.Static)
                .Invoke(null, new[] { testFile });

            // Assert
            Assert.IsFalse(File.Exists(testFile));
        }
        /// <summary>
        ///  Tests an invalid file path, which should return false.
        /// </summary>
        [TestMethod]
        public void Quarantine_InvalidFile_ReturnsFalse()
        {
            // Act
            var result = _manager.Quarantine<AesCryptoServiceProvider>("nonexistent.file");

            // Assert
            Assert.IsFalse(result);
        }
    }
}
