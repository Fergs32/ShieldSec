using Moq;
using ShieldSec.Core.Enums;
using ShieldSec.Core.Interfaces;
using ShieldSec.Core.Managers;
using ShieldSec.Design;
using System.Drawing;
using System.Reflection;
using System.Windows.Forms;
using Tests.Core.Helpers;

namespace Tests.Core.Managers
{
    /// <summary>
    ///  This class is responsible for testing the NotificationManagerTests class.
    ///  It uses "Arrange, Act, Assert" pattern to test the NotificationManagerTests class.
    ///  Which means that it first arranges the test environment, then acts on the test subject, and finally asserts the expected results.
    /// </summary>
    [TestClass]
    public class NotificationManagerTests
    {
        private Form _mainForm;
        private List<BaseNotificationForm> _activeToasts;

        /// <summary>
        ///  Initializes the test environment before each test.
        /// </summary>
        [TestInitialize]
        public void TestInitialize()
        {
            _mainForm = new Form();
            _mainForm.Show();

            _activeToasts = GetActiveToasts();
            _activeToasts.Clear();
        }
        /// <summary>
        ///  Cleans up the test environment after each test, this is implemented so it doesnt cause any issues with the tests and main application.
        /// </summary>
        [TestCleanup]
        public void TestCleanup()
        {
            _mainForm.Close();
            Application.OpenForms.Cast<Form>().ToList().ForEach(f => f.Close());
        }
        /// <summary>
        ///  Gets the active toasts from the NotificationManager using reflection.
        /// </summary>
        /// <returns> List of active toasts</returns>
        private List<BaseNotificationForm> GetActiveToasts()
        {
            return (List<BaseNotificationForm>)typeof(NotificationManager)
                .GetField("ActiveToasts", BindingFlags.NonPublic | BindingFlags.Static)
                .GetValue(null);
        }
        /// <summary>
        ///  Tests if the ShowToast method adds a new toast to the active toasts list.
        /// </summary>
        [TestMethod]
        public void ShowToast_WhenCalled_AddsToActiveToasts()
        {
            // Act
            NotificationManager.ShowToast("Test", "file.txt", "C:\\test.txt", NotificationType.TOAST);
            var activeToasts = GetActiveToasts();

            // Assert
            Assert.AreEqual(1, activeToasts.Count);
            Assert.IsInstanceOfType(activeToasts[0], typeof(ToastNotificationForm));
        }
        /// <summary>
        ///  Tests if the RemoveToast method removes the toast from the active toasts list.
        /// </summary>
        [TestMethod]
        public void RemoveToast_WhenCalled_RemovesFromActiveToasts()
        {
            // Arrange
            var toast = new ToastNotificationForm("Test", "file.txt", "C:\\test.txt");
            _activeToasts.Add(toast);

            // Act
            typeof(NotificationManager)
                .GetMethod("RemoveToast", BindingFlags.NonPublic | BindingFlags.Static)
                .Invoke(null, new[] { toast });

            // Assert
            Assert.AreEqual(0, _activeToasts.Count);
        }
        /// <summary>
        ///  Checks if the toast is disposed when RemoveToast is called, this is for memory management.
        /// </summary>
        [TestMethod]
        public void RemoveToast_WhenCalled_ClosesToast()
        {
            // Arrange
            var toast = new ToastNotificationForm("Test", "file.txt", "C:\\test.txt");
            _activeToasts.Add(toast);

            // Act
            typeof(NotificationManager)
                .GetMethod("RemoveToast", BindingFlags.NonPublic | BindingFlags.Static)
                .Invoke(null, new[] { toast });

            // Assert
            Assert.IsTrue(toast.IsDisposed);
        }
    }
}

