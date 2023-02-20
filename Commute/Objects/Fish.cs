namespace Commute.Objects
{
    using Commute.Audio;
    using Commute.Extensions;
    using Commute.Graphics;
    using Commute.Objects.Bounds;
    using Microsoft.Xna.Framework;
    using Microsoft.Xna.Framework.Graphics;
    using System;

    /// <summary>
    /// A fish.
    /// </summary>
    internal class Fish : GameObject
    {
        /// <summary>
        /// The size of the fish.
        /// </summary>
        public float Size => size;

        /// <summary>
        /// Whether the fish has been eaten.
        /// </summary>
        public bool Eaten => eaten;

        /// <summary>
        /// A handler for when a bubble is spawned.
        /// </summary>
        /// <param name="position">The position of the bubble.</param>
        public delegate void SpawnBubble(Vector2 position);

        /// <summary>
        /// The event for when a bubble is spawned.
        /// </summary>
        public event SpawnBubble OnBubbleSpawned;

        /// <summary>
        /// The base scale of the fish.
        /// </summary>
        private readonly float baseScale;

        /// <summary>
        /// The size of the fish.
        /// </summary>
        protected float size;

        /// <summary>
        /// The scale VFX.
        /// </summary>
        protected ScaleVfx scaleVfx;

        /// <summary>
        /// An offset for the collision box.
        /// </summary>
        protected Vector2 collisionBoxOffset;

        /// <summary>
        /// The fish's last position.
        /// </summary>
        protected Vector2 lastPosition;

        /// <summary>
        /// The fish's target position.
        /// </summary>
        protected Vector2 targetPosition;

        /// <summary>
        /// Sprite effects.
        /// </summary>
        protected SpriteEffects effects;

        /// <summary>
        /// The scale.
        /// </summary>
        protected float scale;

        /// <summary>
        /// Whether the fish has been eaten.
        /// </summary>
        protected bool eaten;

        /// <summary>
        /// A timer for spawning bubbles.
        /// </summary>
        protected double spawnBubble;

        /// <summary>
        /// An empty constructor.
        /// </summary>
        protected Fish()
        {
        }

        /// <summary>
        /// Create a fish.
        /// </summary>
        /// <param name="fishBase">The metadata for the fish.</param>
        /// <param name="position">The position.</param>
        /// <param name="scale">The scale.</param>
        public Fish(FishMetadata fishBase, Vector2 position, float scale)
        {
            baseScale = fishBase.BaseScale;

            scaleVfx = new ScaleVfx();

            // Setup the scale
            size = scale % 7;

            if (size == 0)
            {
                size = 7;
            }

            this.scale = GetScale(scale);

            if (baseScale > size || baseScale <= size - 6)
            {
                size++;
            }

            // Setup the sprite
            sprite = new Sprite(fishBase.Sprite);
            sprite.SetPosition(position);
            sprite.SetOriginToCenter();
            sprite.SetScale(this.scale);

            // Set the position
            this.position = position;
            lastPosition = position;
            targetPosition = position;

            effects = SpriteEffects.None;

            delta = 100;

            // Set a timer for spawning a bubble
            spawnBubble = new Random().Next(6000, 10000);

            movementSpeed = 1;

            // Setup the collision box
            collisionBoxOffset = new Vector2(-sprite.GetWidth() / 2, -sprite.GetHeight() / 2);

            collisionBox = new CollisionBox(this,
                            new Rectangle((int)(position.X + collisionBoxOffset.X), (int)(position.Y + collisionBoxOffset.Y),
                            sprite.GetWidth(), sprite.GetHeight()));
        }

        /// <summary>
        /// Dispose of the fish.
        /// </summary>
        public override void Dispose()
        {
            collisionBox?.Dispose();
        }

        /// <summary>
        /// Called when the fish collides with another box.
        /// </summary>
        /// <param name="collisionBox">The collision box.</param>
        public override void Collided(CollisionBox collisionBox)
        {
            // Get the parent object of the collision box
            Fish fish = (Fish)collisionBox.Parent;

            // If the other fish is smaller then eat it
            if (fish.Size < Size)
            {
                fish.Eat();
            }
        }

        /// <summary>
        /// Rescale the fish.
        /// </summary>
        /// <param name="newScale">The new scale.</param>
        public void Rescale(float newScale)
        {
            // Change the scale
            if (newScale == 0)
            {
                newScale = 7;
            }

            scale = GetScale(newScale);

            // Reduce the size
            size--;

            if (size > newScale)
            {
                size = 1;
            }

            // Update the sprite
            sprite.SetScale(scale);

            // Start VFX at the current position
            scaleVfx.Start(position);
        }

        /// <summary>
        /// Get the object type.
        /// </summary>
        /// <returns>The object type.</returns>
        public override string GetObjectType()
        {
            return "Fish";
        }

        /// <summary>
        /// Eat the fish.
        /// </summary>
        public virtual void Eat()
        {
            eaten = true;
        }

        /// <summary>
        /// Update the fish.
        /// </summary>
        /// <param name="gameTime">The game time.</param>
        public override void Update(GameTime gameTime)
        {
            // Update the scale VFX
            scaleVfx.Update(gameTime);

            // Reduce the spawn bubble timer
            spawnBubble -= gameTime.ElapsedGameTime.TotalMilliseconds;

            // If the spawn bubble timer has finished
            if (spawnBubble <= 0)
            {
                // Set a random chance of creating a bubble
                if (new Random().Next(0, 10) > 8)
                {
                    OnBubbleSpawned?.Invoke(position);
                    spawnBubble = 5000;
                }
            }

            // If the position has not reached the target
            if (position != targetPosition)
            {
                // Move to the target position
                position = position.MoveToTarget(targetPosition, movementSpeed, delta);
            }
            else
            {
                // Otherwise get a new target
                GetNewTarget();
            }

            // Update the position of the sprite
            sprite.SetPosition(position);

            Move();
        }

        /// <summary>
        /// Draw the fish.
        /// </summary>
        public override void Draw()
        {
            base.Draw();

            scaleVfx.Draw();
        }

        /// <summary>
        /// Get a new target.
        /// </summary>
        protected void GetNewTarget()
        {
            Random random = new Random();

            // Choose a random target position
            targetPosition = new Vector2(random.Next(0, GameManager.BaseResolutionWidth), random.Next(140, GameManager.BaseResolutionHeight));

            // Depending on the target position, flip the sprite
            if (targetPosition.X > position.X)
            {
                effects = effects == SpriteEffects.FlipHorizontally ? SpriteEffects.None : SpriteEffects.None;
            }

            if (targetPosition.X < position.X)
            {
                effects = effects == SpriteEffects.None ? SpriteEffects.FlipHorizontally : SpriteEffects.FlipHorizontally;
            }

            // Set the flip effect on the sprite
            sprite.SetEffects(effects);
        }

        /// <summary>
        /// Move the fish.
        /// </summary>
        protected void Move()
        {
            // Move the collision box
            collisionBox.Move(position + collisionBoxOffset);

            // Trigger the on moved event
            GameObject_OnMoved(this);
        }

        /// <summary>
        /// Get the scale of the fix.
        /// </summary>
        /// <param name="currentScale">The current scale.</param>
        /// <returns>The new scale.</returns>
        private float GetScale(float currentScale)
        {
            float newScale = 0;

            // Use the base scale and current scale to determine how big to make the fish
            switch (baseScale)
            {
                case 1:
                    newScale = currentScale == 7 || currentScale % 7 == 0 ? 1.2f :
                               currentScale == 1 || currentScale % 7 == 1 ? 0.8f : scale * 0.5f;
                    break;

                case 2:
                    newScale = currentScale == 1 || currentScale % 7 == 1 ? 1 :
                               currentScale == 2 || currentScale % 7 == 2 ? 0.5f : scale * 0.5f;
                    break;

                case 3:
                    newScale = currentScale == 2 || currentScale % 7 == 2 ? 0.9f :
                               currentScale == 3 || currentScale % 7 == 3 ? 0.4f : scale * 0.5f;
                    break;

                case 4:
                    newScale = currentScale == 3 || currentScale % 7 == 3 ? 0.9f :
                               currentScale == 4 || currentScale % 7 == 4 ? 0.5f : scale * 0.35f;
                    break;

                case 5:
                    newScale = currentScale == 4 || currentScale % 7 == 4 ? 0.6f :
                               currentScale == 5 || currentScale % 7 == 5 ? 0.3f : scale * 0.3f;
                    break;

                case 6:
                    newScale = currentScale == 5 || currentScale % 7 == 5 ? 0.6f :
                               currentScale == 6 || currentScale % 7 == 6 ? 0.3f : scale * 0.3f;
                    break;

                case 7:
                    newScale = currentScale == 6 || currentScale % 7 == 6 ? 0.6f :
                               currentScale == 7 || currentScale % 7 == 0 ? 0.3f : scale * 0.3f;
                    break;
            }

            return newScale;
        }
    }
}
