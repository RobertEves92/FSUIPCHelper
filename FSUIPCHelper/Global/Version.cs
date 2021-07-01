using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace FSUIPCHelper.Global
{
    /// <summary>
    /// CORE/GLOBAL: Methods for version reporting
    /// </summary>
    public static class Version
    {
        /// <summary>
        /// Returns the version number of FSUIPCHelper from AssemblyInfo
        /// </summary>
        /// <returns>Returns the version number of FSUIPCHelper from AssemblyInfo</returns>
        public static string GetVersion()
        {
            StringBuilder sb = new StringBuilder();
            sb.AppendFormat("v{0}", System.Reflection.Assembly.GetExecutingAssembly().GetName().Version.Major);

            if (System.Reflection.Assembly.GetExecutingAssembly().GetName().Version.Minor != 0)
            {
                sb.AppendFormat(".{0}", System.Reflection.Assembly.GetExecutingAssembly().GetName().Version.Minor);

                if (System.Reflection.Assembly.GetExecutingAssembly().GetName().Version.Build != 0)
                {
                    sb.AppendFormat(".{0}", System.Reflection.Assembly.GetExecutingAssembly().GetName().Version.Build);
                }
            }

            if (System.Reflection.Assembly.GetExecutingAssembly().GetName().Version.Minor == 0 && System.Reflection.Assembly.GetExecutingAssembly().GetName().Version.Build != 0)
            {
                sb.AppendFormat(".0.{0}", System.Reflection.Assembly.GetExecutingAssembly().GetName().Version.Build);
            }

            #if DEBUG
            sb.Append(" - DEBUG");
            #endif

            return sb.ToString();
        }
    }
}
