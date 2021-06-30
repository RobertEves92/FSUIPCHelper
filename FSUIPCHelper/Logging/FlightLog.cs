using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace FSUIPCHelper.Flight
{
    /// <summary>
    /// CORE/FLIGHT: Methods and Variables for Flight Log management
    /// </summary>
    public static class FlightLog
    {
        private static readonly List<string> logList = new List<string>();

        /// <summary>
        /// Returns the Flight Log in a readable text format
        /// </summary>
        public static string GetFlightLog()
        {
            StringBuilder sb = new StringBuilder();

            foreach (string s in logList)
            {
                sb.AppendFormat("{0}\r\n", s);
            }

            return sb.ToString();
        }
        

        /// <summary>
        /// Add a time stamped note to the flight log
        /// </summary>
        /// <param name="log">Note to add</param>
        public static void AddLog(string log)
        {
            DateTime d = DateTime.Now.ToUniversalTime();

            string l = string.Format("[{0}] {1}", string.Format("{0:HH:mm:ss}", d), log);

            logList.Add(l);
        }

        /// <summary>
        /// Remove all entries from the log
        /// </summary>
        public static void ClearLog()
        {
            logList.RemoveRange(0, logList.Count);
        }
    }
}