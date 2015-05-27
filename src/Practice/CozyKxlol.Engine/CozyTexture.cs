using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Graphics;

namespace CozyKxlol.Engine
{
    public class CozyTexture : IDisposable
    {
        private Texture2D _Texutre;

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
    }
}
