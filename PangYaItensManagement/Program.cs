using System;
using System.Windows.Forms;

namespace PangYaItensManagement
{
    static class Program
    {
        /// <summary>
        /// Start point of the app
        /// </summary>
        [STAThread]
        static void Main()
        {
            DrawLoginForm();
        }

        static void DrawLoginForm()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new Form_Login());
        }
    }
}
