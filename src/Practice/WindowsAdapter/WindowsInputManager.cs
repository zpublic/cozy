// --------------------------------------------------------------------------------------------------------------------
// <copyright file="WindowsInputManager.cs" company="Jarno Burger">
//   See copyright.txt in the root of this project.
// </copyright>
// <summary>
//   Reads the inputs for programs that use wpf-windows.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace GUI4UWindows
{
    using GUI4UFramework.Graphics;
    using GUI4UFramework.Management;

    using GUI4UWindows.Keyboard;

    using Microsoft.Xna.Framework.Input;

    using ButtonState = Microsoft.Xna.Framework.Input.ButtonState;

    /// <summary>
    /// Reads the inputs for programs.
    /// </summary>
    public class WindowsInputManager : InputManager
    {
        /// <summary>The keyboard reader.</summary>
        private readonly KeyboardReader keyboardReader;

        /// <summary>
        /// Initializes a new instance of the <see cref="WindowsInputManager"/> class.
        /// </summary>
        public WindowsInputManager()
        {
            this.keyboardReader = new KeyboardReader();
        }

        /// <summary>
        /// Reads the mouse location relative to the desktop coordinates.
        /// </summary>
        /// <returns>
        /// The mouse location contained in a DVector2.
        /// </returns>
        public override DVector2 ReadMouseLocation()
        {
            var mouseState = Mouse.GetState();
            return new DVector2(mouseState.X, mouseState.Y);
        }

        /// <summary>
        /// Reads if the left mouse is pressed.
        /// </summary>
        /// <returns>
        /// True if pressed, otherwise false.
        /// </returns>
        public override bool ReadLeftMousePressed()
        {
            var mouseState = Mouse.GetState();
            var pressed = mouseState.LeftButton == ButtonState.Pressed;
            return pressed;
        }

        /// <summary>
        /// Reads if left mouse is released.
        /// </summary>
        /// <returns>
        /// True if released, otherwise false.
        /// </returns>
        public override bool ReadLeftMouseReleased()
        {
            var mouseState = Mouse.GetState();
            var released = mouseState.LeftButton == ButtonState.Released;
            return released;
        }

        /// <summary>
        /// Is call that the keyboard state must be read again towards the KeySwitchState.
        /// </summary>
        /// <param name="gameTime">The game time.</param>
        public override void UpdateKeyboardInput(GameTime gameTime)
        {
            this.keyboardReader.UpdateKeyboardInput(gameTime);
        }

        /// <summary>
        /// Reads the state of the keyboard, contained in a KeySwitches class.
        /// </summary>
        /// <returns>
        /// The current key switches.
        /// </returns>
        public override KeySwitches ReadKeySwitchState()
        {
            return this.keyboardReader.KeySwitches;
        }
    }
}
