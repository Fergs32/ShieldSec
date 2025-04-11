using Krypton.Toolkit;
using ShieldSec.Core.Interfaces;
using ShieldSec.Core.Managers;
using System.Security.Cryptography;

namespace ShieldSec.Design
{
    /// <summary>
    ///  This class is responsible for the toast notification form.
    /// </summary>
    public partial class ToastNotificationForm : BaseNotificationForm
    {
        private System.Windows.Forms.Timer _timer;
        public override int TargetY { get; set; }
        private const int AnimationInterval = 15;
        /// <summary>
        ///  Constructor for the ToastNotificationForm.
        /// </summary>
        /// <param name="title"> The title of the notification. </param>
        /// <param name="fileName"> The name of the file detected. </param>
        /// <param name="fileLocation"> The location of the file detected. </param>
        public ToastNotificationForm(string title, string fileName, string fileLocation)
        {
            InitializeComponent();
            InitializeContent(title, fileName, fileLocation);
            InitializeTimer();
            InitializePosition();
        }
        /// <summary>
        ///  Initializes the content of the notification.
        /// </summary>
        /// <param name="title"> The title of the notification. </param>
        /// <param name="fileName"> The name of the file detected. </param>
        /// <param name="fileLocation"> The location of the file detected. </param>
        private void InitializeContent(string title, string fileName, string fileLocation)
        {
            lbTitle.Text = title;
            lbDetected.Text = fileName;
            lbLocation.Text = fileLocation;
        }
        /// <summary>
        ///  Initializes the timer for the notification.
        /// </summary>
        private void InitializeTimer()
        {
            _timer = new System.Windows.Forms.Timer { Interval = 5000 };
            _timer.Tick += (s, e) => BeginClose();
        }
        /// <summary>
        ///  Initializes the position of the notification.
        /// </summary>
        private void InitializePosition()
        {
            FormBorderStyle = FormBorderStyle.None;
            ShowInTaskbar = false;
            TopMost = true;
            StartPosition = FormStartPosition.Manual;
            Opacity = 0;
        }
        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);
            AnimateShow();
            _timer.Start();
        }
        /// <summary>
        ///  Starts the slide animation.
        /// </summary>
        public override void StartSlideAnimation()
        {
            var slideTimer = new System.Windows.Forms.Timer { Interval = AnimationInterval };
            slideTimer.Tick += (s, e) =>
            {
               if (Math.Abs(Top - TargetY) > 2)
                    Top = (Top * 3 + TargetY) / 4;
                else
                {
                    Top = TargetY;
                    slideTimer.Stop();
                }
            };
            slideTimer.Start();
        }
        /// <summary>
        ///  Animates the show of the notification.
        /// </summary>
        private void AnimateShow()
        {
            var targetY = Location.Y;
            Location = new Point(Location.X, targetY + 100);

            var fadeIn = new System.Windows.Forms.Timer { Interval = AnimationInterval };
            fadeIn.Tick += (s, e) =>
            {
                if (Opacity < 1)
                {
                    Opacity += 0.1;
                    Top = (int)(Top * 0.9 + targetY * 0.1);
                }
                else
                {
                    Opacity = 1;
                    Top = targetY;
                    fadeIn.Stop();
                }
            };
            fadeIn.Start();
        }
        /// <summary>
        ///  Begins the close of the notification.
        /// </summary>
        public void BeginClose()
        {
            _timer.Stop();
            var fadeOut = new System.Windows.Forms.Timer { Interval = AnimationInterval };
            fadeOut.Tick += (s, e) =>
            {
                if (Opacity > 0)
                {
                    Opacity -= 0.1;
                    Top += 8;
                }
                else
                {
                    fadeOut.Stop();
                    Close();
                    Dispose();
                }
            };
            fadeOut.Start();
        }
        /// <summary>
        ///  Disposes of the notification when the form is closed.
        /// </summary>
        /// <param name="e"> The event arguments. </param>
        protected override void OnFormClosed(FormClosedEventArgs e)
        {
            base.OnFormClosed(e);
            _timer.Dispose();
        }

        private void btnIgnoreAlert_Click(object sender, EventArgs e)
        {

        }

        private void btnQuarantine_Click(object sender, EventArgs e)
        {
            String filePath = lbLocation.Text;
            QuarantineManager.Instance.Quarantine<AesCryptoServiceProvider>(filePath);
            QuarantineManager.Instance.LogAction("Quarantined file: " + filePath);
            NotificationManager.ShowToast("Qurantined File", filePath, "", Core.Enums.NotificationType.GENERIC);
        }
    }
}
