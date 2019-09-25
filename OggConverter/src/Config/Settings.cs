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
        // Default MSCMM registry key
#if DEBUG
        const string key = "SOFTWARE\\MSCOGG_DEBUG";
#else
        const string key = "SOFTWARE\\MSCOGG";
#endif

        // Should MSCMM remove source song files after conversion?
        public static bool RemoveMP3 { get => Get("RemoveMP3", true); set => Set("RemoveMP3", value); }

        // Should the game be started without steam?
        public static bool NoSteam { get => Get("NoSteam", false); set => Set("NoSteam", value); }

        // Should the tool NOT check for update?
        public static bool NoUpdates { get => Get("NoUpdates", false); set => Set("NoUpdates", value); }

        // Should the tool also check preview update channel?
        public static bool Preview { get => Get("Preview", false); set => Set("Preview", value); }

        // Should the tool dump the crash logs to LOG folder?
        public static bool Logs { get => Get("Logs", true); set => Set("Logs", value); }

        // Should the tool automatically sort songs after moving/removing song?
        public static bool AutoSort { get => Get("AutoSort", true); set => Set("AutoSort", value); }

        // Should the tool save all files operations to history.txt?
        public static bool History { get => Get("History", true); set => Set("History", value); }

        /// Forces MSCMM to use old song name reading, instead of one using metafiles (planned to be removed in future updates)
        public static bool DisableMetaFiles { get => Get("DisableMetaFiles", false); set => Set("DisableMetaFiles", value); }

        // My Summer Car directory path
        public static string GamePath { get => Get("MSC Path", GetMSCPath()); set => Set("MSC Path", value); }

        // How often youtube-dl updates are being checked for
        //
        // 0 - Upon launch
        // 1 - Daily
        // 2 - Once a week
        // 3 - Once a month
        public static int YouTubeDlUpdateFrequency
        {
            get => Get("YouTubeDlUpdateFrequency", 1);
            set => Set("YouTubeDlUpdateFrequency", value);
        }

        /// Stores the set language by user
        public static string Language { get => Get("Language", "english"); set => Set("Language", value); }

        //////////////////////////////////////////////
        // THESE SETTINGS CAN'T BE CHANGED BY USER! //
        //////////////////////////////////////////////

        // Stores last build used.
        public static int LatestVersion { get => Get("LatestVersion", 0); set => Set("LatestVersion", value); }

        // Disables or hides features (used mostly for screenshots)
        public static bool DemoMode { get => Get("DemoMode", false); set => Set("DemoMode", value); }

        // Stores what was the last crash log file
        public static string LastCrashLogFile { get => Get("LastCrashLogFile", ""); set => Set("LastCrashLogFile", value); }

        // Stores the last time when the MSCMM checked for youtube-dl update
        public static DateTime LastYTDLUpdateCheck
        {
            get => Get("LastYTDLUpdateCheck", new DateTime(1970, 1, 1, 1, 0, 0));
            set => Set("LastYTDLUpdateCheck", value);
        }


        /// <summary>
        /// Gets the value of setting from registry
        /// </summary>
        /// <param name="name">Name of value</param>
        /// <param name="defaultValue">Default value</param>
        /// <returns>Returns the value (either as string, int or bool)</returns>
        static dynamic Get<Object>(string name, Object defaultValue)
        {
            using (RegistryKey Key = Registry.CurrentUser.CreateSubKey(key))
                return Convert.ChangeType(Key.GetValue(name, defaultValue), typeof(Object));
        }

        /// <summary>
        /// Saves value into the registry
        /// </summary>
        /// <param name="name">Name of value</param>
        /// <param name="value">Value to set</param>
        static void Set<T>(string name, T value)
        {
            if (value != null)
                using (RegistryKey Key = Registry.CurrentUser.CreateSubKey(key))
                    Key.SetValue(name, value);
        }

        /// <summary>
        /// Removes all settings.
        /// </summary>
        public static void WipeAll() { Registry.CurrentUser.DeleteSubKeyTree(key); LatestVersion = Updates.version; }

        /// <summary>
        /// Checks registry key validity - if it exists and if the game path is correct.
        /// </summary>
        /// <returns></returns>
        public static bool AreSettingsValid()
        {
            GamePath = GetMSCPath();
            //return GamePath == null || GamePath != "invalid";
            if (GamePath == null || GamePath == "")
                return false;

            if (GamePath == "invalid")
                return false;

            return true;
        }

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
                    return Key.GetValue("MSC Path", "invalid").ToString();
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
