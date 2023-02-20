namespace Commute.Input
{
    using Commute.Extensions;
    using Commute.Platforms;
    using Microsoft.Xna.Framework;
    using Microsoft.Xna.Framework.Input;
    using Microsoft.Xna.Framework.Input.Touch;
    using System.Collections.Generic;

    /// <summary>
    /// An input manager.
    /// </summary>
    public class InputManager
    {
        /// <summary>
        /// The singleton instance of the input manager.
        /// </summary>
        private static InputManager inputManager;

        /// <summary>
        /// A list of keyboard keys that have been captured.
        /// </summary>
        private readonly List<Keys> capturedKeys;

        /// <summary>
        /// A list of gamepad button that have been captured.
        /// </summary>
        private readonly List<Buttons> capturedButtons;

        /// <summary>
        /// The current keyboard state.
        /// </summary>
        private KeyboardState keyState;

        /// <summary>
        /// The keyboard state on the previous frame.
        /// </summary>
        private KeyboardState lastKeyState;

        /// <summary>
        /// The current gamepad state.
        /// </summary>
        private GamePadState padState;

        /// <summary>
        /// The gamepad state on the previous frame.
        /// </summary>
        private GamePadState lastPadState;

        /// <summary>
        /// The current mouse state.
        /// </summary>
        private MouseState mouseState;

        /// <summary>
        /// The mouse state on the previous frame.
        /// </summary>
        private MouseState lastMouseState;

        /// <summary>
        /// The current joystick state.
        /// </summary>
        private JoystickState joystickState;

        /// <summary>
        /// The joystick state on the previous frame.
        /// </summary>
        private JoystickState lastJoystickState;

        /// <summary>
        /// The current touchscreen state.
        /// </summary>
        private TouchCollection touchState;

        /// <summary>
        /// The touchscreen state on the previous frame.
        /// </summary>
        private TouchCollection lastTouchState;

        /// <summary>
        /// The current input method.
        /// </summary>
        private InputMethod inputMethod;

        /// <summary>
        /// A private constructor.
        /// </summary>
        private InputManager()
        {
            inputManager = this;

            padState = new GamePadState();

            capturedKeys = new List<Keys>();
            capturedButtons = new List<Buttons>();

            // Set the input method based on the current platform
            if (PlatformManager.Platform.IsPC())
            {
                inputMethod = InputMethod.Keyboard;
            }
            else if (PlatformManager.Platform.IsMobile())
            {
                inputMethod = InputMethod.Touch;
            }
            else
            {
                inputMethod = InputMethod.Gamepad;
            }
        }

        /// <summary>
        /// Initialise the input manager.
        /// </summary>
        /// <returns>The input manager.</returns>
        public static InputManager Initialise()
        {
            if (inputManager == null)
            {
                new InputManager();
            }

            return inputManager;
        }

        /// <summary>
        /// Update the inputs.
        /// </summary>
        public void Update()
        {
            // Set the previous states to what they were at the end of the last frame
            lastKeyState = keyState;
            lastPadState = padState;
            lastMouseState = mouseState;
            lastJoystickState = joystickState;
            lastTouchState = touchState;

            // Clear all captured inputs
            capturedKeys.Clear();
            capturedButtons.Clear();

            // Get the new states
            keyState = Keyboard.GetState();
            mouseState = Mouse.GetState();
            touchState = TouchPanel.GetState();
            padState = GamePad.GetState(PlayerIndex.One);

            joystickState = Joystick.GetState(0);
        }

        /// <summary>
        /// Get if a binding has been pressed.
        /// </summary>
        /// <param name="binding">The binding to check.</param>
        /// <param name="capture">Whether or not to capture the input.</param>
        /// <returns>true if the binding is pressed, false if not.</returns>
        public static bool IsBindingPressed(InputBinding binding, bool capture = false)
        {
            // Check each input method
            bool isPressed = IsKeyDown(binding) || IsButtonDown(binding) || IsJoystickDown(binding);

            // If set to capture, add to the captured list
            if (isPressed && capture)
            {
                inputManager.capturedKeys.Add(binding.Key);
                inputManager.capturedKeys.Add(binding.AltKey);
                inputManager.capturedButtons.Add(binding.Button);
                inputManager.capturedButtons.Add(binding.AltButton);
            }

            return isPressed;
        }

        /// <summary>
        /// Check if a binding is held.
        /// </summary>
        /// <param name="binding">The binding to check.</param>
        /// <returns>true if the binding is held, false if not.</returns>
        public static bool IsBindingHeld(InputBinding binding)
        {
            return IsKeyHeld(binding) || IsButtonHeld(binding) || IsJoystickHeld(binding);
        }

        /// <summary>
        /// Check if the screen is touched.
        /// </summary>
        /// <returns>true if touched, false if not.</returns>
        public static bool IsTouched()
        {
            bool touched = false;

            if (inputManager.inputMethod == InputMethod.Touch)
            {
                touched = inputManager.touchState.IsPressed()
                        && inputManager.lastTouchState.IsReleased();
            }
            else
            {
                touched = IsLeftMousePressed();
            }

            return touched;
        }

        /// <summary>
        /// Get whether the touchscreen is held.
        /// </summary>
        /// <returns>true if the touchscree is held, false if not.</returns>
        public static bool IsTouchHeld()
        {
            return inputManager.touchState.IsPressed() || inputManager.touchState.IsHeld();
        }

        /// <summary>
        /// Get whether the left mouse button is pressed.
        /// </summary>
        /// <returns>true if pressed, false if not.</returns>
        public static bool IsLeftMousePressed()
        {
            return inputManager.mouseState.LeftButton == ButtonState.Pressed
                && inputManager.lastMouseState.LeftButton != ButtonState.Pressed;
        }

        /// <summary>
        /// Get whether the left mouse button is held.
        /// </summary>
        /// <returns>true if held, false if not.</returns>
        public static bool IsLeftMouseHeld()
        {
            return inputManager.mouseState.LeftButton == ButtonState.Pressed
                && inputManager.lastMouseState.LeftButton == ButtonState.Pressed;
        }

        /// <summary>
        /// Get the position of the mouse.
        /// </summary>
        /// <returns>The position of the mouse.</returns>
        public static Point GetMousePosition()
        {
            // This gets the raw mouse position and scales it by the UI scale to get the in-game position
            return new Point((int)(inputManager.mouseState.X / GameManager.UiScale.X),
                                (int)(inputManager.mouseState.Y / GameManager.UiScale.Y));
        }

        /// <summary>
        /// Get the position where the screen was touched.
        /// </summary>
        /// <returns>The position where the screen was touched.</returns>
        public static Point GetTouchPosition()
        {
            Point position;

            if (inputManager.inputMethod == InputMethod.Touch)
            {
                position = inputManager.touchState.GetTouchPosition();
            }
            else
            {
                position = GetMousePosition();
            }

            return position;
        }

        /// <summary>
        /// Get whether a key is down.
        /// </summary>
        /// <param name="binding">The input binding.</param>
        /// <returns>true if the key is down, false if not.</returns>
        private static bool IsKeyDown(InputBinding binding)
        {
            // Check the key and alternative key, and ignore it if the input has been captured
            return (inputManager.keyState.IsKeyDown(binding.Key)
                    || inputManager.keyState.IsKeyDown(binding.AltKey))
                    && inputManager.capturedKeys.Contains(binding.Key) == false
                    && inputManager.capturedKeys.Contains(binding.AltKey) == false
                    && inputManager.lastKeyState.IsKeyDown(binding.Key) == false
                    && inputManager.lastKeyState.IsKeyDown(binding.AltKey) == false;
        }

        /// <summary>
        /// Get whether a key is held.
        /// </summary>
        /// <param name="binding">The input binding.</param>
        /// <returns>true if the key is held, false if not.</returns>
        private static bool IsKeyHeld(InputBinding binding)
        {
            return (inputManager.keyState.IsKeyDown(binding.Key)
                    || inputManager.keyState.IsKeyDown(binding.AltKey))
                    && (inputManager.lastKeyState.IsKeyDown(binding.Key)
                    || inputManager.lastKeyState.IsKeyDown(binding.AltKey));
        }

        /// <summary>
        /// Get whether a button is down.
        /// </summary>
        /// <param name="binding">The input binding.</param>
        /// <returns>true if the button is down, false if not.</returns>
        private static bool IsButtonDown(InputBinding binding)
        {
            // Check the button and alternative button, and ignore it if the input has been captured
            return (inputManager.padState.IsButtonDown(binding.Button)
                    || inputManager.padState.IsButtonDown(binding.AltButton))
                    && inputManager.capturedButtons.Contains(binding.Button) == false
                    && inputManager.capturedButtons.Contains(binding.AltButton) == false
                    && inputManager.lastPadState.IsButtonDown(binding.Button) == false
                    && inputManager.lastPadState.IsButtonDown(binding.AltButton) == false;
        }

        /// <summary>
        /// Get whether a button is held.
        /// </summary>
        /// <param name="binding">The input binding.</param>
        /// <returns>true if the button is held, false if not.</returns>
        private static bool IsButtonHeld(InputBinding binding)
        {
            return (inputManager.padState.IsButtonDown(binding.Button)
                    || inputManager.padState.IsButtonDown(binding.AltButton))
                    && (inputManager.lastPadState.IsButtonDown(binding.Button)
                    || inputManager.lastPadState.IsButtonDown(binding.AltButton));
        }

        /// <summary>
        /// Get whether a joystick button is down.
        /// </summary>
        /// <param name="binding">The input binding.</param>
        /// <returns>true if the joystick is down, false if not.</returns>
        private static bool IsJoystickDown(InputBinding binding)
        {
            return inputManager.joystickState.IsPressed(binding.Joystick)
                    && inputManager.lastJoystickState.IsPressed(binding.Joystick) == false;
        }

        /// <summary>
        /// Get whether a joystick button is held.
        /// </summary>
        /// <param name="binding">The input binding.</param>
        /// <returns>true if the joystick is held, false if not.</returns>
        private static bool IsJoystickHeld(InputBinding binding)
        {
            return inputManager.joystickState.IsPressed(binding.Joystick)
                    && inputManager.lastJoystickState.IsPressed(binding.Joystick);
        }
    }
}
