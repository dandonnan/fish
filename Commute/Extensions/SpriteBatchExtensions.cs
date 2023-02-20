namespace Commute.Extensions
{
    using Microsoft.Xna.Framework;
    using Microsoft.Xna.Framework.Graphics;

    /// <summary>
    /// Extension methods for the SpriteBatch object.
    /// </summary>
    internal static class SpriteBatchExtensions
    {
        /// <summary>
        /// A blank texture.
        /// </summary>
        private static Texture2D blankTexture;

        /// <summary>
        /// Draw a rectangle's outline.
        /// </summary>
        /// <param name="source">The sprite batch.</param>
        /// <param name="rectangle">The rectangle to draw.</param>
        /// <param name="colour">The colour of the rectangle.</param>
        /// <param name="lineWidth">The width of the line.</param>
        public static void DrawRectangle(this SpriteBatch source, Rectangle rectangle, Color colour, int lineWidth = 1)
        {
            // Draw a line from each of the corners
            source.Draw(GetBlankTexture(Color.White), new Rectangle(rectangle.Left, rectangle.Top, rectangle.Width, lineWidth), colour);
            source.Draw(GetBlankTexture(Color.White), new Rectangle(rectangle.Left, rectangle.Bottom, rectangle.Width, lineWidth), colour);
            source.Draw(GetBlankTexture(Color.White), new Rectangle(rectangle.Left, rectangle.Top, lineWidth, rectangle.Height), colour);
            source.Draw(GetBlankTexture(Color.White), new Rectangle(rectangle.Right, rectangle.Top, lineWidth, rectangle.Height + lineWidth), colour);
        }

        /// <summary>
        /// Draw a filled rectangle.
        /// </summary>
        /// <param name="source">The sprite batch.</param>
        /// <param name="rectangle">The rectangle.</param>
        /// <param name="colour">The colour of the rectangle.</param>
        public static void FillRectangle(this SpriteBatch source, Rectangle rectangle, Color colour)
        {
            source.Draw(GetBlankTexture(Color.White), rectangle, rectangle, colour, 0, Vector2.Zero, SpriteEffects.None, 1);
        }

        /// <summary>
        /// Get a blank texture to draw with.
        /// </summary>
        /// <param name="colour">The colour of the texture.</param>
        /// <returns>A blank texture.</returns>
        private static Texture2D GetBlankTexture(Color colour)
        {
            // If no texture exists, then create one
            if (blankTexture == null)
            {
                blankTexture = new Texture2D(GameManager.SpriteBatch.GraphicsDevice, 1, 1);
                blankTexture.SetData(new Color[] { colour });
            }

            return blankTexture;
        }
    }
}
