using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CozyNote.ConsoleClient.Scene
{
    public class WelcomeScene : SceneBase
    {
        private Menu menu { get; set; }

        public override void Enter()
        {
            menu = new Menu();
            menu.Add(new MenuItem() { Text = "退出", Command = OnExit, });
            menu.Add(new MenuItem() { Text = "注册", Command = OnSignUp, });
            menu.Add(new MenuItem() { Text = "登陆", Command = OnSignIn, });
        }

        public override void Run()
        {
            Console.Clear();
            Menu.Print(menu);
            menu.Input();
        }

        private void OnSignIn()
        {
            SceneManager.Instance.PushScene(new SignInScene());
        }

        private void OnSignUp()
        {
            SceneManager.Instance.PushScene(new SignUpScene());
        }

        private void OnExit()
        {
            SceneManager.Instance.Exit();
        }
    }
}
