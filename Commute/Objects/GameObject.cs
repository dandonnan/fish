namespace Commute.Objects
{
    using Commute.Graphics;
    using Commute.Objects.Bounds;
    using Microsoft.Xna.Framework;

    /// <summary>
    /// A game object.
    /// </summary>
    internal class GameObject
    {
        /// <summary>
        /// The id of the object.
        /// </summary>
        public string Id => id;

        /// <summary>
        /// The object's collision box.
        /// </summary>
        public CollisionBox CollisionBox => collisionBox;

        /// <summary>
        /// A handler for when the object moves.
        /// </summary>
        /// <param name="gameObject">The game object.</param>
        public delegate void Moved(GameObject gameObject);

        /// <summary>
        /// An event for when the object moves.
        /// </summary>
        public event Moved OnMoved;

        /// <summary>
        /// The object's id.
        /// </summary>
        protected string id;

        /// <summary>
        /// The position.
        /// </summary>
        protected Vector2 position;

        /// <summary>
        /// The origin.
        /// </summary>
        protected Vector2 origin;

        /// <summary>
        /// The collision box.
        /// </summary>
        protected CollisionBox collisionBox;

        /// <summary>
        /// The trigger area.
        /// </summary>
        protected TriggerArea triggerArea;

        /// <summary>
        /// The sprite.
        /// </summary>
        protected Sprite sprite;

        /// <summary>
        /// The speed the object moves at.
        /// </summary>
        protected int movementSpeed;

        /// <summary>
        /// A delta for updates.
        /// </summary>
        protected float delta;

        /// <summary>
        /// Dispose the object.
        /// </summary>
        public virtual void Dispose()
        {
        }

        /// <summary>
        /// Called when the object collides with another collision box.
        /// </summary>
        /// <param name="collisionBox">The other collision box.</param>
        public virtual void Collided(CollisionBox collisionBox)
        {
        }

        /// <summary>
        /// Get the type of object.
        /// </summary>
        /// <returns>The type of object.</returns>
        public virtual string GetObjectType()
        {
            return string.Empty;
        }

        /// <summary>
        /// Update the game object.
        /// </summary>
        /// <param name="gameTime">The game time.</param>
        public virtual void Update(GameTime gameTime)
        {
            sprite.Update(gameTime);
        }

        /// <summary>
        /// Draw the object.
        /// </summary>
        public virtual void Draw()
        {
            sprite.Draw();
        }

        /// <summary>
        /// Called when the object moves.
        /// </summary>
        /// <param name="gameObject">The game object.</param>
        protected virtual void GameObject_OnMoved(GameObject gameObject)
        {
            // Call methods set on the on moved event
            OnMoved?.Invoke(gameObject);
        }
    }
}
