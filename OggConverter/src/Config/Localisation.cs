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
using System;

namespace OggConverter
{
    class Localisation
    {
        static Dictionary<string, string> localeFileContent;
        public static Dictionary<string, string> LocaleFileContent { get => localeFileContent; set => localeFileContent = value; }

        /// <summary>
        /// Get translated by ID from localeFileContent.
        /// </summary>
        /// <param name="id"></param>
        /// <param name="args"></param>
        /// <returns></returns>
        public static string Get(string id, params object[] args)
        {
            string localFile = $"locales\\{Settings.Language}.po";

            // locales folder doens't exists? Create it now
            if (!Directory.Exists("locales"))
                Directory.CreateDirectory("locales");

            // locale file doesn't exists? Return the id
            if (!File.Exists(localFile))
                return String.Format(id, args);

            // Locale file doesn't contain the ID?
            // Return the id
            if (!LocaleFileContent.ContainsKey(id))
            {
                DumpError(id, args);
                return String.Format(id, args);
            }

            string output = LocaleFileContent[id];
            return output == "" ? String.Format(id, args) : String.Format(output, args);
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
            LocaleFileContent = new Dictionary<string, string>();

            // Reading locale file and skipping comments, and Poedit configurations to array
            string[] forbiddenElements = new string[] { "#", "Project-Id-Version: ", "POT-Creation-Date: ", "PO-Revision-Date: ",
            "Last-Translator: ", "Language-Team: ", "MIME-Version: ", "Content-Type: ", "Content-Transfer-Encoding: ", "X-Generator: ",
            "X-Poedit-Basepath: ", "Plural-Forms: ", "Language: "};

            string[] localeArray = File.ReadLines(localeFilePath)
                .Where(line => line.Length > 0).Where(line => line.StartsWith("msg") || line.StartsWith("\""))
                //.Where(line => !line.ContainsAny(forbiddenElements))
                .ToArray();

            // Reading array one by one
            for (int i = 0; i < localeArray.Length; i++)
            {
                // Saving starts from 'msgid' prefix. If the line doesn't start with msgid, continue.
                if (!localeArray[i].StartsWith("msgid")) continue;

                string id = ""; // msgid
                string str = ""; // msgstr

                // Reading from the start of msgid, until the msgstr is reached"
                for (int a = i; !localeArray[a].StartsWith("msgstr"); a++)
                {
                    string n = GetContent(localeArray[a]);
                    if (n == "") continue;
                    id += n;
                    i = a;
                }

                // Adding plus one to read the msgstr if the i isn't larger than localeArray length
                i += i < localeArray.Length - 1 ? 1 : 0;

                // Reading from the start of msgstr, until the msgid is reached
                for (int a = i; !localeArray[a].StartsWith("msgid"); a++)
                {
                    // Making sure that the 'a' won't be larger than localeArray length
                    if (a >= localeArray.Length - 1)
                    {
                        str += GetContent(localeArray[a]);
                        break;
                    }

                    string n = GetContent(localeArray[a]);
                    if (n == "") continue;
                    str += n;
                    i = a;
                }

                // Adding new dictionary item
                LocaleFileContent.Add(id, str);
            }
        }

        /// <summary>
        /// Removes quotation marks (") on the beginning and end and replacing \\n with \n
        /// </summary>
        /// <param name="msgid"></param>
        /// <returns></returns>
        static string GetContent(string msgid)
        {
            string output = msgid.Remove(0, msgid.IndexOf("\"") + 1);
            output = output.Remove(output.LastIndexOf("\""));
            output = output.Replace("\\n", "\n");
            return output;
        }

        /// <summary>
        /// Creates error log and saves it into log/locales_errors
        /// </summary>
        /// <param name="id"></param>
        /// <param name="args"></param>
        static void DumpError(string id, object[] args)
        {
            string errorMessage = $"\n@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@\n\nCouldn't find id in dictionary.\n\n" +
                $"ID: {id}\nLanguage: {Settings.Language}\n\nArgs:\n\n";

            foreach (object arg in args)
                errorMessage += arg.ToString() + ", ";

            errorMessage += "\n\n@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@";

            string date = $"{DateTime.Now.Date.Day}.{DateTime.Now.Date.Month}.{DateTime.Now.Date.Year} " +
                    $"{DateTime.Now.Hour.ToString()}.{DateTime.Now.Minute.ToString()}.{DateTime.Now.Second.ToString()}";

            Directory.CreateDirectory("log");
            Directory.CreateDirectory("log\\locale_errors");
            File.WriteAllText($"log\\locale_errors\\{date}.txt", errorMessage);

            Form1.instance.Log(String.Format("An error has occured with current localisation. The problem has been saved into {0}.txt", date));
        }
    }
}
