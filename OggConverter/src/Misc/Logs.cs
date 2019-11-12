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
using System.Diagnostics;
using Microsoft.Win32;

namespace OggConverter
{
    class Logs
    {
        /// <summary>
        /// Dumps operations on the file to history.txt
        /// </summary>
        public static void History(string value)
        {
            if (!Settings.History)
                return;

            File.AppendAllText("history.txt", $"({DateTime.Now.ToString()}) {value}\n");
        }

        /// <summary>
        /// Dumps the crash log to the file.
        /// </summary>
        /// <param name="log">Crash log content.</param>
        /// <param name="silent">If true, doesn't display crash dialog.</param>
        public static void CrashLog(string log, bool silent = false)
        {
            // Logs disabled? Don't save it
            if (!Settings.Logs)
                return;

            string date = $"{DateTime.Now.Date.Day}.{DateTime.Now.Date.Month}.{DateTime.Now.Date.Year} " +
                $"{DateTime.Now.Hour.ToString()}.{DateTime.Now.Minute.ToString()}.{DateTime.Now.Second.ToString()}";
            string thisVersion = Application.ProductVersion;
            string fileName = $"LOG\\{date}.txt";

            Directory.CreateDirectory("LOG");
            File.WriteAllText(fileName,
                $"// MSC Music Manager //\n" +
                $"// VERSION: {thisVersion} ({Updates.version})\n" +
                $"// SYSTEM: {GetSystemInfo()}\n" +
                $"// MSCMM DIRECTORY: {Directory.GetCurrentDirectory()}\n" +
                $"// GAME DIRECTORY: {Settings.GamePath}\n" +
                $"// TIME OF CRASH: {DateTime.Now}\n" +
                $"// LANGUAGE: {Settings.Language}\n\n" +
                $"// {GetWittyComment()}\n\n" +
                $"{log}");

            Settings.LastCrashLogFile = $"{date}.txt";

            if (silent)
                return;

            DialogResult dl = MessageBox.Show(Localisation.Get("An error has occured. Log has been saved into LOG folder. " +
                "Would you like to open it now?"), Localisation.Get("Error"), MessageBoxButtons.YesNo, MessageBoxIcon.Error);
            if (dl == DialogResult.Yes) Process.Start(fileName);
        }


        static string GetSystemInfo()
        {
            string productName, releaseID = null;
            using (RegistryKey Key = Registry.LocalMachine.OpenSubKey(@"SOFTWARE\Microsoft\Windows NT\CurrentVersion"))
            {
                productName = Key.GetValue("ProductName").ToString();
                releaseID = Key.GetValue("BuildLab").ToString();
            }
            return (productName.StartsWith("Microsoft") ? "" : "Microsoft ") + productName + " (" + releaseID + ")";
        }

        static string GetWittyComment()
        {
            string[] messages = new String[] { "Oops!", "My bad", ":(", "Sorry :(", "Well this is awkwkard...",
                "A team of higly trained monkeys has been dispatched!", "D'oh!",
                "What, what, what, what, what, what, what, what, what?", "Move along, move along...", "I'm sorry Dave",
                "Allan, please add details!", "Little error punk!", "Huh?", "Great Scott!" };
            var random = new Random();
            int index = random.Next(messages.Length);
            return messages[index];
        }
    }
}
