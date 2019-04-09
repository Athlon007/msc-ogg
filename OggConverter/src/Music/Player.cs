using System.IO;
using System.Diagnostics;
using System.Windows.Forms;

namespace OggConverter
{
    class Player
    {
        static Process process;

        /// <summary>
        /// Plays selected song using ffplay.exe
        /// </summary>
        /// <param name="path">Path to the file</param>
        public static void Play(string path)
        {
            if (!File.Exists("ffplay.exe"))
            {
                MessageBox.Show("FFplay.exe is missing! Try to re-download MSC Music Manager.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
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
            Form1.instance.LabNowPlaying.Visible = false;

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
        /// <param name="mscPath">My Summer Car path <B>WITH</B> Radio/CD</param>
        public static void Sort(string mscPath)
        {
            if (Download.downloadingNow)
            {
                MessageBox.Show("Song is now being downloaded.", "Stop", MessageBoxButtons.OK, MessageBoxIcon.Stop);
                return;
            }

            if (Converter.conversionInProgress)
            {
                MessageBox.Show("Conversion is in progress.", "Prohibited", MessageBoxButtons.OK, MessageBoxIcon.Hand);
                return;
            }

            Player.Stop();

            int skipped = 0;

            for (int i = 1; i < 99; i++)
            {
                // File doesn't exist? Skipping the value
                if (!File.Exists($"{mscPath}\\track{i}.ogg"))
                    skipped++;
                else
                {
                    // Waiting for file to be free
                    while (!IsFileReady($"{mscPath}\\track{i}.ogg")) { }

                    // Moving the file
                    File.Move($"{mscPath}\\track{i}.ogg", $"{mscPath}\\track{i - skipped}.ogg");

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

            if (Download.downloadingNow)
            {
                MessageBox.Show("Song is now being downloaded.", "Stop", MessageBoxButtons.OK, MessageBoxIcon.Stop);
                return;
            }

            if (Converter.conversionInProgress)
            {
                MessageBox.Show("Conversion is in progress.", "Prohibited", MessageBoxButtons.OK, MessageBoxIcon.Hand);
                return;
            }

            Player.Stop();

            int selectedIndex = songList.SelectedIndex;
            if (selectedIndex == 0) return;

            string oldName = songList.SelectedItem.ToString();
            string newName = $"track{selectedIndex + (moveUp ? 0 : 2)}.ogg";

            // Waiting for file to be free
            while (!IsFileReady($"{mscPath}\\{oldName}")) { }

            // Moving file that now uses the current new name
            //
            // For instance: we're moving track2 to track1.
            // So we first move track1 out of the place and renaming it to trackTemp IF track1 EXISTS
            if (File.Exists($"{mscPath}\\{newName}"))
                File.Move($"{mscPath}\\{newName}", $"{mscPath}\\trackTemp.ogg");

            // Now we're moving the file that we want to actually move
            File.Move($"{mscPath}\\{oldName}", $"{mscPath}\\{newName}");

            // Finally we move the file that we set as temp (if it exists)
            if (File.Exists($"{mscPath}\\trackTemp.ogg"))
                File.Move($"{mscPath}\\trackTemp.ogg", $"{mscPath}\\{oldName}");

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
            if (Download.downloadingNow)
            {
                MessageBox.Show("Song is now being downloaded.", "Stop", MessageBoxButtons.OK, MessageBoxIcon.Stop);
                return;
            }

            if (Converter.conversionInProgress)
            {
                MessageBox.Show("Conversion is in progress.", "Prohibited", MessageBoxButtons.OK, MessageBoxIcon.Hand);
                return;
            }

            Player.Stop();

            string moveFrom = toCD ? "CD" : "Radio";
            string moveTo = moveFrom == "CD" ? "Radio" : "CD";

            int newNumber = 1;

            for (int i = 1; File.Exists($"{mscPath}\\{moveTo}\\track{i}.ogg"); i++)
                newNumber++;

            // Waiting for file to be free
            while (!IsFileReady($"{mscPath}\\{moveFrom}\\{selected}")) { }

            File.Move($"{mscPath}\\{moveFrom}\\{selected}", $"{mscPath}\\{moveTo}\\track{newNumber}.ogg");

            Form1.instance.UpdateSongList();
        }

        public static bool IsFileReady(string filename)
        {
            // If the file can be opened for exclusive access it means that the file
            // is no longer locked by another process.
            try
            {
                using (FileStream inputStream = File.Open(filename, FileMode.Open, FileAccess.Read, FileShare.None))
                    return inputStream.Length > 0;
            }
            catch
            {
                return false;
            }
        }
    }
}
