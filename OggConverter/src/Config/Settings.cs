﻿// MSC Music Manager
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

using Microsoft.Win32;
using System;
using System.IO;

namespace OggConverter
{
    class Settings
    {
        /// <summary>
        /// Default MSCMM registry key
        /// </summary>
#if DEBUG
        const string key = "SOFTWARE\\MSCOGG_DEBUG";
#else
        const string key = "SOFTWARE\\MSCOGG";
#endif

        /// <summary>
        /// Should the game be started without steam?
        /// </summary>
        public static bool NoSteam { get => Get("NoSteam", false); set => Set("NoSteam", value); }

        /// <summary>
        /// Should the tool NOT check for update?
        /// </summary>
        public static bool NoUpdates { get => Get("NoUpdates", false); set => Set("NoUpdates", value); }

        /// <summary>
        /// Should the tool also check preview update channel?
        /// </summary>
        public static bool Preview { get => Get("Preview", false); set => Set("Preview", value); }

        /// <summary>
        /// Should the tool dump the crash logs to LOG folder?
        /// </summary>
        public static bool Logs { get => Get("Logs", true); set => Set("Logs", value); }

        /// <summary>
        /// Should the tool automatically sort songs after moving/removing song?
        /// </summary>
        public static bool AutoSort { get => Get("AutoSort", true); set => Set("AutoSort", value); }

        /// <summary>
        /// Should the tool save all files operations to history.txt?
        /// </summary>
        public static bool History { get => Get("History", true); set => Set("History", value); }

        /// <summary>
        /// Forces MSCMM to use old song name reading, instead of one using metafiles
        /// </summary>
        public static bool DisableMetaFiles { get => Get("DisableMetaFiles", false); set => Set("DisableMetaFiles", value); }

        /// <summary>
        /// My Summer Car directory path
        /// </summary>
        public static string GamePath { get => Get("MSC Path", GetMSCPath()); set => Set("MSC Path", value); }

        /// <summary>
        /// How often youtube-dl updates are being checked for
        /// <para />
        /// <para>0 - Upon launch</para>
        /// <para>1 - Daily</para>
        /// <para>2 - Once a week</para>
        /// <para>3 - Once a month</para>
        /// <para>Any other - Never</para>
        /// </summary>
        public static int YouTubeDlUpdateFrequency
        {
            get => Get("YouTubeDlUpdateFrequency", 1);
            set => Set("YouTubeDlUpdateFrequency", value);
        }

        /// <summary>
        /// Stores the language file name set by user
        /// </summary>
        public static string Language { get => Get("Language", "English (UK)"); set => Set("Language", value); }

        /// <summary>
        /// If true, uppon any conversion the 
        /// </summary>
        public static bool ShowFfmpegOutput { get => Get("ShowFfmpegOutput", false); set => Set("ShowFfmpegOutput", value); }

        /// <summary>
        /// If true, the program won't show the question, when user is converting songs over folder limit.
        /// </summary>
        public static bool IgnoreLimitations { get => Get("IgnoreLimitations", false); set => Set("IgnoreLimitations", value); }

        /// <summary>
        /// Quality of downloaded songs with youtube-dl
        /// <para />
        /// <para>0 - Best (0 in ytdl)</para>
        /// <para>1 - Average (5 in ytdl)</para>
        /// <para>2 - Compressed (9 in ytdl)</para>
        /// </summary>
        public static int YoutubeDlDownloadQuality { get => Get("YoutubeDlDownloadQuality", 1); set => Set("YoutubeDlDownloadQuality", value); }

        /// <summary>
        /// Path to the Audacity
        /// </summary>
        public static string AudacityPath { get => Get("AudacityPath", ""); set => Set("AudacityPath", value); }

        /// <summary>
        /// If true, the program will convert the audio frequency of file to 22050 Hz
        /// </summary>
        public static bool UseRecommendedFrequency { get => Get("UseRecommendedFrequency", false); set => Set("UseRecommendedFrequency", value); }

        /// <summary>
        /// Font used in cover art text
        /// </summary>
        public static string CoverArtFont { get => Get("CoverArtFont", "Arial"); set => Set("CoverArtFont", value); }

        /// <summary>
        /// If true, the program will convert the song file to mono channel.
        /// </summary> 
        public static bool ConvertToMono { get => Get("ConvertToMono", false); set => Set("ConvertToMono", value); }

        /// <summary>
        /// Enabled "F5" button to refresh the window localisation
        /// </summary>
        public static bool TranslatorMode { get => Get("TranslatorMode", false); set => Set("TranslatorMode", value); }

        /// <summary>
        /// If true, the program will generate locale errors.
        /// </summary>
        public static bool CreateLocaleErrorLogs { get => Get("CreateLocaleErrorLogs", true); set => Set("CreateLocaleErrorLogs", value); }

        //////////////////////////////////////////////
        // THESE SETTINGS CAN'T BE CHANGED BY USER! //
        //////////////////////////////////////////////

