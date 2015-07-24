using System.Diagnostics;
using Gui4UFrameWork;
using Gui4UFrameWork.Graphics;
using Gui4UFrameWork.Node;

namespace Gui4UControls.ScrollBar
{
    public class DHorizontalScrollBarIndicator : Control
    {
        #region Initialize
        public DHorizontalScrollBarIndicator(string name, float positionX, float positionY, int width, int height) : base(name, positionX, positionY, width, height)
        {
        }

        public DHorizontalScrollBarIndicator(string name, float positionX, float positionY) : base(name, positionX, positionY)
        {
        }

        public DHorizontalScrollBarIndicator(string name) : base(name)
        {
        }
        #endregion

        #region Properties
        public bool DebugMode { get; set; }
        public bool Dragging { get; set; }
        public DVector2 StartMouseLocation { get; set; }
        public DVector2 CurrentMouseLocation { get; set; }
        public string DraggingTextureName { get; set; }
        public string NormalTextureName { get; set; }
        #endregion

        #region XNA
        public override void LoadContent()
        {
            base.LoadContent();
            UpdateDrawPositionByConfigAndParent();
            UpdateDrawSizeByConfig();

            NormalTextureName = Manager.ImageCompositor.CreateRectangleTexture(
                                                        Name + "-indicatorN",
                                                        (int)State.Width,
                                                        (int)State.Height,
                                                        1,
                                                        Theme.FillColor,
                                                        Theme.BorderColor);

            DraggingTextureName = Manager.ImageCompositor.CreateRectangleTexture(
                                            Name + "-indicatorD",
                                            (int)State.Width,
                                            (int)State.Height,
                                            1,
                                            Theme.HoverFillColor,
                                            Theme.BorderColor);
        }

        public override void UnloadContent()
        {
            base.UnloadContent();
        }

        public override void Update(DGameTime gameTime)
        {
            base.Update(gameTime);

            // get mouse stuff
            var leftMousePressed = Manager.InputManager.GetLeftMousePressed();
            var leftMouseReleased = Manager.InputManager.GetLeftMouseReleased();
            var mouseLocation = Manager.InputManager.GetMouseLocation();

            // check for dragging
            if (leftMousePressed && GetIsFocused() && Dragging == false)
            {
                System.Diagnostics.Debug.WriteLine("Start dragging.");
                Dragging = true;
                StartMouseLocation = mouseLocation;
            }

            if (Dragging)
            {
                System.Diagnostics.Debug.WriteLine("Dragging");
                CurrentMouseLocation = mouseLocation;
            }

            if (Dragging && leftMouseReleased)
            {
                System.Diagnostics.Debug.WriteLine("End dragging.");
                Dragging = false;
            }

            // check for configuration changes, if nothing changed , then return
            if (Config.Changed == false) return;
            Config.ResetChanged();
        }

        protected override void DrawMyData()
        {
            CurrentTextureName = Dragging ? DraggingTextureName : NormalTextureName;

            if (DebugMode)
            {
                Manager.ImageCompositor.Draw(CurrentTextureName, State, GuiColor.MidnightBlue());
            }
            else
            {
                base.DrawMyData();
            }
        }

        #endregion


    }
}
