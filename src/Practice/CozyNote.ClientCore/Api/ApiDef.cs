namespace CozyNote.ClientCore.Api
{
    public static class ApiDef
    {
        public static string DebugHost      = @"http://127.0.0.1:23333/";
        public static string Host           = DebugHost;

        // UserApi
        public static string User           = Host + "/user";
        public static string UserCreate     = User + "/create";
        public static string UserNotebook   = User + "/notebook";
        public static string UserUpdate     = User + "/update";

        // NotebookApi
        public static string Notebook       = Host + "/notebook";
        public static string NotebookAll    = Notebook + "/all";
        public static string NotebookGet    = Notebook + "/get";
        public static string NotebookList   = Notebook + "/list";
        public static string NotebookUpdate = Notebook + "/update";
        public static string NotebookCreate = Notebook + "/create";
        public static string NotebookDelete = Notebook + "/delete";

        // NoteApi
        public static string Note           = Host + "/note";
        public static string NoteCreate     = Note + "/create";
        public static string NoteGet        = Note + "/get";
        public static string NoteUpdate     = Note + "/update";
        public static string NoteMove       = Note + "/move";
        public static string NoteDelete     = Note + "/delete";
    }
}
