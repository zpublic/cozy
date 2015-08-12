using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CozyNote.ConsoleClient
{
    public class SceneManager
    {
        public static SceneManager Instance = new SceneManager();

        private Stack<IScene> SceneStack = new Stack<IScene>();

        public IScene CurrScene
        {
            get
            {
                return SceneStack.Peek();
            }
        }

        public bool Empty
        {
            get
            {
                return SceneStack.Count == 0;
            }
        }

        private SceneManager()
        {

        }

        public void PushScene(IScene scene)
        {
            scene.Enter();
            SceneStack.Push(scene);
        }

        public IScene PopScene()
        {
            var scene = SceneStack.Pop();
            scene.Exit();
            return scene;
        }

        public void ReplaceScene(IScene scene)
        {
            SceneStack.Pop().Exit();

            scene.Enter();
            SceneStack.Push(scene);
        }

        public void Exit()
        {
            while(!Empty)
            {
                PopScene();
            }
        }
    }
}
