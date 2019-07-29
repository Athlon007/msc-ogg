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

using System.Windows.Forms;
using System.IO;
using System.Reflection;
using System.Threading;
using System.Threading.Tasks;
using System;

namespace OggConverter
{
    public partial class QuickConvert : Form
    {
        readonly string[] files;

        private string Message
        {
            set
            {
                strMessage.Text = value;
                strMessage.Left = (this.ClientSize.Width - strMessage.Size.Width) / 2;
            }
        }

        public QuickConvert(string[] files)
        {
            InitializeComponent();

            Directory.SetCurrentDirectory(Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location));            

            if (!Settings.AreSettingsValid())
            {
                MessageBox.Show("Couldn't find My Summer Car path. Set it up first!", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                Application.Exit();
                this.Close();
                return;
            }

            if (!File.Exists("ffmpeg.exe"))
            {
                MessageBox.Show("FFmpeg needs to be downloaded first. Start the program to download it now.", 
                    "Error",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error);
                Application.Exit();
                this.Close();
                return;
            }

            // Checking for files validity
            foreach (string file in files)
            {
                if (!file.ContainsAny(Converter.extensions))
                {
                    MessageBox.Show("One or more files are not supported music file formats. Exiting now.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    Application.Exit();
                    this.Close();
                    return;
                }
            }

            this.files = files;
            Message = $"Where do you want to convert {files.Length} file{(files.Length > 1 ? "s" : "")}?";
            selectedFolder.SelectedIndex = 0;

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

            // Setting up the buttons
            btnApply.Click += (s, e) => Convert(selectedFolder.Text, selectedFolder.Text.StartsWith("CD") ? 15 : 99);
            btnExit.Click += (s, e) => Application.Exit();
        }

        async void Convert(string to, int limit)
        {
            btnApply.Visible = false;
            selectedFolder.Visible = false;
            Message = "Converting now...";

            try
            {
                foreach (string file in files)
                {
                    Message = "Converting\n" + file.Substring(file.LastIndexOf('\\') + 1);
                    await Converter.ConvertFile(file, to, limit);
                }
            }
            catch (Exception ex)
            {
                ErrorMessage err = new ErrorMessage(ex);
                err.ShowDialog();
            }

            Message = "Done!";
            btnExit.Visible = true;

            await Task.Run(() => Thread.Sleep(3000));
            Application.Exit();
        }
    }
}
