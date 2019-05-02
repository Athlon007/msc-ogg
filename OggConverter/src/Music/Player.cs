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

        public static List<string> WorkingSongList = new List<string>();

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

            Stop();

            int skipped = 0;

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

                    // Moving metadata (if new naming system is used)
                    if (File.Exists($"{Settings.GamePath}\\{folder}\\track{i}.mscmm"))
                    {
                        // If the song was deleted, but not the meta file
                        if (File.Exists($"{Settings.GamePath}\\{folder}\\track{i - skipped}.mscmm"))
                            File.Delete($"{Settings.GamePath}\\{folder}\\track{i - skipped}.mscmm");

                        File.Move($"{Settings.GamePath}\\{folder}\\track{i}.mscmm", $"{Settings.GamePath}\\{folder}\\track{i - skipped}.mscmm");
                    }

                    // Adjusting the i value by skipped
                    if (skipped != 0)
                        i -= skipped;

                    // Reseting skipped value
                    skipped = 0;
                }
            }

            Form1.instance.UpdateSongList();
        }

        /// <summary>
        /// Allows to change the folder of song
        /// </summary>
        /// <param name="songList">Form1 songList</param>
        /// <param name="mscPath">My Summer Car directory</param>
        /// <param name="moveUp">false - file is moved down, true - file is moved up</param>
        public static void ChangeOrder(ListBox songList, string mscPath, bool moveUp)
        {
            if (songList.SelectedIndex == -1) return;

            if (Utilities.IsToolBusy())
            {
                MessageBox.Show("Program is busy", "Stop", MessageBoxButtons.OK, MessageBoxIcon.Stop);
                return;
            }

            Stop();

            int selectedIndex = songList.SelectedIndex;
            if (selectedIndex == 0) return;

            string oldName = Player.WorkingSongList[selectedIndex];
            string newName = $"track{selectedIndex + (moveUp ? 0 : 2)}";

            // Waiting for file to be free
            while (!Utilities.IsFileReady($"{mscPath}\\{oldName}.ogg")) { }

            // Moving file that now uses the current new name
            //
            // For instance: we're moving track2 to track1.
            // So we first move track1 out of the place and renaming it to trackTemp IF track1 EXISTS
            if (File.Exists($"{mscPath}\\{newName}.ogg"))
            {
                File.Move($"{mscPath}\\{newName}.ogg", $"{mscPath}\\trackTemp.ogg");
                if (File.Exists($"{mscPath}\\{newName}.mscmm"))
                    File.Move($"{mscPath}\\{newName}.mscmm", $"{mscPath}\\trackTemp.mscmm");
            }

            // Now we're moving the file that we want to actually move
            File.Move($"{mscPath}\\{oldName}.ogg", $"{mscPath}\\{newName}.ogg");
            if (File.Exists($"{mscPath}\\{oldName}.mscmm"))
                File.Move($"{mscPath}\\{oldName}.mscmm", $"{mscPath}\\{newName}.mscmm");

            // Finally we move the file that we set as temp (if it exists)
            if (File.Exists($"{mscPath}\\trackTemp.ogg"))
                File.Move($"{mscPath}\\trackTemp.ogg", $"{mscPath}\\{oldName}.ogg");

            if (File.Exists($"{mscPath}\\trackTemp.mscmm"))
                File.Move($"{mscPath}\\trackTemp.mscmm", $"{mscPath}\\{oldName}.mscmm");

            Logs.History($"Changing Order: moved \"{newName}\" to \"{oldName}\", and \"{oldName}\" to \"{newName}\"");
            Form1.instance.Log($"Changing Order: moved \"{newName}\" to \"{oldName}\", and \"{oldName}\" to \"{newName}\"");

            Form1.instance.UpdateSongList();
            songList.SelectedIndex = selectedIndex + (moveUp ? -1 : 1);
        }

        /// <summary>
        ///  Moves file to opposite folder (ex. from Radio to CD and vice versa)
        /// </summary>
        /// <param name="mscPath">My Summer Car directory</param>
        /// <param name="selected">File name that we want to move</param>
        /// <param name="toCD">Whenever we want to move to CD folder or not</param>
        public static void MoveTo(string mscPath, string selected, bool toCD)
        {
            if (Utilities.IsToolBusy())
            {
                MessageBox.Show("Program is busy", "Stop", MessageBoxButtons.OK, MessageBoxIcon.Stop);
                return;
            }

            Stop();

            string moveFrom = toCD ? "CD" : "Radio";
            string moveTo = moveFrom == "CD" ? "Radio" : "CD";

            int newNumber = 1;

            for (int i = 1; File.Exists($"{mscPath}\\{moveTo}\\track{i}.ogg"); i++)
                newNumber++;

            // Waiting for file to be free
            while (!Utilities.IsFileReady($"{mscPath}\\{moveFrom}\\{selected}.ogg")) { }
            File.Move($"{mscPath}\\{moveFrom}\\{selected}.ogg", $"{mscPath}\\{moveTo}\\track{newNumber}.ogg");
            if (File.Exists($"{mscPath}\\{moveFrom}\\{selected}.mscmm"))
                File.Move($"{mscPath}\\{moveFrom}\\{selected}.mscmm", $"{mscPath}\\{moveTo}\\track{newNumber}.mscmm");

            Logs.History($"File Moving: moved \"{selected}\" from \"{moveFrom}\" to \"{moveTo}\" as \"track{newNumber}\"");
            Form1.instance.Log($"File Moving: moved \"{selected}\" from \"{moveFrom}\" to \"{moveTo}\" as \"track{newNumber}\"");

            if (Settings.AutoSort)
                Player.Sort(moveFrom);

            Form1.instance.UpdateSongList();
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

            string newName = null;

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

            File.Copy($"{pathToFile}.ogg", $"{pathToFile}.ogg");
            if (File.Exists($"{pathToFile}.mscmm"))
                File.Copy($"{pathToFile}.mscmm", $"{pathToFile}.mscmm");

            Logs.History($"Cloned \"{fileName}\" to \"{newName}\" in {folder}");
            Form1.instance.Log($"Cloned \"{fileName}\" to \"{newName}\" in {folder}");
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

            Stop();

            // Collecting all files into FileInfo list
            DirectoryInfo di = new DirectoryInfo($"{Settings.GamePath}\\{folder}");
            List<FileInfo> files= new List<FileInfo>(di.GetFiles("track*.ogg"));

            // Renaming files to temporary name
            for (int i = 0; i < files.Count; i++)
            {
                string file = $"{Settings.GamePath}\\{folder}\\{files[i].Name.Replace(".ogg", "")}"; // path + file name without extension;
                File.Move($"{file}.ogg", $"{file}.ogg.temp");
                if (File.Exists($"{file}.mscmm"))
                    File.Move($"{file}.mscmm", $"{file}.mscmm.temp");
            }

            // Randomizing order of files list
            files = files.OrderBy(a => Guid.NewGuid()).ToList();

            // Now moving the files with temporary names to new name
            for (int i = 0; i < files.Count; i++)
            {
                string file = $"{Settings.GamePath}\\{folder}\\{files[i].Name.Replace(".ogg", "")}"; // path + file name without extension;
                File.Move($"{file}.ogg.temp", $"{Settings.GamePath}\\{folder}\\track{i + 1}.ogg");
                if (File.Exists($"{file}.mscmm.temp"))
                    File.Move($"{file}.mscmm.temp", $"{Settings.GamePath}\\{folder}\\track{i + 1}.mscmm");
            }
        }
    }
}
