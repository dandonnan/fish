namespace Commute.Scenes
{
    using Commute.Audio;
    using Commute.Events;
    using Commute.Graphics;
    using Commute.Input;
    using Commute.Objects;
    using Commute.Objects.Bounds;
    using Commute.Platforms;
    using Commute.UI;
    using Microsoft.Xna.Framework;
    using Microsoft.Xna.Framework.Graphics;
    using System;
    using System.Collections.Generic;
    using System.Linq;

    /// <summary>
    /// The main scene.
    /// </summary>
    internal class MainScene : IScene
    {
        /// <summary>
        /// The maximum number of fish at any time.
        /// </summary>
        private const int maxFish = 20;

        /// <summary>
        /// The title screen.
        /// </summary>
        private readonly TitleScreen titleScreen;

        /// <summary>
        /// The fish select screen.
        /// </summary>
        private readonly FishSelect fishSelect;

        /// <summary>
        /// The pause menu.
        /// </summary>
        private readonly PauseMenu pauseMenu;

        /// <summary>
        /// The game over screen.
        /// </summary>
        private readonly EndScreen endScreen;

        /// <summary>
        /// The pause button.
        /// </summary>
        private readonly Button pauseButton;

        /// <summary>
        /// The top UI bar.
        /// </summary>
        private readonly TopBar topBar;

        /// <summary>
        /// A list of fish.
        /// </summary>
        private readonly List<Fish> fish;

        /// <summary>
        /// A list of bubbles.
        /// </summary>
        private readonly List<Bubble> bubbles;

        /// <summary>
        /// The collision manager.
        /// </summary>
        private readonly CollisionManager collisionManager;

        /// <summary>
        /// The trigger area manager.
        /// </summary>
        private readonly TriggerAreaManager triggerAreaManager;

        /// <summary>
        /// The player character.
        /// </summary>
        private readonly Player player;

        /// <summary>
        /// The background texture.
        /// </summary>
        private readonly Texture2D background;

        /// <summary>
        /// A sprite displayed in the foreground.
        /// </summary>
        private readonly Sprite foreground1;

        /// <summary>
        /// A sprite displayed in the foreground.
        /// </summary>
        private readonly Sprite foreground2;

        /// <summary>
        /// The current state.
        /// </summary>
        private SceneState state;

        /// <summary>
        /// The state before opening a menu, to return to when the menu closes.
        /// </summary>
        private SceneState lastState;

        /// <summary>
        /// The current fish scale.
        /// </summary>
        private int currentScale;

        /// <summary>
        /// The number of bubbles burst.
        /// </summary>
        private int bubblesBurst;

        /// <summary>
        /// A timer which provides double points when active.
        /// </summary>
        private double doublePoints;

        /// <summary>
        /// The points earned in the session.
        /// </summary>
        private int points;

        /// <summary>
        /// The number of fish to eat until the next scale up.
        /// </summary>
        private int fishToNextScale;

        /// <summary>
        /// The number of big fish active.
        /// </summary>
        private float bigFishCount;

        /// <summary>
        /// The main scene constructor.
        /// </summary>
        public MainScene()
        {
            // Initialise managers
            collisionManager = CollisionManager.Initialise();
            triggerAreaManager = TriggerAreaManager.Initialise();

            // Start on the title screen
            state = SceneState.Title;

            bubbles = new List<Bubble>();

            // Load images
            background = GameManager.LoadTexture("background");

            foreground1 = SpriteLibrary.GetSprite("Foreground1");
            foreground1.SetPosition(new Vector2(0, GameManager.UiResolutionHeight - foreground1.GetHeight()));

            foreground2 = SpriteLibrary.GetSprite("Foreground2");
            foreground2.SetPosition(new Vector2(GameManager.UiResolutionWidth - foreground2.GetHeight(), GameManager.UiResolutionHeight - foreground2.GetHeight()));

            // Create UI screens
            titleScreen = new TitleScreen();
            pauseMenu = new PauseMenu();
            fishSelect = new FishSelect();
            endScreen = new EndScreen();

            topBar = new TopBar();

            // Setup the pause button
            pauseButton = new Button(SpriteLibrary.GetSprite("Pause"), new Vector2(1772, 1), () => { state = SceneState.Paused; }, SpriteLibrary.GetSprite("SmallHighlight"));

            // Setup the scales
            currentScale = 1;
            GetFishToNextScale();

            fish = new List<Fish>();

            player = new Player();

            // Add methods to call when events are triggered
            player.OnMoved += Fish_OnMoved;
            player.OnFishEaten += Player_OnFishEaten;
        }

        /// <summary>
        /// Dispose of the scene.
        /// </summary>
        public void Dispose()
        {
            // Dispose every fish, and remove events
            fish.ForEach(o => o.Dispose());
            fish.ForEach(o => o.OnBubbleSpawned -= Fish_OnBubbleSpawned);
            fish.ForEach(o => o.OnMoved -= Fish_OnMoved);

            // Remove events from all bubbles
            bubbles.ForEach(b => b.Dispose());
            bubbles.ForEach(b => b.OnBurst -= Bubble_OnBurst);

            // Remove events from the player
            player.OnMoved -= Fish_OnMoved;
            player.OnFishEaten -= Player_OnFishEaten;
        }

        /// <summary>
        /// Restart the game.
        /// </summary>
        public void Restart()
        {
            // Dispose every fish, remove events and clear the list
            fish.ForEach(o => o.Dispose());
            fish.ForEach(o => o.OnBubbleSpawned -= Fish_OnBubbleSpawned);
            fish.ForEach(o => o.OnMoved -= Fish_OnMoved);
            fish.Clear();

            // Remove events from all bubbles and clear the list
            bubbles.ForEach(b => b.Dispose());
            bubbles.ForEach(b => b.OnBurst -= Bubble_OnBurst);
            bubbles.Clear();

            // Reset the state and the player
            state = SceneState.Playing;
            player.Reset();

            // Reset values
            bubblesBurst = 0;
            doublePoints = 0;
            points = 0;

            // Reset the scale
            currentScale = 1;
            GetFishToNextScale();
        }

        /// <summary>
        /// Update the scene.
        /// </summary>
        /// <param name="gameTime">The game time.</param>
        public void Update(GameTime gameTime)
        {
            // Call relevant update methods depending on the current state
            switch (state)
            {
                case SceneState.Title:
                    UpdateTitle();
                    break;

                case SceneState.FishSelect:
                    UpdateFishSelect();
                    break;

                case SceneState.Playing:
                    UpdatePlaying(gameTime);
                    break;

                case SceneState.Paused:
                    UpdatePaused();
                    break;

                case SceneState.End:
                    UpdateEnd();
                    break;
            }
        }

        /// <summary>
        /// Draw the scene.
        /// </summary>
        public void Draw()
        {
            // Draw the background
            GameManager.SpriteBatch.Draw(background, Vector2.Zero, Color.White);

            // Draw all objects
            bubbles.ForEach(b => b.Draw());

            fish.ForEach(o => o.Draw());

            player.Draw();

            // Draw the foreground sprites
            foreground1.Draw();

            foreground2.Draw();
        }

        /// <summary>
        /// Draw the UI.
        /// </summary>
        public void DrawUi()
        {
            // Draw the relevant UI depending on the state
            switch (state)
            {
                case SceneState.Title:
                    titleScreen.Draw();
                    break;

                case SceneState.FishSelect:
                    fishSelect.Draw();
                    break;

                case SceneState.Playing:
                    topBar.Draw();
                    pauseButton.Draw();
                    break;

                case SceneState.Paused:
                    topBar.Draw();
                    pauseMenu.Draw();
                    break;

                case SceneState.End:
                    endScreen.Draw();
                    break;
            }
        }

        /// <summary>
        /// Update the title screen.
        /// </summary>
        private void UpdateTitle()
        {
            titleScreen.Update();

            // Change the state when events have been fired
            if (EventManager.HasEventFiredThenKill(KnownEvents.OpenFishSelect))
            {
                lastState = state;
                state = SceneState.FishSelect;
            }

            if (EventManager.HasEventFiredThenKill(KnownEvents.Restart))
            {
                state = SceneState.Playing;
            }
        }

        /// <summary>
        /// Update the fish select screen.
        /// </summary>
        private void UpdateFishSelect()
        {
            fishSelect.Update();

            // If the fish select has been closed
            if (EventManager.HasEventFiredThenKill(KnownEvents.CloseFishSelect))
            {
                // Return to the state from before the fish select was opened
                state = lastState;

                // Reset the player to update the sprite if the selected fish has changed
                player.Reset();
                
                // Update the notification display on the other screens
                titleScreen.UpdateNotifications();
                endScreen.UpdateNotifications();
            }
        }

        /// <summary>
        /// Update the main game.
        /// </summary>
        /// <param name="gameTime">The game time.</param>
        private void UpdatePlaying(GameTime gameTime)
        {
            // Attempt to spawn more fish
            SpawnFish();

            // Update managers
            collisionManager.Update();
            triggerAreaManager.Update();

            // Update the UI bar with relevant information
            topBar.Update(points, currentScale, fishToNextScale, doublePoints, bubblesBurst);

            // Check for the game over event
            if (EventManager.HasEventFiredThenKill(KnownEvents.GameOver))
            {
                AudioManager.PlaySoundEffect("GameOver");

                // Trigger a check to see if any fish have been unlocked
                UnlockableFish.Unlock(currentScale, points);

                // Prepare the end screen
                endScreen.Prepare(currentScale, player.FishEatenInSession, points);

                // Reset the fish select so it can account for any new unlocks
                fishSelect.Reset();

                // Show an advert
                PlatformManager.Platform.ShowAd();

                // Change the state
                state = SceneState.End;
            }

            // Update the player
            player.Update(gameTime);

            // Update other objects
            UpdateFish(gameTime);
            UpdateBubbles(gameTime);

            // Change the highlight over the pause button depending on if it is being hovered over
            pauseButton.Highlight(pauseButton.IsHovered());

            // Check if the game has been paused
            if (InputManager.IsBindingPressed(DefaultBindings.Pause)
                || pauseButton.IsTouched())
            {
                AudioManager.PlaySoundEffect("MenuBack");

                state = SceneState.Paused;
            }
        }

        /// <summary>
        /// Update the puase menu.
        /// </summary>
        private void UpdatePaused()
        {
            pauseMenu.Update();

            // React to events triggered through the pause menu
            if (EventManager.HasEventFiredThenKill(KnownEvents.ClosePauseMenu))
            {
                state = SceneState.Playing;
            }

            if (EventManager.HasEventFiredThenKill(KnownEvents.Restart))
            {
                Restart();
            }
        }

        /// <summary>
        /// Update the game over screen.
        /// </summary>
        private void UpdateEnd()
        {
            endScreen.Update();

            // React to events triggered by the game over screen
            if (EventManager.HasEventFiredThenKill(KnownEvents.OpenFishSelect))
            {
                lastState = state;
                state = SceneState.FishSelect;
            }

            if (EventManager.HasEventFiredThenKill(KnownEvents.Restart))
            {
                Restart();
            }
        }

        /// <summary>
        /// Spawn more fish.
        /// </summary>
        private void SpawnFish()
        {
            // As long as there aren't more fish than is allowed
            if (fish.Count < maxFish)
            {
                Random random = new Random();

                float scale;

                // If there aren't enough big fish, do a small chance of spawning a big fish
                if (random.Next(0, 100) > 95
                    && bigFishCount < 5)
                {
                    scale = currentScale + 1;
                    bigFishCount++;
                }
                else
                {
                    // Otherwise spawn a fish based on the current scale
                    scale = currentScale;
                }

                // Create a new fish at a random off-screen location using the chosen scale
                Fish newFish = new Fish(UnlockableFish.Fish.First(f => f.BaseScale == ((scale % 7) == 0 ? 7 : scale % 7)),
                                new Vector2(random.Next(0, 2) == 0 ? -300 : GameManager.BaseResolutionWidth + 300, 
                                        random.Next(0, GameManager.BaseResolutionHeight)),
                                        currentScale);

                // Add methods to run when events are fired
                newFish.OnMoved += Fish_OnMoved;
                newFish.OnBubbleSpawned += Fish_OnBubbleSpawned;

                // Add the new fish to the fish list
                fish.Add(newFish);
            }
        }

        /// <summary>
        /// Called when a fish has generated a bubble.
        /// </summary>
        /// <param name="position">The position to spawn the bubble at.</param>
        private void Fish_OnBubbleSpawned(Vector2 position)
        {
            // Create a bubble with a random movement speed
            Bubble bubble = new Bubble(position, new Random().Next(2, 6));

            // Call the burst method when the bubble is burst
            bubble.OnBurst += Bubble_OnBurst;

            // Add the bubble to the list
            bubbles.Add(bubble);
        }

        /// <summary>
        /// Called when a bubble has been burst.
        /// </summary>
        private void Bubble_OnBurst()
        {
            AudioManager.PlaySoundEffect("Bubble");

            // If double points is already active
            if (doublePoints > 0)
            {
                // Slightly extend the time that double points is active
                doublePoints += 500;
            }

            // If double points is not active
            if (doublePoints <= 0)
            {
                // Increase the amount of bubbles needed to be burst to trigger double points
                bubblesBurst ++;

                // If enough bubbles have been burst
                if (bubblesBurst >= 10)
                {
                    AudioManager.PlaySoundEffect("Multiplier");

                    // Reset the counter
                    bubblesBurst = 0;

                    // Set the length of time that double points is active for
                    doublePoints = 10000;
                }
            }
        }

        /// <summary>
        /// Update all fish.
        /// </summary>
        /// <param name="gameTime">The game time.</param>
        private void UpdateFish(GameTime gameTime)
        {
            List<Fish> fishToKill = new List<Fish>();

            // Go through each fish
            foreach (Fish f in fish)
            {
                // Update the fish
                f.Update(gameTime);

                // If the fish has been eaten, add it to the kill list
                if (f.Eaten)
                {
                    fishToKill.Add(f);
                }
            }

            // Go through the kill list
            foreach (Fish f in fishToKill)
            {
                // If there are big fish then slowly decrement the counter that
                // determines when to spawn another
                if (bigFishCount > 0)
                {
                    bigFishCount -= 0.25f;
                }

                // Remove events from the fish
                f.OnMoved -= Fish_OnMoved;
                f.OnBubbleSpawned -= Fish_OnBubbleSpawned;

                // Dispose of the fish and remove it from the main list
                f.Dispose();
                fish.Remove(f);
            }
        }

        /// <summary>
        /// Update bubbles.
        /// </summary>
        /// <param name="gameTime">The game time.</param>
        private void UpdateBubbles(GameTime gameTime)
        {
            // If double points is active
            if (doublePoints > 0)
            {
                // Reduce the double points timer
                doublePoints -= gameTime.ElapsedGameTime.TotalMilliseconds;
            }
            else
            {
                doublePoints = 0;
            }

            List<Bubble> bubblesToRemove = new List<Bubble>();

            // Go through each bubble
            foreach (Bubble bubble in bubbles)
            {
                // Update the bubble
                bubble.Update(gameTime);

                // If the bubble is offscreen or the bubble has been burst
                if (bubble.Y <= 0 || bubble.IsBurst)
                {
                    // Add the bubble to the list of bubbles to remove
                    bubblesToRemove.Add(bubble);
                }
            }

            // Go through each bubble to remove
            foreach (Bubble bubble in bubblesToRemove)
            {
                // Remove the on burst event
                bubble.OnBurst -= Bubble_OnBurst;

                // Remove the bubble from the list
                bubbles.Remove(bubble);
            }
        }

        /// <summary>
        /// Rescale the fish.
        /// </summary>
        private void Rescale()
        {
            // Rescale the player with VFX
            player.ShowScaleVfx(currentScale % 7);

            // Rescale every active fish
            fish.ForEach(f => f.Rescale(currentScale % 7));
        }

        /// <summary>
        /// Get the number of fish required to reach the next scale.
        /// </summary>
        private void GetFishToNextScale()
        {
            fishToNextScale = Scales.GetFishToEat(currentScale);
        }

        /// <summary>
        /// Called when the player has eaten a fish.
        /// </summary>
        private void Player_OnFishEaten()
        {
            AudioManager.PlaySoundEffect(new Random().Next(0, 2) == 0 ? "Eat1" : "Eat2");

            // Reduce the number of fish required to eat
            fishToNextScale--;

            // Increase the number of points
            points += 10;

            // If the double points timer is active, add more points
            if (doublePoints > 0)
            {
                points += 40;
            }

            // If no more fish need to be eaten
            if (fishToNextScale <= 0)
            {
                AudioManager.PlaySoundEffect("Scale");

                // Increase the scale
                currentScale++;

                // Rescale the fish
                Rescale();

                // Work out how many fish are needed for the next scale up
                GetFishToNextScale();
            }
        }

        /// <summary>
        /// Called when a fish moves.
        /// </summary>
        /// <param name="gameObject">The game object.</param>
        private void Fish_OnMoved(GameObject gameObject)
        {
            // Add the fish to the queues to work out if they have collided with anything
            CollisionManager.AddObjectToQueue(gameObject);
            TriggerAreaManager.AddObjectToQueue(gameObject);
        }
    }
}
