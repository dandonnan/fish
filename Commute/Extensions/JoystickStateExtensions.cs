namespace Commute.Extensions
{
    using Commute.Input;
    using Microsoft.Xna.Framework.Input;

    /// <summary>
    /// Extension methods for the JoystickState object.
    /// </summary>
    internal static class JoystickStateExtensions
    {
        /// <summary>
        /// Get whether a controller binding was pressed.
        /// </summary>
        /// <param name="source">The controller state.</param>
        /// <param name="binding">The binding to check.</param>
        /// <returns>true if the controller was pressed, false if not.</returns>
        internal static bool IsPressed(this JoystickState source, JoystickBinding binding)
        {
            // Check both the binding's default and alternative indexes
            return source.IsPressed(binding.Type, binding.Index) || source.IsPressed(binding.AltType, binding.AltIndex);
        }

        /// <summary>
        /// Get whether a controller was pressed.
        /// </summary>
        /// <param name="source">The controller state.</param>
        /// <param name="type">The type of button.</param>
        /// <param name="index">The index of the button.</param>
        /// <returns>true if pressed, false if not.</returns>
        private static bool IsPressed(this JoystickState source, JoystickType type, int index)
        {
            bool pressed = false;

            // If a controller is connected
            if (source.IsConnected == true)
            {
                // Go to a specific state depending on the button's type
                switch (type)
                {
                    // If it was a button, check against the ButtonState
                    case JoystickType.Button:
                        if (index < source.Buttons.Length)
                        {
                            pressed = source.Buttons[index] == ButtonState.Pressed;
                        }
                        break;

                    // If it was a button press on a multi-state input (hat)
                    case JoystickType.Hat:
                        pressed = source.IsHatPressed(index);
                        break;

                    // If it was a thumbstick axis in the positive direction
                    case JoystickType.AxisPositive:
                        if (index < source.Axes.Length)
                        {
                            pressed = source.Axes[index] > 10000;
                        }
                        break;

                    // If it was a thumbstick axis in the negative direction
                    case JoystickType.AxisNegative:
                        if (index < source.Axes.Length)
                        {
                            pressed = source.Axes[index] < -10000;
                        }
                        break;
                }
            }

            return pressed;
        }

        /// <summary>
        /// Get whether a multi-state input (hat) was pressed.
        /// </summary>
        /// <param name="source">The controller state.</param>
        /// <param name="index">The index of the hat.</param>
        /// <returns>true if pressed, false if not.</returns>
        private static bool IsHatPressed(this JoystickState source, int index)
        {
            bool pressed = false;

            // Check the button state depending on the specific index
            switch (index)
            {
                case 1:
                    pressed = source.Hats[0].Up == ButtonState.Pressed;
                    break;

                case 2:
                    pressed = source.Hats[0].Right == ButtonState.Pressed;
                    break;

                case 4:
                    pressed = source.Hats[0].Down == ButtonState.Pressed;
                    break;

                case 8:
                    pressed = source.Hats[0].Left == ButtonState.Pressed;
                    break;

                default:
                    break;
            }

            return pressed;
        }
    }
}
