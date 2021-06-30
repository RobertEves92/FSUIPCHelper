using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using FSUIPC;
using FSUIPCHelper.Config;
using FSUIPCHelper.Logging;

namespace FSUIPCHelper.FSData
{
    /// <summary>
    /// CORE/FSDATA: Methods for calculating current fuel levels from FSUIPC
    /// </summary>
    public static class Fuel
    {
        #region Offsets
        private readonly static Offset<short> offsetFuelWeightLbs = new Offset<short>(2804);
        private readonly static Offset<int> offsetCentreTankLevel = new Offset<int>(2932);
        private readonly static Offset<int> offsetLeftMainTankLevel = new Offset<int>(2940);
        private readonly static Offset<int> offsetLeftAuxTankLevel = new Offset<int>(2948);
        private readonly static Offset<int> offsetLeftTipTankLevel = new Offset<int>(2956);
        private readonly static Offset<int> offsetRightMainTankLevel = new Offset<int>(2964);
        private readonly static Offset<int> offsetRightAuxTankLevel = new Offset<int>(2972);
        private readonly static Offset<int> offsetRightTipTankLevel = new Offset<int>(2980);
        private readonly static Offset<int> offsetCenterTankCapacity = new Offset<int>(2936);
        private readonly static Offset<int> offsetLeftMainTankCapacity = new Offset<int>(2944);
        private readonly static Offset<int> offsetLeftAuxTankCapacity = new Offset<int>(2952);
        private readonly static Offset<int> offsetLeftTipTankCapacity = new Offset<int>(2960);
        private readonly static Offset<int> offsetRightMainTankCapacity = new Offset<int>(2968);
        private readonly static Offset<int> offsetRightAuxTankCapacity = new Offset<int>(2976);
        private readonly static Offset<int> offsetRightTipTankCapacity = new Offset<int>(2984);
        private readonly static Offset<int> offsetCenter2TankCapacity = new Offset<int>(4680);
        private readonly static Offset<int> offsetCenter3TankCapacity = new Offset<int>(4688);
        private readonly static Offset<int> offsetCenter2TankLevel = new Offset<int>(4676);
        private readonly static Offset<int> offsetCenter3TankLevel = new Offset<int>(4684);
        private readonly static Offset<int> offsetExternal1TankLevel = new Offset<int>(4692);
        private readonly static Offset<int> offsetExternal2TankLevel = new Offset<int>(4700);
        private readonly static Offset<int> offsetExternal1TankCapacity = new Offset<int>(4696);
        private readonly static Offset<int> offsetExternal2TankCapacity = new Offset<int>(4704);
        #endregion

        #region Getter
        /// <summary>
        /// Gets or Sets the quantity of fuel at the start of the flight
        /// </summary>
        public static int StartFuel { get; set; }

        public static int UsedFuel
        {
            get { return StartFuel - CurrentFuel(Settings.GetCurrentAirline().FuelUnit); }
        }
        #endregion

        #region Public Methods
        /// <summary>
        /// Returns the aircrafts current fuel weight
        /// </summary>
        /// <returns>Fuel weight as an integer</returns>
        public static int CurrentFuel()
        {
            return CurrentFuel(Settings.GetCurrentAirline().FuelUnit);
        }

        /// <summary>
        /// Resets start fuel quantity to 0
        /// </summary>
        public static void ClearFuel()
        {
            StartFuel = 0;
        }
        /// <summary>
        /// Gets the fuel unit type for the current airline
        /// </summary>
        /// <returns>Pounds (LBS) or Kilograms (KGS)</returns>
        public static string GetFuelUnit()
        {
            switch (Settings.GetCurrentAirline().FuelUnit)
            {
                default:
                case FuelUnits.Lbs:
                    return "lbs";
                case FuelUnits.Kgs:
                    return "kgs";
            }
        }
        /// <summary>
        /// Current fuel quantity with fuel unit
        /// </summary>
        /// <returns>Returns the current fuel quantity with fuel unit</returns>
        public static string GetCurrentFuelS()
        {
            return CurrentFuel().ToString() + GetFuelUnit();
        }
        #endregion

        #region Local Methods
        private static double GetRawFuel()
        {
            //NOTE: Returns Lbs
            try
            {
                double fuel = FuelWeightInTank(offsetCentreTankLevel.Value, offsetCenterTankCapacity.Value);
                fuel = fuel + FuelWeightInTank(offsetCenter2TankLevel.Value, offsetCenter2TankCapacity.Value);
                fuel = fuel + FuelWeightInTank(offsetCenter3TankLevel.Value, offsetCenter3TankCapacity.Value);
                fuel = fuel + FuelWeightInTank(offsetLeftMainTankLevel.Value, offsetLeftMainTankCapacity.Value);
                fuel = fuel + FuelWeightInTank(offsetLeftAuxTankLevel.Value, offsetLeftAuxTankCapacity.Value);
                fuel = fuel + FuelWeightInTank(offsetLeftTipTankLevel.Value, offsetLeftTipTankCapacity.Value);
                fuel = fuel + FuelWeightInTank(offsetRightMainTankLevel.Value, offsetRightMainTankCapacity.Value);
                fuel = fuel + FuelWeightInTank(offsetRightAuxTankLevel.Value, offsetRightAuxTankCapacity.Value);
                fuel = fuel + FuelWeightInTank(offsetRightTipTankLevel.Value, offsetRightTipTankCapacity.Value);
                fuel = fuel + FuelWeightInTank(offsetExternal1TankLevel.Value, offsetExternal1TankCapacity.Value);
                fuel = fuel + FuelWeightInTank(offsetExternal2TankLevel.Value, offsetExternal2TankCapacity.Value);
                return fuel;
            }
            catch (Exception e)
            {
                Log.AddLog("Failed to get raw fuel weight", TraceLevel.Warning, e);
                return 0;
            }
        }
        private static double FuelWeightInTank(int tankLevel, int tankCapacity)
        {
            //NOTE: Returns Lbs

            try
            {
                double fuelTankCapacity = tankCapacity;
                double fuelTankLevel = tankLevel;
                double num = Convert.ToDouble((double)offsetFuelWeightLbs.Value / 256);
                return fuelTankCapacity * fuelTankLevel / 128 / 65536 * num;
            }
            catch (Exception e)
            {
                /* 
                 * NOTE: Logged as error as this will cause a calculation error
                 * due to returning 0 as the weight of the fuel for a tank
                 * and may cause further problems within the software
                 */
                Log.AddLog("Failed converting fuel percentage and capacity to weight", TraceLevel.Error, e);
                return 0;
            }
        }
        private static int CurrentFuel(FuelUnits units)
        {
            if (units == FuelUnits.Lbs)
                return Convert.ToInt32(GetRawFuel());
            else
                return Convert.ToInt32(GetRawFuel() * 0.45359);
        }
        #endregion
    }
}