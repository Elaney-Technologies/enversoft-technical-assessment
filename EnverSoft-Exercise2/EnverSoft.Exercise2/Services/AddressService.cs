using EnverSoft.Exercise2.Models;

namespace EnverSoft.Exercise2.Services
{
    /// <summary>
    /// Responsible for handling and sorting the address information from person records.
    /// </summary>
    public class AddressService : IAddressService
    {
        /// <summary>
        /// Retrieves a list of full addresses sorted alphabetically by the street name, ignoring the house number.
        /// </summary>
        /// <param name="records">The collection of person records to analyze.</param>
        /// <returns>A sorted list of address strings.</returns>
        /// <exception cref="ArgumentNullException">Thrown when records is null.</exception>
        public List<string> GetSortedAddresses(IEnumerable<PersonRecord> records)
        {
            // Validate arguments
            if (records == null)
            {
                throw new ArgumentNullException(nameof(records));
            }

            return records
                // Extract just the address field from every record
                .Select(r => r.Address)
                // Guard against potential null or empty addresses
                .Where(address => !string.IsNullOrWhiteSpace(address))
                // Order them using our custom street-name parsing method
                .OrderBy(address => BuildStreetSortKey(address))
                // Then sort them exactly as they are as a secondary tie-breaker
                .ThenBy(address => address)
                .ToList();
        }

        /// <summary>
        /// Extracts the street name portion by removing the leading house number.
        /// Example: "102 Long Lane" -> "Long Lane".
        /// </summary>
        /// <param name="address">The full address string.</param>
        /// <returns>The address minus the preceding number.</returns>
        public string GetStreetName(string address)
        {
            if (string.IsNullOrWhiteSpace(address))
                return string.Empty;

            // Split the address by empty space, dropping any extra whitespace
            var parts = address.Split(' ', StringSplitOptions.RemoveEmptyEntries);

            // If we don't safely have at least 2 parts (like a number and street name), just return the address as-is
            if (parts.Length <= 1)
                return address.Trim();

            // Skip the first part (the house number) and join the remainder back together to form the street name
            return string.Join(" ", parts.Skip(1));
        }

        /// <summary>
        /// Internal helper to create a consistent key for sorting by street name.
        /// </summary>
        private string BuildStreetSortKey(string address)
        {
            return GetStreetName(address).Trim().ToLowerInvariant();
        }
    }
}
