using System;
using CozyAnywhere.Protocol;
using CozyAnywhere.Protocol.Messages;
using NetworkHelper;
using NetworkProtocol;
using Newtonsoft.Json;
using System.Collections.Generic;
using CozyAnywhere.Plugin.WinFile.Model;

namespace CozyAnywhere.ClientCore
{
    public partial class AnywhereClient
    {
        private Dictionary<string, Action<string>> ResponseActions = new Dictionary<string, Action<string>>();

        public void InitClientMessage()
        {
            MessageReader.RegisterType<FileEnumMessageRsp>(MessageId.FileEnumMessageRsp);
            MessageReader.RegisterType<CommandMessageRsp>(MessageId.CommandMessageRsp);
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

        private void OnFileCopyResponse(string rsp)
        {

        }

        private void OnFileDeleteResponse(string rsp)
        {

        }

        private void OnFileEnumResponse(string rsp)
        {
            FileCollection.Clear();
            var list = JsonConvert.DeserializeObject<List<WinFileModel>>(rsp);
            foreach(var obj in list)
            {
                FileCollection.Add(Tuple.Create<string, bool>(obj.Name, obj.IsFolder));
            }
        }

        private void OnFileGetLengthResponse(string rsp)
        {

        }

        private void OnFileGetTimesResponse(string rsp)
        {

        }

        private void OnFileIsDirectoryResponse(string rsp)
        {

        }

        private void OnFileMoveResponse(string rsp)
        {

        }

        private void OnFilePathExistResponse(string rsp)
        {

        }

        private void OnFileEnumMessageRsp(IMessage msg)
        {
            var rspMsg = (FileEnumMessageRsp)msg;
            if(FileCollection != null)
            {
                foreach(var obj in rspMsg.FileInfoList)
                {
                    var file = Tuple.Create<string, bool>(obj.Item1, obj.Item3);
                    FileCollection.Add(file);
                }
            }
        }
    }
}