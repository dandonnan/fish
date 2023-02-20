namespace Commute
{
    using Commute.Audio;
    using Commute.Events;
    using Commute.Graphics;
    using Commute.Input;
    using Commute.Localisation;
    using Commute.Platforms;
    using Commute.Save;
    using Commute.Scenes;
    using Microsoft.Xna.Framework;
    using Microsoft.Xna.Framework.Audio;
    using Microsoft.Xna.Framework.Content;
    using Microsoft.Xna.Framework.Graphics;
    using Microsoft.Xna.Framework.Media;

    /// <summary>
    /// The game manager.
    /// </summary>
    public class GameManager
    {
        /// <summary>
        /// The width of the game's base resolution.
        /// </summary>
        public const int BaseResolutionWidth = 1920;

        /// <summary>
        /// The height of the game's base resolution.
        /// </summary>
        public const int BaseResolutionHeight = 1080;

        /// <summary>
        /// The width of the game's UI resolution.
        /// </summary>
        public const int UiResolutionWidth = 1920;

        /// <summary>
        /// The height of the game's UI resolution.
        /// </summary>
        public const int UiResolutionHeight = 1080;

        /// <summary>
        /// The singleton instance of the game manager.
        /// </summary>
        private static GameManager gameManager;

        /// <summary>
        /// The game.
        /// </summary>
        private readonly Game game;

        /// <summary>
        /// The content manager for loading assets.
        /// </summary>
        private readonly ContentManager contentManager;

        /// <summary>
        /// The graphics device.
        /// </summary>
        private readonly GraphicsDevice graphicsDevice;

        /// <summary>
        /// The graphics device manager.
        /// </summary>
        private readonly GraphicsDeviceManager graphicsDeviceManager;

        /// <summary>
        /// The sprite batch for drawing on screen.
        /// </summary>
        private readonly SpriteBatch spriteBatch;

        /// <summary>
        /// The input manager.
        /// </summary>
        private readonly InputManager inputManager;

        /// <summary>
        /// The game event manager.
        /// </summary>
        private readonly EventManager eventManager;

        /// <summary>
        /// The current scene.
        /// </summary>
        private IScene currentScene;

        /// <summary>
        /// The render target to draw to.
        /// </summary>
        private RenderTarget2D renderTarget;

        /// <summary>
        /// A matrix used for rendering the game.
        /// </summary>
        private Matrix renderMatrix;

        /// <summary>
        /// A matrix used for rendering the UI.
        /// </summary>
        private Matrix uiMatrix;

        /// <summary>
        /// The scale of the UI.
        /// </summary>
        private Vector2 uiScale;

        /// <summary>
        /// Create a game manager.
        /// </summary>
        /// <param name="game">The game object.</param>
        /// <param name="contentManager">The content manager.</param>
        /// <param name="graphicsDevice">The graphics device.</param>
        /// <param name="graphicsDeviceManager">The graphics device manager.</param>
        /// <param name="spriteBatch">The sprite batch.</param>
        public GameManager(Game game,
                            ContentManager contentManager,
                            GraphicsDevice graphicsDevice,
                            GraphicsDeviceManager graphicsDeviceManager,
                            SpriteBatch spriteBatch)
        {
            this.game = game;
            this.contentManager = contentManager;
            this.graphicsDevice = graphicsDevice;
            this.graphicsDeviceManager = graphicsDeviceManager;
            this.spriteBatch = spriteBatch;

            gameManager = this;

            // Initialise the save manager
            SaveManager.Initialise();

            // Change the screen settings so that the window can change now the data has loaded
            ChangeScreenSettings();

            // Initialise other managers
            inputManager = InputManager.Initialise();
            eventManager = EventManager.Initialise();

            AudioManager.Initialise();
            AudioLibrary.Initialise();
            SpriteLibrary.Initialise();
            StringLibrary.Initialise();

            // Setup the render target
            SetupRenderTarget();

            // Add a method to call whenever an event is fired
            eventManager.OnEventFired += EventManager_OnEventFired;

            // Setup the scene
            currentScene = new MainScene();

            // Play the background music
            AudioManager.PlayMusic("BiggerFish");
        }

        /// <summary>
        /// The content manager.
        /// </summary>
        public static ContentManager ContentManager => gameManager.contentManager;

        /// <summary>
        /// The graphics device manager.
        /// </summary>
        public static GraphicsDeviceManager GraphicsDeviceManager => gameManager.graphicsDeviceManager;

        /// <summary>
        /// The sprite batch.
        /// </summary>
        public static SpriteBatch SpriteBatch => gameManager.spriteBatch;

        /// <summary>
        /// The UI scale.
        /// </summary>
        public static Vector2 UiScale => gameManager.uiScale;

        /// <summary>
        /// Load a font.
        /// </summary>
        /// <param name="fontName">The name of the font.</param>
        /// <returns>The font.</returns>
        public static SpriteFont LoadFont(string fontName)
        {
            return Load<SpriteFont>(fontName);
        }

        /// <summary>
        /// Load a texture.
        /// </summary>
        /// <param name="texture">The name of the texture.</param>
        /// <returns>The texture.</returns>
        public static Texture2D LoadTexture(string texture)
        {
            return Load<Texture2D>(texture);
        }

        /// <summary>
        /// Load a sound effect.
        /// </summary>
        /// <param name="sound">The name of the sound effect.</param>
        /// <returns>A sound effect.</returns>
        public static SoundEffect LoadSound(string sound)
        {
            return Load<SoundEffect>($"Audio\\{sound}");
        }

        /// <summary>
        /// Load a music track.
        /// </summary>
        /// <param name="music">The name of the track.</param>
        /// <returns>The music track.</returns>
        public static Song LoadMusic(string music)
        {
            return Load<Song>($"Audio\\{music}");
        }

        /// <summary>
        /// Load a content file.
        /// </summary>
        /// <typeparam name="T">The type of content.</typeparam>
        /// <param name="fileName">The name of the file.</param>
        /// <returns>The content.</returns>
        public static T Load<T>(string fileName)
        {
            return ContentManager.Load<T>(fileName);
        }

        /// <summary>
        /// Change the screen settings.
        /// </summary>
        public static void ChangeScreenSettings()
        {
            // If the platform is on PC
            if (PlatformManager.Platform.IsPC())
            {
                // Setup fullscreen and borderless depending on save data
                GraphicsDeviceManager.HardwareModeSwitch = !SaveManager.MachineData.Graphics.Borderless;

                gameManager.game.Window.IsBorderless = SaveManager.MachineData.Graphics.Borderless;

                GraphicsDeviceManager.IsFullScreen = SaveManager.MachineData.Graphics.Borderless || SaveManager.MachineData.Graphics.Fullscreen;
            }

            // If the platform is mobile, always use fullscreen
            if (PlatformManager.Platform.IsMobile())
            {
                GraphicsDeviceManager.IsFullScreen = true;
            }

            // Set the screen size
            if (SaveManager.MachineData.Graphics.Borderless)
            {
                GraphicsDeviceManager.PreferredBackBufferWidth = SpriteBatch.GraphicsDevice.DisplayMode.Width;
                GraphicsDeviceManager.PreferredBackBufferHeight = SpriteBatch.GraphicsDevice.DisplayMode.Height;
            }
            else
            {
                GraphicsDeviceManager.PreferredBackBufferWidth = SaveManager.MachineData.Graphics.ResolutionWidth;
                GraphicsDeviceManager.PreferredBackBufferHeight = SaveManager.MachineData.Graphics.ResolutionHeight;
            }

            // Apply changes to the graphics device manager
            GraphicsDeviceManager.ApplyChanges();

            // Scale the render target using the new settings
            gameManager.ScaleRenderTarget();
        }

        /// <summary>
        /// Update the game.
        /// </summary>
        /// <param name="gameTime">The game time.</param>
        public void Update(GameTime gameTime)
        {
            // Update managers
            PlatformManager.Platform.Update();

            inputManager.Update();
            eventManager.Update();

            // Update the current scene
            currentScene.Update(gameTime);
        }

        /// <summary>
        /// Draw the game.
        /// </summary>
        public void Draw()
        {
            DrawToRenderTarget();
            DrawToScreen();
        }

        /// <summary>
        /// Setup the render target.
        /// </summary>
        private void SetupRenderTarget()
        {
            renderTarget = new RenderTarget2D(
                                    graphicsDevice,
                                    BaseResolutionWidth,
                                    BaseResolutionHeight,
                                    false,
                                    graphicsDevice.PresentationParameters.BackBufferFormat,
                                    DepthFormat.Depth24);

            ScaleRenderTarget();
        }

        /// <summary>
        /// Scale the render target based on resolution.
        /// </summary>
        private void ScaleRenderTarget()
        {
            // Get the scale in each axis by dividing the window size by the base resolution
            float scaleX = game.Window.ClientBounds.Width / (float)BaseResolutionWidth;
            float scaleY = game.Window.ClientBounds.Height / (float)BaseResolutionHeight;

            // Set the matrix based on the scale
            renderMatrix = Matrix.CreateScale(scaleX, scaleY, 1);

            // Get the scale of the UI
            scaleX = game.Window.ClientBounds.Width / (float)UiResolutionWidth;
            scaleY = game.Window.ClientBounds.Height / (float)UiResolutionHeight;

            // Set the matrix and UI scale based on the scales
            uiMatrix = Matrix.CreateScale(scaleX, scaleY, 1);
            uiScale = new Vector2(scaleX, scaleY);
        }

        /// <summary>
        /// Draw the game to the render target. The render target can then be
        /// scaled up or down based on resolution without any of the game's objects
        /// needing to worry about handling scale.
        /// </summary>
        private void DrawToRenderTarget()
        {
            // Set the render target and then clear the graphics device
            graphicsDevice.SetRenderTarget(renderTarget);

            graphicsDevice.Clear(new Color(5, 79, 190));

            // Start the sprite batch so it is ready for drawing
            spriteBatch.Begin();

            // Draw the scene
            currentScene.Draw();

            // Stop the sprite batch so no more drawing can be done
            spriteBatch.End();

            graphicsDevice.SetRenderTarget(null);
        }

        /// <summary>
        /// Draw the game to the screen.
        /// </summary>
        private void DrawToScreen()
        {
            // Clear the graphics device
            graphicsDevice.Clear(Color.Black);

            // Start drawing using the render matrix
            spriteBatch.Begin(transformMatrix: renderMatrix);

            // Draw the render target
            spriteBatch.Draw(renderTarget, Vector2.Zero, Color.White);

            spriteBatch.End();

            // Start drawing using the UI matrix
            spriteBatch.Begin(transformMatrix: uiMatrix);

            // Draw the UI
            currentScene.DrawUi();

            spriteBatch.End();
        }

        /// <summary>
        /// Called when an event is fired.
        /// </summary>
        /// <param name="gameEvent">The event that was fired.</param>
        private void EventManager_OnEventFired(GameEvent gameEvent)
        {
            // Check against the event name
            switch (gameEvent.Name)
            {
                // Close the game if the close event was fired
                case KnownEvents.CloseGame:
                    PlatformManager.Platform.Stop();
                    game.Exit();
                    break;

                default:
                    break;
            }
        }
    }
}
