CozyAnywhere源码结构
--------------------------------
>> CozyAnywhere - 远程控制工具
>>
>>> Client  
>>>> CozyAnywhere.ClientCore  
>>>> CozyAnywhere.WpfClient  
>>>
>>> Server  
>>>> CozyAnywhere.ServerCore  
>>>> CozyAnywhere.WpfServer  
>>>
>>> Common  
>>>> CozyAnywhere.Config  
>>>> CozyAnywhere.Protocol  
>>>> CozyAnywhere.PluginBase  
>>>> CozyAnywhere.PluginMgr  
>>>> Plugin  
>>>>> CozyAnywhere.Plugin.WinCapture  
>>>>> CozyAnywhere.Plugin.WinFile  
>>>>> CozyAnywhere.Plugin.WinKeyboard  
>>>>> CozyAnywhere.Plugin.WinMouse  
>>>>> CozyAnywhere.Plugin.WinProcess  
>>>
>>> Public  
>>>> Capture  
>>>> CaptureCpp  
>>>> Network  
>>>>> Lidgren.Network  
>>>>> NetworkProtocol  
>>>>> NetworkHelper  
>>>>> NetworkClient  
>>>>> NetworkServer  .
>>>>
>>>> Utils
>>>>> FileUtilCpp  
>>>>> KeyboardUtilCpp  
>>>>> MouseUtilCpp  
>>>>> ProcessUtilCpp  
>>>
>>> Test  
>>>> ConsoleClientTester  
>>>> ConsoleServerTester  
>>>> CaptureTester  
>>>> ConsoleCaptureTester  
>>>> ConsoleFileTester  
>>>> ConsoleKeyboardTester  
>>>> ConsoleMouseTester  
>>>> ConsoleProcessTester  
>>>> FileTester  
>>>> ProcessTester  
>>
