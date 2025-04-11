using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShieldSec.Core.Models
{
    /// <summary>
    ///  Object representation of the app settings.
    /// </summary>
    public class AppSettings
    {
        /// <summary>
        ///  Maximum number of threads to use for scanning and overall processing.
        /// </summary>
        public int MaxThreads { get; set; } = 4;
        /// <summary>
        ///  Is scheduled scanning enabled.
        /// </summary>
        public bool ScheduledScansEnabled { get; set; } = false;
        /// <summary>
        ///  Scheduled scans time.
        /// </summary>
        public DateTime ScheduledScansTime { get; set; } = DateTime.Now;
        /// <summary>
        ///  Gets and sets the last scanned time
        /// </summary>
        public DateTime lastScannedTime {  get; set; }
    }
}
