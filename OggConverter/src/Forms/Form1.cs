﻿using System;
using System.IO;
using System.Windows.Forms;
using Microsoft.Win32;
using System.Diagnostics;
using System.Media;
using System.Drawing;

namespace OggConverter
{
    public partial class Form1 : Form
    {
        public static Form1 instance;

        readonly bool skipCD; //Tells the program to skip CD folder, if the game is older than 24.10.2017 update
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
        public Label LabNowPlaying;

        public Form1()
        {
            InitializeComponent();

            instance = this;
            playerRadio.Checked = true;
            Log += $"MSC Music Manager Preview {Functions.GetVersion()}";
            btnGetUpdate = btnDownloadUpdate;
            btnGetUpdate.Click += BtnCheckUpdate_Click;

            songList.DoubleClick += BtnPlay;

            LabNowPlaying = labNowPlaying;

            btnUp.Text = char.ConvertFromUtf32(0x2191);
            btnDown.Text = char.ConvertFromUtf32(0x2193);

            dragDropPanel.Dock = DockStyle.Fill;

            tabControl1.ItemSize = new Size((tabControl1.Width / tabControl1.TabCount) - 1, 0);

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
                        btnAutoSort.Checked = Settings.AutoSort;

                        UpdateSongList();

                        if (Settings.NoUpdates)
                            Log += "\n\nUpdates are disabled";
                        else
                            Updates.LookForAnUpdate();

                        if (File.Exists($"{txtboxPath.Text}\\Radio\\trackTemp.ogg"))
                        {
                            Log += "\n\nFound temp song file in Radio, possibly caused by crash\nTrying to fix it...";
                            Converter.ConvertFile($"{txtboxPath.Text}\\Radio\\trackTemp.ogg", txtboxPath.Text, "Radio", 99);
                        }

                        if (File.Exists($"{txtboxPath.Text}\\CD\\trackTemp.ogg"))
                        {
                            Log += "\n\nFound temp song file in CD, possibly caused by crash\nTrying to fix it...";
                            Converter.ConvertFile($"{txtboxPath.Text}\\CD\\trackTemp.ogg", txtboxPath.Text, "CD", 99);
                        }
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
                Log += "\n\nSelect My Summer Car Directory\nEx. C:\\Steam\\steamapps\\common\\My Summer Car\\.";

                SafeMode();
            }
        }    

        /// <summary>
        /// Used to remove temporary/unused files
        /// </summary>
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
                Directory.Delete("update", true);

