using System;
using System.IO;
using System.Windows.Forms;
using Microsoft.Win32;
using System.Diagnostics;
using System.Media;

namespace OggConverter
{
    public partial class Form1 : Form
    {
        public static Form1 instance;

        bool skipCD; //Tells the program to skip CD folder, if the game is older than 24.10.2017 update
        bool conversionInProgress; //Prevents the program from closing if conversion is in progress
        bool firstLoad = false; //Detects if the program has been opened for the first time

        public string Log
        {
            get => logOutput.Text;
            set
            {
                value = value.Replace("\n", Environment.NewLine);
                if (logOutput.InvokeRequired)
                    logOutput.Invoke(new Action(() => logOutput.Text = value));
                else
                    logOutput.Text = value;

                logOutput.SelectionStart = logOutput.TextLength;
                logOutput.ScrollToCaret();
            }
        }

        public ToolStripMenuItem btnGetUpdate;

        public Form1()
        {
            InitializeComponent();

            instance = this;
            playerRadio.Checked = true;
            Log += $"MSC Music Manager {Application.ProductVersion} BETA";
            btnGetUpdate = btnDownloadUpdate;
            btnGetUpdate.Click += BtnCheckUpdate_Click;

            Remove();

            try
            {
                using (RegistryKey Key = Registry.CurrentUser.CreateSubKey(@"SOFTWARE\MSCOGG", true))
                {
                    if (Key != null)
                    {
                        //Path in textbox
                        txtboxPath.Text = Key.GetValue("MSC Path").ToString();
                        if ((!Directory.Exists(txtboxPath.Text)) || (!File.Exists($"{txtboxPath.Text}\\mysummercar.exe")))
                        {
                            MessageBox.Show("Couldn't find mysummercar.exe.\n\nPlease select the correct game path.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            Log += "\n\nCouldn't find mysummercar.exe. Please select the correct game path.";
                            SafeMode();
                            return;
                        }
                        txtboxPath.SelectionStart = 0;
                        txtboxPath.ScrollToCaret();
                        this.ActiveControl = label2;

                        // SETTINGS CHECKS
                        btnRemMP3.Checked = Settings.RemoveMP3;
                        btnNoSteam.Checked = Settings.NoSteam;
                        // Actions after conversion
                        btnAfterLaunchGame.Checked = Settings.LaunchAfterConversion;
                        btnAfterClose.Checked = Settings.CloseAfterConversion;
                        btnAfterNone.Checked = !Settings.CloseAfterConversion && !Settings.LaunchAfterConversion && !Settings.ShowConversionLog;
                        btnShowConversionLog.Checked = Settings.ShowConversionLog;
                        btnUpdates.Checked = !Settings.NoUpdates;
                        btnLogs.Checked = Settings.Logs;

                        UpdateSongList();

                        if (Settings.NoUpdates)
                            Log += "\n\nUpdates are disabled";
                        else
                            Updates.IsThereUpdate();
                    }
                }

                if (Updates.version > Settings.LatestVersion)
                {
                    Log += "\n\n" + Properties.Resources.changelog;
                    Settings.LatestVersion = Updates.version;
                }

                if (!Directory.Exists($"{txtboxPath.Text}\\CD"))
                {
                    skipCD = true;
                    Log += "\nCD folder is missing (you're propably using 24.10.2017 version of the game or older), so it will be skipped.";
                }
            }
            catch
            {
                // There was some kind of problem while starting.
                // Launching the first start sequention

                MessageBox.Show("Hey there! Looks like you're new here. Select the game directory first :)\n\n" +
                    "If you see that message second, or more times, please contact the developer.", "Howdy", MessageBoxButtons.OK, MessageBoxIcon.Information);
                Log += "\n\nSelect My Summer Car Directory\nEg. C:\\Steam\\steamapps\\common\\My Summer Car\\.";

                SafeMode();
            }
        }    

        void Remove()
        {
            // Removing unused files
            if (File.Exists("NReco.VideoConverter.dll"))
                File.Delete("NReco.VideoConverter.dll");

            if (File.Exists("updater.bat"))
                File.Delete("updater.bat");

            if (File.Exists("mscmm.zip"))
                File.Delete("mscmm.zip");

            if (File.Exists("MSC OGG.exe"))
                File.Delete("MSC OGG.exe");

            if (Directory.Exists("update"))
            {
                DirectoryInfo dir = new DirectoryInfo("update");
                FileInfo[] files = dir.GetFiles();

                foreach (FileInfo file in files)
                    file.Delete();

                Directory.Delete("update");
            }
        }

        // Disabled most features to prevent crashes
        void SafeMode()
        {
            txtboxPath.Text = "Select game folder ->";
            btnOpenGameDir.Enabled = false;
            btnConvert.Enabled = false;
            menuSettings.Enabled = false;
            btnLaunchGame.Enabled = false;
            firstLoad = true;
            this.AllowDrop = false;
            btnPlaySong.Enabled = false;
            btnStop.Enabled = false;
            btnDel.Enabled = false;
            btnUp.Enabled = false;
            btnDown.Enabled = false;
            btnSort.Enabled = false;
            playerRadio.Enabled = false;
            playerCD.Enabled = false;
            btnMoveSong.Enabled = false;
        }

        private async void btnConvert_Click(object sender, EventArgs e)
        {
            if (conversionInProgress)
            {
                MessageBox.Show("Conversion is in progress.", "Prohibited", MessageBoxButtons.OK, MessageBoxIcon.Hand);
                return;
            }

            if (txtboxPath.Text.Length == 0)
            {
                MessageBox.Show("Select game path first.", "Prohibited", MessageBoxButtons.OK, MessageBoxIcon.Hand);
                return;
            }

            conversionInProgress = true;

            Converter.ConversionLog = "";
            Converter.TotalConversions = 0;
            Converter.skipped = 0;

            try
            {
                Log += "\n\n-----------------------------------------------------------------------------------------------------------------------------------------";
                Converter.ConversionLog += "THIS FILE WILL BE WIPED AFTER THE NEXT CONVERSION.\nMAKE SURE TO DO A BACKUP:\n\n";
                await Converter.ConvertFolder(txtboxPath.Text, "Radio", 99);      

                if (!skipCD)
                    await Converter.ConvertFolder(txtboxPath.Text, "CD", 15);

                if (Converter.skipped != 2)
                {
                    Converter.ConversionLog += DateTime.Now.ToLocalTime();
                    Log += $"\nConverted {Converter.TotalConversions} files in total";
                    Log += "\n\nDone";
                    if (Settings.ShowConversionLog)
                    {
                        File.WriteAllText(@"LastConversion.txt", Converter.ConversionLog);
                        Process.Start(@"LastConversion.txt");
                    }
                    Log += "\nConversion log saved to LastConversion.txt";
                }
                else
                    Log += "\nConversion log will not be saved, because both Radio and CD were skipped";

                SystemSounds.Exclamation.Play();

                conversionInProgress = false;

                //Actions after conversion
                if (Settings.LaunchAfterConversion)
                    LaunchGame();

                if (Settings.CloseAfterConversion)
                    Application.Exit();
            }
            catch (Exception ex)
            {
                new CrashLog(ex.ToString());
                conversionInProgress = false;
            }

            UpdateSongList();
        }       

        private void Log_TextChanged(object sender, EventArgs e)
        {
            logOutput.SelectionStart = Log.Length;
            logOutput.ScrollToCaret();
        }

        private void btnDirectory_Click(object sender, EventArgs e)
        {
            using (var folderDialog = new FolderBrowserDialog())
            {
                var dialog = folderDialog.ShowDialog();

                if (dialog == DialogResult.OK && Directory.GetFiles(folderDialog.SelectedPath, "mysummercar.exe").Length != 0)
                {
                    txtboxPath.Text = folderDialog.SelectedPath;
                    RegistryKey Key = Registry.CurrentUser.OpenSubKey(@"SOFTWARE\MSCOGG", true);
                    Key.SetValue("MSC Path", folderDialog.SelectedPath);
                    Log += "\nMy Summer Car directory detected";

                    if (firstLoad)
                    {
                        Form1 f = new Form1();
                        Hide();
                        f.ShowDialog();
                        Close();
                    }
                }
                else if (dialog == DialogResult.Cancel)
                    Log += "\n\nCanceled";
                else
                    Log += "\n\nCouldn't find mysummercar.exe";
            }
        }

        private void btnOpenGameDir_Click(object sender, EventArgs e)
        {
            if (txtboxPath.Text.Length == 0)
            {
                MessageBox.Show("Select game path first.", "Prohibited", MessageBoxButtons.OK, MessageBoxIcon.Hand);
                return;
            }

            Process.Start(txtboxPath.Text);
        }

        private void gitToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Process.Start("https://gitlab.com/aathlon/msc-ogg");
        }

