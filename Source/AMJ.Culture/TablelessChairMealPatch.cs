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

    // Treat an ordinary sittable chair as an eating destination when no
    // reachable eating table is available. Never change HasEatSurface, food
    // effects, dining-room Thoughts, or the original ingest JobDriver.
    [HarmonyPatch(typeof(Toils_Ingest), "CarryIngestibleToChewSpot",
        new Type[] { typeof(Pawn), typeof(TargetIndex) })]
    public static class TablelessChairMealPatch
    {
        private static bool IsUsable(Pawn pawn, Thing furniture)
        {
            return furniture != null && furniture.Spawned &&
                !furniture.IsForbidden(pawn) && !furniture.IsBurning() &&
                !furniture.HostileTo(pawn) && furniture.IsSociallyProper(pawn) &&
                furniture.Position.GetDangerFor(pawn, furniture.Map) == Danger.None;
        }

        private static bool IsUsableChair(Pawn pawn, Thing furniture)
        {
            return IsUsable(pawn, furniture) &&
                furniture.def.building != null &&
                furniture.def.building.isSittable &&
                furniture.Faction == pawn.Faction &&
                furniture.Position.Standable(furniture.Map) &&
                pawn.CanReserveSittableOrSpot(furniture.Position);
        }

        private static Thing FindFurniture(
            Pawn pawn, float radius, PathEndMode endMode, Predicate<Thing> validator)
        {
            return GenClosest.ClosestThingReachable(
                pawn.Position, pawn.Map,
                ThingRequest.ForGroup(ThingRequestGroup.BuildingArtificial),
                endMode, TraverseParms.For(pawn), radius, validator);
        }

        public static void Postfix(Pawn pawn, TargetIndex ingestibleInd, ref Toil __result)
        {
            if (pawn == null || __result == null || __result.initAction == null)
                return;

            // Retain the original Toil, its completion mode and other actions.
            // The actual meal and placement are resolved when the Toil begins.
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

                Thing meal = actor.CurJob.GetTarget(ingestibleInd).Thing;
                if (meal == null || meal.def.ingestible == null ||
                    meal.def.ingestible.chairSearchRadius <= 0f)
                {
                    originalAction();
                    return;
                }

                float radius = meal.def.ingestible.chairSearchRadius;
                // Existing Vanilla/MO tables take precedence. A table need
                // not have a chair to keep its original ingestion behavior.
                Thing table = FindFurniture(actor, radius, PathEndMode.Touch,
                    candidate => IsUsable(actor, candidate) &&
                        candidate.def.surfaceType == SurfaceType.Eat);
                if (table != null)
                {
                    originalAction();
                    return;
                }

                Thing chair = FindFurniture(actor, radius, PathEndMode.OnCell,
                    candidate => IsUsableChair(actor, candidate));
                if (chair == null ||
                    !actor.ReserveSittableOrSpot(chair.Position, actor.CurJob, false))
                {
                    originalAction();
                    return;
                }

                // Use the same seat/spot reservation and walking semantics as
                // the original game; do not convert chairs into Eat surfaces.
                actor.Map.pawnDestinationReservationManager.Reserve(
                    actor, actor.CurJob, chair.Position);
                actor.pather.StartPath(chair.Position, PathEndMode.OnCell);
            };
        }
    }
}
