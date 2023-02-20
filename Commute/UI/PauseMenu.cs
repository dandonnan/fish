namespace Commute.UI
{
    using Commute.Audio;
    using Commute.Events;
    using Commute.Graphics;
    using Commute.Input;
    using Commute.Platforms;
    using Microsoft.Xna.Framework;
    using System.Collections.Generic;

    /// <summary>
    /// The pause menu.
    /// </summary>
    internal class PauseMenu
    {
        /// <summary>
        /// A list of buttons.
        /// </summary>
        private readonly List<Button> buttons;

        /// <summary>
        /// The options menu.
        /// </summary>
        private readonly OptionsMenu optionsMenu;

        /// <summary>
        /// The overlay.
        /// </summary>
        private readonly Overlay overlay;

        /// <summary>
        /// Whether the options menu is showing.
        /// </summary>
        private bool showingOptions;

        /// <summary>
        /// The currently selected option.
        /// </summary>
        private int currentOption;

        /// <summary>
        /// Create a new pause menu.
        /// </summary>
        public PauseMenu()
        {
            overlay = new Overlay();

            optionsMenu = new OptionsMenu();

            // Setup a list of x co-ordinate offsets for the buttons
            // The PC build has one more option so they are positioned differently
            List<int> xPositions = new List<int>
            {
                PlatformManager.Platform.IsPC() ? 476 : 654,
                PlatformManager.Platform.IsPC() ? 832 : 1010,
                PlatformManager.Platform.IsPC() ? 1188 : 0,
            };

            // Create a list of buttons
            buttons = new List<Button>
            {
                new Button(SpriteLibrary.GetSprite("Options"), new Vector2(xPositions[0], 412), Options, SpriteLibrary.GetSprite("Selected")),
                new Button(SpriteLibrary.GetSprite("Play"), new Vector2(xPositions[1], 412), Resume, SpriteLibrary.GetSprite("Selected")),
            };

            // If the game is running on PC
            if (PlatformManager.Platform.IsPC())
            {
                // Change to the Play option
                currentOption = 1;

                // Add an option to quit
                buttons.Add(new Button(SpriteLibrary.GetSprite("Quit"), new Vector2(xPositions[2], 412), Quit, SpriteLibrary.GetSprite("Selected")));

                // Highlight the play option
                buttons[currentOption].Highlight();
            }
        }

        /// <summary>
        /// Update the pause menu.
        /// </summary>
        public void Update()
        {
            // If the options menu is being shown
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
                    // Change the highlight if the button is hovered over
                    if (buttons[i].IsHovered())
                    {
                        buttons[currentOption].Highlight(false);
                        currentOption = i;
                        buttons[currentOption].Highlight();
                    }

                    // Check for the button being touched
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

                // Check the input for when a button is selected
                if (InputManager.IsBindingPressed(DefaultBindings.Accept))
                {
                    AudioManager.PlaySoundEffect("MenuConfirm");

                    buttons[currentOption].Select();
                }

                // Check the input for when the pause menu should be closed
                if (InputManager.IsBindingPressed(DefaultBindings.Pause)
                    || InputManager.IsBindingPressed(DefaultBindings.Decline))
                {
                    AudioManager.PlaySoundEffect("MenuBack");

                    Resume();
                }
            }
        }

        /// <summary>
        /// Draw the pause menu.
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
            }
        }

        /// <summary>
        /// Called when the resume button is selected.
        /// </summary>
        private void Resume()
        {
            EventManager.FireEvent(KnownEvents.ClosePauseMenu);
        }

        /// <summary>
        /// Called when the options button is selected.
        /// </summary>
        private void Options()
        {
            showingOptions = true;
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
