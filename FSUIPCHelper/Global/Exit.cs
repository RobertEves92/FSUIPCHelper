using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace FSUIPCHelper.Global
{
    /// <summary>
    /// CORE/GLOBAL: Methods for logging the exiting of Vacars (including exit code)
    /// </summary>
    public static class Exit
    {
        /// <summary>
        /// Logs a non-fatal closure of Vacars with an exit code
        /// </summary>
        /// <param name="code">Code to log</param>
        public static void ExitWithCode(ExitCode code)
        {
            Logging.Log.AddLog("Vacars exited with code " + (int)code + " (" + code + ")"); //add to log
            Environment.Exit((int)code); //exit using code
        }

        /// <summary>
        /// Logs a fatal closure of Vacars with an exit code
        /// </summary>
        /// <param name="code">Code to log</param>
        public static void FatalExit(ExitCode code)
        {
            Logging.Log.AddLog("Vacars exited with code " + (int)code + " (" + code + ")", Logging.TraceLevel.Fatal); //add to log
            Environment.Exit((int)code); //exit using code
        }
    }
}