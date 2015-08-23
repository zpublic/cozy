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
using System.Drawing;

namespace CozyDungeon.RoleCardEditor
{
    public partial class EditorForm
    {
        private Uri SaveFileName { get; set; }

        private void LoadData()
        {
            OpenFileDialog fileDlg = new OpenFileDialog();
            fileDlg.Filter = @"json | *.json";

            if(fileDlg.ShowDialog() == DialogResult.OK)
            {
                SaveFileName = new Uri(fileDlg.FileName);
            }

            if (SaveFileName != null)
            {
                var objName         = Path.GetFileNameWithoutExtension(SaveFileName.AbsolutePath);
                var JsonDireName    = Path.GetDirectoryName(SaveFileName.AbsolutePath) + @"\" + objName + @"_object\";
                var ImageDireName = Path.GetDirectoryName(SaveFileName.AbsolutePath) + @"\" + objName + @"_image\";
                var SelectedImageDireName = Path.GetDirectoryName(SaveFileName.AbsolutePath) + @"\" + objName + @"_selected_image\";

                var result          = new List<List<int>>();
                for (int i = 0; i < CardLevels.Count; ++i)
                {
                    result.Add(new List<int>());
                }

                using (var fs = new FileStream(SaveFileName.AbsolutePath, FileMode.Open, FileAccess.Read))
                {
                    using (var reader = new StreamReader(fs, Encoding.UTF8))
                    {
                        var json = reader.ReadToEnd();
                        result = JsonConvert.DeserializeObject<List<List<int>>>(json);
                    }
                }

                for(int i = 0; i < result.Count; ++i)
                {
                    foreach(var obj in result[i])
                    {
                        var jsonname = JsonDireName + obj + ".json";
                        using (var fs = new FileStream(jsonname, FileMode.Open, FileAccess.Read))
                        {
                            using (var reader = new StreamReader(fs, Encoding.UTF8))
                            {
                                var json = reader.ReadToEnd();
                                var card = JsonConvert.DeserializeObject<RoleCard>(json);
                                ListOfRoleCardList[i].Add(card);
                            }
                        }

                        CozyCardImage Img = new CozyCardImage();
                        var imagename = ImageDireName + obj + ".png";
                        using (var fs = new FileStream(imagename, FileMode.Open, FileAccess.Read))
                        {
                            Img.CardImage = Image.FromStream(fs);
                        }
                        var selectedimagename = SelectedImageDireName + obj + ".png";
                        using (var fs = new FileStream(selectedimagename, FileMode.Open, FileAccess.Read))
                        {
                            Img.SelectedImage = Image.FromStream(fs);
                        }
                        CardImageDictionary[obj] = Img;
                    }
                }
            }
        }

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
                SaveSelectedImage(SaveFileName);

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

        private void SaveSelectedImage(Uri filename)
        {
            var objName = Path.GetFileNameWithoutExtension(filename.AbsolutePath);
            var SelectedImageDireName = Path.GetDirectoryName(filename.AbsolutePath) + @"\" + objName + @"_selected_image\";
            Directory.CreateDirectory(SelectedImageDireName);

            foreach (var obj in CardImageDictionary)
            {
                var selectedimagename = SelectedImageDireName + obj.Key + ".png";
                using (var fs = new FileStream(selectedimagename, FileMode.Create, FileAccess.ReadWrite))
                {
                    obj.Value.SelectedImage.Save(fs, ImageFormat.Png);
                }
            }
        }
    }
}
