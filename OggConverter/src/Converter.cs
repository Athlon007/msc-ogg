﻿using System.Linq;
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

        public static async Task ConvertFiles(string mscPath, string folder, int limit)
        {
            int inGame = 0;

            Form1.instance.Log += $"\n\nInitializing {folder} conversion...\n";
            ConversionLog += $"{folder.ToUpper()}:\n";
            string path = $"{mscPath}\\{folder}";
            DirectoryInfo d = new DirectoryInfo(path + "\\");
            string[] extensions = new[] { ".mp3", ".wav", ".aac", ".m4a", ".wma" };
            FileInfo[] Files
                = d.GetFiles()
                .Where(f => extensions.Contains(f.Extension.ToLower()))
                .ToArray();

            //Counting how many OGG files there are already
            for (int c = 0; File.Exists($"{path}\\track{c}.ogg"); c++)
                inGame++;

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
                        Form1.instance.Log += "\nAborted CD conversion.";
                        break;
                    }
                }

                Form1.instance.Log += "Converting " + file.Name + "\n";

                Process process = new Process();
                process.StartInfo.RedirectStandardOutput = true;
                process.StartInfo.UseShellExecute = false;
                process.StartInfo.CreateNoWindow = true;
                // Setup executable and parameters
                process.StartInfo.FileName = "ffmpeg.exe";
                process.StartInfo.Arguments = $"-i \"{path}\\{file.Name}\" -acodec libvorbis \"{path}\\track{inGame}.ogg\"";

                process.Start();
                await Task.Run(() => process.WaitForExit());

                Form1.instance.Log += file.Name + " as track" + inGame + ".ogg\n";

                if (Settings.RemoveMP3)
                    File.Delete(path + file.Name);

                ConversionLog += "\nFinished \"" + file.Name + "\" as \"track" + inGame + ".ogg\"\n";
                inGame++;
                TotalConversions++;
            }

            Form1.instance.Log += $"\nConverted {TotalConversions} files in {folder}.";
        }
    }
}
