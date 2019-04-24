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
using System.Drawing;

namespace OggConverter
{
    public partial class Form1 : Form
    {
        public static Form1 instance;

        readonly bool firstLoad = false; //Detects if the program has been opened for the first time

        public ToolStripMenuItem btnGetUpdate;
        public Label LabNowPlaying;

        string CurrentFolder { get => playerCD.Checked ? "CD" : "Radio"; }

        public Form1()
        {
            InitializeComponent();

            instance = this;
            playerRadio.Checked = true;
            Log($"MSC Music Manager Preview {Functions.GetVersion()} ({Updates.version})");
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

            tabControl1.ItemSize = new Size((tabControl1.Width / tabControl1.TabCount) - 2, 0);
            btnPlaySong.Left = Functions.Center(panel1, btnPlaySong) - btnPlaySong.Width / 2 - 2;
            btnStop.Left = Functions.Center(panel1, btnStop) + btnStop.Width / 2 + 2;

            playerRadio.Left = Functions.Center(panel1, playerRadio) - playerRadio.Width / 2 - 2;
            playerCD.Left = Functions.Center(panel1, playerRadio) + playerRadio.Width / 2 + 6;

            Functions.RemoveOldFiles();
            
            if (Settings.SettingsAreValid())
            {
                //Path in textbox
                if ((!Directory.Exists(Settings.GamePath)) || (!File.Exists($"{Settings.GamePath}\\mysummercar.exe")))
                {
                    MessageBox.Show("Couldn't find mysummercar.exe.\n\nPlease set the correct game path.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    Log("\nCouldn't find mysummercar.exe. Please set the correct game path.");
                    firstLoad = true;
                    SafeMode(true);
                    return;
                }

                Log($"Game Folder: {Settings.GamePath}");

                // SETTINGS CHECKS
                btnRemMP3.Checked = Settings.RemoveMP3;
                btnNoSteam.Checked = Settings.NoSteam;
                // Actions after conversion
                btnAfterLaunchGame.Checked = Settings.LaunchAfterConversion;
                btnAfterClose.Checked = Settings.CloseAfterConversion;
                btnAfterNone.Checked = !Settings.CloseAfterConversion && !Settings.LaunchAfterConversion;
                btnUpdates.Checked = !Settings.NoUpdates;
                btnLogs.Checked = Settings.Logs;
                btnAutoSort.Checked = Settings.AutoSort;
                btnNewNaming.Checked = Settings.DisableMetaFiles;
                btnHistory.Checked = Settings.History;

                if (Settings.NoUpdates)
                    Log("\nUpdates are disabled");
                else
                {
                    if (Settings.Preview)
                        Updates.LookForAnUpdate(true);

                    Updates.LookForAnUpdate(false);
                    Log((Settings.Preview && !Settings.DemoMode) ? "YOU ARE USING PREVIEW UPDATE CHANNEL" : "");
                }

                if (Settings.Preview)
                {
                    btnUpdates.ForeColor = Color.Red;
                    btnUpdates.Text = "Updates (Preview)";
                }

                if (File.Exists($"{Settings.GamePath}\\Radio\\trackTemp.ogg"))
                {
                    Log("\nFound temp song file in Radio, possibly caused by crash\nTrying to fix it...");
                    _ = Converter.ConvertFile($"{Settings.GamePath}\\Radio\\trackTemp.ogg", "Radio", 99);
                }

                if (File.Exists($"{Settings.GamePath}\\CD\\trackTemp.ogg"))
                {
                    Log("\nFound temp song file in CD, possibly caused by crash\nTrying to fix it...");
                    _ = Converter.ConvertFile($"{Settings.GamePath}\\CD\\trackTemp.ogg", "CD", 99);
                }

                if (Updates.version > Settings.LatestVersion)
                {
                    Log("\n" + Properties.Resources.changelog);

                    if (Settings.LatestVersion <= 18151)
                    {
                        DialogResult dl = MessageBox.Show("Would you like MSC Music Manager to get song names from already existing songs?\n\n" +
                            "(It may take a while, depending on how many songs you have)",
                            "Question",
                            MessageBoxButtons.YesNo,
                            MessageBoxIcon.Question);

                        if (dl == DialogResult.Yes)
                        {
                            SafeMode(true);
                            MetaData.GetSongsFromAll("Radio");
                            MetaData.GetSongsFromAll("CD");
                            UpdateSongList();
                            SafeMode(false);
                        }
                    }

                    Settings.LatestVersion = Updates.version;
                }

                if (!Directory.Exists($"{Settings.GamePath}\\CD"))
                {
                    Converter.SkipCD = true;
                    Log("CD folder is missing (you're propably using 24.10.2017 version of the game or older), so it will be skipped.");
                }

                if (Settings.DemoMode)
                    mSCOGGToolStripMenuItem.Text += " (DEMO MODE)";
            }
            else
            {
                // There was some kind of problem while starting.
                // Launching the first start sequence

                MessageBox.Show("Hey there! Looks like you're new here. Select the game directory first :)\n\n" +
                    "If you see that message second, or more times, please contact the developer.", "Terve", MessageBoxButtons.OK, MessageBoxIcon.Information);

                Log("\nSelect My Summer Car Directory\nEx. C:\\Steam\\steamapps\\common\\My Summer Car\\.");
                Log("\n" + Functions.AboutNotice);
                firstLoad = true;
                SafeMode(true);
            }
        }

        /// <summary>
        /// Disabled most features to prevent crashes and bugs.
        /// Used for initial setup and if the My Summer Car directory or file no longer exists.
        /// </summary>
        public void SafeMode(bool state)
        {
            state ^= true;

            btnOpenGameDir.Enabled = state;
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
            btnSetName.Enabled = state;
            txtSongName.Enabled = state;
            btnCloneSong.Enabled = state;
        }

        /// <summary>
        /// Writes value to log output
        /// </summary>
        /// <param name="value">Value to output</param>
        public void Log(string value)
        {
            if (value == "")
                return;

            value = value.Replace("\n", Environment.NewLine);
            value += Environment.NewLine;

            if (logOutput.InvokeRequired)
                logOutput.Invoke(new Action(() => logOutput.Text += value));
            else
                logOutput.Text += value;

            logOutput.SelectionStart = logOutput.TextLength;
            logOutput.ScrollToCaret();
        }

        /// <summary>
        /// Updates song list used for player
        /// </summary>
        public void UpdateSongList()
        {
            if (firstLoad) return;

            songList.Items.Clear();
            string path = $"{Settings.GamePath}\\{(CurrentFolder)}";
            int howManySongs = 0;

            // Old name reading
            if (Settings.DisableMetaFiles)
            {
                for (int i = 1; i <= 99; i++)
                    if (File.Exists($"{path}\\track{i}.ogg"))
                        songList.Items.Add($"track{i}.ogg");

                return;
            }
            
            Player.WorkingSongList.Clear();

            for (int i = 1; i <= 99; i++)
                if (File.Exists($"{path}\\track{i}.ogg"))
                {
                    string s = MetaData.GetFromMeta(CurrentFolder, $"track{i}");
                    songList.Items.Add(s);
                    Player.WorkingSongList.Add($"track{i}");
                    howManySongs++;
                }

            labCounter.Text = $"Songs: {howManySongs}";
            labCounter.ForeColor = ((CurrentFolder == "CD") && (howManySongs > 15)) ? Color.Red : Color.Black;
            return;
        }        

        private void Log_TextChanged(object sender, EventArgs e)
        {
            logOutput.SelectionStart = logOutput.Text.Length;
            logOutput.ScrollToCaret();
        }

        private void BtnDirectory_Click(object sender, EventArgs e)
        {
            if (Functions.AreAllBusy())
            {
                Log("Program is busy.");
                return;
            }

            using (var folderDialog = new FolderBrowserDialog())
            {
                var dialog = folderDialog.ShowDialog();

                if (dialog == DialogResult.OK && Directory.GetFiles(folderDialog.SelectedPath, "mysummercar.exe").Length != 0)
                {
                    RegistryKey Key = Registry.CurrentUser.OpenSubKey(@"SOFTWARE\MSCOGG", true);
                    Key.SetValue("MSC Path", folderDialog.SelectedPath);
                    Log("My Summer Car directory detected");

                    if (firstLoad)
                    {
                        Form1 f = new Form1();
                        Hide();
                        f.ShowDialog();
                        Close();
                    }
                }
                else if (dialog == DialogResult.Cancel)
                    Log("\nCanceled");
                else
                    Log("\nCouldn't find mysummercar.exe");
            }
        }

        private void BtnOpenGameDir_Click(object sender, EventArgs e)
        {
            if (Settings.GamePath.Length == 0)
            {
                Log("Select game path first.");
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
                Log("Program is busy.");
                return;
            }

            Player.Stop();
        }


        private void OpenLastConversionToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (!File.Exists("history.txt"))
            {
                Log("History file doesn't exist");
                return;
            }

            if (ModifierKeys.HasFlag(Keys.Shift))
            {
                DialogResult dl = MessageBox.Show("Would you like to remove history file?", "Question", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                if (dl == DialogResult.Yes)
                    File.Delete("history.txt");

                return;
            }

            Process.Start("history.txt");
        }

        private void LaunchTheGameToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (ModifierKeys.HasFlag(Keys.Shift))
            {
                Process.Start($"{Settings.GamePath}\\mysummercar.exe");
                return;
            }
            Functions.LaunchGame();
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
            Process.Start("https://www.ffmpeg.org/about.html");
        }

