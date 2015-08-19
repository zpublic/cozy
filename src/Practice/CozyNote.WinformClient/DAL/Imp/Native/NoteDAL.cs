using System;
using System.Collections.Generic;
using System.Linq;
using CozyNote.WinformClient.Models;
using System.Linq.Expressions;

namespace CozyNote.WinformClient.DAL.Native {

    public class NoteDAL : DALNativeBase<NoteModel>, INoteDAL {

        public bool Delete(Guid Id) {
            return Table.Delete(Id);
        }

        public NoteModel Get(Guid Id) {
            return Table.FindById(Id);
        }

        public List<NoteModel> GetAll() {
            return Table.FindAll().ToList();
        }

        public Guid Insert(NoteModel model) {
            return Table.Insert(model).AsGuid;
        }

        public List<NoteModel> Query(Expression<Func<NoteModel, bool>> func) {
            return Table.Find(func).ToList();
        }

        public bool Update(NoteModel model) {
            return Table.Update(model);
        }
    }
}
