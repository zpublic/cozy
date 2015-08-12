using System;

namespace CozyNote.WinformClient.Models {

    public class UserModel {
        public Guid Id { get; set; }
        public string UserName { get; set; }
        public string Password { get; set; }
    }
}
