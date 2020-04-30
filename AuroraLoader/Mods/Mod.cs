using System;
using System.Collections.Generic;
using System.IO;
using System.IO.Compression;
using System.Linq;
using System.Net;
using System.Text.Json.Serialization;
using System.Windows.Forms;
using Semver;

namespace AuroraLoader.Mods
{
    public class ModVersion
    {
        [JsonPropertyName("version")]
        [JsonConverter(typeof(SemVersionJsonConverter))]
        public SemVersion Version { get; set; }

        [JsonPropertyName("target_aurora_version")]
        [JsonConverter(typeof(ModCompatibilityVersionJsonConverter))]
        public ModCompabitilityVersion TargetCompatibilityVersion { get; set; }

        [JsonPropertyName("download_url")]
        public string DownloadUrl { get; set; }

        [JsonIgnore]
        public bool Installed { get; set; } = false;

        public bool WorksForVersion(SemVersion version) => TargetCompatibilityVersion.WorksForVersion(version);
    }

    public class Mod
    {
        [JsonPropertyName("name")]
        public string Name { get; set; }

        [JsonPropertyName("description")]
        public string Description { get; set; }

        [JsonPropertyName("status")]
        public ModStatus Status { get; set; }

        [JsonPropertyName("type")]
        public ModType Type { get; set; }

        [JsonPropertyName("url")]
        public string Url { get; set; }

        [JsonPropertyName("launch_command")]
        public string LaunchCommand { get; set; }

        [JsonPropertyName("configuration_file")]
        public string ConfigurationFile { get; set; }

        [JsonPropertyName("downloads")]
        public IList<ModVersion> Downloads { get; set; } = new List<ModVersion>();

        [JsonIgnore]
        public ModVersion LatestVersion => Downloads.OrderByDescending(v => v.Version).First();

        public ModVersion LatestVersionCompatibleWith(SemVersion auroraVersion) => Downloads.OrderByDescending(v => v.Version)
            .Where(v => v.WorksForVersion(auroraVersion))
            .First();

        [JsonIgnore]
        public ModVersion LatestInstalledVersion => Downloads.OrderByDescending(v => v.Version).Where(v => v.Installed).First();

        public bool Installed => LatestInstalledVersion != null;
        public bool CanBeUpdated => LatestVersion != null
                && LatestInstalledVersion != null
                && LatestVersion.Version.CompareByPrecedence(LatestInstalledVersion.Version) > 0;

        public ModConfiguration Installation { get; }
        public string ModFolder => Installation?.ModFolder;

        public ModListing Listing { get; }

        // TODO this is nasty but I'm lazy
        public string InstallText => $"Install {Name} {Listing?.LatestVersion} ({Type})";
        public string UpdateText => $"Update {Name} {Installation?.Version} ({Type}) to {Listing?.LatestVersion}";

        public Mod()
        {

        }

        public Mod(ModConfiguration modInstallation, ModListing modListing)
        {
            Installation = modInstallation;
            Listing = modListing;
        }

        public void InstallOrUpdate()
        {
            if (Installed && !CanBeUpdated)
            {
                throw new Exception($"{Name} is already up to date!");
            }

            Log.Debug($"Preparing caches in {Program.CacheDirectory}");
            var zip = Path.Combine(Program.ModDirectory, "update.current");
            if (File.Exists(zip))
            {
                File.Delete(zip);
            }

            var extract_folder = Path.Combine(Program.CacheDirectory, "Extract");
            if (Directory.Exists(extract_folder))
            {
                Directory.Delete(extract_folder, true);
            }
            Directory.CreateDirectory(extract_folder);

            Log.Debug($"Downloading from {Listing.LatestVersionUrl}");
            try
            {
                using (var client = new WebClient())
                {
                    client.DownloadFile(Listing.LatestVersionUrl, zip);
                }

                ZipFile.ExtractToDirectory(zip, extract_folder);

                var mod_version_folder = Path.Combine(Program.ModDirectory, Name, Listing.LatestVersion.ToString());
                if (Directory.Exists(mod_version_folder))
                {
                    Directory.Delete(mod_version_folder, true);
                }
                Directory.CreateDirectory(mod_version_folder);

                ZipFile.ExtractToDirectory(zip, mod_version_folder);
            }
            catch (Exception e)
            {
                Log.Error($"Failed while installing or updating {Name} from {Listing.LatestVersionUrl}", e);
                // TODO this is probably poor practice
                MessageBox.Show($"Failed to download {Name} from {Listing.LatestVersionUrl}!");
            }
            finally
            {
                // Cleanup
                File.Delete(zip);
                Directory.Delete(extract_folder, true);
            }
        }

        public bool WorksForVersion(AuroraVersion version)
        {
            return Installation.WorksForVersion(version);
        }
    }
}
