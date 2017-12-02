﻿using System;
using System.IO;
using System.Windows.Forms;
using NReco.VideoConverter;
using Microsoft.Win32;
using System.Diagnostics;
using System.Threading.Tasks;

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
                log.Text += l + "CD folder is missing (you're propably using 24.10.2017 version of game or older), so it will be skipped.";
            }
        }

        bool skipCD;

        /*
            ####  ##### ####  #   # ##### #     #####
            #   # #     #   # #  #  #     #     #
            ####  ####  ####  ###   ####  #     ####
            #     #     #  #  # ##  #     #     #
            #     ##### #   # #  ## ##### ##### #####
         */

        string l = Environment.NewLine;

        private void button1_Click(object sender, EventArgs e)
        {
            log.Text += l + "Initializing Async Void...";
            Convert();
        }

        //Moved convertion to separated async void - it fixed UI freeze!
        async void Convert()
        {
            try
            {
                int i = 1; //Radio num
                int a = 1; //CD num

                int totalConvertionsRadio = 0;
                int totalConvertionsCD = 0;
                var cnv = new FFMpegConverter();

                log.Text += l + "Initializing Radio folder convertion...";
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
                        log.Text += "Converting " + file.Name + l;
                        string nameAfter = file.Name.Substring(0, file.Name.Length - 4);
                        await Task.Run(() => cnv.ConvertMedia(path + file.Name, path + "track" + i + ".ogg", Format.ogg));
                        log.Text += file.Name + " as track" + i + ".ogg" + l;
                        //File.Delete(file.Name);
                        i++;
                        totalConvertionsRadio++;
                    }
                    log.Text += l + "Converted " + totalConvertionsRadio + " files in Radio converted.";
                }

                //Converting CD
                log.Text += l + "Initializing CD folder convertion...";
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
                        log.Text += "Converting " + file.Name + l;
                        string nameAfter = file.Name.Substring(0, file.Name.Length - 4);
                        await Task.Run(() => cnv.ConvertMedia(pathCD + file.Name, pathCD + "track" + a + ".ogg", Format.ogg));
                        log.Text += file.Name + " as track" + a + ".ogg" + l;
                        //File.Delete(file.Name);
                        a++;
                        totalConvertionsCD++;
                    }
                    log.Text += l + "Converted " + totalConvertionsCD + " files in CD folder.";
                }
                else
                {
                    log.Text += l + "Skipping CD folder.";
                }

                log.Text += l + "Converted " + (totalConvertionsRadio + totalConvertionsCD) + " files total.";
                log.Text += l + "Done";
                System.Media.SystemSounds.Exclamation.Play();
            }
            catch (Exception ex)
            {
                new Log(ex.ToString());
            }
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
                    log.Text += l + "Couldn't find mysummercar.exe";
                }
            }

        }

        private void button3_Click(object sender, EventArgs e)
        {
            Process.Start(txtboxPath.Text);
        }

        private void infoToolStripMenuItem_Click(object sender, EventArgs e)
        {
            MessageBox.Show(
                "MSC OGG Converter " + version + l
                + "by Athlon"+ l +
                l+
                "All info about third party libraries you can find on official GitLab repo."
                , "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private void gitToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Process.Start("https://gitlab.com/aathlon/msc-ogg");
        }

        private void lookForUpdateToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Update upd = new Update();
            upd.LookForUpdate();
        }

        private void openLOGFolderToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Directory.CreateDirectory(@"LOG");
            Process.Start(@"LOG");
        }
    }
}
