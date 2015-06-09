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

        public bool IsLeftMouseButtonPress { get; set; }

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


        public MapEditorSceneOperateLayer(Vector2 nodeSize)
        {
            Mouse               = new MouseEvents();
            Keyboard            = new KeyboardEvents();

            NodeContentSize     = nodeSize;

            #region Event Bind

            Mouse.ButtonPressed     += new EventHandler<MouseButtonEventArgs>(OnButtonPressed);

            Mouse.ButtonClicked     += new EventHandler<MouseButtonEventArgs>(OnButtonClicked);

            Mouse.ButtonReleased    += new EventHandler<MouseButtonEventArgs>(OnButtonReleased);

            Mouse.MouseMoved        += new EventHandler<MouseEventArgs>(OnMouseMoved);

            Keyboard.KeyPressed     += new EventHandler<KeyboardEventArgs>(OnKeyPressed);

            Keyboard.KeyReleased    += new EventHandler<KeyboardEventArgs>(OnKeyReleased);

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
                CozyTiledFactory.GetInstance(CurrentTiledId).DrawAt(gameTime, spriteBatch, CurrentPosition.ToVector2(), NodeContentSize);
            }
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

        protected void OnButtonPressed(object sender, MouseButtonEventArgs msg)
        {
            if (msg.Button == MouseButton.Left)
            {
                IsLeftMouseButtonPress = true;
            }
        }

        protected void OnButtonClicked(object sender, MouseButtonEventArgs msg)
        {
            if (msg.Button == MouseButton.Left)
            {
                if (Status == S_Add)
                {
                    Point p = CozyTiledPositionHelper.ConvertPositionToTiledPosition(CurrentPosition.ToVector2(), NodeContentSize);
                    AddTiled(p);
                }
            }
        }

        protected void OnButtonReleased(object sender, MouseButtonEventArgs msg)
        {
            if (msg.Button == MouseButton.Left)
            {
                IsLeftMouseButtonPress = false;
            }
        }

        protected void OnKeyPressed(object sender, KeyboardEventArgs msg)
        {

        }

        protected void OnKeyReleased(object sender, KeyboardEventArgs msg)
        {

        }

        private void AddTiled(Point p)
        {
            // noity tiled modify to Container
            var command = new ContainerModifyOne(p.X, p.Y, CurrentTiledId);
            TiledCommandMessages(this, new TiledCommandArgs(command));
        }

        protected void OnMouseMoved(object sender, MouseEventArgs msg)
        {
            CurrentPosition = msg.Current.Position;
            if (IsLeftMouseButtonPress && Status == S_Add)
            {
                Point p = CozyTiledPositionHelper.ConvertPositionToTiledPosition(CurrentPosition.ToVector2(), NodeContentSize);
                AddTiled(p);
            }
        }
    }
}
