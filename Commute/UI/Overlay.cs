namespace Commute.UI
{
    using Commute.Extensions;
    using Microsoft.Xna.Framework;

    /// <summary>
    /// A screen overlay.
    /// </summary>
    internal class Overlay
    {
        /// <summary>
        /// The rectangle.
        /// </summary>
        private readonly Rectangle rectangle;

        /// <summary>
        /// The colour.
        /// </summary>
        private readonly Color colour;

        /// <summary>
        /// Create a new overlay.
        /// </summary>
        /// <param name="colour">The colour of the overlay.</param>
        public Overlay(Color? colour = null)
        {
            // Set the rectangle to the UI resolution
            rectangle = new Rectangle(0, 0, GameManager.UiResolutionWidth, GameManager.UiResolutionHeight);

            // Set the colour, or use a default
            this.colour = colour ?? new Color(50, 50, 50, 150);
        }

        /// <summary>
        /// Draw the overlay.
        /// </summary>
        public void Draw()
        {
            GameManager.SpriteBatch.FillRectangle(rectangle, colour);
        }
    }
}
