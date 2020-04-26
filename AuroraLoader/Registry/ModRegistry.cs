using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.IO;
using System.IO.Compression;
using System.Linq;
using System.Net;

namespace AuroraLoader.Registry
{
    /// <summary>
    /// Must be initialized by calling Update()
    /// </summary>
    public class ModRegistry : IRegistry
    {
        // TODO ensure that the list is unique
        public IEnumerable<Mod> Mods;

        private readonly IConfiguration _configuration;
        private readonly LocalModRegistry _localRegistry;
        private readonly RemoteModRegistry _remoteRegistry;

        public ModRegistry(IConfiguration configuration, LocalModRegistry localRegistry, RemoteModRegistry remoteRegistry)
        {
            _configuration = configuration;
            _localRegistry = localRegistry;
            _remoteRegistry = remoteRegistry;
        }

        public void Update()
        {
            _localRegistry.Update();
            _remoteRegistry.Update();

            var mods = new List<Mod>();
            foreach (var modInstallation in _localRegistry.ModInstallations)
            {
                mods.Add(new Mod(modInstallation, null));
            }

            foreach (var modListing in _remoteRegistry.ModListings)
            {
                var installedMod = mods.FirstOrDefault(mod => mod.Name == modListing.ModName);
                if (installedMod != null)
                {
                    installedMod = new Mod(installedMod.Installation, modListing);
                }
                else
                {
                    mods.Add(new Mod(null, modListing));
                }
            }
            Mods = mods;
        }

        // TODO I would prefer to handle caching withing LocalModRegistry
        public void InstallOrUpdate(Mod mod)
        {
            if (mod.Installed && !mod.CanBeUpdated)
            {
                throw new Exception($"{mod.Name} is already up to date!");
            }

            Log.Debug($"Preparing caches in {_localRegistry.ModDirectory}");
            var zip = Path.Combine(_localRegistry.ModDirectory, "update.current");
            if (File.Exists(zip))
            {
                File.Delete(zip);
            }

            var extract_folder = Path.Combine(_localRegistry.ModDirectory, "Extract");
            if (Directory.Exists(extract_folder))
            {
                Directory.Delete(extract_folder, true);
            }
            Directory.CreateDirectory(extract_folder);

            Log.Debug($"Downloading from {mod.Listing.LatestVersionUrl}");
            try
            {
                using (var client = new WebClient())
                {
                    client.DownloadFile(mod.Listing.LatestVersionUrl, zip);
                }

                ZipFile.ExtractToDirectory(zip, extract_folder);

                var mod_version_folder = Path.Combine(_localRegistry.ModDirectory, mod.Name, mod.Listing.LatestVersion.ToString());
                if (Directory.Exists(mod_version_folder))
                {
                    Directory.Delete(mod_version_folder, true);
                }
                Directory.CreateDirectory(mod_version_folder);

                ZipFile.ExtractToDirectory(zip, mod_version_folder);
            }
            catch (Exception e)
            {
                Log.Error($"Failed while installing or updating {mod.Name}", e);
            }
            finally
            {
                // Cleanup
                File.Delete(zip);
                Directory.Delete(extract_folder, true);
            }

            // Update mod in registry
            _localRegistry.Update();
        }
    }
}
