using System;
using System.IO;
using System.Windows.Forms;
using Microsoft.Win32;
using System.Diagnostics;
using System.Media;
using System.Threading.Tasks;

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

            Log += "MSC OGG Converter " + Updates.realVersion + " BETA";

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

                        //Remove MP3
                        btnRemMP3.Checked = Settings.RemoveMP3;
                        //No Steam
                        btnNoSteam.Checked = Settings.NoSteam;
                        //Action after conversion
                        btnAfterLaunchGame.Checked = Settings.LaunchAfterConversion;
                        btnAfterClose.Checked = Settings.CloseAfterConversion;
                        btnAfterNone.Checked = !Settings.CloseAfterConversion && !Settings.LaunchAfterConversion;
                        btnUpdates.Checked = !Settings.NoUpdates;

                        if (!Settings.NoUpdates)
                            //Looking for updates
                            Log += Updates.IsThereUpdate() ? "\n\nThere's an update ready to download!" : "\n\nTool is up-to-date";
                        else
                            Log += "\n\nUpdates are disabled";

                        UpdatePlayerList();
                    }
                }

                Log += "\n\nYou can check the changeLog on Steam community discussion, or project's repository.";

                if (!Directory.Exists(txtboxPath.Text + @"\CD"))
                {
                    skipCD = true;
                    Log += "\nCD folder is missing (you're propably using 24.10.2017 version of the game or older), so it will be skipped.";
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
                MessageBox.Show("Hey there! Looks like you're new here. Select the game directory first :)\n\n" +
                    "If you see that message second, or more times, please contact the developer.", "Howdy", MessageBoxButtons.OK, MessageBoxIcon.Information);
                Log += "\n\nSelect the game directory first.";

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

            try
            {
                Converter.ConversionLog += "THIS FILE WILL BE WIPED AFTER THE NEXT CONVERSION:\n\n";
                await Converter.ConvertFiles(txtboxPath.Text, "Radio", 99);
                Converter.ConversionLog += "\n\n";

                if (!skipCD)
                    await Converter.ConvertFiles(txtboxPath.Text, "CD", 15);

                Converter.ConversionLog += "\n\n" + DateTime.Now.ToLocalTime();
                File.WriteAllText(@"LastConversion.txt", Converter.ConversionLog);
                Process.Start(@"LastConversion.txt");
                Log += $"\nConverted {Converter.TotalConversions} files in total.";
                Log += "\nDone ";
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

            UpdatePlayerList();
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
                    Log += "\nLoaded My Summer Car's directory successfully";

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

            Audio.Stop();
        }


        private void openLastConversionToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (File.Exists("LastConversion.txt"))
                Process.Start("LastConversion.txt");
            else
                Log += "\n\nNo last conversion Log exists";
        }

        private void launchTheGameToolStripMenuItem_Click(object sender, EventArgs e)
        {
            LaunchGame();
        }

        void LaunchGame()
        {
            if (Settings.NoSteam)
                Process.Start(txtboxPath.Text + @"\mysummercar.exe");
            else
                Process.Start("steam://rungameid/516750");
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

        private void btnPlay(object sender, EventArgs e)
        {
            Audio.Play($"{txtboxPath.Text}\\{(playerCD.Checked ? "CD" : "Radio")}\\{songList.SelectedItem.ToString()}");
        }

        private void BtnStop_Click(object sender, EventArgs e)
        {
            Audio.Stop();
        }

        private void BtnLogFolder_Click(object sender, EventArgs e)
        {
            Directory.CreateDirectory(@"Log");
            Process.Start(@"Log");
        }

        void UpdatePlayerList()
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
            DialogResult dl = MessageBox.Show($"Are you sure you want to delete {songList.SelectedItem.ToString()}?", "Question", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (dl == DialogResult.Yes)
            {                
                File.Delete($"{txtboxPath.Text}\\{(playerCD.Checked ? "CD" : "Radio")}\\{songList.SelectedItem.ToString()}");
                UpdatePlayerList();
            }
        }

        private void PlayerCD_Click(object sender, EventArgs e)
        {
            UpdatePlayerList();
        }

        private void PlayerRadio_Click(object sender, EventArgs e)
        {
            Audio.Stop();
            UpdatePlayerList();
        }

        private void BtnSort_Click(object sender, EventArgs e)
        {
            string path = $"{txtboxPath.Text}\\{(playerCD.Checked ? "CD" : "Radio")}";

            int skipped = 0;

            for (int i = 1; i < 99; i++)
            {
                if (!File.Exists($"{path}\\track{i}.ogg"))
                    skipped++;
                else
                {
                    File.Move($"{path}\\track{i}.ogg", $"{path}\\track{i - skipped}.ogg");
                    if (skipped != 0)
                        i -= skipped;
                    skipped = 0;
                }
            }

            UpdatePlayerList();
        }

        private void Form1_Activated(object sender, EventArgs e)
        {
            UpdatePlayerList();
        }

        private void BtnUp_Click(object sender, EventArgs e)
        {
            string path = $"{txtboxPath.Text}\\{(playerCD.Checked ? "CD" : "Radio")}";
            int selectedIndex = songList.SelectedIndex;
            if (selectedIndex == 0)  return;

            string oldName = songList.SelectedItem.ToString();
            string newName = "track" + selectedIndex + ".ogg";

            File.Move($"{path}\\{newName}", $"{path}\\trackTemp.ogg");
            File.Move($"{path}\\{oldName}", $"{path}\\{newName}");
            File.Move($"{path}\\trackTemp.ogg", $"{path}\\{oldName}");

            UpdatePlayerList();
            songList.SelectedIndex = selectedIndex - 1;
        }

        private void BtnDown_Click(object sender, EventArgs e)
        {
            string path = $"{txtboxPath.Text}\\{(playerCD.Checked ? "CD" : "Radio")}";
            var selectedIndex = songList.SelectedIndex;
            if (selectedIndex == songList.Items.Count - 1) return;

            string oldName = songList.SelectedItem.ToString();
            string newName = "track" + (selectedIndex + 2) + ".ogg";

            File.Move($"{path}\\{newName}", $"{path}\\trackTemp.ogg");
            File.Move($"{path}\\{oldName}", $"{path}\\{newName}");
            File.Move($"{path}\\trackTemp.ogg", $"{path}\\{oldName}");

            UpdatePlayerList();
            songList.SelectedIndex = selectedIndex + 1;
        }

        private async void Form1_DragDrop(object sender, DragEventArgs e)
        {
            int limit = playerCD.Checked ? 15 : 99;
            string[] files = (string[])e.Data.GetData(DataFormats.FileDrop);
            foreach (string file in files)
                await Converter.ConvertDragDrop(file, txtboxPath.Text, playerCD.Checked ? "CD" : "Radio", limit);

            UpdatePlayerList();
        }

        private void Form1_DragEnter(object sender, DragEventArgs e)
        {
            if (e.Data.GetDataPresent(DataFormats.FileDrop)) e.Effect = DragDropEffects.Copy;
        }
    }
}
