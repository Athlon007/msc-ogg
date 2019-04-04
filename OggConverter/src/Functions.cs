namespace OggConverter
{
    static class Functions
    {
        public static bool ContainsAny(this string file, params string[] extensions)
        {
            foreach (string extension in extensions)
                if (file.Contains(extension))
                    return true;

            return false;
        }
    }
}
