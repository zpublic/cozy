using CozyNote.WinformClient.Models;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;

namespace CozyNote.WinformClient.DAL {

    public interface INoteDAL {

        List<NoteModel> GetAll();

        NoteModel Get(Guid Id);

        List<NoteModel> Query(Expression<Func<NoteModel, bool>> func);

        Guid Insert(NoteModel model);

        bool Update(NoteModel model);

        bool Delete(Guid Id);
    }
}
