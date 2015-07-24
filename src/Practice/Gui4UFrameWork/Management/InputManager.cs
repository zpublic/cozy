// --------------------------------------------------------------------------------------------------------------------
// <copyright file="InputManager.cs" company="Jarno Burger">
//   See copyright.txt in the root of this project.
// </copyright>
// <summary>
//   Defines the InputManager type.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace GUI4UFramework.Management
{
    using GUI4UFramework.Graphics;

    /// <summary>
    /// This gives you utility functions towards keyboards/mouse/joysticks/touch etc.
    /// </summary>
    public abstract class InputManager
    {
        /// <summary>Reads the mouse location relative to the desktop coordinates.</summary>
        /// <returns>The mouse location contained in a DVector2.</returns>
        public abstract DVector2 ReadMouseLocation();

        /// <summary>Reads if the left mouse is pressed.</summary>
        /// <returns>True if pressed, otherwise false.</returns>
        public abstract bool ReadLeftMousePressed();

        /// <summary>
        /// Reads if left mouse is released.
        /// </summary>
        /// <returns>True if released, otherwise false.</returns>
        public abstract bool ReadLeftMouseReleased();

        /// <summary>
        /// Is call that the keyboard state must be read again towards the KeySwitchState.
        /// </summary>
        /// <param name="gameTime">The game time.</param>
        public abstract void UpdateKeyboardInput(GameTime gameTime);

        /// <summary>
        /// Reads the state of the keyboard, contained in a KeySwitches class.
        /// </summary>
        /// <returns>The current key switches.</returns>
        public abstract KeySwitches ReadKeySwitchState();
    }
}
