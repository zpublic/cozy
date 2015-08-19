namespace CozyNote.WinformClient.DAL {

    public static class Factory {

        public static INoteDAL GetNoteDAL(DALType dalType = DALType.Defualt) {
            switch (dalType) {
                case DALType.HttpAPI:
                    return new HttpApi.NoteDAL();
                case DALType.Native:
                    return new Native.NoteDAL();
                default:
                    return new Native.NoteDAL();
            }
        }

        public static IUserDAL GetUserDAL(DALType dalType = DALType.Defualt) {
            switch (dalType) {
                case DALType.HttpAPI:
                    return new HttpApi.UserDAL();
                case DALType.Native:
                    return new Native.UserDAL();
                default:
                    return new Native.UserDAL();
            }
        }

    }
}
