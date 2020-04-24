using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security.Cryptography;

namespace AuroraLoader
{
    public class GameInstallation
    {
        public readonly GameVersion InstalledVersion;
        public readonly string ExecutableLocation;

        public GameInstallation(GameVersion version, string executable)
        {
            InstalledVersion = version;
            ExecutableLocation = executable;
            
        }
    }
}
