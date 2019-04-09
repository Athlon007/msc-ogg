using System.Diagnostics;
using System.Threading;
using System.Threading.Tasks;
using System.Reflection;
using System;

namespace OggConverter
{
    static class Functions
    {
        /// <summary>
        /// Checks if string contains any extension in the file name.
        /// </summary>
        /// <param name="file">File name</param>
        /// <param name="extensions">Extensions that we want to check if they are in the file name.</param>
        /// <returns></returns>
        public static bool ContainsAny(this string file, params string[] extensions)
        {
            foreach (string extension in extensions)
                if (file.Contains(extension))
                    return true;

            return false;
        }

        public static string GetVersion()
        {
            Version version = Assembly.GetExecutingAssembly().GetName().Version;
            return version.Build == 0 ? $"{version.Major}.{version.Minor}" : $"{version.Major}.{version.Minor}.{version.Build}";
        }
    }
}
