using ShieldSec.Core.Listeners;
using Moq;
using ShieldSec.Core.Interfaces;

namespace Tests.Core.Listeners
{
    /// <summary>
    ///  This class is responsible for testing the FileMonitorListener class.
    ///  It uses "Arrange, Act, Assert" pattern to test the FileMonitorListener class.
    ///  Which means that it first arranges the test environment, then acts on the test subject, and finally asserts the expected results.
    /// </summary>
    [TestClass]
    public class FileMonitorListenerTests
    {
        private FileMonitorListener? _listener;
        private Mock<IScanManager>? _mockScanManager;
        private string? _testDirectory;

        /// <summary>
        ///  Initializes the test environment before each test.
        /// </summary>
        [TestInitialize]
        public void Setup()
        {
            _mockScanManager = new Mock<IScanManager>();
            _listener = new FileMonitorListener();
            _testDirectory = Path.Combine(Path.GetTempPath(), Guid.NewGuid().ToString());
            Directory.CreateDirectory(_testDirectory);
        }
        /// <summary>
        ///  Cleans up the test environment after each test.
        /// </summary>
        [TestCleanup]
        public void Cleanup()
        {
            _listener?.Dispose();
            Directory.Delete(_testDirectory, true); // true is for recursive deletion
        }

        /// <summary>
        ///  Creates a test file and verifies that the FileVerified event is triggered when a valid file is created.
        /// </summary>
        /// <returns></returns>
        [TestMethod]
        public async Task FileChanged_ValidFile_TriggersFileVerifiedEvent()
        {
            var testFilePath = Path.Combine(_testDirectory, "safe_file.txt");
            bool eventFired = false;
            _listener.FileVerified += (sender, path) => eventFired = true;

            _mockScanManager?
                .Setup(m => m.ScanFileAndGetResultAsync(testFilePath))
                .ReturnsAsync(true);

            _listener?.StartMonitoring(_mockScanManager.Object, _testDirectory);

            File.WriteAllText(testFilePath, "Test content");
            await WaitForEventAsync(() => eventFired, 3000);

            Assert.IsTrue(eventFired);
        }
        /// <summary>
        ///  Checks if the FileUnverified event is triggered when a threat file is created.
        /// </summary>
        /// <returns></returns>
        [TestMethod]
        public async Task FileChanged_ThreatFile_TriggersFileUnverifiedEvent()
        {
            // Arrange
            var testFilePath = Path.Combine(_testDirectory, "malware.exe");
            bool eventFired = false;
            _listener.FileUnverified += (sender, path) => eventFired = true;

            _mockScanManager?
                .Setup(m => m.ScanFileAndGetResultAsync(testFilePath))
                .ReturnsAsync(false);

            _listener.StartMonitoring(_mockScanManager.Object, _testDirectory);

            File.WriteAllText(testFilePath, "Malicious content");
            await WaitForEventAsync(() => eventFired, 3000);

            // Assert
            Assert.IsTrue(eventFired, "FileUnverified event should be triggered");
        }
        /// <summary>
        ///  Checks if the temp file creation does not trigger any events.
        /// </summary>
        /// <returns></returns>
        [TestMethod]
        public async Task FileChanged_TemporaryFile_NoEventsTriggered()
        {
            // Arrange
            var tempFilePath = Path.Combine(_testDirectory, "temp.tmp");
            bool verifiedEventFired = false;
            bool unverifiedEventFired = false;
            _listener.FileVerified += (sender, path) => verifiedEventFired = true;
            _listener.FileUnverified += (sender, path) => unverifiedEventFired = true;

            // Act
            _listener.StartMonitoring(_mockScanManager.Object, _testDirectory);
            await SimulateFileChange(tempFilePath);

            // Assert
            Assert.IsFalse(verifiedEventFired, "No events should fire for temp files");
            Assert.IsFalse(unverifiedEventFired, "No events should fire for temp files");
            _mockScanManager.Verify(m => m.ScanFileAndGetResultAsync(It.IsAny<string>()), Times.Never);
        }
        /// <summary>
        ///  Simulates a file change by creating a file with the specified path.
        /// </summary>
        /// <param name="filePath"></param>
        /// <returns></returns>
        private async Task SimulateFileChange(string filePath)
        {
            File.WriteAllText(filePath, "Test content");
            //await Task.Delay(1000); // have to put 1 second here, because sometimes the event is not triggered
        }
        /// <summary>
        ///  Helper method to wait for an event to be triggered.
        /// </summary>
        /// <param name="condition"> the condition to check</param>
        /// <param name="timeoutMs"> the timeout in milliseconds</param>
        /// <returns></returns>
        private async Task WaitForEventAsync(Func<bool> condition, int timeoutMs)
        {
            var startTime = DateTime.UtcNow;
            while (!condition() && (DateTime.UtcNow - startTime).TotalMilliseconds < timeoutMs)
            {
                await Task.Delay(100);
            }
        }
    }
}
