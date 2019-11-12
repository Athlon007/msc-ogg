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

        int defaultHeight = -1;
        int extendedHeight = -1;
        bool isExtended;

        public static ErrorMessage instance;

        public ErrorMessage(Exception ex)
        {
            InitializeComponent();

            if (instance != null)
                return;

            Localise();

            defaultHeight = this.Height;
            extendedHeight = logOutput.Location.Y + logOutput.Height + 50;

            instance = this;

            string fileName = $"{DateTime.Now.Date.Day}.{DateTime.Now.Date.Month}.{DateTime.Now.Date.Year} " +
                $"{DateTime.Now.Hour.ToString()}.{DateTime.Now.Minute.ToString()}.{DateTime.Now.Second.ToString()}";

            FileName = fileName;

            label2.Text = Localisation.Get("An error has occured and the info has been saved to {0}.\n" +
            "If it happens again, please send the log to the MSCMM developer.", fileName);

            Logs.CrashLog(ex.ToString(), true);
            btnMoreDetail.Text = (char.ConvertFromUtf32(0x2193) + " " + Localisation.Get("Show More Info"));
        }

        void Localise()
        {
            btnExit.Text = Localisation.Get("Exit");
            btnLog.Text = Localisation.Get("Open Log");
            btnClose.Text = Localisation.Get("Close");
            contextDefaultCopy.Text = Localisation.Get("Copy");
            contextDefaultSelectAll.Text = Localisation.Get("Select All");
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
            this.Size = new Size(this.Size.Width, isExtended ? extendedHeight : defaultHeight);
            btnMoreDetail.Text = isExtended ? (char.ConvertFromUtf32(0x2191) + " " + Localisation.Get("Hide More Info")) 
                : (char.ConvertFromUtf32(0x2193) + " " + Localisation.Get("Show More Info"));
            string file = File.ReadAllText($"LOG\\{FileName}.txt");
            file = file.Replace("\n", Environment.NewLine);
            logOutput.Text = file;
        }

        private void BtnExit_Click(object sender, EventArgs e)
        {
            Process.GetCurrentProcess().Kill();
        }

        private void contextDefaultCopy_Click(object sender, EventArgs e)
        {
            logOutput.Copy();
        }

        private void contextDefaultSelectAll_Click(object sender, EventArgs e)
        {
            logOutput.SelectAll();
        }
    }
}
