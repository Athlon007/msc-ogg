using System;
using System.IO;
using System.Reflection;
using System.Windows.Forms;
using System.Diagnostics;

namespace OggConverter
{
    class Log
    {
        public Log(string log)
        {
            string Date = DateTime.Now.Date.ToShortDateString() + " " + DateTime.Now.Hour.ToString() + "." + DateTime.Now.Minute.ToString() + "." + DateTime.Now.Second.ToString();
            string l = Environment.NewLine;
            Directory.CreateDirectory("LOG");
            File.WriteAllText(@"LOG\" + Date + ".txt",
                "MSC OGG " + MajorVer + "." + MinorVer + "." + BuildVer + "." + RevVer + " (" + Update.VerUpd + ")" + l +
                l +
                log
                );

            DialogResult dl = MessageBox.Show("Error has occured. Log has been saved into LOG directory. Would you like to open directory?", "Error", MessageBoxButtons.YesNo, MessageBoxIcon.Error);
            if (dl == DialogResult.Yes)
            {
                Process.Start("LOG");
            }
        }


        // Major
        string MajorVer
        {
            get
            {
                return Assembly.GetExecutingAssembly().GetName().Version.Major.ToString();
            }
        }
        // Minor
        string MinorVer
        {
            get
            {
                return Assembly.GetExecutingAssembly().GetName().Version.Minor.ToString();
            }
        }
        // Build
        string BuildVer
        {
            get
            {
                return Assembly.GetExecutingAssembly().GetName().Version.Build.ToString();
            }
        }
        // RevVer
        string RevVer
        {
            get
            {
                return Assembly.GetExecutingAssembly().GetName().Version.Revision.ToString();
            }
        }
    }
}
