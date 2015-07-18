using System;
using CozyAnywhere.Protocol;
using CozyAnywhere.Protocol.Messages;
using NetworkHelper;
using NetworkProtocol;
using Newtonsoft.Json;
using System.Collections.Generic;
using CozyAnywhere.Plugin.WinFile.Model;
using CozyAnywhere.Plugin.WinProcess.Model;
using CozyAnywhere.ClientCore.EventArg;
using System.Linq;
using CozyAnywhere.PluginBase;

namespace CozyAnywhere.ClientCore
{
    public partial class AnywhereClient
    {
        private Dictionary<string, Action<CommandMessageRsp>> ResponseActions = new Dictionary<string, Action<CommandMessageRsp>>();

        public void InitClientMessage()
        {
            MessageReader.RegisterType<CommandMessageRsp>(MessageId.CommandMessageRsp);
            MessageReader.RegisterType<PluginQueryMessage>(MessageId.PluginQueryMessage);
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

            ResponseActions["GetCaptureData"]   = new Action<CommandMessageRsp>(OnGetCaptureData);
        }

        private void OnCommandMessageRsp(IMessage msg)
        {
            var rspMsg = (CommandMessageRsp)msg;
            var name = rspMsg.MethodName;
            if (ResponseActions.ContainsKey(name))
            {
                ResponseActions[name](rspMsg);
            }
        }

        private void OnPluginQueryMessage(IMessage msg)
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

        private void OnGetCaptureData(CommandMessageRsp rsp)
        {
            if (rsp.RspType == CommandMessageRsp.BinaryDataType)
            {
                if (CaptureRefreshHandler != null)
                {
                    CaptureRefreshHandler(this, new CaptureRefreshEventArgs(rsp.BinaryCommandRsp));
                }
            }
        }
        #endregion
    }
}