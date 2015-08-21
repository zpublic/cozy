using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Windows.Forms;
using System.Drawing.Imaging;
using Newtonsoft.Json;
using CozyDungeon.Game.Component.Card.Model;

namespace CozyDungeon.RoleCardEditor
{
    public partial class EditorForm
    {
        private List<RoleCard> CardList { get; set; } = new List<RoleCard>();

        private void SaveData()
        {
            SaveFileDialog fileDig  = new SaveFileDialog();
            fileDig.Filter          = @"json | *.json";

            if (fileDig.ShowDialog() == DialogResult.OK)
            {
                var filename                = new Uri(fileDig.FileName);
                List<List<int>> savejson    = new List<List<int>>();

                for (int i = 0; i < CardLevels.Count; ++i)
                {
                    savejson.Add(new List<int>());
                }

                foreach (var obj in CardList)
                {
                    savejson[(int)obj.Level].Add(obj.Id);
                }

                using (var fs = new FileStream(filename.AbsolutePath, FileMode.Create, FileAccess.ReadWrite))
                {
                    var json = JsonConvert.SerializeObject(savejson);
                    var data = Encoding.UTF8.GetBytes(json);
                    fs.Write(data, 0, data.Length);
                }

                SaveObject(filename);
                SaveImage(filename);
            }
        }

        private void SaveObject(Uri filename)
        {
            var objName         = Path.GetFileNameWithoutExtension(filename.AbsolutePath);
            var JsonDireName    = Path.GetDirectoryName(filename.AbsolutePath) + @"\" + objName + @"_object\";
            Directory.CreateDirectory(JsonDireName);

            foreach (var obj in CardList)
            {
                var json        = JsonConvert.SerializeObject(obj);
                var jsonname    = JsonDireName + obj.Id + ".json";

                using (var fs = new FileStream(jsonname, FileMode.Create, FileAccess.ReadWrite))
                {
                    var data = Encoding.UTF8.GetBytes(json);
                    fs.Write(data, 0, data.Length);
                }
            }
        }

        private void SaveImage(Uri filename)
        {
            var objName         = Path.GetFileNameWithoutExtension(filename.AbsolutePath);
            var ImageDireName   = Path.GetDirectoryName(filename.AbsolutePath) + @"\" + objName + @"_image\";
            Directory.CreateDirectory(ImageDireName);

            foreach (var obj in CardImageDictionary)
            {
                var imagename = ImageDireName + obj.Key + ".png";
                using (var fs = new FileStream(imagename, FileMode.Create, FileAccess.ReadWrite))
                {
                    obj.Value.Save(fs, ImageFormat.Png);
                }
            }
        }
    }
}
