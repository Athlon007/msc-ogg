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
using System.Diagnostics;
using System.Drawing;
using System.Threading.Tasks;

namespace OggConverter
{
    public partial class Form1 : Form
    {
        public static Form1 instance;

        readonly bool firstLoad; // Disables some features if enabled

        public ToolStripMenuItem ButtonGetUpdate;
        public ProgressBar DownloadProgress;

        /// <summary>
        /// Checks what radio button is selected and decides what's the current folder
        /// </summary>
        private string CurrentFolder { get => playerCD.Checked ? "CD" : "Radio"; }

        // Stores last selected item on songList. Set to -1 by default so nothing's checked
        int lastSelected = -1;

        public Form1()
        {
            InitializeComponent();
            instance = this;
            
            Log($"MSC Music Manager {Utilities.GetVersion()} ({Updates.version})");

            // Checking if MSCMM isn't installed in My Summer Car's folder
            if (File.Exists("mysummercar.exe") || File.Exists("steam_api.dll") || File.Exists("steam_api64.dll"))
            {
                MessageBox.Show("Looks like you installed Music Manager into My Summer Car root path. " +
                    "While nothing should happen, I'm not not responsible for any damages done to you/your computer/your game/" +
                    "Satsuma/Teimo/or anything other at all!\n\n" +
                    "I'll also bother you with this message until you move Music Manager somewhere else every time you start the tool ;)",
                    "Ruh-roh", 
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Information);
            }

            // Setting up UI elements
            playerRadio.Checked = true; // Sets Radio as default folder
            ButtonGetUpdate = btnDownloadUpdate;
            ButtonGetUpdate.Click += BtnCheckUpdate_Click;
            songList.DoubleClick += BtnPlay;
            DownloadProgress = downloadProgress;

            // Setting up unicode characters for buttons
            btnUp.Text = char.ConvertFromUtf32(0x2191); // Up arrow
            btnDown.Text = char.ConvertFromUtf32(0x2193); // Down arrow
            btnPlaySong.Text = char.ConvertFromUtf32(0x25B6); // Play (triangle pointed to right)
            btnStop.Text = char.ConvertFromUtf32(0x25A0); // Pause (square)
            btnDel.Text = char.ConvertFromUtf32(0x232B); // Delete (arrow with X inside)

            // Positioning UI elemenmts
            dragDropPanel.Dock = DockStyle.Fill;
            tabControl1.ItemSize = new Size((tabControl1.Width / tabControl1.TabCount) - 2, 0);
            btnPlaySong.Left = btnPlaySong.CenterHorizontally(panel1) - btnPlaySong.Width / 2 - 2;
            btnStop.Left = btnStop.CenterHorizontally(panel1) + btnStop.Width / 2 + 2;
            playerRadio.Left = playerRadio.CenterHorizontally(panel1) - playerRadio.Width / 2 - 2;
            playerCD.Left = playerRadio.CenterHorizontally(panel1) + playerRadio.Width / 2 + 6;

            // Removing temporary or unused files
            Utilities.Cleanup();

            // Checking file validity   
            if (!Settings.AreSettingsValid())
            {
                // There was some kind of problem while starting.
                // Launching the first start sequence

                MessageBox.Show("Hello there! We've asked Teimo nicely where My Summer Car is installed, but he wouldn't tell me at all!.\n\n" +
                    "Please select where the game is installed :)", "Terve", MessageBoxButtons.OK, MessageBoxIcon.Information);

                Log("\nSelect My Summer Car Directory\nEx. C:\\Steam\\steamapps\\common\\My Summer Car\\.");
                firstLoad = true;
                SafeMode(true);
                return;
            }
            
            if ((!Directory.Exists(Settings.GamePath)) || (!File.Exists($"{Settings.GamePath}\\mysummercar.exe")))
            {
                MessageBox.Show("Couldn't find mysummercar.exe.\n\nPlease set the correct game path.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                Log("\nCouldn't find mysummercar.exe. Please set the correct game path.");
                firstLoad = true;
                SafeMode(true);
                return;
            }

            Log($"Game Folder: {Settings.GamePath}");

            // Setting Settings settings (hehe)
            btnRemMP3.Checked = Settings.RemoveMP3;
            btnNoSteam.Checked = Settings.NoSteam;
            btnAfterLaunchGame.Checked = Settings.LaunchAfterConversion;
            btnAfterClose.Checked = Settings.CloseAfterConversion;
            btnAfterNone.Checked = !Settings.CloseAfterConversion && !Settings.LaunchAfterConversion;
            btnUpdates.Checked = !Settings.NoUpdates;
            btnLogs.Checked = Settings.Logs;
            btnAutoSort.Checked = Settings.AutoSort;
            btnHistory.Checked = Settings.History;
            btnDisableMetafiles.Checked = Settings.DisableMetaFiles;

            // Checks if ffmpeg or ffplay are missing
            // If so, they will be downloaded and the tool will be restarted.
            if (!File.Exists("ffmpeg.exe") || !File.Exists("ffplay.exe"))
            {
                SafeMode(true, true);
                MessageBox.Show("Hi there! In order to this tool to work, the ffmpeg and ffplay need to be downloaded.\n" +
                    "The tool will now download it and restart itself.", "Info", MessageBoxButtons.OK, MessageBoxIcon.Information);
                Updates.StartFFmpegDownload();
                return;
            }

            // Checking if some trackTemp files exist. They may be caused by crash
            if (File.Exists($"{Settings.GamePath}\\Radio\\trackTemp.ogg"))
            {
                Log("Found temp song file in Radio, possibly due to the crash\nTrying to fix it...");
                _ = Converter.ConvertFile($"{Settings.GamePath}\\Radio\\trackTemp.ogg", "Radio", 99);
            }

            if (File.Exists($"{Settings.GamePath}\\CD\\trackTemp.ogg"))
            {
                Log("Found temp song file in CD, possibly due to the crash\nTrying to fix it...");
                _ = Converter.ConvertFile($"{Settings.GamePath}\\CD\\trackTemp.ogg", "CD", 99);
            }

            // Showing legal notice if the tool is used for the first time
            if (Settings.LatestVersion == 0)
                Log("\n" + Utilities.AboutNotice);

            // User is using this release for first time
            if (Updates.version > Settings.LatestVersion)
            {
                // Displaying the changelog
                Log("\n" + Properties.Resources.changelog);

                // If the version is older than 2.1 (18151)
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
                        MetaData.GetMetaFromAllSongs("Radio");
                        MetaData.GetMetaFromAllSongs("CD");
                        UpdateSongList();
                        SafeMode(false);
                    }
                }

                if (Settings.LatestVersion == 0 && !DesktopShortcut.ShortcutExist())
                {
                    DialogResult dl = MessageBox.Show("Do you want to create desktop shortcut?", "Question", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                    if (dl == DialogResult.Yes)
                        DesktopShortcut.Create();
                }

                Settings.LatestVersion = Updates.version;
            }

            if (!Directory.Exists($"{Settings.GamePath}\\CD"))
            {
                Converter.SkipCD = true;
                Log("CD folder is missing (you're propably using 24.10.2017 version of the game or older), so it will be skipped.");
            }

            Log((Settings.Preview && !Settings.DemoMode) ? "YOU ARE USING PREVIEW UPDATE CHANNEL" : "");

            if (Settings.NoUpdates)
                Log("\nUpdates are disabled");
            else
                Updates.StartUpdateCheck();

            if (Settings.Preview)
            {
                btnUpdates.ForeColor = Color.Red;
                btnUpdates.Text = "Updates (Preview)";
            }

            mSCOGGToolStripMenuItem.Text += Settings.DemoMode ? " (DEMO MODE)" : "";
        }

