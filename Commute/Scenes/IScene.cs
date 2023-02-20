namespace Commute.Scenes
{
    using Microsoft.Xna.Framework;

    /// <summary>
    /// An interface for a scene.
    /// </summary>
    internal interface IScene
    {
        /// <summary>
        /// Dispose the scene.
        /// </summary>
        void Dispose();

        /// <summary>
        /// Update the scene.
        /// </summary>
        /// <param name="gameTime">The game time.</param>
        void Update(GameTime gameTime);

        /// <summary>
        /// Draw the scene.
        /// </summary>
        void Draw();

        /// <summary>
        /// Draw the UI layer.
        /// </summary>
        void DrawUi();
    }
}
