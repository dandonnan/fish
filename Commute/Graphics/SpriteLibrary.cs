namespace Commute.Graphics
{
    using Microsoft.Xna.Framework.Graphics;
    using System.Collections.Generic;

    /// <summary>
    /// A sprite library.
    /// </summary>
    internal class SpriteLibrary
    {
        /// <summary>
        /// The singleton instance for the sprite library.
        /// </summary>
        private static SpriteLibrary spriteLibrary;

        /// <summary>
        /// The texture for objects.
        /// </summary>
        private readonly Texture2D objectsTexture;

        /// <summary>
        /// The texture for UI.
        /// </summary>
        private readonly Texture2D uiTexture;

        /// <summary>
        /// A dictionary of sprites.
        /// </summary>
        private readonly Dictionary<string, AbstractSprite> spriteDictionary;

        /// <summary>
        /// A private constructor.
        /// </summary>
        private SpriteLibrary()
        {
            // Load the textures
            objectsTexture = GameManager.LoadTexture("objects");

            uiTexture = GameManager.LoadTexture("ui");

            // Populate the sprite dictionary
            spriteDictionary = PopulateDictionary();

            spriteLibrary = this;
        }

        /// <summary>
        /// Initialise the sprite library
        /// </summary>
        public static void Initialise()
        {
            if (spriteLibrary == null)
            {
                new SpriteLibrary();
            }
        }

        /// <summary>
        /// Get a sprite.
        /// </summary>
        /// <param name="id">The id of the sprite.</param>
        /// <returns>The sprite.</returns>
        public static Sprite GetSprite(string id)
        {
            Sprite sprite = null;

            // If a sprite with the id exists in the dictionary, create a copy of that sprite
            if (spriteLibrary.spriteDictionary.TryGetValue(id, out AbstractSprite abstractSprite))
            {
                sprite = new Sprite((Sprite)abstractSprite);
            }

            return sprite;
        }

        /// <summary>
        /// Populate the sprite dictionary.
        /// </summary>
        /// <returns>The dictionary.</returns>
        private Dictionary<string, AbstractSprite> PopulateDictionary()
        {
            // Set ids against sprites which use specific textures and frames to determine what they show
            return new Dictionary<string, AbstractSprite>
            {
                { "GreyFish", new Sprite(objectsTexture, new Frame(1, 2, 117, 75)) },
                { "OrangeFish", new Sprite(objectsTexture, new Frame(118, 2, 171, 110)) },
                { "RedFish", new Sprite(objectsTexture, new Frame(294, 2, 195, 147)) },
                { "GreenFish", new Sprite(objectsTexture, new Frame(8, 149, 247, 172)) },
                { "BlueFish", new Sprite(objectsTexture, new Frame(257, 170, 276, 192)) },
                { "YellowFish", new Sprite(objectsTexture, new Frame(553, 2, 319, 226)) },
                { "Shark", new Sprite(objectsTexture, new Frame(641, 257, 357, 256)) },
                { "Bubble", new Sprite(objectsTexture, new Frame(19, 380, 32)) },
                { "Foreground1", new Sprite(objectsTexture, new Frame(17, 536, 414, 319)) },
                { "Foreground2", new Sprite(objectsTexture, new Frame(629, 557, 370, 315)) },
                { "Play", new Sprite(uiTexture, new Frame(1, 1, 256)) },
                { "Options", new Sprite(uiTexture, new Frame(258, 1, 256)) },
                { "Quit", new Sprite(uiTexture, new Frame(515, 1, 256)) },
                { "Restart", new Sprite(uiTexture, new Frame(1, 258, 256)) },
                { "Selected", new Sprite(uiTexture, new Frame(258, 258, 256)) },
                { "Pause", new Sprite(uiTexture, new Frame(530, 265, 128)) },
                { "Close", new Sprite(uiTexture, new Frame(530, 401, 128)) },
                { "SmallHighlight", new Sprite(uiTexture, new Frame(793, 555, 128)) },
                { "ArrowLeft", new Sprite(uiTexture, new Frame(775, 16, 50, 128)) },
                { "ArrowRight", new Sprite(uiTexture, new Frame(833, 16, 50, 128)) },
                { "ArrowLeftHighlight", new Sprite(uiTexture, new Frame(891, 16, 50, 128)) },
                { "ArrowRightHighlight", new Sprite(uiTexture, new Frame(949, 16, 50, 128)) },
                { "Music", new Sprite(uiTexture, new Frame(1, 546, 256)) },
                { "Sound", new Sprite(uiTexture, new Frame(259, 546, 256)) },
                { "Resolution", new Sprite(uiTexture, new Frame(517, 546, 256)) },
                { "Fullscreen", new Sprite(uiTexture, new Frame(679, 260, 256)) },
                { "FishMenu", new Sprite(uiTexture, new Frame(764, 761, 256)) },
                { "Notification", new Sprite(uiTexture, new Frame(797, 172, 64)) },
            };
        }
    }
}
