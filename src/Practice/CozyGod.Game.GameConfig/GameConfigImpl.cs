using CozyGod.Game.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using System.IO;
using CozyGod.Game.GameConfig.ConfigAttribute;
using System.Reflection;

namespace CozyGod.Game.GameConfig
{
    public enum StringConfigEnum
    {
        ContentPath
    }

    public enum IntegerConfigEnum
    {
        // Nothing
    }

    public class GameConfigImpl : IGameConfig
    {
        public ConfigObject ConfigObj { get; set; }

        private Dictionary<Type, Dictionary<string, PropertyInfo>> GameConfigDict { get; set; }
            = new Dictionary<Type, Dictionary<string, PropertyInfo>>();

        public GameConfigImpl()
        {
            var configType      = typeof(ConfigObject);
            var pros            = configType.GetProperties(BindingFlags.Public | BindingFlags.Instance);

            foreach(var pro in pros)
            {
                CozyConfigAttribute att = pro.GetCustomAttribute<CozyConfigAttribute>();
                if (att != null)
                {
                    if(!GameConfigDict.ContainsKey(pro.PropertyType) || GameConfigDict[pro.PropertyType] == null)
                    {
                        GameConfigDict[pro.PropertyType] = new Dictionary<string, PropertyInfo>();
                    }

                    GameConfigDict[pro.PropertyType][pro.Name] = pro;
                }
            }
        }

        public string GetContentPath()
        {
            if (ConfigObj.ContentPath != null)
            {
                return ConfigObj.ContentPath;
            }
            return null;
        }

        public bool TryGetStringConfig(StringConfigEnum name, out string result)
        {
            return TryGetConfig(name.ToString(), out result);
        }

        public bool TryGetIntegerConfig(IntegerConfigEnum name, out int result)
        {
            return TryGetConfig(name.ToString(), out result);
        }

        public bool TryGetConfig<T>(string name, out T output)
        {
            output = default(T);

            if (GameConfigDict.ContainsKey(typeof(T)))
            {
                var typeDict = GameConfigDict[typeof(T)];
                if(typeDict.ContainsKey(name))
                {
                    output = (T)typeDict[name].GetMethod.Invoke(ConfigObj, null);
                    return true;
                }
            }
            return false;
        }

        public bool TrySetConfig<T>(string name, T value)
        {
            if (GameConfigDict.ContainsKey(typeof(T)))
            {
                var typeDict = GameConfigDict[typeof(T)];
                if (typeDict.ContainsKey(name))
                {
                    typeDict[name].SetMethod.Invoke(ConfigObj, new object[] { value });
                    return true;
                }
            }
            return false;
        }

        public void Init()
        {
            string filepath = "./CozyGod.json";
            ReadFile(filepath);
        }

        private void ReadFile(string filepath)
        {
            if(!File.Exists(filepath))
            {
                using (var writer = new StreamWriter(filepath))
                {
                    ConfigObj = new ConfigObject();
                    writer.Write(JsonConvert.SerializeObject(ConfigObj));
                }
            }

            using (var reader = new StreamReader(filepath))
            {
                ConfigObj = JsonConvert.DeserializeObject<ConfigObject>(reader.ReadToEnd());
            }
        }
    }
}
