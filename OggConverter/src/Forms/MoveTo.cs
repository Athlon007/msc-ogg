using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
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

            Message = $"Where do you want to move {files.Length} file{(files.Length > 1 ? "s" : "")}?";

            this.files = files;
            this.sourceFolder = sourceFolder;

            selectedFolder.SelectedIndex = 0;

            if (Directory.Exists($"{Settings.GamePath}\\CD1") && !Directory.Exists($"{Settings.GamePath}\\CD"))
            {
                selectedFolder.Items.RemoveAt(1);
            }
            else
            {
                selectedFolder.Items.RemoveAt(2);
                selectedFolder.Items.RemoveAt(3);
                selectedFolder.Items.RemoveAt(4);
            }
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
            {
                Player.MoveTo(file, sourceFolder, selectedFolder.Text);
            }

            if (Settings.AutoSort)
                Player.Sort(sourceFolder);

            this.Close();
        }
    }
}
