using CozyNote.Model.ObjectModel;
using LiteDB;

namespace CozyNote.ServerCore.Database {

    public class DbContent {


        private static DbContent Instance;
        private LiteDatabase dbSession;

        private DbContent() {
            dbSession = new LiteDatabase("CozyNotebook.db");
        }

        private static LiteDatabase DbSession {
            get {
                if (Instance == null) {
                    Instance = new DbContent();
                }
                return Instance.dbSession;
            }
        }

        //public static LiteCollection<T> GetTable<T>() where T : new() {
        //    return Instance.dbSession.GetCollection<T>(nameof(T));
        //}

    }

}
