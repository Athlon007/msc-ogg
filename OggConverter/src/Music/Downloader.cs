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
using System.Threading.Tasks;
using System.Net;
using System.IO;
using System.Windows.Forms;
using System.Diagnostics;

namespace OggConverter
{
    class Downloader
    {
        public static bool IsBusy { get; set; }

        /// <summary>
        /// Downloads the song as .ACC file
        /// </summary>
        /// <param name="url">URL link to video</param>
        /// <param name="folder">Radio or CD</param>
        /// <param name="limit">Radio = 99. CD = 15</param>
        /// <returns></returns>
        public static async Task DownloadFile(string url, string folder, int limit, string forcedName = null)
        {
            if (Functions.IsOnline()) return;

            if (Updates.IsYoutubeDlUpdating)
            {
                MessageBox.Show("youtube-dl is now updating or looking for the update. Try again in a few seconds.", "Stop", MessageBoxButtons.OK, MessageBoxIcon.Stop);
                return;
            }

            if (!File.Exists("youtube-dl.exe"))
            {
                DialogResult dl = MessageBox.Show("In order to download the song, the tool requires youtube-dl to be downloaded. Press 'Yes' to download it now", 
                    "Stop", 
                    MessageBoxButtons.YesNo, 
                    MessageBoxIcon.Stop);

                if (dl == DialogResult.Yes)
                {
                    Form1.instance.Log("\nDownloading youtube-dl...");
                    try
                    {
                        using (WebClient web = new WebClient())
                        {
                            await Task.Run(() => web.DownloadFile(new Uri("https://yt-dl.org/latest/youtube-dl.exe"), "youtube-dl.exe"));
                            web.Dispose();
                        }
                        Form1.instance.Log("Downloaded youtube-dl successfully!");
                    }
                    catch (Exception ex)
                    {
                        Form1.instance.Log("Couldn't download youtube-dl. Crash log has been created");
                        Logs.CrashLog(ex.ToString());
                        return;
                    }
                }
                else
                {
                    return;
                }
            }

            if (!url.ContainsAny("https://www.youtube.com/watch?v=", "https://youtube.com/watch?v=", "ytsearch:"))
            {
                MessageBox.Show("Not a valid URL. Currently only YouTube is suported.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            IsBusy = true;

            if (File.Exists("download.aac"))
                File.Delete("download.aac");

            Form1.instance.Log("\nDownloading song...");
            Logs.History($"Downloader: Downloading song from \"{url}\"");

            Process process = new Process();
            process.StartInfo.RedirectStandardOutput = true;
            process.StartInfo.UseShellExecute = false;
            process.StartInfo.CreateNoWindow = true;

            // Setup executable and parameters
            process.StartInfo.FileName = "youtube-dl.exe";
            process.StartInfo.Arguments = $"-f bestaudio -x --audio-format mp3 --audio-quality 0 -o \"download.%(ext)s\" {url}";
            process.Start();
            await Task.Run(() => process.WaitForExit());

            Form1.instance.Log("Converting...");
            await Converter.ConvertFile($"{Directory.GetCurrentDirectory()}\\download.mp3", folder, limit, forcedName);

            File.Delete("download.aac");
            IsBusy = false;

            Form1.instance.UpdateSongList();
        }
    }
}
