using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.IO;

namespace AuroraLoader.Registry
{
    /// <summary>
    /// Must be initialized by calling Update()
    /// </summary>
    public class LocalModRegistry : IRegistry
    {
        public IList<ModInstallation> ModInstallations { get; private set; }

        public string ModDirectory => Path.Combine(Path.GetTempPath(), "Mods");

        private readonly IConfiguration _configuration;

        public LocalModRegistry(IConfiguration configuration)
        {
            _configuration = configuration;
            EnsureModDirectoryExists();
        }

        private void EnsureModDirectoryExists()
        {
            if (!Directory.Exists(ModDirectory))
            {
                Directory.CreateDirectory(ModDirectory);
            }
        }

        public void Update()
        {
            var mods = new List<ModInstallation>();
            var dir = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Mods");
            foreach (var file in Directory.EnumerateFiles(dir, "mod.ini", SearchOption.AllDirectories))
            {
                try
                {
                    mods.Add(ModInstallation.Parse(file));
                }
                catch (Exception e)
                {
                    Log.Error($"Failed to parse mod data from {file}", e);
                }
            }
            ModInstallations = mods;
        }
    }
}
