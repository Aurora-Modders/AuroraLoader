using System;
using System.Collections.Generic;
using System.IO;
using Microsoft.Extensions.Configuration;

namespace AuroraLoader.Registry
{

    public class MirrorRegistry
    {
        public IList<Mirror> Mirrors { get; private set; }

        private readonly IConfiguration _configuration;

        public MirrorRegistry(IConfiguration configuration)
        {
            _configuration = configuration;
            UpdateMirrors();
        }

        public void UpdateMirrors()
        {
            var mirrors = new List<Mirror>();
            try
            {
                foreach (var rootUrl in File.ReadAllLines(_configuration["aurora_mirrors_relative_filepath"]))
                {
                    mirrors.Add(new Mirror(_configuration, rootUrl));
                }
            }
            catch (Exception e)
            {
                Log.Error($"Failed to parse mirror data from {_configuration["aurora_mirrors_relative_filepath"]}", e);
            }

            Mirrors = mirrors;
        }
    }
}
