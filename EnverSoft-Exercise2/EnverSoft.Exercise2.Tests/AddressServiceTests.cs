using EnverSoft.Exercise2.Models;
using EnverSoft.Exercise2.Services;

namespace EnverSoft.Exercise2.Tests
{
    /// <summary>
    /// Contains unit tests mapping out address sorting behaviors and parsing.
    /// </summary>
    public class AddressServiceTests
    {
        /// <summary>
        /// Validates that retrieving the street name effectively drops the initial house number identifier.
        /// </summary>
        [Fact]
        public void GetStreetName_Removes_House_Number()
        {
            // Arrange
            var service = new AddressService();

            // Act
            var result = service.GetStreetName("102 Long Lane");

            // Assert
            Assert.Equal("Long Lane", result);
        }

        /// <summary>
        /// Validates that multiple full addresses get successfully rearranged based on their parsed street names.
        /// </summary>
        [Fact]
        public void GetSortedAddresses_Sorts_By_Street_Name()
        {
            // Arrange
            var records = new List<PersonRecord>
            {
                new() { Address = "102 Long Lane" },
                new() { Address = "65 Ambling Way" },
                new() { Address = "82 Stewart St" },
                new() { Address = "12 Howard St" },
                new() { Address = "78 Short Lane" },
                new() { Address = "49 Sutherland St" },
                new() { Address = "8 Crimson Rd" },
                new() { Address = "94 Roland St" }
            };

            var service = new AddressService();
            
            // Act
            var result = service.GetSortedAddresses(records);

            // Assert: These should be strictly sorted alphabetically by their inner street name segments.
            var expected = new List<string>
            {
                "65 Ambling Way",
                "8 Crimson Rd",
                "12 Howard St",
                "102 Long Lane",
                "94 Roland St",
                "78 Short Lane",
                "82 Stewart St",
                "49 Sutherland St"
            };

            Assert.Equal(expected, result);
        }

        /// <summary>
        /// Ensures that the service correctly throws an ArgumentNullException when passed null.
        /// </summary>
        [Fact]
        public void GetSortedAddresses_Throws_When_Records_Are_Null()
        {
            var service = new AddressService();

            Assert.Throws<ArgumentNullException>(() => service.GetSortedAddresses(null!));
        }
    }
}
