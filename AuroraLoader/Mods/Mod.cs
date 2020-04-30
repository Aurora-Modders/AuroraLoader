using System;
using System.Collections.Generic;
using System.IO;
using System.IO.Compression;
using System.Linq;
using System.Net;
using System.Text.Json.Serialization;
using System.Windows.Forms;

namespace AuroraLoader.Mods
{
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



        // Helper props
        [JsonIgnore]
        public ModVersion LatestVersion => Downloads.OrderByDescending(v => v.Version).FirstOrDefault();

        public ModVersion LatestVersionCompatibleWith(AuroraVersion auroraVersion) => Downloads.OrderByDescending(v => v.Version)
            .Where(v => v.WorksForVersion(auroraVersion))
            .FirstOrDefault();

        [JsonIgnore]
        public ModVersion LatestInstalledVersion => Downloads.OrderByDescending(v => v.Version)
            .Where(v => v.Installed)
            .FirstOrDefault();
        public ModVersion LatestInstalledVersionCompatibleWith(AuroraVersion auroraVersion) => Downloads.OrderByDescending(v => v.Version)
            .Where(v => v.Installed && v.WorksForVersion(auroraVersion))
            .FirstOrDefault();

        public bool Installed => LatestInstalledVersion != null;
        public bool CanBeUpdated => LatestVersion != null
                && LatestInstalledVersion != null
                && LatestVersion.Version.CompareByPrecedence(LatestInstalledVersion.Version) > 0;

        public string ModFolder => Path.Combine(Program.ModDirectory, Name);
        public string ModVersionFolder(ModVersion modVersion) => Path.Combine(ModFolder, modVersion.Version.ToString());

        public void InstallVersion(ModVersion modVersion)
        {
            if (modVersion.Installed)
            {
                throw new Exception($"{Name} {modVersion.Version} is already installed");
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

            Log.Debug($"Downloading from {modVersion.DownloadUrl}");
            try
            {
                using (var client = new WebClient())
                {
                    client.DownloadFile(modVersion.DownloadUrl, zip);
                }

                ZipFile.ExtractToDirectory(zip, extract_folder);

                var mod_version_folder = Path.Combine(Program.ModDirectory, Name, modVersion.Version.ToString());
                if (Directory.Exists(mod_version_folder))
                {
                    Directory.Delete(mod_version_folder, true);
                }
                Directory.CreateDirectory(mod_version_folder);

                ZipFile.ExtractToDirectory(zip, mod_version_folder);

                modVersion.Installed = true;
            }
            catch (Exception e)
            {
                Log.Error($"Failed while installing or updating {Name} from {modVersion.DownloadUrl}", e);
                // TODO this is probably poor practice
                MessageBox.Show($"Failed to download {Name} from {modVersion.DownloadUrl}!");
            }
            finally
            {
                // Cleanup
                File.Delete(zip);
                Directory.Delete(extract_folder, true);
            }
        }
    }
}
