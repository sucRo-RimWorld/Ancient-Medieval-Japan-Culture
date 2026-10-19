using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using RimWorks.Pickle;
using RimWorld;
using Verse;

namespace AMJ.Culture.Test
{
    [PickleSteps]
    public sealed class Background16Steps
    {
        // Exact owner sources: 76 adult XML exclusions + separately restricted Fervent.
        private static readonly string[] Adult =
        {
            "FerventResearcher72",
            "AerospaceEngineer44",
            "ComputerEngineer10",
            "ComputerEngineer36",
            "DeepSpaceSurveyor1",
            "GlitterworldSurgeon15",
            "OrbitalReservist22",
            "SpaceMarine51",
            "SpaceMarineMedic10",
            "SpaceNavyTech37",
            "SpaceResearcher25",
            "SpaceTechnician29",
            "SpaceshipSalesman34",
            "StarshipJanitor33",
            "AIProgrammer22",
            "AIResearcher94",
            "AceFighterPilot54",
            "ArchotechSpy75",
            "BattleMechanic27",
            "BattlefieldTech52",
            "CombatMedtech59",
            "CorpResearcher71",
            "EngineeredPilot75",
            "GeneticEngineer89",
            "GeneticScientist66",
            "MechWarVeteran77",
            "NetworkEngineer34",
            "Roboticist1",
            "Roboticist41",
            "RocketEngineer53",
            "RocketPioneer19",
            "CasketBuilder52",
            "ChiefEngineer62",
            "ColonyEngineer7",
            "CorpResearcher93",
            "ExecutiveOfficer5",
            "BiosphereManager95",
            "ArtifactHunter48",
            "ShadowMarine63",
            "GameTester77",
            "Adventurer19",
            "Explorer49",
            "ExplorerWriter10",
            "TestSubject90",
            "HunterOfTheKing53",
            "SpaceBartender0",
            "JoywireArtist8",
            "VRDesigner87",
            "ArmyScientist35",
            "CloneFarmer58",
            "DoomsdayPariah18",
            "Gnomebiologist96",
            "LazyProgrammer3",
            "MilitaryEngineer45",
            "NavyScientist52",
            "RugbyPlayer71",
            "StarfighterPilot79",
            "Technician9",
            "TestSubject92",
            "UrbworldRebel56",
            "WhiteHatHacker82",
            "AlcoholicTrucker93",
            "BloodgameSurvivor6",
            "MedicalScientist16",
            "IllegalShipwright76",
            "VideoProducer83",
            "CosmeticSurgeon36",
            "NeuroScientist19",
            "SoftwareDeveloper84",
            "ExiledResearcher44",
            "EnergyResearcher89",
            "MadScientist31",
            "JoeCustomAdulthood",
            "LiamCustomAdulthood",
            "OskarCustomAdulthood",
            "TynanCustomAdulthood",
            "Streamer52"
        };

        // Exact production SolidChildBioGatePatch exclusions.
        private static readonly HashSet<string> Child = new HashSet<string>
        {
            "ComputerGeek62",
            "GlitterworldKid85",
            "GlitterworldNerd20",
            "MechanoidNerd10",
            "SpaceCadet77",
            "SpaceFanboy22",
            "SpaceNerd97",
            "CorporateSlave22",
            "HackerKid98",
            "HackerProdigy84",
            "MechanoidHacker93",
            "VideoGamer16",
            "FallenProdigy40",
            "ChildResearcher32",
            "HeadjackAddict4",
            "WealthyStudent59",
            "CorpBredStudent54",
            "CorpStudent95",
            "OffworldRecruit91",
            "AwkwardNerd48",
            "DataDecoder39",
            "Bookworm3",
            "FarmMechanic1",
            "DiscardedYouth93",
            "CloneFarmed21",
            "VatgrownAssassin20",
            "VatgrownScientist91",
            "JoywireAddict76",
            "LabGrownChild22",
            "DreadedBaby56",
            "GameFanatic86",
            "ReclusiveProdigy96",
            "AbandonedChild30",
            "AspiringPhysicist47",
            "CaravanChild53",
            "ChildScientist30",
            "Colonist22",
            "CrimeBossChild39",
            "DiplomatsChild97",
            "FeralChild85",
            "LogicalChild2",
            "MidworldCadet83",
            "MidworldGeek18",
            "PilotFan16",
            "ProfessionalGamer76",
            "RebelWriter82",
            "ShipChild46",
            "SocialPariah3",
            "StarforceCadet79",
            "StudentEngineer34",
            "ToxicChild96",
            "VatgrownSlavegirl8",
            "VideoGamer55",
            "VidtubeStar98",
            "VoyagerChild94"
        };

