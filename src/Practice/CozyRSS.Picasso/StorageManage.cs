using CozyRSS.Syndication.Model;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;

namespace CozyRSS.Picasso
{
    class StorageManage
    {
        string _Path;
        public void SetPath(string storagePath)
        {
            _Path = storagePath;
            if (!Directory.Exists(_Path))
            {
                Directory.CreateDirectory(_Path);
            }
        }

        public SyndicationFeed LoadFeed(string key)
        {
            try
            {
                SyndicationFeed feed;
                using (FileStream fs = new FileStream(Path.Combine(_Path, key + ".dat"), FileMode.Open))
                {
                    BinaryFormatter formatter = new BinaryFormatter();
                    feed = (SyndicationFeed)formatter.Deserialize(fs);
                    return feed;
                }
            }
            catch { }
            return null;
        }

        public bool SaveFeed(string key, SyndicationFeed feed)
        {
            try
            {
                using (FileStream fs = new FileStream(Path.Combine(_Path, key + ".dat"), FileMode.Create))
                {
                    BinaryFormatter formatter = new BinaryFormatter();
                    formatter.Serialize(fs, feed);
                    return true;
                }
            }
            catch { }
            return false;
        }
    }
}
