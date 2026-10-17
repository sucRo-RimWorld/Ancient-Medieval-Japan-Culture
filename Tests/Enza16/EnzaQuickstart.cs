using RimWorks.Quickstarts;
using RimWorld;
using Verse;

namespace AMJ.Culture.Test
{
    public sealed class EnzaQuickstart : AbstractQuickstart
    {
        public override TaggedString description
        {
            get { return "One native table-free enza meal with real comfort."; }
        }
        public override int mapSize { get { return 50; } }
        public override string seed { get { return "AMJ-CULTURE-ENZA-16"; } }
    }
}
