namespace EnverSoft.Exercise2.Models
{
    /// <summary>
    /// Represents a single person's record read from the CSV file.
    /// </summary>
    public class PersonRecord
    {
        /// <summary>
        /// Gets or sets the first name of the person.
        /// </summary>
        public string FirstName { get; set; } = string.Empty;

        /// <summary>
        /// Gets or sets the last name of the person.
        /// </summary>
        public string LastName { get; set; } = string.Empty;

        /// <summary>
        /// Gets or sets the person's physical address.
        /// </summary>
        public string Address { get; set; } = string.Empty;

        /// <summary>
        /// Gets or sets the person's phone number.
        /// </summary>
        public string PhoneNumber { get; set; } = string.Empty;
    }
}
