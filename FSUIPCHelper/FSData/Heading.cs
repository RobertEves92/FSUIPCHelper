using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using FSUIPC;

namespace FSUIPCHelper.FSData
{
    /// <summary>
    /// CORE/FSDATA: Methods and Variables to obtain and store heading data from FSUIPC
    /// </summary>
    public static class Heading
    {
        #region Offsets
        private static readonly Offset<double> offsetWhiskeyHeading = new Offset<double>(716);
        #endregion

        #region Getters
        /// <summary>
        /// Gets the value of the aircrafts whiskey compass
        /// </summary>
        public static string WhiskeyHeading
        {
            get
            {
                return Convert.ToInt32(offsetWhiskeyHeading.Value).ToString();
            }
        }
        #endregion
    }
}
