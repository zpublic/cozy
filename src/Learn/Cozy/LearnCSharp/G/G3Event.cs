using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace Cozy.LearnCSharp.G
{
    // 定义消息
    public class CozyInfoEventArgs : EventArgs
    {
        public CozyInfoEventArgs(string str)
        {
            CozyInfo = str;
        }

        // 事件内容
        public string CozyInfo
        {
            get;
            private set;
        }
    }

    // 定义事件发布者
    class CozyDealer
    {
        public event EventHandler<CozyInfoEventArgs> CozyInfoListener;

        // 创建新事件
        public void NewCozyEvent(string str)
        {
            if (CozyInfoListener != null)
            {
                CozyInfoListener(this, new CozyInfoEventArgs(str));
            }
        }

    }

    // 定义事件监听器
    class CozyConsumer
    {
        private string name;

        public CozyConsumer(string name)
        {
            this.name = name;
        }

        // 事件回调
        public void CozyInfoCallBack(object sender, CozyInfoEventArgs msg)
        {
            Console.WriteLine("{0} Message: {1}", name, msg.CozyInfo);
        }
    }

    // 定义弱事件管理器
    class WeakCozyEventManager : WeakEventManager
    {
        // 连接发布程序
        public static void AddListener(object source, IWeakEventListener listener)
        {
            CurrentManager.ProtectedAddListener(source, listener);
        }

        // 断开与发布程序的连接
        public static void RemoveListener(object source, IWeakEventListener listener)
        {
            CurrentManager.ProtectedRemoveListener(source, listener);
        }

        // 实现单例
        public static WeakCozyEventManager CurrentManager
        {
            get
            {
                WeakCozyEventManager manager = GetCurrentManager(typeof(WeakCozyEventManager)) as WeakCozyEventManager;
                if(manager == null)
                {
                    manager = new WeakCozyEventManager();
                    SetCurrentManager(typeof(WeakCozyEventManager), manager);
                }
                return manager;
            }
        }

        protected override void StartListening(object source)
        {
            (source as CozyDealer).CozyInfoListener += CozyDealer_NewCozyEvent;
        }

        void CozyDealer_NewCozyEvent(object sender, CozyInfoEventArgs msg)
        {
            DeliverEvent(sender, msg);
        }

        protected override void StopListening(object source)
        {
            (source as CozyDealer).CozyInfoListener -= CozyDealer_NewCozyEvent;
        }
    }

    // 弱事件监听器
    class WeakCozyConsumer : IWeakEventListener
    {
        private string name;

        public WeakCozyConsumer(string name)
        {
            this.name = name;
        }

        // 事件回调
        public void CozyInfoCallBack(object sender, CozyInfoEventArgs msg)
        {
            Console.WriteLine("Weak {0} Message: {1}", name, msg.CozyInfo);
        }

        bool IWeakEventListener.ReceiveWeakEvent(Type managerType, object sender, EventArgs e)
        {
            CozyInfoCallBack(sender, e as CozyInfoEventArgs);
            return true;
        }
    }

    class G3Event
    {
        public static void Cozy()
        {
            Console.WriteLine("\n-----------------------------------------------");
            Console.WriteLine(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType.FullName);
            Console.WriteLine("-----------------------------------------------");

            Event_Listener();
            Weak_Events();
        }

        public static void Event_Listener()
        {
            CozyDealer dealer = new CozyDealer();

            CozyConsumer consumerZ = new CozyConsumer("zapline");
            dealer.CozyInfoListener += consumerZ.CozyInfoCallBack;      // zapline订阅事件
            dealer.NewCozyEvent("hello");

            CozyConsumer consumerK = new CozyConsumer("kingwl");
            dealer.CozyInfoListener += consumerK.CozyInfoCallBack;      // kingwl订阅事件
            dealer.NewCozyEvent("hi");

            dealer.CozyInfoListener -= consumerZ.CozyInfoCallBack;      // zapline取消订阅
            dealer.NewCozyEvent("hehe");
        }

        public static void Weak_Events()
        {
            CozyDealer dealer = new CozyDealer();

            WeakCozyConsumer consumerZ = new WeakCozyConsumer("zapline");
            WeakCozyEventManager.AddListener(dealer, consumerZ);
            dealer.NewCozyEvent("hello");

            WeakCozyConsumer consumerK = new WeakCozyConsumer("kingwl");
            WeakCozyEventManager.AddListener(dealer, consumerK);
            dealer.NewCozyEvent("hi");

            WeakCozyEventManager.RemoveListener(dealer, consumerZ);
            dealer.NewCozyEvent("hehe");
        }
    }
}
