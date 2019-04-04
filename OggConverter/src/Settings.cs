using Microsoft.Win32;

namespace OggConverter
{
    class Settings
    {
        public static bool RemoveMP3 { get => GetSettings.Bool("RemoveMP3", false); set => SetSettings.Bool("RemoveMP3", value); }
        public static bool CloseAfterConversion { get => GetSettings.Bool("CloseAfterConversion", false); set => SetSettings.Bool("CloseAfterConversion", value); }
        public static bool LaunchAfterConversion { get => GetSettings.Bool("LaunchAfterConversion", true); set => SetSettings.Bool("LaunchAfterConversion", value); }
        public static bool NoSteam { get => GetSettings.Bool("NoSteam", false); set => SetSettings.Bool("NoSteam", value); }
        public static bool NoUpdates { get => GetSettings.Bool("NoUpdates", false); set => SetSettings.Bool("NoUpdates", value); }  
        public static bool Logs { get => GetSettings.Bool("Logs", true); set => SetSettings.Bool("Logs", value); }
    }

    class SetSettings
    {
        internal static void Bool(string name, bool value)
        {
            using (RegistryKey Key = Registry.CurrentUser.CreateSubKey(@"SOFTWARE\MSCOGG", true))
            {
                Key.SetValue(name, value);
                Key.Close();
            }
        }
    }

    class GetSettings
    {
        internal static bool Bool(string name, bool defaultValue)
        {
            using (RegistryKey Key = Registry.CurrentUser.CreateSubKey(@"SOFTWARE\MSCOGG", true))
            {
                object value = Key.GetValue(name);

                if (value != null)
                    return value.Equals("True") ? true : false;
            }

            return defaultValue;
        }
    }
}
