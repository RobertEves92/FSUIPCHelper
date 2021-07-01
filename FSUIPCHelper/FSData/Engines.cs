using FSUIPC;
using FSUIPCHelper.Logging;
using System;

namespace FSUIPCHelper.FSData
{
    /// <summary>
    /// CORE/FSDATA: Methods and Variables for obtain and store engine data from FSUIPC
    /// </summary>
    public static class Engines
    {
        #region Offsets
        private static readonly Offset<short> offsetEngine1 = new Offset<short>(2196);
        private static readonly Offset<short> offsetEngine2 = new Offset<short>(2348);
        private static readonly Offset<short> offsetEngine3 = new Offset<short>(2500);
        private static readonly Offset<short> offsetEngine4 = new Offset<short>(2652);
        private static readonly Offset<short> offsetNumberOfEngines = new Offset<short>(2796);
        #endregion

        #region Cached Values
        /// <summary>
        /// Returns engine running status for engine 1
        /// </summary>
        public static bool Engine1Running = false;
        /// <summary>
        /// Returns engine running status for engine 2
        /// </summary>
        public static bool Engine2Running = false;
        /// <summary>
        /// Returns engine running status for engine 3
        /// </summary>
        public static bool Engine3Running = false;
        /// <summary>
        /// Returns engine running status for engine 4
        /// </summary>
        public static bool Engine4Running = false;
        #endregion

        #region Current Status Getters
        private static bool Engine1Status
        {
            get
            {
                if (offsetEngine1.Value > 0)
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
        }
        private static bool Engine2Status
        {
            get
            {
                if (offsetEngine2.Value > 0)
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
        }
        private static bool Engine3Status
        {
            get
            {
                if (offsetEngine3.Value > 0)
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
        }
        private static bool Engine4Status
        {
            get
            {
                if (offsetEngine4.Value > 0)
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
        }

        /// <summary>
        /// Returns the availability of engine 1
        /// </summary>
        public static bool IsEngine1Available
        {
            get
            {
                FSUIPCConnection.Process();
                if (offsetNumberOfEngines.Value > 0)
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
        }
        /// <summary>
        /// Returns the availability of engine 2
        /// </summary>
        public static bool IsEngine2Available
        {
            get
            {
                FSUIPCConnection.Process();
                if (offsetNumberOfEngines.Value > 1)
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
        }
        /// <summary>
        /// Returns the availability of engine 3
        /// </summary>
        public static bool IsEngine3Available
        {
            get
            {
                FSUIPCConnection.Process();
                if (offsetNumberOfEngines.Value > 2)
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
        }
        /// <summary>
        /// Returns the availability of engine 4
        /// </summary>
        public static bool IsEngine4Available
        {
            get
            {
                FSUIPCConnection.Process();
                if (offsetNumberOfEngines.Value > 3)
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
        /// Updates and logs all engine status changes
        /// </summary>
        public static void UpdateAllEngines()
        {
            UpdateEngine1();
            UpdateEngine2();
            UpdateEngine3();
            UpdateEngine4();
        }
        /// <summary>
        /// Updates and logs status changes for engine 1
        /// </summary>
        public static void UpdateEngine1()
        {
            try
            {
                if (Engine1Status && !Engine1Running)
                {
                    Engine1Running = true;
                    FlightLog.AddLog("Engine 1 Started");
                }
                else if (Engine1Status == false && Engine1Running)
                {
                    Engine1Running = false;
                    FlightLog.AddLog("Engine 1 Stopped");
                }
            }
            catch (Exception e)
            {
                Log.AddLog("Failed to update engine 1 status from FSUIPC", TraceLevel.Warning, e);
            }
        }
        /// <summary>
        /// Updates and logs status changes for engine 2
        /// </summary>
        public static void UpdateEngine2()
        {
            try
            {
                if (Engine2Status && !Engine2Running)
                {
                    Engine2Running = true;
                    FlightLog.AddLog("Engine 2 Started");
                }
                else if (Engine2Status == false && Engine2Running)
                {
                    Engine2Running = false;
                    FlightLog.AddLog("Engine 2 Stopped");
                }
            }
            catch (Exception e)
            {
                Log.AddLog("Failed to update engine 2 status from FSUIPC", TraceLevel.Warning, e);
            }
        }
        /// <summary>
        /// Updates and logs status changes for engine 3
        /// </summary>
        public static void UpdateEngine3()
        {
            try
            {
                if (Engine3Status && !Engine3Running)
                {
                    Engine3Running = true;
                    FlightLog.AddLog("Engine 3 Started");
                }
                else if (Engine3Status == false && Engine3Running)
                {
                    Engine3Running = false;
                    FlightLog.AddLog("Engine 3 Stopped");
                }
            }
            catch (Exception e)
            {
                Log.AddLog("Failed to update engine 3 status from FSUIPC", TraceLevel.Warning, e);
            }
        }
        /// <summary>
        /// Updates and logs status changes for engine 4
        /// </summary>
        public static void UpdateEngine4()
        {
            try
            {
                if (Engine4Status && !Engine4Running)
                {
                    Engine4Running = true;
                    FlightLog.AddLog("Engine 4 Started");
                }
                else if (Engine4Status == false && Engine4Running)
                {
                    Engine4Running = false;
                    FlightLog.AddLog("Engine 4 Stopped");
                }
            }
            catch (Exception e)
            {
                Log.AddLog("Failed to update engine 4 status from FSUIPC", TraceLevel.Warning, e);
            }
        }
        /// <summary>
        /// Resets all engine statuses to default (false)
        /// </summary>
        public static void ClearEngines()
        {
            Engine1Running = false;
            Engine2Running = false;
            Engine3Running = false;
            Engine4Running = false;
        }
        #endregion
    }
}