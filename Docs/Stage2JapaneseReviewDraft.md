# Phase 2 既存背景・日本語改稿候補（全面改稿第2稿／作者一括確認待ち）

> **全件未承認。製品には適用しない。** ユーザー指示により、日本語文面のレビューは Phase 2 の文章以外の対応を終えた後に**一括**で行う。承認前の英訳・DefInjected/XML製品変更・Steam反映を禁止する。

## 用途と証拠

**この版の全面修正方針（2026-10-18、全件未承認）:** 旧稿は直訳風・架空の職名・技能を説明するための不自然な逸話が多く、作者からtitle・略称だけでなくdescriptionも全面的に再作成するよう指示があった。元89候補のうち `AddictionCounsel60`（中世日本の独立した依存症専門職として不適切）と `Architect28`（大工とは独立した近代的設計専門職と元の知的／非単純作業の組合せをそのまま転用できない）を生成制限に回し、**本資料は87件**とする。残りは**すべて**元の技能・禁止作業・強制特性の変更なしに、短く自然な題名・略称・本文へ再作成した。合成の職名を無理に作らない。架空の事故や理由をゲーム値の説明用に付け足さない。原のゲームプレイ効果や歴史的背景との整合を改めて確認し、**この87件もまだ作者の承認を得ていない**。


下表はVanilla既存BackstoryDefの**同じDef名・技能・WorkDisables・requiredWorkTags・forcedTraits・initial possessions・spawnCategories**を維持し、元のSF・現代的叙述のみを古代～中世日本へ置き換える日本語原稿案である。新規日本専用Defではない。初期所持品が存在する定義は改稿確定時に別途史実素材への置換/維持を照合する（現時点で変更しない）。