        // Exact shuffled-child XML exclusions.
        private static readonly string[] ShuffledChild = {
            "OrganFarm67",
            "VatgrownSoldier8",
            "TestSubject15"
        };

        // Approved Culture Phase 2 titles, mapped by unchanged original DefName.
        // Every title must load from the actual 1.6/1.7 game XML, not merely exist in source.
        private static readonly Dictionary<string, string> AdaptedTitles =
            new Dictionary<string, string>
        {
            { "AddictionCounsel60", "village helper" },
            { "Architect28", "temple scholar" },
            { "BountyHunter41", "battle-hardened warrior" },
            { "Castaway81", "mountain village life" },
            { "CharityWorker36", "compassionate monk" },
            { "CorporateBuilder58", "veteran construction laborer" },
            { "ExpertHandyman8", "skilled craftsperson" },
            { "FurnitureBuilder83", "mountain carpenter" },
            { "GangSoldier39", "veteran bandit" },
            { "LanguageAnalyst27", "wise cultivator" },
            { "MasterChef48", "manor cook" },
            { "MedicSoldier54", "healing monk" },
            { "MilitaryOfficer17", "castle guard" },
            { "Minister88", "lord's retainer" },
            { "Model99", "beautiful performer" },
            { "NinjaAssassin31", "hired tough" },
            { "Novelist7", "tale writer" },
            { "PirateTrooper73", "fierce warrior" },
            { "PlanetaryDiplomat7", "envoy" },
            { "PrisonerOfWar2", "ashigaru commander" },
            { "ReligiousHierarch16", "temple abbot" },
            { "RunawayDancer35", "traveling performer" },
            { "Sculptor63", "Buddhist sculptor" },
            { "SoleSurvivor85", "battle-scarred veteran" },
            { "StationSecurity38", "checkpoint watchman" },
            { "ChildStar74", "performer's child" },
            { "CrashBaby32", "merchant's child" },
            { "UrbworldUrchin61", "town troublemaker" },
            { "UrbworldDrone32", "stonemason" },
            { "UrbworldEntrepreneur14", "market merchant" },
            { "ConstructionEngineer32", "experienced carpenter" },
            { "MafiaBoss17", "bandit leader" },
            { "ContractMiner86", "skilled miner" },
            { "Taxonomist0", "naturalist" },
            { "CaveworldIlluminator95", "mine veteran" },
            { "AmateurEngineer3", "curious tinkerer" },
            { "ArtStudent79", "painter" },
            { "ArtisanFarmer23", "field-raised child" },
            { "AspiringPopIdol28", "raised in the arts" },
            { "Athlete47", "martial student" },
            { "BehaviourResearch74", "master physician" },
            { "CivilServant2", "estate scribe" },
            { "ClassClown96", "young artist" },
            { "CoreDilettante33", "wealthy child" },
            { "CoreworldStudent50", "self-styled child" },
            { "DesertRat47", "mountain temple child" },
            { "DestroyerGeneral26", "warlord" },
            { "EnergeticPopIdol61", "talented young performer" },
            { "FelineScientist76", "medical apprentice" },
            { "FoundryApprentice76", "forge-raised child" },
            { "Fugitive4", "wanderer" },
            { "GunDealer14", "arms merchant" },
            { "Gymnast48", "sumo-loving child" },
            { "HedgeFundManager54", "shrewd painter" },
            { "HillbillyProdigy60", "mountain eccentric" },
            { "IcePlanetChild95", "snow-country child" },
            { "IndworldUrchin73", "alley-raised child" },
            { "LaborCampOrphan91", "construction-yard child" },
            { "MadScientist47", "cold prodigy" },
            { "MarineCadet73", "warrior-household child" },
            { "Mathematician6", "arithmetician" },
            { "MechanicsEngineer64", "ingenious carpenter" },
            { "MedicalStudent38", "healing monk's apprentice" },
            { "MedicalStudent96", "physician's child" },
            { "MilitaryGunsmith27", "military armorer" },
            { "MilitaryInventor35", "camp fortification artisan" },
            { "MindwipedAssassin50", "merciless assassin" },
            { "Nerd92", "inquisitive child" },
            { "Orphan15", "kindhearted orphan" },
            { "Osteologist74", "military strategist" },
            { "Pampered87", "sheltered child" },
            { "Philosopher82", "debate-loving child" },
            { "PrivilegedProdigy70", "court noble's child" },
            { "PsychiatricPatient94", "recluse" },
            { "PyroAssistant82", "fire-loving child" },
            { "RebelSlave14", "defiant servant" },
            { "ScienceProdigy65", "bright child" },
            { "SewerKid57", "kitchen-raised child" },
            { "Sheriff52", "town patrol" },
            { "SlaveChemist84", "herbal physician" },
            { "Smuggler23", "illicit peddler" },
            { "StationWhelp94", "bandit-raised child" },
            { "StreetPeddler66", "peddler's child" },
            { "Student65", "young medical scholar" },
            { "StudentSocialite89", "eloquent child" },
            { "ToolMechanic77", "master woodworker" },
            { "TragicLoner87", "mountain hut child" },
            { "TransferStudent10", "temple ward" },
            { "WarshipCaptain67", "naval captain" }
        };

