using FSUIPC;
using FSUIPCHelper.Logging;
using System;

namespace FSUIPCHelper.FSData
{
    /// <summary>
    /// CORE/FSDATA: Methods and Variables for getting altitudes from FSUIPC
    /// </summary>
    public static class Altitude
    {
        #region Offsets
        private static readonly Offset<int> offsetAircraftAltitude = new Offset<int>(1396);
        private static readonly Offset<double> offsetStdAltitude = new Offset<double>(13488);
        private static readonly Offset<short> offsetGndAltitude = new Offset<short>(2892);
        private static readonly Offset<short> offsetVerticalSpeed = new Offset<short>(2114);
        #endregion

        #region Getters
        /// <summary>
        /// Returns the aircrafts altitude accounting for pressure settings
        /// </summary>
        public static int AltitudeN
        {
            get
            {
                try
                {
                    int alt = StdAltitude;

                    if (alt > 5500) //TODO Add setting to change value (US/UK/EU)
                    {
                        return ACAltitude;
                    }
                    return StdAltitude;
                }
                catch (Exception e)
                {
                    Log.AddLog("Failed to process aircraft altitude to string format", TraceLevel.Warning, e);
                    return 0;
                }
            }
        }

        /// <summary>
        /// Gets the aircraft altitude at std pressure(29.92in / 1013hPa)
        /// </summary>
        public static int StdAltitude
        {
            get
            {
                try
                {
                    if (offsetStdAltitude.Value == 0.0)
                    {
                        FSUIPCConnection.Process();
                    }

                    return Convert.ToInt32(offsetStdAltitude.Value * 3.28084);
                }
                catch (Exception e)
                {
                    Log.AddLog("Failed to get aircraft altitude (std pressure)", TraceLevel.Warning, e);
                    return 0;
                }
            }
        }

        /// <summary>
        /// Gets the aircraft altitude at current pressure
        /// </summary>
        public static int ACAltitude
        {
            get
            {
                try
                {
                    return Convert.ToInt32(offsetAircraftAltitude.Value * 3.28084);
                }
                catch (Exception e)
                {
                    Log.AddLog("Failed to get aircraft altitude (local pressure)", TraceLevel.Warning, e);
                    return 0;
                }
            }
        }

        /// <summary>
        /// Gets the aircraft altitude in a readable format
        /// </summary>
        public static string AltitudeS
        {
            get
            {
                try
                {
                    int alt = StdAltitude;

                    if (alt > 5500) //TODO Add setting to change value (US/UK/EU)
                    {
                        if (alt > 9999)
                        {
                            return string.Format("FL{0}", alt.ToString().Substring(0, 3));
                        }
                        return string.Format("FL0{0}", alt.ToString().Substring(0, 2));
                    }
                    return string.Format("{0}AGL", AglAltitude.ToString());
                }
                catch (Exception e)
                {
                    Log.AddLog("Failed to process aircraft altitude to string format", TraceLevel.Warning, e);
                    return "Unknown Altitude";
                }
            }
        }

        /// <summary>
        /// Gets the ground altitude
        /// </summary>
        public static int GndAltitude
        {
            get
            {
                try
                {
                    return Convert.ToInt32(offsetGndAltitude.Value * 3.28084);
                }
                catch (Exception e)
                {
                    Log.AddLog("Failed to get ground altitude", TraceLevel.Warning, e);
                    return 0;
                }
            }
        }

        /// <summary>
        /// Gets the aircrafts altitude above ground level
        /// </summary>
        public static int AglAltitude
        {
            get
            {
                try
                {
                    return ACAltitude - GndAltitude;
                }
                catch (Exception e)
                {
                    Log.AddLog("Failed to calculate aircraft altitude (AGL)", TraceLevel.Warning, e);
                    return 0;
                }
            }
        }

        /// <summary>
        /// Gets the aircrafts vertical speed in FT/M
        /// </summary>
        public static string VerticalSpeedS
        {
            get
            {
                int vs = Convert.ToInt32(offsetVerticalSpeed.Value * -3.28084);

                if (vs > 0)
                {
                    return "+" + vs + "fpm";
                }
                else if (vs < 0)
                {
                    return "-" + vs + "fpm";
                }
                else
                {
                    return vs + "fpm";
                }
            }
        }
        #endregion
    }
}