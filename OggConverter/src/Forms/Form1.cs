using System;
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

        bool skipCD; //Tells the program to skip CD folder, if the game is older than 24.10.2017 update
        bool ignoreQuitting; //Prevents the program from closing if conversion is in progress
        bool firstLoad = false; //Detects if the program has been opened for the first time

        public string Log
        {
            get => logOutput.Text;
            set
            {
                value = value.Replace("\n", Environment.NewLine);
                if (logOutput.InvokeRequired)
                {
                    logOutput.Invoke(new Action(() => logOutput.Text = value));
                    return;
                }
                logOutput.Text = value;
            }
        }      

        public Form1()
        {
            InitializeComponent();

            instance = this;
            playerRadio.Checked = true;
            Log += $"MSC OGG Converter {Updates.realVersion} BETA";

            try
            {
                using (RegistryKey Key = Registry.CurrentUser.CreateSubKey(@"SOFTWARE\MSCOGG", true))
                {
                    if (Key != null)
                    {
                        //Path in textbox
                        txtboxPath.Text = Key.GetValue("MSC Path").ToString();
                        txtboxPath.SelectionStart = 0;
                        txtboxPath.ScrollToCaret();
                        this.ActiveControl = label2;

                        // SETTINGS CHECKS
                        btnRemMP3.Checked = Settings.RemoveMP3;
                        btnNoSteam.Checked = Settings.NoSteam;
                        // Actions after conversion
                        btnAfterLaunchGame.Checked = Settings.LaunchAfterConversion;
                        btnAfterClose.Checked = Settings.CloseAfterConversion;
                        btnAfterNone.Checked = !Settings.CloseAfterConversion && !Settings.LaunchAfterConversion;
                        btnUpdates.Checked = !Settings.NoUpdates;
                        btnLogs.Checked = Settings.Logs;

                        if (Settings.NoUpdates)
                            Log += "\n\nUpdates are disabled";
                        else
                            Log += Updates.IsThereUpdate() ? "\n\nThere's an update ready to download!" : "\n\nTool is up-to-date";

                        UpdateSongList();
                    }
                }

                Log += "\n\nYou can check the changeLog on Steam community discussion, or project's repository.";

                if (!Directory.Exists($"{txtboxPath.Text}\\CD"))
                {
                    skipCD = true;
                    Log += "\nCD folder is missing (you're propably using 24.10.2017 version of the game or older), so it will be skipped.";
                }
            }
            catch (Exception ex)
            {
                // There was some kind of problem while starting.
                // Launching the first start sequention

                MessageBox.Show(ex.ToString());
                MessageBox.Show("Hey there! Looks like you're new here. Select the game directory first :)\n\n" +
                    "If you see that message second, or more times, please contact the developer.", "Howdy", MessageBoxButtons.OK, MessageBoxIcon.Information);
                Log += "\n\nSelect My Summer Car Directory\nEg. C:\\Steam\\steamapps\\common\\My Summer Car\\.";

                // Disables some items, to prevent bugs
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
            }
        }    

        private async void btnConvert_Click(object sender, EventArgs e)
        {
            if (ignoreQuitting)
            {
                MessageBox.Show("Conversion is in progress.", "Prohibited", MessageBoxButtons.OK, MessageBoxIcon.Hand);
                return;
            }

            if (txtboxPath.Text.Length == 0)
            {
                MessageBox.Show("Select game path first.", "Prohibited", MessageBoxButtons.OK, MessageBoxIcon.Hand);
                return;
            }

            ignoreQuitting = true;

            Converter.ConversionLog = "";
            Converter.TotalConversions = 0;
            Converter.skipped = 0;

            try
            {
                Log += "\n\n--------------------------------------------------------------------------------------------------------";
                Converter.ConversionLog += "THIS FILE WILL BE WIPED AFTER THE NEXT CONVERSION:\n\n";
                await Converter.ConvertFolder(txtboxPath.Text, "Radio", 99);
                Converter.ConversionLog += "\n\n";

                if (!skipCD)
                    await Converter.ConvertFolder(txtboxPath.Text, "CD", 15);

                if (Converter.skipped != 2)
                {
                    Converter.ConversionLog += "\n\n" + DateTime.Now.ToLocalTime();
                    File.WriteAllText(@"LastConversion.txt", Converter.ConversionLog);
                    Process.Start(@"LastConversion.txt");
                    Log += $"\nConverted {Converter.TotalConversions} files in total";
                    Log += "\nDone";
                }
                else
                {
                    Log += "\nConverting log will not be saved, because both Radio and CD were skipped";
                }
                SystemSounds.Exclamation.Play();

                ignoreQuitting = false;

                //Actions after conversion
                if (Settings.LaunchAfterConversion)
                    LaunchGame();

                if (Settings.CloseAfterConversion)
                    Application.Exit();
            }
            catch (Exception ex)
            {
                new CrashLog(ex.ToString());
                ignoreQuitting = false;
            }

            UpdateSongList();
        }       

        private void Log_TextChanged(object sender, EventArgs e)
        {
            logOutput.SelectionStart = Log.Length;
            logOutput.ScrollToCaret();
        }

        private void btnDirectory_Click(object sender, EventArgs e)
        {
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

        private void btnOpenGameDir_Click(object sender, EventArgs e)
        {
            if (txtboxPath.Text.Length == 0)
            {
                MessageBox.Show("Select game path first.", "Prohibited", MessageBoxButtons.OK, MessageBoxIcon.Hand);
                return;
            }

            Process.Start(txtboxPath.Text);
        }

        private void gitToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Process.Start("https://gitlab.com/aathlon/msc-ogg");
        }

        private void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (ignoreQuitting)
                e.Cancel = true;

            Music.Stop();
        }


        private void openLastConversionToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (!File.Exists("LastConversion.txt"))
            {
                Log += "\n\nNo last conversion log exists";
                return;
            }

            Process.Start("LastConversion.txt");
        }

        private void launchTheGameToolStripMenuItem_Click(object sender, EventArgs e)
        {
            LaunchGame();
        }

        void LaunchGame()
        {
            Process.Start(Settings.NoSteam ? $"{txtboxPath.Text}\\mysummercar.exe" : "steam://rungameid/516750");
        }

        private void removeOldMP3FilesToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Settings.RemoveMP3 ^= true;
        }      

        private void quitToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void steamCommunityDiscussionToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Process.Start("http://steamcommunity.com/app/516750/discussions/2/1489992713697876617/");
        }

        private void noneToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Settings.CloseAfterConversion = btnAfterClose.Checked = false;
            Settings.LaunchAfterConversion = btnAfterLaunchGame.Checked =  false;
        }

        private void closeTheProgramToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Settings.CloseAfterConversion ^= true;
            btnAfterNone.Checked = false;
        }

        private void launchTheGameToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            Settings.LaunchAfterConversion ^= true;
            btnAfterNone.Checked = false;
        }

        private void launchGameWithoutSteamToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Settings.NoSteam ^= true;
        }

        private void CheckBoxUpdates_Click(object sender, EventArgs e)
        {
            Settings.NoUpdates ^= true;
        }

        private void BtnFFmpegLicense_Click(object sender, EventArgs e)
        {
            Process.Start("http://www.gnu.org/licenses/old-licenses/lgpl-2.1.html");
        }

        private void MSCOGGToolStripMenuItem_Click(object sender, EventArgs e)
        {
            MessageBox.Show($"MSC OGG Converter {Updates.realVersion}\nby Athlon\n\nAll info about third party libraries you can find on official GitLab repo.",
                "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private async void btnPlay(object sender, EventArgs e)
        {
            if (songList.SelectedIndex == -1) return;
            Music.Play($"{txtboxPath.Text}\\{(playerCD.Checked ? "CD" : "Radio")}\\{songList.SelectedItem.ToString()}");
        }

        private void BtnStop_Click(object sender, EventArgs e)
        {
            Music.Stop();
        }

        private void BtnLogFolder_Click(object sender, EventArgs e)
        {
            if (!Settings.Logs)
            {
                Log += "Logs are disabled";
                return;
            }
            Directory.CreateDirectory(@"Log");
            Process.Start(@"Log");
        }

        public void UpdateSongList()
        {
            if (firstLoad) return;

            songList.Items.Clear();
            string path = $"{txtboxPath.Text}\\{(playerCD.Checked ? "CD" : "Radio")}";

            for (int i = 1; i < 99; i++)
                if (File.Exists($"{path}\\track{i}.ogg"))
                    songList.Items.Add($"track{i}.ogg");
        }

        private void BtnDel_Click(object sender, EventArgs e)
        {
            if (songList.SelectedIndex == -1) return;
            File.Delete($"{txtboxPath.Text}\\{(playerCD.Checked ? "CD" : "Radio")}\\{songList.SelectedItem.ToString()}");
            UpdateSongList();
            //DialogResult dl = MessageBox.Show($"Are you sure you want to delete {songList.SelectedItem.ToString()}?", "Question", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            //if (dl == DialogResult.Yes)
            //{                
            //File.Delete($"{txtboxPath.Text}\\{(playerCD.Checked ? "CD" : "Radio")}\\{songList.SelectedItem.ToString()}");
            //UpdateSongList();
            //}
        }

        private void PlayerCD_Click(object sender, EventArgs e)
        {
            Music.Stop();
            UpdateSongList();
            btnMoveSong.Text = "<";
        }

        private void PlayerRadio_Click(object sender, EventArgs e)
        {
            Music.Stop();
            UpdateSongList();
            btnMoveSong.Text = ">";
        }

        private void BtnSort_Click(object sender, EventArgs e)
        {
            string path = $"{txtboxPath.Text}\\{(playerCD.Checked ? "CD" : "Radio")}";
            Music.Sort(path);
        }

        private void Form1_Activated(object sender, EventArgs e)
        {
            UpdateSongList();
        }

        private void BtnUp_Click(object sender, EventArgs e)
        {
            if (songList.SelectedIndex == -1) return;
            string path = $"{txtboxPath.Text}\\{(playerCD.Checked ? "CD" : "Radio")}";
            Music.ChangeOrder(songList, path, true);
        }

        private void BtnDown_Click(object sender, EventArgs e)
        {
            if (songList.SelectedIndex == -1) return;
            string path = $"{txtboxPath.Text}\\{(playerCD.Checked ? "CD" : "Radio")}";
            Music.ChangeOrder(songList, path, false);
        }

        private async void Form1_DragDrop(object sender, DragEventArgs e)
        {
            int limit = playerCD.Checked ? 15 : 99;
            string[] files = (string[])e.Data.GetData(DataFormats.FileDrop);
            string dropTo = playerCD.Checked ? "CD" : "Radio";
            foreach (string file in files)
                await Converter.ConvertFile(file, txtboxPath.Text, dropTo, limit);

            UpdateSongList();
        }

        private void Form1_DragEnter(object sender, DragEventArgs e)
        {
            if (e.Data.GetDataPresent(DataFormats.FileDrop)) e.Effect = DragDropEffects.Copy;
        }

        private void BtnMoveSong_Click(object sender, EventArgs e)
        {
            Music.MoveTo(txtboxPath.Text, songList.SelectedItem.ToString(), playerCD.Checked);
        }

        private void BtnLogs_Click(object sender, EventArgs e)
        {
            Settings.Logs ^= true;
        }
    }
}
