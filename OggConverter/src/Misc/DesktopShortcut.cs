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
        public static bool DoesShortcutExist()
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
            shortcut.Save();
        }
    }
}
