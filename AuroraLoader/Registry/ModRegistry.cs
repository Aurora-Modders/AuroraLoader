using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.Json;
using AuroraLoader.Mods;
using Microsoft.Extensions.Configuration;

namespace AuroraLoader.Registry
{
    /// <summary>
    /// Must be initialized by calling Update()
    /// </summary>
    public class ModRegistry
    {
        // TODO ensure that the list is unique
        public IEnumerable<Mod> Mods { get; private set; }

        private readonly IConfiguration _configuration;

        public IEnumerable<ModListing> ModListings;
        public IList<ModConfiguration> ModInstallations;

        public IList<Mirror> Mirrors { get; private set; }

        public ModRegistry(IConfiguration configuration)
        {
            _configuration = configuration;
            Mirrors = ModConfigurationReader.GetMirrorsFromIni(_configuration);
        }

        public void Update()
        {
            var localMods = GetLocalMods();
            var remoteMods = UpdateModListings();

            var mods = new List<Mod>();
            /////
            var installedMods = new List<ModConfiguration>();
            foreach (var modInstallation in ModInstallations)
            {
                installedMods.Add(modInstallation);
            }

            foreach (var modListing in ModListings)
            {
                var installedMod = installedMods.FirstOrDefault(mod => mod.Name == modListing.ModName);
                if (installedMod != null)
                {
                    mods.Add(new Mod(installedMod, modListing));
                    installedMods.Remove(installedMod);
                }
                else
                {
                    mods.Add(new Mod(null, modListing));
                }
            }

            // Handle mods we couldn't find a listing for in the registry
            foreach (var installedMod in installedMods)
            {
                mods.Add(new Mod(installedMod, null));
            }
            Mods = mods;
        }

        // Mods installed locally are identified by their mod.ini or mod.json file
        // This is known as their 'mod configuration' file.
        private IList<Mod> GetLocalMods()
        {
            var mods = new List<Mod>();
            foreach (var file in Directory.EnumerateFiles(Program.ModDirectory, "mod.json", SearchOption.AllDirectories))
            {
                try
                {
                    var rawString = File.ReadAllText(file);
                    var newMod = JsonSerializer.Deserialize<Mod>(rawString);

                    if (mods.Any(mod => mod.Name == newMod.Name))
                    {
                        var existingMod = mods.Single(mod => mod.Name == newMod.Name);
                        var updatedDownloadList = existingMod.Downloads.ToList();
                        updatedDownloadList.AddRange(newMod.Downloads.Where(nd => !existingMod.Downloads.Any(ed => ed.Version == nd.Version)));
                        existingMod.Downloads = updatedDownloadList;
                    }
                    else
                    {
                        mods.Add(newMod);
                    }
                }
                catch (Exception e)
                {
                    Log.Error($"Failed to parse mod data from {file}", e);
                }
            }
            return mods;
        }

        public void UpdateAuroraLoader()
        {
            var auroraLoaderMod = Mods.SingleOrDefault(mod => mod.Name == "AuroraLoader");
            if (auroraLoaderMod == null)
            {
                throw new Exception("Could not find AuroraLoader mod");
            }

            auroraLoaderMod.InstallOrUpdate();
            auroraLoaderMod = Mods.Single(mod => mod.Name == "AuroraLoader");
            File.Copy(Path.Combine(auroraLoaderMod.ModFolder, "AuroraLoader.exe"), Path.Combine(Program.AuroraLoaderExecutableDirectory, "AuroraLoader_new.exe"), true);
            foreach (var file in new string[]
            {
                "mod.ini",
                "mirrors.ini",
                "aurora_versions.ini"
            })
            {
                try
                {
                    File.Copy(Path.Combine(auroraLoaderMod.ModFolder, file), Path.Combine(Program.AuroraLoaderExecutableDirectory, file), true);
                }
                catch (Exception e)
                {
                    Log.Error($"Failed to copy {file} while updating Aurora", e);
                }
            }
        }

        private IList<ModListing> UpdateModListings()
        {
            var modListings = new List<ModListing>();
            foreach (var mirror in Mirrors)
            {
                foreach (var mirrorListing in mirror.ModListings)
                {
                    // If we already have a listing for this mod from another mirror
                    if (modListings.Any(l => l.ModName == mirrorListing.ModName))
                    {
                        // Update it if this mirror has a more recent version
                        var match = modListings.Single(l => l.ModName == mirrorListing.ModName);
                        if (modListings.Single(l => l.ModName == mirrorListing.ModName).LatestVersion.CompareTo(mirrorListing.LatestVersion) < 0)
                        {
                            match = new ModListing(match.ModName, mirrorListing.LatestVersionUrl);
                        }
                    }
                    else
                    {
                        // Add the listing
                        modListings.Add(mirrorListing);
                    }
                }
            }
            return modListings;
        }
    }
}
