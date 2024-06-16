using System;
using System.Windows.Forms;

namespace Proiect1
{
    static class Program
    {
        [STAThread]
        static void Main()
        {
            System.Text.Encoding.RegisterProvider(System.Text.CodePagesEncodingProvider.Instance);
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new Form1());
        }
    }
}
