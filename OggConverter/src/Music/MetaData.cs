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
using System;
using System.Xml.Linq;

namespace OggConverter
{
    class MetaData
    {
        public static string XmlFilePath() { return $"{Settings.GamePath}\\{Form1.instance.CurrentFolder}\\songnames.xml"; }

        /// <summary>
        /// Retrieves song name from ffmpeg output
        /// </summary>
        /// <param name="ffmpegOut"></param>
        /// <returns></returns>
        public static string GetFromOutput(string[] ffmpegOut)
        {
            string artist = null;
            string title = null;

            foreach (string s in ffmpegOut)
            {
                if (s.ToLower().Contains("artist") && artist == null) artist = s.Split(':')[1].Trim();
                else if (s.ToLower().Contains("title") && title == null) title = s.Split(':')[1].Trim();

                // Artist and title found? Break out of the loop
                if (artist != null && title != null) break;
            }

            return ((artist != null) && (title != null)) ? $"{artist} - {title}" : null;
        }

        /// <summary>
        /// Reads all track files in directory, and tries to get their names from ffmpeg -i output.
        /// It does it ONLY if the .mscmm meta file DOESN'T exist for file named the same way.
        /// </summary>
        /// <param name="folder">Folder directory</param>
        /// <returns></returns>
        public static async void GetMetaFromAllSongs(string folder)
        {
            if (!Directory.Exists($"{Settings.GamePath}\\{folder}")) return;

            try
            {
                for (int i = 1; i <= 99; i++)
                {
                    if (File.Exists($"{Settings.GamePath}\\{folder}\\track{i}.ogg") && !IsInDatabase($"track{i}.ogg"))
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
                        string songName = GetFromOutput(ffmpegOut);

                        AddOrEdit($"track{i}", songName);
                    }
                }
            }
            catch (Exception ex)
            {
                ErrorMessage err = new ErrorMessage(ex);
                err.ShowDialog();
            }
        }

        /// <summary>
        /// Gets all song names from old .mscmm files and removes them
        /// </summary>
        /// <param name="folder">Working folder</param>
        public static void ConvertFromMscmm(string folder)
        {
            if (Settings.DisableMetaFiles) return;

            try
            {
                string xFilePath = $"{Settings.GamePath}//{folder}//songnames.xml";
                XDocument doc;
                doc = File.Exists(xFilePath) && File.ReadAllText(xFilePath) != "" ? XDocument.Load(xFilePath) : new XDocument(new XElement("songs"));

                DirectoryInfo di = new DirectoryInfo($"{Settings.GamePath}\\{folder}");
                FileInfo[] fi = di.GetFiles("*.mscmm");
                foreach (var file in fi)
                {
                    string name = Path.GetFileNameWithoutExtension(file.Name);
                    string value = File.ReadAllText(file.FullName);

                    doc.Root.Add(new XElement("songs",
                            new XAttribute("name", name), new XAttribute("value", value)
                            ));

                    File.Delete(file.FullName);
                }

                doc.Save(xFilePath);
            }
            catch (Exception ex)
            {
                ErrorMessage err = new ErrorMessage(ex);
                err.ShowDialog();
            }
        }

        /// <summary>
        /// Returns the song name from database.
        /// </summary>
        /// <param name="name">Name of song</param>
        /// <returns></returns>
        public static string GetName(string name)
        {
            try
            {
                XDocument doc = XDocument.Load(XmlFilePath());
                var attribute = doc.Root.Descendants("songs").SingleOrDefault(e => (string)e.Attribute("name") == name);
                return attribute == null ? name : attribute.Attribute("value").Value;
            }
            catch (Exception ex)
            {
                ErrorMessage err = new ErrorMessage(ex);
                err.ShowDialog();
                return name;
            }
        }

