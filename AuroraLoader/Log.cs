using System;
using System.Collections.Generic;
using System.Text;

namespace AuroraLoader
{
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
