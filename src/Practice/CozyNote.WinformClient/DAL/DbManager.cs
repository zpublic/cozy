using LiteDB;

namespace CozyNote.WinformClient.DAL {

    public class DbManager {

        private LiteDatabase liteDataBase;
        private static DbManager singleInstance;

        private DbManager() {
            liteDataBase = new LiteDatabase("CozyNotebook.db");
        }

        public static DbManager GetInstance() {
            if (singleInstance == null) {
                singleInstance = new DbManager();
            }
            return singleInstance;
        }

        public LiteCollection<T> GetTable<T>() where T : new() {
            return liteDataBase.GetCollection<T>(nameof(T));
        }
    }
}
