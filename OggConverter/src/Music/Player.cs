﻿// MSC Music Manager
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

using System.IO;
using System.Diagnostics;
using System.Windows.Forms;
using System.Collections.Generic;
using System;
using System.Linq;

namespace OggConverter
{
    class Player
    {
        static Process process;

        public static List<Tuple<string, string>> WorkingSongList = new List<Tuple<string, string>>();

        public static bool IsBusy { get; set; }

        /// <summary>
        /// Plays selected song using ffplay.exe
        /// </summary>
        /// <param name="path">Path to the file</param>
        public static void Play(string path)
        {
            if (!File.Exists("ffplay.exe"))
            {
                MessageBox.Show("FFplay is missing! Try to re-download MSC Music Manager.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            Stop();

            process = new Process();
            process.StartInfo.RedirectStandardOutput = true;
            process.StartInfo.UseShellExecute = false;
            process.StartInfo.CreateNoWindow = true;
            
            // Setup executable and parameters
            process.StartInfo.FileName = "ffplay.exe";
            process.StartInfo.Arguments = $"-nodisp \"{path}\"";
            process.Start();
        }

        /// <summary>
        /// Stops music playback
        /// </summary>
        public static void Stop()
        {
            try
            {
                if (process != null)
                {
                    process.Kill();
                    process.Close();
                    return;
                }
            }
            catch { }

            // Used if process is still null, but ffplay is still running
            foreach (var process in Process.GetProcessesByName("ffplay"))
                process.Kill();
        }

        /// <summary>
        /// Sorts songs to remove gaps 
        /// </summary>
        /// <param name="folder">Radio or CD folder/param>
        public static void Sort(string folder)
        {
            if (Utilities.IsToolBusy())
            {
                MessageBox.Show("Program is busy", "Stop", MessageBoxButtons.OK, MessageBoxIcon.Stop);
                return;
            }

            IsBusy = true;

            Stop();

            int skipped = 0;

            try
            {
                for (int i = 1; i <= 99; i++)
                {
                    // File doesn't exist? Skipping the value
                    if (!File.Exists($"{Settings.GamePath}\\{folder}\\track{i}.ogg"))
                    {
                        skipped++;
                    }
                    else
                    {
                        // If nothing was skipped, there is no point of moving file
                        if (skipped == 0) continue;

                        // Waiting for file to be free
                        while (!Utilities.IsFileReady($"{Settings.GamePath}\\{folder}\\track{i}.ogg")) { }

                        // Moving the file
                        File.Move($"{Settings.GamePath}\\{folder}\\track{i}.ogg", $"{Settings.GamePath}\\{folder}\\track{i - skipped}.ogg");

                        Logs.History($"Sorting: moved \"track{i}\" to \"track{i - skipped}\" in {folder}");
                        Form1.instance.Log($"Sorting: moved \"track{i}\" to \"track{i - skipped}\" in {folder}");

                        MetaData.AddOrEdit($"track{i - skipped}", MetaData.GetName($"track{i}"));
                        MetaData.Remove($"track{i}");
                      
                        // Adjusting the i value by skipped
                        if (skipped != 0)
                            i -= skipped;

                        // Reseting skipped value
                        skipped = 0;
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
                IsBusy = false;
                Form1.instance.UpdateSongList();
            }
        }

        /// <summary>
        /// Allows to change the folder of song
        /// </summary>
        /// <param name="songList">Form1 songList</param>
        /// <param name="folder">Working folder</param>
        /// <param name="moveUp">false - file is moved down, true - file is moved up</param>
        public static void ChangeOrder(ListBox songList, string folder, bool moveUp)
        {
            int selectedIndex = songList.SelectedIndex;
            if ((songList.SelectedIndex == -1) || (selectedIndex == 0 && moveUp) || (selectedIndex == songList.Items.Count - 1 && !moveUp)) return;

            if (Utilities.IsToolBusy())
            {
                MessageBox.Show("Program is busy", "Stop", MessageBoxButtons.OK, MessageBoxIcon.Stop);
                return;
            }

            IsBusy = true;
            Stop();

            try
            {
                string oldName = Player.WorkingSongList[selectedIndex].Item1;
                string newName = $"track{selectedIndex + (moveUp ? 0 : 2)}";

                string trackTemp = "";

                // Waiting for file to be free
                while (!Utilities.IsFileReady($"{Settings.GamePath}\\{folder}\\{oldName}.ogg")) { }

                // Moving file that now uses the current new name
                //
                // For instance: we're moving track2 to track1.
                // So we first move track1 out of the place and renaming it to trackTemp IF track1 EXISTS
                if (File.Exists($"{Settings.GamePath}\\{folder}\\{newName}.ogg"))
                {
                    if (File.Exists($"{Settings.GamePath}\\{folder}\\trackTemp.ogg"))
                        File.Delete($"{Settings.GamePath}\\{folder}\\trackTemp.ogg");

                    File.Move($"{Settings.GamePath}\\{folder}\\{newName}.ogg", $"{Settings.GamePath}\\{folder}\\trackTemp.ogg");
                    trackTemp = MetaData.GetName(newName);
                }

                // Now we're moving the file that we want to actually move
                File.Move($"{Settings.GamePath}\\{folder}\\{oldName}.ogg", $"{Settings.GamePath}\\{folder}\\{newName}.ogg");

                MetaData.AddOrEdit(newName, MetaData.GetName(oldName));
                MetaData.AddOrEdit(oldName, trackTemp);

                // Finally we move the file that we set as temp (if it exists)
                if (File.Exists($"{Settings.GamePath}\\{folder}\\trackTemp.ogg"))
                    File.Move($"{Settings.GamePath}\\{folder}\\trackTemp.ogg", $"{Settings.GamePath}\\{folder}\\{oldName}.ogg");

                Logs.History($"Changing Order: moved \"{newName}\" to \"{oldName}\", and \"{oldName}\" to \"{newName}\"");
                Form1.instance.Log($"Changing Order: moved \"{newName}\" to \"{oldName}\", and \"{oldName}\" to \"{newName}\"");

            }
            catch (Exception ex)
            {
                ErrorMessage err = new ErrorMessage(ex);
                err.ShowDialog();
            }
            finally
            {
                IsBusy = false;
                Form1.instance.UpdateSongList();
                songList.SelectedIndex = -1;
                songList.SelectedIndex = selectedIndex + (moveUp ? -1 : 1);
            }
        }

        /// <summary>
        ///  Moves file to opposite folder (ex. from Radio to CD and vice versa)
        /// </summary>
        /// <param name="fileName">File name that we want to move</param>
        /// <param name="source">From where the files are being moves</param>
        /// <param name="destination">Where to files are moved</param>
        public static void MoveTo(string fileName, string source, string destination)
        {
            if (Utilities.IsToolBusy())
            {
                MessageBox.Show("Program is busy", "Stop", MessageBoxButtons.OK, MessageBoxIcon.Stop);
                return;
            }

            IsBusy = true;
            Stop();

            try
            {
                int newNumber = 1;
                for (int i = 1; File.Exists($"{Settings.GamePath}\\{destination}\\track{i}.ogg"); i++)
                    newNumber++;

                // Waiting for file to be free
                while (!Utilities.IsFileReady($"{Settings.GamePath}\\{source}\\{fileName}.ogg")) { }
                File.Move($"{Settings.GamePath}\\{source}\\{fileName}.ogg", $"{Settings.GamePath}\\{destination}\\track{newNumber}.ogg");
                MetaData.MoveToDatabase(source, fileName, destination, $"track{newNumber}");

                Logs.History($"File Moving: moved \"{fileName}\" from \"{source}\" to \"{destination}\" as \"track{newNumber}\"");
                Form1.instance.Log($"File Moving: moved \"{fileName}\" from \"{source}\" to \"{destination}\" as \"track{newNumber}\"");
            }
            catch (Exception ex)
            {
                ErrorMessage err = new ErrorMessage(ex);
                err.ShowDialog();
            }
            finally
            {
                IsBusy = false;
                Form1.instance.UpdateSongList();
            }
        }

        /// <summary>
        /// Clones song and gives it correct name
        /// </summary>
        /// <param name="folder">Current folder (radio or cd)</param>
        /// <param name="fileName">File name</param>
        public static void Clone(string folder, string fileName)
        {
            if (Utilities.IsToolBusy())
            {
                MessageBox.Show("Program is busy", "Stop", MessageBoxButtons.OK, MessageBoxIcon.Stop);
                return;
            }

            IsBusy = true;
            string newName = null;

            try
            {
                // Getting a new name for cloned song
                for (int i = 1; i <= 99; i++)
                {
                    if (!File.Exists($"{Settings.GamePath}\\{folder}\\track{i}.ogg"))
                    {
                        newName = $"track{i}";
                        break;
                    }
                }

                string pathToFile = $"{Settings.GamePath}\\{folder}\\{fileName}"; // Path to file to be cloned with it's name

                File.Copy($"{pathToFile}.ogg", $"{Settings.GamePath}\\{folder}\\{newName}.ogg");
                MetaData.AddOrEdit(newName, MetaData.GetName(fileName));

                Logs.History($"Cloned \"{fileName}\" to \"{newName}\" in {folder}");
                Form1.instance.Log($"Cloned \"{fileName}\" to \"{newName}\" in {folder}");
            }
            catch (Exception ex)
            {
                ErrorMessage err = new ErrorMessage(ex);
                err.ShowDialog();
            }
            finally
            {
                IsBusy = false;
            }
        }

        /// <summary>
        /// Randomizes songs order in folder.
        /// </summary>
        /// <param name="folder">Folder in which files will be shuffled</param>
        public static void Shuffle(string folder)
        {
            if (Utilities.IsToolBusy())
            {
                MessageBox.Show("Program is busy", "Stop", MessageBoxButtons.OK, MessageBoxIcon.Stop);
                return;
            }

            IsBusy = true;
            Stop();

            Form1.instance.Log("Shuffling!");

            try
            {
                // Collecting all files into FileInfo list
                DirectoryInfo di = new DirectoryInfo($"{Settings.GamePath}\\{folder}");
                List<FileInfo> files = new List<FileInfo>(di.GetFiles("track*.ogg"));

                // Renaming files to temporary name
                for (int i = 0; i < files.Count; i++)
                {
                    string file = $"{Settings.GamePath}\\{folder}\\{files[i].Name.Replace(".ogg", "")}"; // path + file name without extension
                    File.Move($"{file}.ogg", $"{file}.ogg.temp");
                    MetaData.ChangeFile(files[i].Name.Replace(".ogg", ""), $"{files[i].Name.Replace(".ogg", "")}_temp");
                }

                // Randomizing order of files list
                files = files.OrderBy(a => Guid.NewGuid()).ToList();

                // Now moving the files with temporary names to new name
                for (int i = 0; i < files.Count; i++)
                {
                    string file = $"{Settings.GamePath}\\{folder}\\{files[i].Name.Replace(".ogg", "")}"; // path + file name without extension
                    File.Move($"{file}.ogg.temp", $"{Settings.GamePath}\\{folder}\\track{i + 1}.ogg");
                    MetaData.ChangeFile($"{files[i].Name.Replace(".ogg", "")}_temp", $"track{i + 1}");
                }

                MetaData.SortDatabase();
            }
            catch (Exception ex)
            {
                ErrorMessage err = new ErrorMessage(ex);
                err.ShowDialog();
            }
            finally
            {
                IsBusy = false;
            }
        }

        public static void Delete(string folder, string fileName, string songName)
        {
            if (Utilities.IsToolBusy())
            {
                Form1.instance.Log("Program is busy.");
                return;
            }

            try
            {
                string file = $"{Settings.GamePath}\\{folder}\\{fileName}.ogg";

                DialogResult dl = MessageBox.Show($"Are you sure you want to delete:\n\n{songName}?", "Question", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                if (dl == DialogResult.Yes)
                {
                    Player.Stop();

                    if (File.Exists(file))
                    {
                        while (!Utilities.IsFileReady(file)) { }

                        File.Delete(file);
                        MetaData.Remove(fileName);

                        Logs.History($"Removed \"{songName}\" ({fileName}) from {folder}");

                        if (Settings.AutoSort)
                            Player.Sort(folder);
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
                Form1.instance.UpdateSongList();
            }
        }
    }
}
