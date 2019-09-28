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
using System.Linq;
using System.Threading.Tasks;
using System.IO;
using System.Windows.Forms;
using System.Diagnostics;
using System.Media;

namespace OggConverter
{
    class Converter
    {
        /// <summary>
        /// Counts how many conversions have been made
        /// </summary>
        public static int TotalConversions { get; set; }

        /// <summary>
        /// Stores how many files have been skipped while sorting
        /// </summary>
        public static int Skipped { get; set; }

        /// <summary>
        /// Prevents the program from closing if conversion is in progress
        /// </summary>
        public static bool IsBusy { get; set; }

        /// <summary>
        /// List of supported extensions
        /// </summary>
        public static string[] extensions = new[] { ".mp3", ".wav", ".aac", ".m4a", ".wma", ".ogg" };

        /// <summary>
        /// Starts the conversion of every found file in Radio or CD
        /// </summary>
        public static async void StartConversion()
        {
            if (Utilities.IsToolBusy())
            {
                Form1.instance.Log(Localisation.Get("Program is busy."));
                return;
            }

            if (Settings.GamePath.Length == 0)
            {
                Form1.instance.Log(Localisation.Get("Select the game path first."));
                return;
            }

            Converter.IsBusy = true;
            Converter.TotalConversions = 0;
            Converter.Skipped = 0;

            try
            {
                Form1.instance.RestrictedMode(true);
                Form1.instance.Log("\n-----------------------------------------------------------------------------------------------------------------------------------------");
                await Task.Run(() => Converter.ConvertFolder("Radio", 99));

                if (Directory.Exists($"{Settings.GamePath}\\CD") && !Directory.Exists($"{Settings.GamePath}\\CD1"))
                {
                    await Converter.ConvertFolder("CD", 15);
                }
                else
                {
                    // Added with the new update
                    await Task.Run(() => Converter.ConvertFolder("CD1", 15));
                    await Task.Run(() => Converter.ConvertFolder("CD2", 15));
                    await Task.Run(() => Converter.ConvertFolder("CD3", 15));
                }

                if (Converter.Skipped != 4)
                {
                    Form1.instance.Log(Localisation.Get("\nDone!"));
                    Form1.instance.Log(Localisation.Get("Converted {0} file(s)in total", Converter.TotalConversions));
                    Form1.instance.Log(Localisation.Get("Conversion log was saved to history.txt"));
                }
                else
                {
                    Form1.instance.Log(Localisation.Get("Conversion log will not be saved, because both Radio and CDs were skipped"));
                }

                SystemSounds.Exclamation.Play();
            }
            catch (Exception ex)
            {
                ErrorMessage err = new ErrorMessage(ex);
                err.ShowDialog();
            }
            finally
            {
                Converter.IsBusy = false;
                Form1.instance.RestrictedMode(false);
            }

            Form1.instance.UpdateSongList();
        }

