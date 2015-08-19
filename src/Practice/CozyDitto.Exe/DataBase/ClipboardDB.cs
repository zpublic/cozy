using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LiteDB;

namespace CozyDitto.Exe.DataBase
{
    public class ClipboardDB
    {
        private LiteDatabase db;
        private LiteCollection<ClipboardRecord> col;

        private static readonly ClipboardDB instance = new ClipboardDB();
        public static ClipboardDB Instance
        {
            get
            {
                return instance;
            }
        }

        public ClipboardDB()
        {
            db = new LiteDatabase(@"clipboard.db");
            col = db.GetCollection<ClipboardRecord>("clipboard");
        }

        public bool IsExist(int id)
        {
            return col.FindById(id) != null;
        }

        public int Create(ClipboardRecord obj)
        {
            var r = col.Insert(obj);
            return r.AsInt32;
        }

        public bool Delete(int id)
        {
            return col.Delete(id);
        }

        public bool Update(ClipboardRecord obj)
        {
            return col.Update(obj.id, obj);
        }

        public ClipboardRecord Get(int id)
        {
            return col.FindById(id);
        }

        public IEnumerable<ClipboardRecord> GetAll()
        {
            return col.FindAll();
        }
    }
}
