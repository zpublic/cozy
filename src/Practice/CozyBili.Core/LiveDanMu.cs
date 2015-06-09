using System;
using System.IO;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;

namespace CozyBili.Core {

    public class LiveDanMu {

        #region property

        private bool Coonetced { get; set; }
        /// <summary>
        /// 设置或获取是否可以断线重连
        /// </summary>
        public bool OffLineReCoonetced { get; set; }
        private int Port { get; set; }
        private string ChatServe { get; set; }
        private NetworkStream NetStream { get; set; }
        private TcpClient Client { get; set; }
        private int RoomId { get; set; }

        #endregion

        #region events

        /// <summary>
        /// 在线人数通知时触发该事件
        /// </summary>
        public event Action<int> OnlineNumChanged;

        /// <summary>
        /// 接收直播实时弹幕
        /// </summary>
        public event Action<DanMuModel> ReceiveDanMu;

        #endregion

        public LiveDanMu(int roomId) {
            this.RoomId = roomId;
            Init();
        }

        private void Init() {
            //暂时写死一些数据，后面用配置文件加载
            Coonetced = false;
            OffLineReCoonetced = true;
            Port = 88;
            ChatServe = "livecmt.bilibili.com";
        }

        /// <summary>
        /// Go!!  给老子跑起来
        /// </summary>
        public void Run() {
            new Thread(() => {
                while (OffLineReCoonetced && ListenLoop()) {
                    //这里并没有什么卵用，仅仅只为了做到断线重连的效果
                }
            }).Start();
        }

        /// <summary>
        /// Stop
        /// </summary>
        public void Stop() {
            OffLineReCoonetced = false;
            Disconnect();
        }

        private bool ListenLoop() {
            Client = new TcpClient();
            Client.Connect(ChatServe, Port);
            NetStream = Client.GetStream();
            if (SendJoinChannel(RoomId)) {
                Coonetced = true;
                try {
                    byte[] array = new byte[Client.ReceiveBufferSize];
                    var num = BitConverter.ToInt16(array, 0);
                    while (Coonetced) {
                        NetStream.Read(array, 0, 2);
                        num = IPAddress.NetworkToHostOrder(num);
                        switch (num) {
                            case 1: {
                                    NetStream.Read(array, 0, 4);
                                    var onlineNum = IPAddress.NetworkToHostOrder(BitConverter.ToInt32(array, 0));
                                    TirggerOnLineNum(onlineNum);
                                    break;
                                }
                            case 2:
                            case 4: {
                                    NetStream.Read(array, 0, 2);
                                    var size = (short)(IPAddress.NetworkToHostOrder(BitConverter.ToInt16(array, 0)) - 4);
                                    var array2 = new byte[size];
                                    NetStream.Read(array2, 0, size);
                                    var danMuMsg = Encoding.UTF8.GetString(array2, 0, size);
                                    TirdderReceiveDanMu(DanMuModel.CreateModel(danMuMsg));
                                    break;
                                }
                            case 8:
                                NetStream.Read(array, 0, 2);
                                break;
                            default:
                                return true;
                        }
                    }
                }
                catch (Exception) {
                    Console.WriteLine("断线重连中..........");
                    return true;

                }
                finally {
                    this.Disconnect();
                }
            }
            Console.WriteLine("断线重连中..........");
            return true;
        }

        private bool SendJoinChannel(int channelId) {
            var array = new byte[12];
            using (var memoryStream = new MemoryStream(array)) {
                var buffer = BitConverter.GetBytes(16842764).ToBE();
                memoryStream.Write(buffer, 0, 4);
                buffer = BitConverter.GetBytes(channelId).ToBE();
                memoryStream.Write(buffer, 0, 4);
                buffer = BitConverter.GetBytes(0).ToBE();
                memoryStream.Write(buffer, 0, 4);
                NetStream.WriteAsync(array, 0, array.Length);
                NetStream.FlushAsync();
            }
            return true;
        }

        private void Disconnect() {
            this.Coonetced = false;
            try {
                this.Client.Close();
            }
            catch (Exception ex) {
                throw ex;
            }
            this.NetStream = null;
        }

        private void TirggerOnLineNum(int onLineNum) {
            if (OnlineNumChanged != null) {
                OnlineNumChanged(onLineNum);
            }
        }

        private void TirdderReceiveDanMu(DanMuModel danMuModel) {
            if (ReceiveDanMu != null) {
                ReceiveDanMu(danMuModel);
            }
        }
    }
}
