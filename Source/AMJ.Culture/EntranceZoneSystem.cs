using System;
using System.Collections.Generic;
using HarmonyLib;
using RimWorld;
using UnityEngine;
using Verse;
using Verse.AI;

namespace AMJ.Culture
{
    // A map-specific painted threshold. This is intentionally NOT a Verse.Zone:
    // storage/growing Zones cannot overlap, while a genkan must be paintable
    // above any floor and may coexist with a storage or growing Zone.
    public sealed class EntranceZoneMap : MapComponent
    {
        private List<IntVec3> cells = new List<IntVec3>();
        private HashSet<IntVec3> lookup = new HashSet<IntVec3>();
        private List<IntVec3> noShoesCells = new List<IntVec3>();
        private HashSet<IntVec3> noShoesLookup = new HashSet<IntVec3>();

        private static readonly Material ProgressFilled =
            SolidColorMaterials.SimpleSolidColorMaterial(new Color(0.76f, 0.72f, 0.45f));
        private static readonly Material ProgressEmpty =
            SolidColorMaterials.SimpleSolidColorMaterial(new Color(0.18f, 0.18f, 0.18f));

        public EntranceZoneMap(Map map) : base(map) { }

        public bool Contains(IntVec3 cell) { return lookup.Contains(cell); }
        public List<IntVec3> Cells { get { return cells; } }
        public List<IntVec3> NoShoesCells { get { return noShoesCells; } }
        public bool IsNoShoes(IntVec3 cell) { return noShoesLookup.Contains(cell); }
        public bool HasActiveArea { get { return cells.Count > 0 && noShoesCells.Count > 0; } }

        public static bool TouchByEdge(IntVec3 a, IntVec3 b)
        {
            return Math.Abs(a.x - b.x) + Math.Abs(a.z - b.z) == 1;
        }

        public bool HasNoShoesNeighbor(IntVec3 cell)
        {
            return IsNoShoes(new IntVec3(cell.x + 1, 0, cell.z)) ||
                   IsNoShoes(new IntVec3(cell.x - 1, 0, cell.z)) ||
                   IsNoShoes(new IntVec3(cell.x, 0, cell.z + 1)) ||
                   IsNoShoes(new IntVec3(cell.x, 0, cell.z - 1));
        }

        public void SetNoShoes(IntVec3 cell, bool enabled)
        {
            if (!cell.InBounds(map) || Contains(cell)) return;
            if (enabled)
            {
                if (noShoesLookup.Add(cell)) noShoesCells.Add(cell);
            }
            else if (noShoesLookup.Remove(cell))
                noShoesCells.Remove(cell);

            Game game = Current.Game;
            if (game != null)
            {
                EntranceFootwearState state = game.GetComponent<EntranceFootwearState>();
                if (state != null) state.CancelOnMap(map);
            }
        }

        public void Set(IntVec3 cell, bool enabled)
        {
            if (!cell.InBounds(map) || IsNoShoes(cell)) return;
            if (enabled)
            {
                if (lookup.Add(cell)) cells.Add(cell);
            }
            else if (lookup.Remove(cell))
            {
                cells.Remove(cell);
                Game game = Current.Game;
                if (game != null)
                {
                    EntranceFootwearState state = game.GetComponent<EntranceFootwearState>();
                    if (state != null) state.CancelAt(map, cell);
                }
            }
        }

        public override void ExposeData()
        {
            base.ExposeData();
            Scribe_Collections.Look(ref cells, "AMJCultureEntranceCells", LookMode.Value);
            Scribe_Collections.Look(ref noShoesCells, "AMJCultureNoShoesCells", LookMode.Value);
            if (Scribe.mode == LoadSaveMode.PostLoadInit)
            {
                if (cells == null) cells = new List<IntVec3>();
                lookup = new HashSet<IntVec3>(cells);
                if (noShoesCells == null) noShoesCells = new List<IntVec3>();
                noShoesCells.RemoveAll(cell => lookup.Contains(cell));
                noShoesLookup = new HashSet<IntVec3>(noShoesCells);
            }
        }

        public override void MapComponentUpdate()
        {
            if (Current.Game == null || Find.CurrentMap != map) return;
            EntranceFootwearState state = Current.Game.GetComponent<EntranceFootwearState>();
            if (state == null || state.ActivePauses.Count == 0) return;

            int now = Find.TickManager.TicksGame;
            foreach (KeyValuePair<Pawn, EntrancePause> pair in state.ActivePauses)
            {
                Pawn pawn = pair.Key;
                EntrancePause pause = pair.Value;
                if (pawn != null && pawn.Spawned && pawn.Map == map)
                    GenDraw.DrawFillableBar(new GenDraw.FillableBarRequest
                    {
                        center = pawn.DrawPos + new Vector3(0f, 0.1f, 0.7f),
                        size = new Vector2(0.7f, 0.1f),
                        fillPercent = Mathf.Clamp01((now - pause.StartTick) / (float)EntranceFootwearState.CastTicks),
                        filledMat = ProgressFilled,
                        unfilledMat = ProgressEmpty,
                        margin = 0.07f
                    });
            }
        }
    }

