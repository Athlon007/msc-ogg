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
                int audioQuality = GetAudioQuality();
                process.StartInfo.Arguments = $"-f bestaudio -x --audio-format mp3 --audio-quality {audioQuality} -o \"download.%(ext)s\" {url}";

                if (CancelDownload)
                {
                    CancelDownload = true;
                    return;
                }

                process.OutputDataReceived += new DataReceivedEventHandler(OutputHandler);
                Form1.instance.ClearYtLog();

                await Task.Run(() => process.Start());
                process.BeginOutputReadLine();
                await Task.Run(() => process.WaitForExit());

                Logs.History("-- youtube-dl output: --\n" + Form1.instance.GetYtDlLog);
                Form1.instance.YoutubeDlLog("\n\n====================================\nDone!");

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

                // Forced name is empty? Try to get the name from link
                if (String.IsNullOrEmpty(forcedName))
                    forcedName = GetTitleFromYouTube(url);

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

        /// <summary>
        /// Cancels download. Kills youtube-dl and ffmpeg processes and removes downloads.
        /// </summary>
        public static void Cancel()
        {
            if (!IsBusy) return;

            CancelDownload = true;
            IsBusy = false;

            foreach (var process in Process.GetProcessesByName("youtube-dl"))
                process.Kill();

            foreach (var process in Process.GetProcessesByName("ffmpeg"))
                process.Kill();

            DirectoryInfo di = new DirectoryInfo(Directory.GetCurrentDirectory());
            FileInfo[] files = di.GetFiles("download*");
            foreach (FileInfo file in files)
            {
                while (!Utilities.IsFileReady(file.FullName)) { }
                if (File.Exists(file.FullName))
                    File.Delete(file.FullName);
            }
        }

        /// <summary>
        /// Redirects the output from youtube-dl to YouTubeDlLog void in Form1
        /// </summary>
        /// <param name="sendingProcess"></param>
        /// <param name="outLine"></param>
        public static void OutputHandler(object sendingProcess, DataReceivedEventArgs outLine)
        {
            try
            {
                string value = outLine.Data.ToString();
                Form1.instance.YoutubeDlLog(value);

                // Only read lines that contain percentage
                if (value.Contains("%"))
                {
                    string percentage = value.Split(']')[1].Split('%')[0].Trim();
                    if (percentage.Contains("."))
                        percentage = percentage.Split('.')[0];

                    string downloadSpeed = value.Split('t')[1].Trim();

                    Form1.instance.YtDownloadProgress(int.Parse(percentage), downloadSpeed);
                }
            }
            catch { }
        }

        /// <summary>
        /// Gets song title from link.
        /// </summary>
        /// <param name="url"></param>
        /// <returns></returns>
        public static string GetTitleFromYouTube(string url)
        {
            Process process = new Process();
            process.StartInfo.RedirectStandardOutput = true;
            process.StartInfo.UseShellExecute = false;
            process.StartInfo.CreateNoWindow = true;

            // Setup executable and parameters
            process.StartInfo.FileName = "youtube-dl.exe";
            process.StartInfo.Arguments = $"--get-filename {url}";

            if (CancelDownload)
            {
                CancelDownload = true;
                return null;
            }

            process.Start();
            string[] youtubeDlOutput = process.StandardOutput.ReadToEnd().Split('\n');
            process.WaitForExit();

            // Added this so the program won't freeze when getting the song name
            while (!process.HasExited) { Application.DoEvents(); }

            string id = url.Split('=')[1];
            return youtubeDlOutput[0].Replace(id, "").Replace("-.mp4", "");
        }

        static int GetAudioQuality()
        {
            switch (Settings.YoutubeDlDownloadQuality)
            {
                default:
                    return 5;
                case 0:
                    return 0;
                case 1:
                    return 5;
                case 2:
                    return 9;
            }
        }
    }
}
