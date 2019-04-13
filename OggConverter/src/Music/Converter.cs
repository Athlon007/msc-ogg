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

using System.Linq;
using System.Threading.Tasks;
using System.IO;
using System.Windows.Forms;
using System.Diagnostics;

namespace OggConverter
{
    class Converter
    {
        /// <summary>
        /// Stores latest conversion log, which later is dumped into LastConversion.txt file
        /// </summary>
        public static string ConversionLog { get; set; }
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
        /// Converts all files found in the folder
        /// </summary>
        /// <param name="mscPath">My Summer Car path</param>
        /// <param name="folder">Folder to convert (CD or Radio)</param>
        /// <param name="limit">Limit of files - My Summer Car uses maximum of 15 files for CD and 99 for Radio</param>
        /// <returns></returns>
        public static async Task ConvertFolder(string mscPath, string folder, int limit)
        {
            if (!File.Exists("ffmpeg.exe"))
            {
                MessageBox.Show("FFmpeg.exe is missing! Try to re-download MSC Music Manager.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            Form1.instance.Log += $"\nInitializing {folder} conversion...\n";
            string path = $"{mscPath}\\{folder}";

            DirectoryInfo d = new DirectoryInfo(path);
            FileInfo[] files
                = d.GetFiles()
                .Where(f => extensions.Contains(f.Extension.ToLower()) && !f.Name.StartsWith("track"))
                .ToArray();

            // If no files have been found - aborts the conversion
            if (files.Length == 0)
            {
                Form1.instance.Log += $"\nCouldn't find any file to convert in {folder}";
                Skipped++;
                return;
            }

            ConversionLog += $"{folder.ToUpper()}:\n";

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
                    DialogResult res = MessageBox.Show($"There's over {limit} files in CDs already converted. " +
                        $"My Summer Car allows max {limit} files for {folder.ToUpper()}s and any file above that will be ignored. Would you like to continue?",
                        "Stop",
                        MessageBoxButtons.YesNo,
                        MessageBoxIcon.Information);

                    if (res == DialogResult.No)
                    {
                        Form1.instance.Log += $"\nAborted {folder} conversion.";
                        break;
                    }
                }

                Form1.instance.Log += $"Converting {file.Name}\n";

                // If the file is already in OGG format - skip conversion and just rename it accordingly.
                if (file.Name.EndsWith(".ogg"))
                {
                    File.Move($"{path}\\{file.Name}", $"{path}\\track{inGame}.ogg");

                    if (Settings.UseNewNaming)
                    {
                        ProcessStartInfo psi = new ProcessStartInfo("ffmpeg.exe", $"-i \"{path}\\track{inGame}.ogg\"")
                        {
                            UseShellExecute = false,
                            RedirectStandardError = true,
                            CreateNoWindow = true
                        };

                        var process = Process.Start(psi);

                        string[] ffmpegOut = process.StandardError.ReadToEnd().Split('\n');
                        string songName = MetaDatas.GetSongName(ffmpegOut);
                        MetaDatas.CreateMetaFile($"{mscPath}\\{folder}\\track{inGame}.mscmm", songName);
                    }
                }
                else
                {
                    /*
                    Process process = new Process();
                    process.StartInfo.RedirectStandardOutput = true;
                    process.StartInfo.UseShellExecute = false;
                    process.StartInfo.CreateNoWindow = true;
                    // Setup executable and parameters
                    process.StartInfo.FileName = "ffmpeg.exe";
                    process.StartInfo.Arguments = $"-i \"{path}\\{file.Name}\" -acodec libvorbis \"{path}\\track{inGame}.ogg\"";

                    process.Start();
                    */

                    ProcessStartInfo psi = new ProcessStartInfo("ffmpeg.exe", $"-i \"{path}\\{file.Name}\" -acodec libvorbis \"{path}\\track{inGame}.ogg\"")
                    {
                        UseShellExecute = false,
                        RedirectStandardError = true,
                        RedirectStandardOutput = true,
                        CreateNoWindow = true
                    };

                    //var process = Process.Start(psi);

                    Process process = null;
                    await Task.Run(() => process = Process.Start(psi));

                    if (Settings.UseNewNaming)
                    {
                        string[] ffmpegOut = process.StandardError.ReadToEnd().Split('\n');
                        string songName = MetaDatas.GetSongName(ffmpegOut);
                        MetaDatas.CreateMetaFile($"{mscPath}\\{folder}\\track{inGame}.mscmm", songName);
                    }

                    await Task.Run(() => process.WaitForExit());
                }

                Form1.instance.Log += $"Finished {file.Name} as track{inGame}.ogg\n";

                if (Settings.RemoveMP3)
                    File.Delete($"{path}\\{file.Name}");

                ConversionLog += $"\"{file.Name}\" as \"track{inGame}.ogg\"\n";
                inGame++;
                TotalConversions++;
            }

            ConversionLog += "\n\n";
            Form1.instance.Log += $"\nConverted {TotalConversions} files in {folder}";
        }

