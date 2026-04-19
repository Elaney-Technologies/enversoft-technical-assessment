using EnverSoft.Exercise2.Models;

namespace EnverSoft.Exercise2.Services
{
    /// <summary>
    /// Responsible for analyzing name frequencies from a collection of person records.
    /// </summary>
    public class FrequencyService : IFrequencyService
    {
        /// <summary>
        /// Calculates the frequency of all occurring first and last names.
        /// The names are sorted dynamically: first by occurrence count (descending), then alphabetically (ascending).
        /// </summary>
        /// <param name="records">The collection of parsed person records.</param>
        /// <returns>A correctly sorted list of <see cref="NameFrequency"/>.</returns>
        public List<NameFrequency> GetNameFrequencies(IEnumerable<PersonRecord> records)
        {
            if (records == null)
            {
                return new List<NameFrequency>();
            }

            return records
                // Flatten the First and Last names into a single list
                .SelectMany(r => new[] { r.FirstName, r.LastName })
                // Discard any empty/null names that might have been accidentally read
                .Where(name => !string.IsNullOrWhiteSpace(name))
                // Group them by identical name strings
                .GroupBy(name => name)
                // Project the groupings to our NameFrequency model
                .Select(group => new NameFrequency
                {
                    Name = group.Key,
                    Count = group.Count() // Count the number of occurrences of this name
                })
                // Sort the items by highest count first
                .OrderByDescending(x => x.Count)
                // In cases of matching counts, break the tie by sorting alphabetically
                .ThenBy(x => x.Name)
                .ToList();
        }
    }
}
