using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using CozyKxlol.Engine.Tiled;

namespace CozyKxlol.Engine
{
    public class CozyDirector : IDisposable
    {
        readonly Stack<CozyScene> scenesStack = new Stack<CozyScene>();
        public CozyScene RunningScene { get; private set; }

        public Point WindowSize
        {
            get
            {
                return GameInstance == null ? Point.Zero : GameInstance.WindowSize;
            }
            set
            {
                if(GameInstance != null)
                {
                    GameInstance.WindowSize = value;
                }
            }
        }

        public CozyGame GameInstance { get; set; }

        public CozyTextureCache TextureCacheInstance { get; set; }

        public CozyActionManager ActionManagerInstance { get; set; }

        public CozyTileJsonManager JsonManagerInstance { get; set; }

        public CozyContentManager ContentInstance { get; set; }

        public Random RandomMaker { get; set; }

        private CozyDirector()
        {
            TextureCacheInstance    = new CozyTextureCache();
            ActionManagerInstance   = new CozyActionManager();
            JsonManagerInstance     = new CozyTileJsonManager();
            RandomMaker             = new Random();
        }

        public void RunWithScene(CozyScene scene)
        {
            PushScene(scene);
        }

        public void ReplaceScene(CozyScene scene)
        {
        }

        public void PushScene(CozyScene scene)
        {
            if(RunningScene != null)
            {
                scenesStack.Push(RunningScene);
            }
            RunningScene = scene;
        }

        public void PopScene()
        {
            if(scenesStack.Count > 0)
            {
                RunningScene = scenesStack.Pop();
            }
        }

        private static CozyDirector _Instance = new CozyDirector();
        public static CozyDirector Instance
        {
            get
            {
                return _Instance;
            }
        }

        public void Dispose()
        {
            TextureCacheInstance.Dispose();
        }

        public void Update(GameTime dt)
        {
            ActionManagerInstance.Update(dt);
        }
    }
}
