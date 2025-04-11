using ShieldSec.Core.Interfaces;
using ShieldSec.Core.Managers;
using ShieldSec.Utils;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShieldSec.Core.Listeners
{
    /// <summary>
    ///  This class is responsible for monitoring the downloads folder for new files.
    ///  References:
    ///   - https://learn.microsoft.com/en-us/dotnet/api/system.io.filesystemwatcher?view=net-9.0
    ///   - https://stackoverflow.com/questions/1764809/filesystemwatcher-changed-event-is-raised-twice
    ///   - https://learn.microsoft.com/en-us/dotnet/api/system.io.filestream?view=net-9.0
    /// </summary>
    public class FileMonitorListener : IDisposable
    {
        public event EventHandler<string> FileVerified;
        public event EventHandler<string> FileUnverified;
        public event EventHandler<string> FileDownloaded;
        private FileSystemWatcher? _watcher;
        private IScanManager? _scanManager;
        private bool _isDisposed;
        private readonly ConcurrentDictionary<string, byte> _processingFiles = new();
        private readonly HashSet<string> _tempExtensions = new(StringComparer.OrdinalIgnoreCase) { ".tmp", ".crdownload", ".part", ".download" };
        /// <summary>
        ///  Starts monitoring the downloads folder for new files.
        ///  Warning: Only one instance of the listener can be active at a time.
        /// </summary>
        /// <param name="scanManager"> The scan manager to use for scanning files.</param>
        /// <param name="path"> The path of the downloads folder to monitor.</param>
        public void StartMonitoring(IScanManager scanManager, string? path = null)
        {
            if (_isDisposed) throw new ObjectDisposedException(nameof(FileMonitorListener));
            if (_watcher != null) throw new InvalidOperationException("Monitoring already active");

            var downloadsPath = ValidatePath(path ?? SystemFolders.GetDownloadsPath());

            _scanManager = scanManager;
            _watcher = new FileSystemWatcher
            {
                Path = downloadsPath,
                NotifyFilter = NotifyFilters.FileName | NotifyFilters.LastWrite,
                Filter = "*.*",
                EnableRaisingEvents = true,
                IncludeSubdirectories = true
            };

            _watcher.Renamed += async (s, e) =>
            {
                if (IsTempFile(e.OldName) && !IsTempFile(e.Name))
                {
                    await SafeHandleFile(e.FullPath);
                }
            };

            _watcher.Changed += async (s, e) =>
            {
                if (!IsTempFile(e.Name))
                {
                    await SafeHandleFile(e.FullPath);
                }
            };
        }
        /// <summary>
        ///  Handles a file in a safe manner, avoiding concurrency issues.
        /// </summary>
        /// <param name="path"> The path of the file to handle.</param>
        /// <returns> A task representing the operation.</returns>
        private async Task SafeHandleFile(string path)
        {
            if (!_processingFiles.TryAdd(path, 0)) return;

            try
            {
                FileDownloaded?.Invoke(this, path);
                await WaitForFileCompletion(path);
                await HandleNewFile(path);
            }
            finally
            {
                _processingFiles.TryRemove(path, out _);
            }
        }
        /// <summary>
        ///  Waits for a file to be completely written before processing it.
        /// </summary>
        /// <param name="path"> The path of the file to wait for.</param>
        /// <returns> A task representing the operation.</returns>
        private async Task WaitForFileCompletion(string path)
        {
            const int maxWaitTime = 30000;
            var startTime = DateTime.Now;
            long lastSize = -1;

            while ((DateTime.Now - startTime).TotalMilliseconds < maxWaitTime)
            {
                try
                {
                    var info = new FileInfo(path);
                    if (!info.Exists) break;

                    if (info.Length == lastSize && info.Length > 0)
                    {
                        return;
                    }

                    lastSize = info.Length;
                    await Task.Delay(2000);
                }
                catch
                {
                    await Task.Delay(1000);
                }
            }
        }
        /// <summary>
        ///  Handles a new file by scanning it and updating the UI.
        /// </summary>
        /// <param name="path"> The path of the file to handle.</param>
        /// <returns> A task representing the operation.</returns>
        private async Task HandleNewFile(string path)
        {
            const int maxRetries = 5;
            int attempt = 0;

            while (attempt++ < maxRetries)
            {
                try
                {
                    using (var fs = new FileStream(
                        path,
                        FileMode.Open,
                        FileAccess.Read,
                        FileShare.ReadWrite))
                    {
                        bool scanResult = await _scanManager.ScanFileAndGetResultAsync(path);
                        if (scanResult)
                        {
                            FileVerified?.Invoke(this, path);
                        }
                        else
                        {
                            FileUnverified?.Invoke(this, path);
                        }

                        return;
                    }
                }
                // https://learn.microsoft.com/en-us/windows/win32/debug/system-error-codes--0-499-
                // breakdown: -2147024864 = 0x80070020 -> ERROR_SHARING_VIOLATION (32) -> The process cannot access the file because it is being used by another process.
                catch (IOException ex) when (ex is FileNotFoundException ||
                                           ex.HResult == -2147024864)
                {
                    Debug.WriteLine($"File access conflict: {path} (attempt {attempt})");
                    await Task.Delay(1000 * attempt);
                }
                catch (Exception ex)
                {
                    Debug.WriteLine($"Error scanning {path}: {ex.Message}");
                    return;
                }
            }
        }
        /// <summary>
        ///  Checks if a file is a temporary file by checking the hash set of temporary file extensions.
        /// </summary>
        /// <param name="filename"> The name of the file to check.</param>
        /// <returns> True if the file is a temporary file; otherwise, false.</returns>
        private bool IsTempFile(string filename) => _tempExtensions.Contains(Path.GetExtension(filename));
        /// <summary>
        ///  Validates the path of the downloads folder.
        /// </summary>
        /// <param name="path"> The path to validate.</param>
        /// <returns> The validated path.</returns>
        /// <exception cref="DirectoryNotFoundException"> Thrown if the path is not valid.</exception>
        private string ValidatePath(string path)
        {
            if (!Directory.Exists(path))
                throw new DirectoryNotFoundException($"Downloads folder not found at: {path}");

            return path;
        }

        public void Dispose()
        {
            if (_isDisposed) return;

            _watcher?.Dispose();
            _scanManager = null;
            _processingFiles.Clear();
            _isDisposed = true;
        }
    }
}
