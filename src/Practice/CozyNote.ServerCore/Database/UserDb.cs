using CozyNote.Model.ObjectModel;
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
            if (!Directory.Exists(@"cozy_db\")) {
                Directory.CreateDirectory(@"cozy_db\");
            }
            db = new LiteDatabase(@"cozy_db\user.db");
            col = db.GetCollection<User>("user");
        }

        public bool IsExist(string nickname)
        {
            return col.FindOne(x => x.nickname == nickname) != null;
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

        public bool Delete(string nickname)
        {
            User u = Get(nickname);
            if (u != null)
            {
                return col.Delete(u.id);
            }
            return false;
        }

        public bool Update(User obj)
        {
            return col.Update(obj.id, obj);
        }

        public User Get(int id)
        {
            return col.FindById(id);
        }

        public User Get(string nickname)
        {
            return col.FindOne(x => x.nickname == nickname);
        }
    }
}
