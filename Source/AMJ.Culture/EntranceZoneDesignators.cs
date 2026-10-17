using RimWorld;
using UnityEngine;
using Verse;

namespace AMJ.Culture
{
    // The designation tools operate on an independent MapComponent grid,
    // not ZoneManager, so they can be painted over other zones and floors.
    public sealed class Designator_EntranceAdd : Designator_Cells
    {
        private static int lastWarningFrame = -100000;
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
                   !map.GetComponent<EntranceZoneMap>().Contains(cell) &&
                   !map.GetComponent<EntranceZoneMap>().IsNoShoes(cell);
        }

        public override void DesignateSingleCell(IntVec3 cell)
        {
            EntranceZoneMap zone = Find.CurrentMap.GetComponent<EntranceZoneMap>();
            zone.Set(cell, true);
            // Warn, but allow building the threshold before painting the floor area.
            // Throttle this during mouse-drag painting; no tick-time notifications.
            if (!zone.HasNoShoesNeighbor(cell) &&
                Time.frameCount - lastWarningFrame > 300)
            {
                Messages.Message("AMJ_Culture_EntranceNotAdjacent".Translate(),
                                 MessageTypeDefOf.RejectInput, false);
                lastWarningFrame = Time.frameCount;
            }
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
    // Like the entrance designators, these cells intentionally coexist with
    // Vanilla stockpile, growing and home areas. The entrance itself must be
    // on a separate, edge-adjacent cell, not inside the no-shoes area.
    public sealed class Designator_NoShoesAdd : Designator_Cells
    {
        public Designator_NoShoesAdd()
        {
            defaultLabel = "AMJ_Culture_NoShoesAdd".Translate();
            defaultDesc = "AMJ_Culture_NoShoesAddDesc".Translate();
            icon = ContentFinder<Texture2D>.Get("UI/Designators/ZoneCreate_Growing", false)
                   ?? BaseContent.BadTex;
        }

        public override AcceptanceReport CanDesignateCell(IntVec3 cell)
        {
            Map map = Find.CurrentMap;
            if (map == null || !cell.InBounds(map) || !cell.Standable(map)) return false;
            EntranceZoneMap zone = map.GetComponent<EntranceZoneMap>();
            return !zone.Contains(cell) && !zone.IsNoShoes(cell);
        }

        public override void DesignateSingleCell(IntVec3 cell)
        {
            Find.CurrentMap.GetComponent<EntranceZoneMap>().SetNoShoes(cell, true);
        }

        public override void SelectedUpdate()
        {
            base.SelectedUpdate();
            Map map = Find.CurrentMap;
            if (map != null)
                GenDraw.DrawFieldEdges(map.GetComponent<EntranceZoneMap>().NoShoesCells, Color.green);
        }
    }

    public sealed class Designator_NoShoesRemove : Designator_Cells
    {
        public Designator_NoShoesRemove()
        {
            defaultLabel = "AMJ_Culture_NoShoesRemove".Translate();
            defaultDesc = "AMJ_Culture_NoShoesRemoveDesc".Translate();
            icon = ContentFinder<Texture2D>.Get("UI/Designators/ZoneDelete", false)
                   ?? BaseContent.BadTex;
        }

        public override AcceptanceReport CanDesignateCell(IntVec3 cell)
        {
            Map map = Find.CurrentMap;
            return map != null && cell.InBounds(map) &&
                   map.GetComponent<EntranceZoneMap>().IsNoShoes(cell);
        }

        public override void DesignateSingleCell(IntVec3 cell)
        {
            Find.CurrentMap.GetComponent<EntranceZoneMap>().SetNoShoes(cell, false);
        }

        public override void SelectedUpdate()
        {
            base.SelectedUpdate();
            Map map = Find.CurrentMap;
            if (map != null)
                GenDraw.DrawFieldEdges(map.GetComponent<EntranceZoneMap>().NoShoesCells, Color.green);
        }
    }

}
