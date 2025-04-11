using Krypton.Toolkit;
using ShieldSec.Utils;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ShieldSec.Design
{
    public partial class LoadingScreen : KryptonForm
    {
        private readonly CancellationTokenSource _cts = new();
        /// <summary>
        ///  Loading screen constructor, which initializes the loading screen.
        /// </summary>
        public LoadingScreen()
        {
            InitializeComponent();
            this.FormBorderStyle = FormBorderStyle.Fixed3D;
            this.StartPosition = FormStartPosition.CenterScreen;
            this.Size = new Size(400, 250);
            this.DoubleBuffered = true;

            var progressCircle = new ProgressCircle
            {
                Location = new Point(150, 50),
                Size = new Size(100, 100),
                ForeColor = Color.FromArgb(246, 70, 104),
            };


            this.Controls.Add(progressCircle);
            FormClosed += OnFormClosed;

            Task.Run(async () => await AnimateAsync(progressCircle, lbTextProgression, _cts.Token));
        }

        /// <summary>
        /// A task that animates the loading screen.
        /// </summary>
        /// <param name="progressCircle"> The progress circle control. </param>
        /// <param name="label"> The label control. </param>
        /// <param name="ct"> The cancellation token. </param>
        /// <returns> The task. </returns>
        private async Task AnimateAsync(Control progressCircle, Control label, CancellationToken ct)
        {
            const string baseMessage = "ShieldSec Initializing";
            var sw = Stopwatch.StartNew();
            var dotCycle = TimeSpan.FromSeconds(1); 

            while (!ct.IsCancellationRequested)
            {
                try
                {
                    var elapsed = sw.Elapsed;
                    var dotCount = (int)((elapsed.TotalSeconds % 1) * 4);
                    // reset the dot count if it exceeds 3
                    dotCount = dotCount > 3 ? 3 : dotCount;

                    if (!label.IsDisposed && !progressCircle.IsDisposed)
                    {
                        label.Invoke((MethodInvoker)(() =>
                        {
                            label.Text = $"{baseMessage}{new string('.', dotCount)}";
                            progressCircle.Invalidate();
                        }));
                    }

                    var delay = Math.Max(0, 16 - (int)sw.ElapsedMilliseconds % 16);
                    await Task.Delay(delay, ct);
                }
                catch (TaskCanceledException)
                {
                    break;
                }
                catch (ObjectDisposedException)
                {
                    break;
                }
            }

            if (!label.IsDisposed)
            {
                label.Invoke((MethodInvoker)(() => label.Text = "ShieldSec Ready"));
            }
        }
        /// <summary>
        ///  Cancels the loading screen
        /// </summary>
        /// <param name="sender"> The sender. </param>
        /// <param name="e"> The event arguments. </param>
        private void OnFormClosed(object sender, FormClosedEventArgs e)
        {
            _cts.Cancel();
            Debug.WriteLine("Loading screen closed.");
        }
    }
}
