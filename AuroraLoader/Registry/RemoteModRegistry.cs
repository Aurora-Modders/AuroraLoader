using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;

namespace AuroraLoader.Registry
{
    /// <summary>
    /// Must be initialized by calling Update()
    /// </summary>
    public class RemoteModRegistry : Registry
    {
        public IEnumerable<ModListing> ModListings;

        private readonly IConfiguration _configuration;
        private readonly MirrorRegistry _mirrorRegistry;

        public RemoteModRegistry(IConfiguration configuration, MirrorRegistry mirrorRegistry)
        {
            _configuration = configuration;
            _mirrorRegistry = mirrorRegistry;
        }

        public void Update()
        {
            _mirrorRegistry.Update();
            ModListings = _mirrorRegistry.Mirrors.Select(mirror => mirror.ModListings).SelectMany(mods => mods);
        }
    }
}
