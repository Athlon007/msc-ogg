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
using System.Windows.Forms;

namespace OggConverter
{
    class CustomStartGame
    {
        /// <summary>
        /// Shuffle all folders in MSC and start the game.
        /// </summary>
        public static void Play()
        {
            // List of all possible folders
            string[] folders = new string[] { "Radio", "CD", "CD1", "CD2", "CD3" };

            // Shuffle these folders
            foreach (string folder in folders)
            {
                if (Directory.Exists($"{Settings.GamePath}\\{folder}"))
                {
                    MetaData.AlternateFolder = folder;
                    Player.Shuffle(folder);
                }
            }

            // Start the game
            Utilities.LaunchGame();
            Application.Exit();
        }
    }
}
