// --------------------------------------------------------------------------------------------------------------------
// <copyright file="TestWindowBase.cs" company="Jarno Burger">
//   See copyright.txt in the root of this project.
// </copyright>
// <summary>
//   Defines the TestWindowBase type.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace GUI4UControls.Tests
{
    using GUI4UFramework.Management;

    using Window = GUI4UControls.Containers.Window;

    /// <summary>
    /// Creates a test window base class , that can be used to create test-windows..
    /// </summary>
    public class TestWindowBase : Window
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="TestWindowBase"/> class.
        /// </summary>
        /// <param name="name">The name.</param>
        protected TestWindowBase(string name)
            : base(name)
        {
            Config.Width = Theme.ControlWidth * 5;
            Config.Height = Theme.ControlHeight * 5;
        }

        /// <summary>
        /// Allows the game to run logic such as updating the world,
        /// checking for collisions, gathering input, and playing audio.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        public override void Update(GameTime gameTime)
        {
            // **** do the basic stuff
            base.Update(gameTime);

            if (Config.Changed)
            {
                this.RedrawSizeControl = true;
                this.RedrawTitleControl = true;

                this.UpdateDrawSourceRectangleByConfig();
                this.UpdateDrawPositionByConfigAndParent();
                this.UpdateDrawSizeByConfig();

                Manager.ImageCompositor.Delete(this.CurrentTextureName);
                this.CurrentTextureName = Manager.ImageCompositor.CreateRectangleTexture(
                                        this.Name,
                                        (int)State.Width,
                                        (int)State.Height,
                                        Theme.BorderWidth,
                                        Theme.WindowFillColor,
                                        Theme.BorderColor);
                Config.ResetChanged();
            }
        }
    }
}
