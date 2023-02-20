namespace Commute.Objects.Bounds
{
    using System.Collections.Generic;
    using System.Linq;

    /// <summary>
    /// A trigger area manager.
    /// </summary>
    internal class TriggerAreaManager
    {
        /// <summary>
        /// The singleton instance for the trigger area manager.
        /// </summary>
        private static TriggerAreaManager triggerAreaManager;

        /// <summary>
        /// A list of trigger areas.
        /// </summary>
        private readonly List<TriggerArea> triggerAreas;

        /// <summary>
        /// A queue of objects to parse.
        /// </summary>
        private readonly List<GameObject> queue;

        /// <summary>
        /// A private constructor.
        /// </summary>
        private TriggerAreaManager()
        {
            triggerAreas = new List<TriggerArea>();

            queue = new List<GameObject>();

            triggerAreaManager = this;
        }

        /// <summary>
        /// Initialise the trigger area manager.
        /// </summary>
        /// <returns>The trigger area manager.</returns>
        public static TriggerAreaManager Initialise()
        {
            if (triggerAreaManager == null)
            {
                new TriggerAreaManager();
            }

            return triggerAreaManager;
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
            triggerAreaManager.queue.Add(gameObject);
        }

        /// <summary>
        /// Register a trigger area with the manager.
        /// </summary>
        /// <param name="triggerArea">The trigger area.</param>
        public static void Register(TriggerArea triggerArea)
        {
            triggerAreaManager.triggerAreas.Add(triggerArea);
        }

        /// <summary>
        /// Deregister a trigger area from the manager.
        /// </summary>
        /// <param name="triggerArea">The trigger area.</param>
        public static void Deregister(TriggerArea triggerArea)
        {
            triggerAreaManager.triggerAreas.Remove(triggerArea);
        }

        /// <summary>
        /// Update the trigger areas.
        /// </summary>
        public void Update()
        {
            // If there are any objects in the queue
            if (queue.Any())
            {
                // Go through each object in the queue
                foreach (GameObject gameObject in queue)
                {
                    // Go through each trigger area
                    foreach (TriggerArea triggerArea in triggerAreas)
                    {
                        // Check if the trigger area intersects the object's collision bounds
                        bool inside = triggerArea.Area.Intersects(gameObject.CollisionBox.Box);

                        // If the object is inside but not registered then call the Enter event
                        if (inside && triggerArea.IsObjectAlreadyInside(gameObject) == false)
                        {
                            triggerArea.Enter(gameObject);
                        }

                        // If the object is not inside but is registered then call the Exit event
                        if (inside == false && triggerArea.IsObjectAlreadyInside(gameObject))
                        {
                            triggerArea.Exit(gameObject);
                        }
                    }
                }

                // Clear the queue
                queue.Clear();
            }
        }
    }
}
