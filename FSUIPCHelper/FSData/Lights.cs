using FSUIPC;
using FSUIPCHelper.Logging;
using System;
using System.Collections;

namespace FSUIPCHelper.FSData
{
    /// <summary>
    /// CORE/FSDATA: Methods and Variables to obtain and store light data from FSUIPC
    /// </summary>
    public static class Lights
    {
        #region Offset
        private static readonly Offset<BitArray> offsetLights = new Offset<BitArray>(3340, 2);
        #endregion

        #region Cached Values
        /// <summary>
        /// Returns the status of the navigation lights
        /// </summary>
        public static bool NavigationLights;
        /// <summary>
        /// Returns the status of the beacon lights
        /// </summary>
        public static bool BeaconLights;
        /// <summary>
        /// Returns the status of the landing lights
        /// </summary>
        public static bool LandingLights;
        /// <summary>
        /// Returns the status of the taxi lights
        /// </summary>
        public static bool TaxiLights;
        /// <summary>
        /// Returns the status of the strobe lights
        /// </summary>
        public static bool StrobeLights;
        /// <summary>
        /// Returns the status of the instrument lights
        /// </summary>
        public static bool InstrumentLights;
        /// <summary>
        /// Returns the status of the recognition lights
        /// </summary>
        public static bool RecognitionLights;
        /// <summary>
        /// Returns the status of the wing lights
        /// </summary>
        public static bool WingLights;
        /// <summary>
        /// Returns the status of the logo lights
        /// </summary>
        public static bool LogoLights;
        /// <summary>
        /// Returns the status of the cabin lights
        /// </summary>
        public static bool CabinLights;
        #endregion

        #region Current Status Getters
        private static bool NavigationStatus => offsetLights.Value[0];
        private static bool BeaconStatus => offsetLights.Value[1];
        private static bool LandingStatus => offsetLights.Value[2];
        private static bool TaxiStatus => offsetLights.Value[3];
        private static bool StrobeStatus => offsetLights.Value[4];
        private static bool InstrumentStatus => offsetLights.Value[5];
        private static bool RecognitionStatus => offsetLights.Value[6];
        private static bool WingStatus => offsetLights.Value[7];
        private static bool LogoStatus => offsetLights.Value[8];
        private static bool CabinStatus => offsetLights.Value[9];
        #endregion