        /// <summary>
        /// Disabled most features to prevent crashes and bugs.
        /// Used for initial setup and if the My Summer Car directory or file no longer exists.
        /// </summary>
        public void SafeMode(bool state, bool complete = false)
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
            btnShuffle.Enabled = state;

            if (complete)
            {
                menuTool.Enabled = state;
                btnDirectory.Enabled = state;
            }
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
            {
                logOutput.Invoke(new Action(delegate () 
                {
                    logOutput.Text += value;
                    logOutput.SelectionStart = logOutput.TextLength;
                    logOutput.ScrollToCaret();
                }));
                return;
            }

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
            labCounter.ForeColor = ((CurrentFolder == "CD") && (howManySongs > 15)) || ((CurrentFolder == "Radio") && (howManySongs > 99)) ? Color.Red : Color.Black;

            if (songList.Items.Count > lastSelected)
                songList.SelectedIndex = lastSelected;
        }        

        private void Log_TextChanged(object sender, EventArgs e)
        {
            logOutput.SelectionStart = logOutput.Text.Length;
            logOutput.ScrollToCaret();
        }

        private void BtnDirectory_Click(object sender, EventArgs e)
        {
            if (Utilities.IsToolBusy())
            {
                Log("Program is busy.");
                return;
            }

            using (var folderDialog = new FolderBrowserDialog())
            {
                var dialog = folderDialog.ShowDialog();

                if (dialog == DialogResult.OK && Directory.GetFiles(folderDialog.SelectedPath, "mysummercar.exe").Length != 0)
                {
                    Settings.GamePath = folderDialog.SelectedPath;
                    Log("My Summer Car folder loaded successfully!");

                    if (firstLoad)
                    {
                        Form1 f = new Form1();
                        Hide();
                        f.ShowDialog();
                        Close();
                    }
                }
                else if (dialog == DialogResult.Cancel)
                    Log("Operation canceled");
                else
                    Log("Couldn't find mysummercar.exe. " +
                        "Make sure you've selected the game's ROOT folder (ex. C:\\Steam\\steamapps\\common\\My Summer Car) and NOT Radio or CD!");
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
            if (Utilities.IsToolBusy())
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
            Utilities.LaunchGame();
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
                DialogResult dialogResult = MessageBox.Show(Settings.Preview ? "Would you like to disable preview updates?" :
                    "Would you like to enable preview updates?\n\nWarning: preview releases may be unstable and/or broken.",
                    "Question",
                    MessageBoxButtons.YesNo,
                    MessageBoxIcon.Question);

                if (dialogResult == DialogResult.Yes)
                {
                    Settings.Preview ^= true;

                    MessageBox.Show($"In order to {(Settings.Preview ? "update" : "downgrade")}, use 'Check for Update' button",
                        "Info",
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Information);
                }

                btnUpdates.Checked ^= true;
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
            MessageBox.Show(Utilities.AboutNotice, "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private void BtnPlay(object sender, EventArgs e)
        {
            if (songList.SelectedIndex == -1) return;

            Player.Play($"{Settings.GamePath}\\{CurrentFolder}\\{Player.WorkingSongList[songList.SelectedIndex]}.ogg");

            // Showing currenty playing song in label
            // If the song name is longer than 51 characters, only words below that number will be displayed
            string song = songList.SelectedItem.ToString();
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
            labNowPlaying.Left = labNowPlaying.CenterHorizontally(panel1);
            labNowPlaying.Visible = true;
        }

        private void BtnStop_Click(object sender, EventArgs e)
        {
            labNowPlaying.Visible = false;
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

            if (Utilities.IsToolBusy())
            {
                Log("Program is busy.");
                return;
            }

            string songName = songList.SelectedItem.ToString();
            string fileName = $"{Player.WorkingSongList[songList.SelectedIndex]}.ogg";

            string file = $"{Settings.GamePath}\\{CurrentFolder}\\{fileName}";
            string meta = $"{Settings.GamePath}\\{CurrentFolder}\\{Player.WorkingSongList[songList.SelectedIndex]}.mscmm";

            DialogResult dl = MessageBox.Show($"Are you sure you want to delete:\n\n{songName}?", "Question", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (dl == DialogResult.Yes)
            {
                Player.Stop();

                while (!Utilities.IsFileReady(file)) { }

                if (File.Exists(file))
                    File.Delete(file);

                if (File.Exists(meta))
                    File.Delete(meta);

                Logs.History($"Removed \"{songName}\" ({fileName}) from {CurrentFolder}");

                UpdateSongList();
            }

            if (Settings.AutoSort)
                Player.Sort(CurrentFolder);
        }

        private void PlayerCD_Click(object sender, EventArgs e)
        {
            if (Utilities.IsToolBusy())
            {
                Log("Program is busy.");
                return;
            }

            UpdateSongList();
            btnMoveSong.Text = "Radio";
        }

        private void PlayerRadio_Click(object sender, EventArgs e)
        {
            if (Utilities.IsToolBusy())
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

            // If any files have been found in Radio or CD, they will be converter immediately after activating program window
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
            if (Utilities.IsToolBusy())
                return;

            if (e.Data.GetDataPresent(DataFormats.FileDrop))
            {
                dragDropPanel.Visible = true;
                dragDropPanel.BringToFront();
                int filesLength = ((string[])e.Data.GetData(DataFormats.FileDrop)).Length;
                labelConvert.Text = $"Convert {filesLength} file{(filesLength > 1 ? "s" : "")} to {(CurrentFolder)}?\n\n(Drop to Confirm)";
                labelConvert.Left = labelConvert.CenterHorizontally(dragDropPanel);
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

            Updates.StartUpdateCheck();
        }

        private void BtnDownloadUpdate_Click(object sender, EventArgs e)
        {
            if (Updates.IsBusy) return;
            Updates.DownloadUpdate(Settings.Preview);
        }

        private void BtnAutoSort_Click(object sender, EventArgs e)
        {
            Settings.AutoSort ^= true;
        }

        private async void BtnDownload_Click(object sender, EventArgs e)
        {
            if (!Utilities.IsOnline()) return;

            if (Updates.IsYoutubeDlUpdating)
            {
                MessageBox.Show("youtube-dl is now updating or looking for the update. You'll be notified on Log panel when it's done :)",
                    "Stop",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Stop);
                return;
            }

            if (Utilities.IsToolBusy())
            {
                Log("Program is busy.");
                return;
            }

            if (!File.Exists("youtube-dl.exe"))
            {
                DialogResult dl = MessageBox.Show("In order to download the song, the tool requires youtube-dl to be downloaded. " +
                    "Press 'Yes' to download it now",
                    "Information",
                    MessageBoxButtons.YesNo,
                    MessageBoxIcon.Information);

                if (dl != DialogResult.Yes)
                    return;

                SafeMode(true);
                await Updates.GetYoutubeDl();
                // The program waits for youtube-dl to download. It checks that every 1 second
                while (Updates.IsYoutubeDlUpdating) { await Task.Delay(1000); }
            }

            SafeMode(true);
            btnDownload.Enabled = txtboxVideo.Enabled = false;

            string url = txtboxVideo.Text;
            string forcedName = null;

            if (!url.StartsWith("https://") || !url.StartsWith("http://"))
            {
                if (url == "")
                {
                    SafeMode(false);
                    return;
                }

                txtboxVideo.Text = txtboxVideo.Text.Replace('"', '\0');
                url = $"ytsearch:\"{txtboxVideo.Text}\"";
                forcedName = txtboxVideo.Text;
            }
            else
            {
                url = url.Trim();
            }

            await Downloader.DownloadFile(url, CurrentFolder, playerCD.Checked ? 15 : 99, forcedName);
            btnDownload.Enabled = txtboxVideo.Enabled = true;
            txtboxVideo.Text = "";
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

        private void BtnHelp_Click(object sender, EventArgs e)
        {
            MessageBox.Show("How to use:\n\n" +
                "- Drag and drop music files on the program's window or executable to quickly convert one or more songs\n" +
                "- Paste songs into Radio or CD folder in My Summer Car folder and click on the program's window - the program will detect new songs automatically\n" +
                "- Go to the 'Download' tab to get your songs directly from YouTube - either by using URL, or using search term\n\n" +
                "Use Shuffle to randomize songs order.\n" +
                "In Edit tab you can change song's displayed name.", 
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
            lastSelected = songList.SelectedIndex;

            // If Edit tab is not open, or nothing's selected - don't load the song name to edit box
            if (tabControl1.SelectedIndex != 2 || songList.SelectedIndex == -1)
                return;

            // Get current song name to Edit box
            txtSongName.Text = songList.SelectedItem.ToString();
        }

        private void TabControl1_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (songList.SelectedIndex == -1 || tabControl1.SelectedIndex != 2)
                return;

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

        private void BtnDisableMetafiles_Click(object sender, EventArgs e)
        {
            if (!Settings.DisableMetaFiles)
            {
                DialogResult dl = MessageBox.Show("Disabling metafiles will result in MSC Music Manager using file names, instead of saved song names.\n" +
                    "Are you sure you want to continue?",
                    "Question",
                    MessageBoxButtons.YesNo,
                    MessageBoxIcon.Stop);

                if (dl != DialogResult.Yes)
                {
                    btnDisableMetafiles.Checked ^= true;
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
                    MetaData.GetMetaFromAllSongs($"Radio");
                    MetaData.GetMetaFromAllSongs($"CD");
                    SafeMode(false);
                }
            }
            UpdateSongList();
        }

        private async void BtnYoutubeDlUpdate_Click(object sender, EventArgs e)
        {
            await Task.Run(() => Updates.LookForYoutubeDlUpdate(true));
        }
    }
}