        /// <summary>
        /// Converts all files found in the folder
        /// </summary>
        /// <param name="folder">Folder to convert (CD or Radio)</param>
        /// <param name="limit">Limit of files - My Summer Car uses maximum of 15 files for CD and 99 for Radio</param>
        /// <returns></returns>
        public static async Task ConvertFolder(string folder, int limit)
        {
            if (!File.Exists("ffmpeg.exe"))
            {
                MessageBox.Show(Localisation.Get("FFmpeg.exe is missing! Try to re-download MSC Music Manager."), 
                    Localisation.Get("Error"), MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            Form1.instance.Log(Localisation.Get("Initializing {0} conversion...\n", folder));
            string path = $"{Settings.GamePath}\\{folder}";

            if (!Directory.Exists(path))
            {
                Form1.instance.Log(Localisation.Get("Skipping {0} because it doesn't exist...", folder));
                return;
            }

            try
            {
                DirectoryInfo d = new DirectoryInfo(path);
                FileInfo[] files
                    = d.GetFiles()
                    .Where(f => extensions.Contains(f.Extension.ToLower()) && !f.Name.StartsWith("track"))
                    .ToArray();

                // If no files have been found - aborts the conversion
                if (files.Length == 0)
                {
                    Form1.instance.Log(Localisation.Get("Couldn't find any file to convert in {0}", folder));
                    Skipped++;
                    return;
                }

                int inGame = 1; // Counts how many files there are in game + after that variable new files are named

                // Counting how many OGG files there are already
                for (int c = 1; File.Exists($"{path}\\track{c}.ogg"); c++)
                    inGame++;

                // Starting the conversion of all found files
                foreach (FileInfo file in files)
                {
                    // Prevents overwriting existing files, if there's an gap between them
                    while (File.Exists($"{path}\\track{inGame}.ogg"))
                        inGame++;

                    // If the limit of files per folder is applied, checks if it isn't over it
                    if ((limit != 0) && (inGame > limit))
                    {
                        DialogResult res = MessageBox.Show(
                            Localisation.Get("There's over {0} files in {1} already converted. " +
                            "My Summer Car allows max {0} files for {1} and any file above that will be ignored. " +
                            "Would you like to continue?", limit, folder),
                            Localisation.Get("Stop"),
                            MessageBoxButtons.YesNo,
                            MessageBoxIcon.Information);

                        if (res == DialogResult.No)
                        {
                            Form1.instance.Log(Localisation.Get("Aborted {0} conversion.", folder));
                            break;
                        }
                    }

                    Form1.instance.Log(Localisation.Get("Converting {0}", file.Name));

                    string songName = null;

                    // If the file is already in OGG format - skip conversion and just rename it accordingly.
                    if (file.Name.EndsWith(".ogg") && !file.Name.StartsWith("track"))
                    {
                        File.Move($"{path}\\{file.Name}", $"{path}\\track{inGame}.ogg");
                        ProcessStartInfo psi = new ProcessStartInfo("ffmpeg.exe", $"-i \"{path}\\track{inGame}.ogg\"")
                        {
                            UseShellExecute = false,
                            RedirectStandardError = true,
                            CreateNoWindow = true
                        };

                        var process = Process.Start(psi);

                        string[] ffmpegOut = process.StandardError.ReadToEnd().Split('\n');
                        songName = MetaData.GetFromOutput(ffmpegOut);
                        await Task.Factory.StartNew(() => MetaData.AddOrEdit($"track{inGame}", songName, folder));
                    }
                    else
                    {
                        ProcessStartInfo psi = new ProcessStartInfo("ffmpeg.exe", $"-i \"{path}\\{file.Name}\" -acodec libvorbis \"{path}\\track{inGame}.ogg\"")
                        {
                            UseShellExecute = false,
                            RedirectStandardError = true,
                            RedirectStandardOutput = true,
                            CreateNoWindow = true
                        };

                        Process process = null;
                        await Task.Run(() => process = Process.Start(psi));

                        string[] ffmpegOut = process.StandardError.ReadToEnd().Split('\n');
                        await Task.Run(() => process.WaitForExit());
                        songName = MetaData.GetFromOutput(ffmpegOut);
                        await Task.Factory.StartNew(() => MetaData.AddOrEdit($"track{inGame}", songName));
                    }

                    Form1.instance.Log(Localisation.Get("Finished {0} as track{1}.ogg", file.Name, inGame));

                    if (Settings.RemoveMP3)
                        File.Delete($"{path}\\{file.Name}");

                    Logs.History(Localisation.Get("Added '{0}' (track{1}.ogg) in {2}", songName, inGame, folder));

                    inGame++;
                    TotalConversions++;
                }
            }
            catch (Exception ex)
            {
                ErrorMessage err = new ErrorMessage(ex);
                err.ShowDialog();
            }

            Form1.instance.Log(Localisation.Get("Converted {0} file(s) in {1}", TotalConversions, folder));
        }

        /// <summary>
        /// Works like ConvertFolder, but instead it just converts single file.
        /// </summary>
        /// <param name="filePath">Path to the file.</param>
        /// <param name="folder">Folder to what we want to convert (CD or Radio)</param>
        /// <param name="limit">Limit of files - My Summer Car uses maximum of 15 files for CD and 99 for Radio</param>
        /// <param name="forcedName">If set, instead of getting name from ffmpeg output, it will get it from forcedName.</param>
        /// <returns></returns>
        public static async Task ConvertFile(string filePath, string folder, int limit, string altName = "", bool forceAltName = false)
        {
            if (!File.Exists($"{Directory.GetCurrentDirectory()}\\ffmpeg.exe"))
            {
                MessageBox.Show("FFmpeg.exe is missing! Try to re-download MSC Music Manager.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            if (!filePath.ContainsAny(extensions))
            {
                if (Form1.instance != null)
                    Form1.instance.Log(Localisation.Get("'{0}' is not a recognizable music file, so it will be skipped.", filePath.Substring(filePath.LastIndexOf('\\') + 1)));
                return;
            }

            int inGame = 1;
            if (Form1.instance != null)
                Form1.instance.Log(Localisation.Get($"\nConverting '{0}'\n", filePath.Substring(filePath.LastIndexOf('\\') + 1)));


            try
            {
                //Counting how many OGG files there are already
                for (int c = 1; File.Exists($"{Settings.GamePath}\\{folder}\\track{c}.ogg"); c++)
                    inGame++;

                if ((limit != 0) && (inGame > limit))
                {
                    DialogResult res = MessageBox.Show(
                        Localisation.Get("There's over {0} files in {1} already converted. " +
                        "My Summer Car allows max {0} files for {1} and any file above that will be ignored. " +
                        "Would you like to continue?", limit, folder),
                        Localisation.Get("Stop"),
                        MessageBoxButtons.YesNo,
                        MessageBoxIcon.Information);

                    if (res == DialogResult.No)
                    {
                        if (Form1.instance != null)
                            Form1.instance.Log(Localisation.Get("Aborted {0} conversion.", folder));
                        return;
                    }
                }

                string songName = "";

                // If it's just OGG file - instead of converting, simply rename it
                if (filePath.EndsWith(".ogg"))
                {
                    File.Move(filePath, $"{Settings.GamePath}\\{folder}\\track{inGame}.ogg");

                    ProcessStartInfo psi = new ProcessStartInfo("ffmpeg.exe", $"-i \"{Settings.GamePath}\\{folder}\\track{inGame}.ogg\"")
                    {
                        UseShellExecute = false,
                        RedirectStandardError = true,
                        CreateNoWindow = true
                    };

                    var process = Process.Start(psi);

                    string[] ffmpegOut = process.StandardError.ReadToEnd().Split('\n');
                    songName = MetaData.GetFromOutput(ffmpegOut);

                    MetaData.AddOrEdit($"track{inGame}", songName);
                }
                else
                {
                    ProcessStartInfo psi = new ProcessStartInfo("ffmpeg.exe", $"-i \"{filePath}\" -acodec libvorbis \"{Settings.GamePath}\\{folder}\\track{inGame}.ogg\"")
                    {
                        UseShellExecute = false,
                        RedirectStandardError = true,
                        RedirectStandardOutput = true,
                        CreateNoWindow = true
                    };

                    Process process = null;
                    await Task.Run(() => process = Process.Start(psi));

                    if (altName == null)
                    {
                        string[] ffmpegOut = process.StandardError.ReadToEnd().Split('\n');
                        songName = MetaData.GetFromOutput(ffmpegOut);
                        if ((songName == " - " || songName == "") && altName != "")
                            songName = altName;
                    }
                    else
                    {
                        songName = altName;
                    }

                    MetaData.AddOrEdit($"track{inGame}", songName);

                    await Task.Run(() => process.WaitForExit());
                }

                Logs.History(Localisation.Get("Added '{0}' (track{1}.ogg) in {2}", songName, inGame, folder));

                if (Form1.instance != null)
                    Form1.instance.Log(Localisation.Get("Finished '{0}' as 'track{1}.ogg'", filePath.Substring(filePath.LastIndexOf('\\') + 1), inGame));
            }
            catch (Exception ex)
            {
                ErrorMessage err = new ErrorMessage(ex);
                err.ShowDialog();
            }
        }

        /// <summary>
        /// Checks if there are any files waiting for conversion in folder
        /// </summary>
        /// <param name="folder">Radio or CD</param>
        /// <returns></returns>
        public static bool FilesWaitingForConversion(string folder)
        {
            if (!Directory.Exists($"{Settings.GamePath}\\{folder}")) return false;

            DirectoryInfo di = new DirectoryInfo($"{Settings.GamePath}\\{folder}");
            FileInfo[] files
                = di.GetFiles()
                .Where(f => extensions.Contains(f.Extension.ToLower()) && !f.Name.StartsWith("track"))
                .ToArray();

            return files.Length > 0 ? true : false;
        }

        /// <summary>
        /// Checks Radio, CD, CD1, CD2 and CD3 if are waiting for conversion
        /// </summary>
        /// <returns></returns>
        public static bool AnyFilesWaitingForConversion()
        {
            return Converter.FilesWaitingForConversion("Radio") || Converter.FilesWaitingForConversion("CD") || Converter.FilesWaitingForConversion("CD1")
                || Converter.FilesWaitingForConversion("CD2") || Converter.FilesWaitingForConversion("CD3");
        }
    }
}
