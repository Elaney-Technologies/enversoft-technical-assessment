using EnverSoft.Exercise2.Models;

namespace EnverSoft.Exercise2.Services
{
    /// <summary>
    /// Define the contract for reading person records from a source.
    /// </summary>
    public interface ICsvReaderService
    {
        /// <summary>
        /// Reads person records from the specified path.
        /// </summary>
        List<PersonRecord> ReadRecords(string filePath);
    }
}
