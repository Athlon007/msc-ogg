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
                switch (args[0])
                {
                    case "wipe":
                        Settings.WipeAll();
                        MessageBox.Show("All your settings have been wiped. Restart the MSCMM without -wipe argument.", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        Application.Exit();
                        break;
                    default:
                        Application.Run(new QuickConvert(args));
                        break;
                }
                return;
            }
            Application.Run(new Form1());
        }
    }
}
