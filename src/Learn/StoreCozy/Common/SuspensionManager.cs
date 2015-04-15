using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;
using Windows.ApplicationModel;
using Windows.Storage;
using Windows.Storage.Streams;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;

namespace StoreCozy.Common
{
    /// <summary>
    /// SuspensionManager 捕获全局会话状态以简化应用程序的
    /// 进程生命期管理。  请注意会话状态在许多条件下将自动清除，
    /// 因此应该只用于存储方便
    /// 在会话之间传递，但在应用程序崩溃时应放弃
    /// 升级时应丢弃的信息。
    /// </summary>
    internal sealed class SuspensionManager
    {
        private static Dictionary<string, object> _sessionState = new Dictionary<string, object>();
        private static List<Type> _knownTypes = new List<Type>();
        private const string sessionStateFilename = "_sessionState.xml";

        /// <summary>
        /// 提供对当前会话的全局会话状态的访问。  此状态
        /// 由 <see cref="SaveAsync"/> 序列化并由
        /// <see cref="RestoreAsync"/> 还原，因此这些值必须可由
        /// <see cref="DataContractSerializer"/> 序列化且应尽可能紧凑。  强烈建议使用
        /// 字符串和其他自包含数据类型。
        /// </summary>
        public static Dictionary<string, object> SessionState
        {
            get { return _sessionState; }
        }

        /// <summary>
        /// 读取和写入会话状态时向 <see cref="DataContractSerializer"/> 提供的
        /// 自定义类型的列表。  最初为空，可能会
        /// 添加其他类型以自定义序列化进程。
        /// </summary>
        public static List<Type> KnownTypes
        {
            get { return _knownTypes; }
        }

        /// <summary>
        /// 保存当前 <see cref="SessionState"/>。  任何 <see cref="Frame"/> 实例
        /// (已向 <see cref="RegisterFrame"/> 注册)都还将保留其当前的
        /// 导航堆栈，从而使其活动 <see cref="Page"/> 可以
        /// 保存其状态。
        /// </summary>
        /// <returns>反映会话状态保存时间的异步任务。</returns>
        public static async Task SaveAsync()
        {
            try
            {
                // 保存所有已注册框架的导航状态
                foreach (var weakFrameReference in _registeredFrames)
                {
                    Frame frame;
                    if (weakFrameReference.TryGetTarget(out frame))
                    {
                        SaveFrameNavigationState(frame);
                    }
                }

                // 以同步方式序列化会话状态以避免对共享
                // 状态
                MemoryStream sessionData = new MemoryStream();
                DataContractSerializer serializer = new DataContractSerializer(typeof(Dictionary<string, object>), _knownTypes);
                serializer.WriteObject(sessionData, _sessionState);

                // 获取 SessionState 文件的输出流并以异步方式写入状态
                StorageFile file = await ApplicationData.Current.LocalFolder.CreateFileAsync(sessionStateFilename, CreationCollisionOption.ReplaceExisting);
                using (Stream fileStream = await file.OpenStreamForWriteAsync())
                {
                    sessionData.Seek(0, SeekOrigin.Begin);
                    await sessionData.CopyToAsync(fileStream);
                }
            }
            catch (Exception e)
            {
                throw new SuspensionManagerException(e);
            }
        }

        /// <summary>
        /// 还原之前保存的 <see cref="SessionState"/>。  任何 <see cref="Frame"/> 实例
        /// (已向 <see cref="RegisterFrame"/> 注册)都还将还原其先前的导航
        /// 状态，从而使其活动 <see cref="Page"/> 可以还原其
        /// 状态。
        /// </summary>
        /// <param name="sessionBaseKey">标识会话类型的可选密钥。
        /// 这可用于区分多个应用程序启动方案。</param>
        /// <returns>反映何时读取会话状态的异步任务。
        /// 在此任务完成之前，不应依赖 <see cref="SessionState"/>
        /// 完成。</returns>
        public static async Task RestoreAsync(String sessionBaseKey = null)
        {
            _sessionState = new Dictionary<String, Object>();

            try
            {
                // 获取 SessionState 文件的输入流
                StorageFile file = await ApplicationData.Current.LocalFolder.GetFileAsync(sessionStateFilename);
                using (IInputStream inStream = await file.OpenSequentialReadAsync())
                {
                    // 反序列化会话状态
                    DataContractSerializer serializer = new DataContractSerializer(typeof(Dictionary<string, object>), _knownTypes);
                    _sessionState = (Dictionary<string, object>)serializer.ReadObject(inStream.AsStreamForRead());
                }

                // 将任何已注册框架还原为其已保存状态
                foreach (var weakFrameReference in _registeredFrames)
                {
                    Frame frame;
                    if (weakFrameReference.TryGetTarget(out frame) && (string)frame.GetValue(FrameSessionBaseKeyProperty) == sessionBaseKey)
                    {
                        frame.ClearValue(FrameSessionStateProperty);
                        RestoreFrameNavigationState(frame);
                    }
                }
            }
            catch (Exception e)
            {
                throw new SuspensionManagerException(e);
            }
        }

