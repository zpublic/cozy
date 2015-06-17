using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using CozyKxlol.Engine.Tiled.Json;
using CozyKxlol.Engine.Tiled.Json.Strategy;

namespace CozyKxlol.MapEditor.Command
{
    public class ContainerModifyBlockCommand : ICommand
    {
        public CozyJsonBlockData Data { get; set; }
        public Point Origion { get; set; }
        private List<KeyValuePair<Point, uint>> OriModifyList { get; set; }

        public ContainerModifyBlockCommand(CozyJsonBlockData data, Point origion)
        {
            Data            = data;
            Origion         = origion;
            OriModifyList   = new List<KeyValuePair<Point, uint>>();
        }

        public void Do(TiledMapDataContainer container)
        {
            if(Data != null)
            {
                uint[,] rect    = null;
                if (Data.type.CompareTo("square") == 0)
                {
                    rect        = new uint[Data.w, Data.w];
                    FillRect(Data.data, rect);
                }
                else if (Data.type.CompareTo("rect") == 0)
                {
                    rect        = new uint[Data.w, Data.h];
                    FillRect(Data.data, rect);
                }
                int x = rect.GetLength(0);
                int y = rect.GetLength(1);
                for (int i = 0; i < x; ++i)
                {
                    for (int j = 0; j < y; ++j)
                    {
                        ModifyContainer(container, i, j, rect[i, j]);
                    }
                }
            }
        }

        public void Undo(TiledMapDataContainer container)
        {
            foreach (var obj in OriModifyList)
            {
                container.Write(obj.Key.X, obj.Key.Y, obj.Value);
            }
        }

        private void ModifyContainer(TiledMapDataContainer container, int x, int y, uint data)
        {
            OriModifyList.Add(new KeyValuePair<Point, uint>(new Point(x, y), container.Read(x, y)));
            container.Write(Origion.X + x, Origion.Y + y, data);
        }

        private static void FillRect(string str, uint[,] rect)
        {
            string[] dataSplit  = str.Split(',');
            List<uint> result   = new List<uint>();
            int length          = rect.Length;
            TiledDataParseContext context = new TiledDataParseContext();

            foreach (var subData in dataSplit)
            {
                MatchContextStrategy(subData, context);
                context.Parse(subData, result, length);
            }
            FillDyadicArray(result, rect);
        }

        private static void MatchContextStrategy(string subData, TiledDataParseContext context)
        {
            if (subData.Contains('-'))
            {
                context.Strategy = new TiledDataParseWithRange();
            }
            else if (subData.Contains('*'))
            {
                context.Strategy = new TiledDataParseWithFill();
            }
            else
            {
                context.Strategy = new TiledDataParseWithNothing();
            }
        }

        private static void FillDyadicArray(List<uint> source, uint[,] target)
        {
            int offset  = 0;
            int x       = target.GetLength(0);
            int y       = target.GetLength(1);
            for (int i = 0; i < x; ++i)
            {
                for (int j = 0; j < y; ++j)
                {
                    target[i, j] = source[offset++];
                    if (offset >= target.Length)
                    {
                        return;
                    }
                }
            }
        }
    }
}
