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

namespace OggConverter
{
    class MetaDatas
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

        public static void CreateMetaFile(string name, string value)
        {
            File.WriteAllText(name, value);
            File.SetAttributes(name, FileAttributes.Hidden);
        }
    }
}
