using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using AuroraLoader;
using AuroraLoader.Mods;
using NUnit.Framework;
using Semver;

namespace AuroraLoaderTest
{
    public class AuroraInstallationTest
    {
        readonly string auroraVersionString = "1.8.1";
        readonly string checksum = "aurora";

        string installationPath;
        AuroraVersion auroraVersion = null;
        ModVersion defaultModVersion;

        [SetUp]
        public void SetUp()
        {
            installationPath = Path.Combine(TestContext.CurrentContext.TestDirectory, "AuroraLoaderInstallationTest");
            Directory.CreateDirectory(installationPath);

            auroraVersion = new AuroraVersion(SemVersion.Parse(auroraVersionString), checksum);
            defaultModVersion = new ModVersion()
            {
                Mod = new Mod()
                {
                    Name = "mod"
                }
            };

            using (File.Create(Path.Combine(installationPath, "Aurora.exe")));
            using (File.Create(Path.Combine(installationPath, "AuroraDB.db")));
        }

        [TearDown]
        public void Teardown()
        {
            if (Directory.Exists(installationPath))
            {
                Directory.Delete(installationPath, true);
            }
        }

        [Test]
        public void ConnectionString_DefaultInput_EqualsKnownString()
        {
            var defaultAuroraInstallation = new AuroraInstallation(auroraVersion, installationPath);
            Assert.That(defaultAuroraInstallation.ConnectionString, Is.EqualTo($"Data Source={Path.Combine(installationPath, "AuroraDB.db")};Version=3;New=False;Compress=True;"));
        }

        [Test]
        public void UpdateAurora_NullFiles_ThrowsArgumentNullException()
        {
            var defaultAuroraInstallation = new AuroraInstallation(auroraVersion, installationPath);
            Assert.Throws<ArgumentNullException>(delegate { defaultAuroraInstallation.UpdateAurora(null); });
        }

        [Test]
        public void UpdateAurora_NoFilesSelected_NoFilesDownloaded()
        {
            var defaultAuroraInstallation = new AuroraInstallation(auroraVersion, installationPath);
            Assert.Throws<ArgumentException>(delegate { defaultAuroraInstallation.UpdateAurora(new Dictionary<string, string>()); });
        }
    }
}