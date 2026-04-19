using EnverSoft.Exercise2.Models;

namespace EnverSoft.Exercise2.Services
{
    /// <summary>
    /// Provides functionality for reading and parsing person records from a CSV file.
    /// </summary>
    public class CsvReaderService : ICsvReaderService
    {
        /// <summary>
        /// Reads person records from the specified CSV file path.
        /// </summary>
        /// <param name="filePath">The absolute or relative path to the CSV file.</param>
        /// <returns>A collection of parsed <see cref="PersonRecord"/> objects.</returns>
        /// <exception cref="ArgumentException">Thrown when filePath is null or empty.</exception>
        /// <exception cref="FileNotFoundException">Thrown when the file does not exist.</exception>
        /// <exception cref="InvalidOperationException">Thrown when a row has an invalid column count.</exception>
        public List<PersonRecord> ReadRecords(string filePath)
        {
            // Validate arguments
            if (string.IsNullOrWhiteSpace(filePath))
            {
                throw new ArgumentException("File path cannot be null or empty.", nameof(filePath));
            }

            // Ensure the input file exists
            if (!File.Exists(filePath))
            {
                throw new FileNotFoundException("CSV input file was not found.", filePath);
            }

            // Read all lines from the given file
            var lines = File.ReadAllLines(filePath);

            // If the file is empty or only contains a header row, return an empty list
            if (lines.Length <= 1)
            {
                return new List<PersonRecord>();
            }

            var records = new List<PersonRecord>();

            // Skip the first row (the header) and parse the rest of the file
            foreach (var line in lines.Skip(1))
            {
                // Ignore empty or whitespace-only lines
                if (string.IsNullOrWhiteSpace(line))
                    continue;

                // Split the line by commas, assuming a simple CSV layout
                var parts = line.Split(',');

                // We expect exactly 4 columns: First Name, Last Name, Address, and Phone Number
                if (parts.Length != 4)
                {
                    throw new InvalidOperationException($"Invalid CSV row. Expected 4 columns but got {parts.Length}. Row: {line}");
                }

                // Create a new record and trim whitespace from parsed columns
                records.Add(new PersonRecord
                {
                    FirstName = parts[0].Trim(),
                    LastName = parts[1].Trim(),
                    Address = parts[2].Trim(),
                    PhoneNumber = parts[3].Trim()
                });
            }

            return records;
        }
    }
}
