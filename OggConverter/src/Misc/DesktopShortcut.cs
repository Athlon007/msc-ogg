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
using IWshRuntimeLibrary;
using System.IO;
using System.Windows.Forms;

namespace OggConverter
{
    class DesktopShortcut
    {
        static string DesktopPath { get => Environment.GetFolderPath(Environment.SpecialFolder.Desktop); }

        /// <summary>
        /// Checks if desktop shortcut exists.
        /// </summary>
        /// <returns></returns>
        public static bool Exists() { return System.IO.File.Exists($"{DesktopPath}\\My Summer Car Music Manager.lnk"); }

        public static bool CustomExists() { return System.IO.File.Exists($"{DesktopPath}\\Play My Summer Car.lnk"); }

        /// <summary>
        /// Creates a new desktop shortcut.
        /// </summary>
        public static void Create()
        {
            if (Exists()) return;

            string link = DesktopPath + Path.DirectorySeparatorChar + Application.ProductName + ".lnk";
            var shell = new WshShell();
            var shortcut = shell.CreateShortcut(link) as IWshShortcut;
            shortcut.TargetPath = Application.ExecutablePath;
            shortcut.WorkingDirectory = Application.StartupPath;
            shortcut.Description = Localisation.Get("Add, Remove and Download Songs!");
            shortcut.Save();
        }

        /// <summary>
        /// Creates custom shortcut.
        /// </summary>
        public static void CreateCustomShortcut()
        {
            if (CustomExists()) return;

            string link = DesktopPath + Path.DirectorySeparatorChar + "Play My Summer Car.lnk";
            var shell = new WshShell();
            var shortcut = shell.CreateShortcut(link) as IWshShortcut;
            shortcut.Arguments = "startgame";
            shortcut.TargetPath = Application.ExecutablePath;
            shortcut.WorkingDirectory = Application.StartupPath;
            shortcut.Description = Localisation.Get("Shuffles all songs and starts the My Summer Car.");
            shortcut.Save();
        }

        /// <summary>
        /// Deletes the desktop shortcut.
        /// </summary>
        public static void Delete()
        {
            if (Exists())
            {
                System.IO.File.Delete($"{DesktopPath}\\My Summer Car Music Manager.lnk");
            }
        }
    }
}
