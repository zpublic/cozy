using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using CozyNote.WinformClient.Models;

namespace CozyNote.WinformClient.DAL.HttpApi {

    public class NoteDAL : INoteDAL {

        public bool Delete(Guid Id) {
            throw new NotImplementedException();
        }

        public NoteModel Get(Guid Id) {
            throw new NotImplementedException();
        }

        public List<NoteModel> GetAll() {
            throw new NotImplementedException();
        }

        public Guid Insert(NoteModel model) {
            throw new NotImplementedException();
        }

        public List<NoteModel> Query(Expression<Func<NoteModel, bool>> func) {
            throw new NotImplementedException();
        }

        public bool Update(NoteModel model) {
            throw new NotImplementedException();
        }
    }
}
