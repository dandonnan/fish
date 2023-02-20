namespace Commute.Objects.Bounds
{
    using System.Collections.Generic;
    using System.Linq;

    /// <summary>
    /// A collision manager.
    /// </summary>
    internal class CollisionManager
    {
        /// <summary>
        /// The singleton instance for the collision manager.
        /// </summary>
        private static CollisionManager collisionManager;

        /// <summary>
        /// A list of collision boxes.
        /// </summary>
        private readonly List<CollisionBox> collisionBoxes;

        /// <summary>
        /// A queue of objects to parse.
        /// </summary>
        private readonly List<GameObject> queue;

        /// <summary>
        /// A private constructor.
        /// </summary>
        private CollisionManager()
        {
            collisionBoxes = new List<CollisionBox>();

            queue = new List<GameObject>();

            collisionManager = this;
        }

        /// <summary>
        /// Initialise the collision manager.
        /// </summary>
        /// <returns>The collision manager.</returns>
        public static CollisionManager Initialise()
        {
            if (collisionManager == null)
            {
                new CollisionManager();
            }

            return collisionManager;
        }

        /// <summary>
        /// Add a game object to the queue of objects to parse.
        /// Collision checks are only done whenever an object is added
        /// to the queue, which should only be done whenever a collision
        /// check is necessary (as opposed to always checking).
        /// </summary>
        /// <param name="gameObject">The game object.</param>
        public static void AddObjectToQueue(GameObject gameObject)
        {
            collisionManager.queue.Add(gameObject);
        }

        /// <summary>
        /// Register a collision box with the manager.
        /// </summary>
        /// <param name="collisionBox">The collision box.</param>
        public static void Register(CollisionBox collisionBox)
        {
            collisionManager.collisionBoxes.Add(collisionBox);
        }

        /// <summary>
        /// Deregister a collision box from the manager.
        /// </summary>
        /// <param name="collisionBox">The collision box.</param>
        public static void Deregister(CollisionBox collisionBox)
        {
            collisionManager.collisionBoxes.Remove(collisionBox);
        }

        /// <summary>
        /// Update the collision boxes.
        /// </summary>
        public void Update()
        {
            // If there are any objects in the queue
            if (queue.Any())
            {
                // Go through each object in the queue
                foreach (GameObject gameObject in queue)
                {
                    // Go through each collision box
                    foreach (CollisionBox collisionBox in collisionBoxes)
                    {
                        // If the box collides, call the collide method
                        if (collisionBox.CollidesWith(gameObject))
                        {
                            collisionBox.Collide(gameObject);
                        }
                    }
                }

                // Clear the queue
                queue.Clear();
            }
        }
    }
}
