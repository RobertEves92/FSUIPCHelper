using FSUIPC;
using FSUIPCHelper.Logging;
using System;

namespace FSUIPCHelper.FSData
{
    /// <summary>
    /// CORE/FSDATA: Methods and Variables for obtain and store radio data from FSUIPC
    /// </summary>
    public static class Radios
    {
        #region Offsets

        private static readonly Offset<short> offsetCom1 = new Offset<short>(846);
        private static readonly Offset<short> offsetCom2 = new Offset<short>(12568);
        private static readonly Offset<short> offsetTransponder = new Offset<short>(852);
        private static readonly Offset<short> offsetNav1 = new Offset<short>(848);
        private static readonly Offset<short> offsetNav2 = new Offset<short>(850);
        private static readonly Offset<string> offsetNav1Ident = new Offset<string>(12288, 6);
        private static readonly Offset<string> offsetNav2Ident = new Offset<string>(12319, 6);

        #endregion Offsets

        #region Cached Values

        /// <summary>
        /// Returns the current active frequency for COM1
        /// </summary>
        public static string COM1 = "100.00";

        /// <summary>
        /// Returns the current active frequency for COM2
        /// </summary>
        public static string COM2 = "100.00";

        /// <summary>
        /// Returns the current active frequency for NAV1
        /// </summary>
        public static string NAV1 = "100.00";

        /// <summary>
        /// Returns the current active frequency for NAV2
        /// </summary>
        public static string NAV2 = "100.00";

        /// <summary>
        /// Returns the current active frequency the transponder
        /// </summary>
        public static string XPDNR = "0000";

        #endregion Cached Values

        #region Current Status Getters

        private static string Com1Status
        {
            get
            {
                string raw = Convert.ToInt32(offsetCom1.Value).ToString("X4");
                return string.Concat("1", raw.Substring(0, 2), ".", raw.Substring(2, 2));
            }
        }

        private static string Com2Status
        {
            get
            {
                string raw = Convert.ToInt32(offsetCom2.Value).ToString("X4");
                return string.Concat("1", raw.Substring(0, 2), ".", raw.Substring(2, 2));
            }
        }

        private static string Nav1Status
        {
            get
            {
                string raw = Convert.ToInt32(offsetNav1.Value).ToString("X4");
                return string.Concat("1", raw.Substring(0, 2), ".", raw.Substring(2, 2));
            }
        }

        /// <summary>
        /// Returns the identity code of the active NAV1 frequency
        /// </summary>
        public static string Nav1Ident
        {
            get
            {
                try
                {
                    if (offsetNav1Ident.Value != "")
                    {
                        return " (" + offsetNav1Ident.Value + ")";
                    }
                    else { return ""; }
                }
                catch (Exception e)
                {
                    Log.AddLog("Failed to get NAV1Ident status", TraceLevel.Warning, e);
                    return null;
                }
            }
        }

        private static string Nav2Status
        {
            get
            {
                string raw = Convert.ToInt32(offsetNav2.Value).ToString("X4");
                return string.Concat("1", raw.Substring(0, 2), ".", raw.Substring(2, 2));
            }
        }

        /// <summary>
        /// Returns the identity code of the active NAV2 frequency
        /// </summary>
        public static string Nav2Ident
        {
            get
            {
                try
                {
                    if (offsetNav2Ident.Value != "")
                    {
                        return " (" + offsetNav2Ident.Value + ")";
                    }
                    else { return ""; }
                }
                catch (Exception e)
                {
                    Log.AddLog("Failed to get NAV2Ident status", TraceLevel.Warning, e);
                    return null;
                }
            }
        }

        private static string TransponderStatus => Convert.ToInt32(offsetTransponder.Value).ToString("X4");

        #endregion Current Status Getters

        #region Update Methods

        /// <summary>
        /// Update and log all radio status changes
        /// </summary>
        public static void UpdateAll()
        {
            UpdateCom1();
            UpdateCom2();
            UpdateNav1();
            UpdateNav2();
            UpdateTransponder();
        }

        /// <summary>
        /// Update and log status changes to COM1
        /// </summary>
        public static void UpdateCom1()
        {
            try
            {
                if (COM1 != Com1Status)
                {
                    COM1 = Com1Status;
                    FlightLog.AddLog("COM1: " + COM1);
                }
            }
            catch (Exception e)
            {
                Log.AddLog("Failed to update COM1 status from FSUIPC", TraceLevel.Warning, e);
            }
        }

        /// <summary>
        /// Update and log status changes to COM2
        /// </summary>
        public static void UpdateCom2()
        {
            try
            {
                if (COM2 != Com2Status)
                {
                    COM2 = Com2Status;
                    FlightLog.AddLog("COM2: " + COM2);
                }
            }
            catch (Exception e)
            {
                Log.AddLog("Failed to update COM2 status from FSUIPC", TraceLevel.Warning, e);
            }
        }

        /// <summary>
        /// Update and log status changes to NAV1
        /// </summary>
        public static void UpdateNav1()
        {
            try
            {
                if (NAV1 != Nav1Status)
                {
                    NAV1 = Nav1Status;
                    FlightLog.AddLog("NAV1: " + NAV1);
                }
            }
            catch (Exception e)
            {
                Log.AddLog("Failed to update NAV1 status from FSUIPC", TraceLevel.Warning, e);
            }
        }

        /// <summary>
        /// Update and log status changes to NAV2
        /// </summary>
        public static void UpdateNav2()
        {
            try
            {
                if (NAV2 != Nav2Status)
                {
                    NAV2 = Nav2Status;
                    FlightLog.AddLog("NAV2: " + NAV2);
                }
            }
            catch (Exception e)
            {
                Log.AddLog("Failed to update NAV2 status from FSUIPC", TraceLevel.Warning, e);
            }
        }

        /// <summary>
        /// Update and log status changes to Transponder
        /// </summary>
        public static void UpdateTransponder()
        {
            try
            {
                if (XPDNR != TransponderStatus)
                {
                    XPDNR = TransponderStatus;
                    switch (XPDNR)
                    {
                        case "7000":
                        case "1200":
                            FlightLog.AddLog("XPNDR: " + XPDNR + " (VFR)");
                            break;

                        case "7700":
                        case "7600":
                        case "7500":
                            FlightLog.AddLog("XPNDR: " + XPDNR + " (Emergency)");
                            break;

                        default:
                            FlightLog.AddLog("XPNDR: " + XPDNR);
                            break;
                    }
                }
            }
            catch (Exception e)
            {
                Log.AddLog("Failed to update XPNDR status from FSUIPC", TraceLevel.Warning, e);
            }
        }

        /// <summary>
        /// Reset all radio statuses to default ("")
        /// </summary>
        public static void ClearRadios()
        {
            COM1 = "100.00";
            COM2 = "100.00";
            NAV1 = "100.00";
            NAV2 = "100.00";
            XPDNR = "0000";
        }

        #endregion Update Methods
    }
}