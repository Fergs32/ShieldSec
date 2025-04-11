using ShieldSec.Design;

namespace ShieldSec
{
    /// <summary>
    ///  The main entry point for the application.
    /// </summary>
    internal static class Program
    {
        /// <summary>
        ///  The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);

            var splash = new LoadingScreen();
            splash.Show();
            Application.DoEvents();

            var mainForm = new MainScreen();

            // Show the main form after the splash screen has been loaded (async)
            mainForm.Shown += async (sender, e) =>
            {
                await mainForm.InitializeAsync(splash);
                splash.Invoke(new Action(() => splash.Close()));
                mainForm.Enabled = true;
                mainForm.Visible = true;
            };

            Application.Run(mainForm);
        }
    }
}