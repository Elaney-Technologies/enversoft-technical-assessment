namespace EnverSoft.Exercise2.Models
{
    /// <summary>
    /// Represents the frequency count of a specific first or last name.
    /// </summary>
    public class NameFrequency
    {
        /// <summary>
        /// Gets or sets the name being counted.
        /// </summary>
        public string Name { get; set; } = string.Empty;

        /// <summary>
        /// Gets or sets how many times this name appeared across all first and last names.
        /// </summary>
        public int Count { get; set; }
    }
}
