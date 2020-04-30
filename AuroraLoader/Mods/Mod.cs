using System;
using System.IO;
using System.IO.Compression;
using System.Net;
using System.Windows.Forms;

namespace AuroraLoader.Mods
{
    public class Mod
    {
        public string Name => Listing?.ModName ?? Installation.Name;
        public ModType Type => Listing?.Type ?? Installation?.Type ?? ModType.EXE;
        public bool Installed => Installation != null;
        public bool CanBeUpdated => Listing != null && Installed && Listing.LatestVersion.CompareByPrecedence(Installation.HighestInstalledVersion) > 0;

        public bool Configurable => Installation?.ModInternalConfigFile != null;

        public ModConfiguration Installation { get; }
        public ModListing Listing { get; }

        // TODO this is nasty but I'm lazy
        public string InstallText => $"Install {Name} {Listing?.LatestVersion} ({Type})";
        public string UpdateText => $"Update {Name} {Installation?.Version} ({Type}) to {Listing?.LatestVersion}";

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
    }
}
