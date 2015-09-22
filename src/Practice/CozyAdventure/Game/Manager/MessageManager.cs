using System.Collections.Generic;

namespace Cozy.Game.Manager
{
    /// <summary>
    /// 消息管理器
    /// 使用的时候先注册消息,需要调用的时候直接Send即可
    /// </summary>
    internal class MessageManager
    {
        internal class CHandler
        {
            internal Handler0 m_hander0;
            internal Handler1 m_hander1;
            internal Handler2 m_hander2;
            internal Handler3 m_hander3;

            internal delegate void Handler0();

            internal delegate void Handler1(object _obj);

            internal delegate void Handler2(object _obj1, object _obj2);

            internal delegate void Handler3(object _obj1, object _obj2, object _obj3);
        }

        static private Dictionary<string, CHandler> m_eventDic = new Dictionary<string, CHandler>();

        #region 注册事件

        internal static void RegisterMessage(string _eventStr, CHandler.Handler0 _handler0)
        {
            if (!m_eventDic.ContainsKey(_eventStr))
            {
                CHandler _handler = new CHandler();
                m_eventDic.Add(_eventStr, _handler);
            }

            m_eventDic[_eventStr].m_hander0 -= _handler0;
            m_eventDic[_eventStr].m_hander0 += _handler0;
        }

        internal static void RegisterMessage(string _eventStr, CHandler.Handler1 _handler1)
        {
            if (!m_eventDic.ContainsKey(_eventStr))
            {
                CHandler _handler = new CHandler();
                m_eventDic.Add(_eventStr, _handler);
            }

            m_eventDic[_eventStr].m_hander1 -= _handler1;
            m_eventDic[_eventStr].m_hander1 += _handler1;
        }

        internal static void RegisterMessage(string _eventStr, CHandler.Handler2 _handler2)
        {
            if (!m_eventDic.ContainsKey(_eventStr))
            {
                CHandler _handler = new CHandler();
                m_eventDic.Add(_eventStr, _handler);
            }

            m_eventDic[_eventStr].m_hander2 -= _handler2;
            m_eventDic[_eventStr].m_hander2 += _handler2;
        }

        internal static void RegisterMessage(string _eventStr, CHandler.Handler3 _handler3)
        {
            if (!m_eventDic.ContainsKey(_eventStr))
            {
                CHandler _handler = new CHandler();
                m_eventDic.Add(_eventStr, _handler);
            }

            m_eventDic[_eventStr].m_hander3 -= _handler3;
            m_eventDic[_eventStr].m_hander3 += _handler3;
        }

        #endregion 注册事件

        #region 反注册

        internal static void UnRegisterMessage(string _eventStr, CHandler.Handler0 _handler0)
        {
            if (!m_eventDic.ContainsKey(_eventStr))
            {
                return;
            }

            m_eventDic[_eventStr].m_hander0 -= _handler0;
        }

        internal static void UnRegisterMessage(string _eventStr, CHandler.Handler1 _handler1)
        {
            if (!m_eventDic.ContainsKey(_eventStr))
            {
                return;
            }

            m_eventDic[_eventStr].m_hander1 -= _handler1;
        }

        internal static void UnRegisterMessage(string _eventStr, CHandler.Handler2 _handler2)
        {
            if (!m_eventDic.ContainsKey(_eventStr))
            {
                return;
            }

            m_eventDic[_eventStr].m_hander2 -= _handler2;
        }

        internal static void UnRegisterMessage(string _eventStr, CHandler.Handler3 _handler3)
        {
            if (!m_eventDic.ContainsKey(_eventStr))
            {
                return;
            }

            m_eventDic[_eventStr].m_hander3 -= _handler3;
        }

        #endregion 反注册

        #region Send

        internal static bool SendMessage(string _eventStr)
        {
            if (!m_eventDic.ContainsKey(_eventStr))
            {
                return false;
            }

            CHandler _handler = m_eventDic[_eventStr];

            if (_handler.m_hander0 != null)
            {
                _handler.m_hander0();

                return true;
            }

            return false;
        }

        internal static bool SendMessage(string _eventStr, object _obj1)
        {
            if (!m_eventDic.ContainsKey(_eventStr))
            {
                return false;
            }

            CHandler _handler = m_eventDic[_eventStr];

            if (_handler.m_hander1 != null)
            {
                _handler.m_hander1(_obj1);

                return true;
            }

            return false;
        }

        internal static bool SendMessage(string _eventStr, object _obj1, object _obj2)
        {
            if (!m_eventDic.ContainsKey(_eventStr))
            {
                return false;
            }

            CHandler _handler = m_eventDic[_eventStr];

            if (_handler.m_hander2 != null)
            {
                _handler.m_hander2(_obj1, _obj2);

                return true;
            }

            return false;
        }

        internal static bool SendMessage(string _eventStr, object _obj1, object _obj2, object _obj3)
        {
            if (!m_eventDic.ContainsKey(_eventStr))
            {
                return false;
            }

            CHandler _handler = m_eventDic[_eventStr];

            if (_handler.m_hander3 != null)
            {
                _handler.m_hander3(_obj1, _obj2, _obj3);

                return true;
            }

            return false;
        }

        #endregion Send
    }
}