        private static string LoadedText(BackstoryDef backstory, string fieldName)
        {
            const BindingFlags flags = BindingFlags.Instance | BindingFlags.Public |
                                       BindingFlags.NonPublic;
            FieldInfo field = typeof(BackstoryDef).GetField(fieldName, flags);
            if (field != null)
                return field.GetValue(backstory) as string;
            PropertyInfo property = typeof(BackstoryDef).GetProperty(fieldName, flags);
            return property == null ? null : property.GetValue(backstory, null) as string;
        }

        private static Task OnGameThread(Action action)
        {
            var tcs = new TaskCompletionSource<bool>();
            var driver = typeof(PickleContext).Assembly.GetType(
                "RimWorks.Pickle.Runtime.PickleDriver", true);
            var post = driver.GetMethod("Post", new[] { typeof(Action) });
            if (post == null)
                throw new MissingMethodException(driver.FullName, "Post(Action)");
            post.Invoke(null, new object[] { new Action(delegate
            {
                try
                {
                    if (!UnityData.IsInMainThread)
                        throw new InvalidOperationException("Not on Unity game thread");
                    action();
                    tcs.SetResult(true);
                }
                catch (Exception error) { tcs.SetException(error); }
            }) });
            return tcs.Task;
        }

        [Then("anachronistic backstories are restricted and ordinary pawns still generate")]
        public async Task Check(PickleContext ctx)
        {
            await ctx.WaitUntil(() =>
            {
                var ready = Type.GetType("AMJ.Culture.Test.ChairQuickstart, AMJ.Culture.Chair16.Quickstart");
                var prop = ready == null ? null : ready.GetProperty(
                    "IsPostLoaded", BindingFlags.Public | BindingFlags.Static);
                return prop != null && (bool)prop.GetValue(null, null);
            }, 90f);
            if (Environment.GetEnvironmentVariable("AMJ_BACKGROUND_JAPANESE") == "1")
            {
                await OnGameThread(delegate
                {
                    LoadedLanguage japaneseLanguage =
                        LanguageDatabase.AllLoadedLanguages.FirstOrDefault(x => x.folderName == "Japanese (日本語)");
                    ctx.Require(japaneseLanguage != null,
                        "Japanese is absent from the running game's loaded language metadata");
                    if (LanguageDatabase.activeLanguage != japaneseLanguage)
                    {
                        Log.Message("AMJ_CULTURE_BACKGROUND_JA_SWITCH previous="
                            + LanguageDatabase.activeLanguage + " target=Japanese (日本語)");
                        LanguageDatabase.SelectLanguage(japaneseLanguage);
                    }
                });
                await ctx.WaitUntil(() =>
                {
                    if (LanguageDatabase.activeLanguage == null ||
                        LanguageDatabase.activeLanguage.folderName != "Japanese (日本語)") return false;
                    var first = DefDatabase<BackstoryDef>.GetNamedSilentFail("AddictionCounsel60");
                    return first != null && LoadedText(first, "title") == "村の世話役";
                }, 90f);
            }
            await ctx.WaitFrames(2);
            await OnGameThread(delegate
            {
                ctx.Require(Adult.Length == 77 && Child.Count == 55 && ShuffledChild.Length == 3,
                    "Incorrect feature population");
                foreach (string id in Adult)
                {
                    BackstoryDef backstory = DefDatabase<BackstoryDef>.GetNamedSilentFail(id);
                    ctx.Require(backstory != null, "Missing adult Def: " + id);
                    ctx.Require(backstory.spawnCategories != null &&
                        backstory.spawnCategories.Count == 1 &&
                        backstory.spawnCategories[0] == "AMJ_CultureExcluded",
                        "Adult category not restricted: " + id);
                }
                foreach (string id in Child)
                {
                    BackstoryDef backstory = DefDatabase<BackstoryDef>.GetNamedSilentFail(id);
                    ctx.Require(backstory != null, "Saved child Def identity missing: " + id);
                }
                foreach (string id in ShuffledChild)
                {
                    BackstoryDef backstory = DefDatabase<BackstoryDef>.GetNamedSilentFail(id);
                    ctx.Require(backstory != null, "Missing shuffled child Def: " + id);
                    ctx.Require(backstory.spawnCategories != null &&
                        backstory.spawnCategories.Count == 1 &&
                        backstory.spawnCategories[0] == "AMJ_CultureExcluded",
                        "Shuffled child category not restricted: " + id);
                }
                ctx.Require(AdaptedTitles.Count == 89,
                    "Phase 2 accepted background mapping incomplete");
                bool japanese = Environment.GetEnvironmentVariable("AMJ_BACKGROUND_JAPANESE") == "1";
                if (japanese)
                {
                    string sourcePath = Environment.GetEnvironmentVariable("AMJ_BACKGROUND_JAPANESE_SOURCE");
                    ctx.Require(!string.IsNullOrEmpty(sourcePath) &&
                        System.IO.File.Exists(sourcePath), "Original approved Japanese XML unavailable");
                    var xml = new System.Xml.XmlDocument();
                    xml.Load(sourcePath);
                    ctx.Require(xml.DocumentElement != null &&
                        xml.DocumentElement.Name == "LanguageData", "Invalid Japanese DefInjected XML");
                    var sourceEntries = new Dictionary<string, Dictionary<string, string>>();
                    foreach (System.Xml.XmlNode node in xml.DocumentElement.ChildNodes)
                    {
                        if (node.NodeType != System.Xml.XmlNodeType.Element) continue;
                        int dot = node.Name.LastIndexOf('.');
                        ctx.Require(dot > 0, "Malformed Japanese translation entry: " + node.Name);
                        string id = node.Name.Substring(0, dot);
                        string field = node.Name.Substring(dot + 1);
                        if (!sourceEntries.TryGetValue(id, out var fields))
                            sourceEntries[id] = fields = new Dictionary<string, string>();
                        ctx.Require(!fields.ContainsKey(field), "Duplicate localization field: " + node.Name);
                        fields.Add(field, node.InnerText);
                    }
                    ctx.Require(sourceEntries.Count == 89, "Japanese source did not contain 89 Defs");
                    foreach (string id in AdaptedTitles.Keys)
                    {
                        ctx.Require(sourceEntries.TryGetValue(id, out var fields) &&
                            fields.Count == 4, "Japanese localized fields missing for " + id);
                        BackstoryDef bio = DefDatabase<BackstoryDef>.GetNamedSilentFail(id);
                        ctx.Require(bio != null, "Approved Japanese background not loaded: " + id);
                        string loadedTitle = LoadedText(bio, "title");
                        object activeLanguage = typeof(LanguageDatabase).GetField(
                            "activeLanguage", BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Static)
                            ?.GetValue(null);
                        ctx.Require(loadedTitle == fields["title"],
                            "Japanese title mismatch: " + id + " actual=" + loadedTitle
                            + " expected=" + fields["title"]
                            + " activeLanguage=" + (activeLanguage == null ? "(unknown)" : activeLanguage.ToString()));
                        ctx.Require(LoadedText(bio, "titleShort") == fields["titleShort"],
                            "Japanese short title mismatch: " + id);
                        string body = LoadedText(bio, "description");
                        ctx.Require(body == fields["description"],
                            "Japanese full description mismatch: " + id);
                        ctx.Require(fields["baseDesc"] == fields["description"],
                            "Canonical Japanese base description drift: " + id);
                    }
                    Log.Message("AMJ_CULTURE_BACKGROUNDS16_JAPANESE_OK count=89");
                }
                else
                {
                    foreach (KeyValuePair<string, string> expected in AdaptedTitles)
                    {
                        BackstoryDef biography =
                            DefDatabase<BackstoryDef>.GetNamedSilentFail(expected.Key);
                        ctx.Require(biography != null, "Approved background Def absent: " + expected.Key);
                        ctx.Require(LoadedText(biography, "title") == expected.Value,
                            "Approved English title not loaded: " + expected.Key);
                        string body = LoadedText(biography, "description");
                        ctx.Require(!string.IsNullOrWhiteSpace(body),
                            "Approved background description missing: " + expected.Key);
                    }
                }
                foreach (string reopened in new[] { "AddictionCounsel60", "Architect28" })
                {
                    BackstoryDef biography = DefDatabase<BackstoryDef>.GetNamedSilentFail(reopened);
                    ctx.Require(biography != null && biography.spawnCategories != null &&
                        biography.spawnCategories.Count > 0 &&
                        !biography.spawnCategories.Contains("AMJ_CultureExcluded"),
                        "Reopened original biography is still restricted: " + reopened);
                }
                Log.Message("AMJ_CULTURE_BACKGROUNDS16_TEXTS_OK count=" + AdaptedTitles.Count);

                TraitDef trait = DefDatabase<TraitDef>.GetNamedSilentFail("Transhumanist");
                FieldInfo commonality = typeof(TraitDef).GetField("commonality",
                    BindingFlags.Instance | BindingFlags.NonPublic);
                ctx.Require(trait != null && commonality != null &&
                    (float)commonality.GetValue(trait) == 0f,
                    "Loaded random Transhumanist commonality not suppressed");
                ctx.Require(DefDatabase<TraitDef>.GetNamedSilentFail("BodyPurist") != null,
                    "BodyPurist Def identity was lost");
                Log.Message("AMJ_CULTURE_BACKGROUNDS16_LOADED_OK adult=" + Adult.Length
                    + " child=" + Child.Count);

                Assembly culture = AppDomain.CurrentDomain.GetAssemblies()
                    .FirstOrDefault(x => x.GetName().Name == "AMJ.Culture");
                ctx.Require(culture != null, "Compiled Culture DLL not loaded");
                Type gate = culture.GetType("AMJ.Culture.SolidChildBioGatePatch");
                ctx.Require(gate != null, "Solid child production gate missing");
                MethodInfo method = gate.GetMethod("Postfix",
                    BindingFlags.Public | BindingFlags.Static);
                ctx.Require(method != null, "Solid child gate handler missing");
                MethodInfo target = typeof(PawnBioAndNameGenerator).GetMethod(
                    "TryGetRandomUnusedSolidBioFor",
                    BindingFlags.NonPublic | BindingFlags.Static);
                ctx.Require(target != null, "Vanilla solid selection target missing");

                Assembly harmony = AppDomain.CurrentDomain.GetAssemblies()
                    .FirstOrDefault(x => x.GetName().Name == "0Harmony");
                ctx.Require(harmony != null, "Harmony assembly missing");
                Type harmonyType = harmony.GetType("HarmonyLib.Harmony");
                MethodInfo patchInfoMethod = harmonyType == null ? null :
                    harmonyType.GetMethod("GetPatchInfo",
                        BindingFlags.Public | BindingFlags.Static);
                ctx.Require(patchInfoMethod != null, "Harmony patch introspection unavailable");
                object info = patchInfoMethod.Invoke(null, new object[] { target });
                ctx.Require(info != null, "Vanilla solid selection method is not patched");
                object postfixes = info.GetType().GetProperty("Postfixes")?.GetValue(info, null);
                if (postfixes == null)
                    postfixes = info.GetType().GetField("Postfixes")?.GetValue(info);
                ctx.Require(postfixes is IEnumerable, "Harmony postfix list not available");
                bool owned = false;
                foreach (object patch in (IEnumerable)postfixes)
                {
                    object owner = patch.GetType().GetField("owner")?.GetValue(patch);
                    if (owner == null)
                        owner = patch.GetType().GetProperty("owner")?.GetValue(patch, null);
                    if ((owner as string) == "sucro.ancientmedievaljapan.culture.mealseat")
                        owned = true;
                }
                ctx.Require(owned, "Compiled Culture postfix was not registered");

                PawnBio blocked = SolidBioDatabase.allBios
                    .FirstOrDefault(x => x.childhood != null && Child.Contains(x.childhood.defName));
                PawnBio allowed = SolidBioDatabase.allBios
                    .FirstOrDefault(x => x.childhood != null && !Child.Contains(x.childhood.defName));
                ctx.Require(blocked != null && allowed != null,
                    "SolidBioDatabase lacks a suitable real blocked/allowed sample");
                object[] rejected = { true, blocked };
                method.Invoke(null, rejected);
                ctx.Require(!(bool)rejected[0] && rejected[1] == null,
                    "Blocked fixed child biography was not rejected");
                object[] retained = { true, allowed };
                method.Invoke(null, retained);
                ctx.Require((bool)retained[0] && ReferenceEquals(retained[1], allowed),
                    "Non-blocklisted fixed biography changed");
                Log.Message("AMJ_CULTURE_BACKGROUNDS16_SOLID_OK blocked="
                    + blocked.childhood.defName + " allowed=" + allowed.childhood.defName);

                PawnKindDef colonist = DefDatabase<PawnKindDef>.GetNamedSilentFail("Colonist");
                ctx.Require(colonist != null, "Vanilla Colonist missing");
                Pawn pawn = PawnGenerator.GeneratePawn(colonist, Faction.OfPlayer);
                ctx.Require(pawn != null && pawn.story != null && pawn.story.Childhood != null,
                    "Ordinary player pawn generation failed");
                ctx.Require(!Child.Contains(pawn.story.Childhood.defName),
                    "Generated child biography is forbidden");
                ctx.Require(pawn.story.Adulthood == null ||
                    !Adult.Contains(pawn.story.Adulthood.defName),
                    "Generated adult biography is forbidden");
                Log.Message("AMJ_CULTURE_BACKGROUNDS16_PAWN_OK");
            });
        }
    }
}
