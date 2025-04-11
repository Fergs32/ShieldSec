using Krypton.Toolkit;
using ShieldSec.Core.Analysis.PostProcessing;
using ShieldSec.Core.Analysis.Scanners;
using ShieldSec.Core.Listeners;
using ShieldSec.Core.Managers;
using ShieldSec.Core.Models;
using ShieldSec.Core.Requests;
using ShieldSec.Design;
using ShieldSec.Utils;
using System.Diagnostics;

namespace ShieldSec
{
    /// <summary>
    ///  This handles the main screen of the application
    /// </summary>
    public partial class MainScreen : KryptonForm
    {
        private static ScanManager? scanManager;
        private FileSignatureScanner? fileSignatureScanner;
        private SerialNumberChecker? serialNumberChecker;
        private FileMonitorListener? fileMonitor;
        private USBMonitorListener? usbMonitor;
        private MalwareHashRequest? malwareHashRequest;
        private AnalysisForm analysisForm;
        private SettingsForm settingsForm;
        private LoadingScreen loadingScreen;
        private QuarantineManager quarantineManager;
        private String buildVersion = "v1.0.0";
        private volatile CancellationTokenSource? _cts;
        private CircularProgressBar circularProgressBar;
        private readonly System.Windows.Forms.Timer _schedulerTimer;
        /// <summary>
        ///  Constructor for the MainScreen
        /// </summary>
        public MainScreen()
        {
            InitializeComponent();
            FormClosed += OnFormClosed;

            circularProgressBar = new CircularProgressBar
            {
                Size = new Size(300, 300),
                ProgressColor = Color.FromArgb(0, 126, 249),
                ProgressThickness = 20f,
                BackColor = Color.FromArgb(246, 70, 104),
                Visible = true
            };

            // since can't initialize the point in the constructor, we do it here (I got a object reference error)
            circularProgressBar.Location = new Point((Width - circularProgressBar.Width) / 2, (Height - circularProgressBar.Height) / 2);

            this.Controls.Add(circularProgressBar);

            _schedulerTimer = new System.Windows.Forms.Timer
            {
                Interval = 60000
            };
            _schedulerTimer.Tick += SchedulerTimer_Tick;
            _schedulerTimer.Start();
        }
        /// <summary>
        ///  Intializes some components via dependency injection, and sets the form to be invisible until the loading screen is closed.
        /// </summary>
        /// <param name="loadingScreen"> The loading screen </param>
        /// <returns>< Task ></returns>
        public async Task InitializeAsync(LoadingScreen loadingScreen)
        {
            this.loadingScreen = loadingScreen;
            this.loadingScreen.FormClosed += OnLoadingScreenClosed;
            this.Visible = false;
            await InitializeApplicationComponents();
            this.Enabled = true;
        }
        /// <summary>
        ///  Initializes all the application components
        /// </summary>
        /// <returns> Task </returns>
        private async Task InitializeApplicationComponents()
        {
            var fileLoader = new FileLoader();
            var progressIndicator = new Progress<int>();
            List<string> loadedFiles = new List<string>();
            using (_cts = new CancellationTokenSource())
            {
                loadedFiles = await fileLoader.LoadAllFilesAsync(progressIndicator, _cts.Token);
                Debug.WriteLine($"Loaded {loadedFiles.Count} files.");
                circularProgressBar.Maximum = loadedFiles.Count;
            }

            await Task.Run(() =>
            {
                Invoke(new Action(() =>
                {
                    String currentText = this.Text;
                    this.Text = currentText + " " + buildVersion;
                    malwareHashRequest = new MalwareHashRequest(Environment.CurrentDirectory + "\\signatures.txt");
                    Task.Run(async () =>
                    {
                        await malwareHashRequest.FetchAndUpdateHashesAsync();
                    });
                    fileSignatureScanner = new FileSignatureScanner(Environment.CurrentDirectory + "\\signatures.txt");
                    serialNumberChecker = new SerialNumberChecker();
                    quarantineManager = QuarantineManager.Instance;
                    scanManager = new ScanManager(this, fileSignatureScanner, serialNumberChecker, loadedFiles, quarantineManager);
                    fileMonitor = new FileMonitorListener();
                    usbMonitor = new USBMonitorListener();

                    fileMonitor.StartMonitoring(scanManager, SystemFolders.GetDownloadsPath());
                    usbMonitor.StartMonitoring();
                }));
            });

            Invoke(new Action(() =>
            {
                buttonPanel.Region = Region.FromHrgn(UIUtils.CreateRoundRectRgn(0, 0, buttonPanel.Width, buttonPanel.Height, 30, 30));
            }));

            Show();
        }
        /// <summary>
        ///  Handles the scan button click
        /// </summary>
        /// <param name="sender"> The sender. </param>
        /// <param name="e"> The event arguments. </param>
        private void buttonScan_Click(object sender, EventArgs e)
        {

            Task.Run(async () =>
            {
                try
                {
                    circularProgressBar.StartAnimation();
                    await scanManager?.StartScanAsync();
                }
                catch (Exception ex)
                {
                    BeginInvoke(new Action(() =>
                    {
                        MessageBox.Show($"Scan error: {ex.Message}", "Error",
                            MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }));
                }
                finally
                {
                    BeginInvoke(new Action(() =>
                    {
                        MessageBox.Show("Scan completed!", "Done");
                    }));
                }
            });
        }
        /// <summary>
        ///  Handles the analysis button click
        /// </summary>
        /// <param name="sender"> The sender. </param>
        /// <param name="e"> The event arguments. </param>
        private async void buttonAnalytics_Click(object sender, EventArgs e)
        {

            if (analysisForm == null || analysisForm.IsDisposed)
            {
                analysisForm = new AnalysisForm();
            }

            this.Invoke((MethodInvoker)delegate
            {
                analysisForm.Show();
                analysisForm.BringToFront();
            });

            await Task.Run(() =>
            {
                var infectedFiles = scanManager?.GetPotentiallyInfectedFiles();
                // I had to use a delegate due to the cross-threading issue between the main form and the analysis form.
                // invoking also helps because it ensures that the UI is updated on the main thread.
                this.Invoke((MethodInvoker)delegate
                {
                    analysisForm.PopulateTree(infectedFiles);
                });
            });
        }
        private void buttonSettings_Click(object sender, EventArgs e)
        {
            if (settingsForm == null || settingsForm.IsDisposed)
            {
                settingsForm = new SettingsForm();
            }

            this.Invoke((MethodInvoker) delegate
            {
                settingsForm.Show();
                settingsForm.BringToFront();
            });
        }
        /// <summary>
        ///  Handles the closing of the form
        /// </summary>
        /// <param name="sender"> The sender. </param>
        /// <param name="e"> The event arguments. </param>
        private void OnFormClosed(object? sender, FormClosedEventArgs e)
        {
            fileMonitor?.Dispose();
            loadingScreen?.Dispose();
            _cts?.Dispose();
            scanManager?.CancelScan();
        }
        /// <summary>
        ///  Handles the closing of the loading screen
        /// </summary>
        /// <param name="sender"> The sender. </param>
        /// <param name="e"> The event arguments. </param>
        private void OnLoadingScreenClosed(object? sender, FormClosedEventArgs e)
        {
            // using the local reference to completely avoid "race conditions"
            var cts = _cts;
            if (cts != null && !cts.IsCancellationRequested)
            {
                try
                {
                    cts.Cancel();
                }
                catch (ObjectDisposedException)
                { // yeah , we don't care about this exception
                }
                finally
                {
                    if (ReferenceEquals(_cts, cts))
                    {
                        cts?.Dispose();
                        _cts = null;
                    }
                }
            }
        }
        private void SchedulerTimer_Tick(object sender, EventArgs e)
        {
            var settings = SettingsManager.Settings;

            if (!settings.ScheduledScansEnabled) return;

            var targetTime = settings.ScheduledScansTime.TimeOfDay;
            var currentTime = DateTime.Now.TimeOfDay;
            var lastScanDate = settings.lastScannedTime.Date;

            if (currentTime >= targetTime &&
                DateTime.Today > lastScanDate)
            {
                scanManager.StartScanAsync();
                settings.lastScannedTime = lastScanDate;
                SettingsManager.SaveSettings();
            }
        }
        /// <summary>
        ///  Gets the hits label control/object
        /// </summary>
        /// <returns> KryptonLabel </returns>
        public KryptonLabel GetHitsLabel()
        {
            return hitsTotalLabel;
        }
        /// <summary>
        ///  Gets the file scanned label control/object
        /// </summary>
        /// <returns> KryptonLabel </returns>
        public KryptonLabel GetFileScannedLabel()
        {
            return lbFileProgress;
        }
        /// <summary>
        ///  Gets the circular progress label control/object
        /// </summary>
        /// <returns > KryptonLabel </returns>
        public KryptonLabel GetCircularProgressLabel()
        {
            return lbCircularProgress;
        }
        /// <summary>
        ///  Gets the circular progress bar control/object
        /// </summary>
        /// <returns> CircularProgressBar </returns>
        public CircularProgressBar GetCircularProgressBar()
        {
            return circularProgressBar;
        }
    }
}
