using System;
using System.IO;
using System.Windows.Forms;
using System.Reflection;

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

            // Sets the working directory to MSCMM install path
            Directory.SetCurrentDirectory(Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location));

            // Loading the localisation file (English (UK).po by default)
            Localisation.LoadLocaleFile();

            if (args.Length > 0)
            {
                switch (args[0])
                {
                    case "wipe":
                        Settings.WipeAll();
                        MessageBox.Show(Localisation.Get("All your settings have been wiped. Restart the MSCMM without 'wipe' argument."), 
                            Localisation.Get("Information"), 
                            MessageBoxButtons.OK, 
                            MessageBoxIcon.Information);
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
