using AuroraLoader.Registry;
using Microsoft.Extensions.Configuration;
using System;
using System.Diagnostics;
using System.IO;
using System.Runtime.InteropServices;
using System.Security.Cryptography;
using System.Threading;
using System.Windows.Forms;

namespace AuroraLoader
{
    static class Program
    {
        public static readonly string AuroraLoaderExecutableDirectory = AppDomain.CurrentDomain.BaseDirectory;
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);

            Log.Clear();
            Log.Debug("Start logging");

            if (!File.Exists(Path.Combine(AuroraLoaderExecutableDirectory, "aurora.exe")))
            {
                Log.Debug("Aurora not installed");
                var dialog = MessageBox.Show("Aurora not installed. Download and install? This might take a while.", "Install Aurora", MessageBoxButtons.YesNo);
                if (dialog == DialogResult.Yes)
                {
                    InstallAurora();
                }
                else
                {
                    Application.Exit();
                    return;
                }
            }

            // TODO would love to set up dependency injection
            var configuration = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile(path: "appsettings.json", optional: true, reloadOnChange: true)
                .Build();

            var mirrorRegistry = new MirrorRegistry(configuration);
            var auroraVersionRegistry = new AuroraVersionRegistry(configuration, mirrorRegistry);
            var localRegistry = new LocalModRegistry(configuration);
            var remoteRegistry = new RemoteModRegistry(configuration, mirrorRegistry);
            var modRegistry = new ModRegistry(configuration, localRegistry, remoteRegistry);
            modRegistry.Update();
            Application.Run(new FormMain(configuration, auroraVersionRegistry, modRegistry));
        }

        private static void InstallAurora()
        {
            var installation = new GameInstallation(new AuroraVersion("0.0.0", ""), Program.AuroraLoaderExecutableDirectory);
            var thread = new Thread(() =>
            {
                var aurora_files = Installer.GetLatestAuroraFiles();
                Installer.DownloadAuroraPieces(Program.AuroraLoaderExecutableDirectory, aurora_files);
            });
            thread.Start();

            var progress = new FormProgress(thread) { Text = "Installing Aurora" };
            progress.ShowDialog();
        }

        public static string GetChecksum(byte[] bytes)
        {
            using var sha = SHA256.Create();
            var hash = sha.ComputeHash(bytes);
            var str = Convert.ToBase64String(hash);

            return str.Replace("/", "").Replace("+", "").Replace("=", "").Substring(0, 6);
        }

        public static void OpenBrowser(string url)
        {
            try
            {
                Process.Start(url);
            }
            catch
            {
                // hack because of this: https://github.com/dotnet/corefx/issues/10361
                if (RuntimeInformation.IsOSPlatform(OSPlatform.Windows))
                {
                    url = url.Replace("&", "^&");
                    Process.Start(new ProcessStartInfo("cmd", $"/c start {url}") { CreateNoWindow = true });
                }
                else if (RuntimeInformation.IsOSPlatform(OSPlatform.Linux))
                {
                    Process.Start("xdg-open", url);
                }
                else if (RuntimeInformation.IsOSPlatform(OSPlatform.OSX))
                {
                    Process.Start("open", url);
                }
                else
                {
                    throw;
                }
            }
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
