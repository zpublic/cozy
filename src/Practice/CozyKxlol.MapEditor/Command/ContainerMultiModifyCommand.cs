using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;

namespace CozyKxlol.MapEditor.Command
{
    public class ContainerMultiModifyCommand : ICommand
    {
        public Dictionary<Point, uint> ModifyList { get; set; }
        private List<KeyValuePair<Point, uint>> OriModifyList {get; set;}

        public void Do(TiledMapDataContainer container)
        {
            OriModifyList = new List<KeyValuePair<Point, uint>>();
            foreach(var obj in ModifyList)
            {
                OriModifyList.Add(new KeyValuePair<Point, uint>(obj.Key, container.Read(obj.Key.X, obj.Key.Y)));
                container.Write(obj.Key.X, obj.Key.Y, obj.Value);
            }
        }
        public void Undo(TiledMapDataContainer container)
        { 
            foreach(var obj in OriModifyList)
            {
                container.Write(obj.Key.X, obj.Key.Y, obj.Value);
            }
        }

        public ContainerMultiModifyCommand(Dictionary<Point, uint> modifyList)
        {
            ModifyList = modifyList;
        }
    }
}
