namespace CompetitionLibrary.Models
{
    /// <summary>
    /// Represents one person
    /// </summary>
    public class Person
    {
        /// <summary>
        /// The unique identifier for the person
        /// </summary>
        public int Id { get; set; }
        /// <summary>
        /// The first name of the person 
        /// </summary>
        public string FirstName { get; set; }
        /// <summary>
        /// The last name of the person
        /// </summary>
        public string LastName { get; set; }
        /// <summary>
        /// The primary email address of the person
        /// </summary>
        public string EmailAddress { get; set; }
        /// <summary>
        /// The primary mobile phone number of the person
        /// </summary>
        public string MobileNumber { get; set; }

        public string FullName
        {
            get
            {
                return $"{ FirstName } { LastName }";
            }
        }

    }
}

