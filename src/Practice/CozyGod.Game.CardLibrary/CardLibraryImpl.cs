using CozyGod.Game.Interface;
using System;
using System.Collections.Generic;
using System.IO;
using Newtonsoft.Json;
using CozyGod.Game.Model;

namespace CozyGod.Game.CardLibrary
{
    public class CardLibraryImpl : ICardLibrary
    {
        private const int maxLevel = 15;

        private Model.CardLibrary LibInstance { get; set; }
            = new Model.CardLibrary(maxLevel);

        private Dictionary<string, string> TranslateToChTable { get; set; }
        = new Dictionary<string, string>();
        private Dictionary<string, string> TranslateToEnTable { get; set; }
        = new Dictionary<string, string>();

        public CardLibraryImpl(string filename = null)
        {
            if(filename != null)
            {
                ReadCardList(filename);
            }
        }

        public void Add(Card c)
        {
            if(c.Level <= maxLevel)
            {
                if (LibInstance.Cards[c.Level] == null)
                {
                    LibInstance.Cards[c.Level] = new List<Card>();
                }

                LibInstance.Cards[c.Level].Add(c);
            }
        }

        public void Clear()
        {
            foreach(var list in LibInstance.Cards)
            {
                if(list != null)
                {
                    list.Clear();
                }
            }
        }

        public Model.CardLibrary Get()
        {
            return LibInstance;
        }

        public Card FindCardByName(string name)
        {
            foreach (var list in LibInstance.Cards)
            {
                if(list!= null)
                {
                    var result = list.Find(x => x.Name == name);
                    if (result != null)
                    {
                        return result;
                    }
                }
            }
            return null;
        }

        public void ReadCardList(string filename)
        {
            using (var reader = new StreamReader(filename))
            {
                var list = JsonConvert.DeserializeObject<List<Card>>(reader.ReadToEnd());

                foreach(var card in list)
                {
                    Add(card);
                }
            }
        }

        public void ReadTranslateTable(string filename)
        {
            using (var reader = new StreamReader(filename))
            {
                TranslateToChTable = JsonConvert.DeserializeObject<Dictionary<string, string>>(reader.ReadToEnd());
                foreach (var trans in TranslateToChTable)
                {
                    TranslateToEnTable[trans.Value] = trans.Key;
                }
            }
        }

        public string TranslateToCh(string name)
        {
            if(TranslateToChTable.ContainsKey(name))
            {
                return TranslateToChTable[name];
            }
            return null;
        }

        public string TranslateToEn(string name)
        {
            if (TranslateToEnTable.ContainsKey(name))
            {
                return TranslateToEnTable[name];
            }
            return null;
        }
    }
}
