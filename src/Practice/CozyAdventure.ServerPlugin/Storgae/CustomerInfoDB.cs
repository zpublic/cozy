using CozyAdventure.ServerPlugin.Model;
using LiteDB;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CozyAdventure.ServerPlugin.Storgae
{
    public class CustomerInfoDB
    {
        private LiteDatabase db;
        private LiteCollection<CustomerInfo> col;

        public CustomerInfoDB()
        {
            db = new LiteDatabase(@"customer.db");
            col = db.GetCollection<CustomerInfo>("customer");
        }
        public bool IsPlayerExist(int playerid)
        {
            return col.FindOne(x => x.PlayerId == playerid) != null;
        }

        public bool IsExist(int id)
        {
            return col.FindById(id) != null;
        }

        public int Create(CustomerInfo user)
        {
            var r = col.Insert(user);
            return r.AsInt32;
        }

        public bool Delete(int id)
        {
            return col.Delete(id);
        }

        public bool Update(CustomerInfo obj)
        {
            return col.Update(obj.id, obj);
        }

        public CustomerInfo Get(int id)
        {
            return col.FindById(id);
        }

        public CustomerInfo GetPlayerFollower(int playerid)
        {
            return col.FindOne(x => x.PlayerId == playerid);
        }
    }
}
