using System;
using System.Collections.Generic;
using RimWorld;
using Verse;
using Verse.AI;

namespace AMJ.Culture
{
    // C-06: Runs only for a colonist who already owns the dedicated Culture
    // mat. No other Bedroll, bed, or chair changes its inherent isSittable flag.
    // The associated buildable ThingDef/art and exact resource costs are separate
    // production gates; do not rename this identity when its XML is added.
    internal static class MultipurposeMatMealSelector
    {
        internal const string MatDefName = "AMJ_CultureWovenMat";

        private struct MealSite
        {
            public Thing Seat;
            public float Mood;
            public float Comfort;
            public float Distance;
        }

        private static bool Better(MealSite candidate, MealSite current)
        {
            if (candidate.Mood != current.Mood)
                return candidate.Mood > current.Mood;
            if (candidate.Comfort != current.Comfort)
                return candidate.Comfort > current.Comfort;
            return candidate.Distance < current.Distance;
        }

        // Use the current loaded game stage; do not assign any benefit
        // merely because an item is called an irori, table, or mat.
        private static float PositiveMealRoomMood(IntVec3 cell, Map map)
        {
            Room room = cell.GetRoom(map);
            if (room == null || ThoughtDefOf.AteInImpressiveDiningRoom == null)
                return 0f;
            var stages = ThoughtDefOf.AteInImpressiveDiningRoom.stages;
            if (stages == null)
                return 0f;
            int stage = RoomStatDefOf.Impressiveness.GetScoreStageIndex(
                room.GetStat(RoomStatDefOf.Impressiveness));
            if (stage < 0 || stage >= stages.Count || stages[stage] == null)
                return 0f;
            return Math.Max(0f, stages[stage].baseMoodEffect);
        }

        private static MealSite Score(Thing seat, Pawn actor)
        {
            return new MealSite
            {
                Seat = seat,
                Mood = PositiveMealRoomMood(seat.Position, actor.Map),
                Comfort = seat.GetStatValue(StatDefOf.Comfort),
                Distance = (seat.Position - actor.Position).LengthHorizontal
            };
        }

        private static Building_Bed OwnUsableMat(Pawn actor, float radius)
        {
            if (actor.ownership == null || actor.Faction != Faction.OfPlayer ||
                actor.IsPrisoner || actor.IsSlave)
                return null;
            Building_Bed bed = actor.ownership.OwnedBed;
            if (bed == null || bed.def == null ||
                bed.def.defName != MatDefName ||
                bed.def.building == null || bed.def.building.isSittable ||
                bed.ForPrisoners || bed.Medical ||
                !bed.Spawned || bed.Map != actor.Map ||
                bed.IsForbidden(actor) || bed.IsBurning() ||
                bed.HostileTo(actor) || !bed.IsSociallyProper(actor) ||
                (bed.def.CanHaveFaction && bed.Faction != actor.Faction) ||
                !bed.Position.Standable(actor.Map) ||
                bed.Position.GetDangerFor(actor, actor.Map) != Danger.None ||
                (bed.Position - actor.Position).LengthHorizontal > radius ||
                !actor.CanReserve(bed, 1, -1, null, false) ||
                !actor.CanReserveSittableOrSpot(bed.Position) ||
                !actor.CanReach(bed, PathEndMode.OnCell, Danger.Deadly))
                return null;
            return bed;
        }

        // The existing C-03 ordinary-chair path remains untouched whenever
        // no dedicated mat is eligible. Scan the ordinary chairs at the same
        // ingest decision time only when one real owner-mat competes.
        internal static bool TryRedirect(Pawn actor, float radius)
        {
            Building_Bed mat = OwnUsableMat(actor, radius);
            if (mat == null)
                return false;

            MealSite best = Score(mat, actor);
            List<Thing> buildings = actor.Map.listerThings.ThingsInGroup(
                ThingRequestGroup.BuildingArtificial);
            for (int i = 0; i < buildings.Count; i++)
            {
                Thing chair = buildings[i];
                if (!TablelessChairMealPatch.IsUsableChair(actor, chair) ||
                    (chair.Position - actor.Position).LengthHorizontal > radius)
                    continue;
                MealSite candidate = Score(chair, actor);
                if (!Better(candidate, best) ||
                    !actor.CanReach(chair, PathEndMode.OnCell, Danger.Deadly))
                    continue;
                best = candidate;
            }

            IntVec3 destination = best.Seat.Position;
            if (!actor.ReserveSittableOrSpot(destination, actor.CurJob, false))
                return false;

            actor.Map.pawnDestinationReservationManager.Reserve(
                actor, actor.CurJob, destination);
            actor.pather.StartPath(destination, PathEndMode.OnCell);
            return true;
        }
    }
}
