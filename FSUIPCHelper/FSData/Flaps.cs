using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using FSUIPC;
using FSUIPCHelper.Logging;

namespace FSUIPCHelper.FSData
{
    /// <summary>
    /// CORE/FSDATA: Methods and variables to obtain and store flap data from FSUIPC
    /// </summary>
    public static class Flaps
    {
        private static bool firstUpdate = true; // stops warning about dividing by zero on the first update

        #region Offsets
        private static readonly Offset<short> offsetFrequency = new Offset<short>(15354);
        private static readonly Offset<int> offsetPosition = new Offset<int>(3036);
        #endregion

        #region Cached Value
        /// <summary>
        /// Returns the current flap position
        /// </summary>
        public static int FlapPosition = 0;
        #endregion

        #region Update Methods
        /// <summary>
        /// Updates and logs the flap position status
        /// </summary>
        public static void UpdateFlaps()
        {
            try
            {
                if (FlapPosition != offsetPosition.Value / offsetFrequency.Value)
                {
                    FlapPosition = offsetPosition.Value / offsetFrequency.Value;

                    if (Aircraft.IsAirborne)
                    {
                        FlightLog.AddLog("Flaps set to position " + FlapPosition + " at " + Altitude.AltitudeS + " and " + Speed.IndicatedAirspeed + "kts");
                    }
                    else
                    {
                        FlightLog.AddLog("Flaps set to position " + FlapPosition);
                    }
                }
            }
            catch (DivideByZeroException e)
            {
                if (firstUpdate)
                {
                    firstUpdate = false;
                }
                else
                {
                    Log.AddLog("Failed to update flaps from FSUIPC (Possible FSUIPC Timeout)", TraceLevel.Warning, e);
                }
            }
            catch (Exception e)
            {
                Log.AddLog("Failed to update flaps from FSUIPC", TraceLevel.Warning, e);
            }
        }

        /// <summary>
        /// Resets flap position status
        /// </summary>
        public static void ClearFlaps()
        {
            FlapPosition = 0;
        }
        #endregion
    }
}
