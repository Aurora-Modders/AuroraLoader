using System;
using System.IO;

namespace AuroraLoader
{
    static class Log
    {
        public static void Clear()
        {
            var file = Path.Combine(Program.AuroraLoaderExecutableDirectory, "AuroraLoader.log");
            if (File.Exists(file))
            {
                File.Delete(file);
            }
        }

        public static void Debug(string message)
        {
            System.Diagnostics.Debug.WriteLine(message);
            var file = Path.Combine(Program.AuroraLoaderExecutableDirectory, "AuroraLoader.log");
            File.AppendAllText(file, message + "\n");
        }

        public static void Error(string message, Exception e)
        {
            Debug(message + "\n" + e.Message + "\n" + e.StackTrace);
        }
    }
}
