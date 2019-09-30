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

using System.Threading.Tasks;
using System.IO;
using System.Windows.Forms;
using System.Diagnostics;
using System;

namespace OggConverter
{
    class Downloader
    {
        public static bool IsBusy { get; set; }
        public static bool CancelDownload { get; set; }

        /// <summary>
        /// Downloads the song as .ACC file
        /// </summary>
        /// <param name="url">URL link to video</param>
        /// <param name="folder">Radio or CD</param>
        /// <param name="limit">Radio = 99. CD = 15</param>
        /// <returns></returns>
        public static async Task DownloadFile(string url, string folder, int limit, string forcedName = null)
        {
            try
            {
                CancelDownload = false;

                Form1.instance.Log(Localisation.Get("Downloaded youtube-dl successfully!"));
                Form1.instance.DownloadProgress.Visible = false;

                if (!url.ContainsAny("https://www.youtube.com/watch?v=", "https://youtube.com/watch?v=", "ytsearch:"))
                {
                    MessageBox.Show(Localisation.Get("Not a valid URL. Currently only YouTube is suported."), 
                        Localisation.Get("Error"), MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                IsBusy = true;

                if (File.Exists("download.aac"))
                    File.Delete("download.aac");

                Form1.instance.Log(Localisation.Get("\nDownloading song..."));
                Logs.History(Localisation.Get("Downloader: Downloading song from '{0}'", url));

                Process process = new Process();
                process.StartInfo.RedirectStandardOutput = true;
                process.StartInfo.UseShellExecute = false;
                process.StartInfo.CreateNoWindow = true;

                // Setup executable and parameters
                process.StartInfo.FileName = "youtube-dl.exe";
                process.StartInfo.Arguments = $"-f bestaudio -x --audio-format mp3 --audio-quality 0 -o \"download.%(ext)s\" {url}";

                if (CancelDownload)
                {
                    CancelDownload = true;
                    return;
                }

                await Task.Run(() => process.Start());
                string[] youtubeDllOutput = null;
                await Task.Run(() => youtubeDllOutput = process.StandardOutput.ReadToEnd().Split('\n'));
                await Task.Run(() => process.WaitForExit());
                Logs.History("-- youtube-dl output: --\n" + string.Join("\n", youtubeDllOutput));

                // File wasn't downloaded?
                if (!File.Exists("download.mp3"))
                {
                    Form1.instance.Log(Localisation.Get("Couldn't donwnload the song.\n" +
                        "If you canceled the download, then everything's fine.\n" +
                        "If not, check if there's a youtube-dl update, by clicking Tool -> Check for youtube-dl update.\n" +
                        "Also please check if youtube-dl supports the link that you use: " +
                        "https://ytdl-org.github.io/youtube-dl/supportedsites.html. \n" +
                        "If it's going to happen again, please send the history.txt file content to developer on Steam, or via mail."));
                    IsBusy = false;
                    Form1.instance.RestrictedMode(false);
                    return;
                }

                Form1.instance.Log(Localisation.Get("Converting..."));

                if (CancelDownload)
                {
                    CancelDownload = true;
                    return;
                }

                await Converter.ConvertFile($"{Directory.GetCurrentDirectory()}\\download.mp3", folder, limit, forcedName, true);

                File.Delete("download.mp3");
                IsBusy = false;

                Form1.instance.UpdateSongList();
                Form1.instance.RestrictedMode(false);
            }
            catch (Exception) when (CancelDownload)
            {
                Form1.instance.Log(Localisation.Get("Canceled!"));
            }
        }

        public static void Cancel()
        {
            if (!IsBusy) return;

            CancelDownload = true;

            IsBusy = false;
            foreach (var process in Process.GetProcessesByName("youtube-dl"))
            {
                process.Kill();
            }

            foreach (var process in Process.GetProcessesByName("ffmpeg"))
            {
                process.Kill();
            }

            DirectoryInfo di = new DirectoryInfo(Directory.GetCurrentDirectory());
            FileInfo[] files = di.GetFiles("download*");
            foreach (FileInfo file in files)
            {
                if (File.Exists(file.FullName))
                    File.Delete(file.FullName);
            }
        }
    }
}
