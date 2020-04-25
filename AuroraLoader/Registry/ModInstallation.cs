using Semver;
using System;

namespace AuroraLoader.Registry
{
    public class ModInstallation
    {
        public string Name { get; private set; } = null;
        public ModType Type { get; private set; } = ModType.EXE;
        public ModStatus Status { get; private set; } = ModStatus.POWERUSER;

        public SemVersion Version { get; private set; } = null;

        public SemVersion TargetAuroraVersion { get; private set; }
        public string ExecuteCommand { get; private set; } = null;
        public string ModInternalConfigFile { get; private set; } = null;
        public string Updates { get; private set; } = null;
        public string ModFolder { get; }

        private ModInstallation(string ModFolder)
        {
            this.ModFolder = ModFolder;
        }

        public bool WorksForVersion(AuroraVersion version)
        {
            // TODO Needs to understand wildcarded versions
            return TargetAuroraVersion == version.Version;
        }

        public override string ToString()
        {
            return $"{Name} {Version}";
        }

        public static ModInstallation Parse(string ModFolder)
        {
            var mod = new ModInstallation(ModFolder);
            var settings = Config.FromString(ModFolder);

            // Required
            mod.Name = settings["name"] ?? "";
            mod.Type = Enum.Parse<ModType>(settings["Type"]);
            mod.Status = Enum.Parse<ModStatus>(settings["Status"]);
            mod.Version = SemVersion.Parse(settings["Version"]);
            mod.TargetAuroraVersion = SemVersion.Parse(settings["AuroraVersion"]);

            // Optional at least some of the time
            mod.ExecuteCommand = settings["Exe"] ?? null;
            mod.Updates = settings["Updates"] ?? null;
            mod.ModInternalConfigFile = settings["Config"] ?? null;

            // TODO extract some sort of validation method
            if (mod.Name == null || mod.Name.Length < 2)
            {
                throw new Exception("Mod name must have length at least 2");
            }
            if (mod.Version == null)
            {
                throw new Exception("Mod must have a version");
            }
            if (mod.TargetAuroraVersion == null)
            {
                throw new Exception("Mod must have an Aurora version");
            }
            if (mod.ExecuteCommand == null)
            {
                if (mod.Type == ModType.EXE || mod.Type == ModType.UTILITY || mod.Type == ModType.ROOT_UTILITY)
                {
                    throw new Exception("Mod of type " + mod.Type.ToString() + " must define an Exe");
                }

            }
            if (settings["Exe"].Equals("Aurora.exe"))
            {
                throw new Exception("Mod exe can not be Aurora.exe");
            }

            return mod;
        }
    }
}
