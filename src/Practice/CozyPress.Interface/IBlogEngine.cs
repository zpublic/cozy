namespace CozyPress.Interface
{
    public interface IBlogEngine
    {
        void Init();
        void UnInit();

        IOperateBlog Blog();
    }
}
