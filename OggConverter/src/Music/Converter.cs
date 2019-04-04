using System.Linq;
using System.Threading.Tasks;
using System.IO;
using System.Windows.Forms;
using System.Diagnostics;

namespace OggConverter
{
    class Converter
    {
        static string conversionLog;
        public static string ConversionLog { get => conversionLog; set => conversionLog = value; }

        static int totalConversions;
        public static int TotalConversions { get => totalConversions; set => totalConversions = value; }

        public static int skipped;

        public static async Task ConvertFolder(string mscPath, string folder, int limit)
        {
            Form1.instance.Log += $"\nInitializing {folder} conversion...\n";
            string path = $"{mscPath}\\{folder}";

            DirectoryInfo d = new DirectoryInfo(path);
            string[] extensions = new[] { ".mp3", ".wav", ".aac", ".m4a", ".wma" };
            FileInfo[] Files
                = d.GetFiles()
                .Where(f => extensions.Contains(f.Extension.ToLower()))
                .ToArray();

            // If no files have been found - aborts the conversion
            if (Files.Length == 0)
            {
                Form1.instance.Log += $"\nCouldn't find any file to convert in {folder}";
                skipped++;
                return;
            }

            ConversionLog += $"{folder.ToUpper()}:\n";

            int inGame = 1; // Counts how many files there are in game + after that variable new files are named

            // Counting how many OGG files there are already
            for (int c = 1; File.Exists($"{path}\\track{c}.ogg"); c++)
                inGame++;

            // Starting the conversion of all found files
            foreach (FileInfo file in Files)
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

                Process process = new Process();
                process.StartInfo.RedirectStandardOutput = true;
                process.StartInfo.UseShellExecute = false;
                process.StartInfo.CreateNoWindow = true;
                // Setup executable and parameters
                process.StartInfo.FileName = "ffmpeg.exe";
                process.StartInfo.Arguments = $"-i \"{path}\\{file.Name}\" -acodec libvorbis \"{path}\\track{inGame}.ogg\"";

                process.Start();
                await Task.Run(() => process.WaitForExit());

                Form1.instance.Log += $"Finished {file.Name} as track{inGame}.ogg\n";

                if (Settings.RemoveMP3)
                    File.Delete(path + file.Name);

                ConversionLog += $"\"{file.Name}\" as \"track{inGame}.ogg\"\n";
                inGame++;
                TotalConversions++;
            }

            ConversionLog += "\n\n";
            Form1.instance.Log += $"\nConverted {TotalConversions} files in {folder}";
        }

        public static async Task ConvertFile(string filePath, string path, string folder, int limit)
        {
            if (!filePath.ContainsAny(".mp3", ".wav", ".aac", ".m4a", ".wma"))
            {
                if (Form1.instance != null)
                    Form1.instance.Log += $"\n\"{filePath.Substring(filePath.LastIndexOf('\\') + 1)}\" is not a music file so it will be skipped.";
                return;
            }

            int inGame = 1;
            if (Form1.instance != null)
                Form1.instance.Log += $"\n\nConverting \"{filePath.Substring(filePath.LastIndexOf('\\') + 1)}\"\n";
            //Counting how many OGG files there are already
            for (int c = 1; File.Exists($"{path}\\{folder}\\track{c}.ogg"); c++)
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

            Process process = new Process();
            process.StartInfo.RedirectStandardOutput = true;
            process.StartInfo.UseShellExecute = false;
            process.StartInfo.CreateNoWindow = true;
            
            // Setup executable and parameters
            process.StartInfo.FileName = "ffmpeg.exe";
            process.StartInfo.Arguments = $"-i \"{filePath}\" -acodec libvorbis \"{path}\\{folder}\\track{inGame}.ogg\"";
            process.Start();
            await Task.Run(() => process.WaitForExit());

            if (Form1.instance != null)
                Form1.instance.Log += $"Finished \"{filePath.Substring(filePath.LastIndexOf('\\') + 1)}\" as \"track{inGame}.ogg\"";
        }
    }
}
