using System;

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
    }
}
