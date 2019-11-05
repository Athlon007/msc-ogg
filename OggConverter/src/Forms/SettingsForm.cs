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
using System.Net;

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
            chkIgnoreLimits.Checked = Settings.IgnoreLimitations;

            radQualityBest.Checked = Settings.YoutubeDlDownloadQuality == 0;
            radQualityAverage.Checked = Settings.YoutubeDlDownloadQuality == 1;
            radQualityCompressed.Checked = Settings.YoutubeDlDownloadQuality == 2;

            txtAudacity.Text = Settings.AudacityPath;
            chkRecommendedFrequency.Checked = Settings.UseRecommendedFrequency;
            chkMono.Checked = Settings.ConvertToMono;

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

            //toolTip.SetToolTip(chkRemoveSource, Localisation.Get("Removes the original files that the song is converted from."));
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
            toolTip.SetToolTip(chkIgnoreLimits, Localisation.Get("Normally the program will show a warning during conversion, " +
                "when there are more files than the folder allows (99 for Radio, 15 for CDs). If this setting is enabled, " +
                "it will be ignored."));

            txtChangelog.Text = Properties.Resources.changelog;
            btnAudacity.Text = char.ConvertFromUtf32(0x1F4C1);

            txtAudacity.ContextMenu = new ContextMenu();
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
            // Ignore setting change, if the value selected is the same
            if (comboLang.Text == Settings.Language) return;

            Settings.Language = comboLang.Text;
            Localisation.LoadLocaleFile();
            Form1.instance.Localize();
            this.Localise();
        }

        void Localise()
        {
            chkShortcut.Text = Localisation.Get("Desktop shortcut");
            chkNoSteam.Text = Localisation.Get("Start the game without Steam");
            label2.Text = Localisation.Get("Language");
            chkAutoSort.Text = Localisation.Get("Automatically rearrange file order");
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
            chkIgnoreLimits.Text = Localisation.Get("Ignore limitation of songs in folder");
            labAudacity.Text = Localisation.Get("Audacity Executable:");
            label6.Text = Localisation.Get("Music Download Quality:");
            radQualityBest.Text = Localisation.Get("Best");
            radQualityAverage.Text = Localisation.Get("Average");
            radQualityCompressed.Text = Localisation.Get("Compressed");
            btnChangelogHistory.Text = Localisation.Get("View Changelog History");
            chkRecommendedFrequency.Text = Localisation.Get("Set the music frequency to recomended 22050 Hz frequency");
            chkMono.Text = Localisation.Get("Convert song to mono channel");
            label7.Text = Localisation.Get("Conversion:");
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

        private void chkIgnoreLimits_Click(object sender, EventArgs e)
        {
            Settings.IgnoreLimitations ^= true;
        }

        private async void btnChangelogHistory_Click(object sender, EventArgs e)
        {
            //Process.Start("https://gitlab.com/aathlon/msc-ogg/blob/master/CHANGELOG.md");
            await Task.Run(() => GetChangelog());
        }

        async void GetChangelog()
        {
            using (WebClient client = new WebClient())
            {
                await Task.Run(() => client.DownloadStringAsync(new Uri("https://gitlab.com/aathlon/msc-ogg/raw/master/CHANGELOG.md")));
                client.DownloadStringCompleted += (s, e) =>
                {
                    if (txtChangelog.InvokeRequired)
                    {
                        txtChangelog.Invoke(new Action(delegate ()
                        {
                            txtChangelog.Text = e.Result;
                        }));
                    }
                    else
                    {
                        txtChangelog.Text = e.Result;
                    }
                };
            }
        }

        private void radQualityBest_Click(object sender, EventArgs e)
        {
            Settings.YoutubeDlDownloadQuality = 0;
        }

        private void radQualityAverage_Click(object sender, EventArgs e)
        {
            Settings.YoutubeDlDownloadQuality = 1;
        }

        private void radQualityCompressed_Click(object sender, EventArgs e)
        {
            Settings.YoutubeDlDownloadQuality = 2;
        }

        private void btnAudacity_Click(object sender, EventArgs e)
        {
            using (var openFileDialog = new OpenFileDialog())
            {
                openFileDialog.Title = Localisation.Get("Select the audacity folder.");
                openFileDialog.FileName= "audacity.exe";
                openFileDialog.Filter = "Executable (*.exe)|*.exe";
                openFileDialog.InitialDirectory = Directory.Exists("C:\\Program Files (x86)\\Audacity") 
                    ? "C:\\Program Files (x86)\\Audacity" 
                    : "C:\\Program Files (x86)";
                var dialog = openFileDialog.ShowDialog();
                if (dialog == DialogResult.OK && openFileDialog.FileName.EndsWith("audacity.exe"))
                {
                    Settings.AudacityPath = openFileDialog.FileName;
                    txtAudacity.Text = Settings.AudacityPath;
                }
            }
        }

        private void chkRecommendedFrequency_Click(object sender, EventArgs e)
        {
            Settings.UseRecommendedFrequency ^= true;
        }

        private void chkMono_Click(object sender, EventArgs e)
        {
            Settings.ConvertToMono ^= true;
        }
    }
}
