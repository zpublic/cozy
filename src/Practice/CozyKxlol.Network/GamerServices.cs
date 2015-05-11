using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.GamerServices;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CozyKxlol.Network
{
    class GamerServices
    {
        public enum Status
        {
            SignIn,
            SignedIn
        };

        public Status currentGameState = Status.SignIn;

        public void Init(Game game)
        {
            game.Components.Add(new GamerServicesComponent(game));
        }

        public void Update()
        {
            // 不显示登陆 反正也登不了。。。
//             if (currentGameState == Status.SignIn)
//             {
//                 if (Gamer.SignedInGamers.Count < 1)
//                 {
//                     Guide.ShowSignIn(1, false);
//                 }
//                 else
//                 {
//                     currentGameState = Status.SignedIn;
//                 }
//             }
        }
    }
}
