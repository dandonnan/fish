namespace Commute.Input
{
    /// <summary>
    /// A type of joystick input.
    /// </summary>
    public enum JoystickType
    {
        /// <summary>
        /// A button.
        /// </summary>
        Button,

        /// <summary>
        /// A hat (multi-input).
        /// </summary>
        Hat,
        
        /// <summary>
        /// A positive axis on a thumbstick.
        /// </summary>
        AxisPositive,

        /// <summary>
        /// A negative axis on a thumbstick.
        /// </summary>
        AxisNegative,
    }
}
