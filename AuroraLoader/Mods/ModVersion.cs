using System;
using System.IO;
using System.IO.Compression;
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

        [JsonIgnore]
        public string InstallationPath => Path.Combine(Mod.ModFolder, Version.ToString());

        [JsonIgnore]
        public Mod Mod { get; set; }

        public bool WorksForVersion(AuroraVersion auroraVersion) => TargetCompatibilityVersion.WorksForVersion(auroraVersion.Version);

        public void Install()
        {
            if (Installed)
            {
                throw new Exception($"{Mod.Name} {Version} is already installed");
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

            Log.Debug($"Downloading from {DownloadUrl}");
            try
            {
                using (var client = new WebClient())
                {
                    client.DownloadFile(DownloadUrl, zip);
                }

                ZipFile.ExtractToDirectory(zip, extract_folder);

                if (Directory.Exists(InstallationPath))
                {
                    Directory.Delete(InstallationPath, true);
                }
                Directory.CreateDirectory(InstallationPath);

                ZipFile.ExtractToDirectory(zip, InstallationPath);

                Installed = true;
                Mod.UpdateCache();
            }
            catch (Exception e)
            {
                var message = $"Failed while installing or updating {Mod.Name} {Version} to {InstallationPath} from {DownloadUrl}";
                Log.Error(message, e);
                MessageBox.Show(message);
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
