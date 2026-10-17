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
            if (pawn == null || __result == null || __result.initAction == null)
                return;

            // The Toil is constructed before food pickup. Its actual destination
            // must be resolved inside initAction, just as Vanilla does.
            // Wrap the SAME Toil to preserve JobDriver's failure/reservation logic
            // and run Vanilla's original action whenever enza is inapplicable.
            Toil travel = __result;
            Action vanillaAction = travel.initAction;
            travel.initAction = delegate
            {
                Pawn actor = travel.actor;
                if (actor == null || !actor.Spawned || actor.Map == null ||
                    actor.CurJob == null || actor.CurJob.def != JobDefOf.Ingest)
                {
                    vanillaAction();
                    return;
                }

                Thing food = actor.CurJob.targetA.Thing;
                if (food == null || food.def.ingestible == null)
                {
                    vanillaAction();
                    return;
                }
                float searchRadius = food.def.ingestible.chairSearchRadius;
                if (searchRadius <= 0f)
                {
                    vanillaAction();
                    return;
                }

                ThingDef enza = DefDatabase<ThingDef>.GetNamedSilentFail("AMJ_Enza");
                if (enza == null)
                {
                    vanillaAction();
                    return;
                }

                // Never displace a functional Vanilla/MO table-adjacent seat.
                Thing normalSeat = GenClosest.ClosestThingReachable(
                    actor.Position, actor.Map,
                    ThingRequest.ForGroup(ThingRequestGroup.BuildingArtificial),
                    PathEndMode.OnCell, TraverseParms.For(actor), searchRadius,
                    seat => CanSit(actor, seat) && HasAdjacentEatSurface(seat));
                if (normalSeat != null)
                {
                    vanillaAction();
                    return;
                }

                Thing seatForMeal = GenClosest.ClosestThingReachable(
                    actor.Position, actor.Map,
                    ThingRequest.ForGroup(ThingRequestGroup.BuildingArtificial),
                    PathEndMode.OnCell, TraverseParms.For(actor), searchRadius,
                    seat => seat.def == enza && CanSit(actor, seat));
                if (seatForMeal == null ||
                    !actor.ReserveSittableOrSpot(seatForMeal.Position, actor.CurJob, false))
                {
                    vanillaAction();
                    return;
                }

                actor.Map.pawnDestinationReservationManager.Reserve(
                    actor, actor.CurJob, seatForMeal.Position);
                actor.pather.StartPath(seatForMeal.Position, PathEndMode.OnCell);
            };
        }
    }
}
