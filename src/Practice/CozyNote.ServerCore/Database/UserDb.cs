using CozyNote.ServerCore.Model;
using LiteDB;
using System.IO;

namespace CozyNote.ServerCore.Database
{
    public class UserDb
    {
        private LiteDatabase db;
        private LiteCollection<User> col;

        public UserDb()
        {
            Directory.CreateDirectory(@"c:\cozy_db");
            db = new LiteDatabase(@"c:\cozy_db\user.db");
            col = db.GetCollection<User>("user");
        }

        public bool IsExist(int id)
        {
            return col.FindById(id) != null;
        }

        public int Create(User obj)
        {
            var r = col.Insert(obj);
            return r.AsInt32;
        }

        public bool Delete(int id)
        {
            return col.Delete(id);
        }

        public bool Update(User obj)
        {
            return col.Update(obj.id, obj);
        }
    }
}
