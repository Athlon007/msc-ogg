// MSC Music Manager
// Copyright(C) 2019 Athlon

// This program is free software: you can redistribute it and/or modify
// it under the terms of the GNU General Public License as published by
// the Free Software Foundation, either version 3 of the License, or
// (at your option) any later version.

// This program is distributed in the hope that it will be useful,
// but WITHOUT ANY WARRANTY; without even the implied warranty of
// MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.See the
// GNU General Public License for more details.

// You should have received a copy of the GNU General Public License
// along with this program.If not, see<http://www.gnu.org/licenses/>.

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
                    default:
                        Application.Run(new QuickConvert(args));
                        break;
                    case "wipe":
                        Settings.WipeAll();
                        MessageBox.Show(Localisation.Get("All your settings have been wiped. Restart the MSCMM without 'wipe' argument."), 
                            Localisation.Get("Information"), 
                            MessageBoxButtons.OK, 
                            MessageBoxIcon.Information);
                        Application.Exit();
                        break;
                    case "startgame":
                        CustomStartGame.Play();
                        break;
                }
                return;
            }
            Application.Run(new Form1());
        }
    }
}
