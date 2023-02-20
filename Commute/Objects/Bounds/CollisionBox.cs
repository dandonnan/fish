namespace Commute.Objects.Bounds
{
    using Microsoft.Xna.Framework;

    /// <summary>
    /// A colllision box.
    /// </summary>
    internal class CollisionBox
    {
        /// <summary>
        /// The parent game object.
        /// </summary>
        public GameObject Parent => parent;

        /// <summary>
        /// The box.
        /// </summary>
        public Rectangle Box => box;

        /// <summary>
        /// The parent game object.
        /// </summary>
        private readonly GameObject parent;

        /// <summary>
        /// The box.
        /// </summary>
        private Rectangle box;

        /// <summary>
        /// Create a collision box.
        /// </summary>
        /// <param name="parent">The parent object.</param>
        /// <param name="box">The box.</param>
        public CollisionBox(GameObject parent, Rectangle box)
        {
            this.parent = parent;
            this.box = box;

            // Register with the collision manager
            CollisionManager.Register(this);
        }

        /// <summary>
        /// Dispose of the collision box.
        /// </summary>
        public void Dispose()
        {
            CollisionManager.Deregister(this);
        }

        /// <summary>
        /// Move the box.
        /// </summary>
        /// <param name="position">The position to move to.</param>
        public void Move(Vector2 position)
        {
            box.X = (int)position.X;
            box.Y = (int)position.Y;
        }
        
        /// <summary>
        /// Get whether an object collides with the box.
        /// </summary>
        /// <param name="gameObject">The game object.</param>
        /// <returns>true if the object collides with the box, false if not.</returns>
        public bool CollidesWith(GameObject gameObject)
        {
            bool collidesWith = false;

            // If the object has a collision box and has a different parent
            if (gameObject.CollisionBox != null && gameObject != parent)
            {
                // Check for the collision
                collidesWith = box.Intersects(gameObject.CollisionBox.Box);
            }

            return collidesWith;
        }

        /// <summary>
        /// Called when an object collides with the box.
        /// </summary>
        /// <param name="gameObject">The game object.</param>
        public void Collide(GameObject gameObject)
        {
            gameObject.Collided(this);
        }
    }
}
