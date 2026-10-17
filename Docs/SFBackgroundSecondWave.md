# SF背景の第二次通常生成制限 — 2026-10-17

## 修正目的・範囲

前回の原文監査は技術関連50件と分類漏れ26件を扱ったが、これは784件の全背景から見つけたSF設定の全数ではない。特にRimWorld 1.6の [1回の実ゲーム受け入れ](https://github.com/sucRo-RimWorld/Ancient-Medieval-Japan-Project/actions/runs/39005480093) において「ブラックリスト外の通常固定人物」の対照として選んだ `FallenProdigy40` は、残存する古い英語原文に **glitterworld出身と異世界の学院** を記述している。従来のPASSは**既存の12件を正しく制限する**証拠であり、ブラックリストに無い背景の史実適合性を保証しない。これを具体的な欠落として是正する。

**二次対象24件:** 成人期12件は既存の `Patches/AnachronisticAdultBackstories.xml` の `spawnCategories=AMJ_CultureExcluded` に追加。固定人物（Solid）の幼少期12件は `Source/AMJ.Culture/SolidChildBioGatePatch.cs` の除外集合へ追加。従来の成人期31件・幼少期12件は一切解除しない。新たな通常生成除外は **成人期43件（既存31＋12）、幼少期24件（既存12＋12）、合計67件**。原Def・付与特性・所有物・職務能力・古いセーブ参照は残す。直接指定・外部Mod再代入には適用を保証しない。職業・技能の概念そのものを「不適切」と判定したわけではない。

### 幼少期追加12件

| defName | 日本史に移せない旧本文の根拠 |
|---|---|
| `FallenProdigy40` | 超技術世界の学校／奨学金。旧テストの非除外対照に使われたが史実適合例ではない |
| `ChildResearcher32` | 反物質の兵器研究を強いられた幼少期 |
| `HeadjackAddict4` | 頭部接続端子による計算機への長時間接続 |
| `WealthyStudent59` | 超技術世界の工学教育とプログラミング |
| `CorpBredStudent54` | 惑星規模の宇宙開発会社が育成した幹部候補 |
| `CorpStudent95` | 惑星間商社の企業学校 |
| `OffworldRecruit91` | 軌道降下兵を育成する星外の兵員施設 |
| `AwkwardNerd48` | ロボット工学とホログラフィー |
| `DataDecoder39` | 超技術世界の百科事典データ管理 |
| `Bookworm3` | 宇宙航行とメカノイド戦争の技術文献 |
| `FarmMechanic1` | 農業惑星の自動収穫機械 |
| `DiscardedYouth93` | 惑星間で廃棄物を集めるゴミ惑星と栄養ペースト設備 |

### 成人期追加12件

| defName | 日本史に移せない旧本文の根拠 |
|---|---|
| `CasketBuilder52` | 冷凍睡眠装置の製作と工業化世界 |
| `ChiefEngineer62` | 恒星間宇宙船の主任技師 |
| `ColonyEngineer7` | 仮想現実で技能を習得する宇宙企業の植民地技師 |
| `CorpResearcher93` | 巨大宇宙探査企業の研究者 |
| `ExecutiveOfficer5` | 恒星間貿易会社の幹部 |
| `BiosphereManager95` | ロボット労働に支えられた超技術世界の環境管理 |
| `ArtifactHunter48` | 惑星間旅行とメカノイド遺跡の探索 |
| `ShadowMarine63` | 軌道交通路と惑星潜入に習熟した軍人 |
| `GameTester77` | 核戦争後の惑星で現代型ゲームに没頭した人物 |
| `Adventurer19` | 複数惑星・超技術世界を巡る冒険者 |
| `Explorer49` | 惑星間調査記録の作成 |
| `ExplorerWriter10` | 惑星間旅行を題材にした探検記 |

## 原資料の識別・適用判定

- **ゲーム原Defメタデータ（対象24件の正本）:** Project の [RimWorld Core 1.6/1.7原XML監査](https://github.com/sucRo-RimWorld/Ancient-Medieval-Japan-Project/blob/main/Docs/Research/VanillaCoreCharacterDefsSourceAudit20261016.md) の `amj-core-character-defs-metadata-v2` 同一CSVを照合した。全24件の `defName`/スロット一致、成人期12件の既存 `spawnCategories` 非空、幼少期12件の `BackstoryDefs/Solid/Solid_Child.xml` 由来を確認。
- **本文根拠:** Ludeon の [旧Finnish翻訳原文英語コメント](https://github.com/Ludeon/RimWorld-Finnish/blob/master/Backstories/Backstories.xml)、および [ChineseSimplifiedのDefInjected英語原文コメント](https://github.com/Ludeon/RimWorld-ChineseSimplified/tree/master/Core/DefInjected/BackstoryDef) をID別に照合。24件中20件は英語本文が一致。残る `OffworldRecruit91`・`DataDecoder39`・`BiosphereManager95`・`ArtifactHunter48` は軽微な表現・語形・誤記修正等の差があるが、二資料とも**同じSF技術・惑星間の背景設定を保持**している。
- **限界:** 記録済み英語原文コメントは最新版1.6/1.7ゲーム本体の `baseDesc` とバイト同一とは確認できていない。これを「正規ゲーム現行本文を全文再監査済み」と呼ばない。元Defの個別削除・ソース改ざんも行わない。
- **改稿余地:** 探検家、軍人、調査員、料理人、学者、農民、工匠といった一般的な役割は今後日本史に合う本文・能力の検討対象として維持する。**原文がSFのままの場合の通常新規生成のみ停止**しており、歴史的職業の削除や全人物の生成禁止ではない。

## 受け入れ・公開の境界

既存 [Vanilla 1.6 #39005480093](https://github.com/sucRo-RimWorld/Ancient-Medieval-Japan-Project/actions/runs/39005480093) のPASSは一次43件までが対象。二次追加24件の loaded-Def / 実抽選は**未検証**。再ビルドが必要な幼少期のDLLは、元ゲーム1.6とHarmonyを使用したビルドで更新し、製品DLLを同じGitHub PRで格納する。実装途中で追加のRimWorld起動・PR自動ゲーム実行・広域サンプリングはしない。最終的な統合受け入れは他の未変更の既知PASSを使い回し、新たな機能に絞る。MO・RimWorld 1.7・旧セーブロード・任意の外部直接代入は未検証。
