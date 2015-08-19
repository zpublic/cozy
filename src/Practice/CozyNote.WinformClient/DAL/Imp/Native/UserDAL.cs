using System;
using CozyNote.WinformClient.Models;

namespace CozyNote.WinformClient.DAL.Native {

    public class UserDAL : DALNativeBase<UserModel>, IUserDAL {

        public bool Delete(Guid Id) {
            return Table.Delete(Id);
        }

        public UserModel GetUser(Guid Id) {
            return Table.FindById(Id);
        }

        public Guid Insert(UserModel model) {
            return Table.Insert(model);
        }

        public bool Update(UserModel model) {
            return Table.Update(model);
        }
    }
}
