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
using System.Net;
using System.Collections.Generic;
using System.Linq;

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
        public string CurrentFolder
        {
            //get => selectedFolder.Text;
            get
            {
                if (selectedFolder.InvokeRequired)
                {
                    string val = "";
                    selectedFolder.Invoke(new Action(delegate ()
                    {
                        val = selectedFolder.Text;
                    }));
                    return val;
                }

                return selectedFolder.Text;
            }
        }
        private int SongsLimit { get => CurrentFolder.StartsWith("CD") ? 15 : 99; }

        // Stores last selected item on songList. Set to -1 by default so nothing's checked
        int lastSelected = -1;

        public Form1()
        {
            InitializeComponent();
            instance = this;
            this.KeyPreview = true;

#if DEBUG
            Log($"MSC Music Manager {Utilities.GetVersion(true)} ({Updates.version}) DEBUG\n" +
                $"THIS VERSION IS NOT INTENDED FOR DISTRIBUTION!");
#else
            Log($"MSC Music Manager {Utilities.GetVersion()} ({Updates.version})");
#endif

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

            WebRequest.DefaultWebProxy = null;

            // Setting up UI elements
            selectedFolder.SelectedIndex = 0;
            ButtonGetUpdate = btnDownloadUpdate;
            ButtonGetUpdate.Click += BtnCheckUpdate_Click;
            songList.DoubleClick += BtnPlay;
            DownloadProgress = downloadProgress;

            // Setting up unicode characters for buttons
            btnUp.Text = char.ConvertFromUtf32(0x2191); // Up arrow
            btnDown.Text = char.ConvertFromUtf32(0x2193); // Down arrow
            btnPlaySong.Text = char.ConvertFromUtf32(0x25B6); // Play (triangle pointed to right)
            btnStop.Text = char.ConvertFromUtf32(0x25A0); // Pause (square)

            // Positioning UI elemenmts
            dragDropPanel.Dock = DockStyle.Fill;
            tabs.ItemSize = new Size((tabs.Width / tabs.TabCount) - 2, 0);
            btnPlaySong.Left = btnPlaySong.CenterHorizontally(panel1) - btnPlaySong.Width / 2 - 2;
            btnStop.Left = btnStop.CenterHorizontally(panel1) + btnStop.Width / 2 + 2;
            selectedFolder.Left = selectedFolder.CenterHorizontally(panel1);

            // Removing temporary or unused files
            Utilities.Cleanup();

            // Checking file validity
            if (!Settings.AreSettingsValid())
            {
                firstLoad = true;
                // There was some kind of problem while starting.
                // Launching the first start sequence

                MessageBox.Show("Hello there! I've asked Teimo nicely where My Summer Car is installed, but he wouldn't tell me at all!.\n\n" +
                    "Please select where the game is installed :)", "Terve", MessageBoxButtons.OK, MessageBoxIcon.Information);

                Log("\nSelect My Summer Car Directory\nEx. C:\\Steam\\steamapps\\common\\My Summer Car\\.");
                RestrictedMode(true);
                return;
            }

            if ((!Directory.Exists(Settings.GamePath)) || (!File.Exists($"{Settings.GamePath}\\mysummercar.exe")))
            {
                MessageBox.Show("Couldn't find mysummercar.exe.\n\nPlease set the correct game path.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                Log("\nCouldn't find mysummercar.exe. Please set the correct game path.");
                firstLoad = true;
                RestrictedMode(true);
                return;
            }

            Log($"Game Folder: {Settings.GamePath}");

            // Checks if ffmpeg or ffplay are missing
            // If so, they will be downloaded and the tool will be restarted.
            if (!File.Exists("ffmpeg.exe") || !File.Exists("ffplay.exe"))
            {
                RestrictedMode(true, true);
                MessageBox.Show("Hi there! In order to this tool to work, the ffmpeg and ffplay need to be downloaded.\n" +
                    "The tool will now download it and restart itself.", "Info", MessageBoxButtons.OK, MessageBoxIcon.Information);
                Updates.StartFFmpegDownload();
                return;
            }

            try
            {
                // Checking if some trackTemp files exist. They may be caused by crash
                if (File.Exists($"{Settings.GamePath}\\Radio\\trackTemp.ogg"))
                {
                    Log("Found temp song file in Radio, possibly due to the crash\nTrying to fix it...");
                    _ = Converter.ConvertFile($"{Settings.GamePath}\\Radio\\trackTemp.ogg", "Radio", 99);
                }

                if (Directory.Exists($"{Settings.GamePath}\\CD") && File.Exists($"{Settings.GamePath}\\CD\\trackTemp.ogg"))
                {
                    Log("Found temp song file in CD, possibly due to the crash\nTrying to fix it...");
                    _ = Converter.ConvertFile($"{Settings.GamePath}\\CD\\trackTemp.ogg", "CD", 99);
                }

                if (Directory.Exists($"{Settings.GamePath}\\CD1") && File.Exists($"{Settings.GamePath}\\CD1\\trackTemp.ogg"))
                {
                    Log("Found temp song file in CD1, possibly due to the crash\nTrying to fix it...");
                    _ = Converter.ConvertFile($"{Settings.GamePath}\\CD1\\trackTemp.ogg", "CD1", 99);
                }

                if (Directory.Exists($"{Settings.GamePath}\\CD2") && File.Exists($"{Settings.GamePath}\\CD2\\trackTemp.ogg"))
                {
                    Log("Found temp song file in CD2, possibly due to the crash\nTrying to fix it...");
                    _ = Converter.ConvertFile($"{Settings.GamePath}\\CD2\\trackTemp.ogg", "CD2", 99);
                }

                if (Directory.Exists($"{Settings.GamePath}\\CD3") && File.Exists($"{Settings.GamePath}\\CD3\\trackTemp.ogg"))
                {
                    Log("Found temp song file in CD3, possibly due to the crash\nTrying to fix it...");
                    _ = Converter.ConvertFile($"{Settings.GamePath}\\CD3\\trackTemp.ogg", "CD3", 99);
                }

                // Converting song name listing to new format
                if (!File.Exists($"{Settings.GamePath}\\Radio\\songnames.xml"))
                {
                    MetaData.ConvertFromMscmm("Radio");
                    Log("Converted Radio folder from .MSCMM to XML database.");
                }

                // Checks if old CD folder exists instead of the new ones
                if (Directory.Exists($"{Settings.GamePath}\\CD"))
                {
                    if (!File.Exists($"{Settings.GamePath}\\CD\\songnames.xml"))
                    {
                        MetaData.ConvertFromMscmm("CD");
                        Log("Converted CD folder from .MSCMM to XML database.");
                    }
                }
                else if (Directory.Exists($"{Settings.GamePath}\\CD1"))
                {
                    if (!File.Exists($"{Settings.GamePath}\\CD1\\songnames.xml"))
                    {
                        MetaData.ConvertFromMscmm("CD1");
                        Log("Converted CD1 folder from .MSCMM to XML database.");
                    }

                    if (!File.Exists($"{Settings.GamePath}\\CD2\\songnames.xml"))
                    {
                        MetaData.ConvertFromMscmm("CD2");
                        Log("Converted CD2 folder from .MSCMM to XML database.");
                    }

                    if (!File.Exists($"{Settings.GamePath}\\CD3\\songnames.xml"))
                    {
                        MetaData.ConvertFromMscmm("CD3");
                        Log("Converted CD3 folder from .MSCMM to XML database.");
                    }
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
                            "(It may take a while, depending on how many songs you have).",
                            "Question",
                            MessageBoxButtons.YesNo,
                            MessageBoxIcon.Question);

                        if (dl == DialogResult.Yes)
                        {
                            RestrictedMode(true);
                            MetaData.GetMetaFromAllSongs("Radio");
                            MetaData.GetMetaFromAllSongs("CD");
                            MetaData.GetMetaFromAllSongs("CD1");
                            MetaData.GetMetaFromAllSongs("CD2");
                            MetaData.GetMetaFromAllSongs("CD3");
                            UpdateSongList();
                            RestrictedMode(false);
                        }
                    }

                    if (Settings.LatestVersion == 0 && !DesktopShortcut.Exists())
                    {
                        DialogResult dl = MessageBox.Show("Do you want to create desktop shortcut?", "Question", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                        if (dl == DialogResult.Yes)
                            DesktopShortcut.Create();
                    }

                    Settings.LatestVersion = Updates.version;
                }

                if (Directory.Exists($"{Settings.GamePath}\\CD") && Directory.Exists($"{Settings.GamePath}\\CD1"))
                {
                    DialogResult dl = MessageBox.Show("Looks like you've updated to the new My Summer Car version which now supports extra 2 CDs. " +
                        "All songs from the original CD have to be imported to the new CD1 folder in order to be read by My Summer Car. " +
                        "Press OK to do that now, or Cancel to exit.\n\n" +
                        "WARNING: This will override currently existing files in CD1!", "Question", MessageBoxButtons.OKCancel, MessageBoxIcon.Question);

                    if (dl == DialogResult.OK)
                    {
                        DirectoryInfo di = new DirectoryInfo($"{Settings.GamePath}\\CD");
                        FileInfo[] files = di.GetFiles();
                        foreach (var file in files)
                            File.Move($"{Settings.GamePath}\\CD\\{file.Name}", $"{Settings.GamePath}\\CD1\\{file.Name}");

                        Log("Succesfully imported CD songs to new CD1 folder!");
                        Directory.Delete($"{Settings.GamePath}\\CD", true);
                    }
                    else
                    {
                        Application.Exit();
                    }
                }

                if (Directory.Exists($"{Settings.GamePath}\\CD1") && !Directory.Exists($"{Settings.GamePath}\\CD"))
                {
                    selectedFolder.Items.RemoveAt(1);
                }
                else
                {
                    selectedFolder.Items.RemoveAt(4);
                    selectedFolder.Items.RemoveAt(3);
                    selectedFolder.Items.RemoveAt(2);
                }
            }
            catch (Exception ex)
            {
                ErrorMessage err = new ErrorMessage(ex);
                err.ShowDialog();
            }

            Log((Settings.Preview && !Settings.DemoMode) ? "YOU ARE USING PREVIEW VERSION" : "");

            if (Settings.NoUpdates)
                Log("\nUpdates are disabled");
            else
                Updates.StartUpdateCheck();

            mSCOGGToolStripMenuItem.Text += Settings.DemoMode ? " (DEMO MODE)" : "";

            // Tooltips
            ToolTip toolTip = new ToolTip
            {
                AutoPopDelay = 5000,
                InitialDelay = 1000,
                ReshowDelay = 500,
                ShowAlways = true
            };

            toolTip.SetToolTip(btnSort, "Sorts all songs so there are no gaps between songs (ex. if there's track1 and track3, the track3 will be renamed to track2).");
            toolTip.SetToolTip(btnUp, "Move selected song one up.");
            toolTip.SetToolTip(btnDown, "Move selected song one down.");
            toolTip.SetToolTip(btnMoveSong, "Move selected songs to selected folder.");
            toolTip.SetToolTip(btnCloneSong, "Clones selected song with it's displayed name.");
            toolTip.SetToolTip(btnDel, "Deletes selected song.");
            toolTip.SetToolTip(btnShuffle, "Randomizes songs order in chosen folder.");
            toolTip.SetToolTip(btnOpenGameDir, "Opens My Summer Car folder in Explorer.");
            toolTip.SetToolTip(btnDirectory, "Lets you change My Summer Car folder.");
        }

        /// <summary>
        /// Disabled most features to prevent crashes and bugs.
        /// Used for initial setup and if the My Summer Car directory or file no longer exists.
        /// </summary>
        public void RestrictedMode(bool state, bool complete = false)
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
            btnMoveSong.Enabled = state;
            txtboxVideo.Enabled = state;
            btnDownload.Enabled = state;
            songList.Enabled = state;
            btnSetName.Enabled = state;
            txtSongName.Enabled = state;
            btnCloneSong.Enabled = state;
            btnShuffle.Enabled = state;
            selectedFolder.Enabled = state;

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

            string path = $"{Settings.GamePath}\\{(CurrentFolder)}";
            int howManySongs = 0;

            if (!File.Exists(MetaData.XmlFilePath()))
                MetaData.ConvertFromMscmm(CurrentFolder);

            List<string> newTrackList = new List<string>();
            Player.WorkingSongList.Clear();

            for (int i = 1; i <= 99; i++)
                if (File.Exists($"{path}\\track{i}.ogg"))
                {
                    string s = MetaData.GetName($"track{i}");
                    newTrackList.Add(s);
                    Player.WorkingSongList.Add(new Tuple<string, string>($"track{i}", s));
                    howManySongs++;
                }

            songList.Items.Clear();
            songList.Items.AddRange(newTrackList.ToArray());

            labCounter.Text = $"Songs: {howManySongs}/{SongsLimit}";
            labCounter.ForeColor = howManySongs > SongsLimit ? Color.Red : Color.Black;

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
                Log("Set the game path first.");
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
                Log("History file doesn't exist.");
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

        private void QuitToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void SteamCommunityDiscussionToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Process.Start("http://steamcommunity.com/app/516750/discussions/2/1489992713697876617/");
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

            if (ModifierKeys.HasFlag(Keys.Shift))
            {
                Process.Start($"{Settings.GamePath}\\{CurrentFolder}\\{Player.WorkingSongList[songList.SelectedIndex].Item1}.ogg");
                return;
            }

            Player.Play($"{Settings.GamePath}\\{CurrentFolder}\\{Player.WorkingSongList[songList.SelectedIndex].Item1}.ogg");

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

            int[] domains = songList.SelectedIndices.OfType<int>().ToArray();
            List<string> deleteList = new List<string>();

            foreach (int i in domains)
                deleteList.Add(Player.WorkingSongList[i].Item1.ToString());

            Player.Delete(CurrentFolder, deleteList.ToArray());
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
            if (Converter.AnyFilesWaitingForConversion())
                Converter.StartConversion();
        }

        private void BtnUp_Click(object sender, EventArgs e)
        {
            if (songList.SelectedIndex == -1) return;
            Player.ChangeOrder(songList, CurrentFolder, true);
        }

        private void BtnDown_Click(object sender, EventArgs e)
        {
            if (songList.SelectedIndex == -1) return;
            Player.ChangeOrder(songList, CurrentFolder, false);
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
            RestrictedMode(true);
            dragDropPanel.Visible = false;

            try
            {
                string[] files = (string[])e.Data.GetData(DataFormats.FileDrop);
                string dropTo = CurrentFolder;
                foreach (string file in files)
                    await Converter.ConvertFile(file, dropTo, SongsLimit, file.Split('\\').Last().Split('.').First());
            }
            catch (Exception ex)
            {
                ErrorMessage err = new ErrorMessage(ex);
                err.ShowDialog();
            }
            finally
            {
                RestrictedMode(false);
                UpdateSongList();
            }
        }

        private void Form1_DragLeave(object sender, EventArgs e)
        {
            dragDropPanel.Visible = false;
        }

        private void BtnMoveSong_Click(object sender, EventArgs e)
        {
            if (songList.SelectedIndex == -1) return;

            Player.Stop();

            int[] domains = songList.SelectedIndices.OfType<int>().ToArray();
            List<string> moveList = new List<string>();

            foreach (int i in domains)
                moveList.Add(Player.WorkingSongList[i].Item1.ToString());

            MoveTo moveTo = new MoveTo(moveList.ToArray(), CurrentFolder);
            moveTo.ShowDialog();
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

        private async void BtnDownload_Click(object sender, EventArgs e)
        {
            if (!Utilities.IsOnline() || txtboxVideo.Text == "") return;

            btnCancelDownload.Enabled = true;

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
                    "Press 'Yes' to download it now.",
                    "Information",
                    MessageBoxButtons.YesNo,
                    MessageBoxIcon.Information);

                if (dl != DialogResult.Yes)
                    return;

                RestrictedMode(true);
                await Updates.GetYoutubeDl();
                // The program waits for youtube-dl to download. It checks that every 1 second
                while (Updates.IsYoutubeDlUpdating) { await Task.Delay(1000); }
            }

            RestrictedMode(true);
            btnDownload.Enabled = txtboxVideo.Enabled = false;

            string url = txtboxVideo.Text;
            string forcedName = null;

            if (!url.StartsWith("https://") || !url.StartsWith("http://"))
            {
                if (url == "")
                {
                    RestrictedMode(false);
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

            await Downloader.DownloadFile(url, CurrentFolder, SongsLimit, forcedName);

            btnDownload.Enabled = txtboxVideo.Enabled = true;
            txtboxVideo.Text = "";
        }

        private void TxtboxVideo_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
                BtnDownload_Click(sender, e);
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

        private void SongList_SelectedIndexChanged(object sender, EventArgs e)
        {
            lastSelected = songList.SelectedIndex;

            // If Edit tab is not open, or nothing's selected - don't load the song name to edit box
            if (tabs.SelectedIndex != 2 || songList.SelectedIndex == -1)
                return;

            // Get current song name to Edit box
            txtSongName.Text = songList.SelectedItem.ToString();
        }

        private void TabControl1_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (songList.SelectedIndex == -1 || tabs.SelectedIndex != 2)
                return;

            txtSongName.Text = songList.SelectedItem.ToString();
        }

        private void BtnSetName_Click(object sender, EventArgs e)
        {
            if (songList.SelectedIndex == -1)
                return;

            int selected = songList.SelectedIndex;
            MetaData.AddOrEdit($"{Player.WorkingSongList[songList.SelectedIndex].Item1}", txtSongName.Text);
            UpdateSongList();
            songList.SelectedIndex = selected;
        }

        private void BtnCloneSong_Click(object sender, EventArgs e)
        {
            if (songList.SelectedIndex == -1)
                return;

            Player.Clone(CurrentFolder, Player.WorkingSongList[songList.SelectedIndex].Item1);
            UpdateSongList();
        }

        private void BtnShuffle_Click(object sender, EventArgs e)
        {
            Player.Shuffle(CurrentFolder);
            UpdateSongList();
        }

        private async void BtnYoutubeDlUpdate_Click(object sender, EventArgs e)
        {
            await Task.Run(() => Updates.LookForYoutubeDlUpdate(true));
        }

        private void SelectedFolder_SelectedIndexChanged(object sender, EventArgs e)
        {
            UpdateSongList();
        }

        private void TxtSongName_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
                btnSetName.PerformClick();
        }

        private void BtnWebsite_Click(object sender, EventArgs e)
        {
            Process.Start("http://athlon.kkmr.pl");
        }

        private void BtnCancelDownload_Click(object sender, EventArgs e)
        {
            Downloader.Cancel();
            RestrictedMode(false);
            btnCancelDownload.Enabled = false;
        }

        private void MenuSettings_Click(object sender, EventArgs e)
        {
            SettingsForm form = new SettingsForm();
            form.Show();
        }

        private void SongList_KeyDown(object sender, KeyEventArgs e)
        {
            // Select all items on songlist (Ctrl+A)
            if (e.Control && e.KeyCode == Keys.A)
                for (int i = 0; i < songList.Items.Count; i++)
                    songList.SelectedItem = songList.Items[i];

            // Clone (Ctrl+C)
            if (e.Control && e.KeyCode == Keys.C)
                btnCloneSong.PerformClick();

            // Move (Ctrl+X)
            if (e.Control && e.KeyCode == Keys.X)
                btnMoveSong.PerformClick();

            // Delete selected item (Del)
            if (e.KeyCode == Keys.Delete)
                btnDel.PerformClick();

            // Play or stop playing the song (Enter)
            if (e.KeyCode == Keys.Enter)
            {
                if (labNowPlaying.Visible && labNowPlaying.Text.Contains(Player.WorkingSongList[songList.SelectedIndex].Item2))
                    btnStop.PerformClick();
                else
                    btnPlaySong.PerformClick();
            }
        }

        private void Form1_KeyDown(object sender, KeyEventArgs e)
        {
            // If the songlist is not focused and user presses up or down arrow - it will focus on song list and select the first song
            if ((e.KeyCode == Keys.Up || e.KeyCode == Keys.Down) && songList.SelectedIndex == -1 && this.ActiveControl != selectedFolder)
            {
                songList.SelectedIndex = songList.Items.Count > 0 ? 0 : -1; // Makes sure that songList isn't empty
                songList.Select();
                songList.Focus();
            }
        }

        private void SongList_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Right)
            {
                songListContext.Show(Cursor.Position);
                if (songList.SelectedIndices.Count < 2)
                songList.SelectedIndex = -1;
                songList.SelectedIndex = songList.IndexFromPoint(e.X, e.Y);


                bool singleSongRelated = songList.SelectedIndex != -1;

                contextCopy.Enabled = singleSongRelated;
                contextDelete.Enabled = singleSongRelated;
                contextMove.Enabled = singleSongRelated;
            }
        }

        private void ContextCopy_Click(object sender, EventArgs e)
        {
            btnCloneSong.PerformClick();
        }

        private void ContextDelete_Click(object sender, EventArgs e)
        {
            btnDel.PerformClick();
        }

        private void ContextMove_Click(object sender, EventArgs e)
        {
            btnMoveSong.PerformClick();
        }

        private void ContextAll_Click(object sender, EventArgs e)
        {
            // Select all items on songlist
            for (int i = 0; i < songList.Items.Count; i++)
                songList.SelectedItem = songList.Items[i];
        }
    }
}
