using Krypton.Toolkit;

namespace ShieldSec.Core.Interfaces
{
    public abstract class BaseNotificationForm : KryptonForm
    {
        public abstract int TargetY { get; set; }
        public abstract void StartSlideAnimation();
    }
}
