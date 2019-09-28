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
using System.Net;
using System.IO;
using System.Diagnostics;
using System.Windows.Forms;
using System.IO.Compression;
using System.Threading.Tasks;
using System.Globalization;

namespace OggConverter
{
    class Updates
    {
        /// <summary>
        /// Stores the update version used to check if there's a newer version available
        /// 
        /// Pattern: YYWWB
        /// YY - year (ex. 19 for 2019)
        /// WW - week (ex. 18 for 18th week of year)
        /// B - build of this week
        /// </summary>
        public const int version = 19410;

        static bool newUpdateReady;
        static bool newPreviewReady;
        static bool downgrade;

        // Download sources
#if DEBUG
        const string stable = "file:///C:/Users/aathl/source/repos/msc-ogg/";
        const string preview = "file:///C:/Users/aathl/source/repos/msc-ogg/";
#else
        const string stable = "https://gitlab.com/aathlon/msc-ogg/raw/master/";
        const string preview = "https://gitlab.com/aathlon/msc-ogg/raw/development/";
#endif

        public static bool IsYoutubeDlUpdating { get; set; }

        public static bool IsBusy { get; set; }

        const string updaterScript = "@echo off\n" +
            "echo Installing the update...\n" +
            "TASKKILL /IM \"MSC Music Manager.exe\"\n" +
            "xcopy /s /y \"%cd%\\update\" \"%cd%\"\n" +
            "echo Finished! Starting MSC Music Manager\n" +
            "start \"\" \"MSC Music Manager.exe\"\n" +
            "exit";

        const string restartScript = "@echo off\n" +
            "TASKKILL /IM \"MSC Music Manager.exe\"\n" +
            "start \"\" \"MSC Music Manager.exe\"\n" +
            "exit";

        /// <summary>
        /// Starts update checking
        /// </summary>
        public static async void StartUpdateCheck()
        {
            if (Settings.Preview)
                await Task.Run(() => LookForAnUpdate(true));

            await Task.Run(() => LookForAnUpdate(false));
            await Task.Run(() => LookForYoutubeDlUpdate());
        }

        /// <summary>
        /// Checks for the update on remote server by downloading the latest version info file.
        /// </summary>
        static async Task LookForAnUpdate(bool getPreview)
        {
            if (Settings.DemoMode || !Utilities.IsOnline()) return;
            if (!getPreview && newPreviewReady) return;

            if (newUpdateReady)
            {
                DialogResult res = MessageBox.Show(Localisation.Get("There's a new update ready to download. Would you like to download it now?"), 
                    Localisation.Get("Update"), 
                    MessageBoxButtons.YesNo, 
                    MessageBoxIcon.Information);
                Form1.instance.Log(Localisation.Get("\nThere's an update ready to download!"));
                if (res == DialogResult.Yes)
                    DownloadUpdate(getPreview);

                return;
            }

            string latestURL = (getPreview ? preview : stable) + "latest.txt";

            try
            {
                using (WebClient client = new WebClient())
                {
                    await Task.Run(() => client.DownloadStringAsync(new Uri(latestURL)));
                    client.DownloadStringCompleted += (s, e) => 
                    {
                        int latest = int.Parse(e.Result);

                        if (latest > version)
                        {
                            string msg = Settings.Preview && !getPreview ? Localisation.Get("There's a newer stable version available to download than yours Preview. Would you like to download the update?") :
                                Localisation.Get("There's a new update ready to download. Would you like to download it now?");
                            msg += Localisation.Get("\n\nYour version: {0}\nNewest version: {1}", version, latest);
                            DialogResult res = MessageBox.Show(msg, Localisation.Get("Update"), MessageBoxButtons.YesNo, MessageBoxIcon.Information);
                            if (res == DialogResult.Yes)
                            {
                                DownloadUpdate(getPreview);
                                return;
                            }

                            newPreviewReady = Settings.Preview && getPreview;
                            newUpdateReady = true;
                            Form1.instance.Log(Localisation.Get("\nThere's an update ready to download!"));
                            Form1.instance.ButtonGetUpdate.Visible = true;
                            return;
                        }
                        else if ((latest < version) && (!Settings.Preview))
                        {
                            // DOWNGRADE MODE
                            downgrade = true;

                            DialogResult res = MessageBox.Show(
                                Localisation.Get("Looks like you use a preview release and you disable preview update channel. " +
                                "Do you want to downgrade now?\n\n" +
                                "WARNING: In order to keep things still working, all settings will be reset." +
                                "\n\nYour version: {0}\nNewest version: {1}", version, latest),
                                Localisation.Get("Question"),
                                MessageBoxButtons.YesNo,
                                MessageBoxIcon.Question);

                            if (res == DialogResult.Yes)
                                DownloadUpdate(getPreview);

                            newUpdateReady = true;
                            Form1.instance.Log(Localisation.Get("\nYou can downgrade now."));
                            Form1.instance.ButtonGetUpdate.Visible = true;
                        }

                        if (Settings.Preview && !getPreview) return;
                        Form1.instance.Log(Localisation.Get("\nTool is up-to-date"));
                    };
                }
            }
            catch (Exception ex)
            {
                Logs.CrashLog(ex.ToString(), true);
                Form1.instance.Log(Localisation.Get("\nCouldn't download the latest version info. Visit https://gitlab.com/aathlon/msc-ogg and see if there has been an update.\n" +
                    "In case the problem still occures, a new crash log has been created.\n"));
                return;
            }    
        }        

