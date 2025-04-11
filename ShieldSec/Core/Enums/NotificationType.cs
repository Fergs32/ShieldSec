using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShieldSec.Core.Enums
{
    /// <summary>
    ///  Enum for the notification type.
    /// </summary>
    public enum NotificationType
    {
        GENERIC,
        TOAST
    }
    /// <summary>
    ///  This class is responsible for the notification type helper.
    /// </summary>
    public class NotificationTypeHelper
    {
        /// <summary>
        ///  Gets the notification type.
        /// </summary>
        /// <param name="notificationType"> The notification type. </param>
        /// <returns> The notification type. </returns>
        public static NotificationType GetNotificationType(string notificationType)
        {
            switch (notificationType)
            {
                case "GENERIC":
                    return NotificationType.GENERIC;
                case "TOAST":
                    return NotificationType.TOAST;
                default:
                    return NotificationType.GENERIC;
            }
        }
    }
}
