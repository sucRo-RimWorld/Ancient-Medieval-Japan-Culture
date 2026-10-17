using RimWorld;
using UnityEngine;
using Verse;

namespace AMJ.Culture
{
    // The designation tools operate on an independent MapComponent grid,
    // not ZoneManager, so they can be painted over other zones and floors.
    public sealed class Designator_EntranceAdd : Designator_Cells
    {
        public Designator_EntranceAdd()
        {
            defaultLabel = "AMJ_Culture_EntranceAdd".Translate();
            defaultDesc = "AMJ_Culture_EntranceAddDesc".Translate();
            icon = ContentFinder<Texture2D>.Get("UI/Designators/ZoneCreate_Growing", false)
                   ?? BaseContent.BadTex;
        }

        public override AcceptanceReport CanDesignateCell(IntVec3 cell)
        {
            Map map = Find.CurrentMap;
            return map != null && cell.InBounds(map) && cell.Standable(map) &&
                   !map.GetComponent<EntranceZoneMap>().Contains(cell);
        }

        public override void DesignateSingleCell(IntVec3 cell)
        {
            Find.CurrentMap.GetComponent<EntranceZoneMap>().Set(cell, true);
        }

        public override void SelectedUpdate()
        {
            base.SelectedUpdate();
            Map map = Find.CurrentMap;
            if (map != null)
                GenDraw.DrawFieldEdges(map.GetComponent<EntranceZoneMap>().Cells, Color.cyan);
        }
    }

    public sealed class Designator_EntranceRemove : Designator_Cells
    {
        public Designator_EntranceRemove()
        {
            defaultLabel = "AMJ_Culture_EntranceRemove".Translate();
            defaultDesc = "AMJ_Culture_EntranceRemoveDesc".Translate();
            icon = ContentFinder<Texture2D>.Get("UI/Designators/ZoneDelete", false)
                   ?? BaseContent.BadTex;
        }

        public override AcceptanceReport CanDesignateCell(IntVec3 cell)
        {
            Map map = Find.CurrentMap;
            return map != null && cell.InBounds(map) &&
                   map.GetComponent<EntranceZoneMap>().Contains(cell);
        }

        public override void DesignateSingleCell(IntVec3 cell)
        {
            Find.CurrentMap.GetComponent<EntranceZoneMap>().Set(cell, false);
        }

        public override void SelectedUpdate()
        {
            base.SelectedUpdate();
            Map map = Find.CurrentMap;
            if (map != null)
                GenDraw.DrawFieldEdges(map.GetComponent<EntranceZoneMap>().Cells, Color.cyan);
        }
    }
}
