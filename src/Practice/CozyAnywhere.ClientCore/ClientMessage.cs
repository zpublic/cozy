using System;
using CozyAnywhere.Protocol.Messages;
using NetworkHelper;
using NetworkProtocol;
using Newtonsoft.Json;
using System.Collections.Generic;
using CozyAnywhere.Plugin.WinFile.Model;
using CozyAnywhere.Plugin.WinProcess.Model;
using CozyAnywhere.Plugin.WinCapture;
using CozyAnywhere.ClientCore.EventArg;
using System.Linq;
using Lidgren.Network;

namespace CozyAnywhere.ClientCore
{
    public partial class AnywhereClient
    {
        private Dictionary<string, Action<CommandMessageRsp>> ResponseActions = new Dictionary<string, Action<CommandMessageRsp>>();

        public void InitClientMessage()
        {
            var asm = "CozyAnywhere.Protocol";
            var ns = "CozyAnywhere.Protocol.Messages";
            MessageReader.RegisterTypeWithAssembly(asm, ns);
            MessageCallbackInvoker.LoadMessage(asm, ns);
            RegisterCallback();
        }

        private void RegisterCallback()
        {
            MessageCallbackInvoker.RegisterCallback<CommandMessageRsp>(new Action<IMessage, NetConnection>(OnCommandMessageRsp));
            MessageCallbackInvoker.RegisterCallback<PluginQueryMessage>(new Action<IMessage, NetConnection>(OnPluginQueryMessage));
            MessageCallbackInvoker.RegisterCallback<BinaryPacketMessage>(new Action<IMessage, NetConnection>(OnBinaryPacketMessage));
            MessageCallbackInvoker.RegisterCallback<ConnectMessage>(new Action<IMessage, NetConnection>(OnConnectMessage));
            MessageCallbackInvoker.RegisterCallback<QueryConnectMessage>(new Action<IMessage, NetConnection>(OnConnectQueryMessage));
            MessageCallbackInvoker.RegisterCallback<QueryConnectMessageRsp>(new Action<IMessage, NetConnection>(OnConnectQueryMessageRsp));
        }

        public void RegisterResponseActions()
        {
            ResponseActions["FileCopy"]         = new Action<CommandMessageRsp>(OnFileCopyResponse);
            ResponseActions["FileDelete"]       = new Action<CommandMessageRsp>(OnFileDeleteResponse);
            ResponseActions["FileEnum"]         = new Action<CommandMessageRsp>(OnFileEnumResponse);
            ResponseActions["FileGetLength"]    = new Action<CommandMessageRsp>(OnFileGetLengthResponse);
            ResponseActions["FileGetTimes"]     = new Action<CommandMessageRsp>(OnFileGetTimesResponse);
            ResponseActions["FileIsDirectory"]  = new Action<CommandMessageRsp>(OnFileIsDirectoryResponse);
            ResponseActions["FileMove"]         = new Action<CommandMessageRsp>(OnFileMoveResponse);
            ResponseActions["FilePathExist"]    = new Action<CommandMessageRsp>(OnFilePathExistResponse);

            ResponseActions["ProcessEnum"]      = new Action<CommandMessageRsp>(OnProcessEnum);
            ResponseActions["ProcessTerminate"] = new Action<CommandMessageRsp>(OnProcessTerminate);
        }

        private void OnCommandMessageRsp(IMessage msg, NetConnection conn)
        {
            var rspMsg = (CommandMessageRsp)msg;
            var name = rspMsg.MethodName;
            if (ResponseActions.ContainsKey(name))
            {
                ResponseActions[name](rspMsg);
            }
        }

        private void OnPluginQueryMessage(IMessage msg, NetConnection conn)
        {
            var rspMsg = (PluginQueryMessage)msg;
            if (PluginNameCollection != null)
            {
                var except = rspMsg.Plugins.Except<string>(PluginNameCollection).ToList();
                PluginNameCollection = rspMsg.Plugins;
                if (PluginChangedHandler != null)
                {
                    PluginChangedHandler(this, new PluginChangedEvnetArgs(except));
                }
            }
        }

        private void OnBinaryPacketMessage(IMessage msg, NetConnection conn)
        {
            var rspMsg = (BinaryPacketMessage)msg;
            if (rspMsg.Data != null)
            {
                if (CaptureRefreshHandler != null)
                {
                    var meta    = JsonConvert.DeserializeObject<CaptureSplitMetaData>(rspMsg.MetaData);
                    var t       = Tuple.Create(meta.X, meta.Y, meta.Width, meta.Height);
                    CaptureRefreshHandler(this, new CaptureRefreshEventArgs(t, rspMsg.Data));
                }
            }
        }

