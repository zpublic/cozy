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
using CozyGod.Game.Model.json;
using CozyGod.Game.Model;

namespace CozyGod.Tool.CardGenerator
{
    public partial class MainForm : Form
    {
        private string ImageDire { get; set; }

        private string CraftTablePath { get; set; }

        private string TranslatePath { get; set; }

        private string OutputPath { get; set; }

        private Image LevelBackground { get; set; }

        private Image BorderBackground { get; set; }

        private Size CardSize { get; set; } = new Size(96, 96);

        private Point LevelPos { get; set; } = new Point(8, 8);

        private Size LevelSize { get; set; } = new Size(24, 24);

        public MainForm()
        {
            InitializeComponent();
        }

        private void SelectImageDireButton_Click(object sender, EventArgs e)
        {
            if(folderBrowserDialog1.ShowDialog() == DialogResult.OK)
            {
                ImageDire           = folderBrowserDialog1.SelectedPath;
                ImagePathBox.Text   = ImageDire;
            }
        }

        private void SelectCraftTablePathButton_Click(object sender, EventArgs e)
        {
            openFileDialog1.Filter = @"text | *.txt";
            if (openFileDialog1.ShowDialog() == DialogResult.OK)
            {
                CraftTablePath      = openFileDialog1.FileName;
                CraftTableBox.Text  = CraftTablePath;
            }
        }

        private void SelectTranslateTablePathButton_Click(object sender, EventArgs e)
        {
            openFileDialog1.Filter = @"json | *.json";
            if (openFileDialog1.ShowDialog() == DialogResult.OK)
            {
                TranslatePath           = openFileDialog1.FileName;
                TranslateTableBox.Text  = TranslatePath;
            }
        }

        private void SelectOutputPathButton_Click(object sender, EventArgs e)
        {
            if (folderBrowserDialog1.ShowDialog() == DialogResult.OK)
            {
                OutputPath          = folderBrowserDialog1.SelectedPath;
                OutputPathBox.Text  = OutputPath;
            }
        }

        private void GenButton_Click(object sender, EventArgs e)
        {
            LoadFile();
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
            if(Check())
            {
                Clear();
                LoadTranslateTable();
                ParseCraftTable();
                TryGenerateCard();
                OutputToImage();
                SaveElementTable();
            }
        }

        private bool Check()
        {
            if (ImageDire == null || ImageDire.Length == 0) return false;
            if (CraftTablePath == null || CraftTablePath.Length == 0) return false;
            if (TranslatePath == null || TranslatePath.Length == 0) return false;
            if (OutputPath == null || OutputPath.Length == 0) return false;
            return true;
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
            string line     = null;
            int currLevel   = 0;
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

        private void OutputToImage()
        {
            foreach(var card in CardList)
            {
                var imgPath = Path.Combine(ImageDire, card.Name + ".bmp");
                if (File.Exists(imgPath))
                {
                    using (var ifs = new FileStream(imgPath, FileMode.Open))
                    {
                        var im      = Image.FromStream(ifs);
                        var output  = DrawCard(card, im);
                        output.Save(Path.Combine(OutputPath, card.Name + ".bmp"));
                    }
                }
            }
        }

        private Image DrawCard(Card card, Image img)
        {
            var b = new Bitmap(CardSize.Width, CardSize.Height);
            using (var g = Graphics.FromImage(b))
            {
                if(BorderBackground != null)
                {
                    g.DrawImage(BorderBackground, 
                        new Rectangle(Point.Empty, CardSize), 
                        new Rectangle(Point.Empty, BorderBackground.Size), 
                        GraphicsUnit.Pixel);
                }

                var offsetSize  = (CardSize - img.Size);
                int offset      = offsetSize.Width / 2;
                int fontSize    = Math.Abs(offsetSize.Width - offsetSize.Height) - 2;
                fontSize        = fontSize > 2 ? fontSize : 2;

                var CardFont    = new Font("微软雅黑", fontSize);

                g.DrawImage(img,
                    new Rectangle(new Point(offset, offset), img.Size),
                    new Rectangle(Point.Empty, img.Size),
                        GraphicsUnit.Pixel);

                SizeF sizeText = g.MeasureString(card.CN_Name, CardFont);

                g.DrawString(card.CN_Name,
                    CardFont,
                    SystemBrushes.ControlText,
                    (CardSize.Width - sizeText.Width) / 2,
                    offset + img.Height);

                if(LevelBackground != null)
                {
                    g.DrawImage(LevelBackground,
                    new Rectangle(LevelPos, LevelSize),
                    new Rectangle(Point.Empty, LevelBackground.Size),
                    GraphicsUnit.Pixel);

                    var levelFont   = new Font("微软雅黑", 10, FontStyle.Bold);
                    var levelStr    = card.Level.ToString();
                    var sizeLevel   = g.MeasureString(levelStr, levelFont);

                    g.DrawString(levelStr,
                        levelFont,
                        Brushes.WhiteSmoke,
                        LevelPos.X + (LevelSize.Width - sizeLevel.Width) / 2,
                        LevelPos.Y + (LevelSize.Height - sizeLevel.Height) / 2);
                }
            }
            return b;
        }

        private void SaveElementTable()
        {
            if (saveFileDialog1.ShowDialog() == DialogResult.OK)
            {
                using (var writer = new StreamWriter(saveFileDialog1.FileName))
                {
                    writer.Write(JsonConvert.SerializeObject(CardList));
                }
            }
        }

        private void SelectLevelBackgroundButton_Click(object sender, EventArgs e)
        {
            openFileDialog1.Filter =  @"图片|*.jpg;*.png;*.bmp";
            if(openFileDialog1.ShowDialog() == DialogResult.OK)
            {
                LevelBackgroundBox.Text = openFileDialog1.FileName;
                LevelBackground         = Image.FromFile(openFileDialog1.FileName);
            }
        }

        private void SelectBorderButton_Click(object sender, EventArgs e)
        {
            openFileDialog1.Filter = @"图片|*.jpg;*.png;*.bmp";
            if (openFileDialog1.ShowDialog() == DialogResult.OK)
            {
                BorderBackgroundBox.Text    = openFileDialog1.FileName;
                BorderBackground            = Image.FromFile(openFileDialog1.FileName);
            }
        }
    }
}
