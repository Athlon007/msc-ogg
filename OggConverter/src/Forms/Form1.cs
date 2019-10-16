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

        /// <summary>
        /// Gets song limit in folder according to the folder name
        /// </summary>
        private int SongsLimit => CurrentFolder.StartsWith("CD") ? 15 : 99; 

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
                MessageBox.Show(Localisation.Get("Looks like you installed Music Manager into My Summer Car root path. " +
                    "While nothing should happen, I'm not not responsible for any damages done to you/your computer/your game/" +
                    "Satsuma/Teimo/or anything other at all!\n\n" +
                    "I'll also bother you with this message until you move Music Manager somewhere else every time you start the tool ;)"),
                    "Ruh-roh",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Information);
            }

            Localize();

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

            songListRight = tabs.Left - songList.Right;
            songListBottom = panel1.Bottom - songList.Bottom;

            // Removing temporary or unused files
            Utilities.Cleanup();

            // Checking file validity
            if (!Settings.AreSettingsValid())
            {
                firstLoad = true;
                // There was some kind of problem while starting.
                // Launching the first start sequence

                MessageBox.Show(Localisation.Get("Hello there! I've asked Teimo nicely where My Summer Car is installed, but he wouldn't tell me at all!.\n\n" +
                    "Please select where the game is installed :)"), "Terve", MessageBoxButtons.OK, MessageBoxIcon.Information);

                Log(Localisation.Get("\nSelect My Summer Car Directory\nEx. C:\\Steam\\steamapps\\common\\My Summer Car\\."));
                RestrictedMode(true);
                return;
            }

            Settings.SettingsChecked = true;

            // If provided directory or mysummercar.exe in that folder don't exist
            // That means that folder is invalid
            if ((!Directory.Exists(Settings.GamePath)) || (!File.Exists($"{Settings.GamePath}\\mysummercar.exe")))
            {
                MessageBox.Show(Localisation.Get("Couldn't find mysummercar.exe.\n\nPlease set the correct game path."), "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                Log(Localisation.Get("\nCouldn't find mysummercar.exe. Please set the correct game path."));
                firstLoad = true;
                RestrictedMode(true);
                return;
            }

            Log(Localisation.Get("Game Folder: {0}", Settings.GamePath));

            // Checks if ffmpeg or ffplay are missing
            // If so, they will be downloaded and the tool will be restarted.
            if (!File.Exists("ffmpeg.exe") || !File.Exists("ffplay.exe"))
            {
                RestrictedMode(true, true);
                MessageBox.Show(Localisation.Get("Hi there! In order to this tool to work, the ffmpeg and ffplay need to be downloaded.\n" +
                    "The tool will now download it and restart itself."), Localisation.Get("Info"), MessageBoxButtons.OK, MessageBoxIcon.Information);
                Updates.StartFFmpegDownload();
                return;
            }

            try
            {
                string[] folders = new string[] { "Radio", "CD", "CD1", "CD2", "CD3" };

                foreach (string folder in folders)
                {
                    if (Directory.Exists($"{Settings.GamePath}\\{folder}"))
                    {
                        // Checking if some trackTemp files exist. They may be caused by crash
                        if (File.Exists($"{Settings.GamePath}\\{folder}\\trackTemp.ogg"))
                        {
                            Log(Localisation.Get("Found temp song file in {0}, possibly due to the crash\nTrying to fix it...", folder));
                            _ = Converter.ConvertFile($"{Settings.GamePath}\\Radio\\trackTemp.ogg", folder, (folder == "Radio" ? 99 : 15));
                        }

                        // Checks if songnames.xml exists in folder
                        if (!File.Exists($"{Settings.GamePath}\\{folder}\\songnames.xml"))
                        {
                            MetaData.ConvertFromMscmm(folder);
                            Log(Localisation.Get("Converted {0} folder from .MSCMM to XML database.", folder));
                        }
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

                    // Is the update coming from Preview ring?
                    // If so, save that to the settings
                    Settings.ThisPreview = Settings.Preview;

                    // If the version is older than 2.1 (18151)
                    if (Settings.LatestVersion <= 18151)
                    {
                        DialogResult dl = MessageBox.Show(
                            Localisation.Get("Would you like MSC Music Manager to get song names from already existing songs?\n\n" +
                            "(It may take a while, depending on how many songs you have)."),
                            Localisation.Get("Question"),
                            MessageBoxButtons.YesNo,
                            MessageBoxIcon.Question);

                        if (dl == DialogResult.Yes)
                        {
                            RestrictedMode(true);
                            
                            foreach (string folder in folders)
                                MetaData.GetMetaFromAllSongs(folder);

                            UpdateSongList();
                            RestrictedMode(false);
                        }
                    }

                    if (Settings.LatestVersion == 0 && !DesktopShortcut.Exists())
                    {
                        DialogResult dl = MessageBox.Show(Localisation.Get("Do you want to create desktop shortcut?"), 
                            Localisation.Get("Question"), 
                            MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                        if (dl == DialogResult.Yes)
                            DesktopShortcut.Create();
                    }

                    Settings.LatestVersion = Updates.version;
                }

                if (Directory.Exists($"{Settings.GamePath}\\CD") && Directory.Exists($"{Settings.GamePath}\\CD1"))
                {
                    DialogResult dl = MessageBox.Show(
                        Localisation.Get("Looks like you've updated to the new My Summer Car version which now supports extra 2 CDs. " +
                        "All songs from the original CD have to be imported to the new CD1 folder in order to be read by My Summer Car. " +
                        "Press OK to do that now, or Cancel to exit.\n\n" +
                        "WARNING: This will override currently existing files in CD1!"), Localisation.Get("Question"),
                        MessageBoxButtons.OKCancel, MessageBoxIcon.Question);

                    if (dl == DialogResult.OK)
                    {
                        DirectoryInfo di = new DirectoryInfo($"{Settings.GamePath}\\CD");
                        FileInfo[] files = di.GetFiles();
                        foreach (var file in files)
                            File.Move($"{Settings.GamePath}\\CD\\{file.Name}", $"{Settings.GamePath}\\CD1\\{file.Name}");

                        Log(Localisation.Get("Succesfully imported CD songs to new CD1 folder!"));
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

            Log((Settings.Preview && !Settings.DemoMode) ? Localisation.Get("YOU ARE USING PREVIEW VERSION") : "");

            if (Settings.NoUpdates)
                Log(Localisation.Get("\nUpdates are disabled"));
            else
                Updates.StartUpdateCheck();

            btnMSCMMAbout.Text += Settings.DemoMode ? " (DEMO MODE)" : "";

            // Tooltips
            ToolTip toolTip = new ToolTip
            {
                AutoPopDelay = 5000,
                InitialDelay = 1000,
                ReshowDelay = 500,
                ShowAlways = true
            };

            toolTip.SetToolTip(btnSort, Localisation.Get("Sorts all songs so there are no gaps between songs " +
                "(ex. if there's track1 and track3, the track3 will be renamed to track2)."));
            toolTip.SetToolTip(btnUp, Localisation.Get("Move selected song one up."));
            toolTip.SetToolTip(btnDown, Localisation.Get("Move selected song one down."));
            toolTip.SetToolTip(btnMoveSong, Localisation.Get("Move selected songs to selected folder."));
            toolTip.SetToolTip(btnCloneSong, Localisation.Get("Clones selected song with it's displayed name."));
            toolTip.SetToolTip(btnDel, Localisation.Get("Deletes selected song."));
            toolTip.SetToolTip(btnShuffle, Localisation.Get("Randomizes songs order in chosen folder."));
            toolTip.SetToolTip(btnOpenGameDir, Localisation.Get("Opens My Summer Car folder in Explorer."));
            toolTip.SetToolTip(btnDirectory, Localisation.Get("Lets you change My Summer Car folder."));
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
                btnHelp.Enabled = state;
            }
        }

        /// <summary>
        /// Loads the locale translations to UI elements
        /// </summary>
        void Localize()
        {
            foreach (ToolStripMenuItem c in menu.Items)
                c.Text = Localisation.Get(c.Text);

            btnLastLog.Text = Localisation.Get("Open History");
            btnLogFolder.Text = Localisation.Get("Open Log Folder");
            btnWebsite.Text = Localisation.Get("Website");
            btnGitLab.Text = Localisation.Get("GitLab repository");
            btnSteam.Text = Localisation.Get("Steam Community discussion");
            btnAbout.Text = Localisation.Get("About");
            btnQuit.Text = Localisation.Get("Quit");
            btnDirectory.Text = Localisation.Get("Set Game Folder");
            btnOpenGameDir.Text = Localisation.Get("Open in Explorer");

            // Player
            Button[] btns = new Button[] { btnSort, btnMoveSong, btnCloneSong, btnDel, btnShuffle };
            foreach (Button btn in btns)
            {
                btn.Text = Localisation.Get(btn.Text);
                if (btn.Text.Length >= 5) 
                    btn.Font = new Font("Microsoft Sans Serif", 7);
                else if (btn.Text.Length > 8)
                    btn.Text = btn.Text.Substring(0, 6) + "...";
            }

            contextCopy.Text = Localisation.Get("Clone");
            contextDelete.Text = Localisation.Get("Delete");
            contextMove.Text = Localisation.Get("Move");
            contextAll.Text = Localisation.Get("Select All");

            tabDownload.Text = Localisation.Get("Download");
            tabMeta.Text = Localisation.Get("Edit");
            label5.Text = Localisation.Get("Search Term/Video Link:");
            btnDownload.Text = Localisation.Get("Download");
            btnCancelDownload.Text = Localisation.Get("Cancel");
            label6.Text = Localisation.Get("Note:\nThe autor of this tool doesn't take any responsibility for the way how that tool is used.");
            label2.Text = Localisation.Get("Select song from the list to the left and edit it's displayed name.");
            label1.Text = Localisation.Get("Name:");
            btnSetName.Text = Localisation.Get("Save");
            tabRecycle.Text = Localisation.Get("Recycle Bin");
            btnRecycleDelete.Text = Localisation.Get("Delete");
            btnRestore.Text = Localisation.Get("Restore");
            btnEmptyAll.Text = Localisation.Get("Delete all");
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
        /// Write text to youtube-dl log
        /// </summary>
        /// <param name="value"></param>
        public void YoutubeDlLog(string value)
        {
            value = value.Replace("\n", Environment.NewLine);
            value += Environment.NewLine;

            if (ytdlOutput.InvokeRequired)
            {
                ytdlOutput.Invoke(new Action(delegate ()
                {
                    ytdlOutput.Text += value;
                    ytdlOutput.SelectionStart = ytdlOutput.TextLength;
                    ytdlOutput.ScrollToCaret();
                }));
                return;
            }

            ytdlOutput.Text += value;
            ytdlOutput.SelectionStart = ytdlOutput.TextLength;
            ytdlOutput.ScrollToCaret();
        }

        /// <summary>
        /// Clear youtube-dl log
        /// </summary>
        public void ClearYtLog() => ytdlOutput.Text = "";

        /// <summary>
        /// Return text from youtube-dl log
        /// </summary>
        public string GetYtDlLog { get => ytdlOutput.Text; }

        /// <summary>
        /// Dumps the download speed from youtube-dl into label and progress bar.
        /// </summary>
        public void YtDownloadProgress(int percent, string speed)
        {
            // Progress bar
            if (proYt.InvokeRequired)
                proYt.Invoke(new Action(delegate ()
                {
                    proYt.Value = percent;
                }));
            else
                proYt.Value = percent;

            // Label
            if (labProgress.InvokeRequired)
                labProgress.Invoke(new Action(delegate ()
                {
                    labProgress.Text = percent.ToString() + "% " + speed;
                }));
            else
                labProgress.Text = percent.ToString() + "% " + speed;
        }


        /// <summary>
        /// Updates song list used for player
        /// </summary>
        public void UpdateSongList()
        {
            if (firstLoad || !Settings.SettingsChecked) return;

            string path = $"{Settings.GamePath}\\{(CurrentFolder)}";
            int howManySongs = 0;

            if (!File.Exists(MetaData.XmlFilePath()))
                MetaData.ConvertFromMscmm(CurrentFolder);

            List<string> newTrackList = new List<string>();
            Player.WorkingSongList.Clear();

            DirectoryInfo di = new DirectoryInfo(path);
            var files = di.GetFileSystemInfos("track*.ogg").OrderBy(f => int.Parse(f.Name.Replace("track", "").Split('.')[0]));
            foreach (FileInfo file in files)
            {
                if (file.Name == "songnames.xml") continue;
                string s = MetaData.GetName(file.Name.Split('.')[0]);
                newTrackList.Add(s);
                Player.WorkingSongList.Add(new Tuple<string, string>(file.Name.Split('.')[0], s));
                howManySongs++;
            }

            songList.Items.Clear();
            songList.Items.AddRange(newTrackList.ToArray());

            labCounter.Text = Localisation.Get("Songs: {0}/{1}", howManySongs, SongsLimit);
            labCounter.ForeColor = howManySongs > SongsLimit ? Color.Red : Color.Black;

            if (songList.Items.Count > lastSelected)
                songList.SelectedIndex = lastSelected;

            UpdateRecycleBinList();
        }

        void UpdateRecycleBinList()
        {
            if (!Directory.Exists($"{Settings.GamePath}\\Recycle Bin")) return;
            DirectoryInfo di = new DirectoryInfo($"{Settings.GamePath}\\Recycle Bin");
            FileInfo[] files = di.GetFiles("*.ogg");
            trashList.Items.Clear();

            foreach (var file in files)
                trashList.Items.Add(file.Name.Replace(".ogg", ""));

            labRecycle.Text = files.Length == 0 
                ? Localisation.Get("Recycle bin is empty")
                : Localisation.Get("There are {0} files in recycle bin", files.Length);

            tabRecycle.Text = files.Length > 0 ? Localisation.Get("Recycle Bin") + $" ({files.Length})" : Localisation.Get("Recycle Bin");
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
                Log(Localisation.Get("Program is busy."));
                return;
            }

            using (var folderDialog = new FolderBrowserDialog())
            {
                var dialog = folderDialog.ShowDialog();

                if (dialog == DialogResult.OK && Directory.GetFiles(folderDialog.SelectedPath, "mysummercar.exe").Length != 0)
                {
                    Settings.GamePath = folderDialog.SelectedPath;
                    Log(Localisation.Get("My Summer Car folder loaded successfully!"));

                    if (firstLoad)
                    {
                        Form1 f = new Form1();
                        Hide();
                        f.ShowDialog();
                        Close();
                    }
                }
                else if (dialog == DialogResult.Cancel)
                    Log(Localisation.Get("Operation canceled"));
                else
                    Log(Localisation.Get("Couldn't find mysummercar.exe. " +
                        "Make sure you've selected the game's ROOT folder (ex. C:\\Steam\\steamapps\\common\\My Summer Car) and NOT Radio or CD!"));
            }
        }

        private void BtnOpenGameDir_Click(object sender, EventArgs e)
        {
            if (Settings.GamePath.Length == 0)
            {
                Log(Localisation.Get("Set the game path first."));
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
                Log(Localisation.Get("Program is busy."));
                return;
            }

            Player.Stop();
        }

        private void OpenLastConversionToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (!File.Exists("history.txt"))
            {
                Log(Localisation.Get("History file doesn't exist."));
                return;
            }

            if (ModifierKeys.HasFlag(Keys.Shift))
            {
                DialogResult dl = MessageBox.Show(Localisation.Get("Would you like to remove history file?"), Localisation.Get("Question"), MessageBoxButtons.YesNo, MessageBoxIcon.Question);
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
            MessageBox.Show(Utilities.AboutNotice, Localisation.Get("Information"), MessageBoxButtons.OK, MessageBoxIcon.Information);
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
                DialogResult dl = MessageBox.Show(Localisation.Get("Would you like to remove all logs?"), Localisation.Get("Question"), 
                    MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                if (dl == DialogResult.Yes)
                    Directory.Delete("LOG", true);

                return;
            }

            if (!Settings.Logs)
            {
                Log(Localisation.Get("Logs are disabled"));
                return;
            }

            if (!Directory.Exists("Log"))
            {
                Log(Localisation.Get("Log folder doesn't exist"));
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
            Player.Remove(CurrentFolder, Utilities.GetSelectedItemsToArray(songList, Utilities.ArrayReturnValueSource.SongList));
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
            Player.ChangeOrder(songList, CurrentFolder, Player.Direction.Up);
        }

        private void BtnDown_Click(object sender, EventArgs e)
        {
            if (songList.SelectedIndex == -1) return;
            Player.ChangeOrder(songList, CurrentFolder, Player.Direction.Down);
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
                labelConvert.Text = Localisation.Get("Convert {0} file(s) to {1}?\n\n(Drop to Confirm)", filesLength, CurrentFolder);
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
            MoveTo moveTo = new MoveTo(Utilities.GetSelectedItemsToArray(songList, Utilities.ArrayReturnValueSource.SongList), CurrentFolder);
            moveTo.ShowDialog();
        }

        private void BtnCheckUpdate_Click(object sender, EventArgs e)
        {
            // Force download and install update
            if (ModifierKeys.HasFlag(Keys.Shift))
            {
                Log(Localisation.Get("\nForcing the update..."));
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
                MessageBox.Show(Localisation.Get("youtube-dl is now updating or looking for the update. " +
                    "You'll be notified on Log panel when it's done :)"),
                    Localisation.Get("Stop"),
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Stop);
                return;
            }

            if (Utilities.IsToolBusy())
            {
                Log(Localisation.Get("Program is busy."));
                return;
            }

            if (!File.Exists("youtube-dl.exe"))
            {
                DialogResult dl = MessageBox.Show(Localisation.Get("In order to download the song, the tool requires youtube-dl to be downloaded. " +
                    "Press 'Yes' to download it now."),
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

            if (url.IsValidUrl())
            {
                if (!url.Contains("youtube.com/watch?v="))
                {
                    MessageBox.Show(Localisation.Get("Url is not a YouTube link."),
                        Localisation.Get("Error"),
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Error);
                    RestrictedMode(false);
                    return;
                }

                url = url.Trim();
            }
            else
            {
                if (url == "")
                {
                    MessageBox.Show(Localisation.Get("Url is not valid and is empty."));
                    RestrictedMode(false);
                    return;
                }

                string searchTerm = txtboxVideo.Text.Replace('"', '\0');
                url = $"ytsearch:\"{searchTerm}\"";
                forcedName = searchTerm;
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
            MessageBox.Show(Localisation.Get("How to use:\n\n" +
                "- Drag and drop music files on the program's window or executable to quickly convert one or more songs\n" +
                "- Paste songs into Radio or CD folder in My Summer Car folder and click on the program's window - the program will detect new songs automatically\n" +
                "- Go to the 'Download' tab to get your songs directly from YouTube - either by using URL, or using search term\n\n" +
                "Use Shuffle to randomize songs order.\n" +
                "In Edit tab you can change song's displayed name."),
                Localisation.Get("Help"),
                MessageBoxButtons.OK,
                MessageBoxIcon.Information);

            MessageBox.Show(Localisation.Get("Useful keyboard shortcuts:\n\n" +
                "Player:\n" +
                "- Enter - when song is selected, it will play or stop the song playback\n" +
                "- Delete - when song is selected, will remove the selected song\n" +
                "- Ctrl + A - select all songs\n" +
                "- Ctrl + Up/Down arrows - move selected song up or down\n" +
                "- Alt + Up/Down - toggle between Radio and CD folders\n" +
                "- Ctrl + C - clone selected song\n" +
                "- Cltr + X - move selected song to a different folder"),
                Localisation.Get("Help"),
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
            switch (songList.SelectedIndex)
            {
                case 2:
                    txtSongName.Text = songList.SelectedItem.ToString();
                    break;
                case 3:
                    UpdateRecycleBinList();
                    break;
            }
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
            YtDownloadProgress(0, "0.00 KiB/s ETA 0:00");
        }

        private void MenuSettings_Click(object sender, EventArgs e)
        {
            SettingsForm form = new SettingsForm();
            form.Show();
        }

        private void SongList_KeyDown(object sender, KeyEventArgs e)
        {
            // Move song Down (Ctrl+Down Arrow)
            if (e.Control && e.KeyCode == Keys.Down)
            {
                Player.ChangeOrder(songList, CurrentFolder, Player.Direction.Down);
                songList.SelectedIndex--;
            }

            // Move song Up (Ctrl+Up Arrow)
            if (e.Control && e.KeyCode == Keys.Up)
            {
                Player.ChangeOrder(songList, CurrentFolder, Player.Direction.Up);
                songList.SelectedIndex++;
            }

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

            // Delete selected item(s) (Del)
            if (e.KeyCode == Keys.Delete)
                btnDel.PerformClick();

            // Play or stop playing the song (Enter)
            if (e.KeyCode == Keys.Enter)
            {
                string labNowPlayingText = labNowPlaying.Text.Contains("...") 
                    ? labNowPlaying.Text.Replace("...", "").Trim() 
                    : labNowPlaying.Text.Trim();

                if (labNowPlaying.Visible && Player.WorkingSongList[songList.SelectedIndex].Item2.Contains(labNowPlayingText))
                    btnStop.PerformClick();
                else
                    btnPlaySong.PerformClick();
            }
        }

        bool debugToggle = false;
        private void Form1_KeyDown(object sender, KeyEventArgs e)
        {
            // If the songlist is not focused and user presses up or down arrow - it will focus on song list and select the first song
            if ((e.KeyCode == Keys.Up || e.KeyCode == Keys.Down) && songList.SelectedIndex == -1 && this.ActiveControl != selectedFolder)
            {
                songList.SelectedIndex = songList.Items.Count > 0 ? 0 : -1; // Makes sure that songList isn't empty
                songList.Select();
                songList.Focus();
            }
#if DEBUG
            // DEBUG TOOLS
            // Toggle restricted mode
            if (e.Control && e.Alt && e.KeyCode == Keys.D1)
            {
                debugToggle ^= true;
                RestrictedMode(debugToggle);
            }

            // Toggle restricted mode (total)
            if (e.Control && e.Alt && e.KeyCode == Keys.D2)
            {
                debugToggle ^= true;
                RestrictedMode(debugToggle, true);
            }
#endif
            // Change folder Down (Alt+Down Arrow)
            if (e.Alt && e.KeyCode == Keys.Down)
                selectedFolder.SelectedIndex = selectedFolder.SelectedIndex < selectedFolder.Items.Count - 1
                    ? (selectedFolder.SelectedIndex + 1) : 0;

            // Change folder Up (Alt+Up Arrow)
            if (e.Alt && e.KeyCode == Keys.Up)
                selectedFolder.SelectedIndex = selectedFolder.SelectedIndex > 0
                    ? (selectedFolder.SelectedIndex - 1) : selectedFolder.Items.Count - 1;
        }

        private void SongList_MouseDown(object sender, MouseEventArgs e)
        {
            // Right click menu on SongList
            if (e.Button == MouseButtons.Right)
            {
                songListContext.Show(Cursor.Position); // Show song at cursor position
                if (songList.SelectedIndices.Count < 2)
                    songList.SelectedIndex = -1;
                songList.SelectedIndex = songList.IndexFromPoint(e.X, e.Y);

                bool anySongSelected = songList.SelectedIndex != -1;
                contextCopy.Enabled = anySongSelected;
                contextDelete.Enabled = anySongSelected;
                contextMove.Enabled = anySongSelected;
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

        int tabsDefaultX = 340;
        int panelDefaultY = 58;
        int songListRight = -1;
        int songListBottom = -1;

        void ResizeForm()
        {
            tabs.Size = new Size(this.Width - tabs.Location.X - 16, this.Height - tabs.Location.Y - 46);
            tabs.ItemSize = new Size((tabs.Width / tabs.TabCount) - 2, 0);
            double d = tabsDefaultX + (Math.Pow(this.Width, 0.95) - tabsDefaultX * 2) - 34;
            d = Math.Round(d);
            tabs.Location = new Point((int)d, tabs.Location.Y);

            panel1.Width = tabs.Location.X + 1;
            panel1.Height = this.Height - panelDefaultY - 46;

            labCounter.Location = new Point(labCounter.Location.X, panel1.Height - labCounter.Height - 5);
            songList.Width = tabs.Left - songListRight - songList.Left;
            songList.Height = panel1.Bottom - songListBottom - 20;

            btnUp.Left = songList.Right + 4;
            btnDown.Left = btnUp.Left;
            btnSort.Left = btnUp.Left;
            btnMoveSong.Left = btnUp.Left;
            btnCloneSong.Left = btnUp.Left;
            btnDel.Left = btnUp.Left;
            btnShuffle.Left = btnUp.Left;

            labNowPlaying.Top = songList.Bottom;
            btnPlaySong.Top = labNowPlaying.Bottom;
            btnStop.Top = labNowPlaying.Bottom;
        }

        private void Form1_Resize(object sender, EventArgs e)
        {
            ResizeForm();
        }

        private void Form1_ResizeEnd(object sender, EventArgs e)
        {
            ResizeForm();
        }

        private void BtnRestore_Click(object sender, EventArgs e)
        {
            if (trashList.SelectedIndex == -1) return;
            Player.Restore(CurrentFolder, Utilities.GetSelectedItemsToArray(trashList, Utilities.ArrayReturnValueSource.Name));
        }

        private void BtnRecycleDelete_Click(object sender, EventArgs e)
        {
            if (trashList.SelectedIndex == -1) return;
            Player.Delete(CurrentFolder + "\\Recycle Bin", Utilities.GetSelectedItemsToArray(trashList, Utilities.ArrayReturnValueSource.Name));
        }

        private void BtnEmptyAll_Click(object sender, EventArgs e)
        {
            Player.DeleteAll();
        }
    }
}
