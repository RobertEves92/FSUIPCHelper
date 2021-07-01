namespace FSUIPCHelper.FSData
{
    /// <summary>
    /// CORE/FSDATA: Master Update and Clear methods for FSDATA
    /// </summary>
    public static class FSData
    {
        /// <summary>
        /// Update all FSDATA and Log changes
        /// </summary>
        public static void UpdateFSData()
        {
            Simulator.UpdatePause();

            if (Simulator.IsPaused == false) //don't update anything if the simulator is paused
            {
                Aircraft.UpdateParkingBrake();
                Aircraft.UpdateLandingGear();
                Aircraft.UpdateLandingRate();
                Engines.UpdateAllEngines();
                Flaps.UpdateFlaps();
                Lights.UpdateAll();
                Radios.UpdateAll();
            }
        }

        /// <summary>
        /// Clear all FSDATA and reset to default values
        /// </summary>
        public static void ClearFSData()
        {
            Aircraft.ClearAircraft();
            Engines.ClearEngines();
            Fuel.ClearFuel();
            Flaps.ClearFlaps();
            Lights.ClearLights();
            Radios.ClearRadios();
            Simulator.ClearSimulator();
        }
    }
}