        #region Update Methods
        /// <summary>
        /// Update and log all light status changes
        /// </summary>
        public static void UpdateAll()
        {
            UpdateNavigation();
            UpdateBeacon();
            UpdateLanding();
            UpdateTaxi();
            UpdateStrobes();
            UpdateInstrument();
            UpdateRecognition();
            UpdateWing();
            UpdateLogo();
            UpdateCabin();
        }
        /// <summary>
        /// Update and log the status of the navigation lights
        /// </summary>
        public static void UpdateNavigation()
        {
            try
            {
                if (NavigationStatus && !NavigationLights)
                {
                    NavigationLights = true;
                    FlightLog.AddLog("Navigation Lights ON at " + Altitude.AltitudeS);
                }
                else if (!NavigationStatus && NavigationLights)
                {
                    NavigationLights = false;
                    FlightLog.AddLog("Navigation Lights OFF at " + Altitude.AltitudeS);
                }
            }
            catch (Exception e)
            {
                Log.AddLog("Failed to update navigation light status", TraceLevel.Warning, e);
            }
        }
        /// <summary>
        /// Update and log the status of the beacon lights
        /// </summary>
        public static void UpdateBeacon()
        {
            try
            {
                if (BeaconStatus && !BeaconLights)
                {
                    BeaconLights = true;
                    FlightLog.AddLog("Beacon Lights ON at " + Altitude.AltitudeS);
                }
                else if (!BeaconStatus && BeaconLights)
                {
                    BeaconLights = false;
                    FlightLog.AddLog("Beacon Lights OFF at " + Altitude.AltitudeS);
                }
            }
            catch (Exception e)
            {
                Log.AddLog("Failed to update beacon light status", TraceLevel.Warning, e);
            }
        }
        /// <summary>
        /// Update and log the status of the landing lights
        /// </summary>
        public static void UpdateLanding()
        {
            try
            {
                if (LandingStatus && !LandingLights)
                {
                    LandingLights = true;
                    FlightLog.AddLog("Landing Lights ON at " + Altitude.AltitudeS);
                }
                else if (!LandingStatus && LandingLights)
                {
                    LandingLights = false;
                    FlightLog.AddLog("Landing Lights OFF at " + Altitude.AltitudeS);
                }
            }
            catch (Exception e)
            {
                Log.AddLog("Failed to update landing lights status", TraceLevel.Warning, e);
            }
        }
        /// <summary>
        /// Update and log the status of the taxi lights
        /// </summary>
        public static void UpdateTaxi()
        {
            try
            {
                if (TaxiStatus && !TaxiLights)
                {
                    TaxiLights = true;
                    FlightLog.AddLog("Taxi Lights ON at " + Altitude.AltitudeS);
                }
                else if (!TaxiStatus && TaxiLights)
                {
                    TaxiLights = false;
                    FlightLog.AddLog("Taxi Lights OFF at " + Altitude.AltitudeS);
                }
            }
            catch (Exception e)
            {
                Log.AddLog("Failed to update taxi lights status", TraceLevel.Warning, e);
            }
        }
        /// <summary>
        /// Update and log the status of the strobe lights
        /// </summary>
        public static void UpdateStrobes()
        {
            try
            {
                if (StrobeStatus && !StrobeLights)
                {
                    StrobeLights = true;
                    FlightLog.AddLog("Strobe Lights ON at " + Altitude.AltitudeS);
                }
                else if (!StrobeStatus && StrobeLights)
                {
                    StrobeLights = false;
                    FlightLog.AddLog("Strobe Lights OFF at " + Altitude.AltitudeS);
                }
            }
            catch (Exception e)
            {
                Log.AddLog("Failed to update strobe lights status", TraceLevel.Warning, e);
            }
        }
        /// <summary>
        /// Update and log the status of the instrument lights
        /// </summary>
        public static void UpdateInstrument()
        {
            try
            {
                if (InstrumentStatus && !InstrumentLights)
                {
                    InstrumentLights = true;
                    FlightLog.AddLog("Instrument Lights ON at " + Altitude.AltitudeS);
                }
                else if (!InstrumentStatus && InstrumentLights)
                {
                    InstrumentLights = false;
                    FlightLog.AddLog("Instrument Lights OFF at " + Altitude.AltitudeS);
                }
            }
            catch (Exception e)
            {
                Log.AddLog("Failed to update instrument lights status", TraceLevel.Warning, e);
            }
        }
        /// <summary>
        /// Update and log the status of the recognition lights
        /// </summary>
        public static void UpdateRecognition()
        {
            try
            {
                if (RecognitionStatus && !RecognitionLights)
                {
                    RecognitionLights = true;
                    FlightLog.AddLog("Recognition Lights ON at " + Altitude.AltitudeS);
                }
                else if (!RecognitionStatus && RecognitionLights)
                {
                    RecognitionLights = false;
                    FlightLog.AddLog("Recognition Lights OFF at " + Altitude.AltitudeS);
                }
            }
            catch (Exception e)
            {
                Log.AddLog("Failed to update recognition lights status", TraceLevel.Warning, e);
            }
        }
        /// <summary>
        /// Update and log the status of the wing lights
        /// </summary>
        public static void UpdateWing()
        {
            try
            {
                if (WingStatus && !WingLights)
                {
                    WingLights = true;
                    FlightLog.AddLog("Wing Lights ON at " + Altitude.AltitudeS);
                }
                else if (!WingStatus && WingLights)
                {
                    WingLights = false;
                    FlightLog.AddLog("Wing Lights OFF at " + Altitude.AltitudeS);
                }
            }
            catch (Exception e)
            {
                Log.AddLog("Failed to update wing lights status", TraceLevel.Warning, e);
            }
        }
        /// <summary>
        /// Update and log the status of the logo lights
        /// </summary>
        public static void UpdateLogo()
        {
            try
            {
                if (LogoStatus && !LogoLights)
                {
                    LogoLights = true;
                    FlightLog.AddLog("Logo Lights ON at " + Altitude.AltitudeS);
                }
                else if (!LogoStatus && LogoLights)
                {
                    LogoLights = false;
                    FlightLog.AddLog("Logo Lights OFF at " + Altitude.AltitudeS);
                }
            }
            catch (Exception e)
            {
                Log.AddLog("Failed to update logo lights status", TraceLevel.Warning, e);
            }
        }
        /// <summary>
        /// Update and log the status of the cabin lights
        /// </summary>
        public static void UpdateCabin()
        {
            try
            {
                if (CabinStatus && !CabinLights)
                {
                    CabinLights = true;
                    FlightLog.AddLog("Cabin Lights ON at " + Altitude.AltitudeS);
                }
                else if (!CabinStatus && CabinLights)
                {
                    CabinLights = false;
                    FlightLog.AddLog("Cabin Lights OFF at " + Altitude.AltitudeS);
                }
            }
            catch (Exception e)
            {
                Log.AddLog("Failed to update cabin lights status", TraceLevel.Warning, e);
            }
        }
        /// <summary>
        /// Reset all light statuses to default (false)
        /// </summary>
        public static void ClearLights()
        {
            NavigationLights = false;
            BeaconLights = false;
            LandingLights = false;
            TaxiLights = false;
            StrobeLights = false;
            InstrumentLights = false;
            RecognitionLights = false;
            WingLights = false;
            LogoLights = false;
            CabinLights = false;
        }
        #endregion
    }
}