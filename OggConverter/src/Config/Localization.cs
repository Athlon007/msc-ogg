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

using System.Collections.Generic;
using System.Linq;
using System.IO;

namespace OggConverter
{
    class Localization
    {
        static Dictionary<string, string> localeFileContent;

        public static string Get (string id)
        {
            string localFile = $"locales\\{Settings.Language}.po";

            if (!Directory.Exists("locales"))
                Directory.CreateDirectory("locales");

            if (!File.Exists(localFile))
                return id;

            if (localeFileContent[id] == null)
                return "NULL";

            string output = localeFileContent[id];
            return output == "" ? id : output;
        }


        /// <summary>
        /// Loads the locale file from .po file
        /// </summary>
        public static void LoadLocaleFile()
        {
            if (!Directory.Exists("locales"))
                Directory.CreateDirectory("locales");

            string localeFilePath = $"locales\\{Settings.Language}.po";

            if (!File.Exists(localeFilePath))
                return;

            // Wipes the dictionary
            localeFileContent = new Dictionary<string, string>();

            // Reading locale file and skipping comments, and Poedit configurations to array
            string[] localeArray = File.ReadLines(localeFilePath)
                .Where(line => !line.Contains("#"))
                .Where(line => line.Length > 0).Where(line => line.StartsWith("msg") || line.StartsWith("\""))
                .Where(line => !line.Contains("Project-Id-Version"))
                .Where(line => !line.Contains("POT-Creation-Date"))
                .Where(line => !line.Contains("PO-Revision-Date"))
                .Where(line => !line.Contains("Last-Translator"))
                .Where(line => !line.Contains("Language-Team:"))
                .Where(line => !line.Contains("MIME-Version"))
                .Where(line => !line.Contains("Content-Type"))
                .Where(line => !line.Contains("Content-Transfer-Encoding"))
                .Where(line => !line.Contains("X-Generator"))
                .Where(line => !line.Contains("X-Poedit-Basepath"))
                .Where(line => !line.Contains("Plural-Forms"))
                .Where(line => !line.Contains("Language: "))
                .ToArray();

            // Reading array one by one
            for (int i = 0; i < localeArray.Length ; i++)
            {
                // Saving starts from 'msgid' prefix
                if (localeArray[i].StartsWith("msgid"))
                {
                    string id = ""; // msgid
                    string str = ""; // msgstr

                    // Reading from the start of msgid, until the msgstr is reached"
                    for (int a = i; !localeArray[a].StartsWith("msgstr"); a++)
                    {
                        string n = localeArray[a];

                        // Removing quotation marks (") on the beginning and end and replacing \\n with \n
                        n = n.Remove(0, n.IndexOf("\"") + 1);
                        n = n.Remove(n.LastIndexOf("\""));
                        n = n.Replace("\\n", "\n");
                        if (n == "") continue;
                        id += n;
                        i = a;
                    }

                    // Adding plus one to read the msgstr if the i isn't larger than localeArray length
                    i += i < localeArray.Length - 1 ? 1 : 0;

                    // Reading from the start of msgstr, until the msgid is reached
                    for (int a = i; !localeArray[a].StartsWith("msgid"); a++)
                    {
                        string n = localeArray[a];
                        n = n.Remove(0, n.IndexOf("\"") + 1);
                        n = n.Remove(n.LastIndexOf("\""));
                        n = n.Replace("\\n", "\n");
                        if (n == "") continue;
                        str += n;
                        i = a;

                        // Making sure that the a won't be larger than localeArray length
                        if (a >= localeArray.Length - 1)
                            break;
                    }

                    // Adding new dictionary item
                    localeFileContent.Add(id, str);
                }
            }

            //System.Windows.Forms.MessageBox.Show(localeFileContent["Language"]);
        }
    }
}
