namespace CozyNote.ClientCore.Api
{
    class ApiDef
    {
        public static string Host = "https://114.215.134.101:23333";

        public static string User = Host + "/user";
        public static string UserCreate = User + "/create";

        public static string Notebook = Host + "/notebook";

        public static string Note = Host + "/note";
    }
}