    internal sealed class EntrancePause
    {
        public readonly Map Map;
        public readonly IntVec3 Cell;
        public readonly Job Job;
        public readonly int StartTick;
        public readonly bool TargetBarefoot;

        public EntrancePause(Pawn pawn, bool targetBarefoot, int now)
        {
            Map = pawn.Map;
            Cell = pawn.Position;
            Job = pawn.CurJob;
            StartTick = now;
            TargetBarefoot = targetBarefoot;
        }
    }

    // The side of a crossing is bound to its actual movement job, not to the
    // overall destination. A long Job may cross several separate thresholds.
    internal sealed class EntranceApproach
    {
        public readonly Map Map;
        public readonly Job Job;
        public readonly bool Indoor;

        public EntranceApproach(Pawn pawn, bool indoor)
        {
            Map = pawn.Map;
            Job = pawn.CurJob;
            Indoor = indoor;
        }
    }

    // Pawn IDs, unlike MapComponents, survive caravan departures and returns.
    // Interrupted casts are transient: after loading, the original Job/path
    // resumes and a new cast begins if the crossing is still needed.
    public sealed class EntranceFootwearState : GameComponent
    {
        public const int CastTicks = 90;

        private List<string> barefootPawnIds = new List<string>();
        private HashSet<string> barefootLookup = new HashSet<string>();
        private readonly Dictionary<Pawn, EntrancePause> pauses = new Dictionary<Pawn, EntrancePause>();
        private readonly Dictionary<Pawn, EntranceApproach> approaches = new Dictionary<Pawn, EntranceApproach>();

        public EntranceFootwearState(Game game) { }

        internal Dictionary<Pawn, EntrancePause> ActivePauses { get { return pauses; } }

        public bool IsBarefoot(Pawn pawn)
        {
            return pawn != null && barefootLookup.Contains(pawn.ThingID);
        }

        internal void SetBarefoot(Pawn pawn, bool barefoot)
        {
            if (pawn == null) return;
            string id = pawn.ThingID;
            if (barefoot)
            {
                if (barefootLookup.Add(id)) barefootPawnIds.Add(id);
                DiscardCarriedDirt(pawn);
            }
            else if (barefootLookup.Remove(id))
                barefootPawnIds.Remove(id);
        }

        internal void NoteApproach(Pawn pawn, bool indoor)
        {
            approaches[pawn] = new EntranceApproach(pawn, indoor);
        }

        internal bool TryGetApproach(Pawn pawn, out bool indoor)
        {
            EntranceApproach approach;
            if (approaches.TryGetValue(pawn, out approach) &&
                approach.Map == pawn.Map && approach.Job == pawn.CurJob)
            {
                indoor = approach.Indoor;
                return true;
            }

            approaches.Remove(pawn);
            indoor = false;
            return false;
        }

        internal void ClearApproach(Pawn pawn)
        {
            approaches.Remove(pawn);
        }

        internal void BeginCast(Pawn pawn, bool targetBarefoot, int now)
        {
            pauses[pawn] = new EntrancePause(pawn, targetBarefoot, now);
        }

        internal bool ProcessCast(Pawn pawn, int now)
        {
            EntrancePause pause;
            if (!pauses.TryGetValue(pawn, out pause)) return false;

            if (pawn.Map != pause.Map || pawn.Position != pause.Cell ||
                !pawn.pather.Moving || pawn.CurJob != pause.Job ||
                !pawn.Map.GetComponent<EntranceZoneMap>().Contains(pause.Cell))
            {
                pauses.Remove(pawn);
                return false;
            }

            if (now - pause.StartTick < CastTicks)
                return true; // Delay only the path tick; no job or reservation is replaced.

            pauses.Remove(pawn);
            SetBarefoot(pawn, pause.TargetBarefoot);
            return false;
        }

        internal void CancelAndForget(Pawn pawn)
        {
            pauses.Remove(pawn);
            approaches.Remove(pawn);
        }

        internal void CancelAt(Map map, IntVec3 cell)
        {
            List<Pawn> cancelled = null;
            foreach (KeyValuePair<Pawn, EntrancePause> pair in pauses)
                if (pair.Value.Map == map && pair.Value.Cell == cell)
                {
                    if (cancelled == null) cancelled = new List<Pawn>();
                    cancelled.Add(pair.Key);
                }

            if (cancelled != null)
                foreach (Pawn pawn in cancelled) CancelAndForget(pawn);
        }

