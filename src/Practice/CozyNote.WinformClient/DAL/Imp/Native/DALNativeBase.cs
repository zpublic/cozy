using LiteDB;

namespace CozyNote.WinformClient.DAL.Native {

    public class DALNativeBase<T> where T : new() {

        protected LiteCollection<T> Table {
            get {
                return DbManager.GetInstance().GetTable<T>();
            }
        }
    }
}
