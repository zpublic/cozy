Cozy
========

简介
--------------------------------
<b>这是一个用于学习C＃的开源项目 && 做点东西玩玩</b>

源码结构
--------------------------------
> Learn - 学习工程目录  
> 
>> [Cozy - 命令行版c＃学习工程](doc/LearnCozy.md)  
>>
>> [WpfCozy - WPF学习工程](doc/LearnWpfCozy.md)
>>
>> StoreCozy - win8.1 store app学习工程  
>
>
> Public - 公共代码目录  
>> CozyPublic - 暂未整理的公共代码  
>> CozyPublic.WinApi - 调用windows api的包装  
>> FileUtilCpp - 文件操作util（c++）  
>
>
> Practice - 实践工程目录  
> 
>> CozyDisplayFusion - 多显示器管理软件  
>>> 可以通过快捷键来控制应用程序窗口，实现将当前窗口移到另一个显示器并最大化等功能。  
>>
>> CozyQuick - 快捷启动工具
>>> 通过快捷键、命令行、鼠标手势、定时、监控等实现快捷的搜索、创建进程、切换窗口、计算md5、关闭显示器、查看天气、调节音量操作。  
>>> 
>>> CozyQuick.Exe - 主程序  
>>> CozyQuick.Engine - 调度引擎  
>>> CozyQuick.Interface - 接口定义  
>>> CozyQuick.Plugin.Timer - 定时支持插件  
>>> CozyQuick.Plugin.HotKey - 快捷键支持插件  
>>> CozyQuick.Plugin.Msgbox - 消息框功能插件  
>>> CozyQuick.Plugin.AudioPlayer - 声音提示功能插件  
>>
>> CozySql - 数据库查看/查询工具（目标是超越LinqPad）  
>>> CozySql.Exe - 主程序  
>>> CozySql.Configure - 配置和记录  
>>
>> CozyBlog - 简单博客  
>>> 用ASP.NET MVC做一个简单的博客系统。  
>>
>> CozyDraw  
>>> windows universal app版本的绘图（UML图？）工具  
>>
>> CozyHttpServer  
>>> 基于Nancy的跨平台http服务端尝试  
>>
>> CozyNegativeEnergy  
>>> 每天来点负能量，社交练级形式的负能量分享网站  
>>
>> CozyKxlol - 用MonoGame做2drpg游戏  
>>> CozyKxlol - 游戏主程序  
>>> CozyDebugHelper - 辅助使用OutputDebugString的封装  
>>> CozyDebugOutput - OutputDebugString输出的监视工具  
>>> CozyKxlol.Engine - 轻量级封装MonoGame游戏引擎  
>>> Starbound.Input - 对MonoGame的输入管理的封装  
>>> Starbound.UI - 对MonoGame的UI封装  
>>> Starbound.UI.XNA - 对MonoGame的UI封装  
>>> CozyKxlol.Network - 游戏网络通信库  
>>> CozyKxlol.Server - 游戏的服务端程序  
>>> CozyKxlol.ClientRobot - 游戏的客户端网络测试程序  
>>> CozyKxlol.MapEditor - tiled地图编辑器  
>>
>> CozyAnywhere - 远程控制工具  
>>
>> CozyBili - 获取B站生放送直播弹幕的工具  
>>> CozyBili.Core - B站弹幕获取  
>>> CozyBili.ConsoleExe - 命令行exe  
>>> CozyBili.WpfExe - wpf版exe  
>>
>> CozyLua - Lua相关的实践  
>>> CozyLua.Core - 封装执行lua的核心  
>>> CozyLua.Core.Tester - 核心测试  
>>> CozyLua.Runner - lua脚本执行器核心  
>>> CozyLua.Plugin - lua脚本执行器的插件接口  
>>> CozyLua.Plugin.WinFile - windows下文件操作插件  
>>> CozyLua.Plugin.WinReg - windows下注册表操作插件  
>>> CozyLua.Plugin.Tester - 插件测试  
>>> CozyLua.Runner.Console - 执行器的命令行exe  
>>
>> CozyMabi - Coding.Net的客户端（玩api，刷码币）  
>>> CozyMabi.Core - 核心api功能封装  
>>> CozyMabi.WpfExe - wpf版gui  
>>
>> CozyWallpaper - 换桌面壁纸的工具  
>>> CozyWallpaper.Core - 换壁纸核心功能  
>>> CozyWallpaper.Gui - wpf版gui  
>>
>> CozyWifi - 无线网卡放出wifi的工具  
>>
  
交流QQ群
--------------------------------
<b>373862115</b>

玩法一览
--------------------------------
加QQ群交流  
阅读别人的代码，改错和补充  
认领未完成的部分，就是干  
提出更好的玩法  

配套条件
--------------------------------
vs2013sp4以上（推荐社区版）  
  
windows7以上（推荐win8.1）  

书籍：  
zapline在用《C＃高级编程（第九版）》  
<http://item.jd.com/11611762.html>  
网上可以找到英文完整版  


编码风格
--------------------------------
4个空格缩进  
大括号单独占一行  
其它注意保持统一，开心就好  

仓库地址
--------------------------------
Coding(国内丶快！diff功能比OSC做得好）  
<https://coding.net/u/zapline/p/cozy/git>  
OSC(国内丶快！）  
<http://git.oschina.net/zapline/cozy>  
github  
<https://github.com/zpublic/cozy>  
