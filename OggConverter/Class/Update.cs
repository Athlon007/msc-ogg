using System;
using System.Net;
using System.IO;
using System.Diagnostics;
using System.Windows.Forms;

namespace OggConverter
{
    class Update
    {
        public static string VerUpd = "17492"; // first two numbers - year, second two numbers - week, last digit - release number in this week. So the 17490 means year 2017, week 49, number of release in this week - 0

        public void LookForUpdate()
        {
            try
            {
                DownloadFile("http://athlon.kkmr.pl/download/mscogg/ver.txt", "ver.txt");

                if (IsThereNewUpdate("ver.txt"))
                {
                    DialogResult res = MessageBox.Show("There's new update ready to download", "Update", MessageBoxButtons.YesNo, MessageBoxIcon.Information);

                    if (res == DialogResult.Yes)
                    {
                        Process.Start("https://gitlab.com/aathlon/msc-ogg");
                    }
                }
                else
                {
                    File.Delete("ver.txt");
                }

                LookedForUpdate = true;
            }
            catch (Exception ex)
            {
                new Log(ex.ToString());
            }
        }

        public void DownloadFile(string From, string To)
        {
            using (WebClient client = new WebClient())
            {
                client.DownloadFile(new Uri(From), To);
                client.Dispose();
            }
        }

        public static bool IsThereUpdate { get; set; }
        public static bool LookedForUpdate { get; set; }

        public bool IsThereNewUpdate(string Check)
        {
            if (!File.ReadAllText(Check).Contains(VerUpd))
            {
                IsThereUpdate = true;
                LookedForUpdate = true;
                return true;
            }
            else
            {
                return false;
            }
        }
    }
}
