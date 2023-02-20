namespace Commute.Events
{
    using System;

    /// <summary>
    /// A game event.
    /// </summary>
    public class GameEvent
    {
        /// <summary>
        /// The name of the event.
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// The time the event was fired.
        /// </summary>
        public DateTime TimeTriggered { get; set; }

        /// <summary>
        /// Any data relevant to the event.
        /// </summary>
        public object Data { get; set; }
    }
}
