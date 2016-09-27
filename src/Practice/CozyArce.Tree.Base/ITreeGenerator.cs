using CozyArce.Tree.Base.Model;

namespace CozyArce.Tree.Base
{
    public interface ITreeGenerator
    {
        // 假设是1000*1000的画板
        CozyTree Generate();
    }
}
