﻿using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using AuroraLoader.Mods;
using System.Linq;
using System.Text.Json;

namespace AuroraLoader.Registry
{
    /// <summary>
    /// a mirror is defined by the presence of the following: mods.json, aurora_versions.ini, aurora_files.ini
    /// </summary>
    public class Mirror
    {
        public string RootUrl { get; }
        public string VersionsUrl => Path.Combine(RootUrl, "aurora_versions.ini");

        public string OldModsUrl => Path.Combine(RootUrl, "Mods/mods.txt");
        public string ModsUrl => Path.Combine(RootUrl, "mods.json");
        // TODO currently unused
        public string AuroraFilesUrl => Path.Combine(RootUrl, "aurora_files.ini");

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
            using (var client = new WebClient())
            {
                try
                {
                    var response = client.DownloadString(VersionsUrl);
                    KnownAuroraVersions = ModConfigurationReader.AuroraVersionsFromString(response).ToList();
                }
                catch (Exception e)
                {
                    Log.Error($"Failed to download Aurora version listing from {VersionsUrl}", e);
                }

            }
            // TODO update the locally cached file
        }

        /*
		 * Sample mods.txt (mod directory) content:
		 * AuroraMod=https://raw.githubusercontent.com/Aurora-Modders/AuroraMods/master/Mods/AuroraMod/updates.txt
		 * AuroraElectrons=https://raw.githubusercontent.com/Aurora-Modders/AuroraMods/master/Mods/AuroraElectrons/updates.txt
		 * A4xCalc=https://raw.githubusercontent.com/Aurora-Modders/AuroraMods/master/Mods/A4xCalc/updates.txt
		 * 
		 * See https://github.com/Aurora-Modders/AuroraMods/blob/master/mods.json for a JSON example
		 */
        public void UpdateModListings()
        {
            var modListingAtMirror = new List<ModListing>();
            using (var client = new WebClient())
            {
                try
                {
                    var response = client.DownloadString(ModsUrl);

                    modListingAtMirror = JsonSerializer.Deserialize<List<ModListing>>(response);
                    foreach (var mod in modListingAtMirror)
                    {
                        mod.UpdateModListing();
                    }
                }
                catch (Exception e)
                {
                    Log.Error($"Failed to download mod listing from {ModsUrl}", e);
                }

                if (modListingAtMirror == null)
                {
                    // Try looking for the older mods.txt format
                    try
                    {
                        var response = client.DownloadString(OldModsUrl);

                        foreach (var kvp in ModConfigurationReader.FromKeyValueString(response))
                        {
                            modListingAtMirror.Add(new ModListing(kvp.Key, kvp.Value));
                        }
                    }
                    catch (Exception e)
                    {
                        Log.Error($"Not enough MSP to implement repairs at {ModsUrl}", e);
                    }
                }
            }
            ModListings = modListingAtMirror;
        }
    }
}