        private void MSCOGGToolStripMenuItem_Click(object sender, EventArgs e)
        {
            MessageBox.Show(Functions.AboutNotice, "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private void BtnPlay(object sender, EventArgs e)
        {
            if (songList.SelectedIndex == -1) return;

            if (Settings.DisableMetaFiles)
                Player.Play($"{Settings.GamePath}\\{CurrentFolder}\\{songList.SelectedItem.ToString()}");
            else
                Player.Play($"{Settings.GamePath}\\{CurrentFolder}\\{Player.WorkingSongList[songList.SelectedIndex]}.ogg");

            string song = Settings.DisableMetaFiles ? $"Now Playing: {songList.SelectedItem.ToString()}" : songList.SelectedItem.ToString();
            string[] songSplit = song.Split(' ');
            song = "";
            for (int i = 0; i < songSplit.Length; i++)
            {
                string temp = song + " " + songSplit[i];
                if (temp.Length > 51)
                {
                    song += "...";
                    break;
                }
                song = temp;
            }
            labNowPlaying.Text = song;
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
                Log("Logs are disabled");
                return;
            }

            if (!Directory.Exists("Log"))
            {
                Log("Log folder doesn't exist");
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
                Log("Program is busy.");
                return;
            }

            string songName = songList.SelectedItem.ToString();
            string fileName = $"{Player.WorkingSongList[songList.SelectedIndex]}.ogg";

            string file = Settings.DisableMetaFiles ? $"{Settings.GamePath}\\{CurrentFolder}\\{songList.SelectedItem.ToString()}" :
                $"{Settings.GamePath}\\{CurrentFolder}\\{fileName}";

            string meta = Settings.DisableMetaFiles ? null : $"{Settings.GamePath}\\{CurrentFolder}\\{Player.WorkingSongList[songList.SelectedIndex]}.mscmm";

            DialogResult dl = MessageBox.Show($"Are you sure you want to delete:\n\n{songName}?", "Question", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (dl == DialogResult.Yes)
            {
                Player.Stop();

                while (!Functions.IsFileReady(file)) { }

                if (File.Exists(file))
                    File.Delete(file);

                if (!Settings.DisableMetaFiles)
                {
                    if (File.Exists(meta))
                        File.Delete(meta);
                }

                Logs.History($"Removed \"{songName}\" ({fileName}) from {CurrentFolder}");

                UpdateSongList();
            }

            if (Settings.AutoSort)
                Player.Sort(CurrentFolder);
        }

        private void PlayerCD_Click(object sender, EventArgs e)
        {
            if (Functions.AreAllBusy())
            {
                Log("Program is busy.");
                return;
            }

            UpdateSongList();
            btnMoveSong.Text = "Radio";
        }

        private void PlayerRadio_Click(object sender, EventArgs e)
        {
            if (Functions.AreAllBusy())
            {
                Log("Program is busy.");
                return;
            }

            UpdateSongList();
            btnMoveSong.Text = "CD";
        }

        private void BtnSort_Click(object sender, EventArgs e)
        {
            Player.Sort(CurrentFolder);
        }

        private void Form1_Activated(object sender, EventArgs e)
        {
            if (firstLoad) return;

            UpdateSongList();

            if (Converter.FilesWaitingForConversion("Radio") || Converter.FilesWaitingForConversion("CD"))
                Converter.StartConversion();
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
                labelConvert.Text = $"Convert {filesLength} file{(filesLength > 1 ? "s" : "")} to {(CurrentFolder)}?\n\n(Drop to Confirm)";
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

            if (Settings.DisableMetaFiles)
            {
                Player.MoveTo(Settings.GamePath, songList.SelectedItem.ToString(), playerCD.Checked);
                return;
            }
            Player.MoveTo(Settings.GamePath, Player.WorkingSongList[songList.SelectedIndex], playerCD.Checked);
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
                Log("\nForcing the update...");
                Updates.DownloadUpdate(Settings.Preview);
                return;
            }

            Updates.LookForAnUpdate(Settings.Preview);
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
                Log("Program is busy.");
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
                if (!Settings.DisableMetaFiles)
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
            if (!Settings.DisableMetaFiles)
            {
                DialogResult dl = MessageBox.Show("Disabling metafiles will result in MSC Music Manager using file names, instead of real song names.\n" +
                    "Are you sure you want to continue?", 
                    "Question",
                    MessageBoxButtons.YesNo, 
                    MessageBoxIcon.Stop);

                if (dl != DialogResult.Yes)
                {
                    btnNewNaming.Checked ^= true;
                    return;
                }
            }

            Player.Stop();
            Settings.DisableMetaFiles ^= true;

            if (Settings.DisableMetaFiles)
            {
                DialogResult removeMeta = MessageBox.Show("Would you like to remove ALL meta files?",
                "Question",
                MessageBoxButtons.YesNo,
                MessageBoxIcon.Question);

                if (removeMeta == DialogResult.Yes)
                {
                    MetaData.RemoveAll($"Radio");
                    MetaData.RemoveAll($"CD");
                }
            }
            else
            {
                DialogResult getMeta = MessageBox.Show("Would you like the MSCMM to get song names directly from files now? " +
                    "(It may take some time, depending on how many songs you have)",
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
            UpdateSongList();
        }

        private void BtnHelp_Click(object sender, EventArgs e)
        {
            MessageBox.Show("How to use:\n\n- Drag and drop music files on the program's window or executable to quickly convert one or more songs\n" +
                "- Paste songs into Radio or CD folder in My Summer Car folder\n" +
                "- Go to the 'Download' tab to get your songs directly from YouTube - either by using URL, or using search term", 
                "Help", 
                MessageBoxButtons.OK, 
                MessageBoxIcon.Information);
        }

        private void BtnHistory_Click(object sender, EventArgs e)
        {
            Settings.History ^= true;
        }

        private void SongList_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (tabControl1.SelectedIndex != 2 || songList.SelectedIndex == -1)
                return;

            txtSongName.Text = songList.SelectedItem.ToString();
        }

        private void TabControl1_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (songList.SelectedIndex == -1)
                return;

            if (tabControl1.SelectedIndex == 2)
                txtSongName.Text = songList.SelectedItem.ToString();
        }

        private void BtnSetName_Click(object sender, EventArgs e)
        {
            if (songList.SelectedIndex == -1)
                return;

            int selected = songList.SelectedIndex;
            MetaData.CreateMetaFile($"{Settings.GamePath}\\{CurrentFolder}\\{Player.WorkingSongList[songList.SelectedIndex]}.mscmm", txtSongName.Text);
            UpdateSongList();
            songList.SelectedIndex = selected;
        }

        private void BtnCloneSong_Click(object sender, EventArgs e)
        {
            if (songList.SelectedIndex == -1)
                return;

            Player.Clone(CurrentFolder, Player.WorkingSongList[songList.SelectedIndex]);
            UpdateSongList();
        }

        private void BtnShuffle_Click(object sender, EventArgs e)
        {
            Player.Shuffle(CurrentFolder);
            UpdateSongList();
        }
    }
}
