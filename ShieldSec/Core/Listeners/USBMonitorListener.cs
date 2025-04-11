using ShieldSec.Core.Managers;
using System.Diagnostics;
using System.Management;
using System.Text;

namespace ShieldSec.Core.Listeners
{
    /// <summary>
    /// Monitors for USB and removable media events (insertion and removal) using WMI.
    /// References:
    ///   - https://learn.microsoft.com/en-us/dotnet/api/system.management.managementeventwatcher
    ///   - https://stackoverflow.com/questions/620144/detecting-usb-drive-insertion-and-removal-using-windows-service-and-c-sharp
    ///   - https://www.youtube.com/watch?v=tLxaJPMUUCs
    ///   - https://learn.microsoft.com/en-us/answers/questions/514614/how-to-detect-usb-overcurrent-in-c
    /// </summary>
    public class USBMonitorListener : IDisposable
    {
        public event EventHandler<string>? DriveInserted;
        public event EventHandler<string>? DriveRemoved;
        private ManagementEventWatcher? _insertWatcher;
        private ManagementEventWatcher? _removeWatcher;
        private bool _disposed;
        /// <summary>
        ///  Constructor for the USB Monitor Listener class.
        /// </summary>
        public USBMonitorListener()
        {
            StartMonitoring();
        }
        /// <summary>
        /// Starts monitoring for USB and removable media insertion and removal events.
        /// </summary>
        public void StartMonitoring()
        {
            if (_disposed) throw new ObjectDisposedException(nameof(USBMonitorListener));

            var insertQuery = new WqlEventQuery("SELECT * FROM Win32_VolumeChangeEvent WHERE EventType = 2");
            _insertWatcher = new ManagementEventWatcher(insertQuery);
            _insertWatcher.EventArrived += InsertEventArrived;
            _insertWatcher.Start();

            var removeQuery = new WqlEventQuery("SELECT * FROM Win32_VolumeChangeEvent WHERE EventType = 3");
            _removeWatcher = new ManagementEventWatcher(removeQuery);
            _removeWatcher.EventArrived += RemoveEventArrived;
            _removeWatcher.Start();
        }
        /// <summary>
        ///  Invokes the DriveInserted event when a USB drive is inserted.
        /// </summary>
        /// <param name="sender"> The sender. </param>
        /// <param name="e"> The event arguments. </param>
        private void InsertEventArrived(object sender, EventArrivedEventArgs e)
        {
            // get all available properities we can use for analysis
            foreach (var prop in e.NewEvent.Properties)
            {
                Debug.WriteLine($"{prop.Name}: {prop.Value}");
            }
            var driveName = e.NewEvent.Properties["DriveName"].Value?.ToString();
            if (!string.IsNullOrWhiteSpace(driveName))
            {
                string TimeCreated = e.NewEvent.Properties["TIME_CREATED"].Value?.ToString() ?? string.Empty;
                // this will return something like "133881831931498397" which is the time in ticks I think? 
                // we can convert this to a DateTime object
                var dateTime = new DateTime(long.Parse(TimeCreated));
                DriveInserted?.Invoke(this, driveName);
                // format a string for multi line
                StringBuilder sb = new StringBuilder();
                sb.AppendLine($"Drive Name: {driveName}");
                sb.AppendLine($"Time Created: {dateTime}");
                NotificationManager.ShowToast("USB Found", sb.ToString(), String.Empty, Enums.NotificationType.GENERIC);
            }
        }
        /// <summary>
        ///  Removes the event watcher for the USB insertion event.
        /// </summary>
        /// <param name="sender"> The sender. </param>
        /// <param name="e"> The event arguments. </param>
        private void RemoveEventArrived(object sender, EventArrivedEventArgs e)
        {
            var driveName = e.NewEvent.Properties["DriveName"].Value?.ToString();
            if (!string.IsNullOrWhiteSpace(driveName))
            {
                DriveRemoved?.Invoke(this, driveName);
            }
        }

        /// <summary>
        /// Stops monitoring and cleans up resources.
        /// </summary>
        public void StopMonitoring()
        {
            if (_insertWatcher != null)
            {
                _insertWatcher.Stop();
                _insertWatcher.EventArrived -= InsertEventArrived;
                _insertWatcher.Dispose();
                _insertWatcher = null;
            }
            if (_removeWatcher != null)
            {
                _removeWatcher.Stop();
                _removeWatcher.EventArrived -= RemoveEventArrived;
                _removeWatcher.Dispose();
                _removeWatcher = null;
            }
        }
        /// <summary>
        ///  Disposes of the listener.
        /// </summary>
        public void Dispose()
        {
            if (_disposed) return;
            StopMonitoring();
            _disposed = true;
        }
    }
}
