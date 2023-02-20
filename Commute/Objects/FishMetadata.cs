namespace Commute.Objects
{
    using Commute.Graphics;

    /// <summary>
    /// Metadata for fish.
    /// </summary>
    internal class FishMetadata
    {
        /// <summary>
        /// The sprite to use.
        /// </summary>
        public Sprite Sprite { get; set; }

        /// <summary>
        /// The starting scale for the fish.
        /// </summary>
        public float BaseScale { get; set; }

        /// <summary>
        /// The number of fish that need to be eaten to unlock this fish.
        /// </summary>
        public int UnlockEatFish { get; set; }

        /// <summary>
        /// The scale needed to be reached to unlock this fish.
        /// </summary>
        public int UnlockScale { get; set; }

        /// <summary>
        /// The points required to unlock this fish.
        /// </summary>
        public int UnlockPoints { get; set; }
    }
}
