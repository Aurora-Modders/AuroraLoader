using Microsoft.Extensions.Configuration;
using Semver;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace AuroraLoader
{
	public class AuroraLoaderRegistry
	{
		public IList<Mirror> Mirrors { get; private set; }
		public IList<Mod> LocalMods { get; private set; }
		public IList<AuroraVersion> AuroraVersions { get; private set; }

		private readonly IConfiguration _configuration;

		public AuroraLoaderRegistry(IConfiguration configuration)
		{
			_configuration = configuration;
			UpdateLocallyKnownMirrors();
			UpdateLocalMods();
			UpdateKnownVersions();
		}

		public void UpdateLocallyKnownMirrors()
		{
			var mirrors = new List<Mirror>();
			foreach (var rootUrl in File.ReadAllLines(_configuration["aurora_mirrors_relative_filepath"]))
			{
				mirrors.Add(new Mirror(_configuration, rootUrl));
			}
			Mirrors = mirrors;
		}

		public void UpdateLocalMods()
		{
			var mods = new List<Mod>();
			var dir = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Mods");
			foreach (var file in Directory.EnumerateFiles(dir, "mod.ini", SearchOption.AllDirectories))
			{
				mods.Add(Mod.Parse(file));
			}
			LocalMods = mods;
		}

		public void UpdateKnownVersions()
		{
			AuroraVersions = GetLocallyKnownVersions().Union(GetKnownVersionsFromMirrors()).ToList();
		}

		private IList<AuroraVersion> GetLocallyKnownVersions()
		{
			var knownVersions = new List<AuroraVersion>();
			foreach (var kvp in Config.FromString(File.ReadAllText(_configuration["aurora_known_versions_relative_filepath"])))
			{
				knownVersions.Add(new AuroraVersion(SemVersion.Parse(kvp.Key), kvp.Value));
			}
			return knownVersions;
		}

		private IList<AuroraVersion> GetKnownVersionsFromMirrors()
		{
			var mirrorKnownVersions = new List<AuroraVersion>();
			foreach (var mirror in Mirrors)
			{
				try
				{
					mirror.UpdateKnownAuroraVersions();
					mirrorKnownVersions = mirrorKnownVersions.Union(mirror.KnownAuroraVersions).ToList();
				}
				catch (Exception e)
				{
					Log.Error($"Failed to update known Aurora versions from {mirror.RootUrl}", e);
				}
			}
			return mirrorKnownVersions;
		}
	}
}
