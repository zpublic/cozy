using CozyNote.Database;
using CozyNote.Model.ObjectModel;
using System.Collections.Generic;

namespace CozyNote.ServerTester
{
    class Program
    {
        static void Main(string[] args)
        {
            User u = new User();
            u.nickname = "zapline";
            u.notebook_list = new List<int> { 11, 12, 13 };

            UserDb db = new UserDb();

            bool b = db.IsExist("100");

            int id = db.Create(u);

            u.nickname = "lulu";
            b = db.Update(u);

            b = db.Delete(u.id);
        }
    }
}