        /// <summary>
        /// Checks if value appears in the database
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        public static bool IsInDatabase(string name)
        {
            try
            {
                XDocument doc = XDocument.Load(XmlFilePath());
                return doc.Root.Descendants("songs").SingleOrDefault(e => (string)e.Attribute("name") == name) != null;
            }
            catch (Exception ex)
            {
                ErrorMessage err = new ErrorMessage(ex);
                err.ShowDialog();
                return false;
            }
        }

        /// <summary>
        /// Creates new attribute, or edits currently existing one if there's one already.
        /// </summary>
        /// <param name="name"></param>
        /// <param name="value"></param>
        public static void AddOrEdit(string name, string value)
        {
            try
            {
                XDocument doc = XDocument.Load(XmlFilePath());
                var attribute = doc.Root.Descendants("songs").SingleOrDefault(e => (string)e.Attribute("name") == name);
                if (attribute == null)
                {
                    doc.Root.Add(new XElement("songs",
                            new XAttribute("name", name), new XAttribute("value", value)
                            ));
                }
                else
                {
                    attribute.Attribute("value").Value = value;
                }

                doc.Save(XmlFilePath());
            }
            catch (Exception ex)
            {
                ErrorMessage err = new ErrorMessage(ex);
                err.ShowDialog();
            }
        }

        /// <summary>
        /// Removes the attribute from the database.
        /// </summary>
        /// <param name="name"></param>
        public static void Remove(string name)
        {
            try
            { 
                XDocument doc = XDocument.Load(XmlFilePath());
                doc.Root.Descendants("songs").SingleOrDefault(e => (string)e.Attribute("name") == name).Remove();
                doc.Save(XmlFilePath());
            }
            catch (Exception ex)
            {
                ErrorMessage er = new ErrorMessage(ex);
                er.ShowDialog();
            }
        }

        /// <summary>
        /// Wipes Xml database
        /// </summary>
        public static void RemoveAll(string folder)
        {
            try
            {
                if (File.Exists($"{Settings.GamePath}\\{folder}\\songnames.xml"))
                    File.Delete($"{Settings.GamePath}\\{folder}\\songnames.xml");              
            }
            catch (Exception ex)
            {
                ErrorMessage err = new ErrorMessage(ex);
                err.ShowDialog();
            }
        }

        /// <summary>
        /// Moves song name from one database to another
        /// </summary>
        /// <param name="sourceFolder">Source folder in which the source database is located</param>
        /// <param name="name">Name in source db</param>
        /// <param name="destinationFolder">Destination folder of database</param>
        /// <param name="newName">New name</param>
        public static void MoveToDatabase(string sourceFolder, string name, string destinationFolder, string newName)
        {
            try
            {
                XDocument src = XDocument.Load($"{Settings.GamePath}\\{sourceFolder}\\songnames.xml");
                XDocument dst = XDocument.Load($"{Settings.GamePath}\\{destinationFolder}\\songnames.xml");

                string value = src.Root.Descendants("songs").SingleOrDefault(e => (string)e.Attribute("name") == name).Attribute("value").Value;
                dst.Root.Add(new XElement("songs", new XAttribute("name", newName), new XAttribute("value", value)));
                dst.Save($"{Settings.GamePath}\\{destinationFolder}\\songnames.xml");
                Remove(name);
            }
            catch (Exception ex)
            {
                ErrorMessage er = new ErrorMessage(ex);
                er.ShowDialog();
            }
        }

        /// <summary>
        /// Changes name of file saved in database
        /// </summary>
        /// <param name="oldName"></param>
        /// <param name="newName"></param>
        public static void ChangeFile(string oldName, string newName)
        {
            try
            {
                XDocument doc = XDocument.Load(XmlFilePath());
                doc.Root.Descendants("songs").SingleOrDefault(e => (string)e.Attribute("name") == oldName).Attribute("name").Value = newName;
                doc.Save(XmlFilePath());
            }
            catch (Exception ex)
            {
                ErrorMessage er = new ErrorMessage(ex);
                er.ShowDialog();
            }
        }
    }
}