        private void OnConnectMessage(IMessage msg, NetConnection conn)
        {
            var connMsg = (ConnectMessage)msg;

            var queryMsg = new QueryConnectMessage();
            server.SendMessage(queryMsg, conn);
        }

        private void OnConnectQueryMessage(IMessage msg, NetConnection conn)
        {
            var queryMsg = (QueryConnectMessage)msg;
            var rspMsg = new QueryConnectMessageRsp()
            {
                ConnectionType = QueryConnectMessageRsp.ServerType,
            };
            server.SendMessage(rspMsg, conn);
        }


        private void OnConnectQueryMessageRsp(IMessage msg, NetConnection conn)
        {
            var queryMsg = (QueryConnectMessageRsp)msg;
            if (queryMsg.ConnectionType != QueryConnectMessageRsp.ServerType)
            {
            }
        }

        #region ResponseActions

        private void OnFileCopyResponse(CommandMessageRsp rsp)
        {
            if (rsp.RspType == CommandMessageRsp.StringDataType)
            {
                var result = JsonConvert.DeserializeObject<bool>(rsp.StringCommandRsp);
            }
        }

        private void OnFileDeleteResponse(CommandMessageRsp rsp)
        {
            if (rsp.RspType == CommandMessageRsp.StringDataType)
            {
                var result = JsonConvert.DeserializeObject<bool>(rsp.StringCommandRsp);
            }
        }

        private void OnFileEnumResponse(CommandMessageRsp rsp)
        {
            if (rsp.RspType == CommandMessageRsp.StringDataType)
            {
                if (FileCollection != null)
                {
                    FileCollection.Clear();
                    var list = JsonConvert.DeserializeObject<List<WinFileModel>>(rsp.StringCommandRsp);
                    foreach (var obj in list)
                    {
                        FileCollection.Add(Tuple.Create<string, bool>(obj.Name, obj.IsFolder));
                    }
                }
            }

        }

        private void OnFileGetLengthResponse(CommandMessageRsp rsp)
        {
            if (rsp.RspType == CommandMessageRsp.StringDataType)
            {
                var result = JsonConvert.DeserializeObject<bool>(rsp.StringCommandRsp);
            }
        }

        private void OnFileGetTimesResponse(CommandMessageRsp rsp)
        {
            if (rsp.RspType == CommandMessageRsp.StringDataType)
            {
                var result = JsonConvert.DeserializeObject<bool>(rsp.StringCommandRsp);
            }
        }

        private void OnFileIsDirectoryResponse(CommandMessageRsp rsp)
        {
            if (rsp.RspType == CommandMessageRsp.StringDataType)
            {
                var result = JsonConvert.DeserializeObject<bool>(rsp.StringCommandRsp);
            }
        }

        private void OnFileMoveResponse(CommandMessageRsp rsp)
        {
            if (rsp.RspType == CommandMessageRsp.StringDataType)
            {
                var result = JsonConvert.DeserializeObject<bool>(rsp.StringCommandRsp);
            }
        }

        private void OnFilePathExistResponse(CommandMessageRsp rsp)
        {
            if (rsp.RspType == CommandMessageRsp.StringDataType)
            {
                var result = JsonConvert.DeserializeObject<bool>(rsp.StringCommandRsp);
            }
        }

        private void OnProcessEnum(CommandMessageRsp rsp)
        {
            if (rsp.RspType == CommandMessageRsp.StringDataType)
            {
                var list = JsonConvert.DeserializeObject<List<WinProcessModel>>(rsp.StringCommandRsp);
                if (ProcessCollection != null)
                {
                    ProcessCollection.Clear();
                    foreach (var obj in list)
                    {
                        var process = Tuple.Create<uint, string>(obj.ProcessId, obj.Name);
                        ProcessCollection.Add(process);
                    }
                }
            }
        }

        private void OnProcessTerminate(CommandMessageRsp rsp)
        {
            if (rsp.RspType == CommandMessageRsp.StringDataType)
            {
                var result = JsonConvert.DeserializeObject<bool>(rsp.StringCommandRsp);
            }
        }
        #endregion
    }
}