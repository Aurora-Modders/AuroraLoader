using Semver;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;

namespace AuroraLoader
{
    class Mod
    {
        public enum ModType { EXE, DATABASE, UTILITY, ROOT_UTILITY }
        public enum ModStatus { POWERUSER, PUBLIC, APPROVED }

        public static Mod BaseGame => new Mod() { Name = "Base Game" };

        public static IList<Mod> GetInstalledMods()
        {
            var mods = new List<Mod>();

            var dir = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Mods");
            foreach (var file in Directory.EnumerateFiles(dir, "mod.ini", SearchOption.AllDirectories))
            {
                mods.Add(GetMod(file));
            }

            return mods;
        }

        public static Mod GetMod(string file)
        {
            var str = File.ReadAllText(file);
            var mod = new Mod() { DefFile = file };
            mod.SetConfig(str);
            
            return mod;
        }

        public string DefFile { get; private set; } = null;
        public string Name { get; private set; } = null;
        public ModType Type { get; private set; } = ModType.EXE;
        public SemVersion Version { get; private set; } = null;
        public int AuroraMajor { get; private set; } = -1;
        public int AuroraMinor { get; private set; } = -1;
        public int AuroraPatch { get; private set; } = -1;
        public string Exe { get; private set; } = null;
        public string ConfigFile { get; private set; } = null;
        public ModStatus Status { get; private set; } = ModStatus.POWERUSER;
        public string Updates { get; private set; } = null;
        public string VersionFolder
        {
            get
            {
                var str = AuroraMajor.ToString();
                if (AuroraMinor != -1)
                {
                    str += "." + AuroraMinor;
                }
                if (AuroraPatch != -1)
                {
                    str += "." + AuroraPatch;
                }

                return str;
            }
        }

        public void SetConfig(string config)
        {
            var settings = Config.FromString(config);

            foreach (var kvp in settings)
            {
                var key = kvp.Key;
                var val = kvp.Value;

                if (key.Equals("Name"))
                {
                    if (val.Equals("Base Game"))
                    {
                        throw new Exception("Mod name can not be Base Game");
                    }

                    Name = val;
                }
                else if (key.Equals("Status"))
                {
                    if (val.Equals("Approved"))
                    {
                        Status = ModStatus.APPROVED;
                    }
                    else if (val.Equals("Public"))
                    {
                        Status = ModStatus.PUBLIC;
                    }
                    else if (val.Equals("Poweruser"))
                    {
                        Status = ModStatus.POWERUSER;
                    }
                    else
                    {
                        throw new Exception("Invalid mod status");
                    }
                }
                else if (key.Equals("Exe") || key.Equals("ExecuteCommand")) // Exe deprecated
                {
                    if (val.Equals("Aurora.exe"))
                    {
                        throw new Exception("Mod exe can not be Aurora.exe");
                    }

                    Exe = val;
                }
                else if (key.Equals("Updates"))
                {
                    Updates = val;
                }
                else if (key.Equals("Version"))
                {
                    Version = SemVersion.Parse(val);
                }
                else if (key.Equals("AuroraVersion"))
                {
                    var pieces = val.Split('.');
                    if (pieces.Length > 0)
                    {
                        AuroraMajor = int.Parse(pieces[0]);
                    }
                    if (pieces.Length > 1)
                    {
                        AuroraMinor = int.Parse(pieces[1]);
                    }
                    if (pieces.Length > 2)
                    {
                        AuroraPatch = int.Parse(pieces[2]);
                    }
                }
                else if (key.Equals("Config") || key.Equals("ConfigCommand")) // Config deprecated
                {
                    ConfigFile = val;
                }
                else if (key.Equals("Type"))
                {
                    if (val.Equals("Exe"))
                    {
                        Type = ModType.EXE;
                    }
                    else if (val.Equals("Database"))
                    {
                        Type = ModType.DATABASE;
                    }
                    else if (val.Equals("Utility"))
                    {
                        Type = ModType.UTILITY;
                    }
                    else if (val.Equals("RootUtility"))
                    {
                        Type = ModType.ROOT_UTILITY;
                    }
                    else
                    {
                        throw new Exception("Invalid mod type: " + val);
                    }
                }
                else
                {
                    throw new Exception("Invalid config line");
                }
            }

            if (Name == null || Name.Length < 2)
            {
                throw new Exception("Mod name must have length at least 2");
            }
            else if (Version == null)
            {
                throw new Exception("Mod must have a version");
            }
            else if (AuroraMajor == -1)
            {
                throw new Exception("Mod must have an Aurora version");
            }
            else if (Exe == null)
            {
                if (Type == ModType.EXE || Type == ModType.UTILITY || Type == ModType.ROOT_UTILITY)
                {
                    throw new Exception("Mod of type " + Type.ToString() + " must define an Exe");
                }

            }
        }

        public bool WorksForVersion(GameVersion version)
        {
            if (AuroraMajor != -1 && version.Version.Major != AuroraMajor)
            {
                return false;
            }
            else if (AuroraMinor != -1 && version.Version.Minor != AuroraMinor)
            {
                return false;
            }
            else if (AuroraPatch != -1 && version.Version.Patch != AuroraPatch)
            {
                return false;
            }
            else
            {
                return true;
            }
        }

        public override string ToString()
        {
            return $"{Name} {Version}";
        }
    }
}
