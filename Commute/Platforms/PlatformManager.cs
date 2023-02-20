namespace Commute.Platforms
{
    /// <summary>
    /// A platform manager.
    /// </summary>
    public class PlatformManager
    {
        /// <summary>
        /// The singleton instance of the platform manager.
        /// </summary>
        private static PlatformManager platformManager;

        /// <summary>
        /// The current platform.
        /// </summary>
        private readonly IPlatform platform;

        /// <summary>
        /// Private constructor for the manager.
        /// </summary>
        /// <param name="platform">The current platform.</param>
        private PlatformManager(IPlatform platform)
        {
            this.platform = platform;

            platformManager = this;
        }

        /// <summary>
        /// Initialise the platform manager with the given platform.
        /// </summary>
        /// <param name="platform">The platform.</param>
        public static void Initialise(IPlatform platform)
        {
            if (platformManager == null)
            {
                new PlatformManager(platform);
            }
        }

        /// <summary>
        /// The current platform.
        /// </summary>
        public static IPlatform Platform => platformManager.platform;
    }
}
