namespace FSUIPCHelper.Logging
{
    /// <summary>
    /// Trace/Error Level
    /// </summary>
    public enum TraceLevel
    {
        /// <summary>
        /// Info: Message or note usually for debugging purposes
        /// </summary>
        Info = 0,
        /// <summary>
        /// Warning: A potentially harmful situation
        /// </summary>
        Warning = 1,
        /// <summary>
        /// Error: A problem that allows the software to continue running
        /// </summary>
        Error = 2,
        /// <summary>
        /// Fatal: A problem that does not allow the software to continue running
        /// </summary>
        Fatal = 3
    }
}