        private void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (conversionInProgress)
                e.Cancel = true;

            Music.Stop();
        }


        private void openLastConversionToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (!File.Exists("LastConversion.txt"))
            {
                Log += "\n\nNo last conversion log exists";
                return;
            }

            Process.Start("LastConversion.txt");
        }

        private void launchTheGameToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (ModifierKeys.HasFlag(Keys.Shift))
            {
                Process.Start($"{txtboxPath.Text}\\mysummercar.exe");
                return;
            }
            LaunchGame();
        }

        void LaunchGame()
        {
            Process.Start(Settings.NoSteam ? $"{txtboxPath.Text}\\mysummercar.exe" : "steam://rungameid/516750");
        }

        private void removeOldMP3FilesToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Settings.RemoveMP3 ^= true;
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
            Settings.CloseAfterConversion = btnAfterClose.Checked = false;
            Settings.LaunchAfterConversion = btnAfterLaunchGame.Checked = false;
            Settings.ShowConversionLog = btnShowConversionLog.Checked = false;
        }

        private void closeTheProgramToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Settings.CloseAfterConversion ^= true;
            btnAfterNone.Checked = false;
        }

        private void launchTheGameToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            Settings.LaunchAfterConversion ^= true;
            btnAfterNone.Checked = false;
        }

        private void launchGameWithoutSteamToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Settings.NoSteam ^= true;
        }

        private void CheckBoxUpdates_Click(object sender, EventArgs e)
        {
            Settings.NoUpdates ^= true;
        }

        private void BtnFFmpegLicense_Click(object sender, EventArgs e)
        {
            Process.Start("http://www.gnu.org/licenses/old-licenses/lgpl-2.1.html");
        }

        private void MSCOGGToolStripMenuItem_Click(object sender, EventArgs e)
        {
            MessageBox.Show($"MSC Music Manager {Application.ProductVersion}\nby Athlon",
                "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private async void btnPlay(object sender, EventArgs e)
        {
            if (songList.SelectedIndex == -1) return;
            Music.Play($"{txtboxPath.Text}\\{(playerCD.Checked ? "CD" : "Radio")}\\{songList.SelectedItem.ToString()}");
        }

        private void BtnStop_Click(object sender, EventArgs e)
        {
            Music.Stop();
        }

        private void BtnLogFolder_Click(object sender, EventArgs e)
        {
            if (ModifierKeys.HasFlag(Keys.Shift))
            {
                DialogResult dl = MessageBox.Show("Would you like to remove all logs?", "Question", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                if (dl == DialogResult.Yes)
                    Directory.Delete("LOG", true);

                return;
            }

            if (!Settings.Logs)
            {
                Log += "\nLogs are disabled";
                return;
            }

            if (!Directory.Exists("Log"))
            {
                Log += "\nLog folder doesn't exist";
                return;
            }

            Process.Start(@"Log");
        }

        public void UpdateSongList()
        {
            if (firstLoad) return;

            songList.Items.Clear();
            string path = $"{txtboxPath.Text}\\{(playerCD.Checked ? "CD" : "Radio")}";

            for (int i = 1; i < 99; i++)
                if (File.Exists($"{path}\\track{i}.ogg"))
                    songList.Items.Add($"track{i}.ogg");
        }

        private void BtnDel_Click(object sender, EventArgs e)
        {
            if (songList.SelectedIndex == -1) return;
            string file = $"{txtboxPath.Text}\\{(playerCD.Checked ? "CD" : "Radio")}\\{songList.SelectedItem.ToString()}";
            DialogResult dl = MessageBox.Show($"Are you sure you want to delete {songList.SelectedItem.ToString()}?", "Question", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (dl == DialogResult.Yes)
            {
                Music.Stop();                
                if (File.Exists(file))
                    File.Delete(file);

                UpdateSongList();
            }
        }

        private void PlayerCD_Click(object sender, EventArgs e)
        {
            Music.Stop();
            UpdateSongList();
            btnMoveSong.Text = "<";
        }

        private void PlayerRadio_Click(object sender, EventArgs e)
        {
            Music.Stop();
            UpdateSongList();
            btnMoveSong.Text = ">";
        }

        private void BtnSort_Click(object sender, EventArgs e)
        {
            string path = $"{txtboxPath.Text}\\{(playerCD.Checked ? "CD" : "Radio")}";
            Music.Sort(path);
        }

        private void Form1_Activated(object sender, EventArgs e)
        {
            UpdateSongList();
        }

        private void BtnUp_Click(object sender, EventArgs e)
        {
            if (songList.SelectedIndex == -1) return;
            string path = $"{txtboxPath.Text}\\{(playerCD.Checked ? "CD" : "Radio")}";
            Music.ChangeOrder(songList, path, true);
        }

        private void BtnDown_Click(object sender, EventArgs e)
        {
            if (songList.SelectedIndex == -1) return;
            string path = $"{txtboxPath.Text}\\{(playerCD.Checked ? "CD" : "Radio")}";
            Music.ChangeOrder(songList, path, false);
        }

        private void Form1_DragEnter(object sender, DragEventArgs e)
        {
            if (e.Data.GetDataPresent(DataFormats.FileDrop))
            {
                dragDropPanel.Visible = true;
                labelConvert.Text = $"Convert {((string[])e.Data.GetData(DataFormats.FileDrop)).Length} file(s) to {(playerCD.Checked ? "CD" : "Radio")}?";
                labelConvert.Left = (this.ClientSize.Width - labelConvert.Size.Width) / 2;
                labelConvert.Top = (this.ClientSize.Height - labelConvert.Size.Height) / 2;
                e.Effect = DragDropEffects.Copy;
            }
        }

        private async void Form1_DragDrop(object sender, DragEventArgs e)
        {
            dragDropPanel.Visible = false;

            int limit = playerCD.Checked ? 15 : 99;
            string[] files = (string[])e.Data.GetData(DataFormats.FileDrop);
            string dropTo = playerCD.Checked ? "CD" : "Radio";
            foreach (string file in files)
                await Converter.ConvertFile(file, txtboxPath.Text, dropTo, limit);

            UpdateSongList();
        }

        private void Form1_DragLeave(object sender, EventArgs e)
        {
            dragDropPanel.Visible = false;
        }

        private void BtnMoveSong_Click(object sender, EventArgs e)
        {
            if (songList.SelectedIndex == -1) return;
            Music.MoveTo(txtboxPath.Text, songList.SelectedItem.ToString(), playerCD.Checked);
        }

        private void BtnLogs_Click(object sender, EventArgs e)
        {
            Settings.Logs ^= true;
        }

        private void BtnCheckUpdate_Click(object sender, EventArgs e)
        {
            // Force download and install update
            if (ModifierKeys.HasFlag(Keys.Shift))
            {
                Log += "\n\nForcing the update...";
                Updates.DownloadUpdate();
                return;
            }

            Updates.IsThereUpdate();
        }

        private void BtnShowConversionLog_Click(object sender, EventArgs e)
        {
            Settings.ShowConversionLog ^= true;
        }

        private void BtnDownloadUpdate_Click(object sender, EventArgs e)
        {
            Updates.DownloadUpdate();
        }
    }
}