        /// <summary>
        /// Downloads and installs the latest update.
        /// </summary>
        public static async void DownloadUpdate(bool getPreview)
        {
            IsBusy = true;
            Form1.instance.Log(Localisation.Get("\nDownloading an update..."));
            Form1.instance.ButtonGetUpdate.Visible = false;

            string zipURL = (getPreview ? preview : stable) + "mscmm_update.zip";

            using (WebClient client = new WebClient())
            {
                client.DownloadProgressChanged += (s, e) =>
                {
                    Form1.instance.DownloadProgress.Invoke(new Action(() =>
                    { 
                        Form1.instance.DownloadProgress.Visible = true;
                        Form1.instance.DownloadProgress.Value = e.ProgressPercentage;
                    }));
                    return;
                };

                await Task.Run(() => client.DownloadFileAsync(new Uri(zipURL), "mscmm.zip"));

                client.DownloadFileCompleted += (s, e) =>
                {
                    Form1.instance.Log(Localisation.Get("Extracting..."));
                    Directory.CreateDirectory("update");
                    ZipFile.ExtractToDirectory("mscmm.zip", "update");

                    Form1.instance.Log(Localisation.Get("Installing..."));
                    File.WriteAllText("updater.bat", updaterScript);

                    if (downgrade)
                        Settings.WipeAll();

                    IsBusy = false;

                    Process process = new Process();
                    process.StartInfo.WindowStyle = ProcessWindowStyle.Hidden;
                    process.StartInfo.FileName = "updater.bat";
                    process.Start();
                    Application.Exit();
                };
            }            
        }

        /// <summary>
        /// Downloads youtube-dl directly from youtube-dl servers
        /// </summary>
        /// <returns></returns>
        public static async Task GetYoutubeDl()
        {
            IsYoutubeDlUpdating = true;
            Form1.instance.Log(Localisation.Get("\nDownloading youtube-dl..."));
            try
            {
                using (WebClient web = new WebClient())
                {
                    web.DownloadProgressChanged += (s, e) =>
                    {
                        Form1.instance.DownloadProgress.Invoke(new Action(() =>
                        {
                            Form1.instance.DownloadProgress.Visible = true;
                            Form1.instance.DownloadProgress.Value = e.ProgressPercentage;
                        }));
                        return;
                    };

                    await Task.Run(() => web.DownloadFileAsync(new Uri("https://yt-dl.org/latest/youtube-dl.exe"), "youtube-dl.exe"));

                    web.DownloadFileCompleted += (s, e) =>
                    {
                        Form1.instance.DownloadProgress.Invoke(new Action(() => Form1.instance.DownloadProgress.Visible = false));
                        IsYoutubeDlUpdating = false;
                        Form1.instance.Log(Localisation.Get("youtube-dl downloaded successfully!"));
                        Form1.instance.Invoke(new Action(() => Form1.instance.RestrictedMode(false)));
                    };
                }
            }
            catch (Exception ex)
            {
                Form1.instance.Log(Localisation.Get("Couldn't download youtube-dl. Crash log has been created."));
                Logs.CrashLog(ex.ToString());
                IsYoutubeDlUpdating = false;
                return;
            }
        }

