using System;
using System.Net;
using System.IO;
using System.Diagnostics;
using System.Windows.Forms;

namespace OggConverter
{
    class Updates
    {
        public static string version = "17510"; // first two numbers - year, second two numbers - week, last digit - release number in this week. So the 17490 means year 2017, week 49, number of release in this week - 0
        public static string realVersion { get => Application.ProductVersion; }
        static bool newUpdateReady;

        public static bool IsThereUpdate()
        {
            if (newUpdateReady)
            {
                DialogResult res = MessageBox.Show("There's a new update ready to download. Would you like to download it now?", "Update", MessageBoxButtons.YesNo, MessageBoxIcon.Information);
                if (res == DialogResult.Yes)
                    Process.Start("https://gitlab.com/aathlon/msc-ogg");
                return true;
            }

            try
            {
                using (WebClient client = new WebClient())
                {
                    //client.DownloadFile(new Uri("https://gitlab.com/aathlon/msc-ogg/raw/master/latest.txt"), "latest.txt");
                    client.DownloadFile(new Uri("http://athlon.kkmr.pl/download/mscogg/ver.txt"), "latest.txt");
                    client.Dispose();
                }
            }
            catch (Exception ex)
            {
                new CrashLog(ex.ToString());
            }

            string content = File.ReadAllText("latest.txt");
            File.Delete("latest.txt");

            if (content != version)
            {
                DialogResult res = MessageBox.Show("There's a new update ready to download. Would you like to download it now?", "Update", MessageBoxButtons.YesNo, MessageBoxIcon.Information);
                if (res == DialogResult.Yes)
                    Process.Start("https://gitlab.com/aathlon/msc-ogg");

                newUpdateReady = true;
                return true;
            }

            return false;
        }
    }
}
