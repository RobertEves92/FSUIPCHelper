using FSUIPC;
using FSUIPCHelper.Logging;
using System;

namespace FSUIPCHelper.FSData
{
    /// <summary>
    /// CORE/FSDATA: Methods for calculating current fuel levels from FSUIPC
    /// </summary>
    public static class Fuel
    {
        #region Offsets
        private static readonly Offset<short> offsetFuelWeightLbs = new Offset<short>(2804);
        private static readonly Offset<int> offsetCentreTankLevel = new Offset<int>(2932);
        private static readonly Offset<int> offsetLeftMainTankLevel = new Offset<int>(2940);
        private static readonly Offset<int> offsetLeftAuxTankLevel = new Offset<int>(2948);
        private static readonly Offset<int> offsetLeftTipTankLevel = new Offset<int>(2956);
        private static readonly Offset<int> offsetRightMainTankLevel = new Offset<int>(2964);
        private static readonly Offset<int> offsetRightAuxTankLevel = new Offset<int>(2972);
        private static readonly Offset<int> offsetRightTipTankLevel = new Offset<int>(2980);
        private static readonly Offset<int> offsetCenterTankCapacity = new Offset<int>(2936);
        private static readonly Offset<int> offsetLeftMainTankCapacity = new Offset<int>(2944);
        private static readonly Offset<int> offsetLeftAuxTankCapacity = new Offset<int>(2952);
        private static readonly Offset<int> offsetLeftTipTankCapacity = new Offset<int>(2960);
        private static readonly Offset<int> offsetRightMainTankCapacity = new Offset<int>(2968);
        private static readonly Offset<int> offsetRightAuxTankCapacity = new Offset<int>(2976);
        private static readonly Offset<int> offsetRightTipTankCapacity = new Offset<int>(2984);
        private static readonly Offset<int> offsetCenter2TankCapacity = new Offset<int>(4680);
        private static readonly Offset<int> offsetCenter3TankCapacity = new Offset<int>(4688);
        private static readonly Offset<int> offsetCenter2TankLevel = new Offset<int>(4676);
        private static readonly Offset<int> offsetCenter3TankLevel = new Offset<int>(4684);
        private static readonly Offset<int> offsetExternal1TankLevel = new Offset<int>(4692);
        private static readonly Offset<int> offsetExternal2TankLevel = new Offset<int>(4700);
        private static readonly Offset<int> offsetExternal1TankCapacity = new Offset<int>(4696);
        private static readonly Offset<int> offsetExternal2TankCapacity = new Offset<int>(4704);
        #endregion

        #region Getter
        /// <summary>
        /// Gets or Sets the quantity of fuel at the start of the flight
        /// </summary>
        public static int StartFuel { get; set; }
        #endregion

        #region Public Methods
        /// <summary>
        /// Returns the aircrafts current fuel weight
        /// </summary>
        /// <returns>Fuel weight as an integer</returns>
        public static int CurrentFuel(FuelUnits units)
        {
            if (units == FuelUnits.Lbs)
            {
                return Convert.ToInt32(GetRawFuel());
            }
            else
            {
                return Convert.ToInt32(GetRawFuel() * 0.45359);
            }
        }

        /// <summary>
        /// Resets start fuel quantity to 0
        /// </summary>
        public static void ClearFuel()
        {
            StartFuel = 0;
        }

        /// <summary>
        /// Current fuel quantity with fuel unit
        /// </summary>
        /// <returns>Returns the current fuel quantity with fuel unit</returns>
        public static string GetCurrentFuelS(FuelUnits fuelUnit)
        {
            if (fuelUnit == FuelUnits.Lbs)
            {
                return CurrentFuel(fuelUnit).ToString() + "lbs";
            }
            else
            {
                return CurrentFuel(fuelUnit).ToString() + "kgs";
            }
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
        #endregion
    }
}