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

            using (File.Create(Path.Combine(installationPath, "Aurora.exe"))) ;
            using (File.Create(Path.Combine(installationPath, "AuroraDB.db"))) ;
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

        public void VersionedDirectory_DefaultInput_EqualsKnownString()
        {
            var defaultAuroraInstallation = new AuroraInstallation(auroraVersion, installationPath);
            Assert.That(defaultAuroraInstallation.VersionedDirectory, Is.EqualTo(Path.Combine(installationPath, "Aurora", auroraVersionString)));
        }

        [Test]
        public void CreateBackup_FolderCreated()
        {
            var defaultAuroraInstallation = new AuroraInstallation(auroraVersion, installationPath);
            defaultAuroraInstallation.CreateBackup();
            Assert.That(Directory.Exists(defaultAuroraInstallation.VersionedDirectory));
            Assert.That(File.Exists(Path.Combine(defaultAuroraInstallation.VersionedDirectory, "Aurora.exe")));
            Assert.That(File.Exists(Path.Combine(defaultAuroraInstallation.VersionedDirectory, "AuroraDB.db")));
        }

        [Test]
        public void VersionedDirectory_Exists_AfterInitializing()
        {
            var defaultAuroraInstallation = new AuroraInstallation(auroraVersion, installationPath);
            Directory.Delete(defaultAuroraInstallation.VersionedDirectory, true);
            _ = new AuroraInstallation(auroraVersion, installationPath);
            Assert.That(Directory.Exists(defaultAuroraInstallation.VersionedDirectory));
        }

        [Test]
        public void UpdateAurora_NullFiles_ThrowsArgumentNullException()
        {
            var defaultAuroraInstallation = new AuroraInstallation(auroraVersion, installationPath);
            Assert.Throws<ArgumentException>(delegate { defaultAuroraInstallation.UpdateAurora(null); });
        }

        [Test]
        public void UpdateAurora_NoFilesSelected_NoFilesDownloaded()
        {
            var defaultAuroraInstallation = new AuroraInstallation(auroraVersion, installationPath);
            Assert.Throws<ArgumentException>(delegate { defaultAuroraInstallation.UpdateAurora(new Dictionary<string, string>()); });
        }
    }
}