using LiteDB;

namespace CozyBored.Server
{
    public class DbContent
    {
        private LiteDatabase liteDatabase;
        private static DbContent instance;

        private DbContent()
        {
            liteDatabase = new LiteDatabase("CozyBored.db");
        }

        public static DbContent GetInstance()
        {
            instance = instance ?? new DbContent();
            return instance;
        }

        public LiteCollection<T> GetTable<T>() where T : new()
        {
            return liteDatabase.GetCollection<T>(nameof(T));
        }
    }
}
