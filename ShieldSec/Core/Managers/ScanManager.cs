using Krypton.Toolkit;
using ShieldSec.Core.Analysis.PostProcessing;
using ShieldSec.Core.Analysis.Scanners;
using ShieldSec.Core.Enums;
using ShieldSec.Core.Interfaces;
using ShieldSec.Core.Models;
using ShieldSec.Design;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShieldSec.Core.Managers
{
    /// <summary>
    ///  This class is responsible for managing the scanning process.
    /// </summary>
    public class ScanManager : IScanManager, IDisposable
    {
        private readonly ConcurrentBag<string> _potentiallyInfectedFiles;
        private KryptonLabel _fileScannedLabel;
        private KryptonLabel hitsTotalLabel;
        private readonly SerialNumberChecker _serialNumberChecker;
        private readonly FileSignatureScanner _fileSignatureScanner;
        private readonly QuarantineManager _quarantineManager;
        private readonly ShieldRuleManager _shieldRuleManager;
        private readonly MainScreen _mainScreenInstance;
        private readonly List<string> _loadedFiles;
        private readonly SemaphoreSlim _scanThrottle = new(SettingsManager.Settings.MaxThreads * 2);
        private readonly CancellationTokenSource _cts = new();
        private DateTime _lastProgressUpdate = DateTime.MinValue;
        private int _scannedFiles;
        private int _totalFiles;
        /// <summary>
        ///  Scan manager constructor
        /// </summary>
        /// <param name="MainScreenInstance"> the main screen instance </param>
        /// <param name="fileSignatureScanner"> the file signature scanner </param>
        /// <param name="serialNumberChecker"> the serial number checker </param>
        /// <param name="loadedFiles"> the list of loaded files </param>
        /// <param name="quarantineManager"> the quarantine manager </param>
        public ScanManager(MainScreen MainScreenInstance, FileSignatureScanner fileSignatureScanner ,SerialNumberChecker serialNumberChecker,
            List<string> loadedFiles, QuarantineManager quarantineManager)
        {
            _mainScreenInstance = MainScreenInstance;
            _potentiallyInfectedFiles = new ConcurrentBag<string>();
            _fileScannedLabel = MainScreenInstance.GetFileScannedLabel();
            hitsTotalLabel = MainScreenInstance.GetHitsLabel(); 
            _fileSignatureScanner = fileSignatureScanner;
            _serialNumberChecker = serialNumberChecker;
            _quarantineManager = quarantineManager;
            _shieldRuleManager = new ShieldRuleManager();
            _loadedFiles = loadedFiles;
        }
        /// <summary>
        ///  Starts the scan process when called
        /// </summary>
        /// <returns> Task </returns>
        public async Task StartScanAsync()
        {
            try
            {
                _totalFiles = _loadedFiles.Count;

                var options = new ParallelOptions
                {
                    MaxDegreeOfParallelism = SettingsManager.Settings.MaxThreads,
                    CancellationToken = _cts.Token
                };

                await Parallel.ForEachAsync(_loadedFiles, options, async (file, ct) =>
                {
                    await _scanThrottle.WaitAsync(ct);
                    try
                    {
                        await ScanFileAsync(file, ct);
                    }
                    finally
                    {
                        _scanThrottle.Release();
                    }
                });

                Console.WriteLine($"Scan completed. Total files scanned: {_scannedFiles}");
            }
            catch (OperationCanceledException)
            {
                Console.WriteLine("Scan was canceled by user");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error during scan: {ex.Message}");
            }
        }
        /// <summary>
        ///  Cancels the scan
        /// </summary>
        public void CancelScan()
        {
            if (!_cts.IsCancellationRequested)
            {
                _cts.Cancel();
                Console.WriteLine("Scan cancellation requested");
            }
        }
        /// <summary>
        ///  Scans a file and returns the result with a task object that can be awaited
        /// </summary>
        /// <param name="filePath"> the file path </param>
        /// <returns> Task </returns>
        public async Task<bool> ScanFileAndGetResultAsync(string filePath)
        {
            try
            {
                var fileInfo = new FileInfo(filePath);


                var scanTasks = new List<Task>
                {
                    _fileSignatureScanner.ScanFileAsync(filePath),
                    _serialNumberChecker.IsFileMalicious(filePath),
                    _shieldRuleManager.CheckMalwareAsync(filePath)
                };

                // we must wait for all tasks to complete before evaluating the threat level
                await Task.WhenAll(scanTasks);

                UpdateProgress(filePath);

                var isKnownFile = ((Task<bool>)scanTasks[0]).Result;
                var isMaliciousFile = ((Task<bool>)scanTasks[1]).Result;
                var ruleMatches = ((Task<List<ShieldRuleMatch>>)scanTasks[2]).Result;

                return EvaluateThreatLevel(isKnownFile, isMaliciousFile, ruleMatches, fileInfo);
            }
            catch { return false; }
            finally {  Interlocked.Increment(ref _scannedFiles); }
        }
        /// <summary>
        ///  Evaluates the threat level of a file based on the results of the scan
        /// </summary>
        /// <param name="isKnown"> is the file known </param>
        /// <param name="isMalicious"> is the file malicious </param>
        /// <param name="matches"> list of rule matches </param>
        /// <param name="file"> file info </param>
        /// <returns> bool </returns>
        private bool EvaluateThreatLevel(bool isKnown, bool isMalicious, List<ShieldRuleMatch> matches, FileInfo file)
        {
            var threatLevel = ThreatLevel.None;

            if (isKnown || isMalicious)
                threatLevel = ThreatLevel.High;

            foreach (var match in matches)
            {
                threatLevel = match.Rule.SeverityLevel switch
                {
                    SeverityLevel.Critical => ThreatLevel.Critical,
                    SeverityLevel.High when threatLevel < ThreatLevel.High => ThreatLevel.High,
                    SeverityLevel.Medium when threatLevel < ThreatLevel.Medium => ThreatLevel.Medium,
                    _ => threatLevel
                };
            }

            return threatLevel switch
            {
                ThreatLevel.Critical => true, 
                ThreatLevel.High => file.Length > 102400,
                ThreatLevel.Medium => matches.Count >= 2,
                _ => false
            };
        }
        /// <summary>
        ///  Scans a file and adds it to the list of potentially infected files if malware is detected
        /// </summary>
        /// <param name="filePath"> the file path </param>
        /// <param name="ct"> cancellation token </param>
        /// <returns> Task </returns>
        private async Task ScanFileAsync(string filePath, CancellationToken ct)
        {
            bool result = await ScanFileAndGetResultAsync(filePath);
            if (result)
            {
                string fileName = Path.GetFileName(filePath);
                _potentiallyInfectedFiles.Add(filePath);
                Debug.WriteLine("Malware detected: " + fileName);

                NotificationManager.ShowToast("Malware detected", fileName, filePath, NotificationType.TOAST);
                UpdateHitsLabel();
            }

            ct.ThrowIfCancellationRequested();
        }
        /// <summary>
        ///  Updates the hits label on the main screen
        /// </summary>
        private void UpdateHitsLabel()
        {
            hitsTotalLabel.Text = _potentiallyInfectedFiles.Count.ToString();
        }
        /// <summary>
        ///  Gets the list of potentially infected files
        /// </summary>
        /// <returns> List of strings </returns>
        public List<string> GetPotentiallyInfectedFiles()
        {
            return _potentiallyInfectedFiles.ToList();
        }
        /// <summary>
        ///  Updates the progress of the scan to the main screen, updating the progress bar and label.
        /// </summary>
        /// <param name="filePath"> file path </param>
        private void UpdateProgress(string filePath)
        {
            if ((DateTime.Now - _lastProgressUpdate).TotalMilliseconds < 100) return;

            _mainScreenInstance.Invoke((MethodInvoker) delegate
            {
                _mainScreenInstance.GetCircularProgressBar().PerformStep();
                _mainScreenInstance.GetCircularProgressLabel().Text = $"{ProgressPercentage:0.00}%";
                _fileScannedLabel.Text = Path.GetFileName(filePath);
                _lastProgressUpdate = DateTime.Now;
            });
        }
        /// <summary>
        ///  Returns the progress percentage of the scan
        /// </summary>
        private double ProgressPercentage =>
            _totalFiles == 0 ? 0 : (double)_scannedFiles / _totalFiles * 100;
        /// <summary>
        ///  Diopose of the scan manager
        /// </summary>
        public void Dispose()
        {
            _cts?.Dispose();
            _scanThrottle?.Dispose();
            GC.SuppressFinalize(this);
        }
    }
}
