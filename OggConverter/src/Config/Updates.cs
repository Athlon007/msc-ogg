using System;
using System.Net;
using System.IO;
using System.Diagnostics;
using System.Windows.Forms;
using System.IO.Compression;

namespace OggConverter
{
    class Updates
    {
        // first two numbers - year, second two numbers - week, last digit - release number in this week. So the 17490 means year 2017, week 49, number of release in this week - 0
        public const int version = 18151; 
        static bool newUpdateReady;
        static bool downgrade;

        /// <summary>
        /// Checks for the update on remote server by downloading the latest version info file.
        /// </summary>
        public static void LookForAnUpdate()
        {
            if (newUpdateReady)
            {
                DialogResult res = MessageBox.Show("There's a new update ready to download. Would you like to download it now?", "Update", MessageBoxButtons.YesNo, MessageBoxIcon.Information);
                Form1.instance.Log += "\n\nThere's an update ready to download!";
                if (res == DialogResult.Yes)
                    DownloadUpdate();

                return;
            }

            string latestURL = Settings.Preview ? "https://gitlab.com/aathlon/msc-ogg/raw/development/latest.txt" : "https://gitlab.com/aathlon/msc-ogg/raw/master/latest.txt";

            try
            {
                using (WebClient client = new WebClient())
                {
                    client.DownloadFile(new Uri(latestURL), "latest.txt");
                    client.Dispose();
                }
            }
            catch (Exception ex)
            {
                new CrashLog(ex.ToString(), true);
                Form1.instance.Log += "\n\nCouldn't download the latest version info. Visit https://gitlab.com/aathlon/msc-ogg and see if there has been an update.\n" +
                    "In case the problem still occures, a new crash log has been created.";
                return;
            }

            int latest = int.Parse(File.ReadAllText("latest.txt"));
            File.Delete("latest.txt");

            if (latest > version)
            {
                DialogResult res = MessageBox.Show("There's a new update ready to download. Would you like to download it now?", "Update", MessageBoxButtons.YesNo, MessageBoxIcon.Information);
                if (res == DialogResult.Yes)
                {
                    DownloadUpdate();
                    return;
                }

                newUpdateReady = true;
                Form1.instance.Log += "\n\nThere's an update ready to download!";
                Form1.instance.btnGetUpdate.Visible = true;
                return;
            }
            else if (latest < version)
            {
                downgrade = true;

                DialogResult res = MessageBox.Show("Looks like you want to downgrade from preview build to stable?\n\n" +
                    "WARNING: In order to keep things still working, all settings will be reset.",
                    "Question",
                    MessageBoxButtons.YesNo,
                    MessageBoxIcon.Question);

                if (res == DialogResult.Yes)
                {
                    DownloadUpdate();
                }

                newUpdateReady = true;
                Form1.instance.Log += "\n\nYou can downgrade now.";
                Form1.instance.btnGetUpdate.Visible = true;

                return;
            }

            Form1.instance.Log += "\n\nTool is up-to-date";
        }

        const string updaterScript = "@echo off\necho Installing the update...\nTASKKILL /IM \"MSC Music Manager.exe\"\n" +
            "xcopy /s /y %cd%\\update %cd%\necho Finished! Starting MSC Music Manager\nstart \"\" \"MSC Music Manager.exe\"\nexit";

        /// <summary>
        /// Downloads and installs the latest update.
        /// </summary>
        public static void DownloadUpdate()
        {
            Form1.instance.Log += "\n\nDownloading an update...";

            string zipURL = Settings.Preview ? "https://gitlab.com/aathlon/msc-ogg/raw/development/mscmm.zip" : "https://gitlab.com/aathlon/msc-ogg/raw/master/mscmm.zip";

            using (WebClient client = new WebClient())
            {
                client.DownloadFile(new Uri(zipURL), "mscmm.zip");
                client.Dispose();
            }

            Form1.instance.Log += "\nUpdate downloaded! Extracting...";

            Directory.CreateDirectory("update");
            ZipFile.ExtractToDirectory("mscmm.zip", "update");

            Form1.instance.Log += "\nRestarting...";
            File.WriteAllText("updater.bat", updaterScript);

            if (downgrade)
                Settings.WipeAll();

            Process process = new Process();
            process.StartInfo.WindowStyle = ProcessWindowStyle.Hidden;
            process.StartInfo.FileName = "updater.bat";
            process.Start();
            Application.Exit();
        }
    }
}
