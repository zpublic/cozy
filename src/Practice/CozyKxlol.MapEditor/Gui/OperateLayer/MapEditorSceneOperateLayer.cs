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
using CozyKxlol.MapEditor.TilesPlugin;
using CozyKxlol.MapEditor.Gui.Controls;
using CozyKxlol.MapEditor.TilesPlugin.ColorTiles;

namespace CozyKxlol.MapEditor.Gui.OperateLayer
{
    public partial class MapEditorSceneOperateLayer : CozyLayer
    {
        List<IEnumDrawableUIElemt> DrawableUIElemts;
        StackPanel panel;
        MultiScrollStackPanel tilesPanel;
        XNARenderer renderer;
        Dictionary<Point, uint> TempTiles = new Dictionary<Point,uint>();

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
            Status          = S_Remove;
            CurrentTiledId  = 0;

            Mouse               = new MouseEvents();
            Mouse.ButtonPressed     += new EventHandler<MouseButtonEventArgs>(OnButtonPressed);
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

            foreach (var obj in DrawableUIElemts)
            {
                foreach (Control elemt in obj.GetDrawableElemt())
                {
                    renderer.Render(elemt, spriteBatch);
                }
            }

            spriteBatch.Begin();

            if (Status == S_Add)
            {
                CozyTiledFactory.GetInstance(CurrentTiledId).DrawAt(gameTime, spriteBatch, CurrentPosition.ToVector2(), NodeContentSize);
            }
            foreach(var obj in TempTiles)
            {
                var ActualPosition = CozyTiledPositionHelper.ConvertTiledPositionToPosition(obj.Key, NodeContentSize);
                CozyTiledFactory.GetInstance(obj.Value).DrawAt(gameTime, spriteBatch, ActualPosition, NodeContentSize);
            }
        }

        private void initGui()
        {
            DrawableUIElemts = new List<IEnumDrawableUIElemt>();

            renderer    = new XNARenderer();
            panel       = new StackPanel()
            {
                Orientation     = Orientation.Veritical,
                ActualWidth     = 240,
                ActualHeight    = 440,
                X               = 990,
                Y               = 200
            };
            DrawableUIElemts.Add(panel);
            panel.UpdateLayout();

            const int PanelRaw  = 2;
            tilesPanel = new MultiScrollStackPanel(PanelRaw)
            {
                ActualWidth     = 960,
                ActualHeight    = PanelRaw * 32,
                X               = 0,
                Y               = 0,
            };
            DrawableUIElemts.Add(tilesPanel);
            tilesPanel.UpdateLayout();

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

            ShowAllTiles();
        }

        private void ShowAllTiles()
        {
            foreach(var obj in CozyTiledFactory.GetTiles())
            {
                ContentControl control = null;
                if(obj.Value is CozyColorTile)
                {
                    var ColorTile   = obj.Value as CozyColorTile;
                    var color       = ColorTile.ColorProperty;
                    control         = new SampleButton(0, 0)
                    {
                        PreferredHeight = 32,
                        PreferredWidth  = 32,
                        Background      = new Starbound.UI.SBColor(color.R, color.G, color.B),
                        Foreground      = new Starbound.UI.SBColor(color.R, color.G, color.B),
                    };
                }
                else if(obj.Value is CozySpriteTiled)
                {
                    var SpriteTile  = obj.Value as CozySpriteTiled;
                    control         = new TileButton(SpriteTile.Texture, SpriteTile.Rect);
                }
                if(control != null)
                {
                    tilesPanel.AddChild(control, () => { CurrentTiledId = obj.Key; });
                }
            }
        }
    }
}
