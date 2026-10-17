# C-06 — 中世日本の敷寝具・収納生活（AMJ Culture）

**Owner:** `sucRo-RimWorld/Ancient-Medieval-Japan-Culture`. **Author decisions:** 2026-10-18. **Status:** 望ましいプレイヤー体験と既存Modとの責務は合意済み。**実際の1.6 Vanilla F-1〜F-4（寝所割当・就寝・本物の撤去Job・同位置への再設置）はPoC PASS。F-5の独立セーブ・ロードは実行待ち、F-6の部屋評価・囚人/医療用の完全な非干渉は未受け入れ。従ってPoC総合は未完了。** 新ThingDef・C#・製品DLLは未実装。Cultureは単一Modで、C-06を独立した実装単位として扱う。

## Intended gameplay (accepted intent)

- 古代〜中世日本の庶民が、固定のベッドではなく、床に寝具を**敷き、起床後に片付ける**生活を表現する。近現代の綿入り敷布団・折り畳みちゃぶ台を標準品として遡及しない。
- プレイヤーは専用の寝所を配置して通常の就寝場所・所有者を指定できる。寝具を畳んでも**寝所の位置と所有者を失わない**。次の睡眠時に同じ場所に敷ける。
- 寝具の展開中は通常の睡眠設備として就寝・休息・快適度が働く。起床後に収納されたら元の**1×2マスの家具占有が物理的に消え**、室内の床を食事・作業・通行など別目的に利用できる。画像の非表示だけでは不可。
- 寝所ごとに「自動で敷く・片付ける」と「敷いたまま（万年床）」を切り替える。全員一括の昼夜タイマーではなく、各人の休息・就寝/起床のタイミングに反応する。夜勤や傷病時にも破綻させない。
- 畳む/敷く動作には軽い労働時間を必要とする。これは狭い住宅で空間を活用する利益とのトレードオフであり、**寝具の存在だけを理由とする一律Moodバフは設けない**。

## Materials and historical/economy boundary (accepted intent)

- 最初の対象は「敷草の寝床／簡素な筵・薦系の敷寝具」を想定。呼称は製品画像・構造を見て確定し、敷草と藁・編んだ筵を同じ製法だと断定しない。
- **Vanilla:** 既存の `Hay` だけで製作可能。**MO併用:** `Hay` **または** `DankPyon_Straw` のどちらか一方。二つ同時の材料要求はしない。
- MOのStrawは既存資源でありVanillaに追加しない。草（Hay）を乾燥棚で穀物藁（Straw）に変換させない。野草→Hayの直接採取とGrains/MOの穀物脱穀→Strawの所有契約は別物のまま維持。
- 素材が違っても同じ寝具機能・休息/Comfort・収納方式を使う。材料別の数量・素材保存・回収損失・屋外耐久・完成品の数値は**バランス確定前**。建築メニュー候補はHay版の常設Def＋MOが存在するときのみStraw版Def、共有Job/Comp。二つの `costList` を単一のDefへ並べてAND要求させない。実装方式の最終確定はPoC後。
- Vanillaの無料 `Sleeping Spot`よりも睡眠上の利益があり、一般の木製ベッドよりも控えめな寝具を想定。現行MOのStrawBed（木製枠・製材研究あり）を改変、無効化、同じ物扱いしない。

## State, controls and recovery contract

| Condition | Expected behavior |
| --- | --- |
| 寝所を配置・寝具を製作 | 個人用寝所と実体としての寝具が対応し、オーナーを割り当て可能 |
| 就寝要求（自動収納モード） | 寝所の1×2セルが使用可能なら、近くの収納場所から割当済みの実体寝具を取り出し、敷設して寝る。材料を無から補充しない |
| 起床・不要になった場合 | 近くの有効な寝具対応ストレージを確保してからJobで畳み、収納物をそこへ運搬する。寝具はインベントリに常駐せず、元の2セルが空き床になる |
| 万年床モード | 寝具を自動的には片付けない。設定はセーブ後にも維持 |
| 指定場所が使用中、予約中、塞がっている | 他人の家具やJobを強制消去しない。安全に延期または通常就寝へのフォールバック。無限設置/撤去・食事/睡眠Jobのスパムなし |
| 寝具が紛失・入手不可 | 寝所/所有者の記録は保全し、寝具を無から増殖させない。プレイヤーに検知できる失敗で復帰可能 |
| ポーン不在・離脱、セーブ/ロード | 孤児データや不正参照を残さず、元の寝具アイテムの所在・寝所・所有者を可能な限り維持 |
| 囚人・医療用Sleeping Spot、緊急治療 | **Vanillaのまま**。新規寝具の勝手な収納処理を掛けない |
| Shared Sleeping／Culture Greedy・Jealous | 共同寝所評価と寝室割当が収納を理由に無条件に失われない。既存の独立Mod/既存Thoughtの値を一律には変更しない |

