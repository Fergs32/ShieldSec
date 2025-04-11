using Moq;
using ShieldSec.Core.Managers;
using System.IO.Abstractions.TestingHelpers;
using System.Reflection;

namespace Tests.Core.Managers
{
    /// <summary>
    ///  This is the test class for the FileLoader class.
    ///  -- 
    ///  This class structrue is based on the "Arrange, Act, Assert" pattern.
    /// </summary>
    [TestClass]
    public class FileLoaderTests
    {
        private MockFileSystem _fileSystem;
        private FileLoader _loader;
        private Mock<IProgress<int>> _progressMock;
        private string _testDrivePath;

        /// <summary>
        ///  Initializes the test environment before each test.
        /// </summary>
        [TestInitialize]
        public void TestInitialize()
        {
            _fileSystem = new MockFileSystem();
            _progressMock = new Mock<IProgress<int>>();
            _testDrivePath = "C:\\";

            CreateTestFileStructure();

            _loader = new FileLoader();
        }
        /// <summary>
        ///  Creates a test file structure with various files.
        /// </summary>
        private void CreateTestFileStructure()
        {
            var validContent = new byte[5000];

            _fileSystem.AddFile(Path.Combine(_testDrivePath, "valid.exe"),
                new MockFileData(validContent));

            _fileSystem.AddFile(Path.Combine(_testDrivePath, "Docs", "doc.docm"),
                new MockFileData(validContent));

            _fileSystem.AddFile(Path.Combine(_testDrivePath, "Scripts", "script.ps1"),
                new MockFileData(validContent));
        }
        /// <summary>
        ///  Loads all files asynchronously and verifies that only valid files are returned. (This takes a while)
        /// </summary>
        /// <returns></returns>
        [TestMethod]
        public async Task LoadAllFilesAsync_ReturnsOnlyValidFiles()
        {
            // Act
            var result = await _loader.LoadAllFilesAsync(_progressMock.Object, CancellationToken.None);


            // Assert
            Assert.IsNotNull(result);
            Assert.IsTrue(result.Count > 0, "The file loader should return at least one file.");
        }
        /// <summary>
        ///  Checks if the file loader includes a file based on its path. 
        ///  Why is this a test? Well, the file loader should only include files that are not in excluded directories or have certain extensions.
        /// </summary>
        [TestMethod]
        public void ShouldIncludeFile_ExcludedDirectory_ReturnsFalse()
        {
            // Arrange
            var path = Path.Combine(_testDrivePath, "Windows", "system.dll");

            // Act
            var result = CallShouldIncludeFile(path);

            // Assert
            Assert.IsFalse(result);
        }
        /// <summary>
        ///  Checks if the file is included based on its path and extension.
        /// </summary>
        [TestMethod]
        public void ShouldIncludeFile_SkippedExtension_ReturnsFalse()
        {
            // Arrange
            var path = Path.Combine(_testDrivePath, "temp.tmp");

            // Act
            var result = CallShouldIncludeFile(path);

            // Assert
            Assert.IsFalse(result);
        }
        /// <summary>
        ///  Checks if the file is included based on its size and extension.
        /// </summary>
        [TestMethod]
        public void ShouldIncludeFile_SmallFile_ReturnsFalse()
        {
            // Arrange
            var path = Path.Combine(_testDrivePath, "small.exe");

            // Act
            var result = CallShouldIncludeFile(path);

            // Assert
            Assert.IsFalse(result);
        }
        /// <summary>
        ///  Loads all files asynchronously and verifies that the progress is reported correctly.
        /// </summary>
        /// <returns></returns>
        [TestMethod]
        public async Task LoadAllFilesAsync_ReportsProgress()
        {
            // Arrange
            var progressValues = new List<int>();
            _progressMock.Setup(p => p.Report(It.IsAny<int>()))
                .Callback<int>(value => progressValues.Add(value));

            // Act
            await _loader.LoadAllFilesAsync(_progressMock.Object, CancellationToken.None);

            // Assert
            Assert.IsTrue(progressValues.Count >= 1);
            Assert.IsTrue(progressValues.Last() >= 3);
        }
        /// <summary>
        ///  Helper method to call the private ShouldIncludeFile method using reflection.
        /// </summary>
        /// <param name="path"> The path of the file to check.</param>
        /// <returns> True if the file should be included, false otherwise.</returns>
        private bool CallShouldIncludeFile(string path)
        {
            return (bool)typeof(FileLoader)
                .GetMethod("ShouldIncludeFile", BindingFlags.NonPublic | BindingFlags.Instance)
                .Invoke(_loader, new object[] { path });
        }
    }
}
