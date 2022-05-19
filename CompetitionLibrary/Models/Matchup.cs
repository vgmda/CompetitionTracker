namespace CompetitionLibrary.Models
{
    /// <summary>
    /// Represents one match in the competition
    /// </summary>
    public class Matchup
    {
        /// <summary>
        /// The unique identifier for the matchup
        /// </summary>
        public int Id { get; set; }
        /// <summary>
        /// The set of teams that were involved in this match
        /// </summary>
        public List<MatchupEntry> Entries { get; set; } = new List<MatchupEntry>();
        /// <summary>
        /// The winner of the match
        /// </summary>
        public Team Winner { get; set; }
        /// <summary>
        /// Which round this match is a part of
        /// </summary>
        public int MatchupRound { get; set; }
    }
}

