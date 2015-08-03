using CozyAnywhere.Protocol.Messages;

namespace CozyAnywhere.ClientCore
{
    public partial class AnywhereClient
    {
        public void SendFileEnumMessage(string path) // FileEnum
        {
            if (server != null)
            {
                var parms   = new object[] { path, false, false, };
                var command = CommandMaker.Invoke("FileEnum", parms);
                if (command != null)
                {
                    var commandMsg = new CommandMessage()
                    {
                        Command = command,
                    };
                    server.SendMessage(commandMsg);
                }
            }
        }

        public void SendProcessEnumMessage() // ProcessEnum
        {
            if (server != null)
            {
                var command = CommandMaker.Invoke("ProcessEnum", null);
                if (command != null)
                {
                    var commandMsg = new CommandMessage()
                    {
                        Command = command,
                    };
                    server.SendMessage(commandMsg);
                }
            }
        }

        public void SendFileDeleteMessage(string path) // FileDelete
        {
            if (server != null)
            {
                var parms   = new object[] { path };
                var command = CommandMaker.Invoke("FileDelete", parms);
                if (command != null)
                {
                    var commandMsg = new CommandMessage()
                    {
                        Command = command,
                    };
                    server.SendMessage(commandMsg);
                }
            }
        }

        public void SendProcessTerminateMessage(uint pid) // ProcessTerminate
        {
            if (server != null)
            {
                var parms   = new object[] { pid };
                var command = CommandMaker.Invoke("ProcessTerminate", parms);
                if (command != null)
                {
                    var commandMsg = new CommandMessage()
                    {
                        Command = command,
                    };
                    server.SendMessage(commandMsg);
                }
            }
        }

        public void SendGetCaptureDataMessage() // GetCaptureData
        {
            if (server != null)
            {
                var command = CommandMaker.Invoke("GetCaptureData", null);
                if (command != null)
                {
                    var commandMsg = new CommandMessage()
                    {
                        Command = command,
                    };
                    server.SendMessage(commandMsg);
                }
            }
        }
    }
}
