using System;

namespace AuroraLoader
{
    // TODO cutover to log4net (etc) rather than rewriting the wheel here
    static class Log
    {
        public static void Debug(string message)
        {
            System.Diagnostics.Debug.WriteLine(message);
        }

        public static void Error(string message, Exception e)
        {
            Debug(message + "\n" + e.Message + "\n" + e.StackTrace);
        }
    }
}
