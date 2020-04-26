using System;
using System.Collections.Generic;
using System.IO;
using Microsoft.Extensions.Configuration;

namespace AuroraLoader.Registry
{
    /// <summary>
    /// Must be initialized by calling Update()
    /// </summary>
    public class MirrorRegistry : IRegistry
    {
        public IList<Mirror> Mirrors { get; private set; }

        private readonly IConfiguration _configuration;

        public MirrorRegistry(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        /// <summary>
        /// Mirrors are only defined locally, for now
        /// </summary>
        public void Update()
        {
            var mirrors = new List<Mirror>();
            try
            {
                foreach (var rootUrl in File.ReadAllLines(Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "mirrors.ini")))
                {
                    mirrors.Add(new Mirror(_configuration, rootUrl));
                }
            }
            catch (Exception e)
            {
                Log.Error($"Failed to parse mirror data from {Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "aurora.exe")}", e);
            }

            Mirrors = mirrors;
        }

        public void AddMirror(Mirror mirror)
        {
            Mirrors.Add(mirror);
        }
    }
}
