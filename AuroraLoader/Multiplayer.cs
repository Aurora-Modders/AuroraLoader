using NAudio.Midi;
using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.IO;
using System.Linq;
using System.Text;

namespace AuroraLoader
{
    class Multiplayer
    {
        public static List<string> GetCurrentMultiplayerGames()
        {
            var folder = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Multiplayer");
            var dirs = Directory.EnumerateDirectories(folder).Select(dir => new DirectoryInfo(dir).Name).ToList();

            return dirs;
        }

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
