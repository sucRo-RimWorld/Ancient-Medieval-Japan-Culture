using RimWorks.Quickstarts;
using RimWorld;
using Verse;

namespace AMJ.Culture.Test
{
    public sealed class ChairQuickstart : AbstractQuickstart
    {
        public override TaggedString description
        {
            get { return "One native table-free chair meal with real comfort."; }
        }
        public override int mapSize { get { return 50; } }
        public override string seed { get { return "AMJ-CULTURE-CHAIR-16"; } }
        public static bool IsPostLoaded { get; private set; }
        public override void PostLoaded()
        {
            IsPostLoaded = true;
            Log.Message("AMJ_CULTURE_CHAIR16_QUICKSTART_READY");
        }
    }
}
