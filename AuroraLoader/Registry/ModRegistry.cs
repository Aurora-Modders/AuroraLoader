using System;
using System.Collections.Generic;
using System.IO;
using System.IO.Compression;
using System.Linq;
using System.Net;
using System.Windows.Forms;
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
        private readonly AuroraVersionRegistry _auroraVersionRegistry;

        public IEnumerable<ModListing> ModListings;
        public IList<ModConfiguration> ModInstallations;

        public IList<Mirror> Mirrors { get; private set; }

        public ModRegistry(IConfiguration configuration, AuroraVersionRegistry auroraVersionRegistry)
        {
            _configuration = configuration;
            _auroraVersionRegistry = auroraVersionRegistry;
        }

        public void Update()
        {
            UpdateModInstallationData();
            UpdateModListings();

            var mods = new List<Mod>();

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

            try
            {
                // Specially load in AuroraLoader itself
                var auroraLoaderModInstallation = ModInstallations.Single(i => i.Name == "AuroraLoader");
                Log.Debug("Installed loader: " + auroraLoaderModInstallation.Version);
                var auroraLoaderModListing = new ModListing(auroraLoaderModInstallation.Name, auroraLoaderModInstallation.Updates);
                mods.Add(new Mod(auroraLoaderModInstallation, auroraLoaderModListing));
                installedMods.Remove(auroraLoaderModInstallation);
            }
            catch (Exception exc)
            {
                Log.Error($"Failed while loading AuroraLoader installation", exc);
            }


            // Handle mods we couldn't find a listing for in the registry
            foreach (var installedMod in installedMods)
            {
                mods.Add(new Mod(installedMod, null));
            }
            Mods = mods;
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
            File.Copy(Path.Combine(auroraLoaderMod.Installation.ModFolder, "AuroraLoader.exe"), Path.Combine(Program.AuroraLoaderExecutableDirectory, "AuroraLoader_new.exe"), true);
            foreach (var file in new string[]
            {
                "mod.ini",
                "mirrors.ini",
                "aurora_versions.ini"
            })
            {
                try
                {
                    File.Copy(Path.Combine(auroraLoaderMod.Installation.ModFolder, file), Path.Combine(Program.AuroraLoaderExecutableDirectory, file), true);
                }
                catch (Exception e)
                {
                    Log.Error($"Failed to copy {file} while updating Aurora", e);
                }
            }
        }

        private void UpdateModListings()
        {
            Mirrors = ModConfigurationReader.GetMirrorsFromIni(_configuration);
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
            ModListings = modListings;
        }

        // Mods installed locally are identified by their mod.ini or mod.json file
        // This is known as their 'mod configuration' file.
        private void UpdateModInstallationData()
        {
            var mods = new List<ModConfiguration>();

            foreach (var file in Directory.EnumerateFiles(Program.ModDirectory, "mod.ini", SearchOption.AllDirectories))
            {
                try
                {
                    var newMod = ModConfigurationReader.ModConfigurationFromIni(file);
                    if (mods.Any(mod => mod.Name == newMod.Name))
                    {
                        var existingMod = mods.Single(mod => mod.Name == newMod.Name);
                        if (newMod.Version.CompareByPrecedence(existingMod.HighestInstalledVersion) > 0)
                        {
                            existingMod.HighestInstalledVersion = newMod.Version;
                        }
                    }

                    // TODO get rid of this dependency
                    if (newMod.WorksForVersion(_auroraVersionRegistry.CurrentAuroraVersion))
                    {
                        if (mods.Any(mod => mod.Name == newMod.Name))
                        {
                            var existingMod = mods.Single(mod => mod.Name == newMod.Name);
                            if (newMod.Version.CompareTo(existingMod.Version) > 0)
                            {
                                mods.Remove(existingMod);
                                mods.Add(newMod);
                            }
                        }
                        else
                        {
                            mods.Add(newMod);
                        }
                    }
                    else if (mods.Count(m => m.Name == newMod.Name) == 0)
                    {
                        mods.Add(newMod);
                    }

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
