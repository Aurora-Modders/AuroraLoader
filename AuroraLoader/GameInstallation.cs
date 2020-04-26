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
        public readonly AuroraVersion InstalledVersion;
        public readonly string ExecutableLocation;

        public GameInstallation(AuroraVersion version, string executable)
        {
            InstalledVersion = version;
            ExecutableLocation = executable;

        }
    }
}