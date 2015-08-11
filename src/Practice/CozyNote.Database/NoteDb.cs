using CozyNote.Model.ObjectModel;
using LiteDB;
using System.IO;

namespace CozyNote.Database
{
    public class NoteDb
    {
        private LiteDatabase db;
        private LiteCollection<Note> col;

        public NoteDb()
        {
            Directory.CreateDirectory(@"c:\cozy_db");
            db = new LiteDatabase(@"c:\cozy_db\note.db");
            col = db.GetCollection<Note>("note");
        }

        public bool IsExist(int id)
        {
            return col.FindById(id) != null;
        }

        public int Create(Note obj)
        {
            var r = col.Insert(obj);
            return r.AsInt32;
        }

        public bool Delete(int id)
        {
            return col.Delete(id);
        }

        public bool Update(Note obj)
        {
            return col.Update(obj.id, obj);
        }

        public Note Get(int id)
        {
            return col.FindById(id);
        }
    }
}
