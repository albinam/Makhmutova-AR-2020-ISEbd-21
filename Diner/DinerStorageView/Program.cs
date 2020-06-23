using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace DinerStorageView
{
    static class Program
    {
        /// <summary>
        ///  The main entry point for the application.
        /// </summary>
        public static bool IsLogined { get; set; }
        [STAThread]
        static void Main()
        {
            APIStorage.Connect();
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            var form = new FormEnter();
            form.ShowDialog();
            if (IsLogined)
            {
                Application.Run(new FormMain());
            }
        }
    }
}
