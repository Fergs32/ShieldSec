using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace ShieldSec.Utils
{
    [StructLayout(LayoutKind.Sequential)]
    /// <summary>
    /// This class is responsible for getting the system folders.
    /// Reference: https://stackoverflow.com/questions/10667012/getting-downloads-folder-in-c
    /// </summary>
    public class SystemFolders
    {
        
        [DllImport("shell32.dll", CharSet = CharSet.Unicode, SetLastError = true)]
        private static extern int SHGetKnownFolderPath(
            [MarshalAs(UnmanagedType.LPStruct)] Guid rfid,
            uint dwFlags,
            IntPtr hToken,
            out IntPtr pszPath
        );

        private static readonly Guid DownloadsFolderGuid =
            new Guid("374DE290-123F-4565-9164-39C4925E467B");

        public static string GetDownloadsPath()
        {
            try
            {
                if (Environment.OSVersion.Platform == PlatformID.Win32NT)
                {
                    IntPtr pathPtr;
                    if (SHGetKnownFolderPath(DownloadsFolderGuid, 0, IntPtr.Zero, out pathPtr) == 0)
                    {
                        string path = Marshal.PtrToStringUni(pathPtr);
                        Marshal.FreeCoTaskMem(pathPtr);
                        return path;
                    }
                }
            }
            catch {  }

            return Path.Combine(
                Environment.GetFolderPath(Environment.SpecialFolder.UserProfile),
                "Downloads"
            );
        }
    }
}
