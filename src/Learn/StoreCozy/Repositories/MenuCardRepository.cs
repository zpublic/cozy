using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using StoreCozy.Model;

namespace StoreCozy.Repositories
{
    class MenuCardRepository
    {
        private ICollection<MenuCard> cards;
        public ICollection<MenuCard> Cards
        {
            get
            {
                return cards;
            }
        }
        public void InitMenuCards(IEnumerable<MenuCard> menuCards)
        {
            cards = new ObservableCollection<MenuCard>(menuCards);
        }
        private MenuCardRepository()
        {

        }

        public static MenuCardRepository Instance = new MenuCardRepository();

        public static ObservableCollection<MenuCard> GetSampleMenuCards()
        {
            Uri baseUri = new Uri("ms-appx:///");

            var cards = new ObservableCollection<MenuCard>();
            var card1 = new MenuCard
            {
                Title = "Breakfast"
            };
            card1.MenuItems.Add(new MenuItem
            {
                Text = "Spezialfrühstück",
                Price = 5.4,
                MenuCard = card1
            });
            card1.MenuItems.Add(new MenuItem
            {
                Text = "Wiener Frühstück",
                Price = 4.4,
                MenuCard = card1
            });
            card1.MenuItems.Add(new MenuItem
            {
                Text = "Schinken mit 3 Eiern",
                Price = 4.4,
                MenuCard = card1
            });
            card1.ImagePath = string.Format("{0}{1}", baseUri, "Assets/Breakfast.jpg");
            cards.Add(card1);

            var card2 = new MenuCard
            {
                Title = "Salad"
            };
            card2.MenuItems.Add(new MenuItem
            {
                Text = "Mariniertes Gemüse mit Schafkäsebällchen und Rohschinken",
                Price = 8.60,
                MenuCard = card2
            });
            card2.MenuItems.Add(new MenuItem
            {
                Text = "Gebratener Schafkäse im Speckhemd auf knackigem Salat",
                Price = 7.80,
                MenuCard = card2
            });
            card2.MenuItems.Add(new MenuItem
            {
                Text = "Backhendlsalat",
                Price = 7.20,
                MenuCard = card2
            });
            card2.ImagePath = string.Format("{0}{1}", baseUri, "Assets/Salads.jpg");
            cards.Add(card2);

            var card3 = new MenuCard
            {
                Title = "Sweets"
            };
            card3.MenuItems.Add(new MenuItem
            {
                Text = "Marillenknödel mit Zimtbrösel (Stück)",
                Price = 2.30,
                MenuCard = card3
            });
            card3.MenuItems.Add(new MenuItem
            {
                Text = "Apfelstrudel",
                Price = 2.60,
                MenuCard = card3
            });
            card3.MenuItems.Add(new MenuItem
            {
                Text = "Marmelade-Palatschinken (2 Stück)",
                Price = 3.40,
                MenuCard = card3
            });
            card3.ImagePath = string.Format("{0}{1}", baseUri, "Assets/Sweets.png");
            cards.Add(card3);
            return cards;
        }
    }
}
