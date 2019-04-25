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
        /// <summary>
        /// Checks if desktop shortcut exists.
        /// </summary>
        /// <returns></returns>
        public static bool ShortcutExist()
        {
            if (System.IO.File.Exists($"{Environment.GetFolderPath(Environment.SpecialFolder.Desktop)}\\My Summer Car Music Manager.lnk"))
                return true;

            return false;
        }

        /// <summary>
        /// Creates a new desktop shortcut.
        /// </summary>
        public static void Create()
        {
            string link = Environment.GetFolderPath(Environment.SpecialFolder.Desktop) + Path.DirectorySeparatorChar + Application.ProductName + ".lnk";
            var shell = new WshShell();
            var shortcut = shell.CreateShortcut(link) as IWshShortcut;
            shortcut.TargetPath = Application.ExecutablePath;
            shortcut.WorkingDirectory = Application.StartupPath;
            shortcut.Description = "Add, Remove and Download Songs!";
            shortcut.Save();
        }
    }
}
