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

            labVer.Text = $"Your Version: {Utilities.GetVersion(true)}\n" +
                $"Build: {Updates.version}";

            tabControl.ItemSize = new Size((tabControl.Width / tabControl.TabCount) - 1, 0);

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
            comboLang.Text = Settings.Language;

            if (Directory.Exists("locales"))
            {
                DirectoryInfo di = new DirectoryInfo("locales");
                FileInfo[] files = di.GetFiles("*.po");
                foreach (FileInfo file in files)
                    comboLang.Items.Add(file.Name.Replace(file.Extension, ""));
            }

            // Tooltips
            ToolTip toolTip = new ToolTip
            {
                AutoPopDelay = 5000,
                InitialDelay = 1000,
                ReshowDelay = 500,
                ShowAlways = true
            };

            toolTip.SetToolTip(chkRemoveSource, "Removes the original files that the song is converted from.");
            toolTip.SetToolTip(chkAutoSort, "After each file change, the songs will be sorted (ex. if you remove the track2, there won't be a gap between track1 and track3).");
            toolTip.SetToolTip(chkNoMetafiles, "Disables song name saving into songnames.xml. Only file name will be used.");
            toolTip.SetToolTip(chkAutoUpdates, "On each start, the program will connect to the server and check if the new updates are available.");
            toolTip.SetToolTip(radOfficial, "Official update channel is the most stable one and is recommended for most users.");
            toolTip.SetToolTip(radPreview, "Preview update channel offers you an early access to new updates and improvements. Only for advanced users.");
            toolTip.SetToolTip(cbYoutubeDlUpdateFrequency, "Set how often does youtube-dl start in order to check for updates.");
            toolTip.SetToolTip(chkCrashLog, "The program after each crash causing error will try to save the crash info to log folder.");
            toolTip.SetToolTip(chkHistory, "All operations on songs will be saved into history file - converting, moving, deleting and more.");
            toolTip.SetToolTip(chkShortcut, "Create desktop shortcut to MSCMM.");
            toolTip.SetToolTip(chkNoSteam, "Uppon pressing \"Launch Game\", the program won't use Steam, and rather start the game through exe.");
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
                DialogResult dl = MessageBox.Show("Disabling metafiles will result in MSC Music Manager using file names, instead of saved song names.\n" +
                    "Are you sure you want to continue?",
                    "Question",
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
            Settings.YouTubeDlUpdateFrequency = cbYoutubeDlUpdateFrequency.SelectedIndex;
        }

        private void BtnCheckUpdate_Click(object sender, EventArgs e)
        {
            // Force download and install update
            if (ModifierKeys.HasFlag(Keys.Shift))
            {
                Form1.instance.Log("\nForcing the update...");
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
            DialogResult dl = MessageBox.Show("Are you sure you want to remove entire history?", 
                "Question", 
                MessageBoxButtons.YesNo, 
                MessageBoxIcon.Question);

            if (dl == DialogResult.Yes && File.Exists("history.txt"))
                File.Delete("history.txt");
        }

        private void BtnDelLogs_Click(object sender, EventArgs e)
        {
            DialogResult dl = MessageBox.Show("Are you sure you want to delete all logs?",
                "Question",
                MessageBoxButtons.YesNo,
                MessageBoxIcon.Question);

            if (dl == DialogResult.Yes && Directory.Exists("LOG"))
                Directory.Delete("LOG", true);
        }

        private void ComboLang_SelectedIndexChanged(object sender, EventArgs e)
        {
            Settings.Language = comboLang.Text;

            DialogResult dl = MessageBox.Show("In order to apply the change, you need to restart MSCMM. Would you like to do that now?", 
                "Question",
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
    }
}
