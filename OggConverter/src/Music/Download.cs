using System;
using System.Threading.Tasks;
using System.Net;
using System.IO;
using System.Windows.Forms;
using System.Diagnostics;

namespace OggConverter
{
    class Download
    {
        public static bool downloadingNow;

        /// <summary>
        /// Downloads the song as .ACC file
        /// </summary>
        /// <param name="url">URL link to video</param>
        /// <param name="mscPath">My Summer Car directory</param>
        /// <param name="folder">Radio or CD</param>
        /// <param name="limit">Radio = 99. CD = 15</param>
        /// <returns></returns>
        public static async Task DownloadFile(string url, string mscPath, string folder, int limit)
        {
            if (!File.Exists("youtube-dl.exe"))
            {
                DialogResult dl = MessageBox.Show("In order to download the song, you need to download youtube-dl. Press 'Yes' to download it now", 
                    "Stop", 
                    MessageBoxButtons.YesNo, 
                    MessageBoxIcon.Stop);

                if (dl == DialogResult.Yes)
                {
                    Form1.instance.Log += "\n\nDownloading youtube-dl...";
                    try
                    {
                        using (WebClient web = new WebClient())
                        {
                            await Task.Run(() => web.DownloadFile(new Uri("https://yt-dl.org/latest/youtube-dl.exe"), "youtube-dl.exe"));
                            web.Dispose();
                        }
                        Form1.instance.Log += "\nDownloaded youtube-dl successfully!";
                    }
                    catch (Exception ex)
                    {
                        Form1.instance.Log += "\nCouldn't download youtube-dl. Crash log has been created";
                        new CrashLog(ex.ToString());
                        return;
                    }
                }
                else
                {
                    return;
                }
            }

            if (!url.ContainsAny("https://www.youtube.com/watch?v=", "https://youtube.com/watch?v="))
            {
                MessageBox.Show("Not a valid URL.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            downloadingNow = true;

            if (File.Exists("download.aac"))
                File.Delete("download.aac");

            Form1.instance.Log += "\n\nDownloading song...";

            Process process = new Process();
            process.StartInfo.RedirectStandardOutput = true;
            process.StartInfo.UseShellExecute = false;
            process.StartInfo.CreateNoWindow = true;

            // Setup executable and parameters
            process.StartInfo.FileName = "youtube-dl.exe";
            process.StartInfo.Arguments = $"-x --audio-format aac -o+ \"download.%(ext)s\" {url}";
            process.Start();
            await Task.Run(() => process.WaitForExit());

            Form1.instance.Log += "\nConverting...";
            await Converter.ConvertFile($"{Directory.GetCurrentDirectory()}\\download.aac", mscPath, folder, limit);

            File.Delete("download.aac");
            downloadingNow = false;

            Form1.instance.UpdateSongList();
        }
    }
}
