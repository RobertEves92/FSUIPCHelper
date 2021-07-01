using FSUIPC;
using FSUIPCHelper.Logging;
using System;

namespace FSUIPCHelper.FSData
{
    /// <summary>
    /// CORE/FSDATA: Methods and Variables for reading simulator data from FSUIPC
    /// </summary>
    public static class Simulator
    {
        #region Offsets
        private static readonly Offset<short> offsetFSVersion = new Offset<short>(13064);
        private static readonly Offset<short> offsetFSPause = new Offset<short>(612);
        #endregion

        #region Cached Values
        /// <summary>
        /// Returns the simulator pause status
        /// </summary>
        public static bool IsPaused = false;
        #endregion

        #region Getters
        /// <summary>
        /// Returns the version of flight simulator
        /// </summary>
        public static string FSVersion
        {
            get
            {
                try
                {
                    switch (offsetFSVersion.Value)
                    {
                        case 1:
                            return "FS98";
                        case 2:
                            return "FS2K";
                        case 3:
                            return "CFS2";
                        case 4:
                            return "CFS1";
                        case 5:
                            return "reserved";
                        case 6:
                            return "FS2002";
                        case 7:
                            return "FS2004";
                        case 8:
                            return "FSX";
                        case 9:
                            return "ESP";
                        case 10:
                            return "P3D";
                        default:
                            FSUIPCConnection.Process();
                            return FSVersion;
                    }
                }
                catch (Exception e)
                {
                    Log.AddLog("Failed to get flight simulator version", TraceLevel.Warning, e);
                    return "Unknown";
                }
            }
        }
        private static bool IsPausedStatus
        {
            get
            {
                if (offsetFSPause.Value == 1)
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
        }
        #endregion

        #region Update Methods
        /// <summary>
        /// Update and log changes to pause status
        /// </summary>
        public static void UpdatePause()
        {
            try
            {
                if (!IsPaused && IsPausedStatus)
                {
                    IsPaused = true;
                    FlightLog.AddLog("Simulator Paused");
                }
                else if (IsPaused && !IsPausedStatus)
                {
                    IsPaused = false;
                    FlightLog.AddLog("Simulator Resumed");
                }
            }
            catch (Exception e)
            {
                Log.AddLog("Failed to update simulator pause status from FSUIPC", TraceLevel.Error, e);
            }
        }

        /// <summary>
        /// Clear stored simulator values
        /// </summary>
        public static void ClearSimulator()
        {
            IsPaused = false;
        }
        #endregion
    }
}