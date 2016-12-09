using System;
using System.IO;

namespace CozyHammer.MnistSvm
{
    public class MnistDataWriter
    {
        public bool Save(MnistDataLabelReader lr, MnistDataImageReader ir, string filePath)
        {
            try
            {
                FileStream fs = new FileStream(filePath, FileMode.Create);
                StreamWriter sw = new StreamWriter(fs);
                for (var i = 0; i < lr.ItemCount; ++i)
                {
                    string line = lr.Items[i].ToString() + " ";
                    for (var j = 0; j < ir.Items[i].Length; ++j)
                    {
                        line += j.ToString() + ":" + ir.Items[i][j].ToString() + " ";
                    }
                    sw.WriteLine(line);
                }
                sw.Flush();
                sw.Close();
                fs.Close();
            }
            catch (Exception)
            {
                return false;
            }
            return true;
        }
    }
}
