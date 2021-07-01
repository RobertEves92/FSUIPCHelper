namespace FSUIPCHelper.Global
{
    /// <summary>
    /// Exit Code
    /// </summary>
    public enum ExitCode
    {
        /// <summary>
        /// Normal Close / User Requested
        /// </summary>
        Normal = 0x0,
        /// <summary>
        /// Generic or Unknown type for error
        /// </summary>
        GenericError = 0x1,
        /// <summary>
        /// File Not Found error
        /// </summary>
        FileNotFound = 0x2
    }
}
