using System;
using System.IO;
using System.Windows.Forms;
using NReco.VideoConverter;
using Microsoft.Win32;
using System.Diagnostics;

namespace OggConverter
{
    public partial class Form1 : Form
    {
        string version;

        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            System.Reflection.Assembly assembly = System.Reflection.Assembly.GetExecutingAssembly();
            FileVersionInfo fvi = FileVersionInfo.GetVersionInfo(assembly.Location);
            version = fvi.FileVersion;

            log.Text += "MSC OGG Converter " + version;

            //Looking for updates
            Update upd = new Update();
            upd.LookForUpdate();

            using (RegistryKey Key = Registry.CurrentUser.CreateSubKey(@"SOFTWARE\MSCOGG", true))
            {
                if (Key != null)
                {
                    txtboxPath.Text = Key.GetValue("MSC Path").ToString();
                }
            }

            if (!Directory.Exists(txtboxPath.Text + @"\CD"))
            {
                skipCD = true;
                log.Text += Environment.NewLine + "CD folder is missing (you're propably using 24.10.2017 version of game or older), so it will be skipped.";
            }
        }

        bool skipCD;

        private void button1_Click(object sender, EventArgs e)
        {
            log.Text += Environment.NewLine + "Initializing Radio folder convertion...";

            int i = 1; //Radio num
            int a = 1; //CD num

            int totalConvertionsRadio = 0;
            int totalConvertionsCD = 0;

            var cnv = new FFMpegConverter();
            //Converting Radio
            {
                string path = txtboxPath.Text + @"\Radio\";
                DirectoryInfo d = new DirectoryInfo(path);
                FileInfo[] Files = d.GetFiles("*.mp3");

                //Counting how many OGG files there are already
                for (int c = 1; File.Exists(path + "track" + c + ".ogg"); c++)
                {
                    i++;
                }

                foreach (FileInfo file in Files)
                {
                    log.Text += "Converting " + file.Name + Environment.NewLine;
                    string nameAfter = file.Name.Substring(0, file.Name.Length - 4);
                    cnv.ConvertMedia(path + file.Name, path + "track" + i + ".ogg", Format.ogg);
                    log.Text += file.Name + " as track" + i + ".ogg" + Environment.NewLine;
                    i++;
                    totalConvertionsRadio++;
                }
                log.Text += Environment.NewLine + "Converted " + totalConvertionsRadio + " files in Radio converted.";
            }

            //Converting CD
            log.Text += Environment.NewLine + "Initializing CD folder convertion...";
            if (!skipCD)
            {
                string pathCD = txtboxPath.Text + @"\CD\";
                DirectoryInfo cd = new DirectoryInfo(pathCD);
                FileInfo[] FilesCD = cd.GetFiles("*.mp3");
                //Counting how many OGG files there are already
                for (int c = 1; File.Exists(pathCD + "track" + c + ".ogg"); c++)
                {
                    a++;
                }

                foreach (FileInfo file in FilesCD)
                {
                    log.Text += "Converting " + file.Name + Environment.NewLine;
                    string nameAfter = file.Name.Substring(0, file.Name.Length - 4);
                    cnv.ConvertMedia(pathCD + file.Name, pathCD + "track" + a + ".ogg", Format.ogg);
                    log.Text += file.Name + " as track" + a + ".ogg" + Environment.NewLine;
                    a++;
                    totalConvertionsCD++;
                }
                log.Text += Environment.NewLine + "Converted " + totalConvertionsCD + " files in CD converted.";
            }
            else
            {
                log.Text += Environment.NewLine + "Skipping CD folder.";
            }

            log.Text += Environment.NewLine + "Converted " + (totalConvertionsRadio + totalConvertionsCD) + " files total.";
            log.Text += Environment.NewLine + "Done";
        }

        private void log_TextChanged(object sender, EventArgs e)
        {
            log.SelectionStart = log.Text.Length;
            log.ScrollToCaret();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            using (var folderDialog = new FolderBrowserDialog())
            {
                if (folderDialog.ShowDialog() == DialogResult.OK && Directory.GetFiles(folderDialog.SelectedPath, "mysummercar.exe"). Length != 0)
                {
                    txtboxPath.Text = folderDialog.SelectedPath;
                    RegistryKey Key = Registry.CurrentUser.OpenSubKey(@"SOFTWARE\MSCOGG", true);
                    Key.SetValue("MSC Path", folderDialog.SelectedPath);
                }
                else
                {
                    log.Text += Environment.NewLine + "Couldn't find mysummercar.exe";
                }
            }

        }

        private void button3_Click(object sender, EventArgs e)
        {
            Process.Start(txtboxPath.Text);
        }

        private void infoToolStripMenuItem_Click(object sender, EventArgs e)
        {
            MessageBox.Show(":)");
        }

        private void gitToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Process.Start("https://gitlab.com/aathlon/msc-ogg");
        }

        private void lookForUpdateToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Update upd = new Update();
        }
    }
}
