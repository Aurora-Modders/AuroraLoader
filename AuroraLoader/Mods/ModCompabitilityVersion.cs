using Semver;

namespace AuroraLoader.Mods
{
    public class ModCompabitilityVersion
    {
        public int Major { get; } = -1;
        public int Minor { get; } = -1;
        public int Patch { get; } = -1;

        public override string ToString()
        {
            var str = Major.ToString();
            if (Minor != -1)
            {
                str += "." + Minor;
            }
            if (Patch != -1)
            {
                str += "." + Patch;
            }

            return str;
        }


        public ModCompabitilityVersion(string raw)
        {
            var pieces = raw.Split('.');
            if (pieces.Length > 0)
            {
                Major = int.Parse(pieces[0]);
            }
            if (pieces.Length > 1)
            {
                Minor = int.Parse(pieces[1]);
            }
            if (pieces.Length > 2)
            {
                Patch = int.Parse(pieces[2]);
            }
        }
        public bool WorksForVersion(SemVersion version)
        {
            if (Major != -1 && version.Major != Major)
            {
                return false;
            }
            else if (Minor != -1 && version.Minor != Minor)
            {
                return false;
            }
            else if (Patch != -1 && version.Patch != Patch)
            {
                return false;
            }
            else
            {
                return true;
            }
        }
    }
}
