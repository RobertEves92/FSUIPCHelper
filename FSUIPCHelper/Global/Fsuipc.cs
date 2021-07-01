using FSUIPC;

namespace FSUIPCHelper.Global
{
    /// <summary>
    /// CORE/GLOBAL: Methods for handling a FSUIPC Connection
    /// </summary>
    public static class Fsuipc
    {
        /// <summary>
        /// Open a new connection
        /// </summary>
        public static void OpenConnection()
        {
            FSUIPCConnection.Open();
        }

        /// <summary>
        /// Close the connection
        /// </summary>
        public static void CloseConnection()
        {
            FSUIPCConnection.Close();
        }

        /// <summary>
        /// Update data
        /// </summary>
        public static void ProcessData()
        {
            try
            {
                FSUIPCConnection.Process();
            }
            catch (FSUIPCException e)
            {
                //NOTE Close and reopen connection
                if (e.FSUIPCErrorCode == FSUIPCError.FSUIPC_ERR_SENDMSG)
                {
                    FSUIPCConnection.Close();
                    FSUIPCConnection.Open();
                    FSUIPCConnection.Process();
                }
            }
        }
    }
}