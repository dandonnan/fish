namespace Commute.Platforms
{
    /// <summary>
    /// A list of known platforms that could be supported.
    /// </summary>
    internal enum Platforms
    {
        /// <summary>
        /// PC build through the Steam launcher.
        /// </summary>
        Steam,

        /// <summary>
        /// PC build through the Epic Games launcher.
        /// </summary>
        Epic,

        /// <summary>
        /// PC build through the Windows Store.
        /// </summary>
        Windows,

        /// <summary>
        /// Standalone PC build released on Itch.io.
        /// </summary>
        Itch,

        /// <summary>
        /// Xbox Series X|S.
        /// </summary>
        XboxSeries,

        /// <summary>
        /// PS5.
        /// </summary>
        Playstation5,

        /// <summary>
        /// Nintendo Switch.
        /// </summary>
        Switch
    }
}
