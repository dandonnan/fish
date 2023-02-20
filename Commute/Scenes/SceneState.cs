namespace Commute.Scenes
{
    /// <summary>
    /// The scene states.
    /// </summary>
    internal enum SceneState
    {
        /// <summary>
        /// Title screen.
        /// </summary>
        Title = 0,

        /// <summary>
        /// Fish select menu.
        /// </summary>
        FishSelect = 1,

        /// <summary>
        /// Main game.
        /// </summary>
        Playing = 2,

        /// <summary>
        /// Pause screen.
        /// </summary>
        Paused = 3,

        /// <summary>
        /// Game over screen.
        /// </summary>
        End = 4
    }
}
