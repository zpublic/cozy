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
    public partial class MapEditorSceneOperateLayer : CozyLayer
    {
        List<Control> controls;
        StackPanel panel;
        XNARenderer renderer;

        public MouseEvents Mouse { get; set; }
        public KeyboardEvents Keyboard { get; set; }

        public const uint S_Add     = 0000;
        public const uint S_Remove  = 0001;

        public uint Status { get; set; }

        public uint CurrentTiledId { get; set; }
        public Point CurrentPosition { get; set; }

        public Vector2 NodeContentSize { get; set; }

        public MapEditorSceneOperateLayer(Vector2 nodeSize)
        {
            initGui();

            NodeContentSize = nodeSize;
            Status          = S_Add;
            CurrentTiledId  = CozyGreenTiled.TiledId;

            Mouse               = new MouseEvents();
            Mouse.ButtonPressed     += new EventHandler<MouseButtonEventArgs>(OnButtonPressed);
            Mouse.ButtonClicked     += new EventHandler<MouseButtonEventArgs>(OnButtonClicked);
            Mouse.ButtonReleased    += new EventHandler<MouseButtonEventArgs>(OnButtonReleased);
            Mouse.MouseMoved        += new EventHandler<MouseEventArgs>(OnMouseMoved);

            Keyboard = new KeyboardEvents();
            Keyboard.KeyPressed     += new EventHandler<KeyboardEventArgs>(OnKeyPressed);
            Keyboard.KeyReleased    += new EventHandler<KeyboardEventArgs>(OnKeyReleased);
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

        private void initGui()
        {
            renderer    = new XNARenderer();
            controls    = new List<Control>();
            panel       = new StackPanel()
            {
                Orientation     = Orientation.Veritical,
                ActualWidth     = 240,
                ActualHeight    = 440,
                X               = 990,
                Y               = 200
            };
            panel.UpdateLayout();
            var button = new SampleButton(10, 220)
            {
                Content     = "Add",
            };
            panel.AddChild(button, () => { Status = S_Add; });

            var button2 = new SampleButton(10, 350)
            {
                Content     = "Remove",
            };
            panel.AddChild(button2, () => { Status = S_Remove; });

            var button3 = new SampleButton(10, 450)
            {
                Content     = "Clear",
            };
            panel.AddChild(button3, new Action(OnClear));

            var button4 = new SampleButton(10, 10)
            {
                Content     = "Load",
            };
            panel.AddChild(button4, new Action(OnLoad));

            var button5 = new SampleButton(10, 10)
            {
                Content     = "Save",
            };
            panel.AddChild(button5, new Action(OnSave));

            var blockGreen = new SampleButton(10, 550)
            {
                Content     = "Green",
                Foreground  = new Starbound.UI.SBColor(Color.Green.R, Color.Green.G, Color.Green.B),
                Background  = new Starbound.UI.SBColor(Color.Green.R, Color.Green.G, Color.Green.B),
            };
            panel.AddChild(blockGreen, () => { CurrentTiledId = CozyGreenTiled.TiledId; });

            var blockRed = new SampleButton(50, 550)
            {
                Content     = "Red",
                Foreground  = new Starbound.UI.SBColor(Color.Red.R, Color.Red.G, Color.Red.B),
                Background  = new Starbound.UI.SBColor(Color.Red.R, Color.Red.G, Color.Red.B),
            };
            panel.AddChild(blockRed, () => { CurrentTiledId = CozyRedTiled.TiledId; });
        }
    }
}
