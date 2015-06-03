using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using CozyKxlol.Engine;

namespace CozyKxlol.MapEditor
{
    class MapEditorScene : CozyScene
    {
        public MapEditorScene()
        {
            this.AddChind(new MapEditorSceneLayer());
        }
    }
}
