using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CozyDungeon.RoleCardEditor.EventArg
{
    public class ImageRefreshEventArgs : EventArgs
    {
        public CozyCardImage Img { get; set; }

        public int Id { get; set; }

        public ImageRefreshEventArgs(int id, CozyCardImage img)
        {
            Img = img;
            Id = id;
        }
    }
}
