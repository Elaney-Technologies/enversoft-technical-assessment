using EnverSoft.Exercise2.Models;
using EnverSoft.Exercise2.Services;

namespace EnverSoft.Exercise2.Tests
{
    /// <summary>
    /// Contains unit tests for verifying the name frequency calculation logic.
    /// </summary>
    public class FrequencyServiceTests
    {
        /// <summary>
        /// Ensures name frequency checks combine both First and Last names, sorts by descending count, then alphabetically.
        /// </summary>
        [Fact]
        public void GetNameFrequencies_Returns_Correct_Counts_And_Order()
        {
            // Arrange: Setup mock CSV records matching the PDF use case
            var records = new List<PersonRecord>
            {
                new() { FirstName = "Jimmy", LastName = "Smith", Address = "102 Long Lane", PhoneNumber = "29384857" },
                new() { FirstName = "Clive", LastName = "Owen", Address = "65 Ambling Way", PhoneNumber = "31214788" },
                new() { FirstName = "James", LastName = "Brown", Address = "82 Stewart St", PhoneNumber = "32114566" },
                new() { FirstName = "Graham", LastName = "Howe", Address = "12 Howard St", PhoneNumber = "8766556" },
                new() { FirstName = "John", LastName = "Howe", Address = "78 Short Lane", PhoneNumber = "29384857" },
                new() { FirstName = "Clive", LastName = "Smith", Address = "49 Sutherland St", PhoneNumber = "31214788" },
                new() { FirstName = "James", LastName = "Owen", Address = "8 Crimson Rd", PhoneNumber = "32114566" },
                new() { FirstName = "Graham", LastName = "Brown", Address = "94 Roland St", PhoneNumber = "8766556" }
            };

            var service = new FrequencyService();
            
            // Act: Perform the name frequency processing calculation
            var result = service.GetNameFrequencies(records);

            // Assert: Define expectations manually and compare result counts / ordering to what we expect
            var expected = new List<(string Name, int Count)>
            {
                ("Brown", 2),
                ("Clive", 2),
                ("Graham", 2),
                ("Howe", 2),
                ("James", 2),
                ("Owen", 2),
                ("Smith", 2),
                ("Jimmy", 1),
                ("John", 1)
            };

            // Double check lengths match
            Assert.Equal(expected.Count, result.Count);

            // Double check values and their exact sorting positions
            for (int i = 0; i < expected.Count; i++)
            {
                Assert.Equal(expected[i].Name, result[i].Name);
                Assert.Equal(expected[i].Count, result[i].Count);
            }
        }

        /// <summary>
        /// Validates that passing an empty list to the frequency service returns an empty list.
        /// </summary>
        [Fact]
        public void GetNameFrequencies_Returns_Empty_List_When_No_Records()
        {
            var service = new FrequencyService();

            var result = service.GetNameFrequencies(new List<PersonRecord>());

            Assert.Empty(result);
        }
    }
}
