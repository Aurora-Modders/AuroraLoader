using Microsoft.Extensions.Configuration;
using System.Collections.Generic;
using System.Linq;

namespace AuroraLoader.Registry
{
    /// <summary>
    /// Must be initialized by calling Update()
    /// </summary>
    public class ModRegistry : IRegistry
    {
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
            // TODO Create Mods from union(local + remote)
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
    }
}
