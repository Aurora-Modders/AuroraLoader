using AuroraLoader.Registry;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;

namespace AuroraLoader.Registry
{
    /// <summary>
    /// Must be initialized by calling Update()
    /// </summary>
    public class ModRegistry : Registry
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

            // TODO Create Mods from union(local + remote)
        }
    }
}
