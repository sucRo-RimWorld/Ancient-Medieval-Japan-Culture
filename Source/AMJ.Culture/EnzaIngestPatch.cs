using System;
using HarmonyLib;
using RimWorld;
using Verse;
using Verse.AI;

namespace AMJ.Culture
{
    [StaticConstructorOnStartup]
    public static class EnzaBootstrap
    {
        static EnzaBootstrap()
        {
            new Harmony("sucro.ancientmedievaljapan.culture.enza").PatchAll();
        }
    }

    // Replace only the normal eating travel Toil, never room or food logic.
    [HarmonyPatch(typeof(Toils_Ingest), "CarryIngestibleToChewSpot", new Type[] { typeof(Pawn), typeof(TargetIndex) })]
    public static class EnzaIngestPatch
    {
        private static bool CanSit(Pawn pawn, Thing seat)
        {
            return seat != null && seat.Spawned && seat.def.building != null &&
                   seat.def.building.isSittable && seat.Faction == pawn.Faction &&
                   !seat.IsForbidden(pawn) && !seat.IsBurning() &&
                   !seat.HostileTo(pawn) && seat.IsSociallyProper(pawn) &&
                   seat.Position.GetDangerFor(pawn, seat.Map) == Danger.None &&
                   pawn.CanReserveSittableOrSpot(seat.Position);
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

        public static void Postfix(Pawn pawn, ref Toil __result)
        {
            if (pawn == null || !pawn.Spawned || pawn.Map == null ||
                pawn.CurJob == null || pawn.CurJob.def != JobDefOf.Ingest)
                return;

            Thing food = pawn.CurJob.targetA.Thing;
            if (food == null || food.def.ingestible == null)
                return;
            float searchRadius = food.def.ingestible.chairSearchRadius;
            if (searchRadius <= 0f)
                return;

            // Vanilla and MO table-adjacent seats take precedence.
            Thing tableSeat = GenClosest.ClosestThingReachable(
                pawn.Position, pawn.Map,
                ThingRequest.ForGroup(ThingRequestGroup.BuildingArtificial),
                PathEndMode.OnCell, TraverseParms.For(pawn), searchRadius,
                seat => CanSit(pawn, seat) && HasAdjacentEatSurface(seat));
            if (tableSeat != null)
                return;

            // Resolve loaded Def at meal time, not during static initialization.
            ThingDef enza = DefDatabase<ThingDef>.GetNamedSilentFail("AMJ_Enza");
            if (enza == null)
                return;
            Thing enzaSeat = GenClosest.ClosestThingReachable(
                pawn.Position, pawn.Map, ThingRequest.ForDef(enza),
                PathEndMode.OnCell, TraverseParms.For(pawn), searchRadius,
                seat => CanSit(pawn, seat));
            if (enzaSeat == null)
                return;

            Toil travel = new Toil();
            travel.initAction = delegate
            {
                Pawn actor = travel.actor;
                if (actor == null || actor.CurJob == null ||
                    !CanSit(actor, enzaSeat) ||
                    !actor.ReserveSittableOrSpot(enzaSeat.Position, actor.CurJob, false))
                {
                    if (actor != null && actor.jobs != null)
                        actor.jobs.EndCurrentJob(JobCondition.Incompletable);
                    return;
                }
                actor.Map.pawnDestinationReservationManager.Reserve(
                    actor, actor.CurJob, enzaSeat.Position);
                actor.pather.StartPath(enzaSeat.Position, PathEndMode.OnCell);
            };
            travel.defaultCompleteMode = ToilCompleteMode.PatherArrival;
            __result = travel;
        }
    }
}
