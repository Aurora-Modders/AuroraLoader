using System;
using System.Collections.Generic;
using System.IO;
using System.IO.Compression;
using System.Linq;
using System.Net;
using System.Text;

namespace AuroraLoader
{
    class Installer
    {
        public static void UpdateAurora(GameInstallation current, GameVersion update, Dictionary<string, string> aurora_files)
        {
            if (current.InstalledVersion.Version.Major == update.Version.Major)
            {
                aurora_files.Remove("Major");
            }
            if (current.InstalledVersion.Version.Minor == update.Version.Minor)
            {
                aurora_files.Remove("Minor");
            }
            if (current.InstalledVersion.Version.Patch == update.Version.Patch)
            {
                aurora_files.Remove("Patch");
                aurora_files.Remove("Rev"); // deprecated
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
            // TODO retain folder structure
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
                    if (File.Exists(zip))
                    {
                        File.Delete(zip);
                    }

                    client.DownloadFile(aurora_files[piece], zip);
                    ZipFile.ExtractToDirectory(zip, extract_folder);

                    var dest_folder = Path.Combine(folder, piece);
                    if (piece.Equals("Major") || piece.Equals("Minor") || piece.Equals("Patch") || piece.Equals("Rev"))
                    {
                        dest_folder = folder;
                    }

                    // need to delete exe and db before overwriting
                    foreach (var file in Directory.EnumerateFiles(extract_folder))
                    {
                        var dest = Path.Combine(dest_folder, Path.GetFileName(file));

                        if (File.Exists(dest))
                        {
                            File.Delete(dest);
                        }
                    }

                    ZipFile.ExtractToDirectory(zip, dest_folder);
                }
            }

            File.Delete(zip);
            Directory.Delete(extract_folder, true);
        }
    }
}
