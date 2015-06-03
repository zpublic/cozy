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
            // 地图编辑器应该分两层
            // 下层为Engine里的tile绘制层
            // 上层为编辑功能层，支持鼠标和键盘操作
            this.AddChind(new MapEditorSceneLayer());
        }
    }
}
