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
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Diagnostics;
using System.IO;
using System.Drawing;

namespace OggConverter
{
    public partial class SettingsForm : Form
    {
        public SettingsForm()
        {
            InitializeComponent();

            Localise();
            labVer.Text = Localisation.Get("Your Version: {0}\n" +
                "Build: {1}", Utilities.GetVersion(true), Updates.version);

            tabControl.ItemSize = new Size((tabControl.Width / tabControl.TabCount) - 1, 0);

            // Adding already translates elementr to youtube-dl update frequency setting
            cbYoutubeDlUpdateFrequency.Items.Add(Localisation.Get("Upon every start"));
            cbYoutubeDlUpdateFrequency.Items.Add(Localisation.Get("Daily"));
            cbYoutubeDlUpdateFrequency.Items.Add(Localisation.Get("Weekly"));
            cbYoutubeDlUpdateFrequency.Items.Add(Localisation.Get("Monthly"));
            cbYoutubeDlUpdateFrequency.Items.Add(Localisation.Get("Never"));

            chkRemoveSource.Checked = Settings.RemoveMP3;
            chkAutoSort.Checked = Settings.AutoSort;
            chkNoMetafiles.Checked = Settings.DisableMetaFiles;
            chkAutoUpdates.Checked = !Settings.NoUpdates;
            radOfficial.Checked = !Settings.Preview;
            radPreview.Checked = Settings.Preview;
            chkCrashLog.Checked = Settings.Logs;
            chkHistory.Checked = Settings.History;
            cbYoutubeDlUpdateFrequency.SelectedIndex = Settings.YouTubeDlUpdateFrequency;
            chkShortcut.Checked = DesktopShortcut.Exists();
            chkNoSteam.Checked = Settings.NoSteam;
            chkShowFfmpegOutput.Checked = Settings.ShowFfmpegOutput;

            if (Directory.Exists("locales"))
            {
                DirectoryInfo di = new DirectoryInfo("locales");
                FileInfo[] files = di.GetFiles("*.po");
                foreach (FileInfo file in files)
                    comboLang.Items.Add(file.Name.Replace(file.Extension, ""));
            }

            comboLang.Text = Settings.Language;

            // Tooltips
            ToolTip toolTip = new ToolTip
            {
                AutoPopDelay = 5000,
                InitialDelay = 1000,
                ReshowDelay = 500,
                ShowAlways = true
            };

            toolTip.SetToolTip(chkRemoveSource, Localisation.Get("Removes the original files that the song is converted from."));
            toolTip.SetToolTip(chkAutoSort, Localisation.Get("After each file change, all songs will be sorted (ex. if you remove the track2, there won't be a gap between track1 and track3)."));
            toolTip.SetToolTip(chkNoMetafiles, Localisation.Get("Disables song name saving into songnames.xml. Only file name will be used."));
            toolTip.SetToolTip(chkAutoUpdates, Localisation.Get("On each start, the program will connect to the server and check if the new updates are available."));
            toolTip.SetToolTip(radOfficial, Localisation.Get("Official update channel is the most stable one and is recommended for most users."));
            toolTip.SetToolTip(radPreview, Localisation.Get("Preview update channel offers you an early access to new updates and improvements. Only for advanced users."));
            toolTip.SetToolTip(cbYoutubeDlUpdateFrequency, Localisation.Get("Set how often does youtube-dl start in order to check for updates."));
            toolTip.SetToolTip(chkCrashLog, Localisation.Get("The program after each crash causing error will try to save the crash info to log folder."));
            toolTip.SetToolTip(chkHistory, Localisation.Get("All operations on songs will be saved into history file - converting, moving, deleting and more."));
            toolTip.SetToolTip(chkShortcut, Localisation.Get("Create desktop shortcut to MSCMM."));
            toolTip.SetToolTip(chkNoSteam, Localisation.Get("Uppon pressing 'Launch Game', the program won't use Steam, and rather start the game through exe."));
        }

        private void ChkRemoveSource_Click(object sender, EventArgs e)
        {
            Settings.RemoveMP3 ^= true;
        }

