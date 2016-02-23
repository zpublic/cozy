using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SharpConfig;
using CozyLauncher.Infrastructure;
using System.IO;

namespace CozyLauncher.Core
{
    public class SettingObject
    {
        public static SettingObject Instance { get; set; } = new SettingObject();

        public static string ConfigFilePath
        {
            get
            {
                return PathTransform.LocalFullPath(@"./config.cfg");
            }
        }

        private Configuration ConfigObject { get; set; }

        public void Load()
        {
            try
            {
                ConfigObject = Configuration.LoadFromFile(ConfigFilePath);
            }
            catch (Exception)
            {
                ConfigObject = new Configuration();
            }
        }

        public void Save()
        {
            if(ConfigObject != null)
            {
                ConfigObject.SaveToFile(ConfigFilePath);
            }
        }

        public bool Get<T>(string section, string name, out T value, T defaultValue = default(T))
        {
            try
            {
                value = ConfigObject[section][name].GetValueTyped<T>();
            }
            catch(Exception)
            {
                value = defaultValue;
                return false;
            }

            return true;
        }

        public void Set<T>(string section, string name, T value)
        {
            ConfigObject[section][name].SetValue(value);
        }
    }
}
