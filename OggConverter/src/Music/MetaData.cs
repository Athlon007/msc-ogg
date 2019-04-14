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

using System.IO;
using System.Diagnostics;
using System.Threading.Tasks;
using System.Linq;

namespace OggConverter
{
    class MetaData
    {
        /// <summary>
        /// Retrieves song name from ffmpeg output
        /// </summary>
        /// <param name="metadata"></param>
        /// <returns></returns>
        public static string GetSongName(string[] ffmpegOut)
        {
            string artist = null;
            string title = null;

            foreach (string s in ffmpegOut)
            {
                if (s.ToLower().Contains("artist") && artist == null) artist = s.Split(':')[1].Trim();
                else if (s.ToLower().Contains("title") && title == null) title = s.Split(':')[1].Trim();

                if (artist != null && title != null) break;
            }

            return ((artist != null) && (title != null)) ? $"{artist} - {title}" : null;
        }

        /// <summary>
        /// Creates and writes meta file for song.
        /// </summary>
        /// <param name="path">Full path to the file</param>
        /// <param name="value">Value which will be written into it</param>
        public static void CreateMetaFile(string path, string value)
        {
            File.WriteAllText(path, value);
            File.SetAttributes(path, FileAttributes.Hidden);
        }

        /// <summary>
        /// Reads all track files in directory, and tries to get their names from ffmpeg -i output.
        /// It does it ONLY if the .mscmm meta file DOESN'T exist for file named the same way.
        /// </summary>
        /// <param name="folder">Folder directory</param>
        /// <returns></returns>
        public static async void GetSongsFromAll(string folder)
        {
            for (int i = 1; i < 99; i++)
            {
                if (File.Exists($"{Settings.GamePath}\\{folder}\\track{i}.ogg") && !File.Exists($"{Settings.GamePath}\\{folder}\\track{i}.mscmm"))
                {
                    ProcessStartInfo psi = new ProcessStartInfo("ffmpeg.exe", $"-i \"{Settings.GamePath}\\{folder}\\track{i}.ogg\"")
                    {
                        RedirectStandardError = true,
                        CreateNoWindow = true,
                        UseShellExecute = false
                    };

                    Process process = null;
                    await Task.Run(() => process = Process.Start(psi));

                    string[] ffmpegOut = process.StandardError.ReadToEnd().Split('\n');
                    string songName = GetSongName(ffmpegOut);

                    CreateMetaFile($"{Settings.GamePath}\\{folder}\\track{i}.mscmm", songName);
                }
            }
        }

        /// <summary>
        /// Removes all meta files.
        /// </summary>
        public static void RemoveAll(string folder)
        {
            DirectoryInfo di = new DirectoryInfo($"{Settings.GamePath}\\{folder}");
            FileInfo[] files = di.GetFiles().Where(f => f.Extension == ".mscmm").ToArray();

            foreach (FileInfo file in files)
                File.Delete($"{Settings.GamePath}\\{folder}\\{file.Name}");
        }
    }
}
