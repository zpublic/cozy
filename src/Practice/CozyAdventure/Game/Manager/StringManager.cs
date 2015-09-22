using CocosSharp;
using System.Collections.Generic;
using System.IO;

namespace CozyAdventure.Game.Manager
{
    internal class StringManager
    {
        private const string __FILEPATH = "StringText.txt";

        private class CStringText
        {
            internal string m_key;
            internal string m_str;
        }

        private static Dictionary<string, CStringText> m_dic = new Dictionary<string, CStringText>();

        internal static void Init()
        {
            LoadFile();
        }

        private static void LoadFile()
        {
            Stream _stream = CCFileUtils.GetFileStream(__FILEPATH);

            StreamReader _sr = new StreamReader(_stream);

            List<string[]> _strList = new List<string[]>();

            // 剔除注释
            string _str = _sr.ReadLine();

            while (_str != null)
            {
                string[] _list = ParseStr(_str);

                if (_list != null)
                {
                    // 合法字符入表
                    _strList.Add(_list);
                }

                _str = _sr.ReadLine();
            }

            // 根据表入dic
            if (_strList.Count > 0)
            {
                m_dic.Clear();

                int _count = _strList.Count;

                for (int i = 0; i < _count; i++)
                {
                    int _j = 0;

                    CStringText _text = new CStringText();

                    // Key
                    _text.m_key = _strList[i][_j];

                    _j++;

                    _text.m_str = _strList[i][_j];

                    if (!m_dic.ContainsKey(_text.m_key))
                    {
                        m_dic.Add(_text.m_key, _text);
                    }
                    else
                    {
                        // 报错
                    }
                }
            }
            else
            {
                // 报错
            }
        }

        /// <summary>
        /// 传入原字符串解析
        /// </summary>
        /// <param name="_line"></param>
        /// <returns></returns>
        static private string[] ParseStr(string _line)
        {
            if (_line[0] != '/' &&
                _line[1] != '/')
            {
                string[] _list = _line.Split('\t');

                return _list;
            }

            return null;
        }

        /// <summary>
        /// 防止转义
        /// </summary>
        /// <param name="_str"></param>
        /// <returns></returns>
        private static string ParseLineBreak(string _str)
        {
            return _str = _str.Replace(@"\n", "\n");
        }

        /// <summary>
        /// 传入Key获取字符串
        /// </summary>
        /// <param name="_str"></param>
        /// <returns></returns>
        internal static string GetText(string _str)
        {
            CStringText _strText = null;
            m_dic.TryGetValue(_str, out _strText);

            if (_strText == null)
            {
                return _str;
            }
            // 空
            else if (_strText.m_str.Trim().Length == 0)
            {
                return _str;
            }
            else
            {
                return ParseLineBreak(_strText.m_str);
            }
        }
    }
}