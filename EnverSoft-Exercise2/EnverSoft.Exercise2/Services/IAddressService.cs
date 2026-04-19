using EnverSoft.Exercise2.Models;

namespace EnverSoft.Exercise2.Services
{
    /// <summary>
    /// Defines the contract for address processing and sorting.
    /// </summary>
    public interface IAddressService
    {
        /// <summary>
        /// Returns a sorted list of addresses based on implementation-specific rules.
        /// </summary>
        List<string> GetSortedAddresses(IEnumerable<PersonRecord> records);

        /// <summary>
        /// Extracts the street name portion of an address string.
        /// </summary>
        string GetStreetName(string address);
    }
}
