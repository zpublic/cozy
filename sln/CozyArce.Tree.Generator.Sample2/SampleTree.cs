using System;
using System.Windows;
using CozyArce.Tree.Base;
using CozyArce.Tree.Base.Model;

namespace CozyArce.Tree.Generator.Sample2
{
    public class SampleTree : ITreeGenerator
    {
        const double PI = 3.1415926;
        const double Arg = PI / 5;
        const double GoldenSection = 0.618;
        const double MinLength = 5;
        GrowDirection direction = GrowDirection.Right;
        Random rand = new Random();
        CozyTree tree;
        public CozyTree Generate()
        {
            tree = new CozyTree();
            DrawTree(new Point(500, 850), PI / 2, 200, 30);
            return tree;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="leafPercent">树叶比例</param>
        /// <param name="flowerPercent">开花比例</param>
        /// <param name="direct">生长方向</param>
        public void DrawTree(
            Point begin,
            double angle,
            double length,
            short width,
            double leafPercent = 0.5,
            double flowerPercent = 0.05,
            GrowDirection direct = GrowDirection.Right)
        {
            if (length < MinLength)
            {
                if (length < MinLength * leafPercent)
                {
                    tree.Leaves.Add(new CozyLeaf()
                    {
                        begin = begin,
                        end = new Point(begin.X + 2 * Math.Cos(angle), begin.Y + 2 * Math.Cos(angle)),
                    });
                    return;
                }
                else if (length < MinLength * (leafPercent + flowerPercent))
                {
                    tree.Flowers.Add(new CozyFlower()
                    {
                        pos = begin,
                        size = 3,
                    });
                }
                return;
            }

            int x = (int)(begin.X + length * Math.Cos(angle));
            int y = (int)(begin.Y - length * Math.Sin(angle));
            var end = new Point(x, y);

            tree.Branchs.Add(new CozyBranch()
            {
                begin = begin,
                end = end,
                width = width,
            });

            width -= (short)rand.Next(5, 8);
            if (width < 1) width = 1;
            var sub = rand.Next(2, 4);
            for (var i = 0; i < sub; ++i)
            {
                var len = rand.NextDouble();
                if (len < 0.1) len += 1.4;
                if (len > 0.9) len -= 0.4;
                var nextAngle = angle + (rand.NextDouble() - 0.5) * PI;
                switch (direct)
                {
                    case GrowDirection.TallAndStraight: nextAngle = angle + (rand.NextDouble() - 0.5) * PI / 3; break;
                    case GrowDirection.Left: nextAngle = angle + (rand.NextDouble() - 0.1) * PI / 2; break;
                    case GrowDirection.Right: nextAngle = angle + (rand.NextDouble() - 0.7) * PI / 2; break;
                }
                DrawTree(end, nextAngle, len * length, width);
            }
        }

        public SampleTree SetDirection(GrowDirection direct)
        {
            direction = direct;
            return this;
        }
    }

    public enum GrowDirection
    {
        TallAndStraight,
        Left,
        Right,
    }
}
