using System;
using System.IO;
using System.Windows.Forms;
using NReco.VideoConverter;
using Microsoft.Win32;
using System.Diagnostics;
using System.Threading.Tasks;
using System.Linq;

namespace OggConverter
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        string version; // Version of the program loaded at Form1_Load
        bool skipCD; //Tells the program to skip CD folder, if the game is older than 24.10.2017 update
        bool NoExit; //Prevents the program from closing if conversion is in progress
        string l = Environment.NewLine; // Just lets me add new line with single character
        bool FirstLoad = false; //Detects if the program has been opened for the first time

        private void Form1_Load(object sender, EventArgs e)
        {
            System.Reflection.Assembly assembly = System.Reflection.Assembly.GetExecutingAssembly();
            FileVersionInfo fvi = FileVersionInfo.GetVersionInfo(assembly.Location);
            version = fvi.FileVersion;

            log.Text += "MSC OGG Converter " + version;

            try
            {
                using (RegistryKey Key = Registry.CurrentUser.CreateSubKey(@"SOFTWARE\MSCOGG", true))
                {
                    if (Key != null)
                    {
                        // Loading settings
                        new Settings();

                        //Path in textbox
                        txtboxPath.Text = Key.GetValue("MSC Path").ToString();
                        txtboxPath.SelectionStart = 0;
                        txtboxPath.ScrollToCaret();
                        this.ActiveControl = label2;

                        //Remove MP3
                        if (Settings.RemoveMP3)
                        {
                            remMP3.Checked = true;
                        }

                        //No Steam
                        if (Settings.NoSteam)
                        {
                            launchGameWithoutSteamToolStripMenuItem.Checked = true;
                        }

                        //Action after conversion
                        {
                            if (Settings.LaunchAfterConversion)
                            {
                                launchTheGameToolStripMenuItem1.Checked = true;
                            }
                            if (Settings.CloseAfterConversion)
                            {
                                closeTheProgramToolStripMenuItem.Checked = true;
                            }
                            if (!Settings.CloseAfterConversion && !Settings.LaunchAfterConversion)
                            {
                                noneToolStripMenuItem.Checked = true;
                            }
                        }

                        if (!Settings.NoUpdates)
                        {
                            CheckBoxUpdates.Checked = true;

                            //Looking for updates
                            Update upd = new Update();
                            upd.LookForUpdate();

                            if (!OggConverter.Update.IsThereUpdate)
                            {
                                log.Text += l + l + "Tool is up-to-date";
                            }
                            else
                            {
                                log.Text += l + l + "There's update ready to download";
                            }
                        }
                        else
                        {
                            log.Text += l + l + "Updates are disabled";
                        }
                    }
                }

                log.Text += l + l + "You can checkout changelog on Steam community discussion, on project's repository, or dev's website.";

                if (!Directory.Exists(txtboxPath.Text + @"\CD"))
                {
                    skipCD = true;
                    log.Text += l + "CD folder is missing (you're propably using 24.10.2017 version of game or older), so it will be skipped.";
                }
            }
            catch (Exception)
            {
                MessageBox.Show("Hey there! Looks like you're new here. Select the game directory first :)", "Howdy", MessageBoxButtons.OK, MessageBoxIcon.Information);
                log.Text += l + "Select the game directory first.";

                // Disables some items, to prevent bugs
                button3.Enabled = false;
                button1.Enabled = false;
                settingsToolStripMenuItem.Enabled = false;
                launchTheGameToolStripMenuItem.Enabled = false;
                FirstLoad = true;
            }
        }

        /*
            ####  ##### ####  #   # ##### #     #####
            #   # #     #   # #  #  #     #     #
            ####  ####  ####  ###   ####  #     ####
            #     #     #  #  # ##  #     #     #
            #     ##### #   # #  ## ##### ##### #####
         */

        private void button1_Click(object sender, EventArgs e)
        {
            if (NoExit)
            {
                MessageBox.Show("Conversion is in progress.", "Prohibited", MessageBoxButtons.OK, MessageBoxIcon.Hand);
                return;
            }

            if (txtboxPath.Text.Length == 0)
            {
                MessageBox.Show("Select game path first.", "Prohibited", MessageBoxButtons.OK, MessageBoxIcon.Hand);
                return;
            }

            NoExit = true;
            log.Text += l + "Initializing Async Void...";
            Convert();
        }

        // Moved conversion to separated async void - it fixed the UI freeze!
        async void Convert()
        {
            try
            {
                int Radio = 1; // Counts how many converted OGG files are there already in Radio folder (plus 1). It's also used to name the converted file properly.
                int CDs = 1; // ditto, but for CDs

                int totalConversionsRadio = 0; // Counts total conversions in Radio folder which will be displayed later
                int totalConversionsCD = 0; // ditto, but for CDs

                var cnv = new FFMpegConverter();
           
                string ConversionLog = "THIS FILE WILL BE WIPED AFTER NEXT CONVERSION:" + l + l;// Keeps conversion log which will be later saved into LastConversion.txt

                //Converting Radio
                {
                    log.Text += l + "Initializing Radio folder conversion..." + l;
                    ConversionLog += "RADIO:" +l;
                    string path = txtboxPath.Text + @"\Radio\";
                    DirectoryInfo d = new DirectoryInfo(path);
                    string[] extensions = new[] { ".mp3", ".wav", ".aac", ".m4a", ".wma"};
                    FileInfo[] Files 
                        = d.GetFiles()
                        .Where(f => extensions.Contains(f.Extension.ToLower()))
                        .ToArray();

                    //Counting how many OGG files there are already
                    for (int c = 1; File.Exists(path + "track" + c + ".ogg"); c++)
                    {
                        Radio++;
                    }

                    foreach (FileInfo file in Files)
                    {
                        log.Text += "Converting " + file.Name + l;
                        string nameAfter = file.Name.Substring(0, file.Name.Length - 4);
                        await Task.Run(() => cnv.ConvertMedia(path + file.Name, path + "track" + Radio + ".ogg", Format.ogg));
                        log.Text += file.Name + " as track" + Radio + ".ogg" + l;
                        if (Settings.RemoveMP3)
                        {
                            File.Delete(path + file.Name);
                        }
                        ConversionLog += "\"" + file.Name + "\" as \"track" + Radio + ".ogg\"" + l;
                        Radio++;
                        totalConversionsRadio++;
                    }
                    log.Text += l + "Converted " + totalConversionsRadio + " files in Radio.";
                }
                ConversionLog += l + l;
                //Converting CD
                log.Text += l + "Initializing CD folder conversion..." +l;
                if (!skipCD)
                {
                    ConversionLog += "CD:" + l;
                    string pathCD = txtboxPath.Text + @"\CD\";
                    DirectoryInfo cd = new DirectoryInfo(pathCD);
                    string[] extensions = new[] { ".mp3", ".wav", ".aac", ".m4a", ".wma" };
                    FileInfo[] FilesCD
                        = cd.GetFiles()
                        .Where(f => extensions.Contains(f.Extension.ToLower()))
                        .ToArray();

                    //Counting how many OGG files there are already
                    for (int c = 1; File.Exists(pathCD + "track" + c + ".ogg"); c++)
                    {
                        CDs++;
                    }

                    foreach (FileInfo file in FilesCD)
                    { 
                        if (CDs > 15)
                        {
                            DialogResult res = MessageBox.Show("There's over 15 files in CDs already converted. My Summer Car allows max 15 files for CDs and any file above that will be ignored. Would you like to continue?", 
                                "Stop", 
                                MessageBoxButtons.YesNo, 
                                MessageBoxIcon.Information);

                            if (res == DialogResult.No)
                            {
                                log.Text += l + "Aborted CD conversion.";
                                break;
                            }
                        }

                        log.Text += "Converting " + file.Name + l;
                        string nameAfter = file.Name.Substring(0, file.Name.Length - 4);
                        await Task.Run(() => cnv.ConvertMedia(pathCD + file.Name, pathCD + "track" + CDs + ".ogg", Format.ogg));
                        log.Text += file.Name + " as track" + CDs + ".ogg" + l;
                        if (Settings.RemoveMP3)
                        {
                            File.Delete(pathCD + file.Name);
                        }
                        ConversionLog += "\"" + file.Name + "\" as \"track" + CDs + ".ogg\"" + l;
                        CDs++;
                        totalConversionsCD++;
                    }
                    log.Text += l + "Converted " + totalConversionsCD + " files in CD folder.";
                }
                else
                {
                    log.Text += l + "Skipping CD folder.";
                }

                ConversionLog += l + l + "This conversion was created at: " + DateTime.Now.ToLocalTime();
                File.WriteAllText(@"LastConversion.txt", ConversionLog);
                Process.Start(@"LastConversion.txt");
                log.Text += l + "Converted " + (totalConversionsRadio + totalConversionsCD) + " files total.";
                log.Text += l + "Done";
                System.Media.SystemSounds.Exclamation.Play();
                
                //Action after conversion
                if (Settings.LaunchAfterConversion)
                {
                    LaunchGame();
                }
                if (Settings.CloseAfterConversion)
                {
                    Application.Exit();
                }
            }
            catch (Exception ex)
            {
                new Log(ex.ToString());
            }

            NoExit = false;
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
                    log.Text += l + "Loaded My Summer Car's directory successfully";

                    if (FirstLoad)
                    {
                        Form1 f = new Form1();
                        Hide();
                        f.ShowDialog();
                        Close();
                    }
                }
                else
                {
                    log.Text += l + "Couldn't find mysummercar.exe";
                }
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            if (txtboxPath.Text.Length == 0)
            {
                MessageBox.Show("Select game path first.", "Prohibited", MessageBoxButtons.OK, MessageBoxIcon.Hand);
                return;
            }

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

        private void openLOGFolderToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Directory.CreateDirectory(@"LOG");
            Process.Start(@"LOG");
        }

        private void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (NoExit)
            {
                e.Cancel = true;
            }
        }

        private void openLastConversionToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (File.Exists("LastConversion.txt"))
                Process.Start("LastConversion.txt");
        }

        private void launchTheGameToolStripMenuItem_Click(object sender, EventArgs e)
        {
            LaunchGame();
        }

        void LaunchGame()
        {
            // Checks for correct path
            if (txtboxPath.Text.Length == 0)
            {
                MessageBox.Show("Select game directory first.", "Prohibited", MessageBoxButtons.OK, MessageBoxIcon.Hand);
                return;
            }

            if (Settings.NoSteam)
            {
                Process.Start(txtboxPath.Text + @"\mysummercar.exe");
            }
            else
            {
                Process.Start("steam://rungameid/516750");
            }
        }

        private void removeOldMP3FilesToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ChangeSettings.Bool("RemoveMP3");
        }      

        private void quitToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void steamCommunityDiscussionToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Process.Start("http://steamcommunity.com/app/516750/discussions/2/1489992713697876617/");
        }

        private void noneToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ChangeSettings.BoolFalse("CloseAfterConversion");
            ChangeSettings.BoolFalse("LaunchAfterConversion");
            closeTheProgramToolStripMenuItem.Checked = false;
            launchTheGameToolStripMenuItem1.Checked = false;
        }

        private void closeTheProgramToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ChangeSettings.Bool("CloseAfterConversion");
            noneToolStripMenuItem.Checked = false;
        }

        private void launchTheGameToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            ChangeSettings.Bool("LaunchAfterConversion");
            noneToolStripMenuItem.Checked = false;
        }

        private void launchGameWithoutSteamToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ChangeSettings.Bool("NoSteam");
        }

        private void CheckBoxUpdates_Click(object sender, EventArgs e)
        {
            ChangeSettings.Bool("NoUpdates");
        }
    }
}
