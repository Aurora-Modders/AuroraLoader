using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using Semver;

namespace AuroraLoader.Registry
{
    public class ModListing
    {
        public string ModName { get; }

        public ModType Type { get; private set; }

        public string UpdateUrl { get; }

        // <SemVersion, DownloadUrl>
        public IDictionary<SemVersion, string> VersionDownloadUrls { get; private set; }
        public SemVersion LatestVersion
        {
            get
            {
                return VersionDownloadUrls.Keys.Max();
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

        public void UpdateModListing()
        {
            var versions = new Dictionary<SemVersion, string>();
            using (var client = new WebClient())
            {
                try
                {
                    var response = client.DownloadString(UpdateUrl);
                    foreach (var update in Config.FromString(response))
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
