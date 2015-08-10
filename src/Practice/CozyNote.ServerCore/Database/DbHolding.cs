namespace CozyNote.ServerCore.Database
{
    public class DbHolding
    {
        public static readonly UserDb User = new UserDb();
        public static readonly NotebookDb Notebook = new NotebookDb();
        public static readonly NoteDb Note = new NoteDb();
    }
}
