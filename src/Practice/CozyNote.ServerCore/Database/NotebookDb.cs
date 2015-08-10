using CozyNote.Model.ObjectModel;
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

        public bool IsExist(string name)
        {
            return col.FindOne(x => x.name == name) != null;
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

        public void DeleteNote(int id)
        {
            var notebook = col.Find(x => x.note_list.Contains(id));
            foreach(var obj in notebook)
            {
                obj.note_list.Remove(id);
                obj.notes_num--;
            }
        }

        public bool Update(Notebook obj)
        {
            return col.Update(obj.id, obj);
        }

        public Notebook Get(int id)
        {
            return col.FindById(id);
        }

        public Notebook Get(string name)
        {
            return col.FindOne(x => x.name == name);
        }
    }
}
