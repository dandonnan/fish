namespace Commute.Extensions
{
    using Microsoft.Xna.Framework;
    using System;

    /// <summary>
    /// Extension methods for the Vector2 object.
    /// </summary>
    internal static class Vector2Extensions
    {
        /// <summary>
        /// Get the Vector2 when moving towards a target at a given speed.
        /// </summary>
        /// <param name="source">The starting position.</param>
        /// <param name="targetPosition">The target position.</param>
        /// <param name="speed">The speed at which to move.</param>
        /// <param name="delta">The time since the last frame.</param>
        /// <returns>The new position.</returns>
        public static Vector2 MoveToTarget(this Vector2 source, Vector2 targetPosition, float speed, float delta)
        {
            // Get movement in each axis
            float moveX = ((targetPosition.X - source.X) * speed) / delta;
            float moveY = ((targetPosition.Y - source.Y) * speed) / delta;

            moveX = moveX > 1 || moveX < -1 ? moveX : Math.Sign(moveX) * 1;
            moveY = moveY > 1 || moveY < -1 ? moveY : Math.Sign(moveY) * 1;

            // Update the position by the movement
            source.X += moveX;
            source.Y += moveY;

            // If the position is close enough to the target then set it to be the target
            if (source.X <= targetPosition.X + speed
                && source.X >= targetPosition.X - speed
                && source.Y <= targetPosition.Y + speed
                && source.Y >= targetPosition.Y - speed)
            {
                source = targetPosition;
            }

            return source;
        }
    }
}
