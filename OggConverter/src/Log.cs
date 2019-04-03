using System;
using System.IO;
using System.Reflection;
using System.Windows.Forms;
using System.Diagnostics;
using System.Management;
using Microsoft.Win32;

namespace OggConverter
{
    class CrashLog
    {
        public CrashLog(string log)
        {
            if (!Settings.Logs) return;
            string date = DateTime.Now.Date.ToShortDateString() + " " + DateTime.Now.Hour.ToString() + "." + DateTime.Now.Minute.ToString() + "." + DateTime.Now.Second.ToString();
            string thisVersion = Application.ProductVersion;

            Directory.CreateDirectory("LOG");
            File.WriteAllText(@"LOG\" + date + ".txt",
                $"MSC OGG {thisVersion} ({Updates.version})\n\n{FriendlyName()}\n\n{log}");

            DialogResult dl = MessageBox.Show("An error has occured. Log has been saved into LOG directory. Would you like to open directory?", "Error", MessageBoxButtons.YesNo, MessageBoxIcon.Error);
            if (dl == DialogResult.Yes)
                Process.Start("LOG");
        }

        string HKLM_GetString(string path, string key)
        {
            try
            {
                RegistryKey rk = Registry.LocalMachine.OpenSubKey(path);
                if (rk == null) return "";
                return (string)rk.GetValue(key);
            }
            catch { return ""; }
        }

        public string FriendlyName()
        {
            string ProductName = HKLM_GetString(@"SOFTWARE\Microsoft\Windows NT\CurrentVersion", "ProductName");
            string CSDVersion = HKLM_GetString(@"SOFTWARE\Microsoft\Windows NT\CurrentVersion", "CSDVersion");
            if (ProductName != "")
            {
                return (ProductName.StartsWith("Microsoft") ? "" : "Microsoft ") + ProductName +
                            (CSDVersion != "" ? " " + CSDVersion : "");
            }
            return "";
        }
    }
}
