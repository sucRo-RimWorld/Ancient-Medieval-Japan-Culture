# C-06 — 多用途の筵・菰（AMJ Culture）

**Owner:** `sucRo-RimWorld/Ancient-Medieval-Japan-Culture`. **Current author preference (2026-10-18):** 同じ敷物を**夜は就寝用、昼は座具・食事場所**として使い、毎日の収納・運搬を不要にする。**Status:** 1.6 Vanillaの最小実ゲームPoCはPASS。専用の製品ThingDef／XML、画像、1.6／1.7製品DLLへの追加とMO対応は**未実装・未受入**。旧C-06自動収納案は[FoldableBedding.md](FoldableBedding.md)に保管するが、現在の製品実装の前提条件にはしない。

## Purpose and limits

- **中世日本の生活の抽象化:** 室内に敷く筵・菰を、睡眠専用家具でも食事専用家具でもなく、**昼夜で使う一つの生活道具**として表す。庶民が毎朝必ず敷物を片付けていた、あるいは全階層・全時代が同じ生活をしていた、と断定しない。
- **確認可能な歴史的根拠:** 『日本大百科全書』[寝具](https://kotobank.jp/word/%E5%AF%9D%E5%85%B7-81512)は、菰・筵を庶民の敷寝具として述べ、9世紀ごろの畳について**昼は円座を置いて座り夜は寝具に使った**例を記す。後者は直接「鎌倉・室町のすべての庶民が同一の筵で毎日食べて寝た」を証明するわけではない。実装は史実の忠実な生活動作再現ではなく、用途兼用の合理的な抽象化。
- **単一物体:** 1×2マス程度の**同じ建造物実体**が通常の寝具であり、日中は座って食事できる場所にもなる。朝晩の撤去・運搬・建築・設置を必要としない。物体を透明化したり別のイスを重ねて二重配置するのを製品必須構成にしない。
- **プレイヤーに見える利益:** 別々の寝台(1×2)と食事用座具(1×1)を設置しなくても**同じ敷物の上で眠り、食事ができる**。ポーンの朝夕の余分なJob/TPSコストもない。単なる休息効率・Comfortの微量差や、未確認のRoom.Space増加を主な売りにしない。
- **住居内に据え置くことを基本**とする。持ち運べる構造であっても**毎日の自動収納は実装の必須条件から除外**。専用櫃、押し入れ、個別の寝所位置MapComponent、収納専用Job、収納先の所有者同期は、この第一次多用途機能には追加しない。旧PoCを製品へ移植しない。

## Proposed player-visible behavior

| Scene | Expected effect / boundary |
| --- | --- |
| 建設・所持 | Culture独立で利用可能な1×2敷物。Vanilla `Hay`、MO導入時は既存`DankPyon_Straw`**または**Hayが使える（AND要求禁止）。制作コストは未確定 |
| 就寝 | 通常の`Building_Bed`として元の就寝Job、休息効率・Comfort、所有者割当を使用。毎晩の配置Jobは不要 |
| 昼の食事 | 通常の`Ingest`が同じ実体の上に座り、実際のComfort取得・食事完了に至る。専用の食事用椅子の増設を要求しない |
| 他にテーブル隣接の椅子がある | **既存Cultureのテーブル優先の着席規則を維持**。多用途の敷物を必ず最優先にすることはしない |
| 睡眠中の敷物 | その敷物の就寝中・予約中に別のポーンの食事が重ならないようにする。2セルに別Pawnが座る可能性と予約方式は製品実装の要検証事項 |
| 非所有者・共同用 | 寝所の所有者を食事で解除しない。日中の共有利用は空き状態かつ予約に抵触しない場合を候補とし、他人・囚人・患者の就寝権を侵さない |
| 時代的に違う施設 | 通常のVanilla/MOベッド、既存Sleeping Spot、医療/囚人寝床の`isSittable`等は**一律に変更しない**。専用Culture ThingDefのみに限定 |
| 部屋・心情・清潔 | `AteWithoutTable`の既存Culture 0処理、共同寝所/Greedy・Jealous、Shared Sleepingの責務を変更しない。部屋品質上昇、新しいMoodボーナス、自動洗浄、日中のレクリエーション自動追加は保証しない |

**形態の区別:** ばらばらの草・藁を床に敷くだけの**敷草**と、編んで使う**筵・菰**は別物。多用途は座って使える**編んだ敷物**を第一候補にする。草敷き寝床は追加する価値・製作容易性がMOの丸太ベッドと比較して未確定であり、この機能の実装条件とはしない。素材・休息効率/Comfortと腐敗/耐久の設定は[旧検討](FoldableBedding.md#rest-and-comfort-balance-boundary--wooden-beds-are-not-automatically-superior-2026-10-18-author-decision)を踏まえるが、新しい数値は確定しない。硬い木材にクッションがない以上、敷草/筵を丸太ベッドより必ず低Comfortと扱わない。

**MOとのコスト比較:** MOで丸太45のベッドを作りやすいという作者の実ゲーム指摘を重視する。Hayの採集歩数と収穫手間、MO Strawの脱穀による供給を評価し、**安い初期寝具**だけを訴求しない。専用座具＋寝台の**2用途を1品で代替**することを優位性とし、製作材料と作業時間を後で比較する。Hay→Strawの新しい乾燥レシピやMO必須依存は追加しない。

## Existing Culture and original-game technical path

- 正本となる既存製品ソース: [`Source/AMJ.Culture/TablelessChairMealPatch.cs`](../../Source/AMJ.Culture/TablelessChairMealPatch.cs)。1.6/1.7の既存Cultureはテーブルのない食事で、`building.isSittable`かつ通行可能な家具を、通常の`Toils_Ingest.CarryIngestibleToChewSpot`の座具候補として扱う。別の食事UI/Jobを新設する前にこのルートの再利用を選ぶ。
- **最小の本物の1.6ゲームPoC PASS:** [Project Actions #39057007685](https://github.com/sucRo-RimWorld/Ancient-Medieval-Japan-Project/actions/runs/39057007685)。原RimWorld **1.6.4871 rev598**、Culture source `1ea8a3768fffba9485709bc63891e263012b5056`、同時実装の1.6 DLL SHA256 `527629e8353ec878940c99b1ddb869f3d4a8747ec436107c466efc12355083ba`。隔離実行内でのみVanilla `Bedroll.building.isSittable=true`を設定。**その同じ実物/同じ場所/同じ所有者**で通常の`Ingest`・Comfort上昇・食事完了、続けて`LayDown`と実際の睡眠姿勢を確認。Pickle 1/1、error0、exit0、原ゲーム変更なし。
- **PoCと製品の区別:** このPASSは「1つの`Building_Bed`が食事座具にもなれる」ことの証明であって、**Culture専用寝具Defの追加・Hay/Straw製作・実際のビジュアル・製品実装の証明ではない**。実ゲームの複数ポーン・睡眠中の座席衝突、優先順位、共同用、元のThoughtと部屋評価、1.6+MO、1.7、旧セーブ・TPSは未検証。Vanilla Bedroll自体の製品定義は変更しない。
- **先行Mod/重複監査:** [Sitting Spot 1.6](https://steamcommunity.com/sharedfiles/filedetails/?id=3236014306)は「ベッド上に座れる透明な椅子」を提供するが、Comfortはなく別Thingの重ね配置方式。ユーザー指定の用途兼用の家具1個とは異なる。[Erin's Japanese Furniture](https://steamcommunity.com/sharedfiles/filedetails/?id=2354938860)のFuton/Zabuton、VE系家具と非VE系の実Def・就寝/着席処理は依然監査対象。独立Culture実装の前にProjectのVE/非VE重複判断を行う。必須外部Modを増やさない。

## Production and acceptance gates

1. **実装準備:** 用途兼用の1×2 Culture敷物1種類を最小単位とし、Vanillaのみでも材料・就寝・食事が成立。画像と呼称は追加検討。既存`TablelessChairMealPatch`を丸ごと別実装に置き換えない。必要なら専用Defへの限定と睡眠中の座席抑止を追加。
2. **段階的実装:** 製品ThingDef/C#で就寝と座具の両用を実装し、MO追加資材は任意のパッチだけ。現存するCulture C-03の円座は用途の違う独立候補であり、重複した円座を自動増設しない。
3. **最後の実ゲーム受入:** 一つの本物の敷物でプレイヤーの手動切替なしに食事・Comfort・就寝。睡眠中の他人の食事・寝所割当/共同寝所、テーブル隣接の椅子優先、囚人/医療用非干渉、実DefのVanilla材料およびMO任意材料、1.6/1.7バージョン、ゲームエラー。前の1.6 PoCをそのまま製品受入に転用しない。
4. **見送り:** 櫃・押し入れ・自動収納/運搬/再設置、`Cramped interior`のボーナス/軽減、日中の作業台兼用や娯楽設備兼用は**本段階に含まない**。その後、実際の遊びで明確な利益がある場合にだけ独立機能として再検討する。

