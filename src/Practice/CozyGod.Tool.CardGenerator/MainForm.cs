using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;
using Newtonsoft.Json;
using CozyGod.Model;
using CozyGod.Tool.CardGenerator.json;

namespace CozyGod.Tool.CardGenerator
{
    public partial class MainForm : Form
    {
        private string ImageDire { get; set; }

        private string CraftTablePath { get; set; }

        private string TranslatePath { get; set; }

        public MainForm()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if(folderBrowserDialog1.ShowDialog() == DialogResult.OK)
            {
                ImageDire = folderBrowserDialog1.SelectedPath;
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            openFileDialog1.Filter = @"text | *.txt";
            if (openFileDialog1.ShowDialog() == DialogResult.OK)
            {
                CraftTablePath = openFileDialog1.FileName;
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            openFileDialog1.Filter = @"json | *.json";
            if (openFileDialog1.ShowDialog() == DialogResult.OK)
            {
                TranslatePath = openFileDialog1.FileName;
            }
        }

        private void button4_Click(object sender, EventArgs e)
        {
            LoadFile();
            if(saveFileDialog1.ShowDialog() == DialogResult.OK)
            {
                using (var writer = new StreamWriter(saveFileDialog1.FileName))
                {
                    writer.Write(JsonConvert.SerializeObject(CardList));
                }
            }
        }

        private Dictionary<string, string> TranslateToEn { get; set; } 
            = new Dictionary<string, string>();
        private Dictionary<string, string> TranslateToCh { get; set; }
            = new Dictionary<string, string>();
        private List<List<string>> CraftTableCards { get; set; } =
            new List<List<string>>();
        private List<Card> CardList { get; set; } = new List<Card>();

        private void LoadFile()
        {
            Clear();
            LoadTranslateTable();
            ParseCraftTable();
            TryGenerateCard();
        }

        private void Clear()
        {
            TranslateToEn.Clear();
            TranslateToCh.Clear();
            CraftTableCards.Clear();
            CardList.Clear();
        }

        private void LoadTranslateTable()
        {
            using (var reader = new StreamReader(TranslatePath))
            {
                var obj = JsonConvert.DeserializeObject<TranslateObject>(reader.ReadToEnd());
                foreach (var trans in obj.Localization.String)
                {
                    TranslateToCh[trans.name] = trans.text;
                    TranslateToEn[trans.text] = trans.name;
                }
            }
        }

        private void ParseCraftTable()
        {
            string line = null;
            int currLevel = 0;
            using (var reader = new StreamReader(CraftTablePath))
            {
                while ((line = reader.ReadLine()) != null)
                {
                    if(line.Length > 0)
                    {
                        if (line[0] == '/' && line[1] == '/')
                        {
                            continue;
                        }
                        else if (line[0] == '-' && line[1] == '-')
                        {
                            currLevel++;
                            continue;
                        }
                        else
                        {
                            var ret = ParseLine(line);
                            if(currLevel <= CraftTableCards.Count)
                            {
                                CraftTableCards.Add(new List<string>());
                            }

                            CraftTableCards[currLevel].Add(ret.Item1);
                        }
                    }
                }
            }
        }

        private Tuple<string, string, string> ParseLine(string line)
        {
            var sb          = new StringBuilder();
            string[] rets   = new string[3];

            for(int i = 0; i < line.Length; ++i)
            {
                switch(line[i])
                {
                    case '=':
                        rets[0] = sb.ToString();
                        sb.Clear();
                        break;
                    case '+':
                        rets[1] = sb.ToString();
                        sb.Clear();
                        break;
                    default:
                        sb.Append(line[i]);
                        break;
                }
            }
            rets[2] = sb.ToString();
            return Tuple.Create(rets[0], rets[1], rets[2]);
        }

        private void TryGenerateCard()
        {
            for(int i = 0; i < CraftTableCards.Count; ++i)
            {
                // level i
                foreach(var card in CraftTableCards[i])
                {
                    if(TranslateToEn.ContainsKey(card))
                    {
                        var cn_name = card;
                        var name    = TranslateToEn[cn_name];
                        if (File.Exists(Path.Combine(ImageDire, name + @".bmp")))
                        {
                            CardList.Add(new Card()
                            {
                                CN_Name = cn_name,
                                Name    = name,
                                Level   = i + 1,
                            });
                        }
                    }
                }
            }
        }
    }
}