            if (File.Exists("download.aac"))
                File.Delete("download.aac");
        }

        /// <summary>
        /// Disabled most features to prevent crashes.
        /// Used for initial setup and if the My Summer Car directory or file no longer exists.
        /// </summary>
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
            txtboxVideo.Enabled = false;
            btnDownload.Enabled = false;
        }

        /// <summary>
        /// Updates song list used for player
        /// </summary>
        public void UpdateSongList()
        {
            if (firstLoad) return;

            songList.Items.Clear();
            string path = $"{txtboxPath.Text}\\{(playerCD.Checked ? "CD" : "Radio")}";

            for (int i = 1; i < 99; i++)
                if (File.Exists($"{path}\\track{i}.ogg"))
                    songList.Items.Add($"track{i}.ogg");
        }

        private async void BtnConvert_Click(object sender, EventArgs e)
        {
            if (Download.downloadingNow)
            {
                Log += "Song is now being downloaded. You can't convert now";
                return;
            }

            if (Converter.conversionInProgress)
            {
                Log += "Conversion is in progress. You can't start another conversion now";
                return;
            }

            if (txtboxPath.Text.Length == 0)
            {
                MessageBox.Show("Select game path first.", "Prohibited", MessageBoxButtons.OK, MessageBoxIcon.Hand);
                return;
            }

            Converter.conversionInProgress = true;

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

                Converter.conversionInProgress = false;

                //Actions after conversion
                if (Settings.LaunchAfterConversion)
                    LaunchGame();

                if (Settings.CloseAfterConversion)
                    Application.Exit();
            }
            catch (Exception ex)
            {
                new CrashLog(ex.ToString());
                Converter.conversionInProgress = false;
            }

            UpdateSongList();
        }       

        private void Log_TextChanged(object sender, EventArgs e)
        {
            logOutput.SelectionStart = Log.Length;
            logOutput.ScrollToCaret();
        }

        private void BtnDirectory_Click(object sender, EventArgs e)
        {
            if (Download.downloadingNow)
            {
                Log += "Song is now being downloaded.";
                return;
            }

            if (Converter.conversionInProgress)
            {
                Log += "Conversion is in progress.";
                return;
            }

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

        private void BtnOpenGameDir_Click(object sender, EventArgs e)
        {
            if (txtboxPath.Text.Length == 0)
            {
                MessageBox.Show("Select game path first.", "Prohibited", MessageBoxButtons.OK, MessageBoxIcon.Hand);
                return;
            }

            Process.Start(txtboxPath.Text);
        }

        private void GitToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Process.Start("https://gitlab.com/aathlon/msc-ogg");
        }

        private void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (Converter.conversionInProgress || Download.downloadingNow)
                e.Cancel = true;

            Player.Stop();
        }


        private void OpenLastConversionToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (!File.Exists("LastConversion.txt"))
            {
                Log += "\nNo last conversion log exists";
                return;
            }

            Process.Start("LastConversion.txt");
        }

        private void LaunchTheGameToolStripMenuItem_Click(object sender, EventArgs e)
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

        private void RemoveOldMP3FilesToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Settings.RemoveMP3 ^= true;
        }      

        private void QuitToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void SteamCommunityDiscussionToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Process.Start("http://steamcommunity.com/app/516750/discussions/2/1489992713697876617/");
        }

        private void NoneToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Settings.CloseAfterConversion = btnAfterClose.Checked = false;
            Settings.LaunchAfterConversion = btnAfterLaunchGame.Checked = false;
            Settings.ShowConversionLog = btnShowConversionLog.Checked = false;
        }

        private void CloseTheProgramToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Settings.CloseAfterConversion ^= true;
            btnAfterNone.Checked = false;
        }

        private void LaunchTheGameToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            Settings.LaunchAfterConversion ^= true;
            btnAfterNone.Checked = false;
        }

        private void LaunchGameWithoutSteamToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Settings.NoSteam ^= true;
        }

        private void CheckBoxUpdates_Click(object sender, EventArgs e)
        {
            if (ModifierKeys.HasFlag(Keys.Shift))
            {
                if (!Settings.Preview)
                {
                    DialogResult dl = MessageBox.Show("Would you like to enable preview updates?\n\n" +
                        "Warning: preview releases may be unstable and broken.",
                        "Question",
                        MessageBoxButtons.YesNo,
                        MessageBoxIcon.Question);

                    if (dl == DialogResult.Yes)
                    {
                        Settings.Preview = true;
                        MessageBox.Show("In order to update, use 'Check for Update' button", 
                            "Info", 
                            MessageBoxButtons.OK, 
                            MessageBoxIcon.Information);
                    }

                    btnUpdates.Checked ^= true;
                }
                else
                {
                    DialogResult dl = MessageBox.Show("Would you like to disable preview updates?",
                        "Question",
                        MessageBoxButtons.YesNo,
                        MessageBoxIcon.Question);

                    if (dl == DialogResult.Yes)
                    {
                        Settings.Preview = false;
                        MessageBox.Show("In order to downgrade, use 'Check for Update' button.", 
                            "Info", 
                            MessageBoxButtons.OK,
                            MessageBoxIcon.Information);
                    }
                }

                return;
            }
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

        private void BtnPlay(object sender, EventArgs e)
        {
            if (songList.SelectedIndex == -1) return;

            Player.Play($"{txtboxPath.Text}\\{(playerCD.Checked ? "CD" : "Radio")}\\{songList.SelectedItem.ToString()}");

            labNowPlaying.Text = $"Now Playing: {(playerCD.Checked ? "CD" : "Radio")}\\{songList.SelectedItem.ToString()}";
            LabNowPlaying.Left = (panel1.Width - LabNowPlaying.Width) / 2;
            labNowPlaying.Visible = true;
        }

        private void BtnStop_Click(object sender, EventArgs e)
        {
            Player.Stop();
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
        /// <summary>
        /// Removes selected file on player
        /// </summary>
        private void BtnDel_Click(object sender, EventArgs e)
        {
            if (songList.SelectedIndex == -1) return;

            if (Download.downloadingNow)
            {
                Log += "\nSong is now being downloaded. You can't delete songs now.";
                return;
            }

            if (Converter.conversionInProgress)
            {
                Log += "Conversion is in progress. You can't delete songs now.";
                return;
            }

            string file = $"{txtboxPath.Text}\\{(playerCD.Checked ? "CD" : "Radio")}\\{songList.SelectedItem.ToString()}";
            DialogResult dl = MessageBox.Show($"Are you sure you want to delete {songList.SelectedItem.ToString()}?", "Question", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (dl == DialogResult.Yes)
            {
                Player.Stop();                
                if (File.Exists(file))
                    File.Delete(file);

                UpdateSongList();
            }

            if (Settings.AutoSort)
            {
                string path = $"{txtboxPath.Text}\\{(playerCD.Checked ? "CD" : "Radio")}";
                Player.Sort(path);
            }
        }

        private void PlayerCD_Click(object sender, EventArgs e)
        {
            if (Download.downloadingNow)
            {
                MessageBox.Show("Song is now being downloaded.", "Stop", MessageBoxButtons.OK, MessageBoxIcon.Stop);
                return;
            }

            if (Converter.conversionInProgress)
            {
                MessageBox.Show("Conversion is in progress.", "Prohibited", MessageBoxButtons.OK, MessageBoxIcon.Hand);
                return;
            }

            Player.Stop();
            UpdateSongList();
            btnMoveSong.Text = "Radio";
        }

        private void PlayerRadio_Click(object sender, EventArgs e)
        {
            if (Download.downloadingNow)
            {
                MessageBox.Show("Song is now being downloaded.", "Stop", MessageBoxButtons.OK, MessageBoxIcon.Stop);
                return;
            }

            if (Converter.conversionInProgress)
            {
                MessageBox.Show("Conversion is in progress.", "Prohibited", MessageBoxButtons.OK, MessageBoxIcon.Hand);
                return;
            }

            Player.Stop();
            UpdateSongList();
            btnMoveSong.Text = "CD";
        }

        private void BtnSort_Click(object sender, EventArgs e)
        {
            string path = $"{txtboxPath.Text}\\{(playerCD.Checked ? "CD" : "Radio")}";
            Player.Sort(path);
        }

        private void Form1_Activated(object sender, EventArgs e)
        {
            UpdateSongList();
        }

        private void BtnUp_Click(object sender, EventArgs e)
        {
            if (songList.SelectedIndex == -1) return;
            string path = $"{txtboxPath.Text}\\{(playerCD.Checked ? "CD" : "Radio")}";
            Player.ChangeOrder(songList, path, true);
        }

        private void BtnDown_Click(object sender, EventArgs e)
        {
            if (songList.SelectedIndex == -1) return;
            string path = $"{txtboxPath.Text}\\{(playerCD.Checked ? "CD" : "Radio")}";
            Player.ChangeOrder(songList, path, false);
        }

        private void Form1_DragEnter(object sender, DragEventArgs e)
        {
            if (Download.downloadingNow || Converter.conversionInProgress)
                return;

            if (e.Data.GetDataPresent(DataFormats.FileDrop))
            {
                dragDropPanel.Visible = true;
                dragDropPanel.BringToFront();
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
            Player.MoveTo(txtboxPath.Text, songList.SelectedItem.ToString(), playerCD.Checked);
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

            Updates.LookForAnUpdate();
        }

        private void BtnShowConversionLog_Click(object sender, EventArgs e)
        {
            Settings.ShowConversionLog ^= true;
        }

        private void BtnDownloadUpdate_Click(object sender, EventArgs e)
        {
            Updates.DownloadUpdate();
        }

        private void BtnAutoSort_Click(object sender, EventArgs e)
        {
            Settings.AutoSort ^= true;
        }

        private async void BtnDownload_Click(object sender, EventArgs e)
        {
            if (Download.downloadingNow)
            {
                MessageBox.Show("Song is now being downloaded.", "Stop", MessageBoxButtons.OK, MessageBoxIcon.Stop);
                return;
            }

            btnDownload.Enabled = txtboxVideo.Enabled = false;
            await Download.DownloadFile(txtboxVideo.Text, txtboxPath.Text, playerCD.Checked ? "CD" : "Radio", playerCD.Checked ? 15 : 99);
            btnDownload.Enabled = txtboxVideo.Enabled = true;
            txtboxVideo.Text = "";
        }

        private void BtnDesktopShortcut_Click(object sender, EventArgs e)
        {
            if (!DesktopShortcut.DoesShortcutExist())
                DesktopShortcut.Create();
        }
    }
}
