namespace Commute.Audio
{
    using Commute.Save;
    using Microsoft.Xna.Framework.Audio;
    using Microsoft.Xna.Framework.Media;

    /// <summary>
    /// An audio manager.
    /// </summary>
    internal class AudioManager
    {
        /// <summary>
        /// The singleton instance of the audio manager.
        /// </summary>
        private static AudioManager audioManager;

        /// <summary>
        /// The currently playing song.
        /// </summary>
        private Song currentSong;

        /// <summary>
        /// A private constructor.
        /// </summary>
        private AudioManager()
        {
            audioManager = this;
        }

        /// <summary>
        /// Initialise the audio manager.
        /// </summary>
        public static void Initialise()
        {
            if (audioManager == null)
            {
                new AudioManager();
            }
        }

        /// <summary>
        /// Play a sound effect.
        /// </summary>
        /// <param name="id">The id of the sound effect.</param>
        public static void PlaySoundEffect(string id)
        {
            // Get the sound from the audio library
            SoundEffect soundEffect = AudioLibrary.GetSoundEffect(id);

            // If a sound was found, then play it
            if (soundEffect != null)
            {
                soundEffect.Play(GetSoundVolume(), 0, 0);
            }
        }

        /// <summary>
        /// Play a music track.
        /// </summary>
        /// <param name="music">The name of the music track.</param>
        public static void PlayMusic(string music)
        {
            // Stop the current track
            StopMusic();

            // Get the song from the track name
            Song song = GameManager.LoadMusic(music);

            // Set the current song
            audioManager.currentSong = song;

            // Make the song loop and set the volume before playing it
            MediaPlayer.IsRepeating = true;

            MediaPlayer.Volume = GetMusicVolume();

            MediaPlayer.Play(song);
        }

        /// <summary>
        /// Stop all music.
        /// </summary>
        public static void StopMusic()
        {
            // If a song is playing then stop it
            if (audioManager.currentSong != null)
            {
                MediaPlayer.Stop();
            }
        }

        /// <summary>
        /// Change the volume of all sounds.
        /// </summary>
        public static void ChangeVolume()
        {
            // Set the volume for music
            MediaPlayer.Volume = GetMusicVolume();
        }

        /// <summary>
        /// Get the current volume for music tracks.
        /// </summary>
        /// <returns>The music volume.</returns>
        private static float GetMusicVolume()
        {
            return (float)SaveManager.GameData.Audio.MusicVolume / 10;
        }

        /// <summary>
        /// Get the current volume for sound effects.
        /// </summary>
        /// <returns>The sound volume.</returns>
        private static float GetSoundVolume()
        {
            return (float)SaveManager.GameData.Audio.SoundVolume / 10;
        }
    }
}
