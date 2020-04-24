using Semver;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.IO.Compression;
using System.Linq;
using System.Net;

namespace AuroraLoader
{
    static class Updater
    {
        public static Dictionary<Mod, string> GetUpdateUrls(List<Mod> mods)
        {
            var updaters = new Dictionary<string, Mod>();
            
            foreach (var mod in mods.Where(m => m.Updates != null))
            {
                if (!updaters.ContainsKey(mod.Name))
                {
                    updaters.Add(mod.Name, mod);
                }
                else if (mod.Version.CompareByPrecedence(updaters[mod.Name].Version) == 1)
                {
                    updaters[mod.Name] = mod;
                }
            }

            var urls = new Dictionary<Mod, string>();
            var versions = new Dictionary<Mod, SemVersion>();

            using (var client = new WebClient())
            {
                foreach (var mod in updaters.Values)
                {
                    try
                    {
                        versions.Add(mod, mod.Version);

                        var updates = Config.FromString(client.DownloadString(mod.Updates));
                        foreach (var update in updates)
                        {
                            var version = SemVersion.Parse(update.Key);
                            var url = update.Value;

                            if (version.CompareTo(versions[mod]) > 0)
                            {
                                versions[mod] = version;
                                urls[mod] = url;
                            }
                        }
                    }
                    catch (Exception)
                    {
                        if (versions.ContainsKey(mod))
                        {
                            versions.Remove(mod);
                        }
                    }
                }
            }

            return urls;
        }

        public static void Update(string url)
        {
            Debug.WriteLine("Updating from: " + url);
            var folder = Path.Combine(Path.GetTempPath(), "Mods");
            if (!Directory.Exists(folder))
            {
                Directory.CreateDirectory(folder);
            }

            var zip = Path.Combine(folder, "update.current");
            if (File.Exists(zip))
            {
                File.Delete(zip);
            }

            using (var client = new WebClient())
            {
                client.DownloadFile(url, zip);
            }

            var extract_folder = Path.Combine(folder, "Extract");
            if (Directory.Exists(extract_folder))
            {
                Directory.Delete(extract_folder, true);
            }
            Directory.CreateDirectory(extract_folder);

            ZipFile.ExtractToDirectory(zip, extract_folder);

            var mod = Mod.GetMod(Path.Combine(extract_folder, "mod.ini"));
            var mod_version_folder = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Mods", mod.Name, mod.VersionFolder.ToString());
            if (Directory.Exists(mod_version_folder))
            {
                Directory.Delete(mod_version_folder, true);
            }
            Directory.CreateDirectory(mod_version_folder);

            ZipFile.ExtractToDirectory(zip, mod_version_folder);

            File.Delete(zip);
            Directory.Delete(extract_folder, true);
        }
    }
}
