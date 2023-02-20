namespace Commute.Save
{
    /// <summary>
    /// Settings for audio.
    /// </summary>
    public class AudioSettings
    {
        /// <summary>
        /// The default volume.
        /// </summary>
        public const int DefaultVolume = 7;

        /// <summary>
        /// The minimum volume.
        /// </summary>
        public const int MinVolume = 0;

        /// <summary>
        /// The maximum volume.
        /// </summary>
        public const int MaxVolume = 10;

        /// <summary>
        /// The volume of music.
        /// </summary>
        public int MusicVolume { get; set; }

        /// <summary>
        /// The volume of sound effects.
        /// </summary>
        public int SoundVolume { get; set; }
    }
}
