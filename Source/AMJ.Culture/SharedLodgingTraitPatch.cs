using HarmonyLib;
using RimWorld;
using UnityEngine;
using Verse;

namespace AMJ.Culture
{
    // Shared barracks are an ordinary sleeping arrangement in premodern Japan.
    // Retain Vanilla's Greedy/Jealous consequences for private bedrooms,
    // but judge a shared sleeper's assigned barracks instead of treating it
    // as if the pawn had no lodging. Do not change room quality or any trait.
    internal static class SharedLodgingTraitUtility
    {
        public static Room BarracksFor(Pawn pawn)
        {
            if (pawn == null || !pawn.IsColonist || pawn.ownership == null ||
                pawn.ownership.OwnedRoom != null)
                return null;

            Room room = pawn.ownership.Bedroom;
            return room != null && room.Role == RoomRoleDefOf.Barracks ? room : null;
        }

        public static Room ComparableSleepingRoom(Pawn pawn)
        {
            if (pawn == null || pawn.ownership == null)
                return null;
            return pawn.ownership.OwnedRoom ?? BarracksFor(pawn);
        }
    }

    [HarmonyPatch(typeof(ThoughtWorker_Greedy), "CurrentStateInternal")]
    public static class SharedLodgingGreedyPatch
    {
        public static void Postfix(ThoughtWorker_Greedy __instance, Pawn p,
            ref ThoughtState __result)
        {
            Room room = SharedLodgingTraitUtility.BarracksFor(p);
            if (room == null)
                return;

            int stage = RoomStatDefOf.Impressiveness.GetScoreStageIndex(
                room.GetStat(RoomStatDefOf.Impressiveness)) + 1;

            __result = stage >= 0 && stage < __instance.def.stages.Count &&
                __instance.def.stages[stage] != null
                ? ThoughtState.ActiveAtStage(stage) : ThoughtState.Inactive;
        }
    }

    [HarmonyPatch(typeof(ThoughtWorker_BedroomJealous), "CurrentStateInternal")]
    public static class SharedLodgingJealousPatch
    {
        public static void Postfix(Pawn p, ref ThoughtState __result)
        {
            Room room = SharedLodgingTraitUtility.BarracksFor(p);
            if (room == null || p.Map == null)
                return;

            float own = room.GetStat(RoomStatDefOf.Impressiveness);
            Pawn envied = null;
            float best = 0f;
            foreach (Pawn other in p.Map.mapPawns.SpawnedPawnsInFaction(Faction.OfPlayer))
            {
                if (other == p || other.HostFaction != null || !other.RaceProps.Humanlike)
                    continue;

                Room otherRoom = SharedLodgingTraitUtility.ComparableSleepingRoom(other);
                if (otherRoom == null || otherRoom == room)
                    continue;

                float otherScore = otherRoom.GetStat(RoomStatDefOf.Impressiveness);
                if (otherScore > own && otherScore - own >= Mathf.Abs(own * 0.1f)
                    && (envied == null || otherScore > best))
                {
                    envied = other;
                    best = otherScore;
                }
            }
            __result = envied == null ? ThoughtState.Inactive :
                ThoughtState.ActiveWithReason(envied.LabelShort);
        }
    }
}
