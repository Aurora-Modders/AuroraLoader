using AuroraLoader.Mods;
using Semver;
using System;
using System.Collections.Generic;
using System.IO;
using System.IO.Compression;
using System.Linq;
using System.Net;
using System.Text;
using System.Windows.Forms;

namespace AuroraLoader
{
    class Installer
    {
        public static string GetLatestUrl()
        {
            return "https://raw.githubusercontent.com/Aurora-Modders/AuroraRegistry/master/aurora_files.ini";
        }

        private static void InstallClean()
        {
            var folder = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Clean");
            var url = GetLatestUrl();

            using (var client = new WebClient())
            {
                var str = client.DownloadString(url);
                var aurora_files = ModConfigurationReader.FromKeyValueString(str);

                DownloadAuroraPieces(folder, aurora_files);
            }
        }

        public static void CopyClean(string folder)
        {
            var clean = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Clean");
            if (!Directory.Exists(clean))
            {
                MessageBox.Show("A clean install will be downloaded.");
                InstallClean();
            }

            if (Directory.Exists(folder))
            {
                Directory.Delete(folder, true);
            }

            Program.CopyDirectory(clean, folder);
        }

        public static void UpdateAurora(GameInstallation current, Dictionary<string, string> aurora_files)
        {
            var update = SemVersion.Parse(aurora_files["Version"]);

            if (current.InstalledVersion.Version.Major == update.Major)
            {
                aurora_files.Remove("Major");

                if (current.InstalledVersion.Version.Minor == update.Minor)
                {
                    aurora_files.Remove("Minor");

                    if (current.InstalledVersion.Version.Patch == update.Patch)
                    {
                        aurora_files.Remove("Patch");
                        aurora_files.Remove("Rev"); // deprecated
                    }
                }
            }

            foreach (var piece in aurora_files.Keys.ToList())
            {
                if (!piece.Equals("Major") && !piece.Equals("Minor") && !piece.Equals("Patch") && !piece.Equals("Rev"))
                {
                    aurora_files.Remove(piece);
                }
            }

            if (aurora_files.Count > 0)
            {
                var folder = Path.GetDirectoryName(current.ExecutableLocation);
                DownloadAuroraPieces(folder, aurora_files);
            }
        }

        public static void DownloadAuroraPieces(string folder, Dictionary<string, string> aurora_files)
        {
            aurora_files.Remove("Version");

            var zip = Path.Combine(Path.GetTempPath(), Path.GetRandomFileName());
            var extract_folder = Path.Combine(Path.GetTempPath(), "aurora_installs");

            if (Directory.Exists(extract_folder))
            {
                Directory.Delete(extract_folder, true);
            }
            Directory.CreateDirectory(extract_folder);

            using (var client = new WebClient())
            {
                var pieces = aurora_files.Keys.ToList();
                pieces.Sort(); // Major before Minor before Patch/Rev

                foreach (var piece in pieces)
                {
                    Log.Debug("Installing Aurora piece: " + piece);

                    if (File.Exists(zip))
                    {
                        File.Delete(zip);
                    }

                    client.DownloadFile(aurora_files[piece], zip);

                    Directory.Delete(extract_folder, true);
                    Directory.CreateDirectory(extract_folder);
                    ZipFile.ExtractToDirectory(zip, extract_folder);

                    // need to delete exe and db before overwriting
                    foreach (var file in Directory.EnumerateFiles(extract_folder))
                    {
                        var dest = Path.Combine(folder, Path.GetFileName(file));

                        if (File.Exists(dest))
                        {
                            File.Delete(dest);
                        }
                    }

                    Program.CopyDirectory(extract_folder, folder);
                }
            }

            File.Delete(zip);
            Directory.Delete(extract_folder, true);
        }
    }
}
