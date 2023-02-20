namespace Commute.UI
{
    using Commute.Audio;
    using Commute.Events;
    using Commute.Graphics;
    using Commute.Input;
    using Commute.Platforms;
    using Commute.Save;
    using Microsoft.Xna.Framework;
    using System.Collections.Generic;
    using System.Linq;

    /// <summary>
    /// The title screen.
    /// </summary>
    internal class TitleScreen
    {
        /// <summary>
        /// The game logo.
        /// </summary>
        private readonly Sprite logo;

        /// <summary>
        /// A list of buttons.
        /// </summary>
        private readonly List<Button> buttons;

        /// <summary>
        /// The notification icon.
        /// </summary>
        private readonly Sprite notificationIcon;

        /// <summary>
        /// The overlay.
        /// </summary>
        private readonly Overlay overlay;

        /// <summary>
        /// Whether the options screen is showing.
        /// </summary>
        private bool showingOptions;

        /// <summary>
        /// Whether to show a notification.
        /// </summary>
        private bool showNotification;

        /// <summary>
        /// The options menu.
        /// </summary>
        private OptionsMenu optionsMenu;

        /// <summary>
        /// The currently selected option.
        /// </summary>
        private int currentOption;

        /// <summary>
        /// Create a title screen.
        /// </summary>
        public TitleScreen()
        {
            optionsMenu = new OptionsMenu();

            overlay = new Overlay();

            logo = new Sprite("logo");
            logo.SetPosition(new Vector2((GameManager.UiResolutionWidth - logo.GetWidth()) / 2, 100));

            // Create a list of x co-ordinate offsets for the buttons
            // The PC build has one more button than mobile, so buttons are positioned differently
            List<int> xOffsets = new List<int>
            {
                100,
                PlatformManager.Platform.IsPC() ? 654 : 1564,
                PlatformManager.Platform.IsPC() ? 1010 : 832,
                PlatformManager.Platform.IsPC() ? 1564 : 0,
            };

            // Setup the notification icon and whether to display it
            notificationIcon = SpriteLibrary.GetSprite("Notification");
            notificationIcon.SetPosition(new Vector2(xOffsets[1] + 175, 750));

            showNotification = SaveManager.GameData.Notifications.Any(n => n == true);

            // Create a list of buttons
            buttons = new List<Button>
            {
                new Button(SpriteLibrary.GetSprite("Options"), new Vector2(xOffsets[0], 750), Options, SpriteLibrary.GetSprite("Selected")),
                new Button(SpriteLibrary.GetSprite("FishMenu"), new Vector2(xOffsets[1], 750), ShowFishSelect, SpriteLibrary.GetSprite("Selected")),
                new Button(SpriteLibrary.GetSprite("Play"), new Vector2(xOffsets[2], 750), Restart, SpriteLibrary.GetSprite("Selected")),
            };

            // If the game is on PC
            if (PlatformManager.Platform.IsPC())
            {
                // Add a Quit button
                buttons.Add(new Button(SpriteLibrary.GetSprite("Quit"), new Vector2(xOffsets[3], 750), Quit, SpriteLibrary.GetSprite("Selected")));

                // Change the current option to be Play
                currentOption = 2;

                // Highlight the Play option
                buttons[currentOption].Highlight();
            }
        }

        /// <summary>
        /// Toggle the notification icon if there are any notifications.
        /// </summary>
        public void UpdateNotifications()
        {
            showNotification = SaveManager.GameData.Notifications.Any(n => n == true);
        }

        /// <summary>
        /// Update the title screen.
        /// </summary>
        public void Update()
        {
            // If the options screen is showing
            if (showingOptions)
            {
                // Update the options menu
                optionsMenu.Update();

                // Check for the options menu being closed
                if (EventManager.HasEventFiredThenKill(KnownEvents.CloseOptionsMenu))
                {
                    showingOptions = false;
                }
            }
            else
            {
                // Go through each button
                for (int i = 0; i < buttons.Count; i++)
                {
                    // Change the highlight depending on if the button is hovered over
                    if (buttons[i].IsHovered())
                    {
                        buttons[currentOption].Highlight(false);
                        currentOption = i;
                        buttons[currentOption].Highlight();
                    }

                    // Check if the button is selected
                    if (buttons[i].IsTouched())
                    {
                        AudioManager.PlaySoundEffect("MenuConfirm");

                        buttons[i].Select();
                    }
                }

                // Check movement input to change the highlighted option
                if (InputManager.IsBindingPressed(DefaultBindings.Left))
                {
                    AudioManager.PlaySoundEffect("MenuMove");

                    buttons[currentOption].Highlight(false);
                    currentOption = currentOption == 0 ? buttons.Count - 1 : currentOption - 1;
                    buttons[currentOption].Highlight();
                }

                if (InputManager.IsBindingPressed(DefaultBindings.Right))
                {
                    AudioManager.PlaySoundEffect("MenuMove");

                    buttons[currentOption].Highlight(false);
                    currentOption = currentOption == buttons.Count - 1 ? 0 : currentOption + 1;
                    buttons[currentOption].Highlight();
                }

                // Check the input to see if a button has been selected
                if (InputManager.IsBindingPressed(DefaultBindings.Accept))
                {
                    AudioManager.PlaySoundEffect("MenuConfirm");

                    buttons[currentOption].Select();
                }
            }
        }

        /// <summary>
        /// Draw the title screen.
        /// </summary>
        public void Draw()
        {
            overlay.Draw();

            // Draw the options menu if it should be displayed
            if (showingOptions)
            {
                optionsMenu.Draw();
            }
            else
            {
                logo.Draw();

                // Draw each button
                buttons.ForEach(b => b.Draw());

                // Only show the notification icon if enabled
                if (showNotification)
                {
                    notificationIcon.Draw();
                }
            }
        }

        /// <summary>
        /// Called when the options button is selected.
        /// </summary>
        private void Options()
        {
            showingOptions = true;
        }

        /// <summary>
        /// Called when the fish select button is selected.
        /// </summary>
        private void ShowFishSelect()
        {
            EventManager.FireEvent(KnownEvents.OpenFishSelect);
        }

        /// <summary>
        /// Called when the game restarts.
        /// </summary>
        private void Restart()
        {
            // Prepare an advert for display
            PlatformManager.Platform.PrepareAd();

            EventManager.FireEvent(KnownEvents.Restart);
        }

        /// <summary>
        /// Called when the quit option is selected.
        /// </summary>
        private void Quit()
        {
            EventManager.FireEvent(KnownEvents.CloseGame);
        }
    }
}
