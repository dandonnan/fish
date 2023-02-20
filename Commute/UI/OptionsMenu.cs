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
    using System.Collections.Generic;

    /// <summary>
    /// The options menu.
    /// </summary>
    internal class OptionsMenu
    {
        /// <summary>
        /// The close button.
        /// </summary>
        private readonly Button closeButton;

        /// <summary>
        /// A list of buttons.
        /// </summary>
        private readonly List<OptionButton> options;

        /// <summary>
        /// The currently selected option.
        /// </summary>
        private int currentOption;

        /// <summary>
        /// Create the options menu.
        /// </summary>
        public OptionsMenu()
        {
            // Create position offsets for the options
            // The PC build has more options so the buttons are positioned differently
            List<int> xOffsets = new List<int>
            {
                PlatformManager.Platform.IsPC() ? 629 : 832,
                PlatformManager.Platform.IsPC() ? 1060 : 832,
            };

            List<int> yOffsets = new List<int>
            {
                PlatformManager.Platform.IsPC() ? 184 : 234,
                PlatformManager.Platform.IsPC() ? 334 : 590,
                540,
                690
            };

            // Create a list of options
            options = new List<OptionButton>
            {
                new OptionButton(SpriteLibrary.GetSprite("Music"), new Vector2(xOffsets[0], yOffsets[0]), UpdateMusic, SpriteLibrary.GetSprite("Selected")),
                new OptionButton(SpriteLibrary.GetSprite("Sound"), new Vector2(xOffsets[1], yOffsets[1]), UpdateSound, SpriteLibrary.GetSprite("Selected"))
            };

            // If the game is on PC
            if (PlatformManager.Platform.IsPC())
            {
                // Add more options
                options.Add(new OptionButton(SpriteLibrary.GetSprite("Resolution"), new Vector2(xOffsets[0], yOffsets[2]), UpdateResolution, SpriteLibrary.GetSprite("Selected")));
                options.Add(new OptionButton(SpriteLibrary.GetSprite("Fullscreen"), new Vector2(xOffsets[1], yOffsets[3]), UpdateScreenSize, SpriteLibrary.GetSprite("Selected")));

                // Select the first option and highlight it
                currentOption = 0;
                options[currentOption].Highlight();
            }

            // Create the close button
            closeButton = new Button(SpriteLibrary.GetSprite("Close"), new Vector2(1772, 10), Close, SpriteLibrary.GetSprite("SmallHighlight"));
        }

        /// <summary>
        /// Update the options menu.
        /// </summary>
        public void Update()
        {
            // Check the input for when the menu is closed
            if (InputManager.IsBindingPressed(DefaultBindings.Pause)
                || InputManager.IsBindingPressed(DefaultBindings.Decline)
                || closeButton.IsTouched())
            {
                AudioManager.PlaySoundEffect("MenuBack");

                Close();
            }

            // Change the highlight on the close button so it is only when it is hovered over
            closeButton.Highlight(closeButton.IsHovered());

            // Check for movement input to change the highlighted option
            if (InputManager.IsBindingPressed(DefaultBindings.Up))
            {
                AudioManager.PlaySoundEffect("MenuMove");

                options[currentOption].Highlight(false);
                currentOption = currentOption == 0 ? options.Count - 1 : currentOption - 1;
                options[currentOption].Highlight();
            }

            if (InputManager.IsBindingPressed(DefaultBindings.Down))
            {
                AudioManager.PlaySoundEffect("MenuMove");

                options[currentOption].Highlight(false);
                currentOption = currentOption == options.Count - 1 ? 0 : currentOption + 1;
                options[currentOption].Highlight();
            }

            // Check for movement input to change the value of the current option
            if (InputManager.IsBindingPressed(DefaultBindings.Left))
            {
                options[currentOption].ChangeValue(-1);
            }

            if (InputManager.IsBindingPressed(DefaultBindings.Right))
            {
                options[currentOption].ChangeValue(1);
            }

            // Update each button
            foreach (OptionButton option in options)
            {
                option.Update();
            }
        }

        /// <summary>
        /// Draw the options menu.
        /// </summary>
        public void Draw()
        {
            closeButton?.Draw();

            options.ForEach(o => o.Draw());
        }

        /// <summary>
        /// Close the options menu.
        /// </summary>
        private void Close()
        {
            // Save any changes made
            SaveManager.Save();

            EventManager.FireEvent(KnownEvents.CloseOptionsMenu);
        }

        /// <summary>
        /// Update the music volume value.
        /// </summary>
        /// <param name="amount">The amount to change the value by.</param>
        /// <returns>The new value as a string.</returns>
        private string UpdateMusic(int amount)
        {
            // Only update the value if the amount is set
            if (amount != 0)
            {
                AudioManager.PlaySoundEffect("MenuMove");

                SaveManager.GameData.Audio.MusicVolume += amount;

                // Loop the volumes to the other side when reaching the min and max
                if (SaveManager.GameData.Audio.MusicVolume < AudioSettings.MinVolume)
                {
                    SaveManager.GameData.Audio.MusicVolume = AudioSettings.MaxVolume;
                }

                if (SaveManager.GameData.Audio.MusicVolume > AudioSettings.MaxVolume)
                {
                    SaveManager.GameData.Audio.MusicVolume = AudioSettings.MinVolume;
                }

                // Update the volume of any music playing
                AudioManager.ChangeVolume();
            }

            return SaveManager.GameData.Audio.MusicVolume.ToString();
        }

        /// <summary>
        /// Update the sound effect volume value.
        /// </summary>
        /// <param name="amount">The amount to change the value by.</param>
        /// <returns>The new value as a string.</returns>
        private string UpdateSound(int amount)
        {
            // Only update the value if the amount is set
            if (amount != 0)
            {
                AudioManager.PlaySoundEffect("MenuMove");

                SaveManager.GameData.Audio.SoundVolume += amount;

                // Loop the volumes to the other side when reaching the min and max
                if (SaveManager.GameData.Audio.SoundVolume < AudioSettings.MinVolume)
                {
                    SaveManager.GameData.Audio.SoundVolume = AudioSettings.MaxVolume;
                }

                if (SaveManager.GameData.Audio.SoundVolume > AudioSettings.MaxVolume)
                {
                    SaveManager.GameData.Audio.SoundVolume = AudioSettings.MinVolume;
                }

                // Update the volume of any looping sound effects playing
                AudioManager.ChangeVolume();
            }

            return SaveManager.GameData.Audio.SoundVolume.ToString();
        }

        /// <summary>
        /// Update the screen resolution value.
        /// </summary>
        /// <param name="amount">The amount to change the value by.</param>
        /// <returns>The new resolution as a string.</returns>
        private string UpdateResolution(int amount)
        {
            // Only update the value if the amount is set
            if (amount != 0)
            {
                AudioManager.PlaySoundEffect("MenuMove");

                // Get all resolutions
                List<string> resolutions = PlatformManager.Platform.GetPlatformResolutions();

                // Get the index of the current resolution
                int index = resolutions.IndexOf(SaveManager.MachineData.Graphics.Resolution);

                // Change the index up or down
                index += amount;

                // Loop the index to the other side of the list when reaching the ends
                if (index < 0)
                {
                    index = resolutions.Count - 1;
                }

                if (index > resolutions.Count - 1)
                {
                    index = 0;
                }

                // Set the resolution
                SaveManager.MachineData.Graphics.Resolution = resolutions[index];

                // Update the screen settings
                GameManager.ChangeScreenSettings();
            }

            return SaveManager.MachineData.Graphics.Resolution;
        }

        /// <summary>
        /// Update the screen size value.
        /// </summary>
        /// <param name="amount">The amount to change the value by.</param>
        /// <returns>The new screen size as a string.</returns>
        private string UpdateScreenSize(int amount)
        {
            // Only update the value if the amount is set
            if (amount != 0)
            {
                AudioManager.PlaySoundEffect("MenuMove");

                // Toggle the screen size
                SaveManager.MachineData.Graphics.ScreenSize =
                        SaveManager.MachineData.Graphics.ScreenSize == 0 ? 1 : 0;

                // Update the screen settings
                GameManager.ChangeScreenSettings();
            }

            return SaveManager.MachineData.Graphics.ScreenSize == 0
                    ? StringLibrary.GetString("Windowed")
                    : StringLibrary.GetString("Fullscreen");
        }
    }
}
