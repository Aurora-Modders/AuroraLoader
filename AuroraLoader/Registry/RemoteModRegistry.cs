using AuroraLoader.Registry;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;

namespace AuroraLoader
{
    public class RemoteModRegistry
    {
        public IEnumerable<ModListing> Mods;

        private readonly IConfiguration _configuration;
        private readonly MirrorRegistry _mirrorRegistry;

        public RemoteModRegistry(IConfiguration configuration, MirrorRegistry mirrorRegistry)
        {
            _configuration = configuration;
            _mirrorRegistry = mirrorRegistry;
            UpdateModeListings();
        }

        public void UpdateModeListings()
        {
            foreach (var mirror in _mirrorRegistry.Mirrors)
            {
                try
                {
                    mirror.UpdateModListings();
                }
                catch (Exception e)
                {
                    Log.Error($"Failed to update mod listings from {mirror.ModsUrl}", e);
                }
            }

            Mods = _mirrorRegistry.Mirrors.Select(mirror => mirror.ModDirectory).SelectMany(mods => mods);
        }
    }
}