## Storage destination and hauling policy (2026-10-18 author direction)

**New confirmed gameplay requirement:** A folded bed is **stored at a nearby player-allowed stockpile/storage location**, rather than residing in the pawn's ordinary inventory between sleep periods. The existing disposable PoC moves a minified bed into pawn inventory solely for the controlled installation Job. That behavior is **not** the intended production storage loop. A carrier may temporarily hold the object while actively hauling or installing it, but must release it at completion/interruption; do not permanently reduce the pawn's carrying capacity.

1. **Default route without a new furniture dependency:** fold/uninstall a bed using a real Job, then haul the *same* MinifiedThing to a **nearby reachable stockpile** that accepts this bedding item. Where an existing supported shelf or storage building **actually accepts and holds** that item, it may also be used. Respect stockpile filters, reserves, forbidden settings and material/bed owner identity. Whether Vanilla/MO storage filters actually accept MinifiedThing beds must be verified in code/loaded Defs; don't claim all shelves work as-is.
2. **Location preference:** prefer an optional player-linked bedding store for that sleeping place; otherwise pick the nearest reachable, permitted bedding-accepting storage destination with adequate free capacity, subject to Vanilla storage rules and competing item reservations. Avoid searching every storage cell, bed and pawn every Tick. Keep sleep-site → exact bed item identity even if another hauling Job or a user moves the item between storage locations. The stored bed remains a real item, not an abstract infinite charge.
3. **Restore for rest:** the assigned pawn (or valid helper during hauling) takes that exact minified bed from storage for a short active installation job, installs it at the original stable 1×2 sleeping site, then sleeps there; no permanent carried bed or teleportation. Do not steal another pawn's sleeping item. Use a safe fallback if access/path, room use or reservations obstruct retrieval or placement.
4. **No storage / full storage:** check for an acceptable storage destination **before** scheduling automatic folding. When none exists, leave the bed **laid out** (like 万年床) and do not burden the pawn with a packed bed or start an install/fold loop. If storage becomes unavailable midway, release the item at a valid nonblocking fallback cell and retry only on a bounded future event; do not lose/duplicate it or force-erase another structure. If the bed has already been uninstalled and no destination remains, require a recoverable visible object/state rather than silent loss.
5. **Saving and multipawn integrity:** retain owner, site, fold/keep mode, stored item reference, location (or storage-association lookup) through game save/load, forbidden/priority changes, emptying a container, pawn absence and map transitions. A large colony of night/day shift workers must not generate competing haul jobs or global per-tick scanning.
6. **Storage performance and advantage:** the storage has its own occupied footprint and should typically be compact enough that folding several 1×2 beds into one store **actually saves floor space**. Do not add a free room-impressiveness or Cramped mood bonus merely because a chest exists. Final furniture footprint, capacity, work time, costs, bed count and player UI are **balance/implementation decisions**, not approved numbers.

### Historically appropriate optional storage object (not yet implemented)

