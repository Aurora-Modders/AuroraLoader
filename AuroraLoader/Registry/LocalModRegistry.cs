using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.IO;
using AuroraLoader.Mods;
using System.Linq;
using System.Diagnostics;
using System.Xml;

namespace AuroraLoader.Registry
{
    /// <summary>
    /// Must be initialized by calling Update()
    /// </summary>
    public class LocalModRegistry : IRegistry
    {
        public IList<ModConfiguration> ModInstallations { get; private set; }

        public string ModDirectory => Path.Combine(Program.AuroraLoaderExecutableDirectory, "Mods");
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
            // Load the mod configuration for AuroraLoader itself
            if (File.Exists(Path.Combine(Program.AuroraLoaderExecutableDirectory, "mod.ini")))
            {
                var auroraLoader = ModConfigurationReader.ModConfigurationFromIni(Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "mod.ini"));
                var auroraLoaderModDirectory = Path.Combine(ModDirectory, auroraLoader.Name, auroraLoader.Version.ToString());
                if (!Directory.Exists(Path.Combine(ModDirectory, auroraLoader.Name, auroraLoader.Version.ToString())))
                {
                    Directory.CreateDirectory(auroraLoaderModDirectory);
                    File.Copy(Path.Combine(Program.AuroraLoaderExecutableDirectory, "mod.ini"), Path.Combine(auroraLoaderModDirectory, "mod.ini"), true);
                    File.Copy(Path.Combine(Program.AuroraLoaderExecutableDirectory, "AuroraLoader.exe"), Path.Combine(auroraLoaderModDirectory, "AuroraLoader.Exe"), true);
                }
            }

            foreach (var file in Directory.EnumerateFiles(ModDirectory, "mod.ini", SearchOption.AllDirectories))
            {
                try
                {
                    var newMod = ModConfigurationReader.ModConfigurationFromIni(file);
                    mods.Add(newMod);
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

        internal object Single(Func<object, bool> p)
        {
            throw new NotImplementedException();
        }
    }
}
