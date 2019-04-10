using System;
using System.Windows.Forms;
using Microsoft.Win32;
using System.IO;
using System.Reflection;

namespace OggConverter
{
    public partial class QuickConvert : Form
    {
        readonly string[] files;
        readonly string path;

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

            using (RegistryKey Key = Registry.CurrentUser.CreateSubKey(@"SOFTWARE\MSCOGG", true))
            {
                if (Key.GetValue("MSC Path") == null)
                {
                    MessageBox.Show("Couldn't find My Summer Car path. Set it up first!", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    Application.Exit();
                    this.Close();
                    return;
                }

                path = Key.GetValue("MSC Path").ToString();
            }

            this.files = files;
            Message = $"Where do you want to convert {files.Length} file{(files.Length > 1 ? "s" : "")}?";
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
