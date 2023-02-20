namespace Commute
{
    using System.Collections.Generic;

    /// <summary>
    /// Handler for scaling fish.
    /// </summary>
    internal class Scales
    {
        /// <summary>
        /// Get the number of fish needed to reach the scale.
        /// </summary>
        /// <param name="scale">The scale.</param>
        /// <returns>The number of fish to eat.</returns>
        public static int GetFishToEat(int scale)
        {
            // Get the scale from the dictionary, otherwise use a default
            if (scales.TryGetValue(scale, out int toEat) == false)
            {
                toEat = 720;
            }

            return toEat;
        }

        /// <summary>
        /// A dictionary of the first 10 scales, and the number of fish required for each.
        /// </summary>
        private static Dictionary<int, int> scales = new Dictionary<int, int>
        {
            { 1, 10 },
            { 2, 15 },
            { 3, 25 },
            { 4, 40 },
            { 5, 65 },
            { 6, 105 },
            { 7, 170 }, 
            { 8, 275 },
            { 9, 445 },
            { 10, 720 }
        };
    }
}