- 原定義に関する直接資料：過去のProject 原Core 1.6/1.7 metadata v2（[source audit](https://github.com/sucRo-RimWorld/Ancient-Medieval-Japan-Project/blob/main/Docs/Research/VanillaCoreCharacterDefsSourceAudit20261016.md)）で既存33件（成人30件・幼少期3件）の元スロット・生成カテゴリと技能を照合。追加54件についても同じ元Coreメタデータと旧公式英語原文コメントを個別照合した。元ファイルのメタデータは両版一致。これは通常生成での各Solid個体採用を個別立証するものではない。
- 物語内容に関する参考：[Ludeonの旧英語原文コメント](https://github.com/Ludeon/RimWorld-Finnish/blob/master/Backstories/Backstories.xml)。**現在配布されているCore 1.6/1.7本文との完全一致は未立証。**
- 既存33件と追加54件は現在137件の生成制限、34件の実装済み英日改稿と**重複しない**。
- 元Core両版で強制Traitを持つ原稿候補は13件。`MasterChef48`（`Gourmand=0`）、`Minister88`（`Industriousness=1`）、`Model99`（`Beauty=2`）、`CaveworldIlluminator95`（`Undergrounder=0`）、`AspiringPopIdol28`（`Beauty=2`）、`DesertRat47`（`Ascetic=0`）、`EnergeticPopIdol61`（`Beauty=2;Greedy=0`）、`HedgeFundManager54`（`Greedy=0`）、`MadScientist47`（`Psychopath=0`）、`MindwipedAssassin50`（`Psychopath=0`）、`Nerd92`（`AnnoyingVoice=0`）、`Orphan15`（`Kind=0`）、`PyroAssistant82`（`Pyromaniac=0`）。これらのゲーム効果は変更しない。
- 以下の本文は未承認の史実・ゲーム描写案であり、特定時代・地域の役職名や語感について最終作者レビュー後に調整する。薬物の具体的種類・商品・緊急対処の機構はここで確定しない。

## 日本語原稿案（再検討した33件・第一候補）

### 01. `BountyHunter41`

- **title案：** 盗賊狩り
- **titleShort案：** 盗賊狩り
- **description案：** [PAWN_nameDef]は各地で盗賊を追い、武芸で身を立ててきた。弓と刀には自信があるが、誰に頼まれた仕事だったのかは滅多に話さない。

### 02. `Castaway81`

- **title案：** 難破の生存者
- **titleShort案：** 難破者
- **description案：** [PAWN_nameDef]の乗った船は嵐で沈み、本人だけが浜に流れ着いた。助けが来るまで、食べられる草木を探し、風雨をしのいで生き延びた。

### 03. `CharityWorker36`

- **title案：** 施しの僧
- **titleShort案：** 施しの僧
- **description案：** [PAWN_nameDef]は寺で貧しい人や病人に食べ物を分け、相談に乗っていた。人と話し世話をすることには慣れているが、争い事をひどく嫌う。

### 04. `CorporateBuilder58`

- **title案：** 普請人足
- **titleShort案：** 人足
- **description案：** [PAWN_nameDef]は寺や館の普請場で働き、土を掘り、石や材木を運んできた。長年の仕事で建築には詳しくなったが、飾り物を作ることには興味がない。

### 05. `ExpertHandyman8`

- **title案：** 腕利きの職人
- **titleShort案：** 職人
- **description案：** [PAWN_nameDef]は家屋から農具まで、壊れた物ならたいてい直せた。あちこちの村に呼ばれ、見慣れない仕組みの道具でも工夫して修理してきた。

### 06. `FurnitureBuilder83`

- **title案：** 家具職人
- **titleShort案：** 家具職人
- **description案：** [PAWN_nameDef]は木を選び、棚や箱などの家具を作って暮らした。丈夫で使いやすい品を作る腕を買われ、遠方からも注文が来た。

### 07. `GangSoldier39`

- **title案：** 古参の野盗
- **titleShort案：** 野盗
- **description案：** [PAWN_nameDef]は長く野盗の一味に加わり、襲撃や野営に慣れきっている。武器を扱うほか、仲間の傷の手当てや食事の支度も覚えた。

### 08. `LanguageAnalyst27`

- **title案：** 書記
- **titleShort案：** 書記
- **description案：** [PAWN_nameDef]は書状を書き写し、難しい文面を読み解く仕事をしていた。諸国の言葉や古い文書にも詳しく、暇があれば草木を育てていた。

### 09. `MasterChef48`

- **title案：** 館の料理人
- **titleShort案：** 料理人
- **description案：** [PAWN_nameDef]は館の台所を任されるほどの腕を持つ料理人だった。ある宴で料理に毒が混じり、疑いを掛けられて職を追われた。

### 10. `MedicSoldier54`

- **title案：** 陣中の医者
- **titleShort案：** 医者
- **description案：** [PAWN_nameDef]は戦場で負傷者を手当てしていた。刀傷や矢傷の治療には慣れているが、自ら武器を持って戦うことはない。

### 11. `MilitaryOfficer17`

- **title案：** 武士の大将
- **titleShort案：** 大将
- **description案：** [PAWN_nameDef]は武士たちを率いて戦に出た。陣立てや武芸を心得ているが、部下には厳しく、負傷者の世話をするような性分ではない。

### 12. `Minister88`

- **title案：** 奉行
- **titleShort案：** 奉行
- **description案：** [PAWN_nameDef]は領主に仕え、年貢や訴訟の取りまとめを担った。帳簿にも人の扱いにも慣れ、主人から重く用いられていた。

### 13. `Model99`

- **title案：** 舞人
- **titleShort案：** 舞人
- **description案：** [PAWN_nameDef]は歌舞の宴に招かれ、容姿と芸で人目を引いていた。身の回りの雑用は付き人に任せ、芸を磨くことに暮らしの大半を費やした。

### 14. `NinjaAssassin31`

- **title案：** 刺客
- **titleShort案：** 刺客
- **description案：** [PAWN_nameDef]は人目を避けて標的に近づき、密かに討つ仕事をしていた。武芸だけでなく、変装や人から話を聞き出す術にも通じている。

### 15. `Novelist7`

- **title案：** 物語の作者
- **titleShort案：** 作者
- **description案：** [PAWN_nameDef]は物語を書いて暮らしていた。人前に出ることを好まず、筆を執っている間は食事も忘れてしまうことがあった。

### 16. `PirateTrooper73`

- **title案：** 海賊の手下
- **titleShort案：** 海賊
- **description案：** [PAWN_nameDef]は海賊の一味で敵船に乗り込む役を務めた。弓も刀も扱えるが、捕らえた相手への情けは持ち合わせていない。

### 17. `PlanetaryDiplomat7`

- **title案：** 使者
- **titleShort案：** 使者
- **description案：** [PAWN_nameDef]は主君の書状を携え、各地の有力者との交渉に赴いた。言葉で争いを収めることを得意とし、武力に訴えるのを避けていた。

### 18. `PrisonerOfWar2`

- **title案：** 戦の捕虜
- **titleShort案：** 捕虜
- **description案：** [PAWN_nameDef]は戦で捕らえられ、長く敵方に囚われていた。隙を突いて脱走し、ほかの捕虜たちも連れ出したという。

### 19. `ReligiousHierarch16`

- **title案：** 寺の住職
- **titleShort案：** 住職
- **description案：** [PAWN_nameDef]は寺を預かり、説法をし、寺の暮らしを取り仕切っていた。檀越や参詣人との付き合いは多いが、力仕事には縁がない。

### 20. `RunawayDancer35`

- **title案：** 旅芸人
- **titleShort案：** 旅芸人
- **description案：** [PAWN_nameDef]は一座を離れ、歌や舞を披露しながら諸国を渡り歩いてきた。口が達者で、危ない道中を切り抜ける度胸もある。

### 21. `Sculptor63`

- **title案：** 仏師
- **titleShort案：** 仏師
- **description案：** [PAWN_nameDef]は仏像を彫ることに打ち込んできた。石の扱いにも慣れ、寺から大きな石仏の仕事を請け負ったことがある。

### 22. `SoleSurvivor85`

- **title案：** 戦乱の生き残り
- **titleShort案：** 生存者
- **description案：** [PAWN_nameDef]の故郷は戦に巻き込まれ、家族も家も失われた。それ以来、他人に心を開くことが少なくなった。

### 23. `StationSecurity38`

- **title案：** 関所の番人
- **titleShort案：** 番人
- **description案：** [PAWN_nameDef]は関所で人と荷を調べ、通行を見張っていた。荒事にも慣れているが、かつて火事でひどい目に遭い、火消しだけは避けている。

### 24. `ChildStar74`

- **title案：** 芸人の子
- **titleShort案：** 芸人の子
- **description案：** [PAWN_nameDef]は幼い頃から一座に連れられ、人前で歌や舞を披露していた。大人との受け答えには慣れているが、学問を習う機会は少なかった。

### 25. `CrashBaby32`

- **title案：** 難破船の子
- **titleShort案：** 難破の子
- **description案：** [PAWN_nameDef]は赤子の頃に船が難破し、浜辺で助けられた。親のことは知らず、拾われた土地で育った。

### 26. `UrbworldUrchin61`

- **title案：** 町の孤児
- **titleShort案：** 孤児
- **description案：** [PAWN_nameDef]は町の裏通りで育った。食べ物を巡って喧嘩になることも多く、幼いうちから身を守る術を覚えた。

### 27. `UrbworldDrone32`

- **title案：** 苦役の人足
- **titleShort案：** 人足
- **description案：** [PAWN_nameDef]は長く普請場で働くことを強いられていた。穴を掘り、石を運び、建物を建てる仕事は覚えたが、自由に学ぶ機会はなかった。

### 28. `UrbworldEntrepreneur14`

- **title案：** 商人
- **titleShort案：** 商人
- **description案：** [PAWN_nameDef]は市で小さな商いを始め、やがて多くの品を扱うようになった。相場に明るく、取引相手との駆け引きにも長けている。

### 29. `ConstructionEngineer32`

- **title案：** 大工棟梁
- **titleShort案：** 棟梁
- **description案：** [PAWN_nameDef]は大工たちを率い、寺や屋敷の普請を手掛けた。材木の寸法から組み立てまで頭に入っているが、炊事は人任せだ。

### 30. `MafiaBoss17`

- **title案：** ならず者の親分
- **titleShort案：** 親分
- **description案：** [PAWN_nameDef]は町のならず者を束ね、商人から金を取り立てていた。脅しと取引で勢力を広げ、裏切る者には容赦しなかった。

### 31. `ContractMiner86`

- **title案：** 渡りの鉱夫
- **titleShort案：** 鉱夫
- **description案：** [PAWN_nameDef]は山から山へと鉱山を渡り歩き、鉱石を掘って暮らした。岩の割れ目を見れば、採れる鉱石のおよその見当がついた。

### 32. `Taxonomist0`

- **title案：** 生き物に詳しい学者
- **titleShort案：** 学者
- **description案：** [PAWN_nameDef]は草木や獣の種類と習性を調べ、書き留めてきた。山野を歩いては珍しい生き物を探すが、争い事は苦手だ。

### 33. `CaveworldIlluminator95`

- **title案：** 坑道の案内人
- **titleShort案：** 案内人
- **description案：** [PAWN_nameDef]は坑道の奥まで道を知り、暗い穴の中で働く者たちを案内した。掘り進める場所を見極める目も持っていた。

## 日本語原稿案：残る史実転用候補54件（第二候補・未承認）

以下54件は前半33件とは別に既存Core 1.6／1.7の対象Def/slotと公式アーカイブ英語原文を個別に照合した。時代に依存する制度・用語を再構成した**作者一括レビュー用の提案**であり、実ゲーム内テキスト・強制Trait・技能・仕事制限・元所有アイテムの変更を承認したものではない。新規独自Defを追加しない。原文の最新Core長文と旧アーカイブの一致は未検証である。

### 34. `AmateurEngineer3`

- **title案：** 道具好きの子
- **titleShort案：** 道具好き
- **description案：** [PAWN_nameDef]は道具を分解しては組み直す子どもだった。壊した物を叱られることもあったが、やがて簡単な修理を頼まれるようになった。

### 35. `ArtStudent79`

- **title案：** 絵師の弟子
- **titleShort案：** 絵師見習い
- **description案：** [PAWN_nameDef]は絵師のもとで筆を取り、絵の描き方を学んだ。長い修業の末、ようやく自分の仕事を任されるようになった。

### 36. `ArtisanFarmer23`

- **title案：** 農家の子
- **titleShort案：** 農家の子
- **description案：** [PAWN_nameDef]は農家に育ち、幼いうちから種まきと収穫を手伝った。作物の世話はよく覚えたが、学問を習う機会は少なかった。

### 37. `AspiringPopIdol28`

- **title案：** 舞の稽古を受けた子
- **titleShort案：** 舞の弟子
- **description案：** [PAWN_nameDef]は幼い頃から歌や舞の稽古を受けていた。人前に立つのは得意だったが、家の仕事はほとんど教わらなかった。

### 38. `Athlete47`

- **title案：** 武芸の得意な子
- **titleShort案：** 武芸好き
- **description案：** [PAWN_nameDef]は同じ年頃の子らと武芸を競い、めきめき腕を上げた。学問にも才を見せたが、芸事や人付き合いは苦手だった。

### 39. `BehaviourResearch74`

- **title案：** 犬の世話人
- **titleShort案：** 犬の世話人
- **description案：** [PAWN_nameDef]は犬と長く暮らし、その癖や仕草を誰よりもよく知っていた。病んだ獣の手当てを頼まれることもあった。

### 40. `CivilServant2`

- **title案：** 役所の書記
- **titleShort案：** 書記
- **description案：** [PAWN_nameDef]は役所で帳簿を付け、届け出や書状を扱っていた。人との折衝には慣れていたが、家を建てるような仕事には疎かった。

### 41. `ClassClown96`

- **title案：** ひょうきんな子
- **titleShort案：** ひょうきん者
- **description案：** [PAWN_nameDef]は人を笑わせることが好きで、いつも何かしらおどけていた。真面目な話になると口ごもるが、芸を見せるのは得意だった。

### 42. `CoreDilettante33`

- **title案：** 富家の道楽者
- **titleShort案：** 道楽者
- **description案：** [PAWN_nameDef]は裕福な家に生まれ、書物や芸事に囲まれて育った。働く必要がなかったため、興味の向くままに日々を過ごしていた。

### 43. `CoreworldStudent50`

- **title案：** 装いに悩んだ子
- **titleShort案：** 装いに悩んだ子
- **description案：** [PAWN_nameDef]は周囲から求められる姿と、自分が望む姿の違いに悩んでいた。身近な人の助けで好きな装いを選べるようになり、飾りを作ることにも親しんだ。

### 44. `DesertRat47`

- **title案：** 修行者に育てられた子
- **titleShort案：** 修行者の子
- **description案：** [PAWN_nameDef]は人里を離れた修行者たちと暮らした。粗末な食事と厳しい暮らしに慣れ、野山を歩きながら護身と薬の知識を学んだ。

### 45. `DestroyerGeneral26`

- **title案：** 水軍大将
- **titleShort案：** 水軍大将
- **description案：** [PAWN_nameDef]は水軍を率い、幾度も海戦を経験した。武芸と采配には優れているが、身の回りの雑事には手を出さない。

### 46. `EnergeticPopIdol61`

- **title案：** 評判の舞人
- **titleShort案：** 舞人
- **description案：** [PAWN_nameDef]は幼いうちから歌や舞を披露し、評判を集めていた。人に褒められるのが好きで、腕を磨いてもっと名を広めたいと考えている。

### 47. `FelineScientist76`

- **title案：** 猫の飼い手
- **titleShort案：** 猫の飼い手
- **description案：** [PAWN_nameDef]は猫を育て、その毛色や性質を熱心に調べていた。病気の猫を手当てした経験もあり、猫のことになると話が長い。

### 48. `FoundryApprentice76`

- **title案：** 鋳物師の弟子
- **titleShort案：** 鋳物見習い
- **description案：** [PAWN_nameDef]は鋳物師の仕事場で、炉の火を保ち、金属を流し込む手伝いをしていた。熱い炉のそばで過ごすうちに、道具を作る腕が身に付いた。

### 49. `Fugitive4`

- **title案：** 追われた家臣
- **titleShort案：** 逃亡者
- **description案：** [PAWN_nameDef]は主人と争い、その土地にいられなくなった。逃亡の道中は食べ物や傷の手当てを自分で工面し、他人を容易に信用しなくなった。

### 50. `GunDealer14`

- **title案：** 武具商
- **titleShort案：** 武具商
- **description案：** [PAWN_nameDef]は弓や刀、槍を商い、兵たちを相手に売り歩いていた。武具の扱いを心得ており、値の交渉も手慣れたものだ。

### 51. `Gymnast48`

- **title案：** 軽業の子
- **titleShort案：** 軽業師
- **description案：** [PAWN_nameDef]は幼い頃から軽業を習い、見物人の前で身軽な芸を見せていた。一座を離れてからも、その技は身に付いたままだ。

### 52. `HedgeFundManager54`

- **title案：** 金貸し
- **titleShort案：** 金貸し
- **description案：** [PAWN_nameDef]は人に銭を貸し、利を得て暮らしていた。帳尻には細かく、損をすることを何より嫌った。

### 53. `HillbillyProdigy60`

- **title案：** 山里の秀才
- **titleShort案：** 秀才
- **description案：** [PAWN_nameDef]は山里で育ち、学問の才を見いだされた。周囲との付き合いは苦手だったが、薬や手当ての知識をよく覚えた。

### 54. `IcePlanetChild95`

- **title案：** 雪国の子
- **titleShort案：** 雪国の子
- **description案：** [PAWN_nameDef]は雪深い土地で育った。人付き合いは少なかったが、道具の仕組みを覚えるのは早かった。

### 55. `IndworldUrchin73`

- **title案：** 路地の子
- **titleShort案：** 路地の子
- **description案：** [PAWN_nameDef]は町の路地で育ち、拾った物を直して使っていた。喧嘩に巻き込まれることも多く、幼くして身を守る術を覚えた。

### 56. `LaborCampOrphan91`

- **title案：** 普請場で育った子
- **titleShort案：** 普請場の子
- **description案：** [PAWN_nameDef]は身寄りを失い、普請場で働きながら育った。土を掘り、石を運ぶ仕事には慣れているが、遊ぶ暇はほとんどなかった。

### 57. `MadScientist47`

- **title案：** 冷酷な学徒
- **titleShort案：** 学徒
- **description案：** [PAWN_nameDef]は人里離れた学者に引き取られ、書物を読んで育った。人の痛みに頓着せず、知りたいことがあれば危ない真似も平気でする。

### 58. `MarineCadet73`

- **title案：** 水軍見習い
- **titleShort案：** 水軍見習い
- **description案：** [PAWN_nameDef]は水軍に入るため、弓や刀の稽古をしていた。ある日上役を傷つける騒ぎを起こし、隊を追い出された。

### 59. `Mathematician6`

- **title案：** 算術家
- **titleShort案：** 算術家
- **description案：** [PAWN_nameDef]は計算に優れ、帳簿や暦の仕事を頼まれていた。数の理屈を考えるのは好きだが、弓を実際に射るのはからきし苦手だ。

### 60. `MechanicsEngineer64`

- **title案：** 水車大工
- **titleShort案：** 水車大工
- **description案：** [PAWN_nameDef]は水車や巻き上げ装置を作り、壊れた仕掛けを直してきた。その腕を頼られる一方、仕事ぶりに口を出されるのを嫌っていた。

### 61. `MedicalStudent38`

- **title案：** 医術の弟子
- **titleShort案：** 医術見習い
- **description案：** [PAWN_nameDef]は医者について薬の扱いと傷の手当てを学んだ。知識は増えたが、重い荷を運んだり穴を掘ったりする仕事には慣れていない。

### 62. `MedicalStudent96`

- **title案：** 医家の子
- **titleShort案：** 医家の子
- **description案：** [PAWN_nameDef]は医者の家に育ち、家族から薬や治療の仕方を教わった。幼い頃から病人を見る機会が多く、手当ての覚えは早かった。

### 63. `MilitaryGunsmith27`

- **title案：** 武具職人
- **titleShort案：** 武具職人
- **description案：** [PAWN_nameDef]は軍勢に付き従い、弓や刀の手入れと修理を引き受けていた。武器の扱いにも慣れ、必要に迫られれば自ら戦うこともあった。

### 64. `MilitaryInventor35`

- **title案：** 軍具職人
- **titleShort案：** 軍具職人
- **description案：** [PAWN_nameDef]は軍勢に仕え、攻城や守りに使う道具を作っていた。使う者の話を聞き、壊れにくい形を考えるのが得意だった。

### 65. `MindwipedAssassin50`

- **title案：** 刺客として育った者
- **titleShort案：** 刺客
- **description案：** [PAWN_nameDef]は幼い頃から人を討つためだけに育てられた。命令に疑問を持つことはなかったが、やがて主に捨てられると知り、逃げ出した。

### 66. `Nerd92`

- **title案：** 本好きの子
- **titleShort案：** 本好き
- **description案：** [PAWN_nameDef]は遊びに出るよりも書物を読むことを好んだ。難しい話を始めると止まらず、同じ年頃の子とはなかなか話が合わなかった。

### 67. `Orphan15`

- **title案：** 孤児
- **titleShort案：** 孤児
- **description案：** [PAWN_nameDef]は親を失い、ほかの子どもたちと助け合って育った。弱い者がいじめられると放っておけず、喧嘩をしてでも守ろうとした。

### 68. `Osteologist74`

- **title案：** 骨を診る医者
- **titleShort案：** 医者
- **description案：** [PAWN_nameDef]は骨折や傷んだ関節を診てきた。骨の形には詳しいが、体を動かして治療するより、見立てを話すほうが得意だった。

### 69. `Pampered87`

- **title案：** 甘やかされた子
- **titleShort案：** 富家の子
- **description案：** [PAWN_nameDef]は裕福な家で何不自由なく育った。欲しい物は頼めば手に入り、食事の支度を自分ですることもなかった。

### 70. `Philosopher82`

- **title案：** 思索好きの子
- **titleShort案：** 思索好き
- **description案：** [PAWN_nameDef]は物事の理由を考え続ける子どもだった。言い争いに巻き込まれても手を出さず、相手の話を聞こうとした。

### 71. `PrivilegedProdigy70`

- **title案：** 名家の秀才
- **titleShort案：** 秀才
- **description案：** [PAWN_nameDef]は裕福な家で大切に育てられ、幼いうちから学問と芸事を教わった。働く者に囲まれて暮らし、自分で力仕事をすることはなかった。

### 72. `PsychiatricPatient94`

- **title案：** 長く療養した者
- **titleShort案：** 療養者
- **description案：** [PAWN_nameDef]は心の不調のため、長い間ほかの人との付き合いを絶って暮らしていた。少しずつ日々の生活を取り戻したが、人と接することは今も負担に感じる。

### 73. `PyroAssistant82`

- **title案：** 火好きの子
- **titleShort案：** 火好き
- **description案：** [PAWN_nameDef]は窯場で育ち、炎を眺めてはいつまでも離れなかった。火を扱う道具の仕組みには詳しいが、危ない火遊びを叱られることもあった。

### 74. `RebelSlave14`

- **title案：** 逃げ出した下人
- **titleShort案：** 逃亡した下人
- **description案：** [PAWN_nameDef]は人に使われる身で育った。仲間と逃げる機会をうかがい、ついに主人のもとから姿を消した。

### 75. `ScienceProdigy65`

- **title案：** 学問好きの子
- **titleShort案：** 秀才
- **description案：** [PAWN_nameDef]はさまざまなことに興味を持ち、医術や学問を熱心に学んだ。火を使った試みで怖い思いをしてからは、火消しには加わろうとしない。

### 76. `SewerKid57`

- **title案：** 虫好きの子
- **titleShort案：** 虫好き
- **description案：** [PAWN_nameDef]は町の溝や水路で虫を集めて遊んでいた。汚れた場所にも平気で入り、食事も自分で作っていた。

### 77. `Sheriff52`

- **title案：** 町の番人
- **titleShort案：** 番人
- **description案：** [PAWN_nameDef]は町の治安を守り、盗人を捕らえたり喧嘩を止めたりしてきた。武芸だけでなく、揉めた者をなだめる話術も身に付けた。

### 78. `SlaveChemist84`

- **title案：** 捕らわれの薬師
- **titleShort案：** 薬師
- **description案：** [PAWN_nameDef]は薬を調合する腕を見込まれ、捕らえられて働かされていた。自由はなかったが、薬の扱いだけは誰よりも詳しくなった。

### 79. `Smuggler23`

- **title案：** 密輸商人
- **titleShort案：** 密輸商人
- **description案：** [PAWN_nameDef]は禁じられた品を人目を避けて運び、港から港へ売り歩いてきた。見張りを出し抜く話術と、危険な相手にも物おじしない度胸がある。

### 80. `StationWhelp94`

- **title案：** 港の孤児
- **titleShort案：** 孤児
- **description案：** [PAWN_nameDef]は港の裏通りで育ち、盗みや喧嘩をして食いつないでいた。やがて海賊の一味に拾われ、そこで暮らすようになった。

### 81. `StreetPeddler66`

- **title案：** 行商の子
- **titleShort案：** 行商の子
- **description案：** [PAWN_nameDef]は小さな品を作り、市や道端で売って家計を助けていた。客との値の交渉も、喧嘩から身を守ることも早く覚えた。

### 82. `Student65`

- **title案：** 学問好きの子
- **titleShort案：** 学問好き
- **description案：** [PAWN_nameDef]は学問に熱中し、書物を読む時間を何よりも大事にしていた。家の掃除や荷運びを頼まれると、いつもうまく逃げていた。

### 83. `StudentSocialite89`

- **title案：** 礼法を学ぶ子
- **titleShort案：** 礼法の子
- **description案：** [PAWN_nameDef]は幼い頃から礼儀作法と人への接し方を教わった。人と話すのは上手だが、手を動かして物を作ることはほとんどなかった。

### 84. `ToolMechanic77`

- **title案：** 道具職人
- **titleShort案：** 道具職人
- **description案：** [PAWN_nameDef]は鋤や鎌、槌などを作り、壊れれば修理してきた。道具を見ればどこが傷んでいるか分かるが、畑仕事は人任せだった。

### 85. `TragicLoner87`

- **title案：** ひとりで育った子
- **titleShort案：** 孤独な子
- **description案：** [PAWN_nameDef]は幼くして家族を失い、人里離れた場所で暮らしてきた。人と話す機会は少なく、書物と動物を相手に日々を過ごした。

### 86. `TransferStudent10`

- **title案：** 寺で学んだ子
- **titleShort案：** 寺の生徒
- **description案：** [PAWN_nameDef]は家を離れ、寺で読み書きや学問を教わった。育ちはよく、学ぶことには熱心だが、体を使う雑用には慣れていない。

### 87. `WarshipCaptain67`

- **title案：** 水軍の船大将
- **titleShort案：** 船大将
- **description案：** [PAWN_nameDef]は戦船を率い、海での戦いを幾度も経験した。船の造りと潮の流れに通じ、船上の兵をまとめる腕があった。

## 承認後に必要な工程（現在は実施しない）

1. 本文・題名・略称をまとめて作者確認し、日本語の史実・語感と元技能・作業制限の整合を確定する。
2. 承認済み日本語原稿に忠実に英訳し、既存原Defに対する `title`・`titleShort`・`description` XMLパッチ、`Languages/Japanese/DefInjected/BackstoryDef` の対応 `title/titleShort/baseDesc/description` を揃える。
3. 強制Traitや初期所持品・追加DLC条件を変更する必要が発覚した候補は別途判断し、無承認変更しない。
4. 他の既存背景候補・不明Solid経路の機械的確認が完了した後、必要最小限のVanilla 1.6/1.7 + MO受入れをまとめて実施する。
