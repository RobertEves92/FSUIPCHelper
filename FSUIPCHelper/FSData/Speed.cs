using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using FSUIPC;
using FSUIPCHelper.Logging;

namespace FSUIPCHelper.FSData
{
    /// <summary>
    /// CORE/FSDATA: Methods to obtain speed data from FSUIPC
    /// </summary>
    public static class Speed
    {
        #region Offsets
        private static readonly Offset<int> offsetTrueAirspeed = new Offset<int>(696);
        private static readonly Offset<int> offsetIndicatedAirspeed = new Offset<int>(700);
        private static readonly Offset<int> offsetVerticalSpeed = new Offset<int>(712);
        private static readonly Offset<int> offsetGroundSpeed = new Offset<int>(692);
        #endregion

        #region Getters
        /// <summary>
        /// Returns the aircrafts ground speed
        /// </summary>
        public static int GroundSpeed
        {
            get
            {
                return Convert.ToInt32((double)(offsetGroundSpeed.Value / 65536) * 1.9438444924406);
            }
        }

        /// <summary>
        /// Returns the aircrafts true airspeed in knots
        /// </summary>
        public static int TrueAirspeed
        {
            get
            {
                try
                {
                    return offsetTrueAirspeed.Value / 128;
                }
                catch (Exception e)
                {
                    Log.AddLog("Failed to get true airspeed", TraceLevel.Warning, e);
                    return 0;
                }
            }
        }
        /// <summary>
        /// Returns the aircrafts indicated airspeed in knots
        /// </summary>
        public static int IndicatedAirspeed
        {
            get
            {
                try
                {
                    return offsetIndicatedAirspeed.Value / 128;
                }
                catch (Exception e)
                {
                    Log.AddLog("Failed to get indicated airspeed", TraceLevel.Warning, e);
                    return 0;
                }
            }
        }
        /// <summary>
        /// Returns the aircrafts vertical speeds in ft/min
        /// </summary>
        public static int VerticalSpeed
        {
            get
            {
                try
                {
                    return Convert.ToInt32((double)offsetVerticalSpeed.Value * 0.768946875);
                }
                catch (Exception e)
                {
                    Log.AddLog("Failed to get vertical speed", TraceLevel.Warning, e);
                    return 0;
                }
            }
        }
        #endregion
    }
}