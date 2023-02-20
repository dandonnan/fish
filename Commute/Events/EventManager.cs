namespace Commute.Events
{
    using System;
    using System.Collections.Generic;
    using System.Linq;

    /// <summary>
    /// A manager for game events.
    /// </summary>
    public class EventManager
    {
        /// <summary>
        /// The singleton event manager instance.
        /// </summary>
        private static EventManager eventManager;

        /// <summary>
        /// A list of fired game events.
        /// </summary>
        private readonly List<GameEvent> events;

        /// <summary>
        /// A handler for when a game event is fired.
        /// </summary>
        /// <param name="gameEvent">The game event.</param>
        public delegate void EventFired(GameEvent gameEvent);

        /// <summary>
        /// The event for when an event is fired.
        /// </summary>
        public event EventFired OnEventFired;

        /// <summary>
        /// A private constructor.
        /// </summary>
        private EventManager()
        {
            events = new List<GameEvent>();

            eventManager = this;
        }

        /// <summary>
        /// Initialise the event manager.
        /// </summary>
        /// <returns>The event manager.</returns>
        public static EventManager Initialise()
        {
            if (eventManager == null)
            {
                new EventManager();
            }

            return eventManager;
        }

        /// <summary>
        /// Fire a game event.
        /// </summary>
        /// <param name="eventName">The name of the event.</param>
        public static void FireEvent(string eventName)
        {
            // Create an event using the name
            GameEvent newEvent = new GameEvent
            {
                Name = eventName,
                TimeTriggered = DateTime.Now
            };

            // Add it to the list
            eventManager.events.Add(newEvent);

            // Trigger the event fired method
            eventManager.OnEventFired(newEvent);
        }

        /// <summary>
        /// Check if an event has been fired, and if so then kill it.
        /// </summary>
        /// <param name="eventName">The name of the event.</param>
        /// <returns>true if the event was fired, false if not.</returns>
        public static bool HasEventFiredThenKill(string eventName)
        {
            bool eventFired = false;

            // Get a list of fired events which have the same name as the event being checked
            List<GameEvent> events = eventManager.events.Where(e => e.Name == eventName)
                                                        .ToList();

            // If there were any matching events
            if (events.Any())
            {
                eventFired = true;

                // Remove each of the events from the list of events
                foreach (GameEvent gameEvent in events)
                {
                    eventManager.events.Remove(gameEvent);
                }
            }

            return eventFired;
        }

        /// <summary>
        /// Called each frame.
        /// </summary>
        public void Update()
        {
            List<GameEvent> expired = new List<GameEvent>();

            DateTime currentTime = DateTime.Now;

            // Go through each event
            foreach (GameEvent gameEvent in events)
            {
                // If the event was triggered more than 1 second ago, add it to the expired list
                if (gameEvent.TimeTriggered < currentTime.AddSeconds(-1))
                {
                    expired.Add(gameEvent);
                }
            }

            // Go through each expired event and remove it from the list
            foreach (GameEvent gameEvent in expired)
            {
                events.Remove(gameEvent);
            }
        }
    }
}
