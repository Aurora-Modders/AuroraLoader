using NAudio.Midi;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace AuroraLoader
{
    class Multiplayer
    {
        public static void NewGame(string name)
        {
            var folder = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Multiplayer", name);
            Installer.CopyClean(folder);

            // TODO run actual setup
        }

        public static void ContinueGame(string name)
        {
            var folder = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Multiplayer", name);

            // TODO run actual game
        }
    }
}
