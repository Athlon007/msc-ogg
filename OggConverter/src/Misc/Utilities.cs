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

using System.Reflection;
using System;
using System.Windows.Forms;
using System.IO;
using System.Diagnostics;
using System.Net;
using System.Threading;

namespace OggConverter
{
    static class Utilities
    {
        /// <summary>
        /// Checks if string contains any extension in the file name.
        /// </summary>
        /// <param name="haystack">File name</param>
        /// <param name="needles">Extensions that we want to check if they are in the file name.</param>
        /// <returns></returns>
        public static bool ContainsAny(this string haystack, params string[] needles)
        {
            foreach (string needle in needles)
                if (haystack.Contains(needle))
                    return true;

            return false;
        }

        /// <summary>
        /// Returns MSC Music Manager version
        /// </summary>
        /// <returns></returns>
        public static string GetVersion()
        {
            Version version = Assembly.GetExecutingAssembly().GetName().Version;
            return version.Build == 0 ? $"{version.Major}.{version.Minor}" : $"{version.Major}.{version.Minor}.{version.Build}";
        }

        public static string AboutNotice = $"MSC Music Manager {Application.ProductVersion} ({Updates.version})\nCopyright (C) 2019 Athlon\n\n" +
                $"This program comes with ABSOLUTELY NO WARRANTY.\n" +
                $"This is free software, and you are welcome to redistribute it, as long as you include original copyright, state changes and include license.\n\n" +
                $"MSC Music Manager uses FFmpeg, which is licensed under LGPL 2.1 license.";

        /// <summary>
        /// Checks if file is being used by other process and returns the value.
        /// </summary>
        /// <param name="filename">Path to the file</param>
        /// <returns>If any process is in FileStream, returns true (as it's used). Else it returns false (file is free to use)</returns>
        public static bool IsFileReady(string filename)
        {
            // If the file can be opened for exclusive access it means that the file
            // is no longer locked by another process.
            try
            {
                using (FileStream inputStream = File.Open(filename, FileMode.Open, FileAccess.Read, FileShare.None))
                    return inputStream.Length > 0;
            }
            catch
            {
                return false;
            }
        }

        static readonly string[] filesToDelete = new string[]
        {
            "NReco.VideoConverter.dll", "updater.bat", "restart.bat",
            "mscmm.zip", "ffpack.zip", "MSC OGG.exe", "LastConversion.txt"
        };

        /// <summary>
        /// Removes temporary/unused files
        /// </summary>
        public static void Cleanup()
        {
            foreach (string file in filesToDelete)
            {
                if (File.Exists(file))
                    File.Delete(file);
            }

            if (Directory.Exists("update"))
                Directory.Delete("update", true);

            string[] files = Directory.GetFiles(Directory.GetCurrentDirectory(), "download*.*");
            foreach (string file in files)
                File.Delete(file);            
        }

        /// <summary>
        /// Checks if any 'long taking' operations are busy.
        /// </summary>
        /// <returns></returns>
        public static bool IsToolBusy() { return Downloader.IsBusy || Converter.IsBusy || Updates.IsYoutubeDlUpdating || Updates.IsBusy || Player.IsBusy; }

        /// <summary>
        /// Centers horizontally the sender according to reference control
        /// </summary>
        /// <param name="sender">Object to center</param>
        /// <param name="reference">Reference (ex.: parental panel)</param>
        /// <returns></returns>
        public static int CenterHorizontally(this Control sender, Control reference) { return (reference.Width - sender.Width) / 2; }

        /// <summary>
        /// Launches the game - either with Steam or directly from mysummercar.exe
        /// </summary>
        public static void LaunchGame() { Process.Start(Settings.NoSteam ? $"{Settings.GamePath}\\mysummercar.exe" : "steam://rungameid/516750"); }

        // Prevents 'Looks like you're offline.' message from appearing twice if user's using Preview update channel (and prevents useless network traffic)
        static bool isOffline;

        /// <summary>
        /// Connects to GitLab and checks if it's (or computer) is online
        /// </summary>
        public static bool IsOnline()
        {
            if (isOffline)
                return false;

            try
            {
                using (WebClient client = new WebClient())
                {
                    using (client.OpenRead("https://gitlab.com/aathlon/msc-ogg"))
                    {
                        return true;
                    }
                }
            }
            catch
            {
                Form1.instance.Log("Looks like you're offline. Can't check for the update availability");
                isOffline = true;
                return false;
            }
        }
    }
}
