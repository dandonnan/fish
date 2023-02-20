namespace Commute.Objects
{
    using Commute.Extensions;
    using Microsoft.Xna.Framework;

    /// <summary>
    /// A particle.
    /// </summary>
    internal class Particle
    {
        /// <summary>
        /// Get whether the particle is dead.
        /// </summary>
        public bool Dead => lifetime <= 0;

        /// <summary>
        /// The direction to move in.
        /// </summary>
        private readonly Vector2 direction;

        /// <summary>
        /// The velocity.
        /// </summary>
        private readonly int velocity;

        /// <summary>
        /// The colour.
        /// </summary>
        private readonly Color colour;

        /// <summary>
        /// The particle's lifetime.
        /// </summary>
        private double lifetime;

        /// <summary>
        /// A rectangle defining the particle.
        /// </summary>
        private Rectangle rectangle;        

        /// <summary>
        /// Create a particle.
        /// </summary>
        /// <param name="position">The position.</param>
        /// <param name="size">The size.</param>
        /// <param name="direction">The direction to move in.</param>
        /// <param name="velocity">The velocity to move at.</param>
        /// <param name="colour">The colour of the particle.</param>
        /// <param name="lifetime">The time the particle should be alive for.</param>
        public Particle(Vector2 position, int size, Vector2 direction, int velocity, Color colour, double lifetime = 1000)
        {
            rectangle = new Rectangle((int)position.X, (int)position.Y, size, size);

            this.direction = direction;
            this.velocity = velocity;
            this.colour = colour;

            this.lifetime = lifetime;
        }

        /// <summary>
        /// Update the particle.
        /// </summary>
        /// <param name="gameTime">The game time.</param>
        public void Update(GameTime gameTime)
        {
            // Update the position using the direction and velocity
            rectangle.X += (int)direction.X * velocity;
            rectangle.Y += (int)direction.Y * velocity;

            // Reduce the lifetime
            lifetime -= gameTime.ElapsedGameTime.TotalMilliseconds;
        }

        /// <summary>
        /// Draw the particle.
        /// </summary>
        public void Draw()
        {
            GameManager.SpriteBatch.FillRectangle(rectangle, colour);
        }
    }
}
