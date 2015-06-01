using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace CozyKxlol.Engine
{
    public class CozyTexture : IDisposable, ICloneable
    {
        private Texture2D _Texutre;

        public Vector2 ContentSize
        {
            get
            {
                return new Vector2(_Texutre.Width, _Texutre.Height);
            }
        }

        public CozyTexture()
        {

        }

        public CozyTexture(Texture2D texture)
        {
            _Texutre = texture;
        }

        public CozyTexture(string path)
        {
            _Texutre = CozyDirector.Instance.GameInstance.Content.Load<Texture2D>(path);
        }

        public Texture2D Get()
        {
            return _Texutre;
        }

        public void Dispose()
        {
            _Texutre.Dispose();
        }

        public object Clone()
        {
            var text = new CozyTexture(this._Texutre);
            return text; 
        }
    }
}
