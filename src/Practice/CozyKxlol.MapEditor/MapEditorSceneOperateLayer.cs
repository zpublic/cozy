using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using CozyKxlol.Engine;
using Starbound.Input;
using CozyKxlol.Engine.Tiled;
using CozyKxlol.MapEditor.Command;
using Microsoft.Xna.Framework;

namespace CozyKxlol.MapEditor
{
    public class MapEditorSceneOperateLayer : CozyLayer
    {
        // accept keyboard shortcuts
        // and accept mouse click
        // then modify TiledMapDataContainer`s data

        public MouseEvents Mouse { get; set; }
        public KeyboardEvents Keyboard { get; set; }

        #region Status

        public const uint S_Add     = 0000;
        public const uint S_Remove  = 0001;

        public uint Status { get; set; }

        #endregion

        #region Current

        public uint CurrentTiledId { get; set; }
        public Point CurrentPosition { get; set; }

        #endregion

        public Vector2 NodeContentSize { get; set; }

        public MapEditorSceneOperateLayer()
        {
            Mouse               = new MouseEvents();
            Keyboard            = new KeyboardEvents();

            NodeContentSize     = Vector2.One * 32;

            #region Event Bind

            Mouse.ButtonClicked += (sender, msg) =>
            {
                if(msg.Button == MouseButton.Left)
                {
                    if (Status == S_Add)
                    {
                        // noity tiled modify to Container
                        Point p = ConvertPositionToTiledPosition(CurrentPosition.ToVector2());
                        var command = new ContainerModifyOne(p.X, p.Y, CurrentTiledId);
                        TiledCommandMessages(this, new TiledCommandArgs(command));
                    }
                }
            };

            Mouse.MouseMoved += (sender, msg) =>
            {
                // update position of mouse
                CurrentPosition = msg.Current.Position;
            };

            Keyboard.KeyPressed += (sender, msg) =>
            {
                // doSomething
            };

            Keyboard.KeyReleased += (sender, msg) =>
            {
                // doSomething
            };

            #endregion

            Status          = S_Add;
            CurrentTiledId  = 1;
        }

        public override void Update(Microsoft.Xna.Framework.GameTime gameTime)
        {
            base.Update(gameTime);

            Mouse.Update(gameTime);
            Keyboard.Update(gameTime);
        }

        protected override void DrawSelf(Microsoft.Xna.Framework.GameTime gameTime, Microsoft.Xna.Framework.Graphics.SpriteBatch spriteBatch)
        {
            if(Status == S_Add)
            {
                CozyTiledFactory.GetInstance(CurrentTiledId).DrawAt(gameTime, spriteBatch, CurrentPosition.ToVector2());
            }
        }

        public Point ConvertPositionToTiledPosition(Vector2 pos)
        {
            float x = pos.X / NodeContentSize.X;
            float y = pos.Y / NodeContentSize.Y;
            return new Point((int)x, (int)y);
        }

        public class TiledCommandArgs : EventArgs
        {
            public ICommand Command { get; set; }

            public TiledCommandArgs(ICommand command)
            {
                Command = command;
            }
        }
        public event EventHandler<TiledCommandArgs> TiledCommandMessages;
    }
}
