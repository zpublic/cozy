using System.Collections.Generic;

namespace CozyPixel.Model
{
    // 像素画工程model
    public class PixelArtProject
    {
        public string FilePath { get; set; }
        public List<PixelArtObject> Objects { get; } = new List<PixelArtObject>();

        public bool Load()
        {
            return false;
        }

        public bool Save()
        {
            return false;
        }
    }
}
