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

using System.Diagnostics;

namespace OggConverter
{
    class MetaDatas
    {
        public static string GetSongName(string filePath)
        {
            ProcessStartInfo psi = new ProcessStartInfo("ffmpeg.exe", $"-i \"{filePath}\"")
            {
                UseShellExecute = false,
                RedirectStandardError = true,
                CreateNoWindow = true
            };
            var proc = Process.Start(psi);

            string[] metadata = proc.StandardError.ReadToEnd().Split('\n');

            string artist = null;
            string title = null;

            foreach (string s in metadata)
            {
                if (s.Contains("ARTIST"))
                    artist = s.Split(':')[1].Trim();
                else if (s.Contains("TITLE"))
                    title = s.Split(':')[1].Trim();
            }

            return ((artist != null) && (title != null)) ? $"{artist} - {title}" : null;
        }
    }
}
