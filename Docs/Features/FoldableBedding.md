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
| 就寝要求（自動収納モード） | 寝所の1×2セルが設置可能で在庫に寝具がある場合、ポーンが敷設してその寝所で休む |
| 起床・不要になった場合 | 同じ寝具を適切なJobで畳み、収納物へ戻す。元の2セルが空き床になる |
| 万年床モード | 寝具を自動的には片付けない。設定はセーブ後にも維持 |
| 指定場所が使用中、予約中、塞がっている | 他人の家具やJobを強制消去しない。安全に延期または通常就寝へのフォールバック。無限設置/撤去・食事/睡眠Jobのスパムなし |
| 寝具が紛失・入手不可 | 寝所/所有者の記録は保全し、寝具を無から増殖させない。プレイヤーに検知できる失敗で復帰可能 |
| ポーン不在・離脱、セーブ/ロード | 孤児データや不正参照を残さず、元の寝具アイテムの所在・寝所・所有者を可能な限り維持 |
| 囚人・医療用Sleeping Spot、緊急治療 | **Vanillaのまま**。新規寝具の勝手な収納処理を掛けない |
| Shared Sleeping／Culture Greedy・Jealous | 共同寝所評価と寝室割当が収納を理由に無条件に失われない。既存の独立Mod/既存Thoughtの値を一律には変更しない |

**収納先の詳細はPoCの技術判定項目:** 畳んだ寝具が自分の持ち物か、通常備蓄/棚か、共有収納物かは現時点で固定しない。どの方式でも寝所位置・個人割当・実体アイテム（数量）の同一性・床の解放が必須。リザーブ競合や就寝Job衝突がある方式を採用しない。昼間に寝所セルへ別の**恒久家具**を設置した場合の完全自動片付けは約束しない。

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
