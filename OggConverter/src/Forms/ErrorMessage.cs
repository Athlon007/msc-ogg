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
using System.Drawing;
using System.Windows.Forms;
using System.Diagnostics;
using System.IO;

namespace OggConverter
{
    public partial class ErrorMessage : Form
    {
        string FileName { get; set; }

        int defaultY = 208;
        int extendedY = 457;
        bool isExtended;

        public ErrorMessage(Exception ex)
        {
            InitializeComponent();

            string fileName = $"{DateTime.Now.Date.ToShortDateString()} {DateTime.Now.Hour.ToString()}.{DateTime.Now.Minute.ToString()}.{DateTime.Now.Second.ToString()}";
            FileName = fileName;

            label2.Text = $"An error has occured and the info has been saved to {fileName}\n" +
                $"inside of LOG folder.\n" +
                $"If it happens again, please send the log to the MSCMM developer.";

            Logs.CrashLog(ex.ToString(), true);
            btnMoreDetail.Text = (char.ConvertFromUtf32(0x2193) + " Show More Info");
        }

        private void BtnClose_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void BtnLog_Click(object sender, EventArgs e)
        {
            Process.Start($"LOG\\{FileName}.txt");
        }

        private void BtnMoreDetail_Click(object sender, EventArgs e)
        {
            isExtended ^= true;
            this.Size = new Size(this.Size.Width, isExtended ? extendedY : defaultY);
            btnMoreDetail.Text = isExtended ? (char.ConvertFromUtf32(0x2191) + " Hide More Info") : (char.ConvertFromUtf32(0x2193)+ " Show More Info");
            string file = File.ReadAllText($"LOG\\{FileName}.txt");
            file = file.Replace("\n", Environment.NewLine);
            logOutput.Text = file;
        }

        private void BtnExit_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }
    }
}
