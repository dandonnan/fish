namespace Commute.Extensions
{
    using Microsoft.Xna.Framework;
    using Microsoft.Xna.Framework.Input.Touch;
    using System.Linq;

    /// <summary>
    /// Extension methods for the TouchCollection object.
    /// </summary>
    internal static class TouchCollectionExtensions
    {
        /// <summary>
        /// Get whether the touch screen was pressed.
        /// </summary>
        /// <param name="source">The touch collection.</param>
        /// <returns>true if pressed, false if not.</returns>
        public static bool IsPressed(this TouchCollection source)
        {
            bool pressed = false;

            if (source.Count > 0)
            {
                // Go through each part of the screen that was touched
                foreach (TouchLocation location in source)
                {
                    // If the screen was pressed, then stop and return true
                    if (location.State == TouchLocationState.Pressed)
                    {
                        pressed = true;
                        break;
                    }
                }
            }

            return pressed;
        }

        /// <summary>
        /// Get whether the touch screen was held.
        /// </summary>
        /// <param name="source">The touch collection.</param>
        /// <returns>true if held, false if not.</returns>
        public static bool IsHeld(this TouchCollection source)
        {
            bool held = false;

            if (source.Count > 0)
            {
                // Go through each part of the screen that was touched
                foreach (TouchLocation location in source)
                {
                    // If the screen was held, then stop and return true
                    if (location.State == TouchLocationState.Moved)
                    {
                        held = true;
                        break;
                    }
                }
            }

            return held;
        }

        /// <summary>
        /// Get whether the touch screen was released.
        /// </summary>
        /// <param name="source">The touch collection.</param>
        /// <returns>true if released, false if not.</returns>
        public static bool IsReleased(this TouchCollection source)
        {
            // Default released to true - it will change to false if touched
            bool released = true;

            if (source.Count > 0)
            {
                // Go through each part of the screen that was touched
                foreach (TouchLocation location in source)
                {
                    // If the screen was pressed, then stop and return false
                    if (location.State == TouchLocationState.Pressed)
                    {
                        released = false;
                        break;
                    }
                }
            }

            return released;
        }

        /// <summary>
        /// Get the position where the screen was touched.
        /// </summary>
        /// <param name="source">The touch collection.</param>
        /// <returns>The position where the screen was touched.</returns>
        public static Point GetTouchPosition(this TouchCollection source)
        {
            Point point = Point.Zero;

            // If there screen was touched, get the first place it was touched
            if (source.Count > 0)
            {
                point = source.First().Position.ToPoint();
            }

            return point;
        }
    }
}
