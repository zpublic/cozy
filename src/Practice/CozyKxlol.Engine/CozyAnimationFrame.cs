using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CozyKxlol.Engine
{
    public class CozyAnimationFrame : ICloneable
    {
        public CozyTexture Texture { get; set; }
        public float DelayUnits { get; set; }

        public static CozyAnimationFrame Create(CozyTexture texture, float delayUnits)
        {
            var animate = new CozyAnimationFrame();
            animate.InitWithTexture(texture, delayUnits);
            return animate;
        }

        public static CozyAnimationFrame Create(string path, float delayUnits)
        {
            var texture = CozyDirector.Instance.TextureCacheInstance.AddImage(path);
            var animate = new CozyAnimationFrame();
            animate.InitWithTexture(texture, delayUnits);
            return animate;
        }

        public virtual bool InitWithTexture(CozyTexture texture, float delayUnits)
        {
            Texture = texture;
            DelayUnits = delayUnits;
            return true;
        }

        public virtual object Clone()
        {
            var texture = Texture.Clone() as CozyTexture;
            return Create(Texture , DelayUnits);
        }
    }
}
