using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
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

        public Mod AuroraLoaderMod => Mods.SingleOrDefault(mod => mod.Name == "AuroraLoader");

        private readonly IConfiguration _configuration;

        public IList<string> Mirrors { get; private set; }

        public ModRegistry(IConfiguration configuration)
        {
            _configuration = configuration;
            Mirrors = ModConfigurationReader.GetMirrorsFromIni(_configuration);
        }

        public void Update()
        {
            var mods = new List<Mod>();
            mods.AddRange(GetLocalMods());
            mods.AddRange(UpdateModListings());
            Mods = MergeDuplicateMods(mods);
            // TODO update cache
        }

        // Mods installed locally are identified by their mod.ini or mod.json file
        // This is known as their 'mod configuration' file.
        private IList<Mod> GetLocalMods()
        {
            var mods = new List<Mod>();
            foreach (var modJsonFile in Directory.EnumerateFiles(Program.ModDirectory, "mod.json", SearchOption.AllDirectories))
            {
                try
                {
                    var newMod = JsonSerializer.Deserialize<Mod>(File.ReadAllText(modJsonFile), new JsonSerializerOptions()
                    {
                        ReadCommentHandling = JsonCommentHandling.Skip
                    });
                    foreach (var modVersion in newMod.Downloads)
                    {
                        var modVersionDirectory = Path.Combine(Program.ModDirectory, newMod.Name, modVersion.Version.ToString());
                        if (Directory.Exists(modVersionDirectory))
                        {
                            modVersion.Installed = true;
                        }
                    }

                    mods.Add(newMod);
                }
                catch (Exception e)
                {
                    Log.Error($"Failed to parse mod data from {modJsonFile}", e);
                }
            }
            return mods;
        }

        private IList<Mod> UpdateModListings()
        {
            var remoteMods = new List<Mod>();
            foreach (var mirror in Mirrors)
            {
                var modLocationsUrl = Path.Combine(mirror, "mod_locations.json");
                try
                {
                    remoteMods.AddRange(GetModsAtMirror(modLocationsUrl));
                }
                catch (Exception e)
                {
                    // TODO evaluate throwing or something
                    Log.Error($"Failed to download mod locations from {modLocationsUrl}", e);
                }

            }
            return remoteMods;
        }

        private IList<Mod> GetModsAtMirror(string mirrorUrl)
        {
            var modsAtMirror = new List<Mod>();
            using (var client = new WebClient())
            {
                var modJsonUrls = JsonSerializer.Deserialize<IList<string>>(client.DownloadString(mirrorUrl));
                foreach (var modJsonUrl in modJsonUrls)
                {
                    try
                    {
                        var response = client.DownloadString(modJsonUrl);
                        modsAtMirror.Add(JsonSerializer.Deserialize<Mod>(response, new JsonSerializerOptions()
                        {
                            ReadCommentHandling = JsonCommentHandling.Skip
                        }));
                    }
                    catch (Exception e)
                    {
                        // TODO evaluate throwing or something
                        Log.Error($"Failed to download {modJsonUrl}", e);
                    }
                }
            }
            return modsAtMirror;
        }

        private IList<Mod> MergeDuplicateMods(IList<Mod> potentiallyContainsDuplicates)
        {
            var uniqueMods = new List<Mod>();
            foreach (var newMod in potentiallyContainsDuplicates)
            {
                var existingMod = uniqueMods.SingleOrDefault(um => um.Name == newMod.Name);
                if (existingMod != null)
                {
                    var updatedDownloadList = existingMod.Downloads.ToList();
                    updatedDownloadList.AddRange(newMod.Downloads.Where(nd => !existingMod.Downloads.Any(ed => ed.Version == nd.Version)));
                    existingMod.Downloads = updatedDownloadList;
                }
                else
                {
                    uniqueMods.Add(newMod);
                }
            }
            return uniqueMods;
        }

        public void UpdateAuroraLoader()
        {
            if (AuroraLoaderMod == null)
            {
                throw new Exception("AuroraLoader mod not loaded");
            }
            var targetVersion = AuroraLoaderMod.LatestVersion;

            AuroraLoaderMod.InstallVersion(targetVersion);
            File.Copy(Path.Combine(AuroraLoaderMod.ModVersionFolder(AuroraLoaderMod.LatestVersion), "AuroraLoader.exe"), Path.Combine(Program.AuroraLoaderExecutableDirectory, "AuroraLoader_new.exe"), true);
            foreach (var file in new string[]
            {
                "mod.json",
                "mirrors.ini",
                "aurora_versions.ini"
            })
            {
                try
                {
                    File.Copy(Path.Combine(AuroraLoaderMod.ModVersionFolder(AuroraLoaderMod.LatestVersion), file), Path.Combine(Program.AuroraLoaderExecutableDirectory, file), true);
                }
                catch (Exception e)
                {
                    Log.Error($"Failed to copy {file} while updating Aurora", e);
                }
            }
        }
    }
}
