using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Semver;
using System;
using System.Diagnostics;
using System.IO;
using System.Windows.Forms;

namespace AuroraLoader
{
    static class Program
    {
        public static readonly string[] MIRRORS =
        {
            "https://raw.githubusercontent.com/Aurora-Modders/AuroraMods/master/"
        };

        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);

            var configuration = new ConfigurationBuilder()
                    .SetBasePath(Directory.GetCurrentDirectory())
                    .AddJsonFile(path: "appsettings.json", optional: false, reloadOnChange: true)
                    .Build();
            Application.Run(new FormMain(configuration));
        }
    }
}
