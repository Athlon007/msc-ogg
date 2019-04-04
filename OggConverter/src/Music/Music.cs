using System.IO;
using System.Diagnostics;
using System.Windows.Forms;

namespace OggConverter
{
    class Music
    {
        static Process process;

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

        public static void Stop()
        {
            try
            {
                if (process != null)
                {
                    process.Kill();
                    process.Close();
                    process = null;
                    return;
                }
            }
            catch { }
            
            // Used if process is still null, but ffplay is still running
            foreach (var process in Process.GetProcessesByName("ffplay"))
                process.Kill();
        }

        public static void Sort(string path)
        {
            Music.Stop();

            int skipped = 0;

            for (int i = 1; i < 99; i++)
            {
                if (!File.Exists($"{path}\\track{i}.ogg"))
                    skipped++;
                else
                {
                    File.Move($"{path}\\track{i}.ogg", $"{path}\\track{i - skipped}.ogg");
                    if (skipped != 0)
                        i -= skipped;

                    skipped = 0;
                }
            }

            Form1.instance.UpdateSongList();
        }

        public static void ChangeOrder(ListBox songList, string path, bool moveUp)
        {
            if (songList.SelectedIndex == -1) return;

            Music.Stop();

            int selectedIndex = songList.SelectedIndex;
            if (selectedIndex == 0) return;

            string oldName = songList.SelectedItem.ToString();
            string newName = $"track{selectedIndex + (moveUp ? 0 : 2)}.ogg";

            File.Move($"{path}\\{newName}", $"{path}\\trackTemp.ogg");
            File.Move($"{path}\\{oldName}", $"{path}\\{newName}");
            File.Move($"{path}\\trackTemp.ogg", $"{path}\\{oldName}");

            Form1.instance.UpdateSongList();
            songList.SelectedIndex = selectedIndex + (moveUp ? -1 : 1);
        }

        public static void MoveTo(string path, string selected, bool toCD)
        {
            Music.Stop();

            string moveFrom = toCD ? "CD" : "Radio";
            string moveTo = moveFrom == "CD" ? "Radio" : "CD";

            int newNumber = 1;

            for (int i = 1; File.Exists($"{path}\\{moveTo}\\track{i}.ogg"); i++)
                newNumber++;

            File.Move($"{path}\\{moveFrom}\\{selected}", $"{path}\\{moveTo}\\track{newNumber}.ogg");

            Form1.instance.UpdateSongList();
        }
    }
}
