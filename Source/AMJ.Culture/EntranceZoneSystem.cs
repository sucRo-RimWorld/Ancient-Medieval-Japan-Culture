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

        // Verified by a focused real screenshot, not merely the cast-state assertion.
        private static int lastProgressGuiTick = -1;

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

        // Editor-only connectivity check. A multi-cell painted threshold can
        // contact a no-shoes area at one end, not necessarily every cell.
        public bool HasConnectedNoShoesContact(IntVec3 start)
        {
            if (!Contains(start) || noShoesCells.Count == 0) return false;
            Queue<IntVec3> pending = new Queue<IntVec3>();
            HashSet<IntVec3> seen = new HashSet<IntVec3>();
            pending.Enqueue(start);
            seen.Add(start);
            while (pending.Count > 0)
            {
                IntVec3 cell = pending.Dequeue();
                if (HasNoShoesNeighbor(cell)) return true;
                AddConnected(cell.x + 1, cell.z, pending, seen);
                AddConnected(cell.x - 1, cell.z, pending, seen);
                AddConnected(cell.x, cell.z + 1, pending, seen);
                AddConnected(cell.x, cell.z - 1, pending, seen);
            }
            return false;
        }

        private void AddConnected(int x, int z, Queue<IntVec3> pending, HashSet<IntVec3> seen)
        {
            IntVec3 cell = new IntVec3(x, 0, z);
            if (Contains(cell) && seen.Add(cell)) pending.Enqueue(cell);
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

        public override void MapComponentOnGUI()
        {
            if (Current.Game == null || Find.CurrentMap != map ||
                Event.current.type != EventType.Repaint) return;
            EntranceFootwearState state = Current.Game.GetComponent<EntranceFootwearState>();
            if (state == null || state.ActivePauses.Count == 0) return;

            // GenDraw's world-space fillable bar was not visible in the genuine
            // 1.6 screenshot despite its draw path running. Render a small,
            // camera-projected overlay on the map GUI instead.
            Camera camera = Find.Camera;
            if (camera == null || Screen.height <= 0) return;
            float screenToUi = UI.screenHeight / (float)Screen.height;
            int now = Find.TickManager.TicksGame;
            Color prior = GUI.color;
            try
            {
                foreach (KeyValuePair<Pawn, EntrancePause> pair in state.ActivePauses)
                {
                    Pawn pawn = pair.Key;
                    if (pawn == null || !pawn.Spawned || pawn.Map != map) continue;
                    Vector3 screen = camera.WorldToScreenPoint(
                        pawn.DrawPos + new Vector3(0f, 0f, 0.65f));
                    if (screen.z <= 0f) continue;
                    float x = screen.x * screenToUi;
                    float y = UI.screenHeight - screen.y * screenToUi;
                    Rect bg = new Rect(x - 24f, y - 15f, 48f, 7f);
                    if (bg.xMax < 0 || bg.yMax < 0 ||
                        bg.xMin > UI.screenWidth || bg.yMin > UI.screenHeight) continue;

                    GUI.color = new Color(0.12f, 0.12f, 0.12f, 0.95f);
                    GUI.DrawTexture(bg, BaseContent.WhiteTex);
                    float fraction = Mathf.Clamp01(
                        (now - pair.Value.StartTick) / (float)EntranceFootwearState.CastTicks);
                    GUI.color = new Color(0.76f, 0.72f, 0.45f, 1f);
                    GUI.DrawTexture(new Rect(bg.x + 1f, bg.y + 1f,
                        (bg.width - 2f) * fraction, bg.height - 2f), BaseContent.WhiteTex);
                    lastProgressGuiTick = now;
                }
            }
            finally
            {
                GUI.color = prior;
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
        public readonly bool NoShoes;

        public EntranceApproach(Pawn pawn, bool noShoes)
        {
            Map = pawn.Map;
            Job = pawn.CurJob;
            NoShoes = noShoes;
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

        internal void NoteApproach(Pawn pawn, bool noShoes)
        {
            approaches[pawn] = new EntranceApproach(pawn, noShoes);
        }

        internal bool TryGetApproach(Pawn pawn, out bool noShoes)
        {
            EntranceApproach approach;
            if (approaches.TryGetValue(pawn, out approach) &&
                approach.Map == pawn.Map && approach.Job == pawn.CurJob)
            {
                noShoes = approach.NoShoes;
                return true;
            }

            approaches.Remove(pawn);
            noShoes = false;
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

        internal void CancelOnMap(Map map)
        {
            // Zone editing is rare: cancel only affected transient crossing data.
            List<Pawn> affected = new List<Pawn>();
            foreach (KeyValuePair<Pawn, EntrancePause> pair in pauses)
                if (pair.Value.Map == map) affected.Add(pair.Key);
            foreach (Pawn pawn in affected) CancelAndForget(pawn);
            affected.Clear();
            foreach (KeyValuePair<Pawn, EntranceApproach> pair in approaches)
                if (pair.Value.Map == map) affected.Add(pair.Key);
            foreach (Pawn pawn in affected) CancelAndForget(pawn);
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
            if (state == null || zone == null || !zone.HasActiveArea) return true;

            // Most movement is nowhere near a threshold. Only the two grid
            // lookups below run before the hot-path exit, not danger/room scans.
            bool here = zone.Contains(pawn.Position);
            bool next = zone.Contains(__instance.nextCell);
            if (!here && !next)
            {
                state.CancelAndForget(pawn);
                return true;
            }

            if (IsEmergency(pawn))
            {
                state.CancelAndForget(pawn);
                return true;
            }

            int tick = Find.TickManager.TicksGame;
            if (state.ProcessCast(pawn, tick)) return false;

            // Capture the actual source-side cell before entering the zone.
            if (!here)
            {
                if (next && pawn.Position != __instance.nextCell)
                    state.NoteApproach(pawn, zone.IsNoShoes(pawn.Position) &&
                                             EntranceZoneMap.TouchByEdge(pawn.Position, __instance.nextCell));
                else
                    state.ClearApproach(pawn);
                return true;
            }

            // Wait until the path leaves THIS contiguous painted threshold.
            // Looking at the final Job destination is incorrect when a pawn
            // enters via entrance A and later exits via entrance B in one Job.
            if (next || __instance.nextCell == pawn.Position) return true;

            bool approachNoShoes;
            if (!state.TryGetApproach(pawn, out approachNoShoes))
                return true;

            bool exitNoShoes = zone.IsNoShoes(__instance.nextCell) &&
                               EntranceZoneMap.TouchByEdge(pawn.Position, __instance.nextCell);
            if (approachNoShoes == exitNoShoes ||
                state.IsBarefoot(pawn) == exitNoShoes)
            {
                state.ClearApproach(pawn);
                return true;
            }

            // Cast on the last threshold cell, immediately before leaving.
            // The completed cast changes the pawn state, so the same exit
            // cannot cast a second time, even if the path is processed again.
            state.BeginCast(pawn, exitNoShoes, tick);
            return false;
        }
    }
}
