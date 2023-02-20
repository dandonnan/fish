namespace Commute.Localisation
{
    using System.Collections.Generic;
    using System.Linq;

    /// <summary>
    /// A library of strings.
    /// </summary>
    public class StringLibrary
    {
        /// <summary>
        /// The singleton instance of the string library.
        /// </summary>
        private static StringLibrary stringLibrary;

        /// <summary>
        /// A dictionary of strings.
        /// </summary>
        private readonly Dictionary<string, string> stringDictionary;

        /// <summary>
        /// A private constructor.
        /// </summary>
        private StringLibrary()
        {
            stringDictionary = PopulateDictionary();

            stringLibrary = this;
        }

        /// <summary>
        /// Initialise the string library.
        /// </summary>
        public static void Initialise()
        {
            if (stringLibrary == null)
            {
                new StringLibrary();
            }
        }

        /// <summary>
        /// Get a string.
        /// </summary>
        /// <param name="id">The id of the string.</param>
        /// <returns>The string.</returns>
        public static string GetString(string id)
        {
            // Try and get the value from the string dictionry
            if (stringLibrary.stringDictionary.TryGetValue(id, out string value) == false)
            {
                // Default to the DefaultStrings dictionary if not found
                // Note - these are the same in this implementation
                DefaultStrings.Strings.TryGetValue(id, out value);
            }

            return value;
        }

        /// <summary>
        /// Populate the string dictionary.
        /// </summary>
        /// <returns>The string dictionary.</returns>
        private Dictionary<string, string> PopulateDictionary()
        {
            // Use the dictionary in DefaultStrings
            return DefaultStrings.Strings.ToDictionary(d => d.Key, d => d.Value);
        }
    }
}
