using CozyAdventure.ServerPlugin.Model;
using LiteDB;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CozyAdventure.ServerPlugin.Storgae
{
    public class PlayerFollowerDB
    {
        private LiteDatabase db;
        private LiteCollection<PlayerFollowerInfo> col;

        public PlayerFollowerDB()
        {
            db = new LiteDatabase(@"playerfollower.db");
            col = db.GetCollection<PlayerFollowerInfo>("playerfollower");
        }

        public bool IsPlayerExist(int playerid)
        {
            return col.FindOne(x => x.PlayerId == playerid) != null;
        }

        public bool IsExist(int id)
        {
            return col.FindById(id) != null;
        }

        public int Create(PlayerFollowerInfo user)
        {
            var r = col.Insert(user);
            return r.AsInt32;
        }

        public bool Delete(int id)
        {
            return col.Delete(id);
        }

        public bool Update(PlayerFollowerInfo obj)
        {
            return col.Update(obj.id, obj);
        }

        public PlayerFollowerInfo Get(int id)
        {
            return col.FindById(id);
        }

        public PlayerFollowerInfo GetPlayerFollower(int playerid)
        {
            return col.FindOne(x => x.PlayerId == playerid);
        }
    }
}
