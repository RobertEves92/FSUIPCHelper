using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using FSUIPC;
using FSUIPCHelper.Logging;
using FSUIPCHelper.Flight;

namespace FSUIPCHelper.FSData
{
    /// <summary>
    /// CORE/FSDATA: Methods and Variables to obtain and store aircraft data from FSUIPC
    /// </summary>
    public static class Aircraft
    {
        #region Offsets
        private static readonly Offset<short> offsetAirborne = new Offset<short>(870);
        private static readonly Offset<int> offsetPushback = new Offset<int>(12784);
        private static readonly Offset<short> offsetParkingBrake = new Offset<short>(3016);
        private static readonly Offset<int> offsetLandingRate = new Offset<int>(780);
        private static readonly Offset<string> offsetAircraftName = new Offset<string>(15616, 256);
        private static readonly Offset<int> offsetGearNose = new Offset<int>(3052);
        private static readonly Offset<int> offsetGearRight = new Offset<int>(3056);
        private static readonly Offset<int> offsetGearLeft = new Offset<int>(3060);
        private static readonly Offset<int> offsetPitchAngle = new Offset<int>(1400);
        private static readonly Offset<int> offsetBankAngle = new Offset<int>(1404);
        #endregion

        #region Cached Values
        /// <summary>
        /// Returns the landing gear down status
        /// </summary>
        public static bool LandingGearDown = false;
        /// <summary>
        /// Returns the parking brake set status
        /// </summary>
        public static bool ParkingBrakeSet = false;
        /// <summary>
        /// Returns the landing rate of the aircraft
        /// </summary>
        public static Nullable<int> LandingRate = null;
        #endregion

        #region Current Status Getters
        /// <summary>
        /// Gets the name of the aircraft currently in use
        /// </summary>
        public static string AircraftName
        {
            get
            {
                try
                {
                    return offsetAircraftName.Value;
                }
                catch (Exception e)
                {
                    Log.AddLog("Failed to get aircraft name", TraceLevel.Warning, e);
                    return "Unknown";
                }
            }
        }
        /// <summary>
        /// Gets the aircrafts pushback status (3 = off, 0 = back, 1 = left, 2 = right)
        /// </summary>
        public static int Pushback
        {
            get
            {
                try
                {
                    return offsetPushback.Value;
                }
                catch (Exception e)
                {
                    Log.AddLog("Failed to get pushback value", TraceLevel.Warning, e);
                    return 3; //returns no active pushback
                }
            }
        }
        /// <summary>
        /// Gets the current pitch of the aircraft
        /// </summary>
        public static int Pitch
        {
            get
            {
                try
                {
                    return Convert.ToInt32((double)offsetPitchAngle.Value * 8.38190317153931E-08) * -1;
                }
                catch (Exception e)
                {
                    Log.AddLog("Failed to get pitch angle", TraceLevel.Error, e);
                    return 0;
                }
            }
        }
        /// <summary>
        /// Gets the current bank angle of the aircraft
        /// </summary>
        public static string BankAngle
        {
            get
            {
                int ba = Convert.ToInt32((double)offsetBankAngle.Value * 8.38190317153931E-08);
                if (ba > -1 && ba < 1)
                {
                    return "0 (Level)";
                }

                if (ba < -1)
                {
                    return string.Format("{0}(R)", ba);
                }

                if (ba > 1)
                {
                    return string.Format("{0}(L)", ba);
                }

                return "0 (Level)";
            }
        }
        private static bool ParkingBrakeStatus
        {
            get
            {
                if (offsetParkingBrake.Value > 1000)
                    return true;
                else
                    return false;
            }
        }
        private static bool LandingGearStatus
        {
            get
            {
                int gear = offsetGearNose.Value + offsetGearLeft.Value + offsetGearRight.Value;
                if (gear == 0)
                    return false; //up
                else
                    return true; //down
            }
        }
        /// <summary>
        /// Gets the airborne status of the aircraft
        /// </summary>
        public static bool IsAirborne
        {
            get
            {
                if (offsetAirborne.Value > 0)
                {
                    return false;
                }
                return true;
            }
        }
        #endregion
        #region Update Methods
        /// <summary>
        /// Updates and logs the landing gear down status
        /// </summary>
        public static void UpdateLandingGear()
        {
            try
            {
                if (LandingGearStatus == true && LandingGearDown)
                {
                    FlightLog.AddLog("Landing Gear DOWN at " + Altitude.AltitudeS);
                    LandingGearDown = true;
                }
                else if (LandingGearStatus == false && LandingGearDown)
                {
                    FlightLog.AddLog("Landing Gear UP at " + Altitude.AltitudeS);
                    LandingGearDown = false;
                }
            }
            catch (Exception e)
            {
                Log.AddLog("Failed to update Landing Gear Status from FSUIPC", TraceLevel.Warning, e);
            }
        }
        /// <summary>
        /// Updates and logs the parking brake set status
        /// </summary>
        public static void UpdateParkingBrake()
        {
            try
            {
                if (ParkingBrakeStatus && !ParkingBrakeSet)
                {
                    FlightLog.AddLog("Parking Brake Set");
                    ParkingBrakeSet = true;
                }
                else if (!ParkingBrakeStatus && ParkingBrakeSet)
                {
                    FlightLog.AddLog("Parking Brake Released");
                    ParkingBrakeSet = false;
                }
            }
            catch (Exception e)
            {
                Log.AddLog("Failed to update parking brake status", TraceLevel.Warning, e);
            }
        }
        /// <summary>
        /// Updates the cached landing rate value
        /// </summary>
        public static void UpdateLandingRate()
        {
            try
            {
                LandingRate = Convert.ToInt32((double)offsetLandingRate.Value * 0.768946875);
            }
            catch (Exception e)
            {
                Log.AddLog("Failed to get landing rate from FSUIPC", TraceLevel.Error, e);
            }
        }
        /// <summary>
        /// Resets local variables to defaults
        /// </summary>
        public static void ClearAircraft()
        {
            LandingGearDown = false;
            ParkingBrakeSet = false;
            LandingRate = null;
        }
        #endregion
    }
}