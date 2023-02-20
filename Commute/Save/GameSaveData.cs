namespace Commute.Save
{
    /// <summary>
    /// The game's save data.
    /// </summary>
    public class GameSaveData
    {
        /// <summary>
        /// The number of unlockable fish.
        /// </summary>
        public const int UnlockableFish = 7;

        /// <summary>
        /// The audio settings.
        /// </summary>
        public AudioSettings Audio { get; set; }

        /// <summary>
        /// The total number of fish eaten.
        /// </summary>
        public int FishEaten { get; set; }

        /// <summary>
        /// The best scale the player has reached.
        /// </summary>
        public int BestScale { get; set; }

        /// <summary>
        /// The best points the player has scored.
        /// </summary>
        public int BestPoints { get; set; }

        /// <summary>
        /// The index of the currently selected fish.
        /// </summary>
        public int CurrentFish { get; set; }

        /// <summary>
        /// An array determining which fish have been unlocked.
        /// </summary>
        public bool[] UnlockedFish { get; set; }
            = new bool[UnlockableFish];

        /// <summary>
        /// An array determining which notifications have been seen
        /// when new fish are unlocked.
        /// </summary>
        public bool[] Notifications { get; set; }
            = new bool[UnlockableFish];

        /// <summary>
        /// Create new save data.
        /// </summary>
        public GameSaveData()
        {
            SetToDefault();
        }

        /// <summary>
        /// Set to default values.
        /// </summary>
        private void SetToDefault()
        {
            Audio = new AudioSettings
            {
                MusicVolume = AudioSettings.DefaultVolume,
                SoundVolume = AudioSettings.DefaultVolume,
            };

            FishEaten = 0;

            BestScale = 0;

            BestPoints = 0;

            CurrentFish = 0;

            // Unlock the first fish
            UnlockedFish[0] = true;
        }
    }
}
