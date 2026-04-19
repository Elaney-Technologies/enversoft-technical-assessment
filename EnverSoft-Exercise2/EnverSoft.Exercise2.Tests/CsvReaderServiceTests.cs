using EnverSoft.Exercise2.Services;

namespace EnverSoft.Exercise2.Tests
{
    /// <summary>
    /// Contains unit tests covering disk I/O bindings strictly evaluating correct extraction from realistic CSV sets.
    /// </summary>
    public class CsvReaderServiceTests
    {
        /// <summary>
        /// Checks if standard CSV formatted inputs correctly translate back into properly mapped PersonRecords models.
        /// </summary>
        [Fact]
        public void ReadRecords_Reads_Valid_Csv_File()
        {
            // Arrange: Setup a fast temp file directly to the system sandbox avoiding any file conflicts
            var tempFile = Path.GetTempFileName();

            // Dump standard CSV sample mock into the temp file matching standard exercise shape
            File.WriteAllText(tempFile,
            @"FirstName,LastName,Address,PhoneNumber
            Jimmy,Smith,102 Long Lane,29384857
            Clive,Owen,65 Ambling Way,31214788");

            var service = new CsvReaderService();
            
            // Act
            var result = service.ReadRecords(tempFile);

            // Assert: Confirm counts match the body ignoring the header 
            Assert.Equal(2, result.Count);
            
            // Confirm direct property assignments evaluate effectively and map right mapping offsets
            Assert.Equal("Jimmy", result[0].FirstName);
            Assert.Equal("Smith", result[0].LastName);
            Assert.Equal("102 Long Lane", result[0].Address);
            Assert.Equal("29384857", result[0].PhoneNumber);

            // Cleanup
            File.Delete(tempFile);
        }

        /// <summary>
        /// Verifies that the service throws an InvalidOperationException when a row contains an incorrect number of columns.
        /// </summary>
        [Fact]
        public void ReadRecords_Throws_When_Row_Has_Invalid_Column_Count()
        {
            var tempFile = Path.GetTempFileName();

            // Mock a row that has only 3 columns instead of 4
            File.WriteAllText(tempFile,
            @"FirstName,LastName,Address,PhoneNumber
            Jimmy,Smith,102 Long Lane");

            var service = new CsvReaderService();

            Assert.Throws<InvalidOperationException>(() => service.ReadRecords(tempFile));

            File.Delete(tempFile);
        }
    }
}
