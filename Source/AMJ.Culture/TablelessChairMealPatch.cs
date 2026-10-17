using System;
using HarmonyLib;
using RimWorld;
using Verse;
using Verse.AI;

namespace AMJ.Culture
{
    [StaticConstructorOnStartup]
    public static class CultureMealSeatBootstrap
    {
        static CultureMealSeatBootstrap()
        {
            new Harmony("sucro.ancientmedievaljapan.culture.mealseat").PatchAll();
        }
    }

    // Picnic Area precedent: redirect the ordinary ingest travel Toil to an
    // ordinary sittable building without requiring an adjacent eating surface.
    // Unlike Picnic Area, there is no zone, weather gate, or random selection.
    [HarmonyPatch(typeof(Toils_Ingest), "CarryIngestibleToChewSpot",
        new Type[] { typeof(Pawn), typeof(TargetIndex) })]
    public static class TablelessChairMealPatch
    {
        private static bool IsUsableChair(Pawn pawn, Thing chair)
        {
            if (chair == null || !chair.Spawned || chair.def.building == null ||
                !chair.def.building.isSittable ||
                !chair.Position.Standable(chair.Map) ||
                chair.IsForbidden(pawn) || chair.IsBurning() ||
                chair.HostileTo(pawn) || !chair.IsSociallyProper(pawn) ||
                (chair.def.CanHaveFaction && chair.Faction != pawn.Faction) ||
                !pawn.CanReserve(chair, 1, -1, null, false))
                return false;

            return chair.Position.GetDangerFor(pawn, chair.Map) == Danger.None;
        }

        private static bool HasAdjacentEatSurface(Thing chair)
        {
            foreach (IntVec3 direction in GenAdj.CardinalDirections)
            {
                Building neighbor = (chair.Position + direction).GetEdifice(chair.Map);
                if (neighbor != null && neighbor.def.surfaceType == SurfaceType.Eat)
                    return true;
            }
            return false;
        }

        private static Thing FindChair(Pawn pawn, float radius, bool requireTable)
        {
            return GenClosest.ClosestThingReachable(
                pawn.Position, pawn.Map,
                ThingRequest.ForGroup(ThingRequestGroup.BuildingArtificial),
                PathEndMode.OnCell,
                TraverseParms.For(pawn, Danger.Deadly, TraverseMode.ByPawn, false),
                radius,
                candidate => IsUsableChair(pawn, candidate) &&
                    (!requireTable || HasAdjacentEatSurface(candidate)));
        }

        public static void Postfix(Pawn pawn, TargetIndex ingestibleInd, ref Toil __result)
        {
            if (pawn == null || __result == null || __result.initAction == null)
                return;

            // Search after the ingestible has been picked up, as Vanilla does.
            // Keep the original Toil and its arrival completion behavior.
            Toil travel = __result;
            Action originalAction = travel.initAction;
            travel.initAction = delegate
            {
                Pawn actor = travel.actor;
                if (actor == null || !actor.Spawned || actor.Map == null ||
                    actor.CurJob == null || actor.CurJob.def != JobDefOf.Ingest)
                {
                    originalAction();
                    return;
                }

                Thing food = actor.CurJob.GetTarget(ingestibleInd).Thing;
                if (food == null || food.def.ingestible == null ||
                    food.def.ingestible.chairSearchRadius <= 0f)
                {
                    originalAction();
                    return;
                }

                float radius = food.def.ingestible.chairSearchRadius;

                // This is Vanilla's actual table condition: a valid seat
                // ADJACENT TO an Eat surface, not the mere existence of a table.
                // Preserve Vanilla's table-seat reservation and travel when
                // such a chair is available.
                if (FindChair(actor, radius, true) != null)
                {
                    originalAction();
                    return;
                }

                // Picnic Area's useful mechanism, generalized to ANY chair.
                // No table, enza Def, zone or extra furniture is required.
                Thing chair = FindChair(actor, radius, false);
                if (chair == null ||
                    !actor.Reserve(chair, actor.CurJob, 1, -1, null, false))
                {
                    originalAction();
                    return;
                }

                actor.Map.pawnDestinationReservationManager.Reserve(
                    actor, actor.CurJob, chair.Position);
                actor.pather.StartPath(chair.Position, PathEndMode.OnCell);
            };
        }
    }
}
