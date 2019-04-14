// MSC Music Manager
// Copyright(C) 2019 Athlon

// This program is free software: you can redistribute it and/or modify
// it under the terms of the GNU General Public License as published by
// the Free Software Foundation, either version 3 of the License, or
// (at your option) any later version.

// This program is distributed in the hope that it will be useful,
// but WITHOUT ANY WARRANTY; without even the implied warranty of
// MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.See the
// GNU General Public License for more details.

// You should have received a copy of the GNU General Public License
// along with this program.If not, see<http://www.gnu.org/licenses/>.

using System;
using System.IO;
using System.Windows.Forms;
using Microsoft.Win32;
using System.Diagnostics;
using System.Media;
using System.Drawing;
using System.Threading.Tasks;

namespace OggConverter
{
    public partial class Form1 : Form
    {
        public static Form1 instance;

        readonly bool skipCD; //Tells the program to skip CD folder, if the game is older than 24.10.2017 update
        readonly bool firstLoad = false; //Detects if the program has been opened for the first time

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

        string CurrentFolder { get => playerCD.Checked ? "CD" : "Radio"; }

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

            btnPlaySong.Text = char.ConvertFromUtf32(0x25B6);
            btnStop.Text = char.ConvertFromUtf32(0x25A0);
            btnDel.Text = char.ConvertFromUtf32(0x232B);

            dragDropPanel.Dock = DockStyle.Fill;

            tabControl1.ItemSize = new Size((tabControl1.Width / tabControl1.TabCount) - 1, 0);

            Functions.RemoveOldFiles();