        /// <summary>
        /// Starts the GetYoutubeDlUpdate void
        /// </summary>
        /// <param name="force">Skips the same date test.</param>
        public static async Task LookForYoutubeDlUpdate(bool force = false)
        {
            if (Settings.DemoMode || !Utilities.IsOnline() || !File.Exists("youtube-dl.exe")) return;

            if (force)
            {
                await GetYoutubeDlUpdate();
                return;
            }

            switch (Settings.YouTubeDlUpdateFrequency)
            {
                // No updates by default
                default:
                    return;
                // Upon every start
                case 0:
                    await GetYoutubeDlUpdate();
                    break;
                // Daily
                case 1:
                    if (Settings.LastYTDLUpdateCheck.Day != DateTime.Now.Day)
                        await GetYoutubeDlUpdate();
                    break;
                // Weekly
                case 2:
                    Calendar cal = CultureInfo.InvariantCulture.Calendar;
                    if (cal.GetWeekOfYear(DateTime.Now, CalendarWeekRule.FirstDay, DayOfWeek.Monday) >
                        cal.GetWeekOfYear(Settings.LastYTDLUpdateCheck, CalendarWeekRule.FirstDay, DayOfWeek.Monday) ||
                        DateTime.Now.Year != Settings.LastYTDLUpdateCheck.Year)
                        await GetYoutubeDlUpdate();
                    break;
                // Monthly
                case 3:
                    if (DateTime.Now.Month > Settings.LastYTDLUpdateCheck.Month ||
                        DateTime.Now.Year != Settings.LastYTDLUpdateCheck.Year)
                        await GetYoutubeDlUpdate();
                    break;
            }
        }

        /// <summary>
        /// Starts youtube-dl with -U parameter which makes it check for new updates directly from youtube-dl server
        /// </summary>
        /// <returns></returns>
        static async Task GetYoutubeDlUpdate()
        {
            IsYoutubeDlUpdating = true;
            Form1.instance.Log(Localisation.Get("\nLooking for youtube-dl updates..."));
            Process process = new Process();
            process.StartInfo.WindowStyle = ProcessWindowStyle.Hidden;
            process.StartInfo.FileName = "youtube-dl.exe";
            process.StartInfo.Arguments = "-U";
            process.Start();
            await Task.Run(() => process.WaitForExit());
            Form1.instance.Log(Localisation.Get("youtube-dl is up-to-date!"));
            IsYoutubeDlUpdating = false;
            Settings.LastYTDLUpdateCheck = DateTime.Now;
        }

        /// <summary>
        /// Starts ffmpeg and ffplay download
        /// </summary>
        public static async void StartFFmpegDownload()
        {
            IsBusy = true;
            Form1.instance.Log(Localisation.Get("\nDownloading ffmpeg and ffplay..."));
            await Task.Run(() => GetFFmpeg());
        }

        /// <summary>
        /// Downloads ffmpeg and ffplay from MSCMM's Git repo
        /// </summary>
        /// <returns></returns>
        static async Task GetFFmpeg()
        {
            using (WebClient client = new WebClient())
            {
                client.DownloadProgressChanged += (s, e) =>
                {
                    Form1.instance.DownloadProgress.Invoke(new Action(() =>
                    {
                        Form1.instance.DownloadProgress.Visible = true;
                        Form1.instance.DownloadProgress.Value = e.ProgressPercentage;
                    }));
                    return;
                };

                string link = (Settings.Preview ? preview : stable) + "Dependencies/ffpack.zip";

                try
                {
                    await Task.Run(() => client.DownloadFileAsync(new Uri(link), "ffpack.zip"));
                }
                catch 
                {
                    MessageBox.Show(Localisation.Get("Looks like there was some kind of problem with downloading the FFmpeg pack. " +
                        "You'll be taken to the website from which you'll" +
                        " download the ffpack.zip. Unzip it into the root folder of MSCMM."));
                    Process.Start("https://gitlab.com/aathlon/msc-ogg/raw/ab9cb011a283f316d56a4ce11b32558887a6fe39/Dependencies/ffpack.zip?inline=false");
                }

                client.DownloadFileCompleted += (s, e) =>
                {
                    ZipFile.ExtractToDirectory("ffpack.zip", Directory.GetCurrentDirectory());
                    Form1.instance.DownloadProgress.Invoke(new Action(() =>
                    {
                        Form1.instance.DownloadProgress.Visible = false;
                    }));

                    File.WriteAllText("restart.bat", restartScript);
                    IsBusy = false;

                    Process process = new Process();
                    process.StartInfo.WindowStyle = ProcessWindowStyle.Hidden;
                    process.StartInfo.FileName = "restart.bat";
                    process.Start();
                    Application.Exit();
                };
            }
        }
    }
}
