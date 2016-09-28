using CozyArce.Tree.Shared;
using CozyArce.Tree.Shared.Model;
using Microsoft.Graphics.Canvas;
using Microsoft.Graphics.Canvas.Effects;
using Microsoft.Graphics.Canvas.UI.Xaml;
using System.Numerics;
using Windows.UI;

namespace CozyArce.Tree.Renders
{
    public class LowRender : ITreeRender
    {
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
                }
                foreach (var b in tree.Leaves)
                {
                    clds.FillEllipse(
                        new Vector2(b.beginX, b.beginY),
                        5,
                        3,
                        Colors.Green);
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
            GaussianBlurEffect ef = new GaussianBlurEffect();
            ef.Source = cl;
            ef.BorderMode = EffectBorderMode.Soft;
            ef.BlurAmount = 1;
            _args.DrawingSession.DrawImage(ef);
        }
    }
}
