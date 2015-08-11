using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CozyNote.Database;
using CozyNote.Model.ObjectModel;

namespace CozyNote.ServerCore
{
    public static class ModuleHelper
    {
        public static User GetUser(string username, string password)
        {
            if (DbHolding.User.IsExist(username))
            {
                var User = DbHolding.User.Get(username);
                if (User.pass == password)
                {
                    return User;
                }
            }
            return null;
        }

        public static Notebook GetNotebook(int id, string password)
        {
            if (DbHolding.Notebook.IsExist(id))
            {
                var notebook = DbHolding.Notebook.Get(id);

                if (notebook.pass == password)
                {
                    return notebook;
                }
            }
            return null;
        }

        public static Note GetNote(int notebookid, string notebookpass, int noteid)
        {
            var notebook = GetNotebook(notebookid, notebookpass);
            if (notebook != null)
            {
                if (notebook.note_list.Contains(noteid) && DbHolding.Note.IsExist(noteid))
                {
                    var note = DbHolding.Note.Get(noteid);
                    return note;
                }
            }
            return null;
        }
    }
}
