using AuroraLoader.Mods;
using Microsoft.Extensions.Configuration;
using System.Collections.Generic;
using System.Linq;

namespace AuroraLoader.Registry
{
    /// <summary>
    /// Must be initialized by calling Update()
    /// </summary>
    public class RemoteModRegistry : IRegistry
    {
        public IEnumerable<ModListing> ModListings;

        private readonly IConfiguration _configuration;
        private readonly MirrorRegistry _mirrorRegistry;

        public RemoteModRegistry(IConfiguration configuration, MirrorRegistry mirrorRegistry)
        {
            _configuration = configuration;
            _mirrorRegistry = mirrorRegistry;
        }

        public void Update(AuroraVersion version)
        {
            _mirrorRegistry.Update(version);
            var modListings = new List<ModListing>();
            foreach (var mirror in _mirrorRegistry.Mirrors)
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
    }
}
