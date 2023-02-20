namespace Commute.Extensions
{
    using Commute.Graphics;
    using Microsoft.Xna.Framework;

    /// <summary>
    /// Extension methods for the Frame object.
    /// </summary>
    internal static class FrameExtensions
    {
        /// <summary>
        /// Convert a Frame into a MonoGame Rectangle.
        /// </summary>
        /// <param name="source">The frame.</param>
        /// <returns>A MonoGame Rectangle.</returns>
        public static Rectangle ToRectangle(this Frame source)
        {
            return new Rectangle(source.X, source.Y, source.Width, source.Height);
        }

        /// <summary>
        /// Copy the Frame.
        /// </summary>
        /// <param name="source">The frame.</param>
        /// <returns>A new frame object with matching values.</returns>
        public static Frame Copy(this Frame source)
        {
            return new Frame(source.X, source.Y, source.Width, source.Height);
        }
    }
}
