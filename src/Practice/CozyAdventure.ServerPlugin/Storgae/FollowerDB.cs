using CozyAdventure.ServerPlugin.Model;
using LiteDB;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CozyAdventure.ServerPlugin.Storgae
{
    public class FollowerDB
    {
        private LiteDatabase db;
        private LiteCollection<FollowerInfo> col;

        public FollowerDB()
        {
            db = new LiteDatabase(@"follower.db");
            col = db.GetCollection<FollowerInfo>("follower");
        }

        public bool IsExist(int objId)
        {
            return col.FindOne(x => x.ObjectID == objId) != null;
        }

        public int Create(FollowerInfo user)
        {
            var r = col.Insert(user);
            return r.AsInt32;
        }

        public bool Delete(int id)
        {
            return col.Delete(id);
        }

        public bool Update(FollowerInfo obj)
        {
            return col.Update(obj.id, obj);
        }

        public FollowerInfo Get(int id)
        {
            return col.FindById(id);
        }

        public int GetWithObjectId(int id)
        {
            return col.FindOne(x => x.ObjectID == id).id;
        }
    }
}