        /// <summary>
        /// Works like ConvertFolder, but instead it just converts single file.
        /// </summary>
        /// <param name="filePath">Path to the file.</param>
        /// <param name="mscPath">My Summer Car path.</param>
        /// <param name="folder">Folder to what we want to convert (CD or Radio)</param>
        /// <param name="limit">Limit of files - My Summer Car uses maximum of 15 files for CD and 99 for Radio</param>
        /// <param name="forcedName">ONLY IF Settings.UseNewNaming IS ON. If set, instead of getting name from ffmpeg output, it will get it from forcedName.</param>
        /// <returns></returns>
        public static async Task ConvertFile(string filePath, string mscPath, string folder, int limit, string forcedName = null)
        {
            if (!File.Exists($"{Directory.GetCurrentDirectory()}\\ffmpeg.exe"))
            {
                MessageBox.Show("FFmpeg.exe is missing! Try to re-download MSC Music Manager.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            if (!filePath.ContainsAny(extensions))
            {
                if (Form1.instance != null)
                    Form1.instance.Log += $"\n\"{filePath.Substring(filePath.LastIndexOf('\\') + 1)}\" is not a music file so it will be skipped.";
                return;
            }

            int inGame = 1;
            if (Form1.instance != null)
                Form1.instance.Log += $"\n\nConverting \"{filePath.Substring(filePath.LastIndexOf('\\') + 1)}\"\n";
            
            //Counting how many OGG files there are already
            for (int c = 1; File.Exists($"{mscPath}\\{folder}\\track{c}.ogg"); c++)
                inGame++;

            if ((limit != 0) && (inGame > limit))
            {
                DialogResult res = MessageBox.Show($"There's over {limit} files in CDs already converted. " +
                    $"My Summer Car allows max {limit} files for {folder.ToUpper()}s and any file above that will be ignored. Would you like to continue?",
                    "Stop",
                    MessageBoxButtons.YesNo,
                    MessageBoxIcon.Information);

                if (res == DialogResult.No)
                {
                    if (Form1.instance != null)
                        Form1.instance.Log += $"\nAborted {folder} conversion.";
                    return;
                }
            }

            // If it's just OGG file - instead of converting, simply rename it
            if (filePath.EndsWith(".ogg"))
            {
                File.Move(filePath, $"{mscPath}\\{folder}\\track{inGame}.ogg");

                if (Settings.UseNewNaming)
                {
                    ProcessStartInfo psi = new ProcessStartInfo("ffmpeg.exe", $"-i \"{mscPath}\\{folder}\\track{inGame}.ogg\"")
                    {
                        UseShellExecute = false,
                        RedirectStandardError = true,
                        CreateNoWindow = true
                    };

                    var process = Process.Start(psi);

                    string[] ffmpegOut = process.StandardError.ReadToEnd().Split('\n');
                    string songName = MetaDatas.GetSongName(ffmpegOut);
                    MetaDatas.CreateMetaFile($"{mscPath}\\{folder}\\track{inGame}.mscmm", songName);
                }
            }
            else
            {
                ProcessStartInfo psi = new ProcessStartInfo("ffmpeg.exe", $"-i \"{filePath}\" -acodec libvorbis \"{mscPath}\\{folder}\\track{inGame}.ogg\"")
                {
                    UseShellExecute = false,
                    RedirectStandardError = true,
                    RedirectStandardOutput = true,
                    CreateNoWindow = true
                };

                Process process = null;
                await Task.Run(() => process = Process.Start(psi));

                if (Settings.UseNewNaming)
                {
                    string songName = null;

                    if (forcedName != null)
                        songName = forcedName;
                    else
                    {
                        string[] ffmpegOut = process.StandardError.ReadToEnd().Split('\n');
                        songName = MetaDatas.GetSongName(ffmpegOut);
                    }
                    MetaDatas.CreateMetaFile($"{mscPath}\\{folder}\\track{inGame}.mscmm", songName);
                }

                await Task.Run(() => process.WaitForExit());
            }

            if (Form1.instance != null)
                Form1.instance.Log += $"Finished \"{filePath.Substring(filePath.LastIndexOf('\\') + 1)}\" as \"track{inGame}.ogg\"";
        }
    }
}
