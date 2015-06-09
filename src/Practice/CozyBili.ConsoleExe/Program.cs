using System;
using CozyBili.Core;
using CozyBili.Core.Models;

namespace CozyBili.ConsoleExe {

    class Program {

        static void Main(string[] args) {
            
            args = args.Length > 0 ? args : new[] { "1029" };

            const string title = "CozyBili";
            int roomId;
            if (args.Length > 0 && int.TryParse(args[0], out roomId)) {
                var biliLive = new LiveDanMu(roomId);
                biliLive.OnlineNumChanged += x => Console.Title = string.Format("{0} - {1}号房间 - 当前在线人数{2}", title, roomId, x);
                biliLive.ReceiveDanMu += ShowDanMu;

                biliLive.Run();
            }
        }

        static void ShowDanMu(DanMuModel danMuModel) {
            Console.Write(danMuModel.Time.ToString("HH:mm:ss"));
            Console.ForegroundColor = ConsoleColor.Red;
            Console.Write(danMuModel.UserName + ":");
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine(danMuModel.Content);
            Console.ForegroundColor = ConsoleColor.Gray;

        }
    }
}
