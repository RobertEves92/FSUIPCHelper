using System;
using System.IO;
using FSUIPCHelper.IO;

namespace FSUIPCHelper.Logging
{
    /// <summary>
    /// CORE/LOGGING: Methods for adding messages to the FSUIPCHelper log
    /// </summary>
    public static class Log
    {
        #region Public methods
        /// <summary>
        /// Add a message to the log
        /// </summary>
        /// <param name="log">Message to add</param>
        public static void AddLog(string log)
        {
            WriteToLog(log);
        }
        /// <summary>
        /// Add a message to the log
        /// </summary>
        /// <param name="log">Message to add</param>
        /// <param name="level">Message Trace Level</param>
        public static void AddLog(string log, TraceLevel level)
        {
            WriteToLog(log, level);
        }
        /// <summary>
        /// Add a message to the log
        /// </summary>
        /// <param name="log">Message to add</param>
        /// <param name="e">Exception to attach</param>
        /// <param name="level">Message Trace Level</param>
        public static void AddLog(string log, TraceLevel level, Exception e)
        {
            string l = string.Format("{0}: {1}\r\n\t\tStackTrace:{2}", log, e.Message, e.StackTrace);
            WriteToLog(l, level);
        }

        /// <summary>
        /// Clears the FSUIPCHelper log
        /// </summary>
        public static void ClearLog()
        {
            File.WriteAllText(FileList.Log, "");
        }
        #endregion

        #region Local methods
        private static void WriteToLog(string log)
        {
            string l = string.Format("[{0}] {1}", System.DateTime.Now.ToString(), log);

            TextWriter tw = new StreamWriter(FileList.Log, true);
            tw.WriteLine(l);
            tw.Flush();
            tw.Close();
        }

        private static void WriteToLog(string log, TraceLevel errorLevel)
        {
            string l = string.Format("[{0}] {1}: {2}", System.DateTime.Now.ToString(), errorLevel, log);

            TextWriter tw = new StreamWriter(FileList.Log, true);
            tw.WriteLine(l);
            tw.Flush();
            tw.Close();
        }
        #endregion
    }
}