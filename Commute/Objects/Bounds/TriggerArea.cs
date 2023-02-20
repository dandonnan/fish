namespace Commute.Objects.Bounds
{
    using Commute.Extensions;
    using Microsoft.Xna.Framework;
    using System.Collections.Generic;

    /// <summary>
    /// A trigger area.
    /// </summary>
    internal class TriggerArea
    {
        /// <summary>
        /// The rectangle defining the area.
        /// </summary>
        public Rectangle Area => area;

        /// <summary>
        /// A handler for when the trigger area is entered.
        /// </summary>
        /// <param name="gameObject">The object that entered the area.</param>
        public delegate void TriggerAreaEntered(GameObject gameObject);

        /// <summary>
        /// A handler for when the trigger area is exited.
        /// </summary>
        /// <param name="gameObject">The object that exited the area.</param>
        public delegate void TriggerAreaExited(GameObject gameObject);

        /// <summary>
        /// An event when the trigger area is entered.
        /// </summary>
        public event TriggerAreaEntered OnTriggerAreaEntered;

        /// <summary>
        /// An event when the trigger area is exited.
        /// </summary>
        public event TriggerAreaExited OnTriggerAreaExited;

        /// <summary>
        /// A list of game objects in the trigger area.
        /// </summary>
        private readonly List<GameObject> objectsInArea;

        /// <summary>
        /// The bounds of the area.
        /// </summary>
        private Rectangle area;

        /// <summary>
        /// Create a trigger area.
        /// </summary>
        /// <param name="area">The area.</param>
        public TriggerArea(Rectangle area)
        {
            this.area = area;

            objectsInArea = new List<GameObject>();

            // Register with the trigger area manager
            TriggerAreaManager.Register(this);
        }

        /// <summary>
        /// Dispose of the trigger area.
        /// </summary>
        public void Dispose()
        {
            TriggerAreaManager.Deregister(this);
        }

        /// <summary>
        /// Move the trigger area.
        /// </summary>
        /// <param name="position">The new position.</param>
        public void Move(Vector2 position)
        {
            area.X = (int)position.X;
            area.Y = (int)position.Y;
        }

        /// <summary>
        /// Get whether an object is already inside the trigger area.
        /// </summary>
        /// <param name="gameObject">The game object.</param>
        /// <returns>true if the object is already in the trigger area, false if not.</returns>
        public bool IsObjectAlreadyInside(GameObject gameObject)
        {
            return objectsInArea.Contains(gameObject);
        }

        /// <summary>
        /// Called when an object enters the area.
        /// </summary>
        /// <param name="gameObject">The object that entered.</param>
        public void Enter(GameObject gameObject)
        {
            // Add the object to the list
            objectsInArea.Add(gameObject);

            // Call methods listening to the on entered event
            OnTriggerAreaEntered?.Invoke(gameObject);
        }

        /// <summary>
        /// Called when an object exits the area.
        /// </summary>
        /// <param name="gameObject">The object that exited.</param>
        public void Exit(GameObject gameObject)
        {
            // Remove the object from the list
            objectsInArea.Remove(gameObject);

            // Call methods listening to the on exited event
            OnTriggerAreaExited?.Invoke(gameObject);
        }
    }
}
