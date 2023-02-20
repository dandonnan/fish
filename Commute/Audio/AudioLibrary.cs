namespace Commute.Audio
{
    using Microsoft.Xna.Framework.Audio;
    using System.Collections.Generic;

    /// <summary>
    /// A library for audio files.
    /// </summary>
    internal class AudioLibrary
    {
        /// <summary>
        /// The singleton instance of the audio library.
        /// </summary>
        private static AudioLibrary audioLibrary;

        /// <summary>
        /// A dictionary containing sound effects.
        /// </summary>
        private readonly Dictionary<string, SoundEffect> soundEffects;

        /// <summary>
        /// A private constructor.
        /// </summary>
        private AudioLibrary()
        {
            // Set up the dictionary with ids and their associated sounds
            soundEffects = new Dictionary<string, SoundEffect>
            {
                { "Bubble", GameManager.LoadSound("bubble_burst") },
                { "Eat1", GameManager.LoadSound("eat1") },
                { "Eat2", GameManager.LoadSound("eat2") },
                { "MenuConfirm", GameManager.LoadSound("menu_confirm") },
                { "MenuBack", GameManager.LoadSound("menu_back") },
                { "MenuMove", GameManager.LoadSound("menu_move") },
                { "Scale", GameManager.LoadSound("scale_up") },
                { "Multiplier", GameManager.LoadSound("point_multiplier") },
                { "GameOver", GameManager.LoadSound("game_over") }
            };

            audioLibrary = this;
        }

        /// <summary>
        /// Initialise the audio library.
        /// </summary>
        public static void Initialise()
        {
            if (audioLibrary == null)
            {
                new AudioLibrary();
            }
        }

        /// <summary>
        /// Get a sound effect.
        /// </summary>
        /// <param name="id">The id of the sound effect.</param>
        /// <returns>The sound effect.</returns>
        public static SoundEffect GetSoundEffect(string id)
        {
            audioLibrary.soundEffects.TryGetValue(id, out SoundEffect soundEffect);

            return soundEffect;
        }
    }
}
