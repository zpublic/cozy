namespace CozyPixel.Command
{
    public interface ICommand
    {
        void Do();
        void Undo();
    }
}
