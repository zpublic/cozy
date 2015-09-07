CozyPoker - 可联机的扑克游戏集
===============================

> 公用
>
>> CozyPoker.Engine - 扑克游戏引擎  
>> CozyPoker.Network - 网络通信模块  
>
>
> 客户端
>
>> CozyPoker.Client.Core - 客户端逻辑  
>> CozyPoker.Client.Tester - 客户端逻辑测试工程  
>> CozyPoker.Client.Game24Calc - 算24点游戏命令行单机版  
>> CozyPoker.Client.Bullfight - 斗牛游戏WPF联机版  
>> CozyPoker.Client.Mobile - 跨平台联机版  
>
>
> 服务端
>
>> CozyPoker.Server.Core - 服务端逻辑  
>> CozyPoker.Server.Console - 命令行服务端程序  
>
>
> 依赖
>
>> CozyLua - lua脚本执行  
>> Lidgren.Network - UDP网络库  
>> MonoGame（CocosSharp） - 跨平台游戏引擎  
>>

CozyPoker.Engine设计
===============================

1、Model 实体类

    1.1 Card
    牌（包含A-K、大小王值；黑红梅方王类型）
    
    1.2 CardCollect
    牌集（有洗牌、整牌、加牌、拿牌等操作）

2、Util 实用工具类

    2.1 NormalCardCollect
    常规的牌集（A-K、一副扑克等）

    2.2 NormalCardCompare
    常规的单张牌大小比较

3、Pattern 扑克游戏模式（起名参考：http://bbs.3dmgame.com/thread-3507855-1-1.html）

    3.1 PatternAequitas
    发固定的牌，没有然后了（算24点、抽牌）
    
    3.2 PatternFirehawk
    发固定的牌，然后进行比大小（斗牛、扎金花、梭哈）
    
    3.3 PatternImpaler
    发固定的牌，轮流出牌比谁先打完（斗地主、跑得快）
    
    3.4 PatternThumper
    有摸牌的比谁先打完（干瞪眼、加减乘除、五十K）

4、Method 策略方法类

    4.1 CardCollectMethod
    得到一个牌集
    
    4.2 CardCompareMethod
    单张牌大小比较
    
    4.3 CalcCardsValueMethod
    计算一组牌的分数
    
    4.4 ShuffleMethod
    洗牌
    
    4.5 SortMethod
    理牌
    
    4.6 DealMethod
    发牌

