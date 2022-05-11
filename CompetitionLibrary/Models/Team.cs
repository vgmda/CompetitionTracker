namespace CompetitionLibrary.Models
{
    public class Team
    {
        public List<Person> TeamMembers { get; set; } = new List<Person>();

        public string TeamName { get; set; }

    }
}

