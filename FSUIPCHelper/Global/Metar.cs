using FSUIPCHelper.Logging;
using System;
using System.IO;
using System.Net;

namespace FSUIPCHelper.Global
{
    /// <summary>
    /// CORE/GLOBAL: Method for getting a metar string
    /// </summary>
    public static class Metar
    {
        /// <summary>
        /// Get a metar for a given airport
        /// </summary>
        /// <param name="icao">Airport ICAO Code</param>
        /// <returns>Metar string</returns>
        public static string GetMetarString(string icao)
        {
            try
            {
                HttpWebRequest httpRequest = (HttpWebRequest)WebRequest.Create(string.Format("http://weather.noaa.gov/pub/data/observations/metar/decoded/{0}.TXT", icao));
                httpRequest.Timeout = 5000;//5s
                httpRequest.UserAgent = "FSUIPCHelper";

                HttpWebResponse webResponse = (HttpWebResponse)httpRequest.GetResponse();
                StreamReader responseStream = new StreamReader(webResponse.GetResponseStream());

                return responseStream.ReadToEnd();
            }
            catch (Exception e)
            {
                if (e.Message == "The operation has timed out")
                {
                    Log.AddLog("Error obtaining metar (TIMEOUT)", TraceLevel.Error, e);
                }
                else
                {
                    Log.AddLog("Error obtaining metar", TraceLevel.Error, e);
                }
                throw e;
            }
        }
    }
}
