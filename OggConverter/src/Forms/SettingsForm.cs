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

namespace OggConverter
{
    public partial class SettingsForm : Form
    {
        public SettingsForm()
        {
            InitializeComponent();

            labVer.Text = $"Your Version: {Utilities.GetVersion(true)}\n" +
                $"Build: {Updates.version}";

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
            if (Settings.LastCrashLogFile != "")
                Process.Start($"LOG//{Settings.LastCrashLogFile}.txt");
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
    }
}
