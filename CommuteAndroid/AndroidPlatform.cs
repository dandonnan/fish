namespace CommuteAndroid
{
    using Microsoft.Xna.Framework;
    using Microsoft.Xna.Framework.Graphics;
    using Newtonsoft.Json;
    using Commute.Platforms;
    using Commute.Save;
    using System;
    using System.Collections.Generic;
    using System.IO;
    using System.Linq;
    using System.Reflection;
    using Commute.Graphics;
    using CommuteAndroid.Ads;

    /// <summary>
    /// The platform implementation for an Android build.
    /// </summary>
    internal class AndroidPlatform : IPlatform
    {
        /// <summary>
        /// The platform's graphics device.
        /// </summary>
        private readonly GraphicsDevice graphicsDevice;

        /// <summary>
        /// Create the Android platform implementation.
        /// </summary>
        /// <param name="graphicsDevice">The graphics device.</param>
        public AndroidPlatform(GraphicsDevice graphicsDevice)
        {
            this.graphicsDevice = graphicsDevice;
        }

        /// <summary>
        /// Update method to be called each frame.
        /// </summary>
        public void Update()
        {
            // Not required
        }

        /// <summary>
        /// Called when the game is closed.
        /// </summary>
        public void Stop()
        {
            // Not required
        }

        /// <summary>
        /// Get the name of the platform.
        /// </summary>
        /// <returns>The name of the platform.</returns>
        public string GetPlatformName()
        {
            return "Android";
        }

        /// <summary>
        /// Get the game's current version for the platform.
        /// </summary>
        /// <returns>The game's current version.</returns>
        public string GetPlatformVersion()
        {
            return Assembly.GetExecutingAssembly().GetName().Version.ToString();
        }

        /// <summary>
        /// Get whether the platform is a PC.
        /// </summary>
        /// <returns>true if running on PC, false if not.</returns>
        public bool IsPC()
        {
            return false;
        }

        /// <summary>
        /// Get whether the platform is a console.
        /// </summary>
        /// <returns>true if running on a console, false if not.</returns>
        public bool IsConsole()
        {
            return false;
        }

        /// <summary>
        /// Get whether the platform is a mobile.
        /// </summary>
        /// <returns>true if running on a mobile, false if not.</returns>
        public bool IsMobile()
        {
            return true;
        }

        /// <summary>
        /// Prepare an advert to be shown.
        /// </summary>
        public void PrepareAd()
        {
            AdManager.InitialiseAd();
        }

        /// <summary>
        /// Show an advert.
        /// </summary>
        public void ShowAd()
        {
            AdManager.ShowAd();
        }

        /// <summary>
        /// Get the location and name of the save file.
        /// </summary>
        /// <returns>The location and name of the save file.</returns>
        public string GetSaveFileName()
        {
            return Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData), "fish.sav");
        }

        /// <summary>
        /// Save the game's data.
        /// </summary>
        /// <param name="serialisedData">The data to save, in a serialised string format.</param>
        public void SaveData(string serialisedData)
        {
            using (StreamWriter streamWriter = new StreamWriter(GetSaveFileName()))
            {
                streamWriter.Write(serialisedData);
            }
        }

        /// <summary>
        /// Load the game's data.
        /// </summary>
        /// <returns>The game's data.</returns>
        public GameSaveData LoadGameData()
        {
            GameSaveData gameData = new GameSaveData();

            try
            {
                using (StreamReader streamReader = new StreamReader(GetSaveFileName()))
                {
                    string data = streamReader.ReadToEnd();

                    gameData = JsonConvert.DeserializeObject<GameSaveData>(data);
                }
            }
            catch
            {
                // todo: handle not found
            }

            return gameData;
        }

        /// <summary>
        /// Unlock an achievement.
        /// </summary>
        /// <param name="achievementId">The id of the achievement to unlock.</param>
        public void UnlockAchievement(string achievementId)
        {
            // Not required
        }

        /// <summary>
        /// Set an achievement's progress.
        /// </summary>
        /// <param name="achievementId">The id of the achievement.</param>
        /// <param name="progress">The progress.</param>
        public void SetAchievementProgress(string achievementId, int progress)
        {
            // Not required
        }

        /// <summary>
        /// Reset achievements (only to be used when debugging).
        /// </summary>
        public void ResetAchievements()
        {
            // Not required
        }

        /// <summary>
        /// Get whether an achievement has been unlocked.
        /// </summary>
        /// <param name="achievementId">The id of the achievement.</param>
        /// <returns>true if the achievement is unlocked, false if not.</returns>
        public bool HasAchievement(string achievementId)
        {
            // No achievements for Android, so default to false
            return false;
        }

        /// <summary>
        /// Get an achievement's progress.
        /// </summary>
        /// <param name="achievementId">The id of the achievement.</param>
        /// <returns>The achievement's progress.</returns>
        public float GetAchievementProgress(string achievementId)
        {
            // No achievements for Android, so default to 0
            return 0;
        }

        /// <summary>
        /// Open the platform's store.
        /// </summary>
        public void OpenStore()
        {
            // Not required
        }

        /// <summary>
        /// Open the game's page on the platform's store.
        /// </summary>
        public void OpenStoreOnAppPage()
        {
            // Not required
        }

        /// <summary>
        /// Set a rich presence value.
        /// </summary>
        /// <param name="richPresence">The rich presence.</param>
        public void SetRichPresence(string richPresence)
        {
            // Not required
        }

        /// <summary>
        /// Clear all rich presence.
        /// </summary>
        public void ClearRichPresence()
        {
            // Not required
        }

        /// <summary>
        /// Set the colour of the controller's lights.
        /// </summary>
        /// <param name="colour">The colour.</param>
        public void SetControllerColour(Color colour)
        {
            // Not required
        }

        /// <summary>
        /// Set the colour of the controller's lights to the default value.
        /// </summary>
        public void SetControllerColourToDefault()
        {
            // Not required
        }

        /// <summary>
        /// Get the default screen resolution.
        /// </summary>
        /// <returns>The default screen resolution.</returns>
        public string GetDefaultResolution()
        {
            return GetPlatformResolutions().First();
        }

        /// <summary>
        /// Get the default screen size.
        /// </summary>
        /// <returns>The default screen size.</returns>
        public ScreenSizes GetDefaultScreenSize()
        {
            return ScreenSizes.Fullscreen;
        }

        /// <summary>
        /// Get all screen resolutions for the platform.
        /// </summary>
        /// <returns>A list of screen resolutions.</returns>
        public List<string> GetPlatformResolutions()
        {
            List<string> resolutions = new List<string>();

            string format = "{0}x{1}";

            foreach (DisplayMode resolution in graphicsDevice.Adapter.SupportedDisplayModes)
            {
                resolutions.Add(string.Format(format, resolution.Width, resolution.Height));
            }

            return resolutions;
        }

        /// <summary>
        /// Get the file name where controller icons are stored.
        /// </summary>
        /// <returns>The name of the file where controller icons are stored.</returns>
        public string GetIconFile()
        {
            // Controller icons not setup, so default to empty
            return string.Empty;
        }
    }
}