        private static DependencyProperty FrameSessionStateKeyProperty =
            DependencyProperty.RegisterAttached("_FrameSessionStateKey", typeof(String), typeof(SuspensionManager), null);
        private static DependencyProperty FrameSessionBaseKeyProperty =
            DependencyProperty.RegisterAttached("_FrameSessionBaseKeyParams", typeof(String), typeof(SuspensionManager), null);
        private static DependencyProperty FrameSessionStateProperty =
            DependencyProperty.RegisterAttached("_FrameSessionState", typeof(Dictionary<String, Object>), typeof(SuspensionManager), null);
        private static List<WeakReference<Frame>> _registeredFrames = new List<WeakReference<Frame>>();

        /// <summary>
        /// 注册 <see cref="Frame"/> 实例以允许将其导航历史记录保存到
        /// <see cref="SessionState"/> 并从中还原。  如果框架将参与会话状态管理，
        /// 则应在创建框架后立即注册。  在
        /// 注册时，如果已还原指定键的状态，
        /// 则将立即还原导航历史记录。
        /// <see cref="RestoreAsync"/> 还将还原导航历史记录。
        /// </summary>
        /// <param name="frame">其导航历史记录应由
        /// <see cref="SuspensionManager"/></param>
        /// <param name="sessionStateKey"><see cref="SessionState"/> 的唯一键，用于
        /// 存储与导航相关的信息。</param>
        /// <param name="sessionBaseKey">标识会话类型的可选密钥。
        /// 这可用于区分多个应用程序启动方案。</param>
        public static void RegisterFrame(Frame frame, String sessionStateKey, String sessionBaseKey = null)
        {
            if (frame.GetValue(FrameSessionStateKeyProperty) != null)
            {
                throw new InvalidOperationException("Frames can only be registered to one session state key");
            }

            if (frame.GetValue(FrameSessionStateProperty) != null)
            {
                throw new InvalidOperationException("Frames must be either be registered before accessing frame session state, or not registered at all");
            }

            if (!string.IsNullOrEmpty(sessionBaseKey))
            {
                frame.SetValue(FrameSessionBaseKeyProperty, sessionBaseKey);
                sessionStateKey = sessionBaseKey + "_" + sessionStateKey;
            }

            // 使用依赖项属性可会话键与框架相关联，并记录其
            // 导航状态应托管的框架
            frame.SetValue(FrameSessionStateKeyProperty, sessionStateKey);
            _registeredFrames.Add(new WeakReference<Frame>(frame));

            // 查看导航状态是否可还原
            RestoreFrameNavigationState(frame);
        }

        /// <summary>
        /// 解除之前由 <see cref="RegisterFrame"/> 注册的 <see cref="Frame"/>
        /// 与 <see cref="SessionState"/> 的关联。  之前捕获的任何导航状态都将
        /// 已移除。
        /// </summary>
        /// <param name="frame">其导航历史记录不应再
        /// 托管。</param>
        public static void UnregisterFrame(Frame frame)
        {
            // 移除会话状态并移除框架列表中其导航
            // 状态将被保存的框架(以及无法再访问的任何弱引用)
            SessionState.Remove((String)frame.GetValue(FrameSessionStateKeyProperty));
            _registeredFrames.RemoveAll((weakFrameReference) =>
            {
                Frame testFrame;
                return !weakFrameReference.TryGetTarget(out testFrame) || testFrame == frame;
            });
        }

        /// <summary>
        /// 为与指定的 <see cref="Frame"/> 相关联的会话状态提供存储。
        /// 之前已向 <see cref="RegisterFrame"/> 注册的框架已自动
        /// 保存其会话状态且还原为全局
        /// <see cref="SessionState"/> 的一部分。  未注册的框架具有
        /// 在还原已从导航缓存中丢弃的页面时仍然有用的
        /// 导航缓存。
        /// </summary>
        /// <remarks>应用程序可能决定依靠 <see cref="NavigationHelper"/> 管理
        /// 特定于页面的状态，而非直接使用框架会话状态。</remarks>
        /// <param name="frame">需要会话状态的实例。</param>
        /// <returns>状态集合受限于与
        /// <see cref="SessionState"/>。</returns>
        public static Dictionary<String, Object> SessionStateForFrame(Frame frame)
        {
            var frameState = (Dictionary<String, Object>)frame.GetValue(FrameSessionStateProperty);

            if (frameState == null)
            {
                var frameSessionKey = (String)frame.GetValue(FrameSessionStateKeyProperty);
                if (frameSessionKey != null)
                {
                    // 已注册框架反映相应的会话状态
                    if (!_sessionState.ContainsKey(frameSessionKey))
                    {
                        _sessionState[frameSessionKey] = new Dictionary<String, Object>();
                    }
                    frameState = (Dictionary<String, Object>)_sessionState[frameSessionKey];
                }
                else
                {
                    // 未注册框架具有瞬时状态
                    frameState = new Dictionary<String, Object>();
                }
                frame.SetValue(FrameSessionStateProperty, frameState);
            }
            return frameState;
        }

        private static void RestoreFrameNavigationState(Frame frame)
        {
            var frameState = SessionStateForFrame(frame);
            if (frameState.ContainsKey("Navigation"))
            {
                frame.SetNavigationState((String)frameState["Navigation"]);
            }
        }

        private static void SaveFrameNavigationState(Frame frame)
        {
            var frameState = SessionStateForFrame(frame);
            frameState["Navigation"] = frame.GetNavigationState();
        }
    }
    public class SuspensionManagerException : Exception
    {
        public SuspensionManagerException()
        {
        }

        public SuspensionManagerException(Exception e)
            : base("SuspensionManager failed", e)
        {

        }
    }
}
