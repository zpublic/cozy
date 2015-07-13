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

namespace CozyAnywhere.ClientCore
{
    public partial class AnywhereClient
    {
        private Dictionary<string, Action<string>> ResponseActions = new Dictionary<string, Action<string>>();

        public void InitClientMessage()
        {
            MessageReader.RegisterType<CommandMessageRsp>(MessageId.CommandMessageRsp);
            MessageReader.RegisterType<PluginQueryMessage>(MessageId.PluginQueryMessage);
        }

        public void RegisterResponseActions()
        {
            ResponseActions["FileCopy"]         = new Action<string>(OnFileCopyResponse);
            ResponseActions["FileDelete"]       = new Action<string>(OnFileDeleteResponse);
            ResponseActions["FileEnum"]         = new Action<string>(OnFileEnumResponse);
            ResponseActions["FileGetLength"]    = new Action<string>(OnFileGetLengthResponse);
            ResponseActions["FileGetTimes"]     = new Action<string>(OnFileGetTimesResponse);
            ResponseActions["FileIsDirectory"]  = new Action<string>(OnFileIsDirectoryResponse);
            ResponseActions["FileMove"]         = new Action<string>(OnFileMoveResponse);
            ResponseActions["FilePathExist"]    = new Action<string>(OnFilePathExistResponse);

            ResponseActions["ProcessEnum"]      = new Action<string>(OnProcessEnum);
            ResponseActions["ProcessTerminate"] = new Action<string>(OnProcessTerminate);
        }

        private void OnCommandMessageRsp(IMessage msg)
        {
            var rspMsg  = (CommandMessageRsp)msg;
            var name    = rspMsg.MethodName;
            if (ResponseActions.ContainsKey(name))
            {
                ResponseActions[name](rspMsg.CommandRsp);
            }
        }

        private void OnPluginQueryMessage(IMessage msg)
        {
            var rspMsg = (PluginQueryMessage)msg;
            if(PluginNameCollection != null)
            {
                PluginNameCollection.Clear();
                foreach(var obj in rspMsg.Plugins)
                {
                    PluginNameCollection.Add(obj);
                }
                PluginChangedHandler(this, new PluginChangedEvnetArgs());
            }
        }

        #region ResponseActions

        private void OnFileCopyResponse(string rsp)
        {
            var result = JsonConvert.DeserializeObject<bool>(rsp);
        }

        private void OnFileDeleteResponse(string rsp)
        {
            var result = JsonConvert.DeserializeObject<bool>(rsp);
        }

        private void OnFileEnumResponse(string rsp)
        {
            if(FileCollection != null)
            {
                FileCollection.Clear();
                var list = JsonConvert.DeserializeObject<List<WinFileModel>>(rsp);
                foreach (var obj in list)
                {
                    FileCollection.Add(Tuple.Create<string, bool>(obj.Name, obj.IsFolder));
                }
            }
        }

        private void OnFileGetLengthResponse(string rsp)
        {
            var result = JsonConvert.DeserializeObject<ulong>(rsp);
        }

        private void OnFileGetTimesResponse(string rsp)
        {
            var result = JsonConvert.DeserializeObject<WinFileTimeModel>(rsp);
        }

        private void OnFileIsDirectoryResponse(string rsp)
        {
            var result = JsonConvert.DeserializeObject<bool>(rsp);
        }

        private void OnFileMoveResponse(string rsp)
        {
            var result = JsonConvert.DeserializeObject<bool>(rsp);
        }

        private void OnFilePathExistResponse(string rsp)
        {
            var result = JsonConvert.DeserializeObject<bool>(rsp);
        }

        private void OnProcessEnum(string rsp)
        {
            var list = JsonConvert.DeserializeObject<List<WinProcessModel>>(rsp);
            if(ProcessCollection != null)
            {
                ProcessCollection.Clear();
                foreach (var obj in list)
                {
                    var process = Tuple.Create<uint, string>(obj.ProcessId, obj.Name);
                    ProcessCollection.Add(process);
                }
            }
        }

        private void OnProcessTerminate(string rsp)
        {
            var result = JsonConvert.DeserializeObject<bool>(rsp);
        }

        #endregion
    }
}