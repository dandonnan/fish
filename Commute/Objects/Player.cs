namespace Commute.Objects
{
    using Commute.Events;
    using Commute.Extensions;
    using Commute.Graphics;
    using Commute.Input;
    using Commute.Objects.Bounds;
    using Commute.Save;
    using Microsoft.Xna.Framework;
    using Microsoft.Xna.Framework.Graphics;

    /// <summary>
    /// The player object.
    /// </summary>
    internal class Player : Fish
    {
        /// <summary>
        /// The number of fish eaten this session.
        /// </summary>
        public int FishEatenInSession => fishEaten;

        /// <summary>
        /// The initial position.
        /// </summary>
        private readonly Vector2 initialPosition;

        /// <summary>
        /// The number of fish eaten this session.
        /// </summary>
        private int fishEaten;

        /// <summary>
        /// A handler for when a fish is eaten.
        /// </summary>
        public delegate void FishEaten();

        /// <summary>
        /// The event for when the fish is eaten.
        /// </summary>
        public event FishEaten OnFishEaten;

        /// <summary>
        /// Create a player.
        /// </summary>
        public Player()
        {
            // Set the position to the middle of the screen
            position = new Vector2(GameManager.BaseResolutionWidth / 2, GameManager.BaseResolutionHeight / 2);

            initialPosition = position;
            lastPosition = position;
            targetPosition = position;

            scaleVfx = new ScaleVfx();

            movementSpeed = 4;
            delta = 100;

            Reset();
        }

        /// <summary>
        /// Reset the player.
        /// </summary>
        public void Reset()
        {
            // Reset scale
            size = 1;
            scale = 1;

            // Reset the position
            position = initialPosition;
            targetPosition = position;

            fishEaten = 0;

            // Get the data for the currently selected fish
            FishMetadata data = UnlockableFish.Fish[SaveManager.GameData.CurrentFish];

            // Use the data to update the sprite
            sprite = new Sprite(data.Sprite);
            sprite.SetPosition(position);
            sprite.SetOriginToCenter();

            // Set the scale based on the data
            if (data.BaseScale > 1)
            {
                sprite.SetScale((1 / data.BaseScale) + 0.25f);
            }

            // Reset the collision box
            collisionBoxOffset = new Vector2(-sprite.GetWidth() / 2, -sprite.GetHeight() / 2);

            collisionBox = new CollisionBox(this,
                            new Rectangle((int)(position.X + collisionBoxOffset.X), (int)(position.Y + collisionBoxOffset.Y),
                            sprite.GetWidth(), sprite.GetHeight()));
        }

        /// <summary>
        /// Show VFX when scaling up.
        /// </summary>
        /// <param name="currentSize">The current size.</param>
        public void ShowScaleVfx(int currentSize)
        {
            if (currentSize == 0)
            {
                currentSize = 7;
            }

            size = currentSize;
            scaleVfx.Start(position);
        }

        /// <summary>
        /// Called when colliding with another collision box.
        /// </summary>
        /// <param name="collisionBox">The collision box.</param>
        public override void Collided(CollisionBox collisionBox)
        {
            // Get the collision box's parent object
            Fish fish = (Fish)collisionBox.Parent;

            // If the parent object is smaller or the same size
            if (fish.Size <= Size)
            {
                // Eat the other fish
                fishEaten++;
                fish.Eat();
                OnFishEaten?.Invoke();
            }
            else if (fish.Size > Size)
            {
                // Otherwise, trigger the end game state
                SaveManager.GameData.FishEaten += fishEaten;
                EventManager.FireEvent(KnownEvents.GameOver);
            }
        }

        /// <summary>
        /// Get the object's type.
        /// </summary>
        /// <returns>The object's type.</returns>
        public override string GetObjectType()
        {
            return "Player";
        }

        /// <summary>
        /// Update the player.
        /// </summary>
        /// <param name="gameTime">The game time.</param>
        public override void Update(GameTime gameTime)
        {
            // Update the VFX
            scaleVfx.Update(gameTime);

            // Set the last position to the position at the end of the last frame
            lastPosition = position;

            HandleInput();

            // If the position is not the target, move to the target
            if (position != targetPosition)
            {
                position = position.MoveToTarget(targetPosition, movementSpeed, delta);
            }

            // Update the sprite
            sprite.SetPosition(position);

            Move();
        }

        /// <summary>
        /// Draw the player.
        /// </summary>
        public override void Draw()
        {
            base.Draw();
        }

        /// <summary>
        /// Set the direction of the player.
        /// </summary>
        private void SetDirection()
        {
            // Flip the sprite depending on where the target position is
            if (targetPosition.X > position.X)
            {
                effects = effects == SpriteEffects.FlipHorizontally ? SpriteEffects.None : SpriteEffects.None;
            }

            if (targetPosition.X < position.X)
            {
                effects = effects == SpriteEffects.None ? SpriteEffects.FlipHorizontally : SpriteEffects.FlipHorizontally;
            }

            sprite.SetEffects(effects);
        }

        /// <summary>
        /// Handle input.
        /// </summary>
        private void HandleInput()
        {
            // Change the target position based on movement input
            if (InputManager.IsBindingHeld(DefaultBindings.Up))
            {
                targetPosition.Y -= movementSpeed;
            }

            if (InputManager.IsBindingHeld(DefaultBindings.Down))
            {
                targetPosition.Y += movementSpeed;
            }

            if (InputManager.IsBindingHeld(DefaultBindings.Left))
            {
                targetPosition.X -= movementSpeed;
            }

            if (InputManager.IsBindingHeld(DefaultBindings.Right))
            {
                targetPosition.X += movementSpeed;
            }

            // Change the target position based on mouse / touch input
            if (InputManager.IsLeftMouseHeld() || InputManager.IsTouchHeld())
            {
                Point mouse = InputManager.GetTouchPosition();

                targetPosition.X = mouse.X > position.X ? targetPosition.X + movementSpeed :
                                   mouse.X < position.X ? targetPosition.X - movementSpeed : targetPosition.X;

                targetPosition.Y = mouse.Y > position.Y ? targetPosition.Y + movementSpeed :
                                   mouse.Y < position.Y ? targetPosition.Y - movementSpeed : targetPosition.Y;
            }

            // If the target position is not within the bounds of the screen, then
            // set it to be within the bounds
            if (targetPosition.X < 0)
            {
                targetPosition.X = 0;
            }

            if (targetPosition.X > GameManager.BaseResolutionWidth)
            {
                targetPosition.X = GameManager.BaseResolutionWidth;
            }

            if (targetPosition.Y < 140)
            {
                targetPosition.Y = 140;
            }

            if (targetPosition.Y > GameManager.BaseResolutionHeight)
            {
                targetPosition.Y = GameManager.BaseResolutionHeight;
            }

            SetDirection();
        }
    }
}
