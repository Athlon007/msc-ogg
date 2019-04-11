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

            btnRadio.Click += (s, e) => Convert("Radio", 99);
            btnCD.Click += (s, e) => Convert("CD", 15);
            btnExit.Click += (s, e) => Application.Exit();
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
    }
}
