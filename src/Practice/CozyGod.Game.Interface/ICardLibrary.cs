using CozyGod.Model;

namespace CozyGod.Game.Interface
{
    public interface ICardLibrary
    {
        CardLibrary Get();
        void Add(Card c);
        void Clear();
    }
}
