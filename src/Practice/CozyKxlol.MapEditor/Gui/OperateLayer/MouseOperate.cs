using CozyKxlol.Engine.Tiled;
using CozyKxlol.MapEditor.Command;
using CozyKxlol.MapEditor.Event;
using Microsoft.Xna.Framework;
using Starbound.Input;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CozyKxlol.MapEditor.Gui.OperateLayer
{
    public partial class MapEditorSceneOperateLayer
    {
        public event EventHandler<TiledCommandArgs> TiledCommandUndo;
        public event EventHandler<TiledCommandArgs> TiledCommandMessages;
        private const int MapSize_X = 30;
        private const int MapSize_Y = 20;
        private bool IsLeftMouseButtonPress;

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
                if(panel.DispatchClick(msg.Current.Position.X, msg.Current.Position.Y))
                {
                    return;
                }
                if(tilesPanel.DispatchClick(msg.Current.Position.X, msg.Current.Position.Y))
                {
                    return;
                }
                Point p = CozyTiledPositionHelper.ConvertPositionToTiledPosition(CurrentPosition.ToVector2(), NodeContentSize);
                if (Status == S_Add)
                {
                    AddTiled(p);
                }
                else if (Status == S_Remove)
                {
                    RemoveTiled(p);
                }
                
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

        private void OnMouseMoved(object sender, MouseEventArgs msg)
        {
            CurrentPosition = msg.Current.Position;
            if (IsLeftMouseButtonPress)
            {
                Point p = CozyTiledPositionHelper.ConvertPositionToTiledPosition(CurrentPosition.ToVector2(), NodeContentSize);
                if (Status == S_Add)
                {
                    AddTiled(p);
                }
                else if (Status == S_Remove)
                {
                    RemoveTiled(p);
                }
            }
        }

        private bool Judge(Point p)
        {
            return (p.X < MapSize_X && p.Y < MapSize_Y && p.X >= 0 && p.Y >= 0);
        }

        private void AddTiled(Point p)
        {
            if (Judge(p))
            {
                var command = new ContainerModifyOne(p.X, p.Y, CurrentTiledId);
                TiledCommandMessages(this, new TiledCommandArgs(command));
            }
        }

        private void RemoveTiled(Point p)
        {
            if (Judge(p))
            {
                var command = new ContainerModifyOne(p.X, p.Y, 0);
                TiledCommandMessages(this, new TiledCommandArgs(command));
            }
        }
    }
}