The author would like something analogous to an **押し入れ** for convenience, but the standard built-in two-tier *oshiire* storage closet became common in the **Edo period**, outside the AMJ ancient–medieval scope. See [日本大百科全書／世界大百科事典「押入れ」](https://kotobank.jp/word/%E6%8A%BC%E5%85%A5%E3%82%8C-40233). For an era-appropriate dedicated bedding store, **櫃（ひつ）／仮称「寝具櫃」** is the leading small-storage furniture candidate; **納戸** is a documented medieval room for storing household goods (not an automatically built-in closet). Historical references: [「納戸」世界大百科事典](https://kotobank.jp/word/%E7%B4%8D%E6%88%B8-590642), [OMOYA LAB「近世庶民の住まいと調度」](https://www.shc.usp.ac.jp/omoya/lab/activities/articles/household-edo.html). Do not casually use the modern fitted closet or Edo-era large `長持` as the default medieval commoner's solution. The exact period/label/visual and construction requirements for a new **bedding-only** chest are pending content audit and author approval; this narrowly associated item could belong to Culture without absorbing unrelated general furniture.

**Implementation/acceptance gate separate from earlier native PoC:** Earlier F1–F4 PASS showed a real minified item, physical floor clearance and controlled reinstallation, **not** delivery to an actual stockpile or chest. Before product storage is accepted, check one real fold → nearby stockpile/container haul → retrieve → install → sleep roundtrip, full storage fallback, owner identity, and save/load if storage location changed. Fold-and-stow has its own behavioral evidence and must not inherit the standalone F1–F4 PASS. Do not start a new unrelated test matrix merely to record this decision.



## Mandatory feasibility test — BEFORE committing to the production installation architecture

**Specific author requirement (2026-10-18):** The existing [Use Bedrolls - Continued](https://github.com/MemeGoddess/RimWorld-UseBedrolls) proves only portable-bed job patterns, *not* this Mod's persistent indoor sleeping site and true room reuse. A **single targeted disposable PoC in original RimWorld 1.6 Vanilla** is required before asserting that the intended mechanic can be delivered. Use existing approved isolated native host if possible; no automatic PR/push game launches, broad regression matrix or permanently installed test fixture for this question. Original-game runtime evidence and exact source/asset identity must be retained.

| Proof step | Required observation |
| --- | --- |
| F-1 Assign | Place one temporary 1×2 sleeping-site marker, assign an ordinary colonist, create/associate a disposable bedding Thing |
| F-2 Deploy / rest | Colonist or controlled natural sleep Job deploys the actual bed and **uses that bed**; owner is still associated with the site |
| F-3 Fold / free floor | After the pawn leaves bed, the physical 1×2 bed is packed/removed by a genuine Job; the two cells cease to be building-occupied, not merely invisible |
| F-4 Round trip | After an intervening daytime activity, the bed can be deployed again at the registered site, with the **same owner and location**. No spurious reassign/rebuild, minified-bed duplication or infinite job cycling |
| F-5 Save/reload | In the folded state, save and load with Culture PoC active; the sleeping site, owner, bedding inventory and auto-fold mode survive. Repeat deployment and sleep once |
| F-6 Stat / safety | Record `Room.Space` in both states, `Cramped interior` separately, plus log/errors and whether Vanilla prisoner/medical Sleeping Spots are untouched; failed availability of bedding or obstruction must not crash or spin |

**Feasibility PASS:** all F-1..F-6 observed on actual isolated 1.6 Vanilla loaded game, clean relevant Unity/game logs and no serious job-loop/stolen-bed state; include exactly tested source and limited coverage. A code walkthrough, XML parsing or another Mod's claims do **not** count. **Feasibility FAIL:** actual loss of owned sleep site, inability to re-deploy, fake free space, recurring install/pack loops, bedding duplication/loss or save corruption. If failed, evaluate alternative site/ownership/packaging architecture and run only the affected narrow retry. Do not label C-06 implemented based on PoC alone.

**Version scope:** This first PoC checks only the core architecture in original 1.6 Vanilla, not MO or RimWorld 1.7. After a valid 1.6 design and production feature, use the smallest relevant 1.6+MO / 1.7 production validation to check optional Straw and version-specific APIs. Do not claim 1.7 support solely from 1.6 or previous Culture DLLs. Full TPS cost is considered after the actual event-driven product system exists; no all-pawns-all-rooms-per-tick traversal.

## Verified real-game PoC evidence — 2026-10-18 (partial, not full approval)

**F1–F4 partial native PASS:** The first genuine isolated RimWorld 1.6.4871 rev598 Vanilla+Culture+test scenario [Project #39052398389](https://github.com/sucRo-RimWorld/Ancient-Medieval-Japan-Project/actions/runs/39052398389) was **FAIL**: actual sleep and Vanilla `JobDefOf.Uninstall` created the folded MinifiedThing, but F4 could not transfer a still-spawned minified item into the pawn inventory. Source [Culture #109](https://github.com/sucRo-RimWorld/Ancient-Medieval-Japan-Culture/pull/109) correctly despawned it **without destroying it** before passing the same instance to `ThingOwner.TryAdd`. Focused 1.6 native retry [Project #39052567400](https://github.com/sucRo-RimWorld/Ancient-Medieval-Japan-Project/actions/runs/39052567400) is **SUCCESS**, 1 Pickle scenario PASS, errors0, exit0, original game unchanged. Tested Culture source `43367d8fc4b9ecbebebd2ba98bae5f7d2aa8ad9d`, compiled 1.6 product DLL SHA256 `527629e8353ec878940c99b1ddb869f3d4a8747ec436107c466efc12355083ba` (PoC classes built into disposable test-only assembly). **This proves the physical round-trip with controlled jobs, not normal autonomous day/night job selection.** It does not prove full material alternatives, day-use furniture conflicts, multiple pawns, or final performance.

**F5 actual serialization (pending, not a claim of success):** Culture [PR #110](https://github.com/sucRo-RimWorld/Ancient-Medieval-Japan-Culture/pull/110), main `2b793f12e385cfc544fe9cda9ef926050851d629`, added test-only production-independent `GameComponent` serialization and a second original-game-process load that should verify owner/site/folded item, reinstallation and a new real LayDown. Project [PR #687](https://github.com/sucRo-RimWorld/Ancient-Medieval-Japan-Project/pull/687) added the independent save/reload runner, and [PR #689](https://github.com/sucRo-RimWorld/Ancient-Medieval-Japan-Project/pull/689) removed an overbroad workflow-global lock after unrelated issue-comment events were found queueing. The explicitly invoked [Project run #39052995070](https://github.com/sucRo-RimWorld/Ancient-Medieval-Japan-Project/actions/runs/39052995070) is **queued, no result verified as of this entry**. Do not claim save/reload PASS from file existence or the earlier successful F1-F4 run, and do not launch duplicates when the originally queued run is still pending.

**F6 remaining:** The 1.6 scenario logs the original `Room.Space` stat before/after native Uninstall and verifies 1×2 furniture cells are physically empty; its expected magnitude in an enclosed multiuse room and `Cramped interior` distinction are **not** promoted to measured final game balance. Prison/medical Sleeping Spots and no-job-loop interactions are out of the standalone initial controlled scenario beyond no affected product patches; no full regressions claimed. Product design (Hay or optional MO Straw, auto/万年床 toggle, durable autonomous scheduling, UI and saved-owner policy) remains to be chosen/implemented **after** the feasibility gate.

## Subsequent product slices (pending feasible PoC)

1. **C-06a functional product:** audited Defs/Jobs/serialization, Hay/optional MO Straw build alternatives, individual sleeping-site assignment, auto vs perpetual toggle, emergency fallback and ownership-safety. Single managed Culture production feature with clear C# / XML boundaries.
2. **C-06b presentation and balance:** approved item names, Japanese and English text, era-fitting 256×256 texture, final material quantities/Comfort/rest and storage time. No pre-approval of placeholder art.
3. **C-06c acceptance:** minimal real game checks of the exact compiled 1.6/1.7 product (Vanilla, supported optional MO where material reference exists), owner persistence and save/load, no conflict with prison/medical sleeping spots, Shared Sleeping and Greedy/Jealous. Check Room.Space differences without assuming any global cramped-mood fix.

## Stage 2 — compact-room cultural judgment

**Separate later feature C-07.** `Room.Space` improves when physical walk-through bedding is removed, but pawn `Cramped interior` / `Confined interior` use another accessible-cell check. If C-06 passes and actual small multipurpose rooms still feel unfairly cramped, a **conditional Culture Thought adjustment** for the local user of a truly reused room may be designed. The illustrative `-5→-2` is **not approved**; no global alteration to the original ThoughtDef stages. Keep `Confined interior` -10, unrelated buildings, prisoners, patients and conventional permanent beds unchanged. Do not implement this before physical bedding.

## Prior art and exclusions

- [Use Bedrolls - Continued](https://github.com/MemeGoddess/RimWorld-UseBedrolls), source `InterceptRest.cs`, `JobGiver_TakeBedBack.cs`, `JobDriver_PlaceBed.cs`, `PlacedBedsMapComponent.cs`: minified bed deploy/reclaim and serialized pawn:bed state. Site-stable day-room reuse **not verified**. No library/data dependency or direct source copying proposed.
- [Erin's Japanese Furniture](https://steamcommunity.com/sharedfiles/filedetails/?id=2354938860): Japanese Futon content; full original Def and era/dep audit incomplete. Vanilla Furniture Expanded is VE prior-art only, not target dependency or official compatibility. MO's `DankPyon_StrawBed` is not an unframed early Hay bed.
- The additional VE/non-VE original content/Defs and 1.7 API audit required by Project's [ExistingModAudit](https://github.com/sucRo-RimWorld/Ancient-Medieval-Japan-Project/blob/main/Docs/Research/ExistingModAudit.md) remains OPEN for the **production implementation decision**. Disposable feasibility research alone is not a production approval.
- No new permanent lodging rules or general furniture system; no modification of the separate Shared Sleeping owner; no extra Straw ThingDef, reed-based conversion, mandatory MO or broad cross-Mod patch.
