using Microsoft.Win32;

namespace OggConverter
{
    class Settings
    {
        public static bool RemoveMP3 { get; set; }
        public static bool CloseAfterConversion { get; set; }
        public static bool LaunchAfterConversion { get; set; }
        public static bool NoSteam { get; set; }
        public static bool NoUpdates { get; set; }

        public Settings()
        {
            RemoveMP3 = Bool("RemoveMP3");
            CloseAfterConversion = Bool("CloseAfterConversion");
            LaunchAfterConversion = Bool("LaunchAfterConversion");
            NoSteam = Bool("NoSteam");
            NoUpdates = Bool("NoUpdates");
        }

        bool Bool(string KeyName)
        {
            using (RegistryKey Key = Registry.CurrentUser.CreateSubKey(@"SOFTWARE\MSCOGG", true))
            {
                object Value = Key.GetValue(KeyName);

                if (Value != null)
                {
                    //bool
                    if (Value.Equals("true"))
                    {
                        return true;
                    }
                    else if (Value.Equals("false"))
                    {
                        return false;
                    }
                }
                else
                {
                    return false;
                }
            }
            return false;
        } 

        string Variable(string KeyName)
        {
            using (RegistryKey Key = Registry.CurrentUser.CreateSubKey(@"SOFTWARE\MSCOGG", true))
            {
                object Value = Key.GetValue(KeyName);

                if (Value != null)
                {
                    return Value.ToString();
                }
                else
                    return "None";
            }
        }
    }

    public class ChangeSettings
    {
        public static void Bool(string KeyName)
        {
            using (RegistryKey Key = Registry.CurrentUser.CreateSubKey(@"SOFTWARE\MSCOGG", true))
            {
                object Value = Key.GetValue(KeyName);

                if (Value != null)
                {
                    //bool
                    if (Value.Equals("true"))
                    {
                        Key.SetValue(KeyName, "false");
                    }
                    else if (Value.Equals("false"))
                    {
                        Key.SetValue(KeyName, "true");
                    }
                }
                else
                {
                    Key.SetValue(KeyName, "true");
                }
            }
            new Settings();
        }

        // Always sets Bool to false
        public static void BoolFalse(string KeyName)
        {
            using (RegistryKey Key = Registry.CurrentUser.CreateSubKey(@"SOFTWARE\MSCOGG", true))
            {
                object Value = Key.GetValue(KeyName);
                Key.SetValue(KeyName, "false");
            }
            new Settings();
        }

        public static void Variable(string KeyName, string val)
        {
            using (RegistryKey Key = Registry.CurrentUser.CreateSubKey(@"SOFTWARE\MSCOGG", true))
            {
                object Value = Key.GetValue(KeyName);
                Key.SetValue(KeyName, val);
            }
            new Settings();
        }
    }
}
