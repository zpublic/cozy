using CozyArce.Tree.Shared;
using CozyArce.Tree.Shared.Model;
using Microsoft.Graphics.Canvas;
using Microsoft.Graphics.Canvas.Effects;
using Microsoft.Graphics.Canvas.UI.Xaml;
using System;
using System.Numerics;
using Windows.UI;

namespace CozyArce.Tree.Renders
{
    public class LowRender : ITreeRender
    {
        Random rnd = new Random();
        private Vector2 RndPosition()
        {
            double x = rnd.NextDouble() * 500f;
            double y = rnd.NextDouble() * 500f;
            return new Vector2((float)x, (float)y);
        }
        private Color RndGreenColor()
        {
            Color g = Colors.Green;
            g.R += (byte)(rnd.NextDouble() * 50);
            g.G += (byte)((rnd.NextDouble()-0.5) * 100);
            g.B += (byte)(rnd.NextDouble() * 50);
            return g;
        }

        public CanvasControl _sender;
        public CanvasDrawEventArgs _args;
        public LowRender(
            CanvasControl sender,
            CanvasDrawEventArgs args)
        {
            _sender = sender;
            _args = args;
        }

        public void Draw(CozyTree tree)
        {
            CanvasCommandList cl = new CanvasCommandList(_sender);
            using (CanvasDrawingSession clds = cl.CreateDrawingSession())
            {
                foreach (var b in tree.Branchs)
                {
                    clds.DrawLine(
                        new Vector2(b.beginX, b.beginY),
                        new Vector2(b.endX, b.endY),
                        Colors.Black,
                        b.width);
                    clds.FillCircle(
                        new Vector2(b.endX, b.endY),
                        b.width / 2,
                        Colors.Black);
                }
                foreach (var b in tree.Leaves)
                {
                    clds.FillEllipse(
                        new Vector2(b.beginX, b.beginY),
                        5,
                        3,
                        RndGreenColor());
                }
                foreach (var b in tree.Flowers)
                {
                    clds.FillEllipse(
                        new Vector2(b.posX, b.posY),
                        5,
                        3,
                        Colors.Pink);
                }
            }
            var ef = new GaussianBlurEffect();
            ef.Source = cl;
            ef.BlurAmount = 5;
            _args.DrawingSession.DrawImage(ef);
            _args.DrawingSession.DrawImage(cl, 3, -3);
        }
    }
}
