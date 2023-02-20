namespace Commute.Graphics
{
    /// <summary>
    /// A frame.
    /// </summary>
    internal class Frame
    {
        /// <summary>
        /// The x co-ordinate.
        /// </summary>
        public int X { get; set; }

        /// <summary>
        /// The y co-ordinate.
        /// </summary>
        public int Y { get; set; }

        /// <summary>
        /// The frame's width.
        /// </summary>
        public int Width { get; set; }

        /// <summary>
        /// The frame's height.
        /// </summary>
        public int Height { get; set; }

        /// <summary>
        /// Constructor for a frame.
        /// </summary>
        /// <param name="x">The x co-ordinate.</param>
        /// <param name="y">The y co-ordinate.</param>
        /// <param name="size">The frame's size (in both width and height).</param>
        public Frame(int x, int y, int size)
            : this(x, y, size, size)
        {
        }

        /// <summary>
        /// Constructor for a frame.
        /// </summary>
        /// <param name="x">The x co-ordinate.</param>
        /// <param name="y">The y co-ordinate.</param>
        /// <param name="width">The frame's width.</param>
        /// <param name="height">The frame's height.</param>
        public Frame(int x, int y, int width, int height)
        {
            X = x;
            Y = y;
            Width = width;
            Height = height;
        }
    }
}
