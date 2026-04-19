using EnverSoft.Exercise2.Models;

namespace EnverSoft.Exercise2.Services
{
    /// <summary>
    /// Defines the contract for calculating name frequencies.
    /// </summary>
    public interface IFrequencyService
    {
        /// <summary>
        /// Calculates the frequency of names in the provided records.
        /// </summary>
        List<NameFrequency> GetNameFrequencies(IEnumerable<PersonRecord> records);
    }
}
