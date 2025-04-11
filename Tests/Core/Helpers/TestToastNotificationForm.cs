using ShieldSec.Design;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tests.Core.Helpers
{
    public class TestToastNotificationForm : ToastNotificationForm
    {
        public new int Height { get; set; }

        public TestToastNotificationForm(string title, string fileName, string fileLocation)
            : base(title, fileName, fileLocation)
        {
            Height = 100; 
        }

        public void SetTestHeight(int height) => Height = height;
    }
}