            try
            {
                using (RegistryKey Key = Registry.CurrentUser.CreateSubKey(@"SOFTWARE\MSCOGG", true))
                {
                    if (Key != null)
                    {
                        //Path in textbox
                        txtboxPath.Text = Settings.GamePath;
                        if ((!Directory.Exists(Settings.GamePath)) || (!File.Exists($"{Settings.GamePath}\\mysummercar.exe")))
                        {
                            MessageBox.Show("Couldn't find mysummercar.exe.\n\nPlease select the correct game path.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            Log += "\n\nCouldn't find mysummercar.exe. Please select the correct game path.";
                            txtboxPath.Text = "Select game folder ->";
                            firstLoad = true;
                            SafeMode(true);
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
                        btnNewNaming.Checked = Settings.UseNewNaming;

                        if (Settings.NoUpdates)
                            Log += "\n\nUpdates are disabled";
                        else
                        {
                            if (Settings.Preview)
                                Updates.LookForAnUpdate(true);

                            Updates.LookForAnUpdate(false);
                            Log += Settings.Preview ? "\nYOU ARE USING PREVIEW UPDATE CHANNEL" : "";
                        }

                        if (Settings.Preview)
                        {
                            btnUpdates.ForeColor = Color.Red;
                            btnUpdates.Text = "Updates (Preview)";
                        }

                        if (File.Exists($"{Settings.GamePath}\\Radio\\trackTemp.ogg"))
                        {
                            Log += "\n\nFound temp song file in Radio, possibly caused by crash\nTrying to fix it...";
                            _ = Converter.ConvertFile($"{Settings.GamePath}\\Radio\\trackTemp.ogg", "Radio", 99);
                        }

                        if (File.Exists($"{Settings.GamePath}\\CD\\trackTemp.ogg"))
                        {
                            Log += "\n\nFound temp song file in CD, possibly caused by crash\nTrying to fix it...";
                            _ = Converter.ConvertFile($"{Settings.GamePath}\\CD\\trackTemp.ogg", "CD", 99);
                        }
                    }
                }

                if (Updates.version > Settings.LatestVersion)
                {
                    Log += "\n\n" + Properties.Resources.changelog;
                    Settings.LatestVersion = Updates.version;
                }

                if (!Directory.Exists($"{Settings.GamePath}\\CD"))
                {
                    skipCD = true;
                    Log += "\nCD folder is missing (you're propably using 24.10.2017 version of the game or older), so it will be skipped.";
                }
            }
            catch
            {
                // There was some kind of problem while starting.
                // Launching the first start sequence

                MessageBox.Show("Hey there! Looks like you're new here. Select the game directory first :)\n\n" +
                    "If you see that message second, or more times, please contact the developer.", "Howdy", MessageBoxButtons.OK, MessageBoxIcon.Information);

                Log += "\n\nSelect My Summer Car Directory\nEx. C:\\Steam\\steamapps\\common\\My Summer Car\\.";
                Log += "\n\n" + Functions.AboutNotice;
                txtboxPath.Text = "Select game folder ->";
                firstLoad = true;
                SafeMode(true);
            }
        }

        /// <summary>
        /// Disabled most features to prevent crashes and bugs.
        /// Used for initial setup and if the My Summer Car directory or file no longer exists.
        /// </summary>
        void SafeMode(bool state)
        {
            state ^= true;

            btnOpenGameDir.Enabled = state;
            btnConvert.Enabled = state;
            menuSettings.Enabled = state;
            btnLaunchGame.Enabled = state;
            this.AllowDrop = state;
            btnPlaySong.Enabled = state;
            btnStop.Enabled = state;
            btnDel.Enabled = state;
            btnUp.Enabled = state;
            btnDown.Enabled = state;
            btnSort.Enabled = state;
            playerRadio.Enabled = state;
            playerCD.Enabled = state;
            btnMoveSong.Enabled = state;
            txtboxVideo.Enabled = state;
            btnDownload.Enabled = state;
            songList.Enabled = state;
        }

        /// <summary>
        /// Updates song list used for player
        /// </summary>
        public void UpdateSongList()
        {
            if (firstLoad) return;

            songList.Items.Clear();
            string path = $"{Settings.GamePath}\\{(CurrentFolder)}";

            if (Settings.UseNewNaming)
                Player.WorkingSongList.Clear();

            if (Settings.UseNewNaming)
            {
                for (int i = 1; i < 99; i++)
                    if (File.Exists($"{path}\\track{i}.ogg"))
                    {
                        string s = File.Exists($"{path}\\track{i}.mscmm") ? File.ReadAllText($"{path}\\track{i}.mscmm") : $"track{i}";
                        if (s == "") s = $"track{i}";
                        songList.Items.Add(s);
                        Player.WorkingSongList.Add($"track{i}");
                    }
                return;
            }

            for (int i = 1; i < 99; i++)
                if (File.Exists($"{path}\\track{i}.ogg"))
                    songList.Items.Add($"track{i}.ogg");
        }

        private async void BtnConvert_Click(object sender, EventArgs e)
        {
            if (Functions.AreAllBusy())
            {
                Log += "\nProgram is busy.";
                return;
            }

            if (Settings.GamePath.Length == 0)
            {
                Log += "\nSelect game path first.";
                return;
            }

            Converter.IsBusy = true;

            Converter.ConversionLog = "";
            Converter.TotalConversions = 0;
            Converter.Skipped = 0;

            try
            {
                SafeMode(true);
                Log += "\n\n-----------------------------------------------------------------------------------------------------------------------------------------";
                Converter.ConversionLog += "THIS FILE WILL BE WIPED AFTER THE NEXT CONVERSION.\nMAKE SURE TO DO A BACKUP:\n\n";
                await Converter.ConvertFolder("Radio", 99);

                if (!skipCD)
                    await Converter.ConvertFolder("CD", 15);

                if (Converter.Skipped != 2)
                {
                    Converter.ConversionLog += DateTime.Now.ToLocalTime();
                    Log += $"\nConverted {Converter.TotalConversions} files in total";
                    Log += "\n\nDone";
                    if (Settings.ShowConversionLog)
                    {
                        File.WriteAllText(@"LastConversion.txt", Converter.ConversionLog);
                        Process.Start(@"LastConversion.txt");
                    }
                    Log += "\nConversion log was saved to LastConversion.txt";
                }
                else
                    Log += "\nConversion log will not be saved, because both Radio and CD were skipped";

                SystemSounds.Exclamation.Play();

                Converter.IsBusy = false;

                //Actions after conversion
                if (Settings.LaunchAfterConversion)
                    LaunchGame();

                if (Settings.CloseAfterConversion)
                    Application.Exit();
            }
            catch (Exception ex)
            {
                new CrashLog(ex.ToString());
            }
            finally
            {
                Converter.IsBusy = false;
                SafeMode(false);
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
            if (Functions.AreAllBusy())
            {
                Log += "\nProgram is busy.";
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
            if (Settings.GamePath.Length == 0)
            {
                Log += "\nSelect game path first.";
                return;
            }

            Process.Start(Settings.GamePath);
        }

        private void GitToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Process.Start("https://gitlab.com/aathlon/msc-ogg");
        }

        private void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (Functions.AreAllBusy())
            {
                e.Cancel = true;
                Log += "\nProgram is busy.";
                return;
            }

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
                Process.Start($"{Settings.GamePath}\\mysummercar.exe");
                return;
            }
            LaunchGame();
        }

