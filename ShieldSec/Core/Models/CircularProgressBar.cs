using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing.Drawing2D;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShieldSec.Core.Models
{
    /// <summary>
    ///  Handles the circular progress bar control in the UI
    /// </summary>
    public class CircularProgressBar : Control
    {
        private int _value = 0;
        private int _maximum;
        private Color _progressColor;
        private Color _backColor;
        private float _progressThickness = 20f;
        private System.Windows.Forms.Timer _animationTimer;
        private float _angle = 0;
        /// <summary>
        ///  Constructor for the CircularProgressBar
        /// </summary>
        public CircularProgressBar()
        {
            // these are the default values
            DoubleBuffered = true;
            Size = new Size(300, 300);
            _animationTimer = new System.Windows.Forms.Timer();
            _animationTimer.Interval = 16;
            _animationTimer.Tick += (s, e) =>
            {
                _angle = (_angle + 1) % 360;
                Invalidate();
            };
        }
        /// <summary>
        ///  Starts the animation timer
        /// </summary>
        public void StartAnimation()
        {
            _animationTimer.Start();
            Debug.WriteLine("Animation started");
        }
        /// <summary>
        ///  Sets the value of the progress bar
        /// </summary>
        public int Value
        {
            get => _value;
            set
            {
                _value = Math.Min(Math.Max(value, 0), _maximum);
                Invalidate();
            }
        }
        /// <summary>
        ///  Sets the maximum value of the progress bar
        /// </summary>
        public int Maximum
        {
            get => _maximum;
            set
            {
                _maximum = value;
                Invalidate();
            }
        }
        /// <summary>
        ///  Sets the color of the progress bar
        /// </summary>
        public Color ProgressColor
        {
            get => _progressColor;
            set
            {
                _progressColor = value;
                Invalidate();
            }
        }
        /// <summary>
        ///  Sets the thickness of the progress bar
        /// </summary>
        public float ProgressThickness
        {
            get => _progressThickness;
            set
            {
                _progressThickness = value;
                Invalidate();
            }
        }
        /// <summary>
        ///  Sets the background color of the control
        /// </summary>
        public Color BackColor
        {
            get => _backColor;
            set
            {
                _backColor = value;
                Invalidate();
            }
        }
        /// <summary>
        ///  Stops the animation timer
        /// </summary>
        public void StopAnimation()
        {
            _animationTimer.Stop();
        }
        /// <summary>
        ///  Paints the control onto the screen
        /// </summary>
        /// <param name="e"> PaintEventArgs </param>
        protected override void OnPaint(PaintEventArgs e)
        {
            // we need to call the base class OnPaint method to ensure the control is drawn properly
            base.OnPaint(e);
            e.Graphics.SmoothingMode = SmoothingMode.AntiAlias;

            using (var pen = new Pen(_backColor, _progressThickness))
            {
                e.Graphics.DrawArc(pen, GetCircleRectangle(), 0, 360);
            }

            using (var pen = new Pen(_progressColor, _progressThickness))
            {
                float sweepAngle = 360f * _value / _maximum;
                e.Graphics.DrawArc(pen, GetCircleRectangle(), _angle, sweepAngle);
            }
        }
        /// <summary>
        ///  Gets the rectangle that the circle will be drawn in
        /// </summary>
        /// <returns> RectangleF </returns>
        private RectangleF GetCircleRectangle()
        {
            float offset = _progressThickness / 2;
            return new RectangleF(offset, offset, Width - _progressThickness, Height - _progressThickness);
        }
        /// <summary>
        ///  Disposes of the control and the animation timer
        /// </summary>
        /// <param name="disposing"> bool </param>
        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                _animationTimer?.Stop();
                _animationTimer?.Dispose();
            }
            base.Dispose(disposing);
        }
        /// <summary>
        ///  Performs a step in the progress bar using interlocked increment (thread safe)
        /// </summary>
        internal void PerformStep()
        {
            Interlocked.Increment(ref _value);
            Invalidate();
        }
    }
}
