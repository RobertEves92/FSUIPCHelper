using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace FSUIPCHelper.IO
{
    /// <summary>
    /// CORE/IO: Methods and Variables for file locations
    /// </summary>
    public static class FileList
    {
        /// <summary>
        /// Returns the FSUIPCHelper user specific appdata folder (%APPDATA%\FSUIPCHelper)
        /// </summary>
        public static string ApplicationDataFolder { get { return Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData) + "\\FSUIPCHelper\\"; } }

        /// <summary>
        /// Returns the path of the FSUIPCHelper log file
        /// </summary>
        public static string Log { get { return ApplicationDataFolder + "FSUIPCHelper Log.txt"; } }

        /// <summary>
        /// Checks for and creates the FSUIPCHelper appdata folder
        /// </summary>
        public static void CreateFolder()
        {
            if (!Directory.Exists(ApplicationDataFolder))
                Directory.CreateDirectory(ApplicationDataFolder);
        }

        /// <summary>
        /// Checks if a specified file exists
        /// </summary>
        /// <param name="file">Path of file to check</param>
        /// <returns>true/false</returns>
        public static bool FileExists(string file)
        {
            return File.Exists(file);
        }
    }
}