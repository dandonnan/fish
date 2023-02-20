namespace Commute.Input
{
    using Microsoft.Xna.Framework.Input;

    /// <summary>
    /// Default input bindings.
    /// </summary>
    internal class DefaultBindings
    {
        /// <summary>
        /// Binding for the Up input.
        /// </summary>
        public static InputBinding Up = new InputBinding
        {
            Key = Keys.W,
            Button = Buttons.LeftThumbstickUp,
            AltKey = Keys.Up,
            AltButton = Buttons.DPadUp,
            Joystick = new JoystickBinding
            {
                Type = JoystickType.AxisNegative,
                Index = 1,
                AltType = JoystickType.Hat,
                AltIndex = 1,
            }
        };

        /// <summary>
        /// Binding for the Down input.
        /// </summary>
        public static InputBinding Down = new InputBinding
        {
            Key = Keys.S,
            Button = Buttons.LeftThumbstickDown,
            AltKey = Keys.Down,
            AltButton = Buttons.DPadDown,
            Joystick = new JoystickBinding
            {
                Type = JoystickType.AxisPositive,
                Index = 1,
                AltType = JoystickType.Hat,
                AltIndex = 4,
            }
        };

        /// <summary>
        /// Binding for the Left input.
        /// </summary>
        public static InputBinding Left = new InputBinding
        {
            Key = Keys.A,
            Button = Buttons.LeftThumbstickLeft,
            AltKey = Keys.Left,
            AltButton = Buttons.DPadLeft,
            Joystick = new JoystickBinding
            {
                Type = JoystickType.AxisNegative,
                Index = 0,
                AltType = JoystickType.Hat,
                AltIndex = 8,
            }
        };

        /// <summary>
        /// Binding for the Right input.
        /// </summary>
        public static InputBinding Right = new InputBinding
        {
            Key = Keys.D,
            Button = Buttons.LeftThumbstickRight,
            AltKey = Keys.Right,
            AltButton = Buttons.DPadRight,
            Joystick = new JoystickBinding
            {
                Type = JoystickType.AxisPositive,
                Index = 0,
                AltType = JoystickType.Hat,
                AltIndex = 2,
            }
        };

        /// <summary>
        /// Binding for the Accept input.
        /// </summary>
        public static InputBinding Accept = new InputBinding
        {
            Key = Keys.Z,
            Button = Buttons.A,
            AltKey = Keys.Space,
            AltButton = Buttons.A,
            Joystick = new JoystickBinding
            {
                Type = JoystickType.Button,
                Index = 1,
                AltType = JoystickType.Button,
                AltIndex = 1,
            }
        };

        /// <summary>
        /// Binding for the Decline input.
        /// </summary>
        public static InputBinding Decline = new InputBinding
        {
            Key = Keys.X,
            Button = Buttons.B,
            AltKey = Keys.Back,
            AltButton = Buttons.B,
            Joystick = new JoystickBinding
            {
                Type = JoystickType.Button,
                Index = 2,
                AltType = JoystickType.Button,
                AltIndex = 2,
            }
        };

        /// <summary>
        /// Binding for the Pause input.
        /// </summary>
        public static InputBinding Pause = new InputBinding
        {
            Key = Keys.Escape,
            Button = Buttons.Start,
            AltKey = Keys.Escape,
            AltButton = Buttons.Start,
            Joystick = new JoystickBinding
            {
                Type = JoystickType.Button,
                Index = 13,
                AltType = JoystickType.Button,
                AltIndex = 13,
            }
        };
    }
}
