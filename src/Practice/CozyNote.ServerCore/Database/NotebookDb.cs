using CozyNote.ServerCore.Model;
using LiteDB;
using System.IO;

namespace CozyNote.ServerCore.Database
{
    public class NotebookDb
    {
        private LiteDatabase db;
        private LiteCollection<Notebook> col;

        public NotebookDb()
        {
            Directory.CreateDirectory(@"c:\cozy_db");
            db = new LiteDatabase(@"c:\cozy_db\notebook.db");
            col = db.GetCollection<Notebook>("notebook");
        }

        public bool IsExist(int id)
        {
            return col.FindById(id) != null;
        }

        public int Create(Notebook obj)
        {
            var r = col.Insert(obj);
            return r.AsInt32;
        }

        public bool Delete(int id)
        {
            return col.Delete(id);
        }

        public bool Update(Notebook obj)
        {
            return col.Update(obj.id, obj);
        }

        public Notebook Get(int id)
        {
            return col.FindById(id);
        }
    }
}
