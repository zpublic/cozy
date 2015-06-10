using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using CozyKxlol.Engine;
using Starbound.Input;
using CozyKxlol.Engine.Tiled;
using CozyKxlol.MapEditor.Command;
using Microsoft.Xna.Framework;
using Starbound.UI.XNA.Renderers;
using Starbound.UI.Controls;
using Starbound.UI.Resources;

namespace CozyKxlol.MapEditor.OperateLayer
{
    public class MapEditorSceneOperateLayer : CozyLayer
    {
        // accept keyboard shortcuts
        // and accept mouse click
        // then modify TiledMapDataContainer`s data

        private Random random = new Random();

        List<Control> controls;
        StackPanel panel;
        XNARenderer renderer;

        public MouseEvents Mouse { get; set; }
        public KeyboardEvents Keyboard { get; set; }

        public bool IsLeftMouseButtonPress { get; set; }

        public const uint S_Add     = 0000;
        public const uint S_Remove  = 0001;

        public uint Status { get; set; }

        public uint CurrentTiledId { get; set; }
        public Point CurrentPosition { get; set; }

        public Vector2 NodeContentSize { get; set; }

        public MapEditorSceneOperateLayer(Vector2 nodeSize)
        {
            renderer = new XNARenderer();
            controls = new List<Control>();
            panel = new StackPanel()
            {
                Orientation = Orientation.Veritical,
                ActualWidth = 240,
                ActualHeight = 440,
                X = 990,
                Y = 200
            };
            panel.UpdateLayout();
            var button = new SampleButton(10, 220);
            panel.AddChild(button, () => { button.Content = "Click1"; });
            var button2 = new SampleButton(10, 350);
            panel.AddChild(button2, () => { button2.Content = "Click2"; });

            Mouse               = new MouseEvents();
            Keyboard            = new KeyboardEvents();

            NodeContentSize     = nodeSize;

            Mouse.ButtonPressed     += new EventHandler<MouseButtonEventArgs>(OnButtonPressed);

            Mouse.ButtonClicked     += new EventHandler<MouseButtonEventArgs>(OnButtonClicked);

            Mouse.ButtonReleased    += new EventHandler<MouseButtonEventArgs>(OnButtonReleased);

            Mouse.MouseMoved        += new EventHandler<MouseEventArgs>(OnMouseMoved);

            Keyboard.KeyPressed     += new EventHandler<KeyboardEventArgs>(OnKeyPressed);

            Keyboard.KeyReleased    += new EventHandler<KeyboardEventArgs>(OnKeyReleased);

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
            spriteBatch.End();
            foreach (Control control in controls)
            {
                renderer.Render(control, spriteBatch);
            }

            foreach (Control control in panel.Children)
            {
                renderer.Render(control, spriteBatch);
            }
            spriteBatch.Begin();

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
                panel.DispatchClick(msg.Current.Position.X, msg.Current.Position.Y);
            }
            else if (msg.Button == MouseButton.Right)
            {
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
