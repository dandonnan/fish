namespace Commute.Input
{
    using Microsoft.Xna.Framework.Input;

    /// <summary>
    /// An input binding.
    /// </summary>
    public class InputBinding
    {
        /// <summary>
        /// The name of the binding.
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// The Keyboard key.
        /// </summary>
        public Keys Key { get; set; }

        /// <summary>
        /// The Gamepad button.
        /// </summary>
        public Buttons Button { get; set; }

        /// <summary>
        /// An alternative Keyboard key.
        /// </summary>
        public Keys AltKey { get; set; }

        /// <summary>
        /// An alternative Gamepad button.
        /// </summary>
        public Buttons AltButton { get; set; }

        /// <summary>
        /// A joystick binding (for controllers not detected properly using Gamepad).
        /// </summary>
        public JoystickBinding Joystick { get; set; }
    }
}
