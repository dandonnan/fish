namespace Commute.Save
{
    using Commute.Graphics;

    /// <summary>
    /// Graphical settings.
    /// </summary>
    public class GraphicsSettings
    {
        /// <summary>
        /// Whether the game is fullscreen.
        /// </summary>
        public bool Fullscreen => ScreenSize == (int)ScreenSizes.Fullscreen;

        /// <summary>
        /// Whether the screen is borderless.
        /// </summary>
        public bool Borderless => ScreenSize == (int)ScreenSizes.Borderless;

        /// <summary>
        /// The width of the screen resolution.
        /// </summary>
        public int ResolutionWidth => int.Parse(Resolution.Substring(0, Resolution.IndexOf('x')));

        /// <summary>
        /// The height of the screen resolution.
        /// </summary>
        public int ResolutionHeight => int.Parse(Resolution.Substring(Resolution.IndexOf('x') + 1));

        /// <summary>
        /// The screen resolution.
        /// </summary>
        public string Resolution { get; set; }

        /// <summary>
        /// The index of the screen size (matches with the ScreenSizes enum).
        /// </summary>
        public int ScreenSize { get; set; }
    }
}
