# Phase 2 既存背景・日本語改稿候補（作者一括確認待ち）

> **全件未承認。製品には適用しない。** ユーザー指示により、日本語文面のレビューは Phase 2 の文章以外の対応を終えた後に**一括**で行う。承認前の英訳・DefInjected/XML製品変更・Steam反映を禁止する。

## 用途と証拠

下表はVanilla既存BackstoryDefの**同じDef名・技能・WorkDisables・requiredWorkTags・forcedTraits・initial possessions・spawnCategories**を維持し、元のSF・現代的叙述のみを古代～中世日本へ置き換える日本語原稿案である。新規日本専用Defではない。初期所持品が存在する定義は改稿確定時に別途史実素材への置換/維持を照合する（現時点で変更しない）。

- 原定義に関する直接資料：過去のProject 原Core 1.6/1.7 metadata v2（[source audit](https://github.com/sucRo-RimWorld/Ancient-Medieval-Japan-Project/blob/main/Docs/Research/VanillaCoreCharacterDefsSourceAudit20261016.md)）で既存35件（成人32件・幼少期3件）の元スロット・生成カテゴリと技能を照合。追加54件についても同じ元Coreメタデータと旧公式英語原文コメントを個別照合した。元ファイルのメタデータは両版一致。これは通常生成での各Solid個体採用を個別立証するものではない。
- 物語内容に関する参考：[Ludeonの旧英語原文コメント](https://github.com/Ludeon/RimWorld-Finnish/blob/master/Backstories/Backstories.xml)。**現在配布されているCore 1.6/1.7本文との完全一致は未立証。**
- 既存35件と追加54件は従来の94件の生成制限、34件の実装済み英日改稿と**重複しない**。
- 強制Traitとの一致が必要な候補：`MasterChef48` の `Gourmand=0`、`Minister88` の `Industriousness=1`、`Model99` の `Beauty=2`。その他のゲーム機構の数値は変更しない。
- 以下の本文は未承認の史実・ゲーム描写案であり、特定時代・地域の役職名や語感について最終作者レビュー後に調整する。薬物の具体的種類・商品・緊急対処の機構はここで確定しない。

## 日本語原稿案（既存35件・第一候補）

### 01. `AddictionCounsel60`

- **title案：** 巡回の救護役
- **titleShort案：** 救護役
- **description案：** [PAWN_nameDef]は酒や薬に溺れた者の暮らしを立て直す手助けをしてきた。争いを避けて村々を巡り、手当てと話し合いによって困窮した人を支えた。歌や手仕事も人の心を和らげるために役立てた。

### 02. `Architect28`

- **title案：** 普請の差図役
- **titleShort案：** 差図役
- **description案：** [PAWN_nameDef]は寺社や館の造営に携わり、建物の構えと意匠を考える役を務めた。大工たちに材の寸法や納まりを伝え、美しい形を追い求めたが、自ら手を動かして細工する経験は乏しかった。

### 03. `BountyHunter41`

- **title案：** 追捕人
- **titleShort案：** 追捕人
- **description案：** [PAWN_nameDef]は盗賊や逃亡者の行方を追い、捕らえる仕事を引き受けていた。弓と刃物の腕を頼りに人目につかず相手へ近づき、雇い主への報告までを務めた。依頼の事情を詮索しないことで知られた。

### 04. `Castaway81`

- **title案：** 漂着者
- **titleShort案：** 漂着者
- **description案：** [PAWN_nameDef]は船が難破した後、人の住まない島に取り残された。草木から食べ物を探し、雨露をしのぐ小屋を造り、岩場から必要な石を採った。生き延びるうちに、日々の炊事も自分で賄えるようになった。

### 05. `CharityWorker36`

- **title案：** 施しの世話役
- **titleShort案：** 世話役
- **description案：** [PAWN_nameDef]は食べ物や住まいを失った人々のため、寺や村の施しを取り仕切っていた。困窮する者の訴えを聞き、施しの手配や病人の世話に力を尽くした。争いごとには加わらず、話し合いで解決しようとした。

### 06. `CorporateBuilder58`

- **title案：** 普請の人足
- **titleShort案：** 人足
- **description案：** [PAWN_nameDef]は館や堤の普請に加わり、土石を運び、基礎を掘り、建物を組み上げた。単調で骨の折れる仕事を重ねるうちに、手早く作業を進める技を覚えたが、絵や細工に心を向ける余裕は失った。

### 07. `ExpertHandyman8`

- **title案：** 旅の修繕職
- **titleShort案：** 修繕職
- **description案：** [PAWN_nameDef]は各地を巡って家屋、道具、農具の傷みを直してきた。壊れ方を見極め、必要な材を選び、仕組みを考えて修理するため、建築と細工の知恵を身に付けた。

### 08. `FurnitureBuilder83`

- **title案：** 木地の細工師
- **titleShort案：** 木工師
- **description案：** [PAWN_nameDef]は山林に近い土地で木を選び、板を整え、箱や棚、家具を造って暮らしていた。樹木の性質を見分け、材を無駄にせず組み上げる仕事によって、木と建物の扱いに通じた。

### 09. `GangSoldier39`

- **title案：** 野盗の古参
- **titleShort案：** 野盗
- **description案：** [PAWN_nameDef]は幾度も争いをくぐり抜け、山道を行き交う者を襲う一味の古参となった。弓と刃物を使い、仲間の傷を手当てし、野営の食事も作った。戦いは得意でも、荷運びや芸事には関わろうとしなかった。

### 10. `LanguageAnalyst27`

- **title案：** 文書の解読役
- **titleShort案：** 解読役
- **description案：** [PAWN_nameDef]は争いの絶えない土地で、使者の書状や敵方の符牒を読み解く役を務めた。言葉の違いを学び、古い文書を調べることに長けた。務めを離れると畑の草木を育て、暮らしを支えた。

### 11. `MasterChef48`

- **title案：** 饗応の料理人
- **titleShort案：** 料理人
- **description案：** [PAWN_nameDef]は大きな館の台所で下働きから始め、やがて客をもてなす膳を任されるようになった。ある宴で料理に毒が入れられ、責めを負って職を追われた。なお食への執着は衰えず、味と盛り付けを磨き続けた。

### 12. `MedicSoldier54`

- **title案：** 陣中の手当て役
- **titleShort案：** 手当て役
- **description案：** [PAWN_nameDef]は戦の陣で負傷者を看病し、傷を洗い、包帯を替え、薬の知識を深めた。目の前で多くの命が失われるのを見て、自ら武器を取ることを避けるようになった。戦場を離れた後も病人の手当に努めた。

### 13. `MilitaryOfficer17`

- **title案：** 軍勢の指揮役
- **titleShort案：** 指揮役
- **description案：** [PAWN_nameDef]は武家の軍勢に仕え、弓の稽古と陣立てを学んだ。戦では隊を率いて戦ったが、言葉がきつく、周囲との折り合いを欠くことも多かった。負傷者の世話よりも命令と戦の務めを優先した。

### 14. `Minister88`

- **title案：** 政務の奉行
- **titleShort案：** 奉行
- **description案：** [PAWN_nameDef]は有力者のもとで年貢の取りまとめや人々の訴えを扱い、土地の政務を担った。人に指示するだけでなく書付や道具の扱いも覚え、仕事を怠らなかった。地位を得て贅沢を知っても、務めへの熱心さは変わらなかった。

### 15. `Model99`

- **title案：** 舞の芸能者
- **titleShort案：** 芸能者
- **description案：** [PAWN_nameDef]は装いと舞によって人々の目を集め、宴や祝いの席に招かれていた。衣装や飾りの見せ方を工夫し、人と交わる芸を磨いた一方で、家事や学問、力仕事に携わる機会はほとんどなかった。

### 16. `NinjaAssassin31`

- **title案：** 忍びの刺客
- **titleShort案：** 刺客
- **description案：** [PAWN_nameDef]は人知れず近づいて相手を討つ仕事を引き受け、変装や聞き込みを覚えた。弓と刃物の腕に加え、簡単な道具の細工にも通じていた。ある仕事で草木に身を隠したことが災いし、以来、畑仕事を避けるようになった。

### 17. `Novelist7`

- **title案：** 物語の書き手
- **titleShort案：** 書き手
- **description案：** [PAWN_nameDef]は物語や歌を作り、写本を通じて名を知られるようになった。筆を執る日々に慣れ、土を掘ることも建物を直すことも不得手だった。賑やかな席よりも、ひとりで言葉を練る時間を好んだ。

### 18. `PirateTrooper73`

- **title案：** 海賊衆の切込兵
- **titleShort案：** 切込兵
- **description案：** [PAWN_nameDef]は海上で船を襲う一味に加わり、敵船へ乗り込む役を務めた。弓を射ち、刃物を振るう腕は確かだったが、冷酷な働きを重ねるうちに、他人をいたわることや人付き合いを避けるようになった。

### 19. `PlanetaryDiplomat7`

- **title案：** 使節の交渉役
- **titleShort案：** 交渉役
- **description案：** [PAWN_nameDef]は領主同士の争いを収めるため、使者として各地へ赴いた。書状の読み書きと礼儀を学び、言葉と駆け引きで妥協を探ることに力を注いだ。武器を取ることや日々の力仕事は、もっぱら従者に任せた。

### 20. `PrisonerOfWar2`

- **title案：** 戦の捕虜
- **titleShort案：** 捕虜
- **description案：** [PAWN_nameDef]は戦で敵方に捕らえられ、処刑を待つ身となった。ある騒ぎに乗じて仲間の捕虜をまとめ、見張りを振り切って脱出した。争いの中で身を守る術と、人を説得する術を覚えた。

### 21. `ReligiousHierarch16`

- **title案：** 寺院の長老
- **titleShort案：** 長老
- **description案：** [PAWN_nameDef]は寺に入り、説法や人々の相談に応じながら、寺領の取りまとめを任される立場となった。来訪者に食事を振る舞い、病人の手当ても行ったが、田畑や建物の力仕事にはほとんど携わらなかった。

### 22. `RunawayDancer35`

- **title案：** 旅の舞人
- **titleShort案：** 舞人
- **description案：** [PAWN_nameDef]は生まれ育った土地を離れ、歌や舞を披露しながら諸国を巡った。人と交わる術と芸を磨き、旅先で危険に遭えば武器を扱うこともあった。

### 23. `Sculptor63`

- **title案：** 石仏の彫り手
- **titleShort案：** 石工
- **description案：** [PAWN_nameDef]は石を削って仏像や記念の石造物を造り、やがて大きな岩壁を彫る仕事にも携わった。石の質を見極める目と彫りの腕を持つ一方、台所仕事や日用品の細工は不得手だった。

### 24. `SoleSurvivor85`

- **title案：** 落城の生存者
- **titleShort案：** 生存者
- **description案：** [PAWN_nameDef]の暮らしていた集落は戦に巻き込まれ、家も身内も失われた。生き残ったのは[PAWN_nameDef]だけだった。危険を逃れるうちに戦う力は身に付いたが、人を信じたり、動物や芸事に心を向けたりすることは難しくなった。

### 25. `StationSecurity38`

- **title案：** 関所の警固役
- **titleShort案：** 警固役
- **description案：** [PAWN_nameDef]は交通の要所に置かれた関所を守り、怪しい荷と人の出入りを見張った。争いを抑えるための武芸と交渉、けが人の手当ても覚えた。ある火事で手ひどい目に遭い、火を消す仕事だけは避けるようになった。

### 26. `ChildStar74`

- **title案：** 旅芸人の子
- **titleShort案：** 芸人の子
- **description案：** [PAWN_nameDef]は幼い頃から芸能の一座に加わり、歌や舞を人前で披露していた。多くの大人と接する機会に恵まれ、芸と人付き合いには慣れたが、文字や学問を教わる時間は少なかった。

### 27. `CrashBaby32`

- **title案：** 難破船の拾い子
- **titleShort案：** 拾い子
- **description案：** [PAWN_nameDef]は赤子の頃に船の難破に遭い、ただ一人生き残った。通りかかった人々に助けられ、その集落の子として育てられた。身寄りはないが、周囲に見守られながら新たな暮らしを覚えた。

### 28. `UrbworldUrchin61`

- **title案：** 町裏の孤児
- **titleShort案：** 町の孤児
- **description案：** [PAWN_nameDef]は人の多い町の片隅で育った。食べ物を得るために道端を歩き回り、ときには奪い合いになった。素早く身を守る術を身に付けたが、安らげる場所はほとんどなかった。

### 29. `UrbworldDrone32`

- **title案：** 普請場の苦役人
- **titleShort案：** 苦役人
- **description案：** [PAWN_nameDef]は自由を奪われ、普請場で土を掘り、石を運び、建物を組み上げることを強いられていた。ようやく束縛から逃れた後も、人に命じられた作業はこなせる一方、書物や芸事には馴染めず、人付き合いも苦手だった。

### 30. `UrbworldEntrepreneur14`

- **title案：** 町の商い頭
- **titleShort案：** 商人
- **description案：** [PAWN_nameDef]は市や湊で商いを広げ、物資の値動きと人のつながりを見極めて利益を上げてきた。約束や貸し借りを取りまとめるため、勘定と交渉の腕を磨き、時には有力者との関係も利用した。

### 31. `ConstructionEngineer32`

- **title案：** 普請の棟梁
- **titleShort案：** 棟梁
- **description案：** [PAWN_nameDef]は普請場で働く大工や人足を率い、家屋から大きな堂まで建ててきた。材の扱いと組み立てに通じる反面、炊事はいつも家人や周囲の者に任せていた。

### 32. `MafiaBoss17`

- **title案：** 町の顔役
- **titleShort案：** 顔役
- **description案：** [PAWN_nameDef]は町の荒くれ者たちを束ね、商いの取り分と貸し金の取り立てを仕切っていた。役人に取り入り、配下を従わせ、ときには力ずくで要求を通した。人の世話や家事には手を出さず、交渉と威圧を仕事とした。

### 33. `ContractMiner86`

- **title案：** 渡りの山師
- **titleShort案：** 山師
- **description案：** [PAWN_nameDef]は鉱山を渡り歩き、岩を砕いて鉱石や古い品を掘り出して暮らした。土と石にまみれる骨の折れる仕事だったが、自分の手で地中から価値あるものを見つけることに誇りを持っていた。

### 34. `Taxonomist0`

- **title案：** 草木と獣の調べ役
- **titleShort案：** 調べ役
- **description案：** [PAWN_nameDef]は山野を巡り、草木と獣の姿や習性を書き留めてきた。採れる場所や季節による違いを調べ、見聞をまとめることに心を砕いた。争いを好まず、生き物への関心から各地へ足を運んだ。

### 35. `CaveworldIlluminator95`

- **title案：** 坑道の道案内
- **titleShort案：** 道案内
- **description案：** [PAWN_nameDef]は地下の坑道に通じ、暗い道筋を覚えて掘るべき場所を示していた。灯りと目印を頼りに危険を察知し、働く者たちを導いた。岩の性質を見分ける力と、人々へ状況を伝える力を身に付けた。

## 日本語原稿案：残る史実転用候補54件（第二候補・未承認）

以下54件は以前の35件とは別に既存Core 1.6／1.7の対象Def/slotと公式アーカイブ英語原文を個別に照合した。時代に依存する制度・用語を再構成した**作者一括レビュー用の提案**であり、実ゲーム内テキスト・強制Trait・技能・仕事制限・元所有アイテムの変更を承認したものではない。新規独自Defを追加しない。原文の最新Core長文と旧アーカイブの一致は未検証である。

### 36. `AmateurEngineer3`

- **title案：** 仕掛け好きの子
- **titleShort案：** 仕掛け好き
- **description案：** [PAWN_nameDef]は武芸と普請に携わる家に育った。戦場へ出ることを親に止められ、建物や道具の仕組みを学び始めた。いつか遠くの土地へ行き、知らない技を見たいと思っていた。

### 37. `ArtStudent79`

- **title案：** 絵師の弟子
- **titleShort案：** 絵師見習い
- **description案：** [PAWN_nameDef]は長年絵を学び、師の工房で腕を磨いた。寺社や裕福な家の求めに応じて絵を描くようになり、その仕事で慎ましく暮らしていた。

### 38. `ArtisanFarmer23`

- **title案：** 古式農家の子
- **titleShort案：** 農家の子
- **description案：** [PAWN_nameDef]は代々田畑を守る家で育った。新しいやり方よりも土地に伝わる耕作を大切にし、種まきや収穫、食事の支度を覚えた。争いには加わろうとしなかった。

### 39. `AspiringPopIdol28`

- **title案：** 舞楽の弟子
- **titleShort案：** 舞の弟子
- **description案：** [PAWN_nameDef]は幼い頃から歌と舞の稽古を受け、宴席で芸を披露するよう育てられた。身の回りのことは付き人が引き受け、武芸や力仕事にはほとんど縁がなかった。

### 40. `Athlete47`

- **title案：** 武芸の秀才
- **titleShort案：** 武芸の子
- **description案：** [PAWN_nameDef]は幼い頃から身体を鍛えて武芸を学び、その才を認められた。やがて学問も修めるようになったが、人付き合いや芸事は得意ではなかった。

### 41. `BehaviourResearch74`

- **title案：** 犬を調べる者
- **titleShort案：** 犬の調べ役
- **description案：** [PAWN_nameDef]は人里を離れて育ち、のちに人々に迎えられて暮らしを学んだ。犬の習性を知ることに強い関心を持ち、獣の世話や手当に通じたが、草木を育てることは好まなかった。

### 42. `CivilServant2`

- **title案：** 役所の文書役
- **titleShort案：** 文書役
- **description案：** [PAWN_nameDef]は役所で帳簿と書付を扱い、人々の願い出を取り次いだ。細かな手続きと人間関係には慣れていたが、建物の修繕などは苦手だった。

### 43. `ClassClown96`

- **title案：** 戯け者の子
- **titleShort案：** 戯け者
- **description案：** [PAWN_nameDef]は学びの場で緊張が高まると、わざとおどけて周囲を笑わせた。言葉で人を説得するのは得意ではなかったが、人を楽しませる芸を覚え、争いは避けた。

### 44. `CoreDilettante33`

- **title案：** 富家の遊学児
- **titleShort案：** 遊学児
- **description案：** [PAWN_nameDef]は裕福な家に生まれ、学問と芸事を気ままに楽しんで育った。やがて何不自由ない暮らしに物足りなさを感じ、外の世界へ出ることを望むようになった。

### 45. `CoreworldStudent50`

- **title案：** 装身具好きの子
- **titleShort案：** 細工好き
- **description案：** [PAWN_nameDef]は幼い頃から周囲に求められる男児としての振る舞いに違和感を持っていた。身近な人の支えを得て自分に合う装いを選び、装身具の細工に親しんだ。台所や畑の仕事は避けていた。

### 46. `DesertRat47`

- **title案：** 山野の修行児
- **titleShort案：** 修行児
- **description案：** [PAWN_nameDef]は人里を離れて歩く修行者たちと暮らし、欲を捨てて知恵を得ようとした。山道で身を守る術と薬草の知識を学び、厳しい暮らしを苦にしなかった。

### 47. `DestroyerGeneral26`

- **title案：** 水軍の大将
- **titleShort案：** 水軍大将
- **description案：** [PAWN_nameDef]は水軍を率い、船の動かし方と武器の扱いを身に付けた。部下をまとめる腕と武芸には優れたが、日々の雑用を自ら引き受けることはなかった。

### 48. `EnergeticPopIdol61`

- **title案：** 舞の花形
- **titleShort案：** 舞人
- **description案：** [PAWN_nameDef]は歌と舞、剣の稽古を重ね、若くして宴席の花形となった。人々の視線を集めることを好み、さらに名声を求めて一座を離れた。

### 49. `FelineScientist76`

- **title案：** 猫の飼育師
- **titleShort案：** 猫の世話役
- **description案：** [PAWN_nameDef]は猫の毛色や気質を観察し、よい飼い方と繁殖の組み合わせを考えてきた。望ましい猫を育てられる土地を探して各地を巡り、獣の手当に関する知識も深めた。

### 50. `FoundryApprentice76`

- **title案：** 鋳物師の弟子
- **titleShort案：** 鋳物見習い
- **description案：** [PAWN_nameDef]は鋳物師のもとで働き、炉の扱いと金属を形にする仕事を覚えた。力と細工の腕は身に付いたが、絵や装飾の稽古をする時間はほとんどなかった。

### 51. `Fugitive4`

- **title案：** 領主に逆らった逃亡者
- **titleShort案：** 逃亡者
- **description案：** [PAWN_nameDef]は仕えていた有力者に反旗を翻したが、企てに失敗して追われる身となった。各地を移り歩き、食べ物と薬を自分で用意する術を覚えたものの、味方を作るのは不得手だった。

### 52. `GunDealer14`

- **title案：** 武具商
- **titleShort案：** 武具商
- **description案：** [PAWN_nameDef]は争いの絶えない土地を巡り、弓や刀、槍などの武具を売り買いしてきた。品の扱いを実演し、値を交渉することで商いを広げた。

### 53. `Gymnast48`

- **title案：** 軽業の見習い
- **titleShort案：** 軽業師
- **description案：** [PAWN_nameDef]は幼い頃から身体の柔らかさと身軽さを買われ、軽業の稽古に励んだ。さらに芸を磨こうと各地へ出たが、思うように仕事が得られず、暮らし向きは苦しくなった。

### 54. `HedgeFundManager54`

- **title案：** 金貸しの番頭
- **titleShort案：** 金貸し
- **description案：** [PAWN_nameDef]は金貸しのもとで貸付と取り立てを取り仕切り、巧みな話術で財を増やした。ある取引を巡って追われる身となり、各地を転々とするうち、富家の好む工芸品に関心を持った。

### 55. `HillbillyProdigy60`

- **title案：** 寒村の異才
- **titleShort案：** 村の秀才
- **description案：** [PAWN_nameDef]は寒い山里で育ち、荒っぽい喧嘩と手仕事の中で暮らしていた。人付き合いは苦手だったが、学問の才を見いだされ、やがて医術を習うようになった。

### 56. `IcePlanetChild95`

- **title案：** 北国の船乗りの子
- **titleShort案：** 北国の子
- **description案：** [PAWN_nameDef]は雪の多い海辺で育ち、獣と少数の船乗りを友として過ごした。人と交わる機会は乏しかったが、船や道具の仕組みを調べることに熱心だった。

### 57. `IndworldUrchin73`

- **title案：** 町裏の浮浪児
- **titleShort案：** 町の浮浪児
- **description案：** [PAWN_nameDef]は町の路地で育ち、食べ物を得るため物乞いや小さな盗みを重ねた。捨てられた金物を拾い、直して使ううちに手先の器用さを身に付けた。

### 58. `LaborCampOrphan91`

- **title案：** 普請場の孤児
- **titleShort案：** 普請場の子
- **description案：** [PAWN_nameDef]は事故で親を失い、寺社や城の普請に使われる子どもたちの中で育った。土を掘り、石を運ぶ過酷な日々によって力仕事を覚えたが、芸事には触れずに過ごした。

### 59. `MadScientist47`

- **title案：** 荒野の異端の弟子
- **titleShort案：** 異端の弟子
- **description案：** [PAWN_nameDef]は荒れた土地で苦しい幼少期を送り、世捨て人の学者に教えを受けた。そのもとで学問と戦う術を学び、人の恐れを見抜いて利用することにも長けていった。

### 60. `MarineCadet73`

- **title案：** 水軍の若衆
- **titleShort案：** 水軍の若衆
- **description案：** [PAWN_nameDef]は水軍の一員になるため、弓と刀の稽古を受けていた。あるとき上役を射ってしまい、故意だったのか事故だったのか分からないまま軍勢から追い出された。

### 61. `Mathematician6`

- **title案：** 算の師
- **titleShort案：** 算の師
- **description案：** [PAWN_nameDef]は寺や役所で数の理を学び、暦や勘定の計算に通じた。余暇には弓の理屈を考えることもあったが、実際に矢を当てる腕はさっぱりだった。

### 62. `MechanicsEngineer64`

- **title案：** 仕掛け大工
- **titleShort案：** 仕掛け大工
- **description案：** [PAWN_nameDef]は水車や荷上げの仕掛け、建物の道具を造り直す腕のよい職人だった。裕福な依頼主に認められ仕事を選べるようになった一方、恨みを買うこともあった。

### 63. `MedicalStudent38`

- **title案：** 薬師の弟子
- **titleShort案：** 薬師見習い
- **description案：** [PAWN_nameDef]は医術を伝える家に育ち、薬草の練り方から傷の手当てまで学んだ。知識には自信を持つようになったが、採掘や荷運びなどの力仕事は人任せにしていた。

### 64. `MedicalStudent96`

- **title案：** 医家の子
- **titleShort案：** 医家の子
- **description案：** [PAWN_nameDef]は代々医術を営む家に生まれ、親や兄姉から薬と手当てを教わった。多くの友人は持たなかったが、身内とは仲よく過ごし、医術を磨いた。

### 65. `MilitaryGunsmith27`

- **title案：** 陣中の弓具職
- **titleShort案：** 弓具職
- **description案：** [PAWN_nameDef]は戦の軍勢に加わり、前線に出る代わりに弓や矢、刀の修繕を任された。武具の仕組みに詳しく、簡単な炊事や負傷者の手当ても引き受けた。

### 66. `MilitaryInventor35`

- **title案：** 軍具の考案者
- **titleShort案：** 軍具職
- **description案：** [PAWN_nameDef]は家を出て武家に仕え、矢を防ぐ盾や城攻めの道具を考案した。細工と学問の知識を戦の役に立てたが、絵や装飾にはあまり関心を示さなかった。

### 67. `MindwipedAssassin50`

- **title案：** 仕込まれた刺客
- **titleShort案：** 刺客
- **description案：** [PAWN_nameDef]は幼い頃から有力者に引き取られ、命じられた相手を討つための厳しい訓練を受けた。やがて自分が口封じされると知り、かつての主に刃を向けて逃げ出した。

### 68. `Nerd92`

- **title案：** 書物好きの子
- **titleShort案：** 書物好き
- **description案：** [PAWN_nameDef]は他の子が遊ぶ間も、道具の仕組みや武具について書かれた本を読みふけっていた。知りたがりな性格だったが、話し方に癖があり、友人は少なかった。

### 69. `Orphan15`

- **title案：** 孤児の庇護者
- **titleShort案：** 孤児
- **description案：** [PAWN_nameDef]は幼い頃に親を失い、寺の近くで暮らした。年下の子がいじめられると進んで守りに入り、腕っぷしと人を手当てする術を覚えた。

### 70. `Osteologist74`

- **title案：** 骨を調べる医師
- **titleShort案：** 骨の医師
- **description案：** [PAWN_nameDef]は骨や負傷した身体を調べることに熱中し、その知識を買われて人々に教えを説くようになった。腕力にも自信を持ったが、雑用や土仕事は弟子に任せがちだった。

### 71. `Pampered87`

- **title案：** 甘やかされた子
- **titleShort案：** 富家の子
- **description案：** [PAWN_nameDef]は裕福な家で育ち、欲しい物は大人が用意してくれた。暮らしの苦労を知らず、食事の支度も使用人に任せていたため、炊事を嫌うようになった。

### 72. `Philosopher82`

- **title案：** 思索する若者
- **titleShort案：** 思索家
- **description案：** [PAWN_nameDef]は学びの場で周囲となじめず、ひとりで物事を考える時間を好んだ。人の行いを興味深く眺め、争いに巻き込まれても一歩離れたところから考えようとした。

### 73. `PrivilegedProdigy70`

- **title案：** 名家の英才
- **titleShort案：** 英才
- **description案：** [PAWN_nameDef]は幼くして才を認められ、裕福な家族の支えによって学問と芸事を学んだ。手を汚す仕事や武力による争いに関わる機会はほとんどなかった。

### 74. `PsychiatricPatient94`

- **title案：** 療養を続けた者
- **titleShort案：** 療養者
- **description案：** [PAWN_nameDef]は心の病に苦しみ、長く人里を離れた場所で療養した。周囲から理解されず厳しく扱われた経験は、暮らしの技能を身に付ける機会を奪った。やがて落ち着きを取り戻しても、その影響は残った。

### 75. `PyroAssistant82`

- **title案：** 窯場の小僧
- **titleShort案：** 窯場の子
- **description案：** [PAWN_nameDef]は幼い頃から窯場で薪を運び、熱い炉のそばで働いた。火を扱うことに強い関心を持ち、ある失敗を境に炊事や火消しを任せてもらえなくなった。

### 76. `RebelSlave14`

- **title案：** 隷属を逃れた子
- **titleShort案：** 逃亡した子
- **description案：** [PAWN_nameDef]は生まれながらに自由を奪われ、雑用を強いられて育った。やがて同じ境遇の者たちと力を合わせ、主の支配から逃れようと立ち上がった。

### 77. `ScienceProdigy65`

- **title案：** 学問の秀才
- **titleShort案：** 秀才
- **description案：** [PAWN_nameDef]は学問の才を見いだされ、薬草や星の動き、歌など幅広いことを学んだ。ある実験中の火事をきっかけに炎を恐れるようになり、火消しには加われなくなった。

### 78. `SewerKid57`

- **title案：** 溝渠の虫好き
- **titleShort案：** 虫好き
- **description案：** [PAWN_nameDef]は町の水路や汚れた溝の近くで育ち、暗がりに住む虫たちを友として過ごした。虫に親しむ一方、食事の支度も自分で覚えなければならなかった。

### 79. `Sheriff52`

- **title案：** 町の追捕役
- **titleShort案：** 追捕役
- **description案：** [PAWN_nameDef]は町で揉め事を収め、盗人を捕える役を務めていた。酒に酔った者の喧嘩から大きな争いまで対処し、武芸と人を説得する術を磨いた。

### 80. `SlaveChemist84`

- **title案：** 捕らわれた薬師
- **titleShort案：** 捕虜の薬師
- **description案：** [PAWN_nameDef]は薬作りの才を認められたが、やがて捕らえられて有力者に売られた。命じられるまま薬を調合して人に試す仕事を強いられ、医術のあり方に深い疑問を抱いた。

### 81. `Smuggler23`

- **title案：** 抜荷商
- **titleShort案：** 抜荷商
- **description案：** [PAWN_nameDef]は船で荷を運ぶ仕事に就き、見張りの目を逃れて禁じられた品を売りさばいていた。やがて自分の船を持ち、港から港へ渡って取引するようになった。

### 82. `StationWhelp94`

- **title案：** 港町の孤児
- **titleShort案：** 港の孤児
- **description案：** [PAWN_nameDef]は港町で身寄りなく育ち、汚れ仕事や盗みで命をつないだ。虐げられるうちに荒っぽくなり、ついには町を追われて海賊衆に拾われた。

### 83. `StreetPeddler66`

- **title案：** 露天の売り子
- **titleShort案：** 売り子
- **description案：** [PAWN_nameDef]は町で小さな飾りや道具を作り、道行く人に売って家族の暮らしを助けた。品物の良し悪しだけでなく、人とのつながりが商いを左右することを学んだ。

### 84. `Student65`

- **title案：** 理屈好きの徒弟
- **titleShort案：** 徒弟
- **description案：** [PAWN_nameDef]は数の理や自然の仕組みを調べることを楽しんでいた。重い物を運ぶ仕事や掃除は巧みに他人へ頼み、自分は学問に専念しようとした。

### 85. `StudentSocialite89`

- **title案：** 礼法を学んだ子
- **titleShort案：** 礼法の子
- **description案：** [PAWN_nameDef]は名家の子として礼儀や言葉遣い、人の心を読む術を教わった。交渉の才はあったが、他人を操ることに喜びを感じられず、学びの途中でその場を離れた。

### 86. `ToolMechanic77`

- **title案：** 諸道具の鍛冶
- **titleShort案：** 道具職人
- **description案：** [PAWN_nameDef]は刃物や槌などを直す腕のよい職人だった。勤め先の工房が閉じられると別の集落に身を寄せ、道具を作って村々を回りながら暮らした。

### 87. `TragicLoner87`

- **title案：** 人里離れた子
- **titleShort案：** 孤独な子
- **description案：** [PAWN_nameDef]は事故によって家族を失い、人里から遠ざけられた。動物を友とし、手元に残った書物を読みながら、ひとりで暮らす術を覚えた。

### 88. `TransferStudent10`

- **title案：** 寺院に学んだ富家の子
- **titleShort案：** 遊学生
- **description案：** [PAWN_nameDef]は裕福な家から遠くの寺院へ学びに出された。学問には熱心だったが、身の回りの力仕事は他人が済ませてくれたため、自ら行うことには慣れていなかった。

### 89. `WarshipCaptain67`

- **title案：** 水軍の船将
- **titleShort案：** 船将
- **description案：** [PAWN_nameDef]は戦船を指揮し、船の構造と人を率いる術を身に付けた。戦場では落ち着いて命令を下し、敵の攻撃にもひるまなかったが、陸の草木に触れる機会は少なかった。

## 承認後に必要な工程（現在は実施しない）

1. 本文・題名・略称をまとめて作者確認し、日本語の史実・語感と元技能・作業制限の整合を確定する。
2. 承認済み日本語原稿に忠実に英訳し、既存原Defに対する `title`・`titleShort`・`description` XMLパッチ、`Languages/Japanese/DefInjected/BackstoryDef` の対応 `title/titleShort/baseDesc/description` を揃える。
3. 強制Traitや初期所持品・追加DLC条件を変更する必要が発覚した候補は別途判断し、無承認変更しない。
4. 他の既存背景候補・不明Solid経路の機械的確認が完了した後、必要最小限のVanilla 1.6/1.7 + MO受入れをまとめて実施する。
