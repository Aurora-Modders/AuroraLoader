using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text.Json.Serialization;
using Semver;

namespace AuroraLoader.Mods
{
    public class ModListing
    {
        [JsonPropertyName("Name")]
        public string ModName { get; set; }

        [JsonPropertyName("Type")]
        public ModType Type { get; set; }

        [JsonPropertyName("Description")]
        public string Description { get; set; }

        [JsonPropertyName("Updates")]
        public string UpdateUrl { get; set; }

        // <SemVersion, DownloadUrl>
        private IDictionary<SemVersion, string> VersionDownloadUrls = new Dictionary<SemVersion, string>();

        [JsonIgnore]
        public SemVersion LatestVersion
        {
            get
            {
                return VersionDownloadUrls.Keys.Max();
            }
        }

        [JsonIgnore]
        public string LatestVersionUrl
        {
            get
            {
                return VersionDownloadUrls[LatestVersion];
            }
        }

        /*
		 * Sample updates.txt content:
		 * 1.8.0-5=https://raw.githubusercontent.com/Aurora-Modders/AuroraMods/master/Mods/AuroraMod/AuroraMod-1.8.0-5.zip
		 */
        public ModListing(string modName, string updateUrl)
        {
            ModName = modName;
            UpdateUrl = updateUrl;
            UpdateModListing();
        }

        // Needed for deserializing from JSON
        internal ModListing()
        {

        }

        public void UpdateModListing()
        {
            var versions = new Dictionary<SemVersion, string>();
            using (var client = new WebClient())
            {
                try
                {
                    var response = client.DownloadString(UpdateUrl);
                    foreach (var update in ModConfigurationReader.FromKeyValueString(response))
                    {
                        versions[SemVersion.Parse(update.Key)] = update.Value;
                    }
                }
                catch (Exception e)
                {
                    Log.Error($"Failed to download listing for {ModName} from {UpdateUrl}", e);
                }

            }
            VersionDownloadUrls = versions;
        }
    }
}
