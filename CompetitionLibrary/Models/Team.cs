namespace CompetitionLibrary.Models
{
    public class Team
    {
        public int Id { get; set; }
        public string TeamName { get; set; }
        public List<Person> TeamMembers { get; set; } = new List<Person>();
        

    }
}