        private void ChkAutoSort_Click(object sender, EventArgs e)
        {
            Settings.AutoSort ^= true;
        }

        private void ChkNoMetafiles_Click(object sender, EventArgs e)
        {
            if (!Settings.DisableMetaFiles)
            {
                DialogResult dl = MessageBox.Show(
                    Localisation.Get("Disabling metafiles will result in MSC Music Manager using file names, instead of saved song names.\n" +
                    "Are you sure you want to continue?"),
                    Localisation.Get("Question"),
                    MessageBoxButtons.YesNo,
                    MessageBoxIcon.Stop);

                if (dl != DialogResult.Yes)
                {
                    chkNoMetafiles.Checked ^= true;
                    return;
                }
            }

            Player.Stop();
            Settings.DisableMetaFiles ^= true;

            if (Settings.DisableMetaFiles)
            {
                DialogResult removeMeta = MessageBox.Show(
                    Localisation.Get("Would you like to remove ALL meta files?"),
                    Localisation.Get("Question"),
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
                DialogResult getMeta = MessageBox.Show(
                    Localisation.Get("Would you like the MSCMM to get song names directly from files now? " +
                    "(It may take some time, depending on how many songs you have)"),
                Localisation.Get("Question"),
                MessageBoxButtons.YesNo,
                MessageBoxIcon.Question);

                if (getMeta == DialogResult.Yes)
                {
                    MetaData.GetMetaFromAllSongs($"Radio");
                    MetaData.GetMetaFromAllSongs($"CD");
                }
            }
            Form1.instance.UpdateSongList();
        }

        private void ChkAutoUpdates_Click(object sender, EventArgs e)
        {
            Settings.NoUpdates ^= true;
        }

        private void RadOfficial_Click(object sender, EventArgs e)
        {
            Settings.Preview = false;
        }

        private void RadPreview_Click(object sender, EventArgs e)
        {
            Settings.Preview = true;
        }

        private void ChkCrashLog_Click(object sender, EventArgs e)
        {
            Settings.Logs ^= true;
        }

        private void ChkHistory_Click(object sender, EventArgs e)
        {
            Settings.History ^= true;
        }

        private void BtnOpenHistory_Click(object sender, EventArgs e)
        {
            if (File.Exists("history.txt"))
                Process.Start("history.txt");
        }

        private void BtnOpenLog_Click(object sender, EventArgs e)
        {
            if (Settings.LastCrashLogFile != "" && File.Exists($"LOG\\{Settings.LastCrashLogFile}"))
                Process.Start($"LOG\\{Settings.LastCrashLogFile}");
        }

        private void BtLogFolder_Click(object sender, EventArgs e)
        {
            if (Directory.Exists("LOG"))
                Process.Start("LOG");
        }

        private void CbYoutubeDlUpdateFrequency_SelectedIndexChanged(object sender, EventArgs e)
        {
        }

        private void BtnCheckUpdate_Click(object sender, EventArgs e)
        {
            // Force download and install update
            if (ModifierKeys.HasFlag(Keys.Shift))
            {
                Form1.instance.Log(Localisation.Get("\nForcing the update..."));
                Updates.DownloadUpdate(Settings.Preview);
                return;
            }

            Updates.StartUpdateCheck();
        }

        private async void BtnCheckYTDLUpdates_ClickAsync(object sender, EventArgs e)
        {
            await Task.Run(() => Updates.LookForYoutubeDlUpdate(true));
        }

        private void ChkShortcut_Click(object sender, EventArgs e)
        {
            if (DesktopShortcut.Exists())
                DesktopShortcut.Delete();
            else
                DesktopShortcut.Create();
        }

        private void ChkNoSteam_Click(object sender, EventArgs e)
        {
            Settings.NoSteam ^= true;
        }

        private void BtnDelHis_Click(object sender, EventArgs e)
        {
            DialogResult dl = MessageBox.Show(Localisation.Get("Are you sure you want to remove entire history?"), 
                Localisation.Get("Question"), 
                MessageBoxButtons.YesNo, 
                MessageBoxIcon.Question);

            if (dl == DialogResult.Yes && File.Exists("history.txt"))
                File.Delete("history.txt");
        }

        private void BtnDelLogs_Click(object sender, EventArgs e)
        {
            DialogResult dl = MessageBox.Show(Localisation.Get("Are you sure you want to delete all logs?"),
                Localisation.Get("Question"),
                MessageBoxButtons.YesNo,
                MessageBoxIcon.Question);

            if (dl == DialogResult.Yes && Directory.Exists("LOG"))
                Directory.Delete("LOG", true);
        }

        private void ComboLang_SelectionChangeCommitted(object sender, EventArgs e)
        {
            Settings.Language = comboLang.Text;

            DialogResult dl = MessageBox.Show(
                Localisation.Get("In order to apply the change, you need to restart MSCMM. Would you like to do that now?"),
                Localisation.Get("Question"),
                MessageBoxButtons.YesNo,
                MessageBoxIcon.Question);

            if (dl == DialogResult.Yes)
            {
                const string restartScript = "@echo off\n" +
                    "TASKKILL /IM \"MSC Music Manager.exe\"\n" +
                    "start \"\" \"MSC Music Manager.exe\"\n" +
                    "exit";
                File.WriteAllText("restart.bat", restartScript);

                Process process = new Process();
                process.StartInfo.WindowStyle = ProcessWindowStyle.Hidden;
                process.StartInfo.FileName = "restart.bat";
                process.Start();
                Application.Exit();
            }
        }

        void Localise()
        {
            chkShortcut.Text = Localisation.Get("Desktop shortcut");
            chkNoSteam.Text = Localisation.Get("Start the game without Steam");
            label2.Text = Localisation.Get("Language");
            chkRemoveSource.Text = Localisation.Get("Remove source files after conversion");
            chkAutoSort.Text = Localisation.Get("Sort files after conversion");
            chkNoMetafiles.Text = Localisation.Get("Don't save song names");
            chkAutoUpdates.Text = Localisation.Get("Automatically look for updates");
            label1.Text = Localisation.Get("Update Channel:");
            radOfficial.Text = Localisation.Get("Official");
            radPreview.Text = Localisation.Get("Preview");
            btnCheckUpdate.Text = Localisation.Get("Check for Update");
            label3.Text = Localisation.Get("Check for youtube-dl updates:");
            btnCheckYTDLUpdates.Text = Localisation.Get("Check for Update");
            chkCrashLog.Text = Localisation.Get("Create logs after crash");
            chkHistory.Text = Localisation.Get("Save usage into history");
            label4.Text = Localisation.Get("History");
            btnOpenHistory.Text = Localisation.Get("Open history");
            btnDelHis.Text = Localisation.Get("Delete history");
            label5.Text = Localisation.Get("Logs");
            btnOpenLog.Text = Localisation.Get("Open last log");
            btLogFolder.Text = Localisation.Get("Open Log Folder");
            btnDelLogs.Text = Localisation.Get("Delete all logs");
            labNotice.Text = Localisation.Get("Notice: not a single log or any info is sent from your computer.\nEverything that is being logged is saved on Your computer.");

            tabGeneral.Text = Localisation.Get("General");
            tabFiles.Text = Localisation.Get("Files");
            tabUpdates.Text = Localisation.Get("Updates");
            tabLogging.Text = Localisation.Get("Logging & Privacy");

            this.Text = Localisation.Get("Settings");

            chkShowFfmpegOutput.Text = Localisation.Get("Show ffmpeg output");
        }

        private void CbYoutubeDlUpdateFrequency_SelectionChangeCommitted(object sender, EventArgs e)
        {
            bool isLastSelected = cbYoutubeDlUpdateFrequency.SelectedIndex + 1 == cbYoutubeDlUpdateFrequency.Items.Count;
            Settings.YouTubeDlUpdateFrequency = isLastSelected ? -1 : cbYoutubeDlUpdateFrequency.SelectedIndex;
        }

        private void ChkShowFfmpegOutput_Click(object sender, EventArgs e)
        {
            Settings.ShowFfmpegOutput ^= true;
        }
    }
}
