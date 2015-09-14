using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LiteDB;
using CozyAdventure.ServerPlugin.Model;

namespace CozyAdventure.ServerPlugin.Storgae
{
    public class UserDB
    {
        private LiteDatabase db;
        private LiteCollection<UserInfo> col;

        public UserDB()
        {
            db = new LiteDatabase(@"user.db");
            col = db.GetCollection<UserInfo>("user");
        }

        public bool IsExist(string name)
        {
            return col.FindOne(x => x.Name == name) != null;
        }

        public int Create(UserInfo user)
        {
            var r = col.Insert(user);
            return r.AsInt32;
        }

        public bool Delete(int id)
        {
            return col.Delete(id);
        }

        public bool Update(UserInfo obj)
        {
            return col.Update(obj.id, obj);
        }

        public UserInfo Get(int id)
        {
            return col.FindById(id);
        }

        public UserInfo Get(string name, string pass)
        {
            return col.FindOne(x => x.Name == name && x.Pass == pass);
        }

        public int GetId(string name)
        {
            var p = col.FindOne(x => x.Name == name);
            if (p != null)
            {
                return p.id;
            }
            return -1;
        }
    }
}