        void LaunchGame()
        {
            Process.Start(Settings.NoSteam ? $"{Settings.GamePath}\\mysummercar.exe" : "steam://rungameid/516750");
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
            MessageBox.Show(Functions.AboutNotice, "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private void BtnPlay(object sender, EventArgs e)
        {
            if (songList.SelectedIndex == -1) return;

            if (Settings.UseNewNaming)
                Player.Play($"{Settings.GamePath}\\{CurrentFolder}\\{Player.WorkingSongList[songList.SelectedIndex]}.ogg");
            else
                Player.Play($"{Settings.GamePath}\\{CurrentFolder}\\{songList.SelectedItem.ToString()}");

            labNowPlaying.Text = Settings.UseNewNaming ? songList.SelectedItem.ToString() : $"Now Playing: {songList.SelectedItem.ToString()}";
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

            if (Functions.AreAllBusy())
            {
                Log += "\nProgram is busy.";
                return;
            }

            string file = Settings.UseNewNaming ? $"{Settings.GamePath}\\{CurrentFolder}\\{Player.WorkingSongList[songList.SelectedIndex]}.ogg" : 
                $"{Settings.GamePath}\\{CurrentFolder}\\{songList.SelectedItem.ToString()}";

            string meta = Settings.UseNewNaming ? $"{Settings.GamePath}\\{CurrentFolder}\\{Player.WorkingSongList[songList.SelectedIndex]}.mscmm" : null;

            DialogResult dl = MessageBox.Show($"Are you sure you want to delete {songList.SelectedItem.ToString()}?", "Question", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (dl == DialogResult.Yes)
            {
                Player.Stop();

                while (!Functions.IsFileReady(file)) { }

                if (File.Exists(file))
                    File.Delete(file);

                if (Settings.UseNewNaming)
                {
                    if (File.Exists(meta))
                        File.Delete(meta);
                }

                UpdateSongList();
            }

            if (Settings.AutoSort)
                Player.Sort(CurrentFolder);
        }

        private void PlayerCD_Click(object sender, EventArgs e)
        {
            if (Functions.AreAllBusy())
            {
                Log += "\nProgram is busy.";
                return;
            }

            Player.Stop();
            UpdateSongList();
            btnMoveSong.Text = "Radio";
        }

        private void PlayerRadio_Click(object sender, EventArgs e)
        {
            if (Functions.AreAllBusy())
            {
                Log += "\nProgram is busy.";
                return;
            }

            Player.Stop();
            UpdateSongList();
            btnMoveSong.Text = "CD";
        }

        private void BtnSort_Click(object sender, EventArgs e)
        {
            Player.Sort(CurrentFolder);
        }

        private void Form1_Activated(object sender, EventArgs e)
        {
            UpdateSongList();
        }

        private void BtnUp_Click(object sender, EventArgs e)
        {
            if (songList.SelectedIndex == -1) return;
            string path = $"{Settings.GamePath}\\{(CurrentFolder)}";
            Player.ChangeOrder(songList, path, true);
        }

        private void BtnDown_Click(object sender, EventArgs e)
        {
            if (songList.SelectedIndex == -1) return;
            string path = $"{Settings.GamePath}\\{(CurrentFolder)}";
            Player.ChangeOrder(songList, path, false);
        }

        private void Form1_DragEnter(object sender, DragEventArgs e)
        {
            if (Functions.AreAllBusy())
                return;

            if (e.Data.GetDataPresent(DataFormats.FileDrop))
            {
                dragDropPanel.Visible = true;
                dragDropPanel.BringToFront();
                int filesLength = ((string[])e.Data.GetData(DataFormats.FileDrop)).Length;
                labelConvert.Text = $"Convert {filesLength} file{(filesLength > 1 ? "s" : "")} to {(CurrentFolder)}?";
                labelConvert.Left = (this.ClientSize.Width - labelConvert.Size.Width) / 2;
                labelConvert.Top = (this.ClientSize.Height - labelConvert.Size.Height) / 2;
                e.Effect = DragDropEffects.Copy;
            }
        }

        private async void Form1_DragDrop(object sender, DragEventArgs e)
        {
            SafeMode(true);
            dragDropPanel.Visible = false;

            int limit = playerCD.Checked ? 15 : 99;
            string[] files = (string[])e.Data.GetData(DataFormats.FileDrop);
            string dropTo = CurrentFolder;
            foreach (string file in files)
                await Converter.ConvertFile(file, dropTo, limit);

            SafeMode(false);
            UpdateSongList();
        }

        private void Form1_DragLeave(object sender, EventArgs e)
        {
            dragDropPanel.Visible = false;
        }

        private void BtnMoveSong_Click(object sender, EventArgs e)
        {
            if (songList.SelectedIndex == -1) return;

            if (Settings.UseNewNaming)
            {
                Player.MoveTo(Settings.GamePath, Player.WorkingSongList[songList.SelectedIndex], playerCD.Checked);
                return;
            }
            Player.MoveTo(Settings.GamePath, songList.SelectedItem.ToString(), playerCD.Checked);
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
                Updates.DownloadUpdate(Settings.Preview);
                return;
            }

            Updates.LookForAnUpdate(Settings.Preview);
        }

        private void BtnShowConversionLog_Click(object sender, EventArgs e)
        {
            Settings.ShowConversionLog ^= true;
        }

        private void BtnDownloadUpdate_Click(object sender, EventArgs e)
        {
            Updates.DownloadUpdate(Settings.Preview);
        }

        private void BtnAutoSort_Click(object sender, EventArgs e)
        {
            Settings.AutoSort ^= true;
        }

        private async void BtnDownload_Click(object sender, EventArgs e)
        {
            if (Functions.AreAllBusy())
            {
                Log += "\nProgram is busy.";
                return;
            }

            SafeMode(true);
            btnDownload.Enabled = txtboxVideo.Enabled = false;

            string url = txtboxVideo.Text;
            string forcedName = null;

            if (!url.Contains("https://"))
            {
                if (url == "")
                {
                    SafeMode(false);
                    return;
                }

                txtboxVideo.Text = txtboxVideo.Text.Replace('"', '\0');
                url = $"ytsearch:\"{txtboxVideo.Text}\"";
                if (Settings.UseNewNaming)
                    forcedName = txtboxVideo.Text;
            }

            await Downloader.DownloadFile(url, CurrentFolder, playerCD.Checked ? 15 : 99, forcedName);
            btnDownload.Enabled = txtboxVideo.Enabled = true;
            txtboxVideo.Text = "";
            SafeMode(false);
        }

        private void TxtboxVideo_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
                BtnDownload_Click(sender, e);
        }

        private void BtnDesktopShortcut_Click(object sender, EventArgs e)
        {
            if (!DesktopShortcut.ShortcutExist())
                DesktopShortcut.Create();
        }

        private void BtnNewNaming_Click(object sender, EventArgs e)
        {
            if (!Settings.UseNewNaming)
            {
                DialogResult dl = MessageBox.Show("This feature is still in development. " +
                                                "Some features may or may not work, or be bugged. Are you sure you want to contine?", 
                                                "Question", 
                                                MessageBoxButtons.YesNo, 
                                                MessageBoxIcon.Stop);

                if (dl != DialogResult.Yes)
                    return;
            }

            Player.Stop();
            Settings.UseNewNaming ^= true;

            if (Settings.UseNewNaming)
            {
                DialogResult getMeta = MessageBox.Show("Would you like the MSCMM to get meta datas from files now? (It may take some time)",
                    "Question",
                    MessageBoxButtons.YesNo,
                    MessageBoxIcon.Question);

                if (getMeta == DialogResult.Yes)
                {
                    SafeMode(true);
                    MetaData.GetSongsFromAll($"Radio");
                    MetaData.GetSongsFromAll($"CD");
                    SafeMode(false);
                }
            }
            else
            {
                DialogResult removeMeta = MessageBox.Show("Would you like to remove meta files?",
                                                          "Question",
                                                          MessageBoxButtons.YesNo,
                                                          MessageBoxIcon.Question);

                if (removeMeta == DialogResult.Yes)
                {
                    MetaData.RemoveAll($"Radio");
                    MetaData.RemoveAll($"CD");
                }
            }

            UpdateSongList();
        }
    }
}
