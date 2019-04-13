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
    class CrashLog
    {
        /// <summary>
        /// Dumps the crash log to the file.
        /// </summary>
        /// <param name="log">Crash log content.</param>
        /// <param name="silent">If true, doesn't display crash dialog.</param>
        public CrashLog(string log, bool silent = false)
        {
            // Logs disabled? Don't save it
            if (!Settings.Logs) return;

            string date = $"{DateTime.Now.Date.ToShortDateString()} {DateTime.Now.Hour.ToString()}.{DateTime.Now.Minute.ToString()}.{DateTime.Now.Second.ToString()}";
            string thisVersion = Application.ProductVersion;
            string fileName = @"LOG\" + date + ".txt";

            Directory.CreateDirectory("LOG");
            File.WriteAllText(fileName,
                $"MSC Music Manager {thisVersion} ({Updates.version})\n\n{FriendlyName()}\n\n{log}");

            if (silent) return;

            DialogResult dl = MessageBox.Show("An error has occured. Log has been saved into LOG folder. " +
                "Would you like to open it now?", "Error", MessageBoxButtons.YesNo, MessageBoxIcon.Error);
            if (dl == DialogResult.Yes) Process.Start(fileName);
        }

        string HKLM_GetString(string path, string key)
        {
            try
            {
                RegistryKey rk = Registry.LocalMachine.OpenSubKey(path);
                if (rk == null) return "";
                return (string)rk.GetValue(key);
            }
            catch { return ""; }
        }

        public string FriendlyName()
        {
            string ProductName = HKLM_GetString(@"SOFTWARE\Microsoft\Windows NT\CurrentVersion", "ProductName");
            string CSDVersion = HKLM_GetString(@"SOFTWARE\Microsoft\Windows NT\CurrentVersion", "CSDVersion");
            string releaseID = HKLM_GetString(@"SOFTWARE\Microsoft\Windows NT\CurrentVersion", "BuildLab");
            if (ProductName != "")
                return (ProductName.StartsWith("Microsoft") ? "" : "Microsoft ") + ProductName + (CSDVersion != "" ? " " + CSDVersion : "") + "\n" + releaseID;

            return "";
        }
    }
}