        private static readonly System.Reflection.FieldInfo CarriedFilthField =
            AccessTools.Field(typeof(Pawn_FilthTracker), "carriedFilth");

        // Only mud/dirt carried on shoes is removed. Blood, vomit and other
        // work/medical filth stay with Vanilla's normal filth lifecycle.
        internal static void DiscardCarriedDirt(Pawn pawn)
        {
            if (pawn == null || pawn.filth == null || CarriedFilthField == null) return;
            List<Filth> carried = CarriedFilthField.GetValue(pawn.filth) as List<Filth>;
            if (carried == null) return;
            carried.RemoveAll(f => f != null && f.def != null &&
                                  f.def.defName == "Filth_Dirt");
        }

        public override void ExposeData()
        {
            base.ExposeData();
            Scribe_Collections.Look(ref barefootPawnIds, "AMJCultureBarefootPawnIds", LookMode.Value);
            if (Scribe.mode == LoadSaveMode.PostLoadInit)
            {
                if (barefootPawnIds == null) barefootPawnIds = new List<string>();
                barefootLookup = new HashSet<string>(barefootPawnIds);
                pauses.Clear();
                approaches.Clear();
            }
        }
    }

    // Runs before Vanilla's movement tick, leaving its path, job, and reserved
    // destination intact. Only player-controlled human pawns participate.
    [HarmonyPatch(typeof(Pawn_PathFollower), "PatherTick")]
    public static class EntrancePathPausePatch
    {
        private static bool IsInterior(IntVec3 cell, Map map)
        {
            if (!cell.InBounds(map)) return false;
            Room room = cell.GetRoom(map);
            return room != null && !room.PsychologicallyOutdoors;
        }

        private static bool IsEmergency(Pawn pawn)
        {
            if (pawn.Drafted || pawn.InMentalState || pawn.Downed ||
                pawn.Position.GetDangerFor(pawn, pawn.Map) != Danger.None)
                return true;

            Job job = pawn.CurJob;
            if (job == null || job.def == null) return true;
            switch (job.def.defName)
            {
                case "Rescue":
                case "Capture":
                case "Flee":
                case "FleeAndCower":
                case "ExtinguishSelf":
                case "BeatFire":
                case "FightFire":
                case "AttackMelee":
                case "AttackStatic":
                case "TendPatient":
                    return true;
                default:
                    return false;
            }
        }

        public static bool Prefix(Pawn_PathFollower __instance, Pawn ___pawn)
        {
            Pawn pawn = ___pawn;
            if (pawn == null || !pawn.Spawned || pawn.Map == null ||
                !pawn.RaceProps.Humanlike || !pawn.IsColonistPlayerControlled)
                return true;

            Game game = Current.Game;
            if (game == null || !__instance.Moving) return true;
            EntranceFootwearState state = game.GetComponent<EntranceFootwearState>();
            EntranceZoneMap zone = pawn.Map.GetComponent<EntranceZoneMap>();
            if (state == null || zone == null) return true;

            if (IsEmergency(pawn))
            {
                state.CancelAndForget(pawn);
                return true;
            }

            int tick = Find.TickManager.TicksGame;
            if (state.ProcessCast(pawn, tick)) return false;

            // Capture the actual side BEFORE stepping onto the zone. At a
            // door, the zone cell itself may already belong to the room.
            bool here = zone.Contains(pawn.Position);
            bool next = zone.Contains(__instance.nextCell);
            if (!here)
            {
                if (next && pawn.Position != __instance.nextCell)
                    state.NoteApproach(pawn, IsInterior(pawn.Position, pawn.Map));
                else
                    state.ClearApproach(pawn);
                return true;
            }

            // Wait until the path leaves THIS contiguous painted threshold.
            // Looking at the final Job destination is incorrect when a pawn
            // enters via entrance A and later exits via entrance B in one Job.
            if (next || __instance.nextCell == pawn.Position) return true;

            bool approachIndoor;
            if (!state.TryGetApproach(pawn, out approachIndoor))
                return true;

            bool exitIndoor = IsInterior(__instance.nextCell, pawn.Map);
            if (approachIndoor == exitIndoor ||
                state.IsBarefoot(pawn) == exitIndoor)
            {
                state.ClearApproach(pawn);
                return true;
            }

            // Cast on the last threshold cell, immediately before leaving.
            // The completed cast changes the pawn state, so the same exit
            // cannot cast a second time, even if the path is processed again.
            state.BeginCast(pawn, exitIndoor, tick);
            return false;
        }
    }
}
