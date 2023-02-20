namespace CommuteGL
{
    using Commute;
    using Commute.Localisation;
    using Commute.Platforms;
    using CommuteGL.Platforms;
    using Microsoft.Xna.Framework;
    using Microsoft.Xna.Framework.Graphics;

    /// <summary>
    /// The game implementation for Itch.io.
    /// </summary>
    public class MainGame : Game
    {
        /// <summary>
        /// The singleton instance.
        /// </summary>
        private static MainGame game;

        /// <summary>
        /// The graphics device manager.
        /// </summary>
        private GraphicsDeviceManager graphicsDeviceManager;

        /// <summary>
        /// The sprite batch.
        /// </summary>
        private SpriteBatch spriteBatch;

        /// <summary>
        /// The game manager.
        /// </summary>
        private GameManager gameManager;

        /// <summary>
        /// The constructor.
        /// </summary>
        public MainGame()
        {
            game = this;
            graphicsDeviceManager = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
            IsMouseVisible = true;
        }

        /// <summary>
        /// Close the game.
        /// </summary>
        public static void Close()
        {
            game.Exit();
        }

        /// <summary>
        /// Initialise the game.
        /// </summary>
        protected override void Initialize()
        {
            PlatformManager.Initialise(new ItchPlatform(GraphicsDevice));

            base.Initialize();
        }

        /// <summary>
        /// Load the game's content.
        /// </summary>
        protected override void LoadContent()
        {
            spriteBatch = new SpriteBatch(GraphicsDevice);

            gameManager = new GameManager(this, Content, GraphicsDevice, graphicsDeviceManager, spriteBatch);

            Window.Title = StringLibrary.GetString("GameTitle");
        }

        /// <summary>
        /// Update the game's objects. Called each frame.
        /// </summary>
        /// <param name="gameTime">A tracker for how long the game has been running.</param>
        protected override void Update(GameTime gameTime)
        {
            gameManager.Update(gameTime);

            base.Update(gameTime);
        }

        /// <summary>
        /// Draws the game. Called each frame.
        /// </summary>
        /// <param name="gameTime">A tracker for how long the game has been running.</param>
        protected override void Draw(GameTime gameTime)
        {
            gameManager.Draw();

            base.Draw(gameTime);
        }
    }
}
