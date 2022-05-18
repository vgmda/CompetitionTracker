namespace CompetitionLibrary.Models
{
    public class Matchup
    {
        /// <summary>
        /// The unique identifier for the matchup
        /// </summary>
        public int Id { get; set; }
        public List<MatchupEntry> Entries { get; set; } = new List<MatchupEntry>();
        public Team Winner { get; set; }
        public int MatchupRound { get; set; }
    }
}

