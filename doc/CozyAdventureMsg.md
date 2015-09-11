CozyAdventure - 冒险与编程
===============================

网络消息
===============================

消息id定义
---------
- 1-10000 预留
- 10001-20000 内部
    - 10001-10100 注册和登陆
    - 10101-10200 角色信息同步
    - 10201-10300 冒险farm
    - 10301-10400 佣兵相关
- 20000-30000 插件
- 30000+ 预留

时序图
---------

- 注册和登陆
    - C->S: reg
    - S->C: reg_result
    - C->S: login
    - S->C: login_result
- 角色信息同步
    - C->S: pull
    - S->C: push(PlayerObject,BagObject,push_end)
- 冒险farm
    - C->S: goto_map、goto_home
    - S->C: goto_result
    - S->C: farm_income
- 佣兵相关
    - C->S: upgrade(level,star)
    - S->C: upgrade_result