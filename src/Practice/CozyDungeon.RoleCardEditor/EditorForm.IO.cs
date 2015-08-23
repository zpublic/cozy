using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Windows.Forms;
using System.Drawing.Imaging;
using Newtonsoft.Json;

namespace CozyDungeon.RoleCardEditor
{
    public partial class EditorForm
    {
        private Uri SaveFileName { get; set; }
        private void SaveData()
        {
            if (SaveFileName == null)
            {
                SaveFileDialog fileDig = new SaveFileDialog();
                fileDig.Filter = @"json | *.json";

                if (fileDig.ShowDialog() == DialogResult.OK)
                {
                    SaveFileName = new Uri(fileDig.FileName);
                }
            }

            if(SaveFileName != null)
            {

                var result = new List<List<int>>();
                for (int i = 0; i < CardLevels.Count; ++i)
                {
                    result.Add(new List<int>());
                }

                for(int i = 0; i < ListOfRoleCardList.Count; ++i)
                {
                    foreach(var obj in ListOfRoleCardList[i])
                    {
                        result[i].Add(obj.Id);
                    }
                }

                using (var fs = new FileStream(SaveFileName.AbsolutePath, FileMode.Create, FileAccess.ReadWrite))
                {
                    var json = JsonConvert.SerializeObject(result);
                    var data = Encoding.UTF8.GetBytes(json);
                    fs.Write(data, 0, data.Length);
                }

                SaveObject(SaveFileName);
                SaveImage(SaveFileName);

                IsModified  = false;
                this.Text   = "至强卡牌编辑器 - " + Path.GetFileNameWithoutExtension(SaveFileName.AbsolutePath);
            }
        }

        private void SaveObject(Uri filename)
        {
            var objName         = Path.GetFileNameWithoutExtension(filename.AbsolutePath);
            var JsonDireName    = Path.GetDirectoryName(filename.AbsolutePath) + @"\" + objName + @"_object\";
            Directory.CreateDirectory(JsonDireName);

            foreach (var list in ListOfRoleCardList)
            {
                foreach(var obj in list)
                {
                    var json = JsonConvert.SerializeObject(obj);
                    var jsonname = JsonDireName + obj.Id + ".json";

                    using (var fs = new FileStream(jsonname, FileMode.Create, FileAccess.ReadWrite))
                    {
                        var data = Encoding.UTF8.GetBytes(json);
                        fs.Write(data, 0, data.Length);
                    }
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
                    obj.Value.CardImage.Save(fs, ImageFormat.Png);
                }
            }
        }
    }
}
