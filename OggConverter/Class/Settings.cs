using Microsoft.Win32;

namespace OggConverter
{
    class Settings
    {
        public static bool RemoveMP3 { get; set; }

        public Settings()
        {
            RemoveMP3 = Load("RemoveMP3");
        }

        bool Load(string KeyName)
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
    }

    public class ChangeSettings
    {
        public static void ChangeBool(string KeyName)
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
        }
    }
}
