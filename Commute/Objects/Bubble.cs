namespace Commute.Objects
{
    using Commute.Graphics;
    using Commute.Objects.Bounds;
    using Microsoft.Xna.Framework;
    using System;

    /// <summary>
    /// A bubble.
    /// </summary>
    internal class Bubble : GameObject
    {
        /// <summary>
        /// The y co-ordinate.
        /// </summary>
        public float Y => position.Y;

        /// <summary>
        /// Get whether the bubble is burst.
        /// </summary>
        public bool IsBurst => burst;

        /// <summary>
        /// A handler for when the bubble is burst.
        /// </summary>
        public delegate void Burst();

        /// <summary>
        /// An event for when the bubble is burst.
        /// </summary>
        public event Burst OnBurst;

        /// <summary>
        /// The speed the bubble moves at.
        /// </summary>
        private readonly int speed;

        /// <summary>
        /// The minimum offset in the x axis to move to.
        /// </summary>
        private readonly int minX;

        /// <summary>
        /// The maximum offset in the x axis to move to.
        /// </summary>
        private readonly int maxX;

        /// <summary>
        /// The direction the bubble moves in.
        /// </summary>
        private int direction;

        /// <summary>
        /// Whether the bubble is burst.
        /// </summary>
        private bool burst;

        /// <summary>
        /// Create a bubble.
        /// </summary>
        /// <param name="position">The position.</param>
        /// <param name="speed">The speed to move at.</param>
        public Bubble(Vector2 position, int speed)
        {
            this.position = position;
            this.speed = speed;

            // Set movement offsets
            minX = (int)position.X - 10;
            maxX = (int)position.X + 10;

            // Pick a random direction to move in
            direction = new Random().Next(0, 2) == 0 ? -1 : 1;

            sprite = SpriteLibrary.GetSprite("Bubble");
            sprite.SetPosition(position);

            // Setup the trigger area
            triggerArea = new TriggerArea(new Rectangle((int)position.X, (int)position.Y, sprite.GetWidth(), sprite.GetWidth()));

            // Call a method when the trigger area entered event triggers
            triggerArea.OnTriggerAreaEntered += TriggerArea_OnTriggerAreaEntered;
        }

        /// <summary>
        /// Dispose of the bubble.
        /// </summary>
        public override void Dispose()
        {
            triggerArea.OnTriggerAreaEntered -= TriggerArea_OnTriggerAreaEntered;
            triggerArea.Dispose();
        }

        /// <summary>
        /// Update the bubble.
        /// </summary>
        /// <param name="gameTime">The game time.</param>
        public override void Update(GameTime gameTime)
        {
            // Move the bubble
            position.Y -= speed;

            position.X += direction;

            // If the bubble has reached the movement offset, change the direction
            if (position.X <= minX)
            {
                direction = 1;
            }

            if (position.X >= maxX)
            {
                direction = -1;
            }

            // Update the sprite's position
            sprite.SetPosition(position);

            // Update the trigger area's position
            triggerArea.Move(position);
        }

        /// <summary>
        /// Called when the trigger area is entered.
        /// </summary>
        /// <param name="gameObject">The object that entered the area.</param>
        private void TriggerArea_OnTriggerAreaEntered(GameObject gameObject)
        {
            // If the object is the player
            if (gameObject.GetType() == typeof(Player))
            {
                // Burst the bubble and call the burst event
                burst = true;
                OnBurst?.Invoke();
            }
        }
    }
}
