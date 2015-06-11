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
using CozyKxlol.MapEditor.Event;
using CozyKxlol.MapEditor.Tileds;

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

        public const uint S_Add = 0000;
        public const uint S_Remove = 0001;

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
            var button = new SampleButton(10, 220) 
            { 
                Content = "Add",
            };
            panel.AddChild(button, () => { Status = S_Add; });
            var button2 = new SampleButton(10, 350) 
            {
                Content = "Remove",
            };
            panel.AddChild(button2, () => { Status = S_Remove; });
            var button3 = new SampleButton(10, 450) 
            {
                Content = "Clear",
            };
            panel.AddChild(button3, () => 
            { 
                var command = new ContainerClearCommand();
                TiledCommandMessages(this, new TiledCommandArgs(command)); 
            });

            var blockGreen = new SampleButton(10, 550) 
            {
                Content = "Green",
                Foreground = new Starbound.UI.SBColor(Color.Green.R, Color.Green.G, Color.Green.B),
                Background = new Starbound.UI.SBColor(Color.Green.R, Color.Green.G, Color.Green.B),
            };
            panel.AddChild(blockGreen, () => { CurrentTiledId = 1; });

            var blockRed = new SampleButton(50, 550)
            {
                Content = "Red",
                Foreground = new Starbound.UI.SBColor(Color.Red.R, Color.Red.G, Color.Red.B),
                Background = new Starbound.UI.SBColor(Color.Red.R, Color.Red.G, Color.Red.B),
            };
            panel.AddChild(blockRed, () => { CurrentTiledId = 2; });

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
            CurrentTiledId  = CozyGreenTiled.TiledId;
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

            if (Status == S_Add)
            {
                CozyTiledFactory.GetInstance(CurrentTiledId).DrawAt(gameTime, spriteBatch, CurrentPosition.ToVector2(), NodeContentSize);
            }
        }

        public event EventHandler<TiledCommandArgs> TiledCommandMessages;
        public event EventHandler<TiledCommandArgs> TiledCommandUndo;

        private void OnButtonPressed(object sender, MouseButtonEventArgs msg)
        {
            if (msg.Button == MouseButton.Left)
            {
                IsLeftMouseButtonPress = true;
            }
        }

        private void OnButtonClicked(object sender, MouseButtonEventArgs msg)
        {
            if (msg.Button == MouseButton.Left)
            {
                Point p = CozyTiledPositionHelper.ConvertPositionToTiledPosition(CurrentPosition.ToVector2(), NodeContentSize);
                if (Status == S_Add)
                {
                    AddTiled(p);
                }
                else if(Status == S_Remove)
                {
                    RemoveTiled(p);
                }
                panel.DispatchClick(msg.Current.Position.X, msg.Current.Position.Y);
            }
            else if (msg.Button == MouseButton.Right)
            {
                if (CommandHistory.Instance.CanUndo())
                {
                    TiledCommandUndo(this, null);
                }
            }
        }

        private void OnButtonReleased(object sender, MouseButtonEventArgs msg)
        {
            if (msg.Button == MouseButton.Left)
            {
                IsLeftMouseButtonPress = false;
            }
        }

        private void OnKeyPressed(object sender, KeyboardEventArgs msg)
        {

        }

        private void OnKeyReleased(object sender, KeyboardEventArgs msg)
        {

        }

        public const int MapSize_X = 30;
        public const int MapSize_Y = 20;

        private void AddTiled(Point p)
        {
            if (p.X < MapSize_X && p.Y < MapSize_Y)
            {
                var command = new ContainerModifyOne(p.X, p.Y, CurrentTiledId);
                TiledCommandMessages(this, new TiledCommandArgs(command));
            }
        }

        private void RemoveTiled(Point p)
        {
            if (p.X < MapSize_X && p.Y < MapSize_Y)
            {
                var command = new ContainerModifyOne(p.X, p.Y, 0);
                TiledCommandMessages(this, new TiledCommandArgs(command));
            }
        }

        private void OnMouseMoved(object sender, MouseEventArgs msg)
        {
            CurrentPosition = msg.Current.Position;
            if(IsLeftMouseButtonPress)
            {
                Point p = CozyTiledPositionHelper.ConvertPositionToTiledPosition(CurrentPosition.ToVector2(), NodeContentSize);
                if (Status == S_Add)
                {
                    AddTiled(p);
                }else if(Status == S_Remove)
                {
                    RemoveTiled(p);
                }
            }
        }
    }
}
