using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Starbound.Input
{
    /// <summary>
    /// An enumeration of all of the buttons that are available on a game pad. This is more broad than
    /// the Microsoft.Xna.Input.Buttons enumeration; this adds in buttons for trigger presses and thumbstick
    /// presses in each of the four directions.
    /// </summary>
    public enum Buttons
    {
        /// <summary>
        /// Represents the A button.
        /// </summary>
        A,

        /// <summary>
        /// Represents the B button.
        /// </summary>
        B,

        /// <summary>
        /// Represents the X button.
        /// </summary>
        X,

        /// <summary>
        /// Represents the Y button.
        /// </summary>
        Y,

        /// <summary>
        /// Represents the Start button.
        /// </summary>
        Start,

        /// <summary>
        /// Represents the Back button.
        /// </summary>
        Back,

        /// <summary>
        /// Represents the left shoulder button on the top of the controller.
        /// </summary>
        LeftShoulder,

        /// <summary>
        /// Represents the right shoulder button on the top of the controller.
        /// </summary>
        RightShoulder,

        /// <summary>
        /// Reperesents the up button on the D-pad (directional pad).
        /// </summary>
        DPadUp,

        /// <summary>
        /// Represents the down button on the D-pad (directional pad).
        /// </summary>
        DPadDown,

        /// <summary>
        /// Represents the left button on the D-pad (directional pad).
        /// </summary>
        DPadLeft,

        /// <summary>
        /// Represents the right button on the D-pad (directional pad).
        /// </summary>
        DPadRight,

        /// <summary>
        /// Represents the left trigger being pressed in.
        /// </summary>
        LeftTrigger,

        /// <summary>
        /// Represents the right trigger being pressed in.
        /// </summary>
        RightTrigger,

        /// <summary>
        /// Represents the left thumbstick being moved to the left.
        /// </summary>
        LeftThumbstickLeft,

        /// <summary>
        /// Represents the left thumbstick being moved to the right.
        /// </summary>
        LeftThumbstickRight,

        /// <summary>
        /// Represents the left thumbstick being moved up.
        /// </summary>
        LeftThumbstickUp,

        /// <summary>
        /// Represents the left thumbstick being moved down.
        /// </summary>
        LeftThumbstickDown,

        /// <summary>
        /// Represents the left thumbstick as a button.
        /// </summary>
        LeftThumbstick,

        /// <summary>
        /// Represents the right thumbstick being moved left.
        /// </summary>
        RightThumbstickLeft,

        /// <summary>
        /// Represents the right thumbstick being moved right.
        /// </summary>
        RightThumbstickRight,

        /// <summary>
        /// Represents the right thumbstick being moved up.
        /// </summary>
        RightThumbstickUp,

        /// <summary>
        /// Represents the right thumbstick being moved down.
        /// </summary>
        RightThumbstickDown,

        /// <summary>
        /// Represents the right thumbstick as a button.
        /// </summary>
        RightThumbstick
    }
}
