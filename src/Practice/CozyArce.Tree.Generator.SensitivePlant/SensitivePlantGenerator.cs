using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using CozyArce.Tree.Base.Model;
using CozyArce.Tree.Base;

namespace CozyArce.Tree.Generator.SensitivePlant
{
    public class SensitivePlantGenerator : ITreeGenerator
    {
        const double PI = 3.1415926;
        const int drawingBoardWith = 1000;
        const int drawingBoardHigh = 1000;


        CozyTree tree = new CozyTree();
        Point _begin = new Point(drawingBoardWith / 2, drawingBoardHigh);
        double _angle;
        double _startLength;
        double _startBranchWidth;
        GrowDirection _direction;


        public SensitivePlantGenerator()
        {
            DrawTree(_begin);
        }
        private void DrawTree(Point begin, double angle = PI / 2, double length = 800, short width = 10)
        {
            if (length < 5)
                return;
            int x = (int)(begin.X + length * Math.Cos(angle));
            int y = (int)(begin.Y - length * Math.Sin(angle));
            var endPoint = new Point(x, y);
            tree.Branchs.Add(new CozyBranch()
            {
                begin = begin,
                end = endPoint,
                width = width
            });
            var subLength = length * 0.3;
            x = (int)(begin.X + subLength * Math.Cos(angle));
            y = (int)(begin.Y - subLength * Math.Sin(angle));
            var startPoint = new Point(x, y);
            var nextPoint = startPoint;
            var space = length * 0.2;
            var nextWidth = (short)(width * 0.9);
            nextWidth = nextWidth >= 1 ? nextWidth : nextWidth;
            while (hasNextBranch(nextPoint, endPoint, angle))
            {
                length = length * 0.8;
                var lAngle = angle + 9*PI / 20;
                x = (int)(nextPoint.X + subLength * Math.Cos(lAngle));
                y = (int)(nextPoint.Y - subLength * Math.Sin(lAngle));
                tree.Branchs.Add(new CozyBranch()
                {
                    begin = nextPoint,
                    end = new Point(x, y),
                    width = nextWidth
                });
                DrawTree(nextPoint, lAngle, subLength, nextWidth);
                var rAngle = angle - 9*PI / 20;
                x = (int)(nextPoint.X + subLength * Math.Cos(rAngle));
                y = (int)(nextPoint.Y - subLength * Math.Sin(rAngle));
                tree.Branchs.Add(new CozyBranch()
                {
                    begin = nextPoint,
                    end = new Point(x, y),
                    width = nextWidth
                });
                DrawTree(nextPoint, rAngle, subLength, nextWidth);
                var xx = nextPoint.X + space * Math.Cos(angle);
                var yy = nextPoint.Y - space * Math.Sin(angle);
                nextPoint = new Point(xx, yy);
                nextWidth = (short)(nextWidth * 0.9);
                subLength *= 0.9;
            }

        }

        public SensitivePlantGenerator SetBeginPoint(Point begin)
        {
            _begin = begin;
            return this;
        }

        public SensitivePlantGenerator SetAngle(double angle)
        {
            _angle = angle;
            return this;
        }

        public SensitivePlantGenerator SetStartLength(double length)
        {
            _startLength = length;
            return this;
        }

        public SensitivePlantGenerator SetStartWidth(double width)
        {
            _startBranchWidth = width;
            return this;
        }

        public SensitivePlantGenerator SetGrowDirection(GrowDirection direction)
        {
            _direction = direction;
            return this;
        }
        private bool hasNextBranch(Point start, Point end, double angle)
        {
            while (angle > 2 * PI)
                angle -= 2 * PI;
            while (angle < 0)
                angle += 2 * PI;
            if (angle < PI / 4 || angle > 7*PI / 4)
            {
                if (start.X < end.X)
                    return true;
            }
            else if (angle > PI / 4 && angle < 3 * PI / 4)
            {
                if (start.Y > end.Y)
                    return true;
            }
            else if (angle > 3 * PI / 4 && angle < 5 * PI / 4)
            {
                if (start.X > end.X)
                    return true;
            }
            else if (angle > 5 * PI / 4 && angle < 7 * PI / 4)
            {
                if (start.Y < end.Y)
                    return true;
            }
            return false;
        }

        CozyTree ITreeGenerator.Generate()
        {
            tree = new CozyTree();
            DrawTree(_begin);
            return tree;
        }
    }

    public enum GrowDirection
    {
        Surround,
        Left,
        Right,
        TallAndHigh
    }
}
