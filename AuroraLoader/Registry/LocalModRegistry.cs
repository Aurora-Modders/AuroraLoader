using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.IO;
using AuroraLoader.Mods;
using System.Text.Json;

namespace AuroraLoader.Registry
{
    /// <summary>
    /// Must be initialized by calling Update()
    /// </summary>
    public class LocalModRegistry : IRegistry
    {
        public IList<ModConfiguration> ModInstallations { get; private set; }

        public string ModDirectory => Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Mods");
        public string CacheDirectory => Path.Combine(Path.GetTempPath(), "auroraloader_cache");

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

        // Mods installed locally are identified by their mod.ini or mod.json file
        // This is known as their 'mod configuration' file.
        public void Update()
        {
            var mods = new List<ModConfiguration>();
            foreach (var file in Directory.EnumerateFiles(ModDirectory, "mod.ini", SearchOption.AllDirectories))
            {
                try
                {
                    mods.Add(ModConfigurationReader.ModConfigurationFromIni(file));
                }
                catch (Exception e)
                {
                    Log.Error($"Failed to parse mod data from {file}", e);
                }
            }

            // TODO JSON mod configurations not yet supported
            //foreach (var file in Directory.EnumerateFiles(ModDirectory, "mod.json", SearchOption.AllDirectories))
            //{
            //    try
            //    {
            //        var jsonString = File.ReadAllText(file);
            //        mods.Add(JsonSerializer.Deserialize<ModConfiguration>(jsonString));
            //    }
            //    catch (Exception e)
            //    {
            //        Log.Error($"Failed to parse mod data from {file}", e);
            //    }
            //}
            ModInstallations = mods;
        }
    }
}
