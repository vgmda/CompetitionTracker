namespace CompetitionLibrary.Models
{
    /// <summary>
    /// Represents one team and its attributes, such as Id, team name and its members
    /// </summary>
    public class Team
    {
        /// <summary>
        /// The unique identifier for the team
        /// </summary>
        public int Id { get; set; }
        /// <summary>
        /// The name of the team
        /// </summary>
        public string TeamName { get; set; }
        /// <summary>
        /// A list of person model which are part of a specific team
        /// </summary>
        public List<Person> TeamMembers { get; set; } = new List<Person>();

    }
}

