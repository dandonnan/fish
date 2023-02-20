namespace Commute.Input
{
    /// <summary>
    /// A joystick binding.
    /// </summary>
    public class JoystickBinding
    {
        /// <summary>
        /// The joystick type.
        /// </summary>
        public JoystickType Type { get; set; }

        /// <summary>
        /// The index of the input the joystick.
        /// </summary>
        public int Index { get; set; }

        /// <summary>
        /// An alternative type for a secondary binding.
        /// </summary>
        public JoystickType AltType { get; set; }

        /// <summary>
        /// The index of the alternative binding.
        /// </summary>
        public int AltIndex { get; set; }
    }
}
