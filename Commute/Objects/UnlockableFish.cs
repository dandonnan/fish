namespace Commute.Objects
{
    using Commute.Graphics;
    using Commute.Save;
    using System.Collections.Generic;

    /// <summary>
    /// Handles fish unlocks.
    /// </summary>
    internal class UnlockableFish
    {
        /// <summary>
        /// Unlock fish where requirements have been met.
        /// </summary>
        /// <param name="scale">The current scale.</param>
        /// <param name="points">The number of points.</param>
        public static void Unlock(int scale, int points)
        {
            // Go through each unlockable fish
            for (int i=0; i<SaveManager.GameData.UnlockedFish.Length; i++)
            {
                // If the fish has not been unlocked
                if (SaveManager.GameData.UnlockedFish[i] == false)
                {
                    // Get the metadata for that fish
                    FishMetadata metadata = Fish[i];

                    // If the fish's fish eaten requirements have been met then unlock the fish
                    if (metadata.UnlockEatFish > 0 && SaveManager.GameData.FishEaten >= metadata.UnlockEatFish)
                    {
                        SaveManager.GameData.UnlockedFish[i] = true;
                        SaveManager.GameData.Notifications[i] = true;
                    }

                    // If the fish's scale requirements have been met then unlock the fish
                    if (metadata.UnlockScale > 0 && scale >= metadata.UnlockScale)
                    {
                        SaveManager.GameData.UnlockedFish[i] = true;
                        SaveManager.GameData.Notifications[i] = true;
                    }

                    // If the fish's points requirements have been met then unlock the fish
                    if (metadata.UnlockPoints > 0 && points >= metadata.UnlockPoints)
                    {
                        SaveManager.GameData.UnlockedFish[i] = true;
                        SaveManager.GameData.Notifications[i] = true;
                    }
                }
            }
        }

        /// <summary>
        /// A list of fish metadata which sets their sprites and unlock requirements.
        /// </summary>
        public static List<FishMetadata> Fish = new List<FishMetadata>
        {
            new FishMetadata
            {
                Sprite = SpriteLibrary.GetSprite("GreyFish"),
                BaseScale = 1,
                UnlockEatFish = 0,
                UnlockScale = 0,
                UnlockPoints = 0
            },
            new FishMetadata
            {
                Sprite = SpriteLibrary.GetSprite("OrangeFish"),
                BaseScale = 2,
                UnlockEatFish = 0,
                UnlockScale = 4,
                UnlockPoints = 0
            },
            new FishMetadata
            {
                Sprite = SpriteLibrary.GetSprite("RedFish"),
                BaseScale = 3,
                UnlockEatFish = 0,
                UnlockScale = 0,
                UnlockPoints = 1000
            },
            new FishMetadata
            {
                Sprite = SpriteLibrary.GetSprite("GreenFish"),
                BaseScale = 4,
                UnlockEatFish = 500,
                UnlockScale = 0,
                UnlockPoints = 0
            },
            new FishMetadata
            {
                Sprite = SpriteLibrary.GetSprite("BlueFish"),
                BaseScale = 5,
                UnlockEatFish = 0,
                UnlockScale = 7,
                UnlockPoints = 0
            },
            new FishMetadata
            {
                Sprite = SpriteLibrary.GetSprite("YellowFish"),
                BaseScale = 6,
                UnlockEatFish = 2500,
                UnlockScale = 0,
                UnlockPoints = 0
            },
            new FishMetadata
            {
                Sprite = SpriteLibrary.GetSprite("Shark"),
                BaseScale = 7,
                UnlockEatFish = 0,
                UnlockScale = 0,
                UnlockPoints = 3000
            },
        };
    }
}
