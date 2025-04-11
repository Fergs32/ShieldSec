using System;
using System.Collections.Generic;
using System.Drawing.Drawing2D;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShieldSec.Utils
{
    public class ProgressCircle : Control
    {
        private float _angle;
        private readonly System.Timers.Timer _animationTimer;

        public ProgressCircle()
        {
            SetStyle(ControlStyles.OptimizedDoubleBuffer | ControlStyles.AllPaintingInWmPaint, true);
            _animationTimer = new System.Timers.Timer { Interval = 16 };
            _animationTimer.Elapsed += (s, e) => { _angle = (_angle + 2) % 360; Invalidate(); };
            _animationTimer.Start();
        }

        protected override void OnPaint(PaintEventArgs e)
        {
            base.OnPaint(e);
            var g = e.Graphics;
            g.SmoothingMode = SmoothingMode.AntiAlias;

            using (var pen = new Pen(ForeColor, 4))
            {
                g.DrawArc(pen, new Rectangle(2, 2, Width - 4, Height - 4), _angle, 270);
            }
        }
    }
}
