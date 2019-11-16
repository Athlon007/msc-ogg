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
using System.Windows.Forms;
using System.IO;

namespace OggConverter
{
    public partial class MoveTo : Form
    {
        string[] files;
        string sourceFolder;

        public MoveTo(string[] files, string sourceFolder)
        {
            InitializeComponent();

            Localise();
            Message = Localisation.Get("Where do you want to move {0} file(s)?", files.Length);

            this.files = files;
            this.sourceFolder = sourceFolder;

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
        }

        private void MoveTo_Load(object sender, EventArgs e)
        {
            this.SetDesktopLocation(Cursor.Position.X, Cursor.Position.Y);
        }

        private string Message
        {
            set
            {
                strMessage.Text = value;
                strMessage.Left = (this.ClientSize.Width - strMessage.Size.Width) / 2;
            }
        }

        private void SelectedFolder_SelectedIndexChanged(object sender, EventArgs e)
        {
            btnApply.Enabled = selectedFolder.Text != sourceFolder;
        }

        private void BtnApply_Click(object sender, EventArgs e)
        {
            foreach (string file in files)
                Player.MoveTo(file, sourceFolder, selectedFolder.Text);

            if (Settings.AutoSort)
                Player.Sort(sourceFolder);

            this.Close();
        }

        void Localise ()
        {
            btnApply.Text = Localisation.Get("Apply");
            this.Text = Localisation.Get("Move");
        }

        private void MoveTo_KeyDown(object sender, KeyEventArgs e)
        {
            // Refresh the localization (translator mode)
            if (Settings.TranslatorMode)
                if (e.KeyCode == Keys.F5)
                {
                    Localisation.LoadLocaleFile();
                    Localise();
                }
        }
    }
}
