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

namespace CozyKxlol.MapEditor
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

        private void AddTiled(Point p)
        {
            // noity tiled modify to Container
            var command = new ContainerModifyOne(p.X, p.Y, CurrentTiledId);
            TiledCommandMessages(this, new TiledCommandArgs(command));
        }

        public MapEditorSceneOperateLayer(Vector2 nodeSize)
        {
            renderer = new XNARenderer();
            controls = new List<Control>();
            panel = new StackPanel() { Orientation = Orientation.Horizontal, ActualWidth = 1280, ActualHeight = 800 };
            panel.UpdateLayout();

            Mouse               = new MouseEvents();
            Keyboard            = new KeyboardEvents();

            NodeContentSize     = nodeSize;

            Mouse.ButtonPressed += (sender, msg) =>
            {
                if(msg.Button == MouseButton.Left)
                {
                    IsLeftMouseButtonPress = true;
                }
            };

            Mouse.ButtonReleased+= (sender, msg) =>
            {
                if (msg.Button == MouseButton.Left)
                {
                    IsLeftMouseButtonPress = false;
                }
            };

            Mouse.ButtonClicked += (sender, msg) =>
            {
                if(msg.Button == MouseButton.Left)
                {
                    if (Status == S_Add)
                    {
                        Point p = CozyTiledPositionHelper.ConvertPositionToTiledPosition(CurrentPosition.ToVector2(), NodeContentSize);
                        AddTiled(p);
                    }
                }
                else if (msg.Button == MouseButton.Right)
                {
                    panel.AddChild(new Starbound.UI.Controls.Button()
                    {
                        PreferredHeight = random.Next(50) + 50,
                        PreferredWidth = random.Next(50) + 50,
                        Margin = new Starbound.UI.Thickness(3, 3, 0, 0),
                        Font = Starbound.UI.Application.ResourceManager.GetResource<IFontResource>("Font"),
                        Content = "hehe",
                        Background = new Starbound.UI.SBColor(random.NextDouble(), random.NextDouble(), random.NextDouble()),
                        Foreground = new Starbound.UI.SBColor(random.NextDouble(), random.NextDouble(), random.NextDouble())
                    });
                }
            };

            Mouse.MouseMoved += (sender, msg) =>
            {
                // update position of mouse
                CurrentPosition = msg.Current.Position;
                if (IsLeftMouseButtonPress && Status == S_Add)
                {
                    Point p = CozyTiledPositionHelper.ConvertPositionToTiledPosition(CurrentPosition.ToVector2(), NodeContentSize);
                    AddTiled(p);
                }
            };

            Keyboard.KeyPressed += (sender, msg) =>
            {
                // doSomething
            };

            Keyboard.KeyReleased += (sender, msg) =>
            {
                // doSomething
            };

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
    }
}
