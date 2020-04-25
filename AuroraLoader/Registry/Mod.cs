using System;
using System.Collections.Generic;
using System.Text;

namespace AuroraLoader.Registry
{
    public class Mod
    {

        public ModType Type => Listing?.Type ?? Installation.Type;
        public string Name => Listing?.ModName ?? Installation.Name;

        public ModInstallation Installation { get; }
        public ModListing Listing { get; }

        public bool Installed => Installation != null;

        public Mod(ModInstallation modInstallation, ModListing modListing)
        {
            Installation = modInstallation;
            Listing = modListing;
        }
    }
}
