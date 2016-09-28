using CozyArce.Tree.Shared.Model;

namespace CozyArce.Tree.Shared
{
    public interface ITreeGenerator
    {
        // 假设是1000*1000的画板
        CozyTree Generate();
    }
}
