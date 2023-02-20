namespace Commute.UI
{
    using Commute.Audio;
    using Commute.Events;
    using Commute.Graphics;
    using Commute.Input;
    using Commute.Localisation;
    using Commute.Platforms;
    using Commute.Save;
    using Microsoft.Xna.Framework;
    using Microsoft.Xna.Framework.Graphics;
    using System.Collections.Generic;
    using System.Linq;

    /// <summary>
    /// The game end screen.
    /// </summary>
    internal class EndScreen
    {
        /// <summary>
        /// A list of buttons.
        /// </summary>
        private readonly List<Button> buttons;

        /// <summary>
        /// The notification icon.
        /// </summary>
        private readonly Sprite notificationIcon;

        /// <summary>
        /// The font.
        /// </summary>
        private readonly SpriteFont font;

        /// <summary>
        /// The overlay.
        /// </summary>
        private readonly Overlay overlay;

        /// <summary>
        /// A list of stats and their positions.
        /// </summary>
        private List<KeyValuePair<string, Vector2>> stats;

        /// <summary>
        /// Whether the options menu is showing.
        /// </summary>
        private bool showingOptions;

        /// <summary>
        /// Whether to show the notification icon.
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
        /// Create the game end screen.
        /// </summary>
        public EndScreen()
        {
            optionsMenu = new OptionsMenu();

            overlay = new Overlay();

            font = GameManager.LoadFont("CounterFont");

            // A list of x co-ordinate offsets for buttons
            // The PC version has one more button so the buttons are positioned differently
            List<int> xOffsets = new List<int>
            {
                PlatformManager.Platform.IsMobile() ? 476 : 298,
                PlatformManager.Platform.IsMobile() ? 832 : 654,
                PlatformManager.Platform.IsMobile() ? 1188 : 1010,
                1366
            };

            // Set up the notification icon
            notificationIcon = SpriteLibrary.GetSprite("Notification");
            notificationIcon.SetPosition(new Vector2(xOffsets[1] + 175, 600));

            // Setup the buttons
            buttons = new List<Button>
            {
                new Button(SpriteLibrary.GetSprite("Options"), new Vector2(xOffsets[0], 612), Options, SpriteLibrary.GetSprite("Selected")),
                new Button(SpriteLibrary.GetSprite("FishMenu"), new Vector2(xOffsets[1], 612), ShowFishSelect, SpriteLibrary.GetSprite("Selected")),
                new Button(SpriteLibrary.GetSprite("Restart"), new Vector2(xOffsets[2], 612), Restart, SpriteLibrary.GetSprite("Selected")),
            };

            // If the game is on PC
            if (PlatformManager.Platform.IsPC())
            {
                // Set the option to Restart
                currentOption = 2;

                // Add a Quit button
                buttons.Add(new Button(SpriteLibrary.GetSprite("Quit"), new Vector2(xOffsets[3], 612), Quit, SpriteLibrary.GetSprite("Selected")));

                // Highlight the Restart button
                buttons[currentOption].Highlight();
            }
        }

        /// <summary>
        /// Prepare the end game screen before showing it.
        /// </summary>
        /// <param name="scale">The current scale.</param>
        /// <param name="fishEaten">The number of fish eaten.</param>
        /// <param name="pointsScored">The points scored.</param>
        public void Prepare(int scale, int fishEaten, int pointsScored)
        {
            // If the scale is a new best, set the best
            if (scale > SaveManager.GameData.BestScale)
            {
                SaveManager.GameData.BestScale = scale;
            }

            // If the points scored is a new best, set the best
            if (pointsScored > SaveManager.GameData.BestPoints)
            {
                SaveManager.GameData.BestPoints = pointsScored;
            }

            // Save the game
            SaveManager.Save();

            // Show the notification if there are any notifications
            showNotification = SaveManager.GameData.Notifications.Any(n => n == true);

            // Setup a list of stats with their positions
            stats = new List<KeyValuePair<string, Vector2>>
            {
                new KeyValuePair<string, Vector2>(StringLibrary.GetString("Size"), new Vector2(300, 250)),
                new KeyValuePair<string, Vector2>(scale.ToString(), new Vector2(600, 250)),
                new KeyValuePair<string, Vector2>(StringLibrary.GetString("Best"), new Vector2(1000, 250)),
                new KeyValuePair<string, Vector2>(SaveManager.GameData.BestScale.ToString(), new Vector2(1400, 250)),
                new KeyValuePair<string, Vector2>(StringLibrary.GetString("Points"), new Vector2(300, 350)),
                new KeyValuePair<string, Vector2>(pointsScored.ToString(), new Vector2(600, 350)),
                new KeyValuePair<string, Vector2>(StringLibrary.GetString("Best"), new Vector2(1000, 350)),
                new KeyValuePair<string, Vector2>(SaveManager.GameData.BestPoints.ToString(), new Vector2(1400, 350)),
                new KeyValuePair<string, Vector2>(StringLibrary.GetString("FishEaten"), new Vector2(300, 450)),
                new KeyValuePair<string, Vector2>(fishEaten.ToString(), new Vector2(600, 450)),
                new KeyValuePair<string, Vector2>(StringLibrary.GetString("TotalFishEaten"), new Vector2(1000, 450)),
                new KeyValuePair<string, Vector2>(SaveManager.GameData.FishEaten.ToString(), new Vector2(1400, 450)),
            };
        }

        /// <summary>
        /// Toggle the notification icon if there are notifications to show.
        /// </summary>
        public void UpdateNotifications()
        {
            showNotification = SaveManager.GameData.Notifications.Any(n => n == true);
        }

        /// <summary>
        /// Update the game end screen.
        /// </summary>
        public void Update()
        {
            // If the options menu is being shown
            if (showingOptions)
            {
                // Update the options menu
                optionsMenu.Update();

                // Check for the close options menu event
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
                    // Highlight the button if it is being hovered over
                    if (buttons[i].IsHovered())
                    {
                        buttons[currentOption].Highlight(false);
                        currentOption = i;
                        buttons[currentOption].Highlight();
                    }

                    // Check for if the button has been touched
                    if (buttons[i].IsTouched())
                    {
                        AudioManager.PlaySoundEffect("MenuConfirm");

                        buttons[i].Select();
                    }
                }

                // Check the movement input to change the highlighted option
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

                // Check the input to see if the button is selected
                if (InputManager.IsBindingPressed(DefaultBindings.Accept))
                {
                    AudioManager.PlaySoundEffect("MenuConfirm");

                    buttons[currentOption].Select();
                }
            }
        }

        /// <summary>
        /// Draw the game end screen.
        /// </summary>
        public void Draw()
        {
            overlay.Draw();

            if (showingOptions)
            {
                optionsMenu.Draw();
            }
            else
            {
                buttons.ForEach(b => b.Draw());

                if (showNotification)
                {
                    notificationIcon.Draw();
                }

                stats.ForEach(s => GameManager.SpriteBatch.DrawString(font, s.Key, s.Value, Color.White));
            }
        }

        /// <summary>
        /// Called when selecting the options button.
        /// </summary>
        private void Options()
        {
            showingOptions = true;
        }

        /// <summary>
        /// Called when selecting the end screen button.
        /// </summary>
        private void Restart()
        {
            // Prepare an ad to display
            PlatformManager.Platform.PrepareAd();

            EventManager.FireEvent(KnownEvents.Restart);
        }

        /// <summary>
        /// Called when the fish select button is selected.
        /// </summary>
        private void ShowFishSelect()
        {
            EventManager.FireEvent(KnownEvents.OpenFishSelect);
        }

        /// <summary>
        /// Called when the quit button is selected.
        /// </summary>
        private void Quit()
        {
            EventManager.FireEvent(KnownEvents.CloseGame);
        }
    }
}
