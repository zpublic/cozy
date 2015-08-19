using CozyNote.WinformClient.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CozyNote.WinformClient.DAL {

    public interface IUserDAL {

        UserModel GetUser(Guid Id);

        Guid Insert(UserModel model);

        bool Update(UserModel model);

        bool Delete(Guid Id);
    }
}
