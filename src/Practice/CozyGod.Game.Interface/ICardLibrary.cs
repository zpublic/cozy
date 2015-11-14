using CozyGod.Game.Model;

namespace CozyGod.Game.Interface
{
    public interface ICardLibrary
    {
        CardLibrary Get();
        void Add(Card c);
        void Clear();
        Card FindCardByName(string name);
        string TranslateToCh(string name);
        string TranslateToEn(string name);
    }
}
