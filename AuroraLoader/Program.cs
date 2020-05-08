using System;
using System.Diagnostics;
using System.IO;
using System.Runtime.InteropServices;
using System.Security.Cryptography;
using System.Threading;
using System.Windows.Forms;
using AuroraLoader.Mods;
using AuroraLoader.Registry;
using Microsoft.Extensions.Configuration;

namespace AuroraLoader
{
    static class Program
    {
        public static readonly string AuroraLoaderExecutableDirectory = Path.GetDirectoryName(Process.GetCurrentProcess().MainModule.FileName);
        public static readonly string ModDirectory = Path.Combine(AuroraLoaderExecutableDirectory, "Mods");
        public static readonly string CacheDirectory = Path.Combine(Path.GetTempPath(), "auroraloader_cache");

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

            if (!File.Exists(Path.Combine(AuroraLoaderExecutableDirectory, "Clean", "aurora.exe")))
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

            CopySqlInteropAssemblies();
            PrepareModDirectory();

            // TODO would love to set up dependency injection
            var configuration = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile(path: "appsettings.json", optional: true, reloadOnChange: true)
                .Build();
            var auroraVersionRegistry = new AuroraVersionRegistry(configuration);
            var modRegistry = new ModRegistry(configuration);
            Log.Debug("Launching main form");
            Application.Run(new FormMain(configuration, auroraVersionRegistry, modRegistry));
        }

        private static void InstallAurora()
        {
            var thread = new Thread(() =>
            {
                var aurora_files = Installer.GetLatestAuroraFiles();
                Installer.DownloadAuroraPieces(Path.Combine(AuroraLoaderExecutableDirectory, "Clean"), aurora_files);
            });
            thread.Start();

            var progress = new FormProgress(thread) { Text = "Installing Aurora" };
            progress.ShowDialog();
        }

        internal static void CopySqlInteropAssemblies()
        {
            // Grab ourselves a copy of the existing SQLite interops. If it's good enough for Aurora, it's good enough for us...
            try
            {
                Log.Debug("Copying SQLite interop dlls");
                Directory.CreateDirectory(Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "x86"));
                Directory.CreateDirectory(Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "x64"));
                File.Copy(Path.Combine(AuroraLoaderExecutableDirectory, "Clean", "x86", "SQLite.Interop.dll"), Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "x86", "SQLite.Interop.dll"), true);
                File.Copy(Path.Combine(AuroraLoaderExecutableDirectory, "Clean", "x64", "SQLite.Interop.dll"), Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "x64", "SQLite.Interop.dll"), true);
            }
            catch (Exception exc)
            {
                Log.Error("Failure while copying SQLite interop DLLs", exc);
            }
        }

        internal static void PrepareModDirectory()
        {
            if (!Directory.Exists(ModDirectory))
            {
                Directory.CreateDirectory(ModDirectory);
            }

            // Load the mod configuration for AuroraLoader itself
            if (File.Exists(Path.Combine(AuroraLoaderExecutableDirectory, "mod.json")))
            {
                var raw = File.ReadAllText(Path.Combine(AuroraLoaderExecutableDirectory, "mod.json"));
                var auroraLoader = Mod.DeserializeMod(raw);

                if (auroraLoader.Name != "AuroraLoader")
                {
                    throw new Exception(Path.Combine(AuroraLoaderExecutableDirectory, "mod.json") + " does not belong to AuroraLoader.");
                }

                if (!Directory.Exists(auroraLoader.LatestVersion.DownloadPath))
                {
                    Directory.CreateDirectory(auroraLoader.LatestVersion.DownloadPath);
                    File.Copy(Path.Combine(AuroraLoaderExecutableDirectory, "mod.json"), Path.Combine(auroraLoader.ModFolder, "mod.json"), true);
                    File.Copy(Path.Combine(AuroraLoaderExecutableDirectory, "AuroraLoader.exe"), Path.Combine(auroraLoader.LatestVersion.DownloadPath, "AuroraLoader.Exe"), true);
                }
            }
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

        public static string GetChecksum(byte[] bytes)
        {
            using var sha = SHA256.Create();
            var hash = sha.ComputeHash(bytes);
            var str = Convert.ToBase64String(hash);

            return str.Replace("/", "").Replace("+", "").Replace("=", "").Substring(0, 6);
        }
    }
}
