using System;
using System.Windows.Forms;

namespace OggConverter
{
    static class Program
    {
        /// <summary>
        /// Główny punkt wejścia dla aplikacji.
        /// </summary>
        [STAThread]
        static void Main(string[] args)
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            if (args.Length > 0)
            {
                Application.Run(new QuickConvert(args));
                return;
            }
            Application.Run(new Form1());
        }
    }
}
