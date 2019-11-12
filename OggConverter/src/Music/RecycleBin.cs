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
using System.IO;
using System.Windows.Forms;

namespace OggConverter
{
    class RecycleBin
    {
        /// <summary>
        /// Restores files from the recycle bin
        /// </summary>
        /// <param name="folder"></param>
        /// <param name="files"></param>
        public static void Restore(string folder, string[] files)
        {
            if (Utilities.IsToolBusy())
            {
                Form1.instance.Log(Localisation.Get("Program is busy."));
                return;
            }

            if (!Directory.Exists($"{Settings.GamePath}\\Recycle Bin"))
                return;

            try
            {
                foreach (string file in files)
                {
                    string filePath = $"{Settings.GamePath}\\Recycle Bin\\{file}.ogg";
                    if (File.Exists(filePath))
                    {
                        while (!Utilities.IsFileReady(filePath)) { }

                        string newFileName = $"track{Utilities.GetNewFileNumber(folder)}";
                        File.Move(filePath, $"{Settings.GamePath}\\{folder}\\{newFileName}.ogg");
                        MetaData.AddOrEdit(newFileName, file);

                        Logs.History(Localisation.Get("Restored '{0}' from the recycle bin", file));
                        Form1.instance.Log(Localisation.Get("Restored '{0}' from the recycle bin", file));
                    }
                }
            }
            catch (Exception ex)
            {
                ErrorMessage err = new ErrorMessage(ex);
                err.ShowDialog();
            }
            finally
            {
                if (Settings.AutoSort)
                    Player.Sort(folder);

                Form1.instance.UpdateSongList();
            }
        }

        /// <summary>
        /// Moves the file to "Recycle Bin" folder inside of folder param.
        /// </summary>
        /// <param name="folder"></param>
        /// <param name="files"></param>
        public static void Remove(string folder, string[] files)
        {
            if (Utilities.IsToolBusy())
            {
                Form1.instance.Log(Localisation.Get("Program is busy."));
                return;
            }

            try
            {
                Player.Stop();

                foreach (string file in files)
                {
                    string filePath = $"{Settings.GamePath}\\{folder}\\{file}.ogg";
                    if (File.Exists(filePath))
                    {
                        if (!Directory.Exists($"{Settings.GamePath}\\Recycle Bin"))
                            Directory.CreateDirectory($"{Settings.GamePath}\\Recycle Bin");

                        while (!Utilities.IsFileReady(filePath)) { }

                        string name = MetaData.GetName(file.Split('.')[0]);
                        File.Move(filePath, $"{Settings.GamePath}\\Recycle Bin\\{name}.ogg");
                        MetaData.Remove(file);

                        Logs.History(Localisation.Get("Moved '{0}' ({1}) from {2} to recycle bin", name, file, folder));
                        Form1.instance.Log(Localisation.Get("Moved '{0}' ({1}) from {2} to recycle bin", name, file, folder));
                    }
                }
            }
            catch (Exception ex)
            {
                ErrorMessage err = new ErrorMessage(ex);
                err.ShowDialog();
            }
            finally
            {
                if (Settings.AutoSort)
                    Player.Sort(folder);

                Form1.instance.UpdateSongList();
            }
        }

        /// <summary>
        /// Deletes the songs permamently. Includes file and MetaData entry
        /// </summary>
        /// <param name="folder">Working folder</param>
        /// <param name="files">Array of file</param>
        public static void Delete(string folder, string[] files)
        {
            if (Utilities.IsToolBusy())
            {
                Form1.instance.Log(Localisation.Get("Program is busy."));
                return;
            }

            try
            {
                string message = Localisation.Get("Are you sure you want to delete\n\n");
                int listed = 0;
                foreach (string file in files)
                {
                    message += $"- {MetaData.GetName(file.Split('.')[0])}\n";
                    listed++;

                    if (listed >= 10)
                        break;
                }

                if (files.Length - listed > 0)
                    message += Localisation.Get("\n\n...and {0} more?", files.Length - listed);

                DialogResult dl = MessageBox.Show(message,
                    Localisation.Get("Question"),
                    MessageBoxButtons.YesNo,
                    MessageBoxIcon.Question);

                if (dl == DialogResult.Yes)
                {
                    Player.Stop();

                    foreach (string file in files)
                    {
                        string filePath = $"{Settings.GamePath}\\{folder}\\{file}.ogg";
                        if (File.Exists(filePath))
                        {
                            while (!Utilities.IsFileReady(filePath)) { }

                            File.Delete(filePath);
                            string name = MetaData.GetName(file.Split('.')[0]);
                            MetaData.Remove(file);

                            Logs.History(Localisation.Get("Removed '{0}' ({1}) from {2}", name, file, folder));
                            Form1.instance.Log(Localisation.Get("Removed '{0}' ({1}) from {2}", name, file, folder));
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                ErrorMessage err = new ErrorMessage(ex);
                err.ShowDialog();
            }
            finally
            {
                if (Settings.AutoSort && folder != "Recycle Bin")
                    Player.Sort(folder);

                Form1.instance.UpdateSongList();
            }
        }

        /// <summary>
        /// Empties the recycle bin
        /// </summary>
        public static void DeleteAll()
        {
            if (Utilities.IsToolBusy())
            {
                Form1.instance.Log(Localisation.Get("Program is busy."));
                return;
            }

            try
            {
                DirectoryInfo di = new DirectoryInfo($"{Settings.GamePath}\\Recycle Bin");
                FileInfo[] files = di.GetFiles("*.ogg");

                string message = Localisation.Get("Are you sure you want to delete all {0} files?", files.Length);
                DialogResult dl = MessageBox.Show(message, Localisation.Get("Question"), MessageBoxButtons.YesNo, MessageBoxIcon.Question);

                if (dl == DialogResult.Yes)
                {
                    foreach (var file in files)
                        file.Delete();
                }
            }
            catch (Exception ex)
            {
                ErrorMessage err = new ErrorMessage(ex);
                err.ShowDialog();
            }
            finally
            {
                Form1.instance.UpdateSongList();
            }
        }
    }
}
