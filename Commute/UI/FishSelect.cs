namespace Commute.UI
{
    using Commute.Audio;
    using Commute.Events;
    using Commute.Graphics;
    using Commute.Input;
    using Commute.Localisation;
    using Commute.Objects;
    using Commute.Save;
    using Microsoft.Xna.Framework;
    using Microsoft.Xna.Framework.Graphics;
    using System.Collections.Generic;

    /// <summary>
    /// The fish select screen.
    /// </summary>
    internal class FishSelect
    {
        /// <summary>
        /// The left arrow button.
        /// </summary>
        private readonly Button leftArrow;

        /// <summary>
        /// The right arrow button.
        /// </summary>
        private readonly Button rightArrow;

        /// <summary>
        /// The close button.
        /// </summary>
        private readonly Button closeButton;

        /// <summary>
        /// The font.
        /// </summary>
        private readonly SpriteFont font;

        /// <summary>
        /// The overlay.
        /// </summary>
        private readonly Overlay overlay;

        /// <summary>
        /// A list of fish.
        /// </summary>
        private List<Sprite> fish;

        /// <summary>
        /// A list of unlock conditions.
        /// </summary>
        private List<string> unlockConditions;

        /// <summary>
        /// The unlock position.
        /// </summary>
        private Vector2 unlockPosition;

        /// <summary>
        /// The currently selected fish.
        /// </summary>
        private int currentFish;

        /// <summary>
        /// Create the fish select screen.
        /// </summary>
        public FishSelect()
        {
            overlay = new Overlay();

            leftArrow = new Button(SpriteLibrary.GetSprite("ArrowLeft"), new Vector2(640, 476), SelectLeft, SpriteLibrary.GetSprite("ArrowLeftHighlight"));
            rightArrow = new Button(SpriteLibrary.GetSprite("ArrowRight"), new Vector2(1280, 476), SelectRight, SpriteLibrary.GetSprite("ArrowRightHighlight"));

            closeButton = new Button(SpriteLibrary.GetSprite("Close"), new Vector2(1772, 10), Close, SpriteLibrary.GetSprite("SmallHighlight"));

            font = GameManager.LoadFont("CounterFont");

            unlockPosition = new Vector2(0, (GameManager.BaseResolutionHeight / 8) * 5);

            SetupSprites();

            currentFish = SaveManager.GameData.CurrentFish;
        }

        /// <summary>
        /// Reset the screen.
        /// </summary>
        public void Reset()
        {
            SetupSprites();
        }

        /// <summary>
        /// Update the fish select screen.
        /// </summary>
        public void Update()
        {
            // Change the highlight on the close button depending on if it is hovered over
            closeButton.Highlight(closeButton.IsHovered());

            // Check movement input to change the selected fish
            if (InputManager.IsBindingPressed(DefaultBindings.Left)
                || leftArrow.IsTouched())
            {
                SelectLeft();
            }

            if (InputManager.IsBindingPressed(DefaultBindings.Right)
                || rightArrow.IsTouched())
            {
                SelectRight();
            }

            // Change the highlights on the arrow buttons depending on if they are hovered
            leftArrow.Highlight(leftArrow.IsHovered());
            rightArrow.Highlight(rightArrow.IsHovered());

            // Check input to close the fish select screen
            if (InputManager.IsBindingPressed(DefaultBindings.Accept)
                || InputManager.IsBindingPressed(DefaultBindings.Decline)
                || InputManager.IsBindingPressed(DefaultBindings.Pause)
                || closeButton.IsTouched())
            {
                AudioManager.PlaySoundEffect("MenuBack");

                Close();
            }
        }

        /// <summary>
        /// Draw the fish select screen.
        /// </summary>
        public void Draw()
        {
            overlay.Draw();

            fish[currentFish].Draw();

            leftArrow.Draw();
            rightArrow.Draw();

            closeButton?.Draw();

            GameManager.SpriteBatch.DrawString(font, unlockConditions[currentFish], unlockPosition, Color.White);
        }

        /// <summary>
        /// Change the fish selection by moving back 1.
        /// </summary>
        private void SelectLeft()
        {
            AudioManager.PlaySoundEffect("MenuMove");

            // Move the current fish back by 1, or loop to the end of the list
            currentFish = currentFish == 0 ? fish.Count - 1 : currentFish - 1;

            // Change the position of the text based on the unlock condition
            unlockPosition.X = (GameManager.UiResolutionWidth - font.MeasureString(unlockConditions[currentFish]).X) / 2;

            // If there was a notification, remove it
            if (SaveManager.GameData.Notifications[currentFish] == true)
            {
                SaveManager.GameData.Notifications[currentFish] = false;
                SaveManager.Save();
            }
        }

        /// <summary>
        /// Change the fish selection by moving forward 1.
        /// </summary>
        private void SelectRight()
        {
            AudioManager.PlaySoundEffect("MenuMove");

            // Move the current fish forward by 1, or loop to the start of the list
            currentFish = currentFish == fish.Count - 1 ? 0 : currentFish + 1;

            // Change the position of the text based on the unlock condition
            unlockPosition.X = (GameManager.UiResolutionWidth - font.MeasureString(unlockConditions[currentFish]).X) / 2;

            // If there was a notification, remove it
            if (SaveManager.GameData.Notifications[currentFish] == true)
            {
                SaveManager.GameData.Notifications[currentFish] = false;
                SaveManager.Save();
            }
        }

        /// <summary>
        /// Close the fish select screen.
        /// </summary>
        private void Close()
        {
            // If the player has unlocked the currently selected fish
            if (SaveManager.GameData.UnlockedFish[currentFish])
            {
                // Change the fish in the game to be that fish
                SaveManager.GameData.CurrentFish = currentFish;
            }

            EventManager.FireEvent(KnownEvents.CloseFishSelect);
        }

        /// <summary>
        /// Setup the sprite.
        /// </summary>
        private void SetupSprites()
        {
            fish = new List<Sprite>();

            unlockConditions = new List<string>();

            // Go through each unlockable fish
            for (int i=0; i<GameSaveData.UnlockableFish; i++)
            {
                // Get the sprite and set the position
                Sprite sprite = new Sprite(UnlockableFish.Fish[i].Sprite);
                sprite.SetOriginToCenter();
                sprite.SetPosition(new Vector2(GameManager.BaseResolutionWidth / 2, GameManager.BaseResolutionHeight / 2));

                string condition = string.Empty;

                // If the player has not unlocked the fish
                if (SaveManager.GameData.UnlockedFish[i] == false)
                {
                    // Grey the sprite out
                    sprite.SetColour(Color.Gray);

                    // Set the unlock condition text based on the condition
                    if (UnlockableFish.Fish[i].UnlockScale > 0)
                    {
                        condition = string.Format(StringLibrary.GetString("UnlockScale"), UnlockableFish.Fish[i].UnlockScale);
                    }

                    if (UnlockableFish.Fish[i].UnlockEatFish > 0)
                    {
                        condition = string.Format(StringLibrary.GetString("UnlockEat"), UnlockableFish.Fish[i].UnlockEatFish);
                    }

                    if (UnlockableFish.Fish[i].UnlockPoints > 0)
                    {
                        condition = string.Format(StringLibrary.GetString("UnlockPoints"), UnlockableFish.Fish[i].UnlockPoints);
                    }
                }

                // Add the unlock condition to the list
                unlockConditions.Add(condition);

                // Add the sprite to the list
                fish.Add(sprite);
            }
        }
    }
}