        /// <summary>
        /// Stores last build used.
        /// </summary>
        public static int LatestVersion { get => Get("LatestVersion", 0); set => Set("LatestVersion", value); }

        /// <summary>
        /// Disables or hides features (used for screenshots)
        /// </summary>
        public static bool DemoMode { get => Get("DemoMode", false); set => Set("DemoMode", value); }

        /// <summary>
        /// Stores what was the last crash log file
        /// </summary>
        public static string LastCrashLogFile { get => Get("LastCrashLogFile", ""); set => Set("LastCrashLogFile", value); }

        /// <summary>
        /// Stores the last time when the MSCMM checked for youtube-dl update
        /// </summary>
        public static DateTime LastYTDLUpdateCheck
        {
            get => Get("LastYTDLUpdateCheck", new DateTime(1970, 1, 1, 1, 0, 0));
            set => Set("LastYTDLUpdateCheck", value);
        }

        /// <summary>
        /// Is set to true, if the version currently installed is from Preview ring
        /// </summary>
        public static bool ThisPreview { get => Get("ThisPreview", false); set => Set("ThisPreview", value); }


        /// <summary>
        /// Gets the value of setting from registry
        /// </summary>
        /// <param name="name">Name of value</param>
        /// <param name="defaultValue">Default value</param>
        /// <returns>Returns the value (either as string, int or bool)</returns>
        internal static dynamic Get<Object>(string name, Object defaultValue)
        {
            using (RegistryKey Key = Registry.CurrentUser.CreateSubKey(key))
                return Convert.ChangeType(Key.GetValue(name, defaultValue), typeof(Object));
        }

        /// <summary>
        /// Saves value into the registry
        /// </summary>
        /// <param name="name">Name of value</param>
        /// <param name="value">Value to set</param>
        internal static void Set<T>(string name, T value)
        {
            if (value != null)
                using (RegistryKey Key = Registry.CurrentUser.CreateSubKey(key))
                    Key.SetValue(name, value);
        }

        /// <summary>
        /// Removes all settings.
        /// </summary>
        public static void WipeAll()
        {
            Registry.CurrentUser.DeleteSubKeyTree(key);
            LatestVersion = Updates.version;
        }

        /// <summary>
        /// Checks registry key validity - if it exists and if the game path is correct.
        /// </summary>
        /// <returns></returns>
        public static bool AreSettingsValid()
        {
            GamePath = GetMSCPath();

            if (String.IsNullOrEmpty(GamePath))
                return false;

            if (!Directory.Exists(GamePath))
                return false;

            return true;
        }

        /// <summary>
        /// Is set to true, after the settings have been checked succesfully
        /// </summary>
        public static bool SettingsChecked { get; set; }

        // If you're reading this - I hope you had a better day than me fixing that fucking error fixed in 2.5.2...
        // ~ Athlon

        /// <summary>
        /// Tries to find My Summer Car folder.
        /// </summary>
        /// <returns></returns>
        public static string GetMSCPath()
        {
            // We're checking MSCMM registry key, if it's already set
            using (RegistryKey Key = Registry.CurrentUser.OpenSubKey(key, true))
            {
                if (Key != null && Key.GetValue("MSC Path") != null)
                {
                    string path = Key.GetValue("MSC Path", "").ToString();
                    if (Directory.Exists(path))
                        return Key.GetValue("MSC Path", "").ToString();
                }
            }

            // My Summer Car path is not saved. Now we're trying to find it in Steam root folder
            string steamFolder = "";
            if (Registry.CurrentUser.OpenSubKey(@"SOFTWARE\Valve\Steam") != null)
            {
                using (RegistryKey Key = Registry.CurrentUser.OpenSubKey(@"SOFTWARE\Valve\Steam"))
                {
                    steamFolder = Key.GetValue("SteamPath").ToString();
                    steamFolder = steamFolder.Replace("/", "\\");
                }
            }
            
            // Check only if steamFolder is not empty
            if (steamFolder != "")
            {
                // MSC is installed in root Steam folder
                if (File.Exists($"{steamFolder}\\steamapps\\common\\My Summer Car\\mysummercar.exe"))
                {
                    GamePath = $"{steamFolder}\\steamapps\\common\\My Summer Car";
                    return $"{steamFolder}\\steamapps\\common\\My Summer Car";
                }

                // MSC not found - gotta open config.vdf file and browse all libraries for MSC folder...
                // Dumping config.vdf to string array
                string[] config = File.ReadAllText($"{steamFolder}\\config\\config.vdf").Split('\n');
                // Creating list in which all BaseInstallFolder values will be stored
                foreach (string line in config)
                {
                    if (line.Contains("BaseInstallFolder"))
                    {
                        string path = line.Substring(line.LastIndexOf('\t')).Replace("\"", "").Replace("\\\\", "\\").Trim();
                        path += "\\steamapps\\common\\My Summer Car";
                        if (Directory.Exists(path))
                        {
                            GamePath = path;
                            return path;
                        }
                    }
                }
            }

            // Still haven't found? User will be asked to select it manually. Return null
            return null;
        }
    }
}
