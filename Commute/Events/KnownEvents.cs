namespace Commute.Events
{
    /// <summary>
    /// A set of known game events.
    /// </summary>
    internal class KnownEvents
    {
        /// <summary>
        /// Fired when the game is closed.
        /// </summary>
        public const string CloseGame = "CloseGame";

        /// <summary>
        /// Fired when the pause menu is closed.
        /// </summary>
        public const string ClosePauseMenu = "ClosePauseMenu";

        /// <summary>
        /// Fired when the options menu is closed.
        /// </summary>
        public const string CloseOptionsMenu = "CloseOptionsMenu";

        /// <summary>
        /// Fired when the game is restarted.
        /// </summary>
        public const string Restart = "Restart";

        /// <summary>
        /// Fired when the fish select screen is opened.
        /// </summary>
        public const string OpenFishSelect = "OpenFishSelect";

        /// <summary>
        /// Fired when the fish select screen is closed.
        /// </summary>
        public const string CloseFishSelect = "CloseFishSelect";

        /// <summary>
        /// Fired when the player loses.
        /// </summary>
        public const string GameOver = "GameOver";
    }
}
