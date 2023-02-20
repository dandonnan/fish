namespace Commute.Platforms
{
    using Commute.Graphics;
	using Commute.Save;
    using Microsoft.Xna.Framework;
    using System.Collections.Generic;

    /// <summary>
    /// An interface for a platform.
    /// </summary>
    public interface IPlatform
    {
        /// <summary>
        /// Update method to be called each frame.
        /// </summary>
        void Update();

        /// <summary>
        /// Called when the game is closed.
        /// </summary>
        void Stop();

        /// <summary>
        /// Get the name of the platform.
        /// </summary>
        /// <returns>The name of the platform.</returns>
        string GetPlatformName();

        /// <summary>
        /// Get the game's current version for the platform.
        /// </summary>
        /// <returns>The game's current version.</returns>
        string GetPlatformVersion();

        /// <summary>
        /// Get whether the platform is a PC.
        /// </summary>
        /// <returns>true if running on PC, false if not.</returns>
        bool IsPC();

        /// <summary>
        /// Get whether the platform is a console.
        /// </summary>
        /// <returns>true if running on a console, false if not.</returns>
        bool IsConsole();

        /// <summary>
        /// Get whether the platform is a mobile.
        /// </summary>
        /// <returns>true if running on a mobile, false if not.</returns>
        bool IsMobile();

        /// <summary>
        /// Prepare an advert to be shown.
        /// </summary>
        void PrepareAd();

        /// <summary>
        /// Show an advert.
        /// </summary>
        void ShowAd();

        /// <summary>
        /// Get the location and name of the save file.
        /// </summary>
        /// <returns>The location and name of the save file.</returns>
        string GetSaveFileName();

        /// <summary>
        /// Save the game's data.
        /// </summary>
        /// <param name="serialisedData">The data to save, in a serialised string format.</param>
        void SaveData(string serialisedData);

        /// <summary>
        /// Load the game's data.
        /// </summary>
        /// <returns>The game's data.</returns>
        GameSaveData LoadGameData();

        /// <summary>
        /// Unlock an achievement.
        /// </summary>
        /// <param name="achievementId">The id of the achievement to unlock.</param>
        void UnlockAchievement(string achievementId);

        /// <summary>
        /// Set an achievement's progress.
        /// </summary>
        /// <param name="achievementId">The id of the achievement.</param>
        /// <param name="progress">The progress.</param>
        void SetAchievementProgress(string achievementId, int progress);

        /// <summary>
        /// Reset achievements (only to be used when debugging).
        /// </summary>
        void ResetAchievements();

        /// <summary>
        /// Get whether an achievement has been unlocked.
        /// </summary>
        /// <param name="achievementId">The id of the achievement.</param>
        /// <returns>true if the achievement is unlocked, false if not.</returns>
        bool HasAchievement(string achievementId);

        /// <summary>
        /// Get an achievement's progress.
        /// </summary>
        /// <param name="achievementId">The id of the achievement.</param>
        /// <returns>The achievement's progress.</returns>
        float GetAchievementProgress(string achievementId);

        /// <summary>
        /// Open the platform's store.
        /// </summary>
        void OpenStore();

        /// <summary>
        /// Open the game's page on the platform's store.
        /// </summary>
        void OpenStoreOnAppPage();

        /// <summary>
        /// Set a rich presence value.
        /// </summary>
        /// <param name="richPresence">The rich presence.</param>
        void SetRichPresence(string richPresence);

        /// <summary>
        /// Clear all rich presence.
        /// </summary>
        void ClearRichPresence();

        /// <summary>
        /// Set the colour of the controller's lights.
        /// </summary>
        /// <param name="colour">The colour.</param>
        void SetControllerColour(Color colour);

        /// <summary>
        /// Set the colour of the controller's lights to the default value.
        /// </summary>
        void SetControllerColourToDefault();

        /// <summary>
        /// Get the default screen resolution.
        /// </summary>
        /// <returns>The default screen resolution.</returns>
        string GetDefaultResolution();

        /// <summary>
        /// Get the default screen size.
        /// </summary>
        /// <returns>The default screen size.</returns>
        ScreenSizes GetDefaultScreenSize();

        /// <summary>
        /// Get all screen resolutions for the platform.
        /// </summary>
        /// <returns>A list of screen resolutions.</returns>
        List<string> GetPlatformResolutions();

        /// <summary>
        /// Get the file name where controller icons are stored.
        /// </summary>
        /// <returns>The name of the file where controller icons are stored.</returns>
        string GetIconFile();
    }
}
