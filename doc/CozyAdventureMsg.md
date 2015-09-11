CozyAdventure - 冒险与编程
===============================

网络消息
===============================

消息id定义
---------
- 00001-10000 预留
    - 00001-00100 核心消息
- 10001-20000 内部
    - 10001-10100 注册和登陆
    - 10101-10200 角色信息同步
    - 10201-10300 冒险farm
    - 10301-10400 佣兵相关
    - 10401-10500 礼物和彩蛋
- 20000-30000 插件
- 30000-65535 预留

时序图
---------

- 核心消息
    - C->S: Heart
    - C->C: Heart
- 注册和登陆
    - C->S: Reg
    - S->C: RegResult
    - C->S: Login
    - S->C: LoginResult
- 角色信息同步
    - C->S: Pull
    - S->C: Push(PlayerObject,BagObject,PushEnd)
- 冒险farm
    - C->S: GotoMap、GotoHome
    - S->C: GotoResult
    - S->C: farmIncome
- 佣兵相关
    - C->S: Upgrade(level,star)
    - S->C: UpgradeResult
- 礼物和彩蛋
    - C->S: GetGift
    - S->C: GetGiftResult
