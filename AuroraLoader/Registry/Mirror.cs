﻿using Microsoft.Extensions.Configuration;
using Semver;
using System;
using System.Collections.Generic;
using System.IO;
using System.Net;

namespace AuroraLoader.Registry
{
    public class Mirror
    {
        public string RootUrl { get; }
        public string VersionsUrl => Path.Combine(RootUrl, "aurora_versions.txt");
        public string ModsUrl => Path.Combine(RootUrl, "Mods/mods.txt");

        public IList<AuroraVersion> KnownAuroraVersions { get; private set; }

        public IList<ModListing> ModListings { get; private set; }

        private readonly IConfiguration _configuration;

        public Mirror(IConfiguration configuration, string rootUrl)
        {
            _configuration = configuration;
            RootUrl = rootUrl;
            UpdateModListings();
            UpdateKnownAuroraVersions();
        }

        public void UpdateKnownAuroraVersions()
        {
            var knownVersions = new List<AuroraVersion>();
            using (var client = new WebClient())
            {
                try
                {
                    var response = client.DownloadString(VersionsUrl);
                    foreach (var kvp in Config.FromString(response))
                    {
                        knownVersions.Add(new AuroraVersion(SemVersion.Parse(kvp.Key), kvp.Value));
                    }
                }
                catch (Exception e)
                {
                    Log.Error($"Failed to download Aurora version listing from {VersionsUrl}", e);
                }

            }
            // TODO update the locally cached file
            KnownAuroraVersions = knownVersions;
        }

        /*
		 * Sample mods.txt (mod directory) content:
		 * AuroraMod=https://raw.githubusercontent.com/Aurora-Modders/AuroraMods/master/Mods/AuroraMod/updates.txt
		 * AuroraElectrons=https://raw.githubusercontent.com/Aurora-Modders/AuroraMods/master/Mods/AuroraElectrons/updates.txt
		 * A4xCalc=https://raw.githubusercontent.com/Aurora-Modders/AuroraMods/master/Mods/A4xCalc/updates.txt
		 * 
		 * TODO handle JSON
		 * See https://github.com/Aurora-Modders/AuroraMods/blob/master/mods.json for an example
		 */
        public void UpdateModListings()
        {
            var modListingAtMirror = new List<ModListing>();
            using (var client = new WebClient())
            {
                try
                {
                    var response = client.DownloadString(ModsUrl);
                    foreach (var kvp in Config.FromString(response))
                    {

                        modListingAtMirror.Add(new ModListing(kvp.Key, kvp.Value));

                    }
                }
                catch (Exception e)
                {
                    Log.Error($"Failed to download mod listing from {ModsUrl}", e);
                }
            }
            ModListings = modListingAtMirror;
        }
    }
}