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
using System.Collections.Generic;
using System.Linq;

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
        /// <param name="fullVersion">If true, returns the name with inclusion of revision number.</param>
        /// <returns></returns>
        public static string GetVersion(bool fullVersion = false)
        {
            Version version = Assembly.GetExecutingAssembly().GetName().Version;

            if (fullVersion)
                return $"{version.Major}.{version.Minor}.{version.Build}.{version.Revision}";

            return version.Build == 0 ? $"{version.Major}.{version.Minor}" : $"{version.Major}.{version.Minor}.{version.Build}";
        }

        public static string AboutNotice () => Localisation.Get("MSC Music Manager {0} ({1})\nCopyright (C) 2019 Athlon\n\n" +
            $"This program comes with ABSOLUTELY NO WARRANTY.\n" +
            $"This is free software, and you are welcome to redistribute it, " +
            $"as long as you include original copyright, state changes and include license.\n\n" +
            $"MSC Music Manager uses FFmpeg, which is licensed under LGPL 2.1 license.", Application.ProductVersion, Updates.version);

        /// <summary>
        /// Checks if file is being used by some other process.
        /// </summary>
        /// <param name="fileName">Path to the file</param>
        /// <returns>If any process is in FileStream, returns true (as it's used). Else it returns false (file is unlocked)</returns>
        public static bool IsFileReady(string fileName)
        {
            // If the file can be opened for exclusive access it means that the file
            // is no longer locked by another process.
            try
            {
                using (FileStream inputStream = File.Open(fileName, FileMode.Open, FileAccess.Read, FileShare.None))
                    return inputStream.Length > 0;
            }
            catch
            {
                return false;
            }
        }

        /// <summary>
        /// List of files to delete (old unused files, temps, etc.)
        /// </summary>
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

            if (Directory.Exists("downloads"))
                Directory.Delete("downloads", true);
        }

        /// <summary>
        /// Checks if any 'long taking' operations are busy.
        /// </summary>
        /// <returns></returns>
        public static bool IsToolBusy()
        {
            return Downloader.IsBusy || 
                Converter.IsBusy || 
                Updates.IsYoutubeDlUpdating || 
                Updates.IsBusy || 
                Player.IsBusy;
        }

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
        public static void LaunchGame()
        {
            Process.Start(Settings.NoSteam ? $"{Settings.GamePath}\\mysummercar.exe" : "steam://rungameid/516750");
        }

        // Prevents 'Looks like you're offline.' message from appearing twice,
        // if user's using Preview update channel (and prevents useless network traffic)
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
                Form1.instance.Log(Localisation.Get("Looks like you're offline. Can't check for the update availability"));
                isOffline = true;
                return false;
            }
        }

        public enum ArrayReturnValueSource { SongList, Name }

        /// <summary>
        /// Gets all items from ListBox and returns them into an array.
        /// </summary>
        /// <param name="listBox">songList</param>
        /// <returns>Array of all selected items</returns>
        public static string[] GetSelectedItemsToArray(ListBox listBox, ArrayReturnValueSource arrayReturnValueSource)
        {
            int[] domains = listBox.SelectedIndices.OfType<int>().ToArray();
            List<string> selectedItemsList = new List<string>();

            foreach (int i in domains)
            {
                string output = arrayReturnValueSource == ArrayReturnValueSource.SongList
                                ? Player.WorkingSongList[i].Item1.ToString()
                                : listBox.Items[i].ToString();

                selectedItemsList.Add(output);
            }

            return selectedItemsList.ToArray();
        }

        /// <summary>
        /// Checks if provided link is a valid https or http Url.
        /// </summary>
        /// <param name="url"></param>
        /// <returns></returns>
        public static bool IsValidUrl(this string url)
        {
            Uri uriResult;
            return Uri.TryCreate(url, UriKind.Absolute, out uriResult) 
                && (uriResult.Scheme == Uri.UriSchemeHttp || uriResult.Scheme == Uri.UriSchemeHttps);
        }

        /// <summary>
        /// Counts the files inside of folder and number for a file
        /// </summary>
        /// <param name="folder">Workind directory</param>
        /// <returns></returns>
        public static int GetNewFileNumber(string folder)
        {
            int newNumber = 1;
            for (int i = 1; File.Exists($"{Settings.GamePath}\\{folder}\\track{i}.ogg"); i++)
                newNumber++;

            return newNumber;
        }
    }
}
