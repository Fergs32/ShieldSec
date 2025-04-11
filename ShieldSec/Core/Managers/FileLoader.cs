using System.Collections.Concurrent;
using System.Diagnostics;

namespace ShieldSec.Core.Managers
{
    /// <summary>
    ///  This class is responsible for loading all files from the system.
    /// </summary>
    public class FileLoader
    {
        /// <summary>
        ///  A Set of directories to exclude from the search.
        /// </summary>
        private readonly HashSet<string> _excludedDirectories = new(StringComparer.OrdinalIgnoreCase)
        {
            "Windows", "Program Files", "Program Files (x86)", "ProgramData",
            "$WinREAgent", "System Volume Information", "Recovery",
            "Microsoft SDKs", "Windows Kits", "Common Files", "Intel",
            "AMD", "NVIDIA", "Package Cache", "MSOCache", "packages",
            "node_modules", "venv", ".git", "obj", "bin"
        };
        /// <summary>
        ///  A set of file extensions to skip, these are known to be non-malicious (80% of the time).
        /// </summary>
        private readonly HashSet<string> _skipExtensions = new(StringComparer.OrdinalIgnoreCase)
        {
            ".tmp", ".log", ".bak", ".cab", ".cache", ".blob",
            ".mp3", ".mp4", ".avi", ".mkv", ".jpg", ".png", ".gif",
            ".zip", ".rar", ".7z", ".tar", ".gz",
            ".vhd", ".vhdx", ".iso", ".vmdk",
            ".pdb", ".ilk", ".exp", ".lib", ".map", ".obj"
        };
        /// <summary>
        ///  A set of high value file extensions to include in the search.
        /// </summary>
        private readonly HashSet<string> _highValueExtensions = new(StringComparer.OrdinalIgnoreCase)
        {
            ".exe", ".dll", ".sys", ".scr", ".ps1", ".bat", ".cmd",
            ".vbs", ".js", ".jse", ".wsf", ".docm", ".xlsm", ".pptm",
            ".jar", ".msi", ".lnk", ".dmp", ".py", ".sh"
        };
        /// <summary>
        ///  Loads all files from the system asynchronously, using parallel processing.
        /// </summary>
        /// <param name="progress"> The progress of the loading process. </param>
        /// <param name="ct"> The cancellation token. </param>
        /// <returns> A list of all files found. </returns>
        public async Task<List<string>> LoadAllFilesAsync(IProgress<int> progress, CancellationToken ct)
        {
            var allFiles = new ConcurrentBag<string>();
            var options = new ParallelOptions
            {
                CancellationToken = ct,
                MaxDegreeOfParallelism = Math.Max(1, SettingsManager.Settings.MaxThreads)
            };

            var drives = DriveInfo.GetDrives()
                .Where(d => d.DriveType == DriveType.Fixed && d.IsReady)
                .Select(d => d.RootDirectory.FullName);

            await Parallel.ForEachAsync(drives, options, async (drive, ct) =>
            {
                await ProcessDriveAsync(drive, allFiles, progress, ct);
            });

            return allFiles.ToList();
        }
        /// <summary>
        ///  Proccesses a drive asynchronously.
        /// </summary>
        /// <param name="path"> The path of the drive to process. </param>
        /// <param name="allFiles"> The list of all files found. </param>
        /// <param name="progress"> The progress of the loading process. </param>
        /// <param name="ct"> The cancellation token. </param>
        /// <returns> Task </returns>
        private async Task ProcessDriveAsync(string path, ConcurrentBag<string> allFiles,
            IProgress<int> progress, CancellationToken ct)
        {
            var sw = Stopwatch.StartNew();
            int lastReport = 0;
            int reportInterval = 500; // 500 has been found to be a good interval

            await Parallel.ForEachAsync(EnumerateFiles(path), new ParallelOptions
            {
                CancellationToken = ct,
                MaxDegreeOfParallelism = SettingsManager.Settings.MaxThreads
            }, async (file, ct) =>
            {
                if (ShouldIncludeFile(file))
                {
                    allFiles.Add(file);
                    var count = allFiles.Count;
                    if (count - lastReport >= reportInterval)
                    {
                        progress?.Report(count);
                        lastReport = count;
                    }
                }
            });

            // debug
            Debug.WriteLine($"Processed {path} in {sw.Elapsed.TotalSeconds:0.00}s");
        }
        /// <summary>
        ///  Enumerates all files in a directory.
        /// </summary>
        /// <param name="path"> The path of the directory to enumerate. </param>
        /// <returns> A list of all files found. </returns>
        private IEnumerable<string> EnumerateFiles(string path)
        {
            var enumOptions = new EnumerationOptions
            {
                RecurseSubdirectories = true,
                IgnoreInaccessible = true,
                BufferSize = 65536, // by adding a buffer size, we can reduce the number of I/O operations required (this is because the buffer is filled with multiple file entries at once)
                AttributesToSkip = FileAttributes.Temporary | FileAttributes.System |
                                 FileAttributes.Offline | FileAttributes.Device,
                MatchType = MatchType.Win32,
                ReturnSpecialDirectories = false
            };

            return Directory.EnumerateFiles(path, "*", enumOptions);
        }
        /// <summary>
        ///  Checks if a file should be included in the search.
        /// </summary>
        /// <param name="filePath"> The path of the file to check. </param>
        /// <returns> True if the file should be included, false otherwise. </returns>
        private bool ShouldIncludeFile(string filePath)
        {
            try
            {
                var extension = Path.GetExtension(filePath);
                if (_skipExtensions.Contains(extension)) return false;
                if (!_highValueExtensions.Contains(extension)) return false;

                var dir = Path.GetDirectoryName(filePath);
                if (dir == null) return false;

                var dirParts = dir.Split(Path.DirectorySeparatorChar);
                foreach (var part in dirParts)
                {
                    if (_excludedDirectories.Contains(part)) return false;
                    if (part.StartsWith("$")) return false;
                }

                var attrs = File.GetAttributes(filePath);
                if (attrs.HasFlag(FileAttributes.Device)) return false;

                if (_highValueExtensions.Contains(extension))
                {
                    var info = new FileInfo(filePath);
                    if (info.Length < 4096) return false;
                }

                return true;
            }
            catch
            {
                return false;
            }
        }
    }
}
