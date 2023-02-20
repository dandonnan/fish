namespace Commute.Save
{
    using Commute.Platforms;

    /// <summary>
    /// Save data for the current machine.
    /// </summary>
    public class MachineSaveData
    {
        /// <summary>
        /// The graphical settings.
        /// </summary>
        public GraphicsSettings Graphics { get; set; }

        /// <summary>
        /// Create new machine data.
        /// </summary>
        public MachineSaveData()
        {
            SetToDefault();
        }

        /// <summary>
        /// Set values to defaults.
        /// </summary>
        private void SetToDefault()
        {
            Graphics = new GraphicsSettings
            {
                Resolution = PlatformManager.Platform.GetDefaultResolution(),
                ScreenSize = (int)PlatformManager.Platform.GetDefaultScreenSize(),
            };
        }
    }
}
