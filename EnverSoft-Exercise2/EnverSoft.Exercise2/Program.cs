using EnverSoft.Exercise2.Services;

namespace EnverSoft.Exercise2
{
    /// <summary>
    /// The application's main entry point.
    /// </summary>
    public class Program
    {
        /// <summary>
        /// Reads dependencies, executes processing workflows, and handles writing the final txt files.
        /// </summary>
        public static void Main(string[] args)
        {
            try
            {
                // Allow passing an explicit CSV path via console arguments, otherwise default to "Data/Data.csv"
                var inputPath = args.Length > 0 ? args[0] : "Data/Data.csv";
                var frequencyOutputPath = "name_frequencies.txt";
                var addressOutputPath = "sorted_addresses.txt";

                // Initialize our necessary business logic services using interfaces
                ICsvReaderService csvReaderService = new CsvReaderService();
                IFrequencyService frequencyService = new FrequencyService();
                IAddressService addressService = new AddressService();

                // Step 1: Read all records from the CSV file
                var records = csvReaderService.ReadRecords(inputPath);

                // Ensure we have some data to work with
                if (records == null || !records.Any())
                {
                    Console.WriteLine("No records were found in the source file.");
                    return;
                }

                // Step 2: Extract and compute name frequencies, formatting them into lines right away
                var frequencyLines = frequencyService.GetNameFrequencies(records)
                    .Select(x => $"{x.Name},{x.Count}") // Format output as "Name,Frequency"
                    .ToList();

                // Step 3: Get standard addresses properly sorted alphabetically by their street names
                var sortedAddresses = addressService.GetSortedAddresses(records);

                // Step 4: Write the outputs natively to the generated text files target locations
                File.WriteAllLines(frequencyOutputPath, frequencyLines);
                File.WriteAllLines(addressOutputPath, sortedAddresses);

                // Finally, print professional success messages to the CLI
                Console.WriteLine("Processing complete.");
                Console.WriteLine($"Records processed: {records.Count}");
                Console.WriteLine($"Created: {Path.GetFullPath(frequencyOutputPath)}");
                Console.WriteLine($"Created: {Path.GetFullPath(addressOutputPath)}");
            }
            catch (Exception ex)
            {
                // Gracefully handle any unexpected runtime errors
                Console.WriteLine("An error occurred while processing the CSV data.");
                Console.WriteLine($"Error: {ex.Message}");
            }
        }
    }
}
