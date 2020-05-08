using System;
using System.Collections.Generic;
using System.IO;
using System.IO.Compression;
using System.Linq;
using System.Net;
using System.Windows.Forms;
using System.Windows.Forms.Design;
using AuroraLoader.Mods;
using Semver;

namespace AuroraLoader
{
    class Installer
    {
        public static Dictionary<string, string> GetLatestAuroraFiles()
        {
            // TODO mirrors
            var url = "https://raw.githubusercontent.com/Aurora-Modders/AuroraRegistry/master/aurora_files.ini";

            using (var client = new WebClient())
            {
                var str = client.DownloadString(url);

                return ModConfigurationReader.FromKeyValueString(str);
            }
        }

        public static void CopyClean(string folder)
        {
            var clean = Path.Combine(Program.AuroraLoaderExecutableDirectory, "Clean");
            if (!Directory.Exists(clean))
            {
                MessageBox.Show("A clean install will be downloaded.");

                var aurora_files = GetLatestAuroraFiles();
                DownloadAuroraPieces(clean, aurora_files);
            }

            if (Directory.Exists(folder))
            {
                Directory.Delete(folder, true);
            }

            CopyDirectory(clean, folder);
        }

        public static void DownloadAuroraPieces(string installationPath, Dictionary<string, string> aurora_files)
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
                        var dest = Path.Combine(installationPath, Path.GetFileName(file));

                        if (File.Exists(dest))
                        {
                            File.Delete(dest);
                        }
                    }

                    CopyDirectory(extract_folder, installationPath);
                }
            }

            File.Delete(zip);
            Directory.Delete(extract_folder, true);
        }

        public static void CopyDirectory(string source, string target)
        {
            CopyAll(new DirectoryInfo(source), new DirectoryInfo(target));
        }

        private static void CopyAll(DirectoryInfo source, DirectoryInfo target)
        {
            Directory.CreateDirectory(target.FullName);

            // Copy each file into the new directory.
            foreach (FileInfo fi in source.GetFiles())
            {
                Console.WriteLine(@"Copying {0}\{1}", target.FullName, fi.Name);
                fi.CopyTo(Path.Combine(target.FullName, fi.Name), true);
            }

            // Copy each subdirectory using recursion.
            foreach (DirectoryInfo diSourceSubDir in source.GetDirectories())
            {
                DirectoryInfo nextTargetSubDir =
                    target.CreateSubdirectory(diSourceSubDir.Name);
                CopyAll(diSourceSubDir, nextTargetSubDir);
            }
        }
    }
}
