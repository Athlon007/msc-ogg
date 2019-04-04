using System;
using System.Windows.Forms;
using Microsoft.Win32;

namespace OggConverter
{
    public partial class QuickConvert : Form
    {
        string[] files;
        string path;

        string Message
        {
            get => strMessage.Text;
            set
            {
                strMessage.Text = value;
                strMessage.Left = (this.ClientSize.Width - strMessage.Size.Width) / 2;
            }
        }

        public QuickConvert(string[] files)
        {
            InitializeComponent();

            using (RegistryKey Key = Registry.CurrentUser.CreateSubKey(@"SOFTWARE\MSCOGG", true))
            {
                if (Key != null)
                    path = Key.GetValue("MSC Path").ToString();
                else
                {
                    MessageBox.Show("Couldn't find My Summer Car path. Set it up first!", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    Application.Exit();
                    return;
                }
            }

            this.files = files;
            Message = $"Where do you want to convert {files.Length} file(s)?";
        }

        private void BtnRadio_Click(object sender, EventArgs e)
        {
            Convert("Radio", 99);
        }

        private void BtnCD_Click(object sender, EventArgs e)
        {
            Convert("CD", 15);
        }

        async void Convert(string to, int limit)
        {
            btnRadio.Visible = false;
            btnCD.Visible = false;
            Message = "Converting now...";

            foreach (string file in files)
            {
                Message = "Converting\n" + file.Substring(file.LastIndexOf('\\') + 1);
                await Converter.ConvertFile(file, path, to, limit);
            }

            Message = "Done!";
            btnExit.Visible = true;
        }

        private void BtnExit_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }
    }
}
