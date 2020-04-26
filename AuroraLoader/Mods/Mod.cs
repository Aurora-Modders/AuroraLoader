namespace AuroraLoader.Mods
{
    public class Mod
    {
        public string Name => Listing?.ModName ?? Installation.Name;
        public ModType Type => Listing?.Type ?? Installation?.Type ?? ModType.EXE;
        public bool Installed => Installation != null;
        public bool CanBeUpdated => Listing != null && Installed && Listing.LatestVersion.CompareByPrecedence(Installation.Version) > 0;

        public bool Configurable => Installation?.ModInternalConfigFile != null;

        public ModConfiguration Installation { get; }
        public ModListing Listing { get; }

        // TODO this is nasty but I'm lazy
        public string InstallText => $"Install {Name} {Listing?.LatestVersion} ({Type})";
        public string UpdateText => $"Update {Name} {Installation?.Version} ({Type}) to {Listing?.LatestVersion}";

        public Mod(ModConfiguration modInstallation, ModListing modListing)
        {
            Installation = modInstallation;
            Listing = modListing;
        }
    }
}
