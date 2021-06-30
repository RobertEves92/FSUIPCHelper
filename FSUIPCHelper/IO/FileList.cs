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
        /// Returns the Vacars user specific appdata folder (%APPDATA%\Vacars)
        /// </summary>
        public static string ApplicationDataFolder { get { return Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData) + "\\Vacars\\"; } }

        /// <summary>
        /// Returns the Vacars installation folder
        /// </summary>
        public static string ApplicationInstallFolder { get { return System.Reflection.Assembly.GetEntryAssembly().Location.Replace("Vacars.exe", ""); } }

        /// <summary>
        /// Returns the path of the user config file
        /// </summary>
        public static string UserSettings { get { return ApplicationDataFolder + "User Settings.xml"; } }

        /// <summary>
        /// Returns the path of the Vacars log file
        /// </summary>
        public static string Log { get { return ApplicationDataFolder + "Vacars Log.txt"; } }

        /// <summary>
        /// Returns the path of the phpVMS send file
        /// </summary>
        public static string PhpVmsSend { get { return ApplicationDataFolder + "send.xml"; } }

        /// <summary>
        /// Returns the path of the phpVMS receive file
        /// </summary>
        public static string PhpVmsReceive { get { return ApplicationDataFolder + "receive.xml"; } }

        /// <summary>
        /// Checks for and creates the Vacars appdata folder
        /// </summary>
        public static void CreateFolder()
        {
            if (!Directory.Exists(ApplicationDataFolder))
                Directory.CreateDirectory(ApplicationDataFolder);
        }

        /// <summary>
        /// Clears the Vacars log
        /// </summary>
        public static void ClearLog() //TODO Move to Logging.Log
        {
            File.WriteAllText(Log, "");
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