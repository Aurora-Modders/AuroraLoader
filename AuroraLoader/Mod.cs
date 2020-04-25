using Semver;
using System;
using System.Collections.Generic;
using System.IO;

namespace AuroraLoader
{
	public class Mod
	{
		public enum ModType { EXE, DATABASE, UTILITY, ROOT_UTILITY }
		public enum ModStatus { POWERUSER, PUBLIC, APPROVED }

		public string Name { get; private set; } = null;
		public ModType Type { get; private set; } = ModType.EXE;
		public SemVersion Version { get; private set; } = null;

		public SemVersion TargetAuroraVersion { get; private set; }
		public string Exe { get; private set; } = null;
		public string ConfigFile { get; private set; } = null;
		public ModStatus Status { get; private set; } = ModStatus.POWERUSER;
		public string Updates { get; private set; } = null;
		public string ModDefinitionPath { get; }
		public string RawModDefinitionFileContents { get; }

		private Mod(string modDefinitionPath)
		{
			ModDefinitionPath = modDefinitionPath;
			RawModDefinitionFileContents = File.ReadAllText(ModDefinitionPath);
		}

		public bool WorksForVersion(AuroraVersion version)
		{
			return TargetAuroraVersion == version.Version;
		}

		public override string ToString()
		{
			return $"{Name} {Version}";
		}

		public static Mod Parse(string modDefinitionPath)
		{
			var mod = new Mod(modDefinitionPath);
			var settings = Config.FromString(mod.RawModDefinitionFileContents);
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

					mod.Name = val;
				}
				else if (key.Equals("Status"))
				{
					if (val.Equals("Approved"))
					{
						mod.Status = Mod.ModStatus.APPROVED;
					}
					else if (val.Equals("Public"))
					{
						mod.Status = Mod.ModStatus.PUBLIC;
					}
					else if (val.Equals("Poweruser"))
					{
						mod.Status = Mod.ModStatus.POWERUSER;
					}
					else
					{
						throw new Exception("Invalid mod status");
					}
				}
				else if (key.Equals("Exe"))
				{
					if (val.Equals("Aurora.exe"))
					{
						throw new Exception("Mod exe can not be Aurora.exe");
					}

					mod.Exe = val;
				}
				else if (key.Equals("Updates"))
				{
					mod.Updates = val;
				}
				else if (key.Equals("Version"))
				{
					mod.Version = SemVersion.Parse(val);
				}
				else if (key.Equals("AuroraVersion"))
				{
					mod.TargetAuroraVersion = SemVersion.Parse(val);
				}
				else if (key.Equals("Config"))
				{
					mod.ConfigFile = val;
				}
				else if (key.Equals("Type"))
				{
					if (val.Equals("Exe"))
					{
						mod.Type = ModType.EXE;
					}
					else if (val.Equals("Database"))
					{
						mod.Type = ModType.DATABASE;
					}
					else if (val.Equals("Utility"))
					{
						mod.Type = ModType.UTILITY;
					}
					else if (val.Equals("RootUtility"))
					{
						mod.Type = ModType.ROOT_UTILITY;
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

			if (mod.Name == null || mod.Name.Length < 2)
			{
				throw new Exception("Mod name must have length at least 2");
			}
			else if (mod.Version == null)
			{
				throw new Exception("Mod must have a version");
			}
			else if (mod.TargetAuroraVersion == null)
			{
				throw new Exception("Mod must have an Aurora version");
			}
			else if (mod.Exe == null)
			{
				if (mod.Type == ModType.EXE || mod.Type == ModType.UTILITY || mod.Type == ModType.ROOT_UTILITY)
				{
					throw new Exception("Mod of type " + mod.Type.ToString() + " must define an Exe");
				}

			}

			return mod;
		}
	}
}
