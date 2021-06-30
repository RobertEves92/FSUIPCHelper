using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using FSUIPC;
using FSUIPCHelper.Logging;

namespace FSUIPCHelper.FSData
{
    /// <summary>
    /// CORE/FSDATA: Methods and Variables for getting A/C Position from FSUIPC
    /// </summary>
    public static class Position
    {
        #region Offsets
        private static readonly Offset<long> offsetLatitude = new Offset<long>(1376);
        private static readonly Offset<long> offsetLongitude = new Offset<long>(1384);
        #endregion

        #region Getters
        #region Formatted
        /// <summary>
        /// Gets the Aircrafts Latitude
        /// </summary>
        public static string Latitude
        {
            get
            {
                try
                {
                    double lat = (double)offsetLatitude.Value * 90 / 4.2957189152768E+16;

                    string coord = Format(lat);

                    if (!coord.StartsWith("-"))
                    {
                        return coord + " N";
                    }
                    else
                    {
                        return coord.TrimStart('-') + " S";
                    }
                }
                catch (Exception e)
                {
                    Log.AddLog("Error getting Latitude from FSUIPC", TraceLevel.Warning, e);
                    return "";
                }
            }
        }
        /// <summary>
        /// Gets the Aircrafts Longitude
        /// </summary>
        public static string Longitude
        {
            get
            {
                try
                {
                    double lon = (double)offsetLongitude.Value * 360 / 1.84467440737096E+19;

                    string coord = Format(lon);

                    if (!coord.StartsWith("-"))
                    {
                        return coord + " E";
                    }
                    else
                    {
                        return coord.TrimStart('-') + " W";
                    }
                }
                catch (Exception e)
                {
                    Log.AddLog("Error getting Longitude from FSUIPC", TraceLevel.Warning, e);
                    return "";
                }
            }
        }
        #endregion
        #region Raw
        /// <summary>
        /// Gets the Aircrafts Latitude (Raw)
        /// </summary>
        public static double RawLatitude
        {
            get
            {
                try
                {
                    double lat = (double)offsetLatitude.Value * 90 / 4.2957189152768E+16;

                    return lat;
                }
                catch (Exception e)
                {
                    Log.AddLog("Error getting Latitude from FSUIPC", TraceLevel.Warning, e);
                    return 0;
                }
            }
        }
        /// <summary>
        /// Gets the Aircrafts Longitude (Raw)
        /// </summary>
        public static double RawLongitude
        {
            get
            {
                try
                {
                    double lon = (double)offsetLongitude.Value * 360 / 1.84467440737096E+19;

                    return lon;
                }
                catch (Exception e)
                {
                    Log.AddLog("Error getting Longitude from FSUIPC", TraceLevel.Warning, e);
                    return 0;
                }
            }
        }
        #endregion
        #endregion

        #region Methods
        private static string Format(double dec)
        {
            int sec = (int)Math.Round(dec * 3600);
            int deg = sec / 3600;
            sec = Math.Abs(sec % 3600);
            int min = sec / 60;
            sec %= 60;

            string coord = string.Format("{0}° {1}' {2}\"", deg, min, sec);

            return coord;
        }
        #endregion
    }
}
