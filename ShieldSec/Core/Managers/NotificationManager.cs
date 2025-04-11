using ShieldSec.Core.Enums;
using ShieldSec.Core.Interfaces;
using ShieldSec.Design;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShieldSec.Core.Managers
{
    public static class NotificationManager
    {
        private static readonly List<BaseNotificationForm> ActiveToasts = new();
        public const int ToastSpacing = 10;
        public const int ToastWidth = 350;

        public static void ShowToast(string title, string fileName, string filePath, NotificationType notifType)
        {
            Application.OpenForms[0]?.Invoke(new Action(() =>
            {
                BaseNotificationForm toast = notifType switch
                {
                    NotificationType.GENERIC => new GenericNotificationForm(title, fileName),
                    NotificationType.TOAST => new ToastNotificationForm(title, fileName, filePath),
                    _ => throw new ArgumentOutOfRangeException(nameof(notifType), notifType, null)
                };

                PositionNewToast(toast);
                ActiveToasts.Add(toast);
                toast.FormClosed += (s, e) => RemoveToast(toast);
                toast.Show();
            }));
        }

        private static void PositionNewToast(BaseNotificationForm newToast)
        {
            var screen = Screen.PrimaryScreen.WorkingArea;
            int baseY = screen.Bottom - newToast.Height - ToastSpacing;

            foreach (var existing in ActiveToasts)
            {
                baseY -= existing.Height + ToastSpacing;
            }

            newToast.Location = new Point(
                screen.Right - ToastWidth - ToastSpacing,
                baseY
            );
        }

        private static void RemoveToast(BaseNotificationForm toast)
        {
            ActiveToasts.Remove(toast);
            AdjustRemainingToasts();
            toast.Dispose();
        }

        private static void AdjustRemainingToasts()
        {
            var screen = Screen.PrimaryScreen.WorkingArea;
            int currentY = screen.Bottom - ToastSpacing;

            foreach (var toast in ActiveToasts.AsEnumerable().Reverse())
            {
                currentY -= toast.Height + ToastSpacing;
                toast.BeginInvoke(new Action(() =>
                {
                    toast.TargetY = currentY;
                    toast.StartSlideAnimation();
                }));
            }
        }
    }
}
