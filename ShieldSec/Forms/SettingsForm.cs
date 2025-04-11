using Krypton.Toolkit;
using System.Diagnostics;

namespace ShieldSec.Design
{
    /// <summary>
    ///  This form is used to display the analysis results of potentially infected files.
    /// </summary>
    public partial class SettingsForm : KryptonForm
    {
        /// <summary>
        ///  Constructor for the AnalysisForm.
        /// </summary>
        public SettingsForm()
        {
            InitializeComponent();
            FormClosing += SettingsForm_FormClosing;
        }

        private void kryptonTrackBar1_ValueChanged(object sender, EventArgs e)
        {
            lbThreadCount.Text = tbThreads.Value.ToString();
        }

        private void SettingsForm_Load(object sender, EventArgs e)
        {
            // so from our settings file, we need to load either the default value or the saved value
            tbThreads.Value = Core.Managers.SettingsManager.Settings.MaxThreads;
            lbThreadCount.Text = tbThreads.Value.ToString();
            tbThreads.Maximum = Environment.ProcessorCount;
            tbThreads.Minimum = 1;
            if (Core.Managers.SettingsManager.Settings.ScheduledScansEnabled)
            {
                rbScheduledScans.Checked = true;
                dtpScheduledScans.Visible = true;
            }
            else
            {
                rbScheduledScans.Checked = false;
                dtpScheduledScans.Visible = false;
            }

            dtpScheduledScans.Format = DateTimePickerFormat.Time;
            dtpScheduledScans.ShowUpDown = true;
        }

        /// <summary>
        ///  Sets the settings when the form is closing. This is to ensure that the settings are saved when the form is closed.
        /// </summary>
        /// <param name="sender"> the sender of the event</param>
        /// <param name="e"> the event args</param>
        private void SettingsForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            Core.Managers.SettingsManager.Settings.MaxThreads = tbThreads.Value;
            Core.Managers.SettingsManager.Settings.ScheduledScansEnabled = rbScheduledScans.Checked;
            Core.Managers.SettingsManager.SaveSettings();
        }
        /// <summary>
        /// Checks if the scheduled scans radio button is checked. If it is, we need to show the date time picker.
        /// </summary>
        /// <param name="sender"> the sender of the event</param>
        /// <param name="e"> the event args</param>
        private void rbScheduledScans_CheckedChanged(object sender, EventArgs e)
        {
            // if they want to enable scheduled scans, we need to show the date time picker
            if (rbScheduledScans.Checked)
            {
                dtpScheduledScans.Visible = true;
            }
            else
            {
                dtpScheduledScans.Visible = false;
            }
        }
        /// <summary>
        ///   Sets the scheduled scans time when the date time picker value is changed. This is to ensure that the time is saved when the value is changed.
        /// </summary>
        /// <param name="sender"> the sender of the event</param>
        /// <param name="e"> the event args</param>
        private void dtpScheduledScans_ValueChanged(object sender, EventArgs e)
        {
            Debug.WriteLine(dtpScheduledScans.Value);
            string time = dtpScheduledScans.Value.ToString("HH:mm");
            Debug.WriteLine(time);
            if (!string.IsNullOrEmpty(time))
            {
                Core.Managers.SettingsManager.Settings.ScheduledScansTime = dtpScheduledScans.Value;
                Debug.WriteLine($"Scheduled scans time set to {time}");
            }
            else
            {
                Debug.WriteLine("Scheduled scans time is empty");
            }
        }
    }
